using System.Collections.Generic;

namespace MonoCross.Utilities.Network
{
    public interface IPoster
    {
        NetworkResponse PostBytes( string uri, byte[] postBytes, string contentType );
        NetworkResponse PostBytes( string uri, byte[] postBytes, string contentType, string verb );
        NetworkResponse PostBytes( string uri, byte[] postBytes, string contentType, Dictionary<string, string> headers );
        NetworkResponse PostBytes( string uri, byte[] postBytes, string contentType, string verb, Dictionary<string, string> headers );

        NetworkResponse PostObject( string uri, object postObject );
        NetworkResponse PostObject( string uri, object postObject, string verb );
        NetworkResponse PostObject( string uri, object postObject, Dictionary<string, string> headers );
        NetworkResponse PostObject( string uri, object postObject, string verb, Dictionary<string, string> headers );

        NetworkResponse PostString( string uri, string postString );
        NetworkResponse PostString( string uri, string postString, string contentType );
        NetworkResponse PostString( string uri, string postString, string contentType, string verb );
        NetworkResponse PostString( string uri, string postString, Dictionary<string, string> headers );
        NetworkResponse PostString( string uri, string postString, string contentType, Dictionary<string, string> headers );
        NetworkResponse PostString( string uri, string postString, string contentType, string verb, Dictionary<string, string> headers );
    }
}
