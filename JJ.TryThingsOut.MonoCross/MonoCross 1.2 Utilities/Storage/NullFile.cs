using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MonoCross.Utilities.Storage
{
    internal class NullFile : IFile
    {
        #region IFile Members

        public byte[] Read( string fileName )
        {
            return null; //            File.OpenWrite( fileName );
        }
        public string ReadString( string fileName )
        {
            return null; //            File.OpenWrite( fileName );
        }
        public void Delete(string fileName) { }
        public long Length(string fileName) { return 0; }
        public void Move(string sourceFileName, string destinationFileName) { }
        public void Copy(string sourceFileName, string destinationFileName) { }
        public void CreateDirectory(string directoryName) { }
        public void DeleteDirectory(string directoryName) { }
        public void MoveDirectory(string sourceDirectoryName, string destinationDirectoryName) { }


        public void CopyDirectory(string sourceDirectoryName, string destinationDirectoryName)
        {
        }

        public void CopyDirectory(string sourceDirectoryName, string destinationDirectoryName, bool overwriteexisting)
        {
        }


        public string[] GetFileNames(string directoryName)
        {
            return new string[] { };
        }

        public string[] GetDirectoryNames(string directoryName)
        {
            return new string[] { };
        }

        public bool Exists(string fileName)
        {
            return false;
        }

        public string DirectoryName(string fileName)
        {
            return string.Empty;
        }

        public void EnsureDirectoryExists(string fileName) { }

        public void Save(string fileName, string contents) { }
        public void Save(string fileName, Stream contents) { }
        public void Save(string fileName, byte[] contents) { }
        #endregion


        public byte[] Read( string fileName, EncryptionMode mode )
        {
            return null; 
        }

        public byte[] Read( string fileName, string key, byte[] salt )
        {
            return null; 
        }

        public string ReadString( string fileName, EncryptionMode mode )
        {
            return null; 
        }

        public string ReadString( string fileName, string key, byte[] salt )
        {
            return null; 
        }

        public void Save( string fileName, string contents, EncryptionMode mode )
        {
        }

        public void Save( string fileName, string contents, string key, byte[] salt )
        {
        }

        public void Save( string fileName, Stream contents, EncryptionMode mode )
        {
        }

        public void Save( string fileName, Stream contents, string key, byte[] salt )
        {
        }

        public void Save( string fileName, byte[] contents, EncryptionMode mode )
        {
        }

        public void Save( string fileName, byte[] contents, string key, byte[] salt )
        {
        }
    }
}
