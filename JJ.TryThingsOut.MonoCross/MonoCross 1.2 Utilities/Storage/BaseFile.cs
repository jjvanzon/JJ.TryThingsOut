using System;
using System.IO;
using MonoCross.Utilities.Network;

namespace MonoCross.Utilities.Storage
{
    public abstract class BaseFile : IFile
    {
        internal BaseFile()
        {
        }

        #region IFile Members

        #region Read() Methods

        public virtual byte[] Read(string filename)
        {
            return Read(filename, EncryptionMode.Default);
        }

        public virtual byte[] Read(string filename, EncryptionMode mode)
        {
            DateTime dtMetric = DateTime.UtcNow;

            if (!Exists(filename))
                return null;

            byte[] bytes = null;

            switch (mode)
            {
                case EncryptionMode.NoEncryption:
                    bytes = ReadClear(filename);
                    break;
                case EncryptionMode.Encryption:
                    bytes = Read(filename, MXDevice.Encryption.Key, MXDevice.Encryption.Salt);
                    break;
                case EncryptionMode.Default:
                    if (MXDevice.Encryption.Required)
                        bytes = Read(filename, MXDevice.Encryption.Key, MXDevice.Encryption.Salt);
                    else
                        bytes = ReadClear(filename);
                    break;
            }

            MXDevice.Log.Metric(string.Format("BaseFile.Read: Mode: {0} File: {1} Time: {2} milliseconds", mode, filename, DateTime.UtcNow.Subtract(dtMetric).TotalMilliseconds));

            return bytes;
        }

        public virtual byte[] Read(string filename, string key, byte[] salt)
        {
            DateTime dtMetric = DateTime.UtcNow;

            if (!Exists(filename))
                return null;
            byte[] bytes = ReadClear(filename);
            byte[] decrypted = MXDevice.Encryption.DecryptBytes(bytes, key, salt);

            MXDevice.Log.Metric(string.Format("BaseFile.Read(key,salt): File: {0} Time: {1} milliseconds", filename, DateTime.UtcNow.Subtract(dtMetric).TotalMilliseconds));

            return decrypted;
        }

        protected virtual byte[] ReadClear(string filename)
        {
#if WINDOWS_PHONE
            byte[] buffer;
            FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;
                buffer = new byte[length];
                int count;
                int sum = 0;
                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
#else
            return File.ReadAllBytes(filename);
#endif
        }

        #endregion

        #region ReadString() Methods

        public virtual string ReadString(string filename)
        {
            return ReadString(filename, EncryptionMode.Default);
        }

        public virtual string ReadString(string filename, EncryptionMode mode)
        {
            DateTime dtMetric = DateTime.UtcNow;

            if (!Exists(filename))
                return null;

            string text = null;

            switch (mode)
            {
                case EncryptionMode.NoEncryption:
                    text = ReadStringClear(filename);
                    break;
                case EncryptionMode.Encryption:
                    text = ReadString(filename, MXDevice.Encryption.Key, MXDevice.Encryption.Salt);
                    break;
                case EncryptionMode.Default:
                    if (MXDevice.Encryption.Required)
                        text = ReadString(filename, MXDevice.Encryption.Key, MXDevice.Encryption.Salt);
                    else
                        text = ReadStringClear(filename);
                    break;
            }
            MXDevice.Log.Metric(string.Format("BasicFile.ReadString: Mode: {0}  File: {1} Time: {2} milliseconds", mode, filename, DateTime.UtcNow.Subtract(dtMetric).TotalMilliseconds));

            return text;
        }

        public virtual string ReadString(string filename, string key, byte[] salt)
        {
            byte[] contents = Read(filename, key, salt);
            return NetworkUtils.ByteArrayToStr(contents);
        }

        public virtual string ReadStringClear(string filename)
        {
#if WINDOWS_PHONE
            byte[] bytes = ReadClear(filename);
            return System.Text.Encoding.Unicode.GetString(bytes, 0, bytes.Length);
#else
            return File.ReadAllText(filename);
            //byte[] contents = ReadClear( filename );
            //return NetworkUtils.ByteArrayToStr( contents );
#endif
        }

        #endregion


        public virtual long Length(string filename)
        {
            FileInfo fi = new FileInfo(filename);
            return fi.Length;
        }

        /// <summary>
        /// Deletes the specified file name.
        /// </summary>
        /// <param name="filename">Name of the file.</param>
        public virtual void Delete(string filename)
        {
            if (Exists(filename))
            {
                File.Delete(filename);
            }
        }

        /// <summary>
        /// Moves the specified source file name.
        /// </summary>
        /// <param name="sourcefilename">Name of the source file.</param>
        /// <param name="destinationfilename">Name of the destination file.</param>
        public virtual void Move(string sourcefilename, string destinationfilename)
        {
            // Ensure that the target does not exist.
            Delete(destinationfilename);
            File.Move(sourcefilename, destinationfilename);
        }

        /// <summary>
        /// Copies the specified source file name.
        /// </summary>
        /// <param name="sourcefilename">Name of the source file.</param>
        /// <param name="destinationfilename">Name of the destination file.</param>
        public virtual void Copy(string sourcefilename, string destinationfilename)
        {
            File.Copy(sourcefilename, destinationfilename, true);
        }

        /// <summary>
        /// Creates a directory.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        public virtual void CreateDirectory(string directoryName)
        {
            Directory.CreateDirectory(directoryName);
        }

        /// <summary>
        /// Deletes a directory.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        public virtual void DeleteDirectory(string directoryName)
        {
            Directory.Delete(directoryName);
        }

        /// <summary>
        /// Moves a directory.
        /// </summary>
        /// <param name="sourceDirectoryName">Name of the source directory.</param>
        /// <param name="destinationDirectoryName">Name of the destination directory.</param>
        public virtual void MoveDirectory(string sourceDirectoryName, string destinationDirectoryName)
        {
            Directory.Move(sourceDirectoryName, destinationDirectoryName);
        }

        /// <summary>
        /// Copies a directory.
        /// </summary>
        /// <param name="sourceDirectoryName">Name of the source directory.</param>
        /// <param name="destinationDirectoryName">Name of the destination directory.</param>
        public virtual void CopyDirectory(string sourceDirectoryName, string destinationDirectoryName)
        {
            CopyDirectory(sourceDirectoryName, destinationDirectoryName, true);
        }

        /// <summary>
        /// Copies a directory.
        /// </summary>
        /// <param name="sourceDirectoryName">Name of the source directory.</param>
        /// <param name="destinationDirectoryName">Name of the destination directory.</param>
        /// <param name="overwriteexisting">if set to <c>true</c> [overwriteexisting].</param>
        public virtual void CopyDirectory(string sourceDirectoryName, string destinationDirectoryName, bool overwriteexisting)
        {
            // Make sure the source exists first
            //if (!Directory.Exists(sourceDirectoryName)) 
            //    return;

            // Create the destination folder if needed
            if (!Directory.Exists(destinationDirectoryName))
                Directory.CreateDirectory(destinationDirectoryName);

#if !SILVERLIGHT
            if (destinationDirectoryName[destinationDirectoryName.Length - 1] != '\\')
                destinationDirectoryName += '\\';

            // Copy the files and overwrite destination files if they already exist.
            foreach (string fls in Directory.GetFiles(sourceDirectoryName))
            {
                FileInfo flInfo = new FileInfo(fls);
                flInfo.CopyTo(destinationDirectoryName + flInfo.Name, overwriteexisting);
            }

#endif
            // Copy all subfolders by calling CopyDirectory recursively
            foreach (string drs in GetDirectoryNames(sourceDirectoryName))
            {
                DirectoryInfo drInfo = new DirectoryInfo(drs);
                CopyDirectory(drs, destinationDirectoryName + drInfo.Name, overwriteexisting);
            }
        }

        /// <summary>
        /// Gets the file names for a directory.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        /// <returns></returns>
        public abstract string[] GetFileNames(string directoryName);

        /// <summary>
        /// Gets the directory name for a file.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        /// <returns></returns>
        public virtual string DirectoryName(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return string.Empty;
            
            int idx = filename.LastIndexOf(System.IO.Path.DirectorySeparatorChar);

            if (idx > 0)
                return filename.Remove(idx);

            return string.Empty;
        }

        /// <summary>
        /// Returns a value indicating whether or not the specified file name .
        /// </summary>
        /// <param name="filename">Name of the file.</param>
        /// <returns><c>true</c> if the specified file exists; otherwise, <c>false</c>.</returns>
        public virtual bool Exists(string filename)
        {
            return Directory.Exists(filename) || File.Exists(filename);
        }

        public virtual void EnsureDirectoryExists(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return;

            string dir = DirectoryName(filename);

            if (!Exists(dir) && dir.Length > 0)
                CreateDirectory(dir);
        }

        /// <summary>
        /// Gets the directory names for a directory.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        /// <returns></returns>
        public abstract string[] GetDirectoryNames(string directoryName);

        public virtual void Save(string filename, string contents)
        {
            Save(filename, contents, EncryptionMode.Default);
        }

        public virtual void Save(string filename, string contents, EncryptionMode mode)
        {
            switch (mode)
            {
                case EncryptionMode.NoEncryption:
                    SaveClear(filename, contents);
                    break;
                case EncryptionMode.Encryption:
                    Save(filename, contents, MXDevice.Encryption.Key, MXDevice.Encryption.Salt);
                    break;
                case EncryptionMode.Default:
                    if (MXDevice.Encryption.Required)
                        Save(filename, contents, MXDevice.Encryption.Key, MXDevice.Encryption.Salt);
                    else
                        SaveClear(filename, contents);
                    break;
            }
        }

        public virtual void Save(string filename, string contents, string key, byte[] salt)
        {
            byte[] bytes = NetworkUtils.StrToByteArray(contents);
            Save(filename, bytes, key, salt);
        }

        protected virtual void SaveClear(string filename, string contents)
        {
#if !WINDOWS_PHONE
            EnsureDirectoryExists(filename);
            File.WriteAllText(filename, contents);
#else
            Stream stream = new MemoryStream(System.Text.UnicodeEncoding.Unicode.GetBytes(contents));
            SaveClear(filename, stream);
#endif
        }

        public virtual void Save(string filename, Stream contents)
        {
            Save(filename, contents, EncryptionMode.Default);
        }

        public virtual void Save(string filename, Stream contents, EncryptionMode mode)
        {
            switch (mode)
            {
                case EncryptionMode.NoEncryption:
                    SaveClear(filename, contents);
                    break;
                case EncryptionMode.Encryption:
                    Save(filename, contents, MXDevice.Encryption.Key, MXDevice.Encryption.Salt);
                    break;
                case EncryptionMode.Default:
                    if (MXDevice.Encryption.Required)
                        Save(filename, contents, MXDevice.Encryption.Key, MXDevice.Encryption.Salt);
                    else
                        SaveClear(filename, contents);
                    break;
            }
        }

        public virtual void Save(string filename, Stream contents, string key, byte[] salt)
        {
            EnsureDirectoryExists(filename);

            DateTime dtMetric = DateTime.UtcNow;

            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(filename, FileMode.Create);
                MXDevice.Encryption.EncryptStream(contents, fileStream, key, salt);
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();
            }
            MXDevice.Log.Metric(string.Format("BasicFile.Save(stream, key, salt): File: {0} Time: {1} milliseconds", filename, DateTime.UtcNow.Subtract(dtMetric).TotalMilliseconds));
        }

        protected virtual void SaveClear(string filename, Stream contents)
        {
            byte[] bytes;

            EnsureDirectoryExists(filename);

            DateTime dtMetric = DateTime.UtcNow;

            using (FileStream fileStream = new FileStream(filename, FileMode.Create))
            using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
            using (BinaryReader binaryReader = new BinaryReader(contents))
            {
                try
                {
                    // process through stream in small chunks to keep peak memory usage down.
                    bytes = binaryReader.ReadBytes(8192);
                    while (bytes.Length > 0)
                    {
                        binaryWriter.Write(bytes);
                        bytes = binaryReader.ReadBytes(8192);
                    }
                }
                finally
                {
                    if (binaryWriter != null)
                        binaryWriter.Close();
                    if (fileStream != null)
                        fileStream.Close();
                    if (binaryReader != null)
                        binaryReader.Close();
                }
                MXDevice.Log.Metric(string.Format("BasicFile.SaveClear(stream): File: {0} Time: {1} milliseconds", filename, DateTime.UtcNow.Subtract(dtMetric).TotalMilliseconds));
            }
        }



        public virtual void Save(string filename, byte[] contents)
        {
            Save(filename, contents, EncryptionMode.Default);
        }

        public virtual void Save(string filename, byte[] contents, EncryptionMode mode)
        {
            switch (mode)
            {
                case EncryptionMode.NoEncryption:
                    SaveClear(filename, contents);
                    break;
                case EncryptionMode.Encryption:
                    Save(filename, contents, MXDevice.Encryption.Key, MXDevice.Encryption.Salt);
                    break;
                case EncryptionMode.Default:
                    if (MXDevice.Encryption.Required)
                        Save(filename, contents, MXDevice.Encryption.Key, MXDevice.Encryption.Salt);
                    else
                        SaveClear(filename, contents);
                    break;
            }
        }

        public virtual void Save(string filename, byte[] contents, string key, byte[] salt)
        {
            EnsureDirectoryExists(filename);

            DateTime dtMetric = DateTime.UtcNow;

            FileStream fileStream = null;
            MemoryStream plainStream = null;

            try
            {
                fileStream = new FileStream(filename, FileMode.Create);
                plainStream = new MemoryStream(contents);

                MXDevice.Encryption.EncryptStream(plainStream, fileStream, key, salt);
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();
            }
            MXDevice.Log.Metric(string.Format("BasicFile.Save(byte[],key,salt): File: {0} Time: {1} milliseconds", filename, DateTime.UtcNow.Subtract(dtMetric).TotalMilliseconds));
        }

        protected virtual void SaveClear(string filename, byte[] contents)
        {
            EnsureDirectoryExists(filename);

            DateTime dtMetric = DateTime.UtcNow;

            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write);
                fileStream.Write(contents, 0, contents.Length);
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();
            }
            MXDevice.Log.Metric(string.Format("BasicFile.SaveClear(byte[]): File: {0} Time: {1} milliseconds", filename, DateTime.UtcNow.Subtract(dtMetric).TotalMilliseconds));
        }

        #endregion



    }
}
