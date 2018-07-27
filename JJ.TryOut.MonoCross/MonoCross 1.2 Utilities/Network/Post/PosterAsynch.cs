using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Text;

namespace MonoCross.Utilities.Network
{
    public class PosterAsynch : IPoster
    {
        #region class fields and properties

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


        const int DefaultTimeout = 60 * 1000; 

        /// <summary>
        /// Defines the delegate for factory events
        /// </summary>
        public delegate void PostRequestEventHandler( RequestState state );
        public delegate void PostRequestErrorHandler( RequestState state );

        /// <summary>
        /// Occurs when asynch download completes.
        /// </summary>
        public event PostRequestEventHandler OnComplete;
        public event PostRequestErrorHandler OnError;

        #endregion

        public NetworkResponse PostString( string uri, string postString )
        {
            // convert object to bytes via an XML
            byte[] postBytes = NetworkUtils.StrToByteArray( postString );

            // default headers to null rather than iApp.RequestInjectionHeaders as we cannot guarantee the 
            // default headers are relevant to the specified URI
            return PostBytes( uri, postBytes, "application/x-www-form-urlencoded", "POST", null );
        }
        public NetworkResponse PostString( string uri, string postString, string contentType )
        {
            // convert object to bytes via an XML
            byte[] postBytes = NetworkUtils.StrToByteArray( postString );

            // default headers to null rather than iApp.RequestInjectionHeaders as we cannot guarantee the 
            // default headers are relevant to the specified URI
            return PostBytes( uri, postBytes, contentType, "POST", null );
        }
        public NetworkResponse PostString( string uri, string postString, string contentType, string verb )
        {
            // convert object to bytes via an XML
            byte[] postBytes = NetworkUtils.StrToByteArray( postString );

            // default headers to null rather than iApp.RequestInjectionHeaders as we cannot guarantee the 
            // default headers are relevant to the specified URI
            return PostBytes( uri, postBytes, contentType, verb, null );
        }
        public NetworkResponse PostString( string uri, string postString, Dictionary<string, string> headers )
        {
            // convert object to bytes via an XML
            byte[] postBytes = NetworkUtils.StrToByteArray( postString );

            return PostBytes( uri, postBytes, "application/x-www-form-urlencoded", "POST", headers );
        }
        public NetworkResponse PostString( string uri, string postString, string contentType, Dictionary<string, string> headers )
        {
            // convert object to bytes via an XML
            byte[] postBytes = NetworkUtils.StrToByteArray( postString );

            return PostBytes( uri, postBytes, contentType, headers );
        }
        public NetworkResponse PostString( string uri, string postString, string contentType, string verb, Dictionary<string, string> headers )
        {
            // convert object to bytes via an XML
            byte[] postBytes = NetworkUtils.StrToByteArray( postString );

            return PostBytes( uri, postBytes, contentType, verb, headers );
        }

        public NetworkResponse PostObject( string uri, object postObject )
        {
            // convert object to bytes via an XML
            byte[] postBytes = NetworkUtils.XmlSerializeObjectToBytes( postObject );

            // default headers to null rather than iApp.RequestInjectionHeaders as we cannot guarantee the 
            // default headers are relevant to the specified URI
            return PostBytes( uri, postBytes, "application/xml", "POST", null );
        }
        public NetworkResponse PostObject( string uri, object postObject, string verb )
        {
            // convert object to bytes via an XML
            byte[] postBytes = NetworkUtils.XmlSerializeObjectToBytes( postObject );

            // default headers to null rather than iApp.RequestInjectionHeaders as we cannot guarantee the 
            // default headers are relevant to the specified URI
            return PostBytes( uri, postBytes, "application/xml", verb, null );
        }
        public NetworkResponse PostObject( string uri, object postObject, Dictionary<string, string> headers )
        {
            // convert object to bytes via an XML
            byte[] postBytes = NetworkUtils.XmlSerializeObjectToBytes( postObject );

            return PostBytes( uri, postBytes, "application/xml", headers );
        }
        public NetworkResponse PostObject( string uri, object postObject, string verb, Dictionary<string, string> headers )
        {
            // convert object to bytes via an XML
            byte[] postBytes = NetworkUtils.XmlSerializeObjectToBytes( postObject );

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
            // default headers to null rather than iApp.RequestInjectionHeaders as we cannot guarantee the 
            // default headers are relevant to the specified URI
            return PostBytes( uri, postBytes, contentType, "POST", headers );
        }
        public NetworkResponse PostBytes( string uri, byte[] postBytes, string contentType, string verb, Dictionary<string, string> headers )
        {
            PostNetworkResponse = new NetworkResponse();
            RequestParameters requestParameters = new RequestParameters()
            {
                PostBytes = postBytes,
                Uri = uri,
                Headers = headers,
                ContentType = contentType,
                Verb = verb
            };
            DateTime dtMetric = DateTime.UtcNow;

            // set callback and error handler
            OnComplete += new PostRequestEventHandler( PostRequest_OnComplete );
            OnError += new PostRequestErrorHandler( PostRequest_OnError );

            System.Threading.ThreadPool.QueueUserWorkItem( parameters =>
            {
                try
                {
                    RequestAsynch( parameters );
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
            }, requestParameters );

            // WaitOne returns true if autoEvent were signaled (i.e. process completed before timeout expired)
            // WaitOne returns false it the timeout expired before the process completed.
            if ( !autoEvent.WaitOne( DefaultTimeout ) )
            {
                //throw TimeoutException( "Waited ages for a thread to come in." );
                string message = "PosterAsynch call to RequestAsynch timed out. uri " + requestParameters.Uri;
                MXDevice.Log.Error( message );

                MXDevice.Log.Debug( string.Format( "PosterAsynch timed out: Uri: {0} Time: {1} milliseconds ", uri, DateTime.UtcNow.Subtract( dtMetric ).TotalMilliseconds ) );

                NetworkResponse networkResponse = new NetworkResponse()
                {
                    Message = message,
                    URI = requestParameters.Uri,
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
            MXDevice.Log.Debug( string.Format( "PosterAsynch Completed: Uri: {0} Time: {1} milliseconds  Size: {2} ", uri, DateTime.UtcNow.Subtract( dtMetric ).TotalMilliseconds, ( PostNetworkResponse.ResponseBytes != null ? PostNetworkResponse.ResponseBytes.Length : -1 ) ) );

            return PostNetworkResponse;
        }

        private void PostRequest_OnError( RequestState state )
        {
            Exception exc = new Exception( "PosterAsynch call to RequestAsynch threw an exception", state.Exception );
            MXDevice.Log.Error( exc );

            PostNetworkResponse.StatusCode = state.StatusCode;
            PostNetworkResponse.Message = exc.Message;
            PostNetworkResponse.Exception = exc;
            PostNetworkResponse.URI = state.Uri;
            PostNetworkResponse.Verb = state.Verb;

            MXDevice.PostNetworkResponse(PostNetworkResponse);
        }

        private void PostRequest_OnComplete( RequestState state )
        {
            PostNetworkResponse.StatusCode = state.StatusCode;
            PostNetworkResponse.URI = state.Uri;
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
                    PostNetworkResponse.Message = String.Format( "PosterAsynch completed but received HTTP {0}", state.StatusCode );
                    // Application.Log.Error( PostNetworkResponse.Message );
                    MXDevice.PostNetworkResponse(PostNetworkResponse);
                    return;
            }
        }

        private void RequestAsynch( Object parameters )
        {
            RequestParameters requestParameters = (RequestParameters) parameters;

            RequestState state = new RequestState()
            {
                PostBytes = requestParameters.PostBytes,
                Uri = requestParameters.Uri,
                Verb = requestParameters.Verb,
            };

            DateTime dtMetric = DateTime.UtcNow;

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create( state.Uri );

            request.Method = requestParameters.Verb; // Post, Put, Delete
            request.ContentType = requestParameters.ContentType;

#if !SILVERLIGHT && !MONO
            request.ContentLength = requestParameters.PostBytes.Length;
            request.AutomaticDecompression = DecompressionMethods.Deflate;
            request.KeepAlive = false;
#endif

            // To-Do: Refactor this central storage of header values to inject into an abstract network object.
            // inject request headers from iApp object.
            if ( requestParameters.Headers != null && requestParameters.Headers.Count() > 0 )
            {
                foreach ( string key in requestParameters.Headers.Keys )
                {
                    request.Headers[key] = requestParameters.Headers[key];
                }
            }

            state.Request = request;

            // Start the asynchronous request.
            IAsyncResult result = request.BeginGetRequestStream( new AsyncCallback( GetRequestStreamCallback ), state );

            if ( !allDone.WaitOne( DefaultTimeout ) )
            {
                OnError( state );
                return;
            }

        }

        private void GetRequestStreamCallback( IAsyncResult asynchronousResult )
        {
            // Get and fill the RequestState
            RequestState state = (RequestState) asynchronousResult.AsyncState;
            HttpWebRequest request = state.Request;

            // End the operation
            Stream postStream = request.EndGetRequestStream( asynchronousResult );

            // Write to the request stream.
            postStream.Write( state.PostBytes, 0, state.PostBytes.Length );
            postStream.Close();

            // Start the asynchronous operation to get the response
            IAsyncResult result = request.BeginGetResponse( new AsyncCallback( GetResponseCallback ), state );
        }

        private void GetResponseCallback( IAsyncResult asynchronousResult )
        {
            // Get and fill the RequestState
            RequestState state = (RequestState) asynchronousResult.AsyncState;

            HttpWebRequest request = state.Request;

            HttpWebResponse response = null;

            Stream streamResponse = null;
            StreamReader streamRead = null;
            try
            {
                // End the Asynchronous response and get the actual response object
                //To-Do: This has the potential for cross-domain calls, which are tricky in SL. This line doesn't work in the MDT POC.
                response = (HttpWebResponse) request.EndGetResponse( asynchronousResult );

                state.StatusCode = response.StatusCode;

                switch ( response.StatusCode )
                {
                    case HttpStatusCode.OK:
                    case HttpStatusCode.Created:
                    case HttpStatusCode.Accepted:
                        break;
                    case HttpStatusCode.NoContent:
                        state.ErrorMessage = String.Format( "No Content returned: Result {0} for {1}", state.StatusCode, state.Uri );
                        MXDevice.Log.Warn( state.ErrorMessage );
                        state.Expiration = DateTime.UtcNow;
                        OnComplete( state );
                        return;
                    default:
                        state.ErrorMessage = String.Format( "{0} failed. Received HTTP {1} for {2}", state.Verb, response.StatusCode, state.Uri );
                        MXDevice.Log.Error( state.ErrorMessage );
                        state.Expiration = DateTime.UtcNow;
                        OnComplete( state );

                        return;
                }

                // extract response into bytes and string.
                WebResponse webResponse = NetworkUtils.ExtractResponse( response );
                state.ResponseBytes = webResponse.ResponseBytes;
                state.ResponseString = webResponse.ResponseString;

                state.Expiration = response.Headers["Expires"].TryParseDateTimeUtc();

                OnComplete( state );
            }
            catch ( WebException ex )
            {
                // To-Do: Consider adding custom post exceptions...
                string message = string.Format( "Call to {0} had a Webexception. {1}   Status: {2}", state.Request.RequestUri, ex.Message, ex.Status );
                ex.Data.Add( "ResponseStatusCode", ex.Status );
                ex.Data.Add( "Uri", state.Uri );
                ex.Data.Add( "Verb", state.Verb );

                state.StatusCode = ( (HttpWebResponse) ex.Response ).StatusCode;
                ex.Data.Add( "StatusCode", state.StatusCode );
                ex.Data.Add( "StatusDescription", ( (HttpWebResponse) ex.Response ).StatusDescription );
                state.Exception = ex;

                OnError( state );
            }
            catch ( Exception ex )
            {
                // To-Do: Consider adding custom post exceptions...
                string message = string.Format( "Call to {0} had an Exception. {1}", state.Request.RequestUri, ex.Message );
                Exception qexc = new Exception( message, ex );
                qexc.Data.Add( "Uri", state.Uri );
                qexc.Data.Add( "Verb", state.Verb );
                state.Exception = qexc;

                OnError( state );
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
                state.Request = null;

                allDone.Set();
            }

        }

        public class RequestParameters
        {
            public byte[] PostBytes
            {
                get;
                set;
            }
            public string Uri
            {
                get;
                set;
            }
            public string ContentType
            {
                get;
                set;
            }
            public string Verb
            {
                get;
                set;
            }
            public Dictionary<string, string> Headers
            {
                get;
                set;
            }
        }

        /// <summary>
        /// subclass to store information for Asynchronous file
        /// </summary>
        public class RequestState
        {
            //public int BufferSize
            //{
            //    get;
            //    private set;
            //}
            public HttpWebRequest Request
            {
                get;
                set;
            }
            public string Uri
            {
                get;
                set;
            }
            public byte[] PostBytes
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
            public string Verb
            {
                get;
                set;
            }
            public DateTime Expiration
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
            public RegisteredWaitHandle Handle = null;
            public string ErrorMessage
            {
                get;
                set;
            }
            public RequestState()
            {
                // BufferSize = 6 * 1024;
                Request = null;
            }
        }


















    }
}
