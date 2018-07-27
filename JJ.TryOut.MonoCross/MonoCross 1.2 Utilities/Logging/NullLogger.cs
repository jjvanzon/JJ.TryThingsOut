using System;

namespace MonoCross.Utilities.Logging
{
    internal class NullLogger : ILog
    {
        #region ILog Members
        public void Info(string message) { }
        public void Info(Exception ex) { }
        public void Info(string message, Exception ex) { }

        public void Warn(string message) { }
        public void Warn(Exception ex) { }
        public void Warn(string message, Exception ex) { }

        public void Debug(string message) { }
        public void Debug(Exception ex) { }
        public void Debug(string message, Exception ex) { }

        public void Error(string message) { }
        public void Error(Exception ex) { }
        public void Error(string message, Exception ex) { }

        public void Fatal(string message) { }
        public void Fatal(Exception ex) { }
        public void Fatal(string message, Exception ex) { }
		
		public void Metric( string message ) { }
        public void Metric( string message, double milliseconds ) { }
        #endregion
    }
}