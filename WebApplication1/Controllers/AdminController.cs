﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
        private ApplicationDbContext context = new ApplicationDbContext();

        [Authorize(Roles = Constantes.Admin)]
        public ActionResult ListarAlunos()
        {
            return View(db.Alunos.ToList());
        }
        [Authorize(Roles = Constantes.Admin)]
        public ActionResult ListarEmpresas()
        {
            return View(db.Empresas.ToList());
        }
        [Authorize(Roles = Constantes.Admin)]
        public ActionResult ListarDocentes()
        {
            return View(db.Docentes.ToList());
        }

        [Authorize(Roles = Constantes.Admin)]
        public ActionResult RetirarDaComissao(int? id)
        {
            var docente = db.Docentes.Where(x => x.DocenteId == id).FirstOrDefault();

            if (docente != null)
            {
                docente.PertenceComissao = false;
                db.SaveChanges();
            }

            return RedirectToAction("ListarDocentes");
        }

        [Authorize(Roles = Constantes.Admin)]
        public ActionResult AdicionarNaComissao(int? id)
        {
            var docente = db.Docentes.Where(x => x.DocenteId == id).FirstOrDefault();

            if (docente != null)
            {
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                UserManager.AddToRole(docente.UserId, "Comissao");
              
                docente.PertenceComissao = true;
                db.SaveChanges();
            }

            return RedirectToAction("ListarDocentes");
        }
        [Authorize(Roles = Constantes.Admin)]
        public ActionResult ListarComissao()
        {
            return View(db.Docentes.Where(x => x.PertenceComissao == true).ToList());
        }

        [Authorize(Roles = Constantes.Admin)]
        [HttpGet]
        public ActionResult EliminarPropostas()
        {
            return View(db.Propostas.ToList());
        }

        [Authorize(Roles = Constantes.Admin)]
        public ActionResult EliminarProposta(int? id)
        {
            var proposta = db.Propostas.Where(x => x.PropostaId == id).FirstOrDefault();
            

            if(proposta.TipoProposta == TipoProposta.Projeto)
            {
                foreach (var docente in proposta.DocentesAtribuidos)
                {
                    docente.PropostasAssociadas.Remove(proposta);
                }         
            }

            db.Propostas.Remove(proposta);


            db.SaveChanges();

            return RedirectToAction("EliminarPropostas");
        }
    }
}
