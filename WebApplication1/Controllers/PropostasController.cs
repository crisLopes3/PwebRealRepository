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
    public class PropostasController : Controller
    {
        private Context db = new Context();

        // GET: Propostas
        public ActionResult ListarPropostas(int? tipoOrdenacao)
        {
            if(User.IsInRole("Aluno"))
            {
                int id = Session.Get<int>("UserId");
                ViewBag.AlunoPreferencias = db.Alunos.Where(x => x.AlunoId == id).SelectMany(x => x.Preferencias).Select(x => x.PropostaId).ToList();
            }

            ViewBag.TiposOrdenacao = new SelectList( new List<Object>{
                       new { value = 0 , text = "Ramo"  },
                       new { value = 1 , text = "Local de Estágio" },
                    },"value","text", 2);

            ViewBag.TiposProposta = new SelectList(new List<Object>{
                       new { value = 0 , text = "Ramo"  },
                       new { value = 1 , text = "Local de Estágio" },
                    }, "value", "text", 2);


            if (tipoOrdenacao == 1)
                return View(db.Propostas.OrderBy(x=>x.LocalEstagio));
            if (tipoOrdenacao == 0)
                return View(db.Propostas.OrderBy(x => x.Ramo));



            return View(db.Propostas.ToList());
        }

      
       
 

        // GET: Propostas/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PropostaId,Descricao,LocalEstagio,TipoProposta,Ramo,DataInicio,DataFim,Objetivos,AlunoId")] Proposta proposta)
        {
            if (ModelState.IsValid)
            {
                db.Propostas.Add(proposta);
                db.SaveChanges();
                return RedirectToAction("ListarPropostas");
            }

            return View(proposta);
        }

        // GET: Propostas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proposta proposta = db.Propostas.Find(id);
            if (proposta == null)
            {
                return HttpNotFound();
            }
            return View(proposta);
        }

        // POST: Propostas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proposta proposta = db.Propostas.Find(id);
            db.Propostas.Remove(proposta);
            db.SaveChanges();
            return RedirectToAction("ListarPropostas");
        }

        public ActionResult AceitarProposta(int? id)
        {
            var proposta = db.Propostas.Where(x => x.PropostaId == id).FirstOrDefault();

            if(proposta != null)
            {
                proposta.Estado = true;
                db.SaveChanges();
            }

            return RedirectToAction("ListarPropostas");
        }

        public ActionResult RegeitarProposta(int? id)
        {
            var proposta = db.Propostas.Where(x => x.PropostaId == id).FirstOrDefault();

            if (proposta != null)
            {
                proposta.Estado = false;
                db.SaveChanges();
            }

            return RedirectToAction("ListarPropostas");
        }

       



        protected override void Dispose(bool disposing)
        { 
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
