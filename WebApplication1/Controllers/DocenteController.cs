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

        [Authorize(Roles = Constantes.Comissao + "," + Constantes.Docente)]
        public ActionResult ListarPropostasCriadas()
        {
            int id = Session.Get<int>("UserId");

            return View(db.Docentes.Where(x => x.DocenteId == id).FirstOrDefault().PropostasCriadas);
        }

        [Authorize(Roles = Constantes.Comissao + "," + Constantes.Docente)]
        public ActionResult ListarPropostasAssociadas()
        {
            int id = Session.Get<int>("UserId");

            return View(db.Docentes.Where(x => x.DocenteId == id).FirstOrDefault().PropostasAssociadas.ToList());
        }

        [Authorize(Roles = Constantes.Comissao + "," + Constantes.Docente)]
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

        [Authorize(Roles = Constantes.Comissao + "," + Constantes.Docente)]
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
        [Authorize(Roles = Constantes.Comissao + "," + Constantes.Docente)]
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

        [Authorize(Roles = Constantes.Comissao + "," + Constantes.Docente)]
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

        [Authorize(Roles = Constantes.Comissao + "," + Constantes.Docente)]
        public ActionResult AtribuirNotaAluno(int? idAluno)
        {
            var aluno = db.Alunos.Where(x => x.AlunoId == idAluno).FirstOrDefault();
            if (aluno != null)
            {
                return View(aluno);
            }
            return View();
        }

        [HttpPost]
        [Authorize(Roles = Constantes.Comissao + "," + Constantes.Docente)]
        public ActionResult AtribuirNotaAluno(Aluno Aluno, int? NotaAtribuida)
        {
            var aluno = db.Alunos.Where(x => x.AlunoId == Aluno.AlunoId).FirstOrDefault();

            if (aluno != null)
            {
                aluno.NotaAtribuida = NotaAtribuida;
                db.SaveChanges();

            }
            return RedirectToAction("ListarPropostasCriadas");
        }

        //////////////////////////////////////////////////////////////////COMISSAO
        [Authorize(Roles = Constantes.Comissao)]
        public ActionResult AtribuirPropostas()
        {
            ViewBag.max = db.Settings.Where(x => x.SettingId == "Max_Candidaturas").Select(x=>x.Value).FirstOrDefault();
            return View(db.Propostas.Where(x => x.Estado == true && x.PropostaAlunoAtribuido == null).ToList());
        }

        [Authorize(Roles = Constantes.Comissao)]
        public ActionResult VerCandidatos(int? idProposta)
        {
            if (idProposta > 0)
            {
                var proposta = db.Propostas.Where(x => x.PropostaId == idProposta).FirstOrDefault();
                ViewBag.PropostaId = proposta.PropostaId;
                return View(proposta.Alunos.ToList());
            }
            return RedirectToAction("AtribuirPropostas");
        }

        [Authorize(Roles = Constantes.Comissao)]
        public ActionResult AtribuirAluno(int idProposta, int idAluno)
        {
            var proposta = db.Propostas.Where(x => x.PropostaId == idProposta).FirstOrDefault();
            var aluno = db.Alunos.Where(x => x.AlunoId == idAluno).FirstOrDefault();

            if (aluno != null && proposta != null)
            {
                aluno.AlunoPropostaAtribuida = proposta;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = Constantes.Comissao)]
        public ActionResult PropostasAtribuidas()
        {
            return View(db.Propostas.Where(x => x.PropostaAlunoAtribuido != null).ToList());
        }
    
        [Authorize(Roles = Constantes.Comissao)]
        public ActionResult DefinirMaxCandidaturasPorAluno(string max)
        {

            var seting = db.Settings.Where(x => x.SettingId == "Max_Candidaturas").FirstOrDefault();
            if (seting == null)
                db.Settings.Add(new Setting { SettingId = "Max_Candidaturas", Value = max });
            else
                seting.Value = max;

            db.SaveChanges();

            return RedirectToAction("AtribuirPropostas");
        }

        //////////////////////////////////////////////////////////////////Estatisticas


        public ActionResult Estatisticas(int? tipoOrdenacao, int? AnoEscolhido)
        {

            ViewBag.TiposOrdenacao = new SelectList(new List<Object>{
                       new { value = 0 , text = "Com mais candidatos"  },
                       new { value = 1 , text = "Com mais candidatos num ano" },
                    }, "value", "text", 2);

            if (tipoOrdenacao!=null)
            {

                return View(db.Propostas.Where(x => x.Estado == true).OrderByDescending(x => x.Alunos.Count).ToList());
            }
            else if (tipoOrdenacao ==null && AnoEscolhido != null)
            {
          
                return View(db.Propostas.Where(x => x.Estado == true && x.DataInicio.Year == AnoEscolhido).OrderByDescending(x => x.Alunos.Count).ToList());
            }
            return View(db.Propostas.Where(x => x.Estado == true).ToList());
        }
    }
}

