using JJ.Apps.SetText.MonoCross.Online.ResourceService;
using JJ.Apps.SetText.MonoCross.Online.SetTextAppService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Apps.SetText.MonoCross.Online.Helpers
{
    internal static class ServiceHelper
    {
        public static SetTextAppServiceClient CreateSetTextAppServiceClient()
        {
            // TODO: Put is some sort of config file. I do not know what will be possible.
            string url = "http://localhost:51116/SetTextAppService.svc";
            var serviceClient = new SetTextAppServiceClient(new BasicHttpBinding(), new EndpointAddress(url));
            return serviceClient;
        }

        public static ResourceServiceClient CreateResourceServiceClient()
        {
            // TODO: Put is some sort of config file. I do not know what will be possible.
            string url = "http://localhost:51116/ResourceService.svc";
            var serviceClient = new ResourceServiceClient(new BasicHttpBinding(), new EndpointAddress(url));
            return serviceClient;
        }
    }
}
