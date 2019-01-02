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
            if (User.IsInRole("Aluno"))
            {
                int id = Session.Get<int>("UserId");
                ViewBag.AlunoPreferencias = db.Alunos.Where(x => x.AlunoId == id).SelectMany(x => x.Preferencias).Select(x => x.PropostaId).ToList();
            }

            ViewBag.TiposOrdenacao = new SelectList(new List<Object>{
                       new { value = 0 , text = "Ramo"  },
                       new { value = 1 , text = "Local de Estágio" },
                    }, "value", "text", 2);

            ViewBag.TiposProposta = new SelectList(new List<Object>{
                       new { value = 0 , text = "Ramo"  },
                       new { value = 1 , text = "Local de Estágio" },
                    }, "value", "text", 2);

            if (User.IsInRole("Comissao"))
            {
                if (tipoOrdenacao == 1)
                    return View(db.Propostas.Where(x=> x.PropostaAlunoAtribuido == null).OrderBy(x => x.LocalEstagio));
                if (tipoOrdenacao == 0)
                    return View(db.Propostas.Where(x => x.PropostaAlunoAtribuido == null).OrderBy(x => x.Ramo));
                return View(db.Propostas.ToList());
            }
            else
            {
                if (tipoOrdenacao == 1)
                    return View(db.Propostas.Where(x => x.Estado == true && x.PropostaAlunoAtribuido == null).
                        OrderBy(x => x.LocalEstagio));
                if (tipoOrdenacao == 0)
                    return View(db.Propostas.Where(x => x.Estado == true 
                    && x.PropostaAlunoAtribuido == null).OrderBy(x => x.Ramo));
                return View(db.Propostas.Where(x => x.Estado == true && x.PropostaAlunoAtribuido == null).ToList());
                //return View(db.Propostas.Where(x => x.Estado == true).ToList());
            }
        }
        public ActionResult DetalhesProposta(int? id)
        {
            var Proposta = db.Propostas.Where(x => x.PropostaId == id).FirstOrDefault();

            return View(Proposta);
        }

            // GET: Propostas/Create
            public ActionResult Create()
        {
            int id = Session.Get<int>("UserId");
            ViewBag.Docentes = new SelectList(db.Docentes.Where(x=>x.DocenteId!=id).ToList(), "DocenteId", "Nome");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PropostaId,Descricao,LocalEstagio,TipoProposta,Ramo,DataInicio,DataFim,Objetivos,AlunoId")] Proposta proposta,string docentesAssociados)
        {
            if (ModelState.IsValid)
            {
                int id = Session.Get<int>("UserId");
                var docente = db.Docentes.Where(x => x.DocenteId == id).FirstOrDefault();

                docente.PropostasCriadas.Add(proposta);
                db.Propostas.Add(proposta);

                if (User.IsInRole("Docente"))
                {
                    if (docentesAssociados != "")
                    {
                        string[] ssize = docentesAssociados.Trim().Split(' ');
                    
                        foreach (var item in ssize)
                        {
                            int idDocente = Int32.Parse(item);

                            var docenteAssociado = db.Docentes.Where(x => x.DocenteId == idDocente).FirstOrDefault();

                            proposta.DocentesAtribuidos.Add(docente);
                            docenteAssociado.PropostasAssociadas.Add(proposta);
                        }
                    }
                 
                }

                   


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

        [HttpPost]
        public ActionResult AceitarProposta(Proposta proposta, string Justificacao)
        {
            var aux = db.Propostas.Where(x => x.PropostaId == proposta.PropostaId).FirstOrDefault();

            aux.Justificacao = string.Copy(Justificacao);

            aux.Estado = true;
            db.SaveChanges();
            return RedirectToAction("ListarPropostas");


        }
        public ActionResult RecusarProposta(int? id)
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
        [HttpPost]
        public ActionResult RecusarProposta(Proposta proposta, string Justificacao)
        {
            var aux = db.Propostas.Where(x => x.PropostaId == proposta.PropostaId).FirstOrDefault();

            aux.Justificacao = string.Copy(Justificacao);

            aux.Estado = false;
            db.SaveChanges();
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
