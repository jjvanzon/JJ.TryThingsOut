using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MonoCross.Utilities.Threading
{
    public class NullThread : IThread
    {
        public void Start(ThreadDelegate method) { }
        public void Start(ParameterDelegate method, object parameter) { }

        public void QueueWorker(ParameterDelegate method) { }
        public void QueueWorker(ParameterDelegate method, object parameter) { }

        public void QueueIdle( ParameterizedThreadStart method )
        {
        }
        public void QueueIdle( ParameterizedThreadStart method, object parameter )
        {
        }

        public void DiscardIdleThread()
        {
        }
    }
}
