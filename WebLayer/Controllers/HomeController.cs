using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLComponents;

using System.Data;
using System.Diagnostics;
using DataAccessLayer;

namespace WebLayer.Controllers
{
    public class TiposDocumentos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Sigla { get; set; }
    }
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Inicia el contador:
            //DataAccessObject ourDB2 = new DataAccessObject("DBModels");
            //AtomicTransaction atom = ourDB2.CreateAtomicTransaction();
            //dynamic data = new { Nombre="Cedula de ciudadania", Sigla="CC" };
            //int aa=ourDB2.ExecuteNonQuery("INSERT INTO gen.tdocumento(nombre, sigla) VALUES(@Nombre, @Sigla);", data, atom);
            //atom.Commit();
            TransactionResult ff = new TransactionResult();
            ff.prueba();
            DataAccessObject ourDB = new DataAccessObject("DBModels");
           // IEnumerable<TiposDocumentos> a = ourDB.ExecuteReader<TiposDocumentos>("SELECT id, nombre, sigla FROM gen.tdocumento;");
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