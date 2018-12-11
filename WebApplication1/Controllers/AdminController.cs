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


        public ActionResult RetirarDaComissao(int? id)
        {
            var docente = db.Docentes.Where(x => x.DocenteId == id).FirstOrDefault();
             
            if (docente != null)
            {

                //ApplicationUserManager a = null;
                //await a.AddToRoleAsync(docente.UserId, "Comissao");

                docente.PertenceComissao = false;
                db.SaveChanges();
            }

            return RedirectToAction("ListarDocentes");
        }

        public ActionResult AdicionarNaComissao(int? id)
        {
            var docente = db.Docentes.Where(x => x.DocenteId == id).FirstOrDefault();

            if (docente != null)
            {
                docente.PertenceComissao = true;
                db.SaveChanges();
            }

            return RedirectToAction("ListarDocentes");
        }

        public ActionResult ListarComissao()
        {
            return View(db.Docentes.Where(x=>x.PertenceComissao == true).ToList());
        }
    }
}
