using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using MonoCross.Utilities.Network;

namespace MonoCross.Utilities.Encryption
{
    public abstract class BaseEncryption : IEncryption
    {
        const int BUFFER_SIZE = 8192;

        #region Public Properties

        public virtual bool Required
        {
            get;
            set;
        }

        public virtual string Key
        {
            get;
            set;
        }

        public virtual byte[] Salt
        {
            get;
            set;
        }

        #endregion

        protected virtual AesManaged GetAesManaged( string aesKey, byte[] salt )
        {
            if ( string.IsNullOrEmpty( aesKey ) )
                throw new ArgumentNullException( "aesKey" );
            if ( salt == null )
                throw new ArgumentNullException( "salt" );

            Rfc2898DeriveBytes rdb = new Rfc2898DeriveBytes( aesKey, salt );
            byte[] key = rdb.GetBytes( 32 );
            byte[] iv = rdb.GetBytes( 16 );

            if ( key == null || key.Length <= 0 )
                throw new ArgumentNullException( "key" );
            if ( iv == null || iv.Length <= 0 )
                throw new ArgumentNullException( "iv" );

            return new AesManaged()
            {
                Key = key,
                IV = iv
            };
        }

        #region String Encryption/Decryption Methods

        public virtual string EncryptString( string txt )
        {
            return EncryptString( txt, Key, Salt );
        }

        public virtual string EncryptString( string txt, string key, byte[] salt )
        {
            if ( string.IsNullOrEmpty( txt ) )
                return string.Empty;

            byte[] plain = NetworkUtils.StrToByteArray( txt );
            byte[] encrypted = EncryptBytes( plain, key, salt );

            return Convert.ToBase64String( encrypted, 0, encrypted.Length );
        }

        public virtual string DecryptString( string cipher )
        {
            return DecryptString( cipher, Key, Salt );
        }

        public virtual string DecryptString( string cipher, string key, byte[] salt )
        {
            if ( string.IsNullOrEmpty( cipher ) )
                return string.Empty;

            byte[] cipherBytes = Convert.FromBase64String( cipher );
            byte[] decrypted = DecryptBytes( cipherBytes, key, salt );

            return NetworkUtils.ByteArrayToStr( decrypted );
        }

        #endregion

        #region Byte Encryption/Decryption Methods

        public virtual byte[] EncryptBytes( byte[] contents )
        {
            return EncryptBytes( contents, Key, Salt );
        }

        public virtual byte[] EncryptBytes( byte[] contents, string key, byte[] salt )
        {
            MemoryStream msInput = new MemoryStream(contents);
            MemoryStream msOutput = new MemoryStream(  );

            EncryptStream( msInput, msOutput, key, salt );

            byte[] retArray = msOutput.ToArray();
            return retArray;
        }

        public virtual byte[] DecryptBytes( byte[] cipher )
        {
            return DecryptBytes( cipher, Key, Salt );
        }

        public virtual byte[] DecryptBytes( byte[] cipher, string key, byte[] salt )
        {
            MemoryStream msInput = new MemoryStream( cipher );
            MemoryStream msOutput = new MemoryStream();

            DecryptStream( msInput, msOutput, key, salt );

            byte[] retArray = msOutput.ToArray();
            return retArray;
        }

        #endregion

        #region Stream Encryption/Decryption Methods

        public virtual void EncryptStream( Stream inputStream, Stream outputStream )
        {
            EncryptStream( inputStream, outputStream, Key, Salt );
        }

        public abstract void EncryptStream( Stream inputStream, Stream outputStream, string key, byte[] salt );

        public virtual void DecryptStream( Stream inputStream, Stream outputStream )
        {
            DecryptStream( inputStream, outputStream, Key, Salt );
        }

        public abstract void DecryptStream( Stream inputStream, Stream outputStream, string key, byte[] salt );

        #endregion

    }
}
