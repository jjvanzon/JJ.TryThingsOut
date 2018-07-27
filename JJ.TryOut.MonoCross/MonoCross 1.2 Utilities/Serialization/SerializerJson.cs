using System.Collections.Generic;
using System.IO;
using System.Text;
using MonoCross.Utilities;
using Newtonsoft.Json;
using System;
using MonoCross.Utilities.Network;

namespace MonoCross.Utilities.Serialization
{
    public class SerializerJson<T> : BaseSerializer<T>, ISerializer<T>
    {
        public string ContentType
        {
            get
            {
                return "application/json";
            }
        }

        #region Serialize Object Methods

        public void SerializeObjectToFile( T obj, string filename )
        {
            DateTime dtMetric = DateTime.UtcNow;
            MXDevice.File.Save( filename, JsonConvert.SerializeObject( obj, Formatting.None ) );
            MXDevice.Log.Metric( string.Format( "SerializerJson.SerializeObjectToFile(1): File {0}  Time: {1} milliseconds", filename, DateTime.UtcNow.Subtract( dtMetric ).TotalMilliseconds ) );
        }

        public void SerializeObjectToFile( T obj, string filename, EncryptionMode mode )
        {
            DateTime dtMetric = DateTime.UtcNow;
            MXDevice.File.Save( filename, JsonConvert.SerializeObject( obj, Formatting.None ), mode );
            MXDevice.Log.Metric( string.Format( "SerializerJson.SerializeObjectToFile(2): File {0}  Time: {1} milliseconds", filename, DateTime.UtcNow.Subtract( dtMetric ).TotalMilliseconds ) );
        }

        public void SerializeObjectToFile( T obj, string filename, string key, byte[] salt )
        {
            DateTime dtMetric = DateTime.UtcNow;
            MXDevice.File.Save( filename, JsonConvert.SerializeObject( obj, Formatting.None ), key, salt );
            MXDevice.Log.Metric( string.Format( "SerializerJson.SerializeObjectToFile(3): File {0}  Time: {1} milliseconds", filename, DateTime.UtcNow.Subtract( dtMetric ).TotalMilliseconds ) );
        }

        protected override string SerializeObjectClear( T obj )
        {
            DateTime dtMetric = DateTime.UtcNow;
            string retval = JsonConvert.SerializeObject( obj, Formatting.None );
            MXDevice.Log.Metric( string.Format( "SerializerJson.SerializeObjectClear: Type: {0} Size: {1} Time: {2} milliseconds", obj.GetType().ToString(), ( string.IsNullOrEmpty( retval ) ? 0 : retval.Length ), DateTime.UtcNow.Subtract( dtMetric ).TotalMilliseconds ) );
            return retval;
        }

        protected override byte[] SerializeObjectToBytesClear( T obj )
        {
            DateTime dtMetric = DateTime.UtcNow;
            byte[] bytes = NetworkUtils.StrToByteArray( JsonConvert.SerializeObject( obj, Formatting.None ) );
            MXDevice.Log.Metric( string.Format( "SerializerJson.SerializeObjectToBytesClear: Time: {0} milliseconds", DateTime.UtcNow.Subtract( dtMetric ).TotalMilliseconds ) );
            return bytes;
        }

        #endregion

        #region Deserialize Object Methods

        public T DeserializeObjectFromFile( string filename )
        {
            return DeserializeObjectClear( MXDevice.File.Read( filename ) );
        }

        public T DeserializeObjectFromFile( string filename, EncryptionMode mode )
        {
            return DeserializeObjectClear( MXDevice.File.Read( filename, mode ) );
        }

        public T DeserializeObjectFromFile( string filename, string key, byte[] salt )
        {
            return DeserializeObject( MXDevice.File.Read( filename, key, salt ) );
        }

        protected override T DeserializeObjectClear( string value )
        {
            DateTime dtMetric = DateTime.UtcNow;

            T obj = default( T );
            if ( string.IsNullOrEmpty( value ) )
                return obj;

            obj = JsonConvert.DeserializeObject<T>( value );

            MXDevice.Log.Metric( string.Format( "SerializerJson.DeserializeObject: Type: {0} Size: {1} Time: {2} milliseconds", obj.GetType().ToString(), value.Length, DateTime.UtcNow.Subtract( dtMetric ).TotalMilliseconds ) );

            return obj;
        }

        #endregion

        #region Serialize List Methods

        public void SerializeListToFile( List<T> list, string filename )
        {
            DateTime dtMetric = DateTime.UtcNow;
            MXDevice.File.Save( filename, JsonConvert.SerializeObject( list, Formatting.None ) );
            MXDevice.Log.Metric( string.Format( "SerializerJson.SerializeListToFile(1): File: {0}  Time: {1} milliseconds", filename, DateTime.UtcNow.Subtract( dtMetric ).TotalMilliseconds ) );
        }

        public void SerializeListToFile( List<T> list, string filename, EncryptionMode mode )
        {
            DateTime dtMetric = DateTime.UtcNow;
            MXDevice.File.Save( filename, JsonConvert.SerializeObject( list, Formatting.None ), mode );
            MXDevice.Log.Metric( string.Format( "SerializerJson.SerializeListToFile(2): File: {0}  Time: {1} milliseconds", filename, DateTime.UtcNow.Subtract( dtMetric ).TotalMilliseconds ) );
        }

        public void SerializeListToFile( List<T> list, string filename, string key, byte[] salt )
        {
            DateTime dtMetric = DateTime.UtcNow;
            MXDevice.File.Save( filename, JsonConvert.SerializeObject( list, Formatting.None ), key, salt );
            MXDevice.Log.Metric( string.Format( "SerializerJson.SerializeListToFile(3): File: {0}  Time: {1} milliseconds", filename, DateTime.UtcNow.Subtract( dtMetric ).TotalMilliseconds ) );
        }

        protected override byte[] SerializeListToBytesClear( List<T> list )
        {
            DateTime dtMetric = DateTime.UtcNow;
            byte[] bytes = UTF8Encoding.UTF8.GetBytes( JsonConvert.SerializeObject( list, Formatting.None ) );
            MXDevice.Log.Metric( string.Format( "SerializerJson.SerializeListToBytes: Time: {0} milliseconds", DateTime.UtcNow.Subtract( dtMetric ).TotalMilliseconds ) );
            return bytes;
        }

        protected override string SerializeListClear( List<T> list )
        {
            DateTime dtMetric = DateTime.UtcNow;
            string retval = JsonConvert.SerializeObject( list, Formatting.Indented );
            MXDevice.Log.Metric( string.Format( "SerializerJson.SerializeList: Type: {0} Size: {1} Time: {2} milliseconds", list.GetType().ToString(), ( string.IsNullOrEmpty( retval ) ? 0 : retval.Length ), DateTime.UtcNow.Subtract( dtMetric ).TotalMilliseconds ) );
            return retval;
        }

        #endregion

        #region Deserialize List Methods

        public List<T> DeserializeListFromFile( string filename )
        {
            return DeserializeListClear( MXDevice.File.Read( filename ) );
        }

        public List<T> DeserializeListFromFile( string filename, EncryptionMode mode )
        {
            return DeserializeListClear( MXDevice.File.Read( filename, mode ) );
        }

        public List<T> DeserializeListFromFile( string filename, string key, byte[] salt )
        {
            return DeserializeListClear( MXDevice.File.Read( filename, key, salt ) );
        }

        protected override List<T> DeserializeListClear( string value )
        {
            DateTime dtMetric = DateTime.UtcNow;

            List<T> list = default( List<T> );
            if ( string.IsNullOrEmpty(value) )
                return list;

            list = JsonConvert.DeserializeObject<List<T>>( value );

            MXDevice.Log.Metric( string.Format( "SerializerJson.DeserializeListClear: Type: {0} Size: {1} Time: {2} milliseconds", list.GetType().ToString(), value.Length, DateTime.UtcNow.Subtract( dtMetric ).TotalMilliseconds ) );

            return list;
        }

        #endregion
    }
}
