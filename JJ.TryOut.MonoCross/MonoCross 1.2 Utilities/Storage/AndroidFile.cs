using System.IO;
using System.Linq;
#if ANDROID
using Android.Content.Res;
#endif

namespace MonoCross.Utilities.Storage
{
#if ANDROID
    public class AndroidFile : BaseFile, IFile
    {
        public AssetManager AssetManager { get; set; }

        public AndroidFile()
        {
        }

        private string AssetFromFileName(string filename)
        {
            if (filename.StartsWith(MXDevice.Factory.ApplicationPath))
                return filename.Substring(MXDevice.Factory.ApplicationPath.Length);
            return filename;
        }

        #region IFile Members

        /// <summary>
        /// Copies the specified source file name.
        /// </summary>
        /// <param name="sourcefilename">Name of the source file.</param>
        /// <param name="destinationfilename">Name of the destination file.</param>
        public override void Copy(string sourcefilename, string destinationfilename)
        {
            if (!sourcefilename.StartsWith(MonoCross.MXDevice.Factory.ApplicationPath))
            {
                base.Copy(sourcefilename, destinationfilename);
                return;
            }

            using (Stream input = AssetManager.Open(AssetFromFileName(sourcefilename)))
            using (Stream output = File.Create(destinationfilename))
            {
                byte[] buffer = new byte[6144];
                int len;
                try
                {
                    while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
                        output.Write(buffer, 0, len);
                }
                finally
                {
                    if (input != null)
                        input.Close();
                    if (output != null)
                        output.Close();
                }
            }
        }

        protected override byte[] ReadClear(string filename)
        {
            if (!filename.StartsWith(MXDevice.Factory.ApplicationPath))
            {
                return base.ReadClear(filename);
            }

            using (Stream input = AssetManager.Open(AssetFromFileName(filename)))
            using (MemoryStream output = new MemoryStream())
            {
                byte[] buffer = new byte[6144];
                int len;
                try
                {
                    while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
                        output.Write(buffer, 0, len);
                    return output.ToArray();
                }
                finally
                {
                    if (input != null)
                        input.Close();
                    if (output != null)
                        output.Close();
                }
            }
        }

        /// <summary>
        /// Gets the file names for a directory.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        /// <returns></returns>
        public override string[] GetFileNames(string directoryName)
        {
            return Directory.GetFiles(directoryName);
        }

        /// <summary>
        /// Gets the directory names for a directory.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        /// <returns></returns>
        public override string[] GetDirectoryNames(string directoryName)
        {
            return Directory.GetDirectories(directoryName);
        }

        /// <summary>
        /// Returns a value indicating whether or not the specified file name exists.
        /// </summary>
        /// <param name="filename">Name of the file.</param>
        /// <returns><c>true</c> if the specified file exists; otherwise, <c>false</c>.</returns>
        public override bool Exists(string filename)
        {
            if (filename.StartsWith(MonoCross.MXDevice.Factory.ApplicationPath))
            {
                filename = AssetFromFileName(filename);
                int separator = filename.LastIndexOf('/') + 1;
                return AssetManager.List(filename.Remove(separator).Trim('/')).Contains(filename.Substring(separator));
            }
            return base.Exists(filename);
        }
        #endregion
    }
#endif
}