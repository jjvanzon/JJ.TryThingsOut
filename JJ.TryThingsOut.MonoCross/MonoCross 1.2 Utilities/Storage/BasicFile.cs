using System.IO;
using System;
using System.Security.Cryptography;
using System.IO.Compression;
using MonoCross.Utilities.Network;

namespace MonoCross.Utilities.Storage
{
    /// <summary>
    /// For now we will not be catching any exceptions, and will not be very defensive either.
    /// The business developer will have to deal with that...
    /// </summary>
    public class BasicFile : BaseFile, IFile
    {
        public BasicFile()
        {
        }

        #region IFile Members

        public override byte[] Read( string filename, string key, byte[] salt )
        {
            DateTime dtMetric = DateTime.UtcNow;

            if ( !Exists( filename ) )
                return null;

            byte[] bytes;

            FileStream fileStream = null;
            MemoryStream decryptStream = null;

            try
            {
                // Create the streams used for decryption.
                fileStream = File.Open( filename, FileMode.OpenOrCreate );
                decryptStream = new MemoryStream();

                MXDevice.Encryption.DecryptStream( fileStream, decryptStream, key, salt );

                MXDevice.Log.Metric( string.Format( "BasicFile.Read(key,salt): File: {0} Time: {1} milliseconds", filename, DateTime.UtcNow.Subtract( dtMetric ).TotalMilliseconds ) );

                bytes = decryptStream.ToArray();

                return bytes;
            }
            catch ( Exception exc )
            {
                exc.Data.Add( "filename", filename );
                MXDevice.Log.Error( exc );
                throw;
            }
            finally
            {
                if ( fileStream != null )
                    fileStream.Close();
            }
        }

        /// <summary>
        /// Gets the file names for a directory.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        /// <returns></returns>
        public override string[] GetFileNames( string directoryName )
        {
            return Directory.GetFiles( directoryName );
        }

        /// <summary>
        /// Gets the directory names for a directory.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        /// <returns></returns>
        public override string[] GetDirectoryNames( string directoryName )
        {
            return Directory.GetDirectories( directoryName );
        }

        #endregion
    }
}