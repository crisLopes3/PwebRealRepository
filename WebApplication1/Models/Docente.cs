using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class Docente 
    {
        public int DocenteId { get; set; }

        [Required]
        public string Nome { get; set; }

        public bool PertenceComissao  { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Proposta> Propostas { get; set; }
        //public virtual ICollection<Proposta> ProstasParaOrientar { get; set; }; // propostas que ele pode rejeitar e aceitar ser orientador propostas por outros docentes

        //public virtual ICollection<Mensagem> MesagensRecebidas { get; set; }
        //public virtual ICollection<Mensagem> MesagensEnviadas { get; set; }

        public Docente()
        {
            Propostas = new HashSet<Proposta>();
        }
    }
}