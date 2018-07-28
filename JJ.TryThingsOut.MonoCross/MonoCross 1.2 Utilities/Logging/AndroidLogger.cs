using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if ANDROID
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
#endif

namespace MonoCross.Utilities.Logging
{
#if ANDROID
    public class AndroidLogger : BaseLogger, ILog
    {
        public const string TAG = "ifactr";
        public const string TAG_METRIC = "ifactr_metric";

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicLogger"/> class.
        /// </summary>
        /// <param name="logPath">A <see cref="String"/> representing the Log path value.</param>
        public AndroidLogger( string logPath )
            : base( logPath )
        {
        }

        #region ILog Members

        #endregion

        #region Private Help Methods

        // Help methods that use our FileSystem abstraction
        public override void AppendLog(String message, LogMessageType messageType)
        {
            string textEntry = string.Format("{0:MM-dd-yyyy HH:mm:ss:ffff} :{1}: [{2}] {3}", DateTime.Now, System.Threading.Thread.CurrentThread.ManagedThreadId, messageType.ToString(), message);

            switch (messageType)
            {
                case LogMessageType.Info:
                    Android.Util.Log.Info(TAG, message);
                    break;
                case LogMessageType.Warn:
                    Android.Util.Log.Debug(TAG, message);
                    break;
                case LogMessageType.Debug:
                    Android.Util.Log.Debug(TAG, message);
                    break;
                case LogMessageType.Error:
                    Android.Util.Log.Error(TAG, message);
                    break;
                case LogMessageType.Fatal:
                    Android.Util.Log.Wtf(TAG, message);
                    break;
                case LogMessageType.Metric:
                    Android.Util.Log.Debug( TAG_METRIC, message );
                    break;
                default:
                    break;
            }

            AppendText(LogFileName, textEntry);
        }

        #endregion

    }
#endif
}