using JJ.Apps.SetText.MonoCross.Online.Helpers;
using JJ.Apps.SetText.MonoCross.Online.SetTextAppService;
using MonoCross.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JJ.Apps.SetText.MonoCross
{
    public class SetTextController : MXController<SetTextViewModel>
    {
        public override string Load(Dictionary<string, string> parameters)
        {
            using (SetTextAppServiceClient service = ServiceHelper.CreateSetTextAppServiceClient())
            {
                Model = service.Show();
            }

            return ViewPerspective.Read;
        }
    }
}