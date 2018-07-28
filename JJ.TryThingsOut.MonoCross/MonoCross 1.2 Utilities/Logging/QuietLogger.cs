using System;

namespace MonoCross.Utilities.Logging
{
    /// <summary>
    /// This class is just like BasicLogger, except it doesn't output to the console window.
    /// </summary>
    public class QuietLogger : BaseLogger, ILog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuietLogger"/> class.
        /// </summary>
        /// <param name="logPath">A <see cref="String"/> representing the Log path value.</param>
        public QuietLogger(string logPath) : base(logPath)
        {
        }

        #region Private Help Methods

        // Help methods that use our FileSystem abstraction
        public override void AppendLog(String message, LogMessageType messageType)
        {
            string textEntry = string.Format("{0:MM-dd-yyyy HH:mm:ss:ffff} :{1}: [{2}] {3}", DateTime.Now, System.Threading.Thread.CurrentThread.ManagedThreadId, messageType.ToString(), message);
            AppendText(LogFileName, textEntry);
        }

        #endregion

     }

}