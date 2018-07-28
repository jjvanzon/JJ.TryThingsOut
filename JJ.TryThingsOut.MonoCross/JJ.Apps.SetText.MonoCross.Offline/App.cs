using MonoCross.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Apps.SetText.MonoCross
{
    public class App : MXApplication
    {
        public override void OnAppLoad()
        {
            // Set the application title.
            Title = "Set Text"; // TODO: put in config file.

            // Add navigation mappings.
            NavigationMap.Add("", new SetTextController());

            // Set default navigation URI.
            NavigateOnLoad = "";
        }
    }
}
