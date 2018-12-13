using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Mensagem
    {
        public int MensagemId { get; set; }

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