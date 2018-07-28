using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoCross.Utilities.Network
{
    public class NetworkAsynch : INetwork
    {
        public IFetcher Fetcher
        {
            get
            {
                return new FetcherAsynch();
            }
        }
        public IPoster Poster
        {
            get
            {
                return new PosterAsynch();
            }
        }

        public string Get( string uri )
        {
            NetworkResponse networkResponse = Fetcher.Fetch( uri, (Dictionary<string, string>) null );
            return networkResponse.ResponseString;
        }

        public string Get( string uri, Dictionary<string, string> headers )
        {
            NetworkResponse networkResponse = Fetcher.Fetch( uri, headers );
            return networkResponse.ResponseString;
        }

        public string PostBytes( string uri, byte[] postBytes, string contentType )
        {
            NetworkResponse NetworkResponse = Poster.PostBytes( uri, postBytes, contentType, (Dictionary<string, string>) null );
            return NetworkResponse.ResponseString;
        }

        public string PostBytes( string uri, byte[] postBytes, string contentType, Dictionary<string, string> headers )
        {
            NetworkResponse networkResponse = Poster.PostBytes( uri, postBytes, contentType, headers );
            return networkResponse.ResponseString;
        }

        public string PostObject( string uri, object postObject )
        {
            NetworkResponse networkResponse = Poster.PostObject( uri, postObject, (Dictionary<string, string>) null );
            return networkResponse.ResponseString;
        }

        public string PostObject( string uri, object postObject, Dictionary<string, string> headers )
        {
            NetworkResponse networkResponse = Poster.PostObject( uri, postObject, headers );
            return networkResponse.ResponseString;
        }

        public string PostString( string uri, string postString )
        {
            NetworkResponse networkResponse = Poster.PostString( uri, postString, (Dictionary<string, string>) null );
            return networkResponse.ResponseString;
        }

        public string PostString( string uri, string postString, Dictionary<string, string> headers )
        {
            NetworkResponse networkResponse = Poster.PostString( uri, postString, headers );
            return networkResponse.ResponseString;
        }
    }
}
