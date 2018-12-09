using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Avaliacao
    {
        public int AvaliacaoId { get; set; }

        [Required]
        public int Nota { get; set; }

        [Required]
        public string Descricao { get; set; }

        public DateTime DataCriacao { get; set; }

        public virtual ICollection<Proposta> PropostasAvaliadas { get; set; }

        public Avaliacao()
        {
            DataCriacao = DateTime.Now;
        }
    }
}