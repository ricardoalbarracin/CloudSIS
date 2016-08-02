using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;

namespace WebLayer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DataAccessObject a = new DataAccessObject("DBModels");
            a.cc();
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