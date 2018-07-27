using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Apps.SetText.MonoCross.Online.SetTextAppService;
using MonoCross.Navigation;
using MonoCross.Console;

namespace JJ.Apps.SetText.MonoCross.Online.Console
{
    public class SetTextView : MXConsoleView<SetTextViewModel>
    {
        public override void Render()
        {
            System.Console.WriteLine(ResourceHelper.Labels.Text);
            System.Console.WriteLine(Model.Text );
            string newText = System.Console.ReadLine();
            // TODO: How do I call a save action on the controller?
        }
    }
}
