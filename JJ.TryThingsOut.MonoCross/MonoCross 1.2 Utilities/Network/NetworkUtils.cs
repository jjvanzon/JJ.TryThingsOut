using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
#if !SILVERLIGHT
using System.IO.Compression;
#endif
using System.Net;

namespace MonoCross.Utilities.Network
{
    internal static class NetworkUtils
    {

        internal static byte[] XmlSerializeObjectToBytes( object obj )
        {
            byte[] byteData = null;

            MemoryStream stream = new MemoryStream();

            XmlSerializer ser = new XmlSerializer( obj.GetType() );
            XmlWriter writer = null;
            try
            {
                writer = XmlWriter.Create( stream, new XmlWriterSettings()
                {
                    Encoding = Encoding.UTF8
                } );

                ser.Serialize( stream, obj );
                byteData = stream.ToArray();

                //Encoding enc = Encoding.GetEncoding( "utf-8" );
                //string a = enc.GetString( byteData, 0, byteData.Length );

            }
            finally
            {
                if ( writer != null )
                    writer.Close();
            }

            return byteData;
        }

        public static byte[] StrToByteArray( string str )
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            return encoding.GetBytes( str );
        }

        public static string ByteArrayToStr( byte[] byteData )
        {
            try
            {
                Encoding enc = Encoding.GetEncoding( "utf-8" );
                return enc.GetString( byteData, 0, byteData.Length );
            }
            catch
            {
                // swallow exception if cannot convert to UTF8 string.
            }

            return null;
        }



        /// <summary>
        /// Reads data from a stream until the end is reached. The
        /// data is returned as a byte array. An IOException is
        /// thrown if any of the underlying IO calls fail.
        /// </summary>
        /// <param name="stream">The stream to read data from</param>
        public static byte[] StreamToByteArray( Stream stream )
        {
            byte[] buffer = new byte[32768];
            using ( MemoryStream ms = new MemoryStream() )
            {
                while ( true )
                {
                    int read = stream.Read( buffer, 0, buffer.Length );
                    if ( read <= 0 )
                        return ms.ToArray();
                    ms.Write( buffer, 0, read );
                }
            }
        }


        public static WebResponse ExtractResponse( HttpWebResponse response )
        {
            return ExtractResponse( response, null );
        }
        public static WebResponse ExtractResponse( HttpWebResponse response, string filename )
        {
            WebResponse webResponse = null;
            Stream streamResponse = response.GetResponseStream();

#if !SILVERLIGHT
            if ( response.ContentEncoding.ToLower().Contains( "deflate" ) )
                streamResponse = new DeflateStream( streamResponse, CompressionMode.Decompress );
			else if ( response.ContentEncoding.ToLower().Contains( "gzip" ) )
                streamResponse = new GZipStream( streamResponse, CompressionMode.Decompress );
#endif

            StreamReader streamRead = null;
            try
            {
                webResponse = new WebResponse();
                webResponse.ResponseBytes = NetworkUtils.StreamToByteArray( streamResponse );
                webResponse.ResponseString = NetworkUtils.ByteArrayToStr( webResponse.ResponseBytes );

                if ( !string.IsNullOrEmpty( filename ) )
                    MXDevice.File.Save(filename, webResponse.ResponseBytes); 
            }
            finally
            {
                // Close the stream object
                if ( streamResponse != null )
                    streamResponse.Close();

                if ( streamRead != null )
                    streamRead.Close();
            }

            return webResponse;
        }
    }

    public class WebResponse
    {
        public byte[] ResponseBytes
        {
            get;
            set;
        }
        public string ResponseString
        {
            get;
            set;
        }
    }
}
