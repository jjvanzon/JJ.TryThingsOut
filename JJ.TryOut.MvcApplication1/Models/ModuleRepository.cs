using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class ModuleRepository
    {
        private Model1Container db = new Model1Container();

        public Module GetModule(int id)
        {
            return db.Modules.Single(x => x.Id == id);
        }

        public int GetModuleByteValue(int moduleID)
        {
            return db.Modules.Where(x => x.Id == moduleID).Single().ModuleByteValue;
        }
    }
}