using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MonoCross.Utilities.Threading
{
    public class BasicThread : IThread
    {
        public void Start( ThreadDelegate method )
        {
            SynchronizationContext context = SynchronizationContext.Current;

            // if there is no synchronization, don't launch a new thread
            if ( context != null )
            {
                // new thread to execute the Load() method for the layer
                new Thread( ( ) =>
                {
                    method.DynamicInvoke( );

                    //// fire the OnLoadComplete event on the original thread
                    //context.Post( new SendOrPostCallback( ( obj ) =>
                    //{
                    //    add callback method call here if needed in future
                    //} ), null );

                } ).Start( );
            }
            else
            {
                method.DynamicInvoke( );
            }
			
        }
        public void Start(ParameterDelegate method, object parameter)
        {
            SynchronizationContext context = SynchronizationContext.Current;

            // if there is no synchronization, don't launch a new thread
            if ( context != null )
            {
                // new thread to execute the Load() method for the layer
                new Thread( ( object args ) =>
                {
                    method.DynamicInvoke( args );

                    //// fire the OnLoadComplete event on the original thread
                    //context.Post( new SendOrPostCallback( ( obj ) =>
                    //{
                    //    add callback method call here if needed in future
                    //} ), null );

                } ).Start( parameter );
            }
            else
            {
                method.DynamicInvoke( parameter );
            }
        }

        public void QueueWorker(ParameterDelegate method)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(method));
        }
        public void QueueWorker(ParameterDelegate method, object parameter)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(method), parameter);
        }

        public void QueueIdle( ParameterizedThreadStart method )
        {
            IdleThreadQueue.Instance.Enqueue( method, null );
        }
        public void QueueIdle( ParameterizedThreadStart method, object parameter )
        {
            IdleThreadQueue.Instance.Enqueue( method, parameter );
        }

        public void DiscardIdleThread()
        {
            IdleThreadQueue.Instance.Clear();
        }
    }
}
