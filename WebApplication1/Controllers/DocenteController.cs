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
    public class DocenteController : Controller
    {
        private Context db = new Context();

        public ActionResult ListarPropostasCriadas()
        {
            int id = Session.Get<int>("UserId");

            return View(db.Docentes.Where(x => x.DocenteId == id).FirstOrDefault().PropostasCriadas);
        }

        public ActionResult ListarPropostasAssociadas()
        {
            int id = Session.Get<int>("UserId");
            return View(db.Docentes.Where(x => x.DocenteId == id).FirstOrDefault().PropostasAssociadas);
        }


        public ActionResult AssociarDocentes(int? id)
        {
            if (id > 0)
            {
                Session.Set("IdProposta", id);
                ViewBag.DocentesAssociados = db.Propostas.Where(x => x.PropostaId == id).SelectMany(x => x.DocentesAtribuidos).Select(x => x.DocenteId).ToList();
            }

            int idUser = Session.Get<int>("UserId");
            return View(db.Docentes.Where(x => x.DocenteId != idUser).ToList());
        }

        public ActionResult Associar(int? id)
        {
            if (id > 0)
            {
                int idProposta = Session.Get<int>("IdProposta");
                var docente = db.Docentes.Where(x => x.DocenteId == id).FirstOrDefault();
                var proposta = db.Propostas.Where(x => x.PropostaId == idProposta).FirstOrDefault();
                proposta.DocentesAtribuidos.Add(docente);
                docente.PropostasAssociadas.Add(proposta);
                db.SaveChanges();
            }
            return RedirectToAction("ListarPropostasCriadas");
        }
        public ActionResult Desassociar(int? id)
        {
            if (id > 0)
            {
                int idProposta = Session.Get<int>("IdProposta");
                var docente = db.Docentes.Where(x => x.DocenteId == id).FirstOrDefault();
                var proposta = db.Propostas.Where(x => x.PropostaId == idProposta).FirstOrDefault();
                proposta.DocentesAtribuidos.Remove(docente);
                docente.PropostasAssociadas.Remove(proposta);
                db.SaveChanges();
            }
            return RedirectToAction("ListarPropostasCriadas");
        }


        public ActionResult SairDaProposta(int? id)
        {
            if (id > 0)
            {
                int idUser = Session.Get<int>("UserId");
                var docente = db.Docentes.Where(x => x.DocenteId == idUser).FirstOrDefault();
                var proposta = db.Propostas.Where(x => x.PropostaId == id).FirstOrDefault();

                proposta.DocentesAtribuidos.Remove(docente);
                docente.PropostasAssociadas.Remove(proposta);

                db.SaveChanges();
            }
            return RedirectToAction("ListarPropostasAssociadas");
        }




        //////////////////////////////////////////////////////////////////COMISSAO

        public ActionResult AtribuirPropostas()
        {
            return View(db.Propostas.Where(x=>x.Estado == true).ToList());
        }

        public ActionResult VerCandidatos(int? idProposta)
        {
            if(idProposta > 0)
            {
                var proposta = db.Propostas.Where(x => x.PropostaId == idProposta).FirstOrDefault();
                return View(proposta.Alunos.ToList());
            }
            return RedirectToAction("AtribuirPropostas");
        }
    }
}

