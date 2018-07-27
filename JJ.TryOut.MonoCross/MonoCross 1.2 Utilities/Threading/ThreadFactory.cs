using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoCross.Utilities.Threading
{
    /// <summary>
    /// Factory for the creation of a thread access strategy.
    /// </summary>
    public static class ThreadFactory
    {
        /// <summary>
        /// Creates an IThread instance.
        /// </summary>
        /// <returns></returns>
        public static IThread Create()
        {
            IThread file = new BasicThread();
            return file;
        }

        // If we ever want to make an implementation of IThread that we want in core,
        /// <summary>
        /// Creates an IThread instance of the specified thread type.
        /// </summary>
        /// <param name="threadType">Type of the file.</param>
        /// <returns></returns>
        public static IThread Create(ThreadType threadType)
        {
            IThread thread = new BasicThread();

            switch (threadType)
            {
                case ThreadType.MockThread:
                    thread = new MockThread();
                    break;
                default:
                    // returns the default - BasicFile implementation                 
                    break;
            }

            return thread;
        }
    }

    /// <summary>
    /// Indicates the thread type to use
    /// </summary>
    public enum ThreadType
    {
        /// <summary>
        /// Basic thread type (default).
        /// </summary>
        BasicThread,
        /// <summary>
        /// Mock thread type for platforms that don't support threading.
        /// </summary>
        MockThread
        //XmlBasicFile
    }
}
