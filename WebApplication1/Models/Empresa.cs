using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class Empresa
    {
        public int EmpresaId { get; set; }

        [Required]
        public string Nome { get; set; }

        public int? NotaAtribuida { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Proposta> PropostasCriadas { get; set; }



        public Empresa()
        {
            PropostasCriadas = new HashSet<Proposta>();
        }
    }
}