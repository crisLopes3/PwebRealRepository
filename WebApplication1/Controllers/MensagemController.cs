using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MensagemController : Controller
    {
        private Context db = new Context();
        // GET: Mensagem
        public ActionResult NovaMensagem()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NovaMensagem(Mensagem mensagem)
        {
            if (ModelState.IsValid)
            {
                var idCriador = User.Identity.GetUserId();
                var Criador = db.Users.Where(x => x.Id == idCriador).First();
                mensagem.Remetente = Criador.Email;
                var destinatario = db.Users.Where(x => x.Email == mensagem.Destinatario).First();
                destinatario.MensagensRecebidas.Add(mensagem);         
                Criador.MensagensCriadas.Add(mensagem);
                db.SaveChanges();
            }
            return RedirectToAction("Index","Home");
        }

        public ActionResult ListaMensagensEnviadas()
        {
            var idUser = User.Identity.GetUserId();
            var mesagensEnviadas = db.Mensagens.Where(x => x.Criador.Id == idUser).ToList();
            return View(mesagensEnviadas);
        }

        public ActionResult ListaMensagensRecebidas()
        {
            var idUser = User.Identity.GetUserId();
            var mesagensRecebidas = db.Mensagens.Where(x => x.Recetor.Id == idUser).ToList();
            return View(mesagensRecebidas);
        }
        public ActionResult DetalhesMensagem(int? id)
        {
            var mensagem = db.Mensagens.Where(x => x.MensagemId == id).First();
            return View(mensagem);
        }
        public JsonResult VerificaDestinario(string destino)
        {
            return Json(!db.Users.Any(x=>x.Email==destino), JsonRequestBehavior.AllowGet);
        }

    }
}