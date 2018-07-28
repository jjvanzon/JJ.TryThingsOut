using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class MerkRepository
    {
        private Model1Container db = new Model1Container();

        public IQueryable<Merk> GetMerksOfModule(int moduleID)
        {
            ModuleRepository moduleRepository = new ModuleRepository();
            int moduleByteValue = moduleRepository.GetModuleByteValue(moduleID);

            return
                from merk in db.Merks
                where (merk.IsActiefInModule & moduleByteValue) == moduleByteValue && merk.IsActief == true
                orderby merk.VolgNummer
                select merk;
        }

        public IQueryable<Artikel> GetArtikelsOfModule(int moduleID, int merkID)
        {
            Merk merk = db.Merks.Single(m => m.Id == merkID);

            ModuleRepository moduleRepository = new ModuleRepository();
            int moduleByteValue = moduleRepository.GetModuleByteValue(moduleID);

            return
                from artikel in merk.Artikels.AsQueryable()
                where
                    (artikel.IsActiefInModule & moduleByteValue) == moduleByteValue &&
                    artikel.IsActief == true &&
                    artikel.Categorie.Id == 112 // Which category is this?
                orderby artikel.NaamCommercieel
                select artikel;
        }
    }
}
