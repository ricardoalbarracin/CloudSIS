using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;

using System.Data;
using System.Diagnostics;

namespace WebLayer.Controllers
{
    public class modelo
    {
        public int id { get; set; }
        public string cargo { get; set; }
        public string sede { get; set; }
        public int salario { get; set; }
    }
    public class HomeController : Controller
    {
        public ActionResult Index()
        {


            // Inicia el contador:
            Stopwatch tiempo = Stopwatch.StartNew();
            for (int i = 0; i < 10; i++)
            {
                DataAccessObject ourDB = new DataAccessObject("DBModels");
                IEnumerable<modelo> a = ourDB.ExecuteReader<modelo>("SELECT * FROM cargos;");
            }
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