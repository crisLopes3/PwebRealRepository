using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private Context db = new Context();
        public ActionResult Index()
        {

            var max = db.Settings.Where(x => x.SettingId == "Max_Candidaturas").Select(x => x.Value).FirstOrDefault();
            Constantes.MaxCandidaturas = Int32.Parse(max);
            db.SaveChanges();
            return View();
        }   
    }
}