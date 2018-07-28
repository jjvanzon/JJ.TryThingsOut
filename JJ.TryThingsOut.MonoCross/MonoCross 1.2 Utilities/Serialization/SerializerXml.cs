using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System;
using MonoCross.Utilities;

namespace MonoCross.Utilities.Serialization
{
    public class SerializerXml<T> : BaseSerializer<T>, ISerializer<T>
    {
        public string ContentType
        {
            get
            {
                return "application/xml";
            }
        }

        #region Serialize Object Methods

        public void SerializeObjectToFile( T obj, string filename )
        {
            MXDevice.File.Save( filename, SerializeObjectToBytesClear( obj ) );
        }

        public void SerializeObjectToFile( T obj, string filename, EncryptionMode mode )
        {
            MXDevice.File.Save( filename, SerializeObjectToBytesClear( obj ), mode );
        }

        public void SerializeObjectToFile( T obj, string filename, string key, byte[] salt )
        {
            MXDevice.File.Save( filename, SerializeObjectToBytesClear( obj ), key, salt );
        }

        protected override byte[] SerializeObjectToBytesClear( T obj )
        {
            DateTime dtMetric = DateTime.UtcNow;

            byte[] byteData = null;

            MemoryStream stream = new MemoryStream();

            XmlSerializer ser = new XmlSerializer( typeof( T ) );
            XmlWriter writer = null;
            try
            {
                writer = XmlWriter.Create( stream, new XmlWriterSettings()
                {
                    Encoding = Encoding.UTF8
                } );

                ser.Serialize( writer, obj );
                byteData = stream.ToArray();

                //Encoding enc = Encoding.GetEncoding( "utf-8" );
                //string a = enc.GetString( byteData, 0, byteData.Length );
                //MXDevice.Log.Info( a );
            }
            finally
            {
                if ( writer != null )
                    writer.Close();
            }

            MXDevice.Log.Metric( string.Format( "SerializerXml.SerializeObjectToBytes: Type: {0} Size: {1} Time: {2} milliseconds", obj.GetType().ToString(), byteData.Length, DateTime.UtcNow.Subtract( dtMetric ).TotalMilliseconds ) );
			
            return byteData;
        }

        protected override string SerializeObjectClear( T obj )
        {
            DateTime dtMetric = DateTime.UtcNow;

            byte[] byteData = SerializeObjectToBytesClear( obj );
            string retval = Encoding.GetEncoding( "utf-8" ).GetString( byteData, 0, byteData.Length );

            MXDevice.Log.Metric( string.Format( "SerializeObject: Type: {0} Size: {1} Time: {2} milliseconds", obj.GetType().ToString(), ( string.IsNullOrEmpty( retval ) ? 0 : retval.Length ), DateTime.UtcNow.Subtract( dtMetric ).TotalMilliseconds ) );

            return retval;
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
            return DeserializeObjectClear( MXDevice.File.Read( filename, key, salt ) );
        }

        protected override T DeserializeObjectClear( string value )
        {
            DateTime dtMetric = DateTime.UtcNow;

            T obj = default( T );
            if ( string.IsNullOrEmpty( value ) )
                return obj;
            
            if ( value[0] != '<' )
                value = value.Substring( 1 );

            XmlSerializer xmlsr = new XmlSerializer( typeof( T ) );
            StringReader reader = null;
            try
            {
                reader = new StringReader( value );
                obj = (T) xmlsr.Deserialize( reader );
            }
            //catch ( Exception e )
            //{
            //    string a = "";
            //}
            finally
            {
                if ( reader != null )
                    reader.Close();
            }

            MXDevice.Log.Metric( string.Format( "SerializerXml.DeserializeObject: Type: {0} Size: {1} Time: {2} milliseconds", obj.GetType().ToString(), value.Length, DateTime.UtcNow.Subtract( dtMetric ).TotalMilliseconds ) );

            return obj;
        }

        #endregion
        
        #region Serialize List Methods

        public void SerializeListToFile( List<T> list, string filename )
        {
            MXDevice.File.Save( filename, SerializeListToBytesClear( list ) );
        }

        public void SerializeListToFile( List<T> list, string filename, EncryptionMode mode )
        {
            MXDevice.File.Save( filename, SerializeListToBytesClear( list ), mode );
        }

        public void SerializeListToFile( List<T> list, string filename, string key, byte[] salt )
        {
            MXDevice.File.Save( filename, SerializeListToBytesClear( list ), key, salt );
        }

        protected override byte[] SerializeListToBytesClear( List<T> list )
        {
            DateTime dtMetric = DateTime.UtcNow;

            byte[] byteData = null;

            MemoryStream stream = new MemoryStream();

            XmlSerializer ser = new XmlSerializer( typeof( List<T> ) );
            XmlWriter writer = null;
            try
            {
                writer = XmlWriter.Create( stream, new XmlWriterSettings()
                {
                    Encoding = Encoding.UTF8
                } );

                ser.Serialize( writer, list );
                byteData = stream.ToArray();
            }
            finally
            {
                if ( writer != null )
                    writer.Close();
            }

            MXDevice.Log.Metric( string.Format( "SerializerXml.SerializeListToBytes: Type: {0} Size: {1} Time: {2} milliseconds", list.GetType().ToString(), byteData.Length, DateTime.UtcNow.Subtract( dtMetric ).TotalMilliseconds ) );

            return byteData;
        }

        protected override string SerializeListClear( List<T> list )
        {
            DateTime dtMetric = DateTime.UtcNow;

            byte[] byteData = SerializeListToBytesClear( list );
            string retval = Encoding.GetEncoding( "utf-8" ).GetString( byteData, 0, byteData.Length );

            MXDevice.Log.Metric( string.Format( "SerializerXml.SerializeList: Type: {0} Size: {1} Time: {2:0,0} milliseconds", list.GetType().ToString(), ( string.IsNullOrEmpty( retval ) ? 0 : retval.Length ), DateTime.UtcNow.Subtract( dtMetric ).TotalMilliseconds ) );
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
            if ( string.IsNullOrEmpty( value ) )
                return list;

            if ( value[0] != '<' )
                value = value.Substring( 1 );

            XmlSerializer xmlsr = new XmlSerializer( typeof( List<T> ) );
            StringReader reader = null;
            try
            {
                reader = new StringReader( value );
                list = (List<T>) xmlsr.Deserialize( reader );
            }
            finally
            {
                if ( reader != null )
                    reader.Close();
            }

            MXDevice.Log.Metric( string.Format( "SerializerXml.DeserializeList: Type: {0} Size: {1} Time: {2} milliseconds", list.GetType().ToString(), value.Length, DateTime.UtcNow.Subtract( dtMetric ).TotalMilliseconds ) );

            return list;
        }
       
        #endregion
    }
}
