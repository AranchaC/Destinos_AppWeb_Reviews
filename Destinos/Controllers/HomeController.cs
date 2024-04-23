using Destinos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Destinos.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new ApplicationDbContext();
            var Destinos = db.Destinos.Where(x => x.Ciudad == "Madrid").OrderBy(x => x.Ciudad).ToList();

            //var Destinos1 = db.Destinos.SingleOrDefault(x => x.Ciudad == "Madrid");

            var Destinos3 = db.Destinos.FirstOrDefault();

            var Destinos4 = db.Destinos.Count();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}