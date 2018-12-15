using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class Mensagem
    {
        public int MensagemId { get; set; }

        [Required]
        [Display(Name = "Para: ")]
        [Remote("VerificaDestinario", "Mensagem", ErrorMessage = "Destino não existe")]
        public string Destinatario { get; set; }

        public string Remetente { get; set; } //usado so para a vista para mostrar quem enviou a mensagem
        [Required]
        public string Assunto { get; set; }
        [Required]
        public string Corpo { get; set; }

        public DateTime Data { get; set; }

        public ApplicationUser Criador { get; set; }
        public ApplicationUser Recetor { get; set; }

        public Mensagem()
        {
            Data = DateTime.Now;
        }
    }
}