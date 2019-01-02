using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmpresaController : Controller
    {
        private Context db = new Context();
        // GET: Empresa

        public ActionResult ListarPropostasCriadas()
        {
            int id = Session.Get<int>("UserId");

            return View(db.Empresas.Where(x => x.EmpresaId == id).FirstOrDefault().PropostasCriadas);
        }
    }
}