using System;
using System.IO;

namespace MonoCross.Utilities.Logging
{
    public abstract class BaseLogger : ILog
    {
        static readonly object _padLock = new object();

        protected readonly string _fileExt = ".log";
        protected readonly string _fileType = "_BasicLogger";

        public virtual string LogPath
        {
            get;
            protected set;
        }

        public virtual string LogFileName
        {
            get
            {
                return LogPath.AppendPath( LogHelper.GetFileNameYYYMMDD( _fileType, _fileExt ) );
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="BaseLogger"/> class.
        /// </summary>
        /// <param name="logPath">A <see cref="String"/> representing the Log path value.</param>
        public BaseLogger( string logPath )
        {
            InitLogger( logPath );
        }

        protected virtual void InitLogger( string logPath )
        {
            LogPath = logPath;
            MXDevice.File.EnsureDirectoryExists( LogFileName );
        }

        #region ILog Members

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">A <see cref="String"/> representing the Message value.</param>
        public virtual void Info( string message )
        {
            AppendLog( message, LogMessageType.Info );
        }
        /// <summary>
        /// Logs an informational message.
        /// </summary>
        public virtual void Info( Exception exc )
        {
            AppendLog( exc, LogMessageType.Info );
        }
        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">A <see cref="String"/> representing the Message value.</param>
        public virtual void Info( string message, Exception exc )
        {
            AppendLog( message, LogMessageType.Info );
            AppendLog( exc, LogMessageType.Info );
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">A <see cref="String"/> representing the Message value.</param>
        public virtual void Warn( string message )
        {
            AppendLog( message, LogMessageType.Warn );
        }
        /// <summary>
        /// Logs an warning message.
        /// </summary>
        public virtual void Warn( Exception exc )
        {
            AppendLog( exc, LogMessageType.Warn );
        }
        /// <summary>
        /// Logs an warning message.
        /// </summary>
        /// <param name="message">A <see cref="String"/> representing the Message value.</param>
        public virtual void Warn( string message, Exception exc )
        {
            AppendLog( message, LogMessageType.Warn );
            AppendLog( exc, LogMessageType.Warn );
        }

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="message">A <see cref="String"/> representing the Message value.</param>
        public virtual void Debug( string message )
        {
            AppendLog( message, LogMessageType.Debug );
        }
        /// <summary>
        /// Logs an debug message.
        /// </summary>
        public virtual void Debug( Exception exc )
        {
            AppendLog( exc, LogMessageType.Debug );
        }
        /// <summary>
        /// Logs an debug message.
        /// </summary>
        /// <param name="message">A <see cref="String"/> representing the Message value.</param>
        public virtual void Debug( string message, Exception exc )
        {
            AppendLog( message, LogMessageType.Debug );
            AppendLog( exc, LogMessageType.Debug );
        }


        /// <summary>
        /// logs an error message.
        /// </summary>
        /// <param name="message">A <see cref="String"/> representing the Message value.</param>
        public virtual void Error( string message )
        {
            AppendLog( message, LogMessageType.Error );
        }

        /// <summary>
        /// Logs an error message from the specified exception.
        /// </summary>
        /// <param name="ex">A <see cref="Exception"/> representing the Ex value.</param>
        public virtual void Error( Exception ex )
        {
            AppendLog( ex, LogMessageType.Error );
        }
        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">A <see cref="String"/> representing the Message value.</param>
        public virtual void Error( string message, Exception exc )
        {
            AppendLog( message, LogMessageType.Error );
            AppendLog( exc, LogMessageType.Error );
        }


        /// <summary>
        /// Logs a fatal error message.
        /// </summary>
        /// <param name="message">A <see cref="String"/> representing the Message value.</param>
        public virtual void Fatal( string message )
        {
            AppendLog( message, LogMessageType.Fatal );
        }

        /// <summary>
        /// Logs a fatal error message from the specified exception.
        /// </summary>
        /// <param name="ex">A <see cref="Exception"/> representing the Ex value.</param>
        public virtual void Fatal( Exception ex )
        {
            AppendLog( ex, LogMessageType.Fatal );
        }
        /// <summary>
        /// Logs an fatal message.
        /// </summary>
        /// <param name="message">A <see cref="String"/> representing the Message value.</param>
        public virtual void Fatal( string message, Exception exc )
        {
            AppendLog( message, LogMessageType.Fatal );
            AppendLog( exc, LogMessageType.Fatal );
        }

        /// <summary>
        /// Logs a Metric message.
        /// </summary>
        /// <param name="message">A <see cref="String"/> representing the Message value.</param>
        public virtual void Metric( string message )
        {
            AppendLog( message, LogMessageType.Metric );
        }

        public virtual void Metric( string message, double milliseconds )
        {
            string msg = string.Format( "{0} : Milliseconds: {1}", message, milliseconds );
            AppendLog( msg, LogMessageType.Metric );
        }

        #endregion

        #region Private Help Methods

        // Help methods that use our FileSystem abstraction
        public virtual void AppendLog( String message, LogMessageType messageType )
        {
            string textEntry = string.Format( "{0:MM-dd-yyyy HH:mm:ss:ffff} :{1}: [{2}] {3}", DateTime.Now, System.Threading.Thread.CurrentThread.ManagedThreadId, messageType.ToString(), message );

            AppendText( LogFileName, textEntry );
        }

        public virtual void AppendLog( Exception ex, LogMessageType messageType )
        {
            AppendLog( LogHelper.BuildExceptionMessage( ex, messageType ), messageType );
        }

        #endregion


        #region File Helper Methods

        /// <summary>
        /// Appends text to the FileStream using a StreamWriter.
        /// StreamWriter similar to FileStream shall be fine for all platforms.
        /// </summary>
        /// <param name="fs">A <see cref="FileStream"/> representing the Fs value.</param>
        /// <param name="value">A <see cref="String"/> representing the Value value.</param>
        protected virtual void AppendText( FileStream fs, string value )
        {
            fs.Seek( 0, SeekOrigin.End );
            StreamWriter sw = new StreamWriter( fs, System.Text.Encoding.UTF8 );
            sw.WriteLine( value );
            sw.Close();
        }

        /// <summary>
        /// Appends text to the FileStream using a StreamWriter.
        /// StreamWriter similar to FileStream shall be fine for all platforms.
        /// </summary>
        /// <param name="filename">A <see cref="String"/> representing the file location.</param>
        /// <param name="value">A <see cref="String"/> representing the Value value.</param>
        protected virtual void AppendText( string filename, string value )
        {
            lock ( _padLock )
            {

                FileStream fs = null;
                StreamWriter sw = null;
                try
                {
                    //TODO: This needs to use a FileStream from MXDevice.File. This block of code doesn't work in Silverlight.
                    // COUNTERMAND ABOVE TODO:  Use alternate method to AVOID an infinite loop.
                    fs = new FileStream( filename, FileMode.OpenOrCreate, FileAccess.ReadWrite );
                    fs.Seek( 0, SeekOrigin.End );
                    sw = new StreamWriter( fs, System.Text.Encoding.UTF8 );
                    sw.WriteLine( value );
                }
                catch ( Exception )
                {
                    //Swallow any errors here... If we tried logging our error, we might end up back here.
                }
                finally
                {
                    if ( sw != null )
                        sw.Close();
                }
            }
        }

        //protected virtual void AddText(FileStream fs, string value)
        //{
        //    byte[] info = new UTF8Encoding(true).GetBytes(value);
        //    fs.Write(info, 0, info.Length);
        //}


        #endregion



    }
}