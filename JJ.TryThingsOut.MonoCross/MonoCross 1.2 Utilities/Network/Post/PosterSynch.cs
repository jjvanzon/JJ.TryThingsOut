using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace MonoCross.Utilities.Network
{
    public class PosterSynch : IPoster
    {
        public NetworkResponse PostString( string uri, string postString )
        {
            // default headers to null rather than iApp.RequestInjectionHeaders as we cannot guarantee the 
            // default headers are relevant to the specified URI
            return PostString( uri, postString, "application/x-www-form-urlencoded", "POST", null );
        }
        public NetworkResponse PostString( string uri, string postString, string contentType )
        {
            // default headers to null rather than iApp.RequestInjectionHeaders as we cannot guarantee the 
            // default headers are relevant to the specified URI
            return PostString( uri, postString, contentType, "POST", null );
        }
        public NetworkResponse PostString( string uri, string postString, string contentType, string verb )
        {
            // default headers to null rather than iApp.RequestInjectionHeaders as we cannot guarantee the 
            // default headers are relevant to the specified URI
            return PostString( uri, postString, contentType, verb, null );
        }
        public NetworkResponse PostString( string uri, string postString, Dictionary<string, string> headers )
        {
            // default headers to null rather than iApp.RequestInjectionHeaders as we cannot guarantee the 
            // default headers are relevant to the specified URI
            return PostString( uri, postString, "application/x-www-form-urlencoded", "POST", headers );
        }
        public NetworkResponse PostString( string uri, string postString, string contentType, Dictionary<string, string> headers )
        {
            byte[] postBytes = NetworkUtils.StrToByteArray( postString );

            // default headers to null rather than iApp.RequestInjectionHeaders as we cannot guarantee the 
            // default headers are relevant to the specified URI
            return PostBytes( uri, postBytes, contentType, "POST", null );
        }
        public NetworkResponse PostString( string uri, string postString, string contentType, string verb, Dictionary<string, string> headers )
        {
            byte[] postBytes = NetworkUtils.StrToByteArray( postString );

            // default headers to null rather than iApp.RequestInjectionHeaders as we cannot guarantee the 
            // default headers are relevant to the specified URI
            return PostBytes( uri, postBytes, contentType, verb, null );
        }

        public NetworkResponse PostObject( string uri, object postObject )
        {
            // convert object to bytes via an XML serializer, JSON not supported in iFactr.Core
            byte[] postBytes = NetworkUtils.XmlSerializeObjectToBytes( postObject );

            // default headers to null rather than iApp.RequestInjectionHeaders as we cannot guarantee the 
            // default headers are relevant to the specified URI
            return PostBytes( uri, postBytes, "application/xml", "POST", null );
        }
        public NetworkResponse PostObject( string uri, object postObject, string verb )
        {
            // convert object to bytes via an XML serializer, JSON not supported in iFactr.Core
            byte[] postBytes = NetworkUtils.XmlSerializeObjectToBytes( postObject );

            // default headers to null rather than iApp.RequestInjectionHeaders as we cannot guarantee the 
            // default headers are relevant to the specified URI
            return PostBytes( uri, postBytes, "application/xml", verb, null );
        }
        public NetworkResponse PostObject( string uri, object postObject, Dictionary<string, string> headers )
        {
            // convert object to bytes via an XML serializer, JSON not supported in iFactr.Core
            byte[] postBytes = NetworkUtils.XmlSerializeObjectToBytes( postObject );

            // default headers to null rather than iApp.RequestInjectionHeaders as we cannot guarantee the 
            // default headers are relevant to the specified URI
            return PostBytes( uri, postBytes, "application/xml", "POST", headers );
        }
        public NetworkResponse PostObject( string uri, object postObject, string verb, Dictionary<string, string> headers )
        {
            // convert object to bytes via an XML serializer, JSON not supported in iFactr.Core
            byte[] postBytes = NetworkUtils.XmlSerializeObjectToBytes( postObject );

            // default headers to null rather than iApp.RequestInjectionHeaders as we cannot guarantee the 
            // default headers are relevant to the specified URI
            return PostBytes( uri, postBytes, "application/xml", verb, headers );
        }

        public NetworkResponse PostBytes( string uri, byte[] postBytes, string contentType )
        {
            // default headers to null rather than iApp.RequestInjectionHeaders as we cannot guarantee the 
            // default headers are relevant to the specified URI
            return PostBytes( uri, postBytes, contentType, "POST", null );
        }
        public NetworkResponse PostBytes( string uri, byte[] postBytes, string contentType, string verb )
        {
            // default headers to null rather than iApp.RequestInjectionHeaders as we cannot guarantee the 
            // default headers are relevant to the specified URI
            return PostBytes( uri, postBytes, contentType, verb, null );
        }
        public NetworkResponse PostBytes( string uri, byte[] postBytes, string contentType, Dictionary<string, string> headers )
        {
            return PostBytes( uri, postBytes, contentType, "POST", null );
        }
        public NetworkResponse PostBytes( string uri, byte[] postBytes, string contentType, string verb, Dictionary<string, string> headers )
        {

#if !SILVERLIGHT && !MONO

            // Create the request obj
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create( uri );

            // Set values for the request back
            request.Method = verb;  // "POST";
            request.ContentType = contentType;
            request.ContentLength = postBytes.Length;
            request.AutomaticDecompression = DecompressionMethods.Deflate;

            NetworkResponse networkResponse = new NetworkResponse()
            {
                URI = request.RequestUri.AbsoluteUri,
                Verb = verb
            };
            DateTime dtMetric = DateTime.UtcNow;

            Stream postStream = null;

            HttpWebResponse response = null;
            Stream streamResponse = null;
            StreamReader streamRead = null;
            try
            {
                #region Send Post object to Request stream

                // To-Do: Refactor this central storage of header values to inject into an abstract network object.
                if ( headers != null && headers.Count() > 0 )
                {
                    foreach ( string key in headers.Keys )
                    {
                        request.Headers[key] = headers[key];
                    }
                }

                postStream = request.GetRequestStream();
                postStream.Write( postBytes, 0, postBytes.Length );
                postStream.Close();

                #endregion

                #region Get Response state from Response stream

                response = (HttpWebResponse) request.GetResponse();

                networkResponse.StatusCode = response.StatusCode;

                switch ( response.StatusCode )
                {
                    case HttpStatusCode.OK:
                    case HttpStatusCode.Created:
                    case HttpStatusCode.Accepted:
                        break;
                    case HttpStatusCode.NoContent:
                        networkResponse.Expiration = DateTime.UtcNow;
                        networkResponse.Message = String.Format( "No Content returned: Result {0} for {1}", response.StatusCode, uri );
                        MXDevice.Log.Warn( networkResponse.Message );
                        break;
                    default:
                        networkResponse.Expiration = DateTime.UtcNow;
                        networkResponse.Message = String.Format( "{0} failed. Received HTTP {1} for {2}", request.Method, response.StatusCode, uri );
                        MXDevice.Log.Error( networkResponse.Message );
                        return networkResponse;
                }

                // extract response into bytes and string.
                WebResponse webResponse = NetworkUtils.ExtractResponse( response );
                networkResponse.ResponseBytes = webResponse.ResponseBytes;
                networkResponse.ResponseString = webResponse.ResponseString;

                MXDevice.Log.Debug( string.Format( "PostBytes Completed: Uri: {0} Time: {1} milliseconds  Size: {2} ", uri, DateTime.UtcNow.Subtract( dtMetric ).TotalMilliseconds, ( networkResponse.ResponseBytes != null ? networkResponse.ResponseBytes.Length : -1 ) ) );

                // set expiration values
                networkResponse.Expiration = response.Headers["Expires"].TryParseDateTimeUtc();

                #endregion

            }
            catch ( WebException ex )
            {
                ex.Data.Add( "ResponseStatusCode", ex.Status );
                ex.Data.Add( "Uri", request.RequestUri );
                ex.Data.Add( "Verb", request.Method );

                MXDevice.Log.Error( string.Format( "Call to {0} had a Webexception. {1}  Status: {2}", request.RequestUri, ex.Message, ex.Status ) );
                MXDevice.Log.Error( ex );

                networkResponse.ResponseBytes = null;
                networkResponse.ResponseString = null;
                networkResponse.Expiration = DateTime.MinValue.ToUniversalTime();

                networkResponse.StatusCode = ( (HttpWebResponse) ex.Response ).StatusCode;
                ex.Data.Add( "StatusCode", networkResponse.StatusCode );
                ex.Data.Add( "StatusDescription", ( (HttpWebResponse) ex.Response ).StatusDescription );

                networkResponse.Message = ex.Message;
                networkResponse.Exception = ex;

            }
            catch ( Exception ex )
            {
                ex.Data.Add( "Uri", request.RequestUri );
                ex.Data.Add( "Verb", request.Method );

                networkResponse.ResponseBytes = null;
                networkResponse.ResponseString = null;
                networkResponse.Expiration = DateTime.MinValue.ToUniversalTime();
                networkResponse.StatusCode = (HttpStatusCode) ( -1 );
                networkResponse.Message = ex.Message;
                networkResponse.Exception = ex;
            }
            finally
            {
                // Close the stream object
                if ( streamResponse != null )
                    streamResponse.Close();

                if ( streamRead != null )
                    streamRead.Close();

                // Release the HttpWebResponse
                if ( response != null )
                    response.Close();
            }

            return networkResponse;
#else
                   throw new NotSupportedException("PosterSynch is not supported in Silverlight");
#endif
        }

    }
}
