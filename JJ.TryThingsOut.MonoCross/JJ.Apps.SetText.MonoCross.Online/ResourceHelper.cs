using JJ.Apps.SetText.MonoCross.Online.Helpers;
using JJ.Apps.SetText.MonoCross.Online.ResourceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Apps.SetText.MonoCross.Online
{
    public static class ResourceHelper
    {
        private static ResourceServiceClient _service;

        static ResourceHelper()
        {
            _service = ServiceHelper.CreateResourceServiceClient();

            InitializeResources();
        }

        public static Labels Labels { get; private set; }
        public static Titles Titles { get; private set; }
        public static Messages Messages { get; private set; }

        private static void InitializeResources()
        {
            string cultureName = GetCultureName();

            Labels = _service.GetLabels(cultureName);
            Titles = _service.GetTitles(cultureName);
            Messages = _service.GetMessages(cultureName);
        }

        private static string GetCultureName()
        {
            return System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
        }
    }
}
