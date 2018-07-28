using JJ.Apps.SetText.MonoCross.Online;
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
            Title = ResourceHelper.Titles.SetText;

            // Add navigation mappings.
            NavigationMap.Add("", new SetTextController());
            NavigationMap.Add("{Action}", new SetTextController());

            // Set default navigation URI.
            NavigateOnLoad = "";
        }
    }
}
