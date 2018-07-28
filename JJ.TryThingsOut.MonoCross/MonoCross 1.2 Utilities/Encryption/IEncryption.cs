using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MonoCross.Utilities.Encryption
{
    public interface IEncryption
    {
        bool Required
        {
            get;
            set;
        }
        string Key
        {
            get;
            set;
        }
        byte[] Salt
        {
            get;
            set;
        }

        string EncryptString( string txt );
        string EncryptString( string txt, string key, byte[] salt );
        string DecryptString( string cipher );
        string DecryptString( string cipher, string key, byte[] salt );

        byte[] EncryptBytes( byte[] bytes );
        byte[] EncryptBytes( byte[] bytes, string key, byte[] salt );
        byte[] DecryptBytes( byte[] cipher );
        byte[] DecryptBytes( byte[] cipher, string key, byte[] salt );

        void EncryptStream( Stream inputStream, Stream outputStream );
        void EncryptStream( Stream inputStream, Stream outputStream, string key, byte[] salt );
        void DecryptStream( Stream inputStream, Stream outputStream );
        void DecryptStream( Stream inputStream, Stream outputStream, string key, byte[] salt );
    }
}
