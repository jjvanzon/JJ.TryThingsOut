using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MonoCross.Utilities.Threading
{
    public class MockThread : IThread
    {
        public void Start(ThreadDelegate method) { method.DynamicInvoke(); }
        public void Start(ParameterDelegate method, object parameter) { method.DynamicInvoke(parameter); }

        public void QueueWorker(ParameterDelegate method) { method.DynamicInvoke(); }
        public void QueueWorker(ParameterDelegate method, object parameter) { method.DynamicInvoke(parameter); }

        public void QueueIdle( ParameterizedThreadStart method )
        {
            method.DynamicInvoke((object) null);
        }
        public void QueueIdle( ParameterizedThreadStart method, object parameter )
        {
            method.DynamicInvoke( parameter );
        }
        public void DiscardIdleThread()
        {
        }
    }
}
