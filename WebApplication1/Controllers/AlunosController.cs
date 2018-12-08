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
            string email = Session.Get<string>("ContaCorrente");
            var conta = db.Alunos.Where(x => x.Nome == email).FirstOrDefault();

            return View(conta.Preferencias.ToList());
        }

        public ActionResult Candidatura(int? id)
        {
            string email = Session.Get<string>("ContaCorrente");
            var aluno = db.Alunos.Where(x => x.Nome == email).FirstOrDefault();
            var proposta = db.Propostas.Where(x => x.PropostaId == id).FirstOrDefault();

            if (email != null && aluno != null)
            {

                //db.Alunos.Where(x => x.Nome == email).FirstOrDefault().Preferencias.Add(db.Propostas.Where(x => x.PropostaId == id).FirstOrDefault());
                //proposta.AlunoAtribuidoId = aluno.;
                aluno.Preferencias.Add(proposta);
                db.SaveChanges();
                return RedirectToAction("ConsultarPreferencias");
            }

            //Session.Set("ContaCorrente", );

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
