using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoCross.Utilities.Network
{
    public class NetworkSynch : INetwork
    {
        public IFetcher Fetcher
        {
            get
            {
                return new FetcherSynch();
            }
        }
        public IPoster Poster
        {
            get
            {
                return new PosterSynch();
            }
        }

        public string Get( string uri )
        {
            IFetcher fetcher = new FetcherSynch();
            NetworkResponse networkResponse = fetcher.Fetch( uri, (Dictionary<string, string>) null  );

            return networkResponse.ResponseString;
        }

        public string Get( string uri, Dictionary<string, string> headers )
        {
            IFetcher fetcher = new FetcherSynch();
            NetworkResponse networkResponse = fetcher.Fetch( uri, headers );

            return networkResponse.ResponseString;
        }

        public string PostBytes( string uri, byte[] postBytes, string contentType )
        {
            IPoster poster = new PosterSynch();
            NetworkResponse networkResponse = poster.PostBytes( uri, postBytes, contentType, (Dictionary<string, string>) null );

            return networkResponse.ResponseString;
        }

        public string PostBytes( string uri, byte[] postBytes, string contentType, Dictionary<string, string> headers )
        {
            IPoster poster = new PosterSynch();
            NetworkResponse networkResponse = poster.PostBytes( uri, postBytes, contentType, headers );

            return networkResponse.ResponseString;
        }

        public string PostObject( string uri, object postObject )
        {
            IPoster poster = new PosterSynch();
            NetworkResponse networkResponse = poster.PostObject( uri, postObject, (Dictionary<string, string>) null );

            return networkResponse.ResponseString;
        }

        public string PostObject( string uri, object postObject, Dictionary<string, string> headers )
        {
            IPoster poster = new PosterSynch();
            NetworkResponse NetworkResponse = poster.PostObject( uri, postObject, headers );

            return NetworkResponse.ResponseString;
        }
        
        public string PostString( string uri, string postString )
        {
            IPoster poster = new PosterSynch();
            NetworkResponse networkResponse = poster.PostString( uri, postString, (Dictionary<string, string>) null );

            return networkResponse.ResponseString;
        }

        public string PostString( string uri, string postString, Dictionary<string, string> headers )
        {
            IPoster poster = new PosterSynch();
            NetworkResponse networkResponse = poster.PostString( uri, postString, headers );

            return networkResponse.ResponseString;
        }
    }
}
