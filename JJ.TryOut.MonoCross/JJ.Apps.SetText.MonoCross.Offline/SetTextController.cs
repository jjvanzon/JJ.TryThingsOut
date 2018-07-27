using JJ.Apps.SetText.MonoCross.Helpers;
using JJ.Apps.SetText.Presenters;
using JJ.Apps.SetText.ViewModels;
using JJ.Framework.Persistence;
using JJ.Models.SetText.Persistence.RepositoryInterfaces;
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
            using (IContext context = PersistenceHelper.CreateContext())
            {
                IEntityRepository entityRepository = PersistenceHelper.CreateRepository<IEntityRepository>(context);
                SetTextPresenter presenter = new SetTextPresenter(entityRepository);
                Model = presenter.Show();
            }
            
            return ViewPerspective.Default;
        }
    }
}