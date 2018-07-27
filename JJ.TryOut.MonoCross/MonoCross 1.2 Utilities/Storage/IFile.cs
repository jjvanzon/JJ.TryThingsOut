using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MonoCross.Utilities.Storage
{
    /// <summary>
    /// Represents the iFactr File system abstraction. 
    /// </summary>
    public interface IFile
    {
        //Stream Create(string filename);
        //Stream Open( string filename );

        byte[] Read( string filename );
        byte[] Read( string filename, EncryptionMode mode );
        byte[] Read( string filename, string key, byte[] salt );

        string ReadString( string filename );
        string ReadString( string filename, EncryptionMode mode );
        string ReadString( string filename, string key, byte[] salt );

        void Save( string filename, string contents );
        void Save( string filename, string contents, EncryptionMode mode );
        void Save( string filename, string contents, string key, byte[] salt );

        void Save( string filename, Stream contents );
        void Save( string filename, Stream contents, EncryptionMode mode );
        void Save( string filename, Stream contents, string key, byte[] salt );

        void Save( string filename, byte[] contents );
        void Save( string filename, byte[] contents, EncryptionMode mode );
        void Save( string filename, byte[] contents, string key, byte[] salt );


        //byte[] ReadEncrypted( string filename, string key, byte[] salt );
        //string ReadStringEncrypted( string filename, string key, byte[] salt );
        //void SaveEncrypted( string filename, string contents, string key, byte[] salt );
        //void SaveEncrypted( string filename, Stream contents, string key, byte[] salt );
        //void SaveEncrypted( string filename, byte[] contents, string key, byte[] salt );

        //byte[] ReadClear( string filename );
        //string ReadStringClear( string filename );
        //void Save( string filename, string contents );
        //void Save( string filename, Stream contents );
        //void Save( string filename, byte[] contents );


        void Delete(string filename);
        bool Exists(string filename);
        void Move(string sourcefilename, string destinationfilename);
        void Copy( string sourcefilename, string destinationfilename );

        //void AppendClearText( string filename, string contents ); // no encryption saving
        long Length( string filename );

        void CreateDirectory(string directoryName);
        void DeleteDirectory(string directoryName);

        void MoveDirectory(string sourceDirectoryName, string destinationDirectoryName);

        void CopyDirectory(string sourceDirectoryName, string destinationDirectoryName);
        void CopyDirectory(string sourceDirectoryName, string destinationDirectoryName, bool overwriteexisting);

        string[] GetFileNames(string directoryName);
        string[] GetDirectoryNames(string directoryName);

        string DirectoryName(string filename);
        void EnsureDirectoryExists(string filename);
    }
}
