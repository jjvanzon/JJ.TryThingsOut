using MonoCross.Utilities;
using System.Collections.Generic;
using MonoCross.Utilities.Network;

namespace MonoCross.Utilities.Serialization
{
    public abstract class BaseSerializer<T>
    {
        #region Base SerializeObject Methods

        public virtual string SerializeObject( T obj )
        {
            return SerializeObject( obj, EncryptionMode.Default );
        }
        public virtual string SerializeObject( T obj, EncryptionMode mode )
        {
            string retval = null;

            switch ( mode )
            {
                case EncryptionMode.NoEncryption:
                    retval = SerializeObjectClear( obj );
                    break;
                case EncryptionMode.Encryption:
                    retval = SerializeObject( obj, MXDevice.Encryption.Key, MXDevice.Encryption.Salt );
                    break;
                case EncryptionMode.Default:
                    if ( MXDevice.Encryption.Required )
                        retval = SerializeObject( obj, MXDevice.Encryption.Key, MXDevice.Encryption.Salt );
                    else
                        retval = SerializeObjectClear( obj );
                    break;
            }

            return retval;
        }
        public virtual string SerializeObject( T obj, string key, byte[] salt )
        {
            string retval = SerializeObjectClear( obj );
            return MXDevice.Encryption.EncryptString( retval, key, salt );
        }
        protected abstract string SerializeObjectClear( T obj );

        #endregion

        #region Base SerializeObjectToBytes Methods

        public virtual byte[] SerializeObjectToBytes( T obj )
        {
            return SerializeObjectToBytes( obj, EncryptionMode.Default );
        }

        public virtual byte[] SerializeObjectToBytes( T obj, EncryptionMode mode )
        {
            byte[] bytes = null;

            switch ( mode )
            {
                case EncryptionMode.NoEncryption:
                    bytes = SerializeObjectToBytesClear( obj );
                    break;
                case EncryptionMode.Encryption:
                    bytes = SerializeObjectToBytes( obj, MXDevice.Encryption.Key, MXDevice.Encryption.Salt );
                    break;
                case EncryptionMode.Default:
                    if ( MXDevice.Encryption.Required )
                        bytes = SerializeObjectToBytes( obj, MXDevice.Encryption.Key, MXDevice.Encryption.Salt );
                    else
                        bytes = SerializeObjectToBytesClear( obj );
                    break;
            }

            return bytes;
        }
        public virtual byte[] SerializeObjectToBytes( T obj, string key, byte[] salt )
        {
            return MXDevice.Encryption.EncryptBytes( SerializeObjectToBytesClear( obj ), key, salt );
        }
        protected abstract byte[] SerializeObjectToBytesClear( T obj );

        #endregion

        #region Base SerializeList Methods

        public virtual string SerializeList( List<T> list )
        {
            return SerializeList( list, EncryptionMode.Default );
        }
        public virtual string SerializeList( List<T> list, EncryptionMode mode )
        {
            string retval = null;

            switch ( mode )
            {
                case EncryptionMode.NoEncryption:
                    retval = SerializeListClear( list );
                    break;
                case EncryptionMode.Encryption:
                    retval = SerializeList( list, MXDevice.Encryption.Key, MXDevice.Encryption.Salt );
                    break;
                case EncryptionMode.Default:
                    if ( MXDevice.Encryption.Required )
                        retval = SerializeList( list, MXDevice.Encryption.Key, MXDevice.Encryption.Salt );
                    else
                        retval = SerializeListClear( list );
                    break;
            }

            return retval;
        }
        public virtual string SerializeList( List<T> list, string key, byte[] salt )
        {
            string retval = SerializeListClear( list );
            return MXDevice.Encryption.EncryptString( retval, key, salt );
        }
        protected abstract string SerializeListClear( List<T> list );

        #endregion

        #region Base SerializeListToBytes Methods

        public virtual byte[] SerializeListToBytes( List<T> list )
        {
            return SerializeListToBytes( list, EncryptionMode.Default );
        }
        public virtual byte[] SerializeListToBytes( List<T> list, EncryptionMode mode )
        {
            byte[] bytes = null;

            switch ( mode )
            {
                case EncryptionMode.NoEncryption:
                    bytes = SerializeListToBytesClear( list );
                    break;
                case EncryptionMode.Encryption:
                    bytes = SerializeListToBytes( list, MXDevice.Encryption.Key, MXDevice.Encryption.Salt );
                    break;
                case EncryptionMode.Default:
                    if ( MXDevice.Encryption.Required )
                        bytes = SerializeListToBytes( list, MXDevice.Encryption.Key, MXDevice.Encryption.Salt );
                    else
                        bytes = SerializeListToBytesClear( list );
                    break;
            }

            return bytes;
        }
        public virtual byte[] SerializeListToBytes( List<T> list, string key, byte[] salt )
        {
            byte[] bytes = SerializeListToBytesClear( list );
            return MXDevice.Encryption.EncryptBytes( bytes, key, salt );
        }
        protected abstract byte[] SerializeListToBytesClear( List<T> list );

        #endregion

        #region Base DeserializeObject Methods

        public virtual T DeserializeObject( string value )
        {
            return DeserializeObject( value, EncryptionMode.Default );
        }
        public virtual T DeserializeObject( string value, EncryptionMode mode )
        {
            T retval = default( T );

            switch ( mode )
            {
                case EncryptionMode.NoEncryption:
                    retval = DeserializeObjectClear( value );
                    break;
                case EncryptionMode.Encryption:
                    retval = DeserializeObject( value, MXDevice.Encryption.Key, MXDevice.Encryption.Salt );
                    break;
                case EncryptionMode.Default:
                    if ( MXDevice.Encryption.Required )
                        retval = DeserializeObject( value, MXDevice.Encryption.Key, MXDevice.Encryption.Salt );
                    else
                        retval = DeserializeObjectClear( value );
                    break;
            }

            return retval;
        }
        public virtual T DeserializeObject( string value, string key, byte[] salt )
        {
            string retval = MXDevice.Encryption.DecryptString( value, key, salt );
            return DeserializeObjectClear( retval );
        }
        protected abstract T DeserializeObjectClear( string value );

        public virtual T DeserializeObject( byte[] value )
        {
            return DeserializeObject( value, EncryptionMode.Default );
        }
        public virtual T DeserializeObject( byte[] value, EncryptionMode mode )
        {
            T retval = default( T );

            switch ( mode )
            {
                case EncryptionMode.NoEncryption:
                    retval = DeserializeObjectClear( value );
                    break;
                case EncryptionMode.Encryption:
                    retval = DeserializeObject( value, MXDevice.Encryption.Key, MXDevice.Encryption.Salt );
                    break;
                case EncryptionMode.Default:
                    if ( MXDevice.Encryption.Required )
                        retval = DeserializeObject( value, MXDevice.Encryption.Key, MXDevice.Encryption.Salt );
                    else
                        retval = DeserializeObjectClear( value );
                    break;
            }

            return retval;
        }
        public virtual T DeserializeObject( byte[] value, string key, byte[] salt )
        {
            byte[] retval = MXDevice.Encryption.DecryptBytes( value, key, salt );
            return DeserializeObjectClear( retval );
        }
        protected virtual T DeserializeObjectClear( byte[] value )
        {
            if ( value == null )
                return default( T );

            string retval = NetworkUtils.ByteArrayToStr( value );

            return DeserializeObjectClear( retval );
        }

        #endregion

        #region Base DeserializeList Methods

        public virtual List<T> DeserializeList( string value )
        {
            return DeserializeList( value, EncryptionMode.Default );
        }

        public virtual List<T> DeserializeList( string value, EncryptionMode mode )
        {
            List<T> retval = default( List<T> );

            switch ( mode )
            {
                case EncryptionMode.NoEncryption:
                    retval = DeserializeListClear( value );
                    break;
                case EncryptionMode.Encryption:
                    retval = DeserializeList( value, MXDevice.Encryption.Key, MXDevice.Encryption.Salt );
                    break;
                case EncryptionMode.Default:
                    if ( MXDevice.Encryption.Required )
                        retval = DeserializeList( value, MXDevice.Encryption.Key, MXDevice.Encryption.Salt );
                    else
                        retval = DeserializeListClear( value );
                    break;
            }

            return retval;
        }

        public virtual List<T> DeserializeList( string value, string key, byte[] salt )
        {
            string retval = MXDevice.Encryption.DecryptString( value, key, salt );
            return DeserializeListClear( retval );
        }

        public virtual List<T> DeserializeList( byte[] value )
        {
            return DeserializeList( value, EncryptionMode.Default );
        }

        public virtual List<T> DeserializeList( byte[] value, EncryptionMode mode )
        {
            List<T> retval = default( List<T> );

            switch ( mode )
            {
                case EncryptionMode.NoEncryption:
                    retval = DeserializeListClear( value );
                    break;
                case EncryptionMode.Encryption:
                    retval = DeserializeList( value, MXDevice.Encryption.Key, MXDevice.Encryption.Salt );
                    break;
                case EncryptionMode.Default:
                    if ( MXDevice.Encryption.Required )
                        retval = DeserializeList( value, MXDevice.Encryption.Key, MXDevice.Encryption.Salt );
                    else
                        retval = DeserializeListClear( value );
                    break;
            }

            return retval;
        }

        public virtual List<T> DeserializeList( byte[] value, string key, byte[] salt )
        {
            byte[] retval = MXDevice.Encryption.DecryptBytes( value, key, salt );
            return DeserializeListClear( retval );
        }

        protected abstract List<T> DeserializeListClear( string value );

        protected virtual List<T> DeserializeListClear( byte[] value )
        {
            if ( value == null )
                return default( List<T> );

            string retval = NetworkUtils.ByteArrayToStr( value );

            //if ( retval[0] != '<' )
            //    retval = retval.Substring( 1 );

            return DeserializeListClear( retval );
        }

        #endregion
    }
}
