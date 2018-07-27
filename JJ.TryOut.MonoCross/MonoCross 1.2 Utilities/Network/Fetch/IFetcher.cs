using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoCross.Utilities.Network
{
    public interface IFetcher
    {
        NetworkResponse Fetch( string uri );
        NetworkResponse Fetch( string uri, Dictionary<string, string> headers );
        NetworkResponse Fetch( string uri, string filename );
        NetworkResponse Fetch( string uri, string filename, Dictionary<string, string> headers );
    }
}
