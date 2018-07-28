using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MonoCross.Utilities.Network;
using MonoCross.Navigation;

namespace MonoCross.Utilities.Threading
{
    public class IdleQueueDelegateCall
    {
        public ParameterizedThreadStart Delegate
        {
            get;
            set;
        }
        public Object Item
        {
            get;
            set;
        }
    }

    internal class IdleThreadQueue : Queue<IdleQueueDelegateCall>
    {
        const int MAXTHREADS = 10;
        // The EventWaitHandle used to demonstrate the difference
        // between AutoReset and ManualReset synchronization events.
        //
        private static EventWaitHandle ewh;

        // A counter to make sure all threads are started and
        // blocked before any are released. A Long is used to show
        // the use of the 64-bit Interlocked methods.
        //
        private static long threadCount = 0;
        private static long ThreadCountSafeRead
        {
            get { lock (typeof(IdleThreadQueue)) { return threadCount; } }
        }

        // An AutoReset event that allows the main thread to block
        // until an exiting thread has decremented the count.
        //
        private static EventWaitHandle clearCount =
            new AutoResetEvent(false);

        private static IdleThreadQueue _queue;
        public static IdleThreadQueue Instance
        {
            get
            {
                if (_queue == null)
                    _queue = new IdleThreadQueue();

                return _queue;
            }
        }

        private Timer timer;
        //private const int timerDelay = 500;

        private object syncLock = new object();

        private bool _enabled = true;
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
                if (value)
                    TriggerTimer();
                else
                    timer = null;
            }
        }

        private IdleThreadQueue()
        {
            MXDevice.OnNetworkResponse += new NetworkResponseEvent(iApp_OnNetworkResponse);
            ewh = new AutoResetEvent(false);
        }

        void iApp_OnNetworkResponse(NetworkResponse networkResponse)
        {

        }

        private void TriggerTimer()
        {
            if (!Enabled)
                return;

            if (timer != null)
            {
                // execute again in 10 seconds.
                timer.Change(10 * 1000, Timeout.Infinite);
            }
            else
            {
                timer = new Timer(new TimerCallback((o) =>
                {
                    ProcessQueue();
                }), null, 1 * 1000, Timeout.Infinite);
            }
        }

        public new void Enqueue(IdleQueueDelegateCall idleDelegateCall)
        {
            lock (syncLock)
            {
                base.Enqueue(idleDelegateCall);
            }

            TriggerTimer();
        }

        public void Enqueue(ParameterizedThreadStart idleQueueDelegate, Object item)
        {
            IdleQueueDelegateCall idleDelegateCall = new IdleQueueDelegateCall()
            {
                Delegate = idleQueueDelegate,
                Item = item
            };

            lock (syncLock)
            {
                base.Enqueue(idleDelegateCall);
            }
            TriggerTimer();
        }

        public void ProcessQueue()
        {
            //MXDevice.Log.Info("ProcessQueue:  Calling StageDelegates.");
            StageDelegates();

            // Release one thread each time the user presses ENTER,
            // until all threads have been released.
            while (ThreadCountSafeRead > 0 && IsIdle)
            {
                //MXDevice.Log.Debug(string.Format("ProcessQueue:  start of loop. staged count {0}  queue count {1}", Interlocked.Read(ref threadCount), this.Count));

                // SignalAndWait signals the EventWaitHandle, which
                // releases exactly one thread before resetting, 
                // because it was created with AutoReset mode. 
                // SignalAndWait then blocks on clearCount, to 
                // allow the signaled thread to decrement the count
                // before looping again.


                // To-Do: Find solution and refactor for Silverlight, or create a silverlight threading class like BasicThread
#if !SILVERLIGHT
                WaitHandle.SignalAndWait(ewh, clearCount);
#endif
            }

            //if ( this.Count > 0 )
            //{
            //    MXDevice.Log.Info( "ProcessQueue:  Calling trigger for next round." );
            TriggerTimer();
            //}
        }

        public void StageDelegates()
        {
            if (this.Count == 0)
                return;

            //lock ( syncLock )
            //{
            long lng = ThreadCountSafeRead;

            for (int i = 0; i < (MAXTHREADS - lng); i++)
            {
                if (this.Count > 0)
                    StageNext();
            }
            //}
        }

        private void StageNext()
        {
            lock (syncLock)
            {
                if (this.Count <= 0)
                    return;

                IdleQueueDelegateCall nextCall = Peek();

                //if ( nextCall == null )
                //{
                //    Dequeue();
                //    continue;
                //}

                try
                {
                    MXDevice.Log.Debug(string.Format("StageNext:  Adding delegate to thread. staged count {0}  queue count {1}", ThreadCountSafeRead, this.Count));

                    AddDelegateToThread(nextCall);
                    Dequeue();
                }
                catch (Exception exc)
                {
                    MXDevice.Log.Error(exc);
                }
            }
        }

        private void AddDelegateToThread(IdleQueueDelegateCall call)
        {
            Thread t = new Thread(
                new ParameterizedThreadStart(IdleThreadProc)
            );
            t.Start(call);
        }

        public void IdleThreadProc(Object obj)
        {
            IdleQueueDelegateCall call = (IdleQueueDelegateCall)obj;

            // Increment the count of blocked threads.
            Interlocked.Increment(ref threadCount);

            // Wait on the EventWaitHandle.
            ewh.WaitOne();

            // invoke command now.
            call.Delegate.DynamicInvoke(call.Item);
            // alternatively, we could release to thread pool
            //MXDevice.Thread.QueueWorker( call.Delegate, call.Item );


            // Decrement the count of blocked threads.
            Interlocked.Decrement(ref threadCount);

            // After signaling ewh, the main thread blocks on
            // clearCount until the signaled thread has 
            // decremented the count. Signal it now.
            //
            clearCount.Set();

            // refill stage list from queue
            StageNext();
        }

        //Random rand = new Random();
        public bool IsIdle
        {
            get
            {
                return DateTime.Now.Subtract(MXContainer.Instance.LastNavigationDate).Seconds > 10;
            }
        }

        ///// <summary>
        ///// Removes serialized file for queue and discards all queue contents
        ///// </summary>
        //public void DiscardQueue()
        //{
        //    lock ( syncLock )
        //    {
        //        this.Clear();
        //    }
        //}

    }
}
