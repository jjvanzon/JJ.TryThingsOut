using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoCross.Utilities;

namespace MonoCross.Utilities.Serialization
{
    public interface ISerializer<T>
    {
        void SerializeObjectToFile( T obj, string filename );
        void SerializeObjectToFile( T obj, string filename, EncryptionMode mode );
        void SerializeObjectToFile( T obj, string filename, string key, byte[] salt );

        void SerializeListToFile( List<T> list, string filename );
        void SerializeListToFile( List<T> list, string filename, EncryptionMode mode );
        void SerializeListToFile( List<T> list, string filename, string key, byte[] salt );

        byte[] SerializeObjectToBytes( T obj );
        byte[] SerializeObjectToBytes( T obj, EncryptionMode mode );
        byte[] SerializeObjectToBytes( T obj, string key, byte[] salt );

        byte[] SerializeListToBytes( List<T> list );
        byte[] SerializeListToBytes( List<T> list, EncryptionMode mode );
        byte[] SerializeListToBytes( List<T> list, string key, byte[] salt );

        string SerializeObject( T obj );
        string SerializeObject( T obj, EncryptionMode mode );
        string SerializeObject( T obj, string key, byte[] salt );

        string SerializeList( List<T> list );
        string SerializeList( List<T> list, EncryptionMode mode );
        string SerializeList( List<T> list, string key, byte[] salt );

        T DeserializeObjectFromFile( string filename );
        T DeserializeObjectFromFile( string filename, EncryptionMode mode );
        T DeserializeObjectFromFile( string filename, string key, byte[] salt );

        List<T> DeserializeListFromFile( string filename );
        List<T> DeserializeListFromFile( string filename, EncryptionMode mode );
        List<T> DeserializeListFromFile( string filename, string key, byte[] salt );
       
        T DeserializeObject( string value );
        T DeserializeObject( string value, EncryptionMode mode );
        T DeserializeObject( string value, string key, byte[] salt );

        T DeserializeObject( byte[] value );
        T DeserializeObject( byte[] value, EncryptionMode mode );
        T DeserializeObject( byte[] value, string key, byte[] salt );

        List<T> DeserializeList( string value );
        List<T> DeserializeList( string value, EncryptionMode mode );
        List<T> DeserializeList( string value, string key, byte[] salt );

        List<T> DeserializeList( byte[] value );
        List<T> DeserializeList( byte[] value, EncryptionMode mode );
        List<T> DeserializeList( byte[] value, string key, byte[] salt );

        string ContentType { get; }
    }
}