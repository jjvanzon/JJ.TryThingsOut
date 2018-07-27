using System.Collections.Generic;

namespace MonoCross.Utilities.Network
{
    public interface INetwork
    {
        IFetcher Fetcher { get; }
        IPoster Poster { get; }
        
        string Get( string uri );
        string Get( string uri, Dictionary<string, string> headers );

        string PostBytes( string uri, byte[] postBytes, string contentType );
        string PostBytes( string uri, byte[] postBytes, string contentType, Dictionary<string, string> headers );

        string PostObject( string uri, object postObject );
        string PostObject( string uri, object postObject, Dictionary<string, string> headers );

        string PostString( string uri, string postString );
        string PostString( string uri, string postString, Dictionary<string, string> headers );


        //static string Request( string uri );
        //static string Request( string uri, params string[] args );

        //static string Post( string uri, Dictionary<string, string> parameters );
        //static string Post( string uri, Dictionary<string, string> parameters, params string[] args );
        //static string Post( string uri, object obj );
        //static string Post( string uri, object obj, params string[] args );

        //static XDocument LoadXml( string uri );
        //static XDocument LoadXml( string uri, params string[] args );

    }
}
