using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using MonoCross.Utilities.Network;

namespace MonoCross.Utilities
{
    public delegate void NetworkResponseEvent( NetworkResponse networkResponse );

    public class NetworkResponse
    {
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
        public string Message
        {
            get;
            set;
        }
        public Exception Exception
        {
            get;
            set;
        }
        public string URI
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

        public NetworkResponse()
        {
        }
    }



}
