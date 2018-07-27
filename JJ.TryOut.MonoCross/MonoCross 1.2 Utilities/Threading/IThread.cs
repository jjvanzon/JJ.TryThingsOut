using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MonoCross.Utilities.Threading
{
    public delegate void ThreadDelegate();
    public delegate void ParameterDelegate(object parameter);

    public interface IThread
    {
        void Start(ThreadDelegate method);
        void Start(ParameterDelegate method, object parameter);

        void QueueWorker(ParameterDelegate method);
        void QueueWorker(ParameterDelegate method, object parameter);

        void QueueIdle( ParameterizedThreadStart method );
        void QueueIdle( ParameterizedThreadStart method, object parameter );

        void DiscardIdleThread();

        // To-Do: add these
        //void ExecuteOnMainThread(Delegate method);
        //void ExecuteOnMainThread(Delegate method, object parameter);
    }

}
