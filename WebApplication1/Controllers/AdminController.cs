using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        private Context db = new Context();

        public ActionResult ListarAlunos()
        {
            return View(db.Alunos.ToList());
        }
        public ActionResult ListarEmpresas()
        {
            return View(db.Empresas.ToList());
        }
        public ActionResult ListarDocentes()
        {
            return View(db.Docentes.ToList());
        }
    }
}
