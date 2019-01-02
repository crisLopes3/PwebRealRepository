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
        [Authorize(Roles = Constantes.Empresa)]
        public ActionResult ListarPropostasCriadas()
        {
            int id = Session.Get<int>("UserId");

            return View(db.Empresas.Where(x => x.EmpresaId == id).FirstOrDefault().PropostasCriadas);
        }

        public ActionResult AvaliarEstagiarios(int? id)
        {
            var proposta = db.Propostas.Where(x => x.PropostaId == id).FirstOrDefault();
            var empresa = db.Empresas.Where(x => x.EmpresaId == proposta.EmpresaCriador.EmpresaId).FirstOrDefault();
            var aluno = proposta.PropostaAlunoAtribuido;

            if (proposta != null && empresa != null && aluno != null)
            {
                ViewBag.aluno =aluno.Nome ;

                ViewBag.Notas = new SelectList(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 });
                return View(proposta);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult AvaliarEstagiarios(string NomeAluno, int ?PropostaId, int? Avaliar)
        {
            var aluno = db.Alunos.Where(x => x.Nome == NomeAluno).FirstOrDefault();
            var Proposta = db.Propostas.Where(x => x.PropostaId == PropostaId).FirstOrDefault();

            if (aluno != null && Proposta != null)
            {
                Proposta.NotaAlunoAvaliade = Avaliar;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
        
}