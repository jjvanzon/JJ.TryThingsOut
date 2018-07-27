using System.IO;
using System.IO.IsolatedStorage;
using System.Text;

namespace MonoCross.Utilities.Storage
{
    public class SLFile : BaseFile, IFile
    {
        IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();

        public SLFile()
        {
        }

        public override long Length(string fileName)
        {
            if (store.FileExists(fileName))
            {
                Stream s = null;
                try
                {
                    s = store.OpenFile(fileName, FileMode.Open, FileAccess.Read);
                    return s.Length;
                }
                finally
                {
                    if (s != null)
                        s.Close();
                }
            }
            return 0;
        }

        public override void Delete(string fileName)
        {
            if (store.FileExists(fileName))
                store.DeleteFile(fileName);
        }

        public override void Move(string sourceFileName, string destinationFileName)
        {

#if !WINDOWS_PHONE
            if ( store.FileExists( sourceFileName ) )
                store.DeleteFile( sourceFileName );
            store.MoveFile(sourceFileName, destinationFileName);
#endif
        }

        public override void Copy(string sourceFileName, string destinationFileName)
        {
#if !WINDOWS_PHONE
            store.CopyFile(sourceFileName, destinationFileName);
#endif
        }

        public override void CreateDirectory(string directoryName)
        {
            store.CreateDirectory(directoryName);
        }

        public override void DeleteDirectory(string directoryName)
        {
            store.DeleteDirectory(directoryName);
        }

        public override void MoveDirectory(string sourceDirectoryName, string destinationDirectoryName)
        {
#if !WINDOWS_PHONE
            store.MoveDirectory(sourceDirectoryName, destinationDirectoryName);
#endif
        }

        public override void CopyDirectory(string sourceDirectoryName, string destinationDirectoryName, bool overwriteexisting)
        {
#if !WINDOWS_PHONE
            // Create the destination folder if needed
            EnsureDirectoryExists(destinationDirectoryName);

            // Copy the files and overwrite destination files if they already exist.
            foreach (string file in store.GetFileNames(Path.Combine(sourceDirectoryName, "*")))
                store.CopyFile(file, Path.Combine(destinationDirectoryName, file), overwriteexisting);

            // Copy all subfolders by calling CopyDirectory recursively
            foreach (string subDirectory in store.GetDirectoryNames(Path.Combine(sourceDirectoryName, "*")))
                CopyDirectory(subDirectory, Path.Combine(destinationDirectoryName, subDirectory), overwriteexisting);
#endif
        }

        public override string[] GetFileNames(string directoryName)
        {
            return store.GetFileNames(Path.Combine(directoryName, "*"));
        }

        public override string DirectoryName(string fileName)
        {
            int index = fileName.LastIndexOf('/');
            return index < 0 ? string.Empty : fileName.Remove(index);
        }

        public override bool Exists(string fileName)
        {
            return store.DirectoryExists(fileName) || store.FileExists(fileName);
        }

        public override string[] GetDirectoryNames(string directoryName)
        {
            return store.GetDirectoryNames(Path.Combine(directoryName, "*"));
        }


        protected override void SaveClear(string filename, Stream contents)
        {
            EnsureDirectoryExists(filename);
            Delete(filename);

            if (contents.Length > store.AvailableFreeSpace &&
                !IncreaseStorage(contents.Length + (store.Quota - store.AvailableFreeSpace)))
                return;

            base.SaveClear(filename, contents);
        }

        public override void Save(string filename, Stream contents, string key, byte[] salt)
        {
            EnsureDirectoryExists(filename);
            Delete(filename);

            if (contents.Length > store.AvailableFreeSpace &&
                !IncreaseStorage(contents.Length + (store.Quota - store.AvailableFreeSpace)))
                return;

            base.Save(filename, contents, key, salt);
        }

        protected override void SaveClear(string filename, byte[] contents)
        {
            EnsureDirectoryExists(filename);
            Delete(filename);

            if (contents.Length > store.AvailableFreeSpace &&
                !IncreaseStorage(contents.Length + (store.Quota - store.AvailableFreeSpace)))
                return;

            base.SaveClear(filename, contents);
        }

        public override void Save(string filename, byte[] contents, string key, byte[] salt)
        {
            EnsureDirectoryExists(filename);
            Delete(filename);

            if (contents.Length > store.AvailableFreeSpace &&
                !IncreaseStorage(contents.Length + (store.Quota - store.AvailableFreeSpace)))
                return;

            base.Save(filename, contents, key, salt);
        }

        protected virtual bool IncreaseStorage(long spaceRequest)
        {
            //increase quota by megabytes
            const long mb = 1048576;
            long count = 1;
            while (spaceRequest > mb * count)
                count++;

            try
            {
                return store.IncreaseQuotaTo((mb * count) + store.Quota);
            }
            catch
            {
                return false;
            }
        }
    }
}