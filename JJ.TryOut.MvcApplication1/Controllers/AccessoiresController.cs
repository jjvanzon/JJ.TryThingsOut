using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class AccessoiresController : Controller
    {
        private SiteModuleEnum ModuleID = SiteModuleEnum.Accessoires; 

        //
        // GET: /Accessoires/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Merk(int id)
        {
            Merk merk = new Merk() { Id = id, Naam = "Nokia" };
            ViewData["ModuleID"] = ModuleID;
            return View(merk);
        }

        public ActionResult Categorie(int id)
        {
            Categorie categorie = new Categorie() { Id = id, Naam = "Carkits" };
            return View();
        }
    }
}
