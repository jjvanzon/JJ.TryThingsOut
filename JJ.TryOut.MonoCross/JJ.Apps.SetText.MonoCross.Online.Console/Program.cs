using JJ.Apps.SetText.MonoCross.Online.SetTextAppService;
using MonoCross.Console;
using MonoCross.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Apps.SetText.MonoCross.Online.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // initialize container
            MXConsoleContainer.Initialize(new App());

            // initialize views
            MXConsoleContainer.AddView<SetTextViewModel>(new SetTextView(), ViewPerspective.Read);

            // navigate to first view
            MXConsoleContainer.Navigate(MXContainer.Instance.App.NavigateOnLoad);
        }
    }
}
