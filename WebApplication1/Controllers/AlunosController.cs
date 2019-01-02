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
    public class AlunosController : Controller
    {
        private Context db = new Context();

       
   

        //Editar Detalhes do aluno
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = db.Alunos.Find(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ramo,Nome")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aluno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aluno);
        }


        public ActionResult ConsultarPreferencias()
        {
            int alunoId = Session.Get<int>("UserId");
            var query = from p in db.Propostas
                        from a in p.Alunos
                        where a.AlunoId == alunoId
                        select p;

            return View(query.ToList());
        }

        public ActionResult Candidatura(int? id)
        {
            int alunoId = Session.Get<int>("UserId");

            var aluno = db.Alunos.Where(x => x.AlunoId == alunoId).FirstOrDefault();
            var proposta = db.Propostas.Where(x => x.PropostaId == id).FirstOrDefault();

            if (aluno != null && proposta != null)
            {
                aluno.Preferencias.Add(proposta);
                db.SaveChanges();
                return RedirectToAction("ListarPropostas", "Propostas");
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult RemoverCandidatura(int? id)
        {
            int alunoId = Session.Get<int>("UserId");

            var aluno = db.Alunos.Where(x => x.AlunoId == alunoId).FirstOrDefault();
            var proposta = db.Propostas.Where(x => x.PropostaId == id).FirstOrDefault();

            if (aluno != null && proposta != null)
            {
                aluno.Preferencias.Remove(proposta);
                db.SaveChanges();
                return RedirectToAction("ListarPropostas", "Propostas");
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        public ActionResult PerfilAluno(int? id)
        {
            var aluno = db.Alunos.Find(id);
            if (aluno != null)
            {
                return View(aluno);
            }
            return View();
        }
        public ActionResult AvaliarEmpresa()
        {
            int alunoId = Session.Get<int>("UserId");
            var aluno = db.Alunos.Where(x => x.AlunoId == alunoId).FirstOrDefault();
            var proposta = aluno.AlunoPropostaAtribuida;

            if (proposta != null && proposta.DataFim<DateTime.Now && proposta.NotaEmpresaAvaliada==null)
            {
                var empresa = db.Empresas.Where(x => x.EmpresaId == proposta.EmpresaCriador.EmpresaId).First();
                if (empresa != null)
                {
                    ViewBag.Proposta = proposta;
                    ViewBag.Empresa = empresa;
                    ViewBag.IdEmpresa = empresa.EmpresaId;
                    ViewBag.Notas = new SelectList(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 });
                    return View(empresa);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult AvaliarEmpresa(string NomeEmpresa, int ? Avaliar,string proposta)
        {
            var Empresa = db.Empresas.Where(x => x.Nome == NomeEmpresa).FirstOrDefault();
            var Proposta = db.Propostas.Where(x => x.Descricao == proposta).FirstOrDefault();
            
            if (Empresa != null && Proposta!=null)
            {
                Proposta.NotaEmpresaAvaliada = Avaliar;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
            

        public ActionResult PropostaAtribuida()
        {
            int alunoId = Session.Get<int>("UserId");
            return View(db.Alunos.Where(x=>x.AlunoId == alunoId).FirstOrDefault().AlunoPropostaAtribuida);
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
