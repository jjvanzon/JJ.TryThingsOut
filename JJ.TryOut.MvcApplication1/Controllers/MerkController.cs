using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class MerkController : Controller
    {
        //
        // GET: /Artikels/

        public ActionResult Artikels(int moduleID, int merkID)
        {
            AccessoiresMerkViewModel viewModel = new AccessoiresMerkViewModel();

            MerkRepository repository = new MerkRepository();
            viewModel.Artikels = repository.GetArtikelsOfModule(merkID, moduleID).ToList();

            return View();
        }
    }
}
