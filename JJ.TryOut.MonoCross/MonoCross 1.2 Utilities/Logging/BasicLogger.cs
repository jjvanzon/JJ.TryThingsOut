using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using MonoCross.Utilities.Storage;

namespace MonoCross.Utilities.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public class BasicLogger : BaseLogger, ILog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicLogger"/> class.
        /// </summary>
        /// <param name="logPath">A <see cref="String"/> representing the Log path value.</param>
        public BasicLogger(string logPath)
            : base(logPath) { }

        #region Private Help Methods

        // Help methods that use our FileSystem abstraction
        public override void AppendLog(String message, LogMessageType messageType)
        {
            string textEntry = string.Format("{0:MM-dd-yyyy HH:mm:ss:ffff} :{1}: [{2}] {3}", DateTime.Now, System.Threading.Thread.CurrentThread.ManagedThreadId, messageType.ToString(), message);

            Console.WriteLine(textEntry);

            AppendText(LogFileName, textEntry);
        }

        #endregion

    }

}