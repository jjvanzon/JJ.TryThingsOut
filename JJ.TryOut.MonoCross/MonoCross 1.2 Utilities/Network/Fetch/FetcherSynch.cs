using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace MonoCross.Utilities.Network
{
    public class FetcherSynch : IFetcher
    {
        public NetworkResponse Fetch( string uri )
        {
            return Fetch( uri, null, null );
        }

        public NetworkResponse Fetch( string uri, Dictionary<string, string> headers )
        {
            return Fetch( uri, null, headers );
        }

        public NetworkResponse Fetch( string uri, string filename )
        {
            return Fetch( uri, filename, null );
        }

        public NetworkResponse Fetch( string uri, string filename, Dictionary<string, string> headers )
        {
#if SILVERLIGHT || MONO
            throw new NotSupportedException("FetcherSynch is not supported in Silverlight");
#else
            NetworkResponse networkResponse = new NetworkResponse()
            {
                URI = uri,
                Verb = "GET"
            };

            DateTime dtMetric = DateTime.UtcNow;

            Stream stream = null;
            StreamReader reader = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                request = (HttpWebRequest) WebRequest.Create( uri );
                request.Method = "GET";
                request.AutomaticDecompression = DecompressionMethods.Deflate;

                //// If required by the server, set the credentials.
                //request.Credentials = CredentialCache.DefaultCredentials;

                // To-Do: Refactor this central storage of header values to inject into an abstract network object.
                if ( headers != null && headers.Count() > 0 )
                {
                    foreach ( string key in headers.Keys )
                    {
                        request.Headers[key] = headers[key];
                    }
                }


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
                WebResponse webResponse = NetworkUtils.ExtractResponse( response, filename );
                networkResponse.ResponseBytes = webResponse.ResponseBytes;
                networkResponse.ResponseString = webResponse.ResponseString;

                MXDevice.Log.Debug( string.Format( "Fetch Completed: Uri: {0} Time: {1} milliseconds  Size: {2} ", uri, DateTime.UtcNow.Subtract( dtMetric ).TotalMilliseconds, ( networkResponse.ResponseBytes != null ? networkResponse.ResponseBytes.Length : -1 ) ) );

                #endregion

                networkResponse.Expiration = response.Headers["Expires"].TryParseDateTimeUtc();
            }
            catch ( WebException ex )
            {
                ex.Data.Add( "Uri", networkResponse.URI );
                ex.Data.Add( "Verb", networkResponse.Verb );

                networkResponse.Message = string.Format( "Call to {0} had a Webexception. {1}   Status: {2}   Desc: {3}", networkResponse.URI, ex.Message, ex.Status, ( (HttpWebResponse) ex.Response ).StatusDescription );

                networkResponse.StatusCode = ( (HttpWebResponse) ex.Response ).StatusCode;
                ex.Data.Add( "StatusCode", networkResponse.StatusCode );
                ex.Data.Add( "StatusDescription", ( (HttpWebResponse) ex.Response ).StatusDescription );

                networkResponse.Exception = ex;
                networkResponse.Expiration = DateTime.UtcNow;

                MXDevice.Log.Error( networkResponse.Message );
                MXDevice.Log.Error( ex );
            }
            catch ( Exception ex )
            {
                ex.Data.Add( "Uri", networkResponse.URI );
                ex.Data.Add( "Verb", networkResponse.Verb );

                networkResponse.Message = string.Format( "Call to {0} had an Exception. {1}", networkResponse.URI, ex.Message );
                networkResponse.Exception = ex;
                networkResponse.StatusCode = (HttpStatusCode) ( -1 );
                networkResponse.Expiration = DateTime.UtcNow;

                MXDevice.Log.Error( networkResponse.Message );
                MXDevice.Log.Error( ex );
            }
            finally
            {
                // Cleanup the streams and the response.
                if ( reader != null )
                    reader.Close();
                if ( stream != null )
                    stream.Close();
                if ( response != null )
                    response.Close();
            }

            return networkResponse;

#endif

        }
    }
}
