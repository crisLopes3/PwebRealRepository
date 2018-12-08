using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    [Table("Alunos")]
    public class Aluno
    {
        public int AlunoId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public Ramo Ramo { get; set; }

        [Required]
        public string DisciplinasPorFazer { get; set; }

        [Required]
        public string DisciplinasFeitas { get; set; }


        public int? PropostaId { get; set; }
        [ForeignKey("PropostaId")]
        public virtual Proposta PropostaAtribuida { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        //[InverseProperty("AlunoAtribuido")]
        public virtual ICollection<Proposta> Preferencias { get; set; }

        public Aluno()
        {
            Preferencias = new HashSet<Proposta>();
        }
    }
}