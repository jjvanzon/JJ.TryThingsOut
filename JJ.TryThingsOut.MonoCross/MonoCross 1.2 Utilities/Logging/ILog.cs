using System;

namespace MonoCross.Utilities.Logging
{
    /// <summary>
    /// Abstract our logging with an interface, so we can easily swap loggers if needed later.
    /// This interface is easily implemented by NLog and Log4Net.
    /// </summary>
    public interface ILog
    {
        void Info(string message);
        void Info(Exception ex);
        void Info(string message, Exception ex);

        void Debug(string message);
        void Debug(Exception ex);
        void Debug(string message, Exception ex);

        void Warn(string message);
        void Warn(Exception ex);
        void Warn(string message, Exception ex);

        void Error(string message);
        void Error(Exception ex);
        void Error(string message, Exception ex);

        void Fatal(string message);
        void Fatal(Exception ex);
        void Fatal(string message, Exception ex);

        void Metric( string message );
        void Metric( string message, double milliseconds );
    }
}