using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Text;

namespace MonoCross.Utilities.Network
{
    /// <summary>
    /// 
    /// </summary>
    public class FetcherAsynch : IFetcher
    {
        private ManualResetEvent allDone = new ManualResetEvent( false );
        private AutoResetEvent autoEvent = new AutoResetEvent( false );

        //private object padLock = new object();
        //NetworkResponse _networkResponse = new NetworkResponse();
        private NetworkResponse PostNetworkResponse
        {
            get;
            //{
            //lock ( padLock )
            //{
            //    return _networkResponse;
            //}
            //}
            set;
            // {
            //lock ( padLock )
            //{
            //    _networkResponse = value;
            //}
            //}
        }

        /// <summary>
        /// Defines the delegate for factory events
        /// </summary>
        public delegate void NetworkingEventHandler( RequestState state );
        public delegate void NetworkingErrorHandler( RequestState state );

        /// <summary>
        /// Occurs when asynch download completes.
        /// </summary>
        public event NetworkingEventHandler OnDownloadComplete;
        public event NetworkingErrorHandler OnError;

        const int DefaultTimeout = 60 * 1000;

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
        /// <summary>
        /// Synchronous Wrapper method around FetchAsynch() method
        /// </summary>
        /// <param name="cacheIndex"></param>
        /// <param name="cacheIndexItem"></param>
        public NetworkResponse Fetch( string uri, string filename, Dictionary<string, string> headers )
        {
            PostNetworkResponse = new NetworkResponse();
            FetchParameters fetchParameters = new FetchParameters()
            {
                Uri = uri,
                Headers = headers,
                FileName = filename
            };

            DateTime dtMetric = DateTime.UtcNow;

            // set callback and error handler
            OnDownloadComplete += new NetworkingEventHandler( FetcherAsynch_OnDownloadComplete );
            OnError += new NetworkingErrorHandler( FetcherAsynch_OnError );

            System.Threading.ThreadPool.QueueUserWorkItem( parameters =>
            {
                try
                {
                    FetchAsynch( parameters );
                }
                //catch ( Exception e )
                //{
                //    // You could react or save the exception to an 'outside' variable 
                //    // threadExc = e;    
                //}
                finally
                {
                    autoEvent.Set(); // if you're firing and not forgetting ;)    
                }
            }, fetchParameters );

            // WaitOne returns true if autoEvent were signaled (i.e. process completed before timeout expired)
            // WaitOne returns false it the timeout expired before the process completed.
            if ( !autoEvent.WaitOne( DefaultTimeout ) )
            {
                string message = "FetcherAsynch call to FetchAsynch timed out. uri " + fetchParameters.Uri;
                MXDevice.Log.Error( message );

                MXDevice.Log.Debug( string.Format( "FetchAsynch timed out: Uri: {0} Time: {1} milliseconds ", uri, DateTime.UtcNow.Subtract( dtMetric ).TotalMilliseconds ) );

                NetworkResponse networkResponse = new NetworkResponse()
                {
                    Message = message,
                    URI = fetchParameters.Uri,
                    StatusCode = HttpStatusCode.RequestTimeout,
                    ResponseString = string.Empty,
                    Expiration = DateTime.MinValue.ToUniversalTime(),
                };

                MXDevice.PostNetworkResponse( networkResponse );
                return networkResponse;
            }

            //if ( threadExc != null )
            //{
            //    throw threadExc;
            //}

            MXDevice.Log.Debug( string.Format( "FetchAsynch Completed: Uri: {0} Time: {1} milliseconds  Size: {2} ", uri, DateTime.UtcNow.Subtract( dtMetric ).TotalMilliseconds, ( PostNetworkResponse.ResponseBytes != null ? PostNetworkResponse.ResponseBytes.Length : -1 ) ) );


            return PostNetworkResponse;
        }

        private void FetcherAsynch_OnError( RequestState state )
        {
            Exception exc = new Exception( "FetcherAsynch call to FetchAsynch threw an exception", state.Exception );
            MXDevice.Log.Error( exc );

            PostNetworkResponse.StatusCode = state.StatusCode;
            PostNetworkResponse.Message = exc.Message;
            PostNetworkResponse.Exception = exc;
            PostNetworkResponse.URI = state.AbsoluteUri;
            PostNetworkResponse.Verb = state.Verb;
            PostNetworkResponse.ResponseString = null;
            PostNetworkResponse.ResponseBytes = null;

            MXDevice.PostNetworkResponse(PostNetworkResponse);
        }

        private void FetcherAsynch_OnDownloadComplete( RequestState state )
        {
            PostNetworkResponse.StatusCode = state.StatusCode;
            PostNetworkResponse.URI = state.AbsoluteUri;
            PostNetworkResponse.Verb = state.Verb;
            PostNetworkResponse.ResponseString = state.ResponseString;
            PostNetworkResponse.ResponseBytes = state.ResponseBytes;
            PostNetworkResponse.Expiration = state.Expiration;
            PostNetworkResponse.Message = state.ErrorMessage;

            switch ( PostNetworkResponse.StatusCode )
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                case HttpStatusCode.Accepted:
                    // things are ok, no event required
                    break;
                case HttpStatusCode.NoContent:           // return when an object is not found
                case HttpStatusCode.Unauthorized:        // return when session expires
                case HttpStatusCode.InternalServerError: // return when an exception happens
                case HttpStatusCode.ServiceUnavailable:  // return when the database or siteminder are unavailable
                    PostNetworkResponse.Message = String.Format( "Network Service responded with status code {0}", state.StatusCode );
                    MXDevice.PostNetworkResponse(PostNetworkResponse);
                    break;
                default:
                    PostNetworkResponse.Message = String.Format( "FetcherAsynch completed but received HTTP {0}", state.StatusCode );
                    MXDevice.Log.Error( PostNetworkResponse.Message );
                    MXDevice.PostNetworkResponse(PostNetworkResponse);
                    return;
            }
        }

        private void FetchAsynch( Object parameters )
        {
            FetchParameters fetchParameters = (FetchParameters) parameters;

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create( fetchParameters.Uri );
            request.Method = "GET";
            //request.Proxy = null;

#if !SILVERLIGHT && !MONO
            request.AutomaticDecompression = DecompressionMethods.GZip;
#endif
			
            if ( fetchParameters.Headers != null && fetchParameters.Headers.Count() > 0 )
            {
                foreach ( string key in fetchParameters.Headers.Keys )
                {
                    request.Headers[key] = fetchParameters.Headers[key];
                }
            }

            RequestState state = new RequestState()
            {
                Request = request,
                AbsoluteUri = fetchParameters.Uri,
                FileName = fetchParameters.FileName
            };

            // Start the asynchronous request.
            IAsyncResult result = request.BeginGetResponse( new AsyncCallback( ResponseCallback ), state );
            if ( !allDone.WaitOne( DefaultTimeout ) )
            {
                OnError( state );
                return;
            }
        }

        // Define other methods and classes here
        private void ResponseCallback( IAsyncResult result )
        {
            // Get and fill the RequestState
            RequestState state = (RequestState) result.AsyncState;

            try
            {
                HttpWebRequest request = state.Request;

                // End the Asynchronous response and get the actual response object
                state.Response = (HttpWebResponse) request.EndGetResponse( result );

                state.Expiration = state.Response.Headers["Expires"].TryParseDateTimeUtc();

                // apply web response headers to data collection.
                // To-Do: evaluate which headers are actually needed and skip those that aren't. So, what's our logic for "needed headers" ?
                foreach ( string key in state.Response.Headers.AllKeys )
                {
                    state.Data[key] = state.Response.Headers[key];
                }

                state.StatusCode = state.Response.StatusCode;

                switch ( state.StatusCode )
                {
                    case HttpStatusCode.OK:
                    case HttpStatusCode.Created:
                    case HttpStatusCode.Accepted:
                        break;
                    case HttpStatusCode.NoContent:
                        state.ErrorMessage = String.Format( "No Content returned: Result {0} for {1}", state.StatusCode, request.RequestUri );
                        MXDevice.Log.Warn( state.ErrorMessage );
                        state.Expiration = DateTime.UtcNow;
                        OnDownloadComplete( state );
                        return;
                    default:
                        state.ErrorMessage = String.Format( "Get failed. Received HTTP {0} for {1}", state.StatusCode, request.RequestUri );
                        MXDevice.Log.Error( state.ErrorMessage );
                        state.Expiration = DateTime.UtcNow;
                        OnDownloadComplete( state );

                        return;
                }

                // extract response into bytes and string.
                WebResponse webResponse = NetworkUtils.ExtractResponse( state.Response, state.FileName );
                state.ResponseBytes = webResponse.ResponseBytes;
                state.ResponseString = webResponse.ResponseString;
                
                OnDownloadComplete( state );

            }
            catch ( WebException ex )
            {
                ex.Data.Add( "Uri", state.Request.RequestUri );
                ex.Data.Add( "Verb", state.Request.Method );

                state.StatusCode = ( (HttpWebResponse) ex.Response ).StatusCode;
                ex.Data.Add( "StatusCode", state.StatusCode );
                ex.Data.Add( "StatusDescription", ( (HttpWebResponse) ex.Response ).StatusDescription );
   
                state.ErrorMessage = string.Format( "Call to {0} had a Webexception. {1}   Status: {2}   Desc: {3}", state.Request.RequestUri, ex.Message, ex.Status, ( (HttpWebResponse) ex.Response ).StatusDescription );
                state.Exception = ex;
                state.Expiration = DateTime.UtcNow;
                
                MXDevice.Log.Error( state.ErrorMessage );
                MXDevice.Log.Error( ex );

                OnError( state );
            }
            catch ( Exception ex )
            {
                ex.Data.Add( "Uri", state.Request.RequestUri );
                ex.Data.Add( "Verb", state.Request.Method );

                state.ErrorMessage = string.Format( "Call to {0} had an Exception. {1}", state.Request.RequestUri, ex.Message );
                state.Exception = ex;
                state.StatusCode = (HttpStatusCode) ( -1 );
                state.Expiration = DateTime.UtcNow;

                MXDevice.Log.Error( state.ErrorMessage );
                MXDevice.Log.Error( ex );
                OnError( state );
            }
            finally
            {
                if ( state.Response != null )
                    state.Response.Close();
                state.Request = null;

                allDone.Set();
            }
        }

        public class FetchParameters
        {
            public string Uri
            {
                get;
                set;
            }
            public Dictionary<string, string> Headers
            {
                get;
                set;
            }
            public string FileName
            {
                get;
                set;
            }
        }

    }

    /// <summary>
    /// subclass to store information for Asynchronous file
    /// </summary>
    public class RequestState
    {
        public string FileName
        {
            get;
            set;
        }
        public HttpWebRequest Request
        {
            get;
            set;
        }
        public HttpWebResponse Response
        {
            get;
            set;
        }
        public string ResponseString
        {
            get;
            set;
        }
        public byte[] ResponseBytes
        {
            get;
            set;
        }
        public DateTime Expiration
        {
            get;
            set;
        }
        public string AbsoluteUri
        {
            get;
            set;
        }
        public string Verb
        {
            get;
            set;
        }
        public HttpStatusCode StatusCode
        {
            get;
            set;
        }
        public Exception Exception
        {
            get;
            set;
        }
        public string ErrorMessage
        {
            get;
            set;
        }
        public Dictionary<string, string> Data
        {
            get;
            set;
        }
        public RequestState()
        {
            Request = null;
            Response = null;
            Data = new Dictionary<string, string>();
        }
    }

}

