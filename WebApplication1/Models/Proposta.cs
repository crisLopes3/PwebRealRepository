using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class Proposta
    {
        public int PropostaId { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public string LocalEstagio { get; set; }

        [Required]
        public TipoProposta TipoProposta { get; set; }

        [Required]
        public Ramo Ramo { get; set; }

        public bool Estado { get; set; }

        public string Justificacao { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataInicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataFim { get; set; }

        [DataType(DataType.MultilineText)]
        public string Objetivos { get; set; }

      
        public virtual Aluno PropostaAlunoAtribuido { get; set; }
        [InverseProperty("Preferencias")]
        public virtual ICollection<Aluno> Alunos { get; set; }

        public int ?NotaAlunoAvaliade { get; set; }
        public int ?NotaEmpresaAvaliada { get; set; }

        public virtual Empresa EmpresaCriador { get; set; }

        public virtual Docente DocenteCriador { get; set; }
        public virtual ICollection<Docente> DocentesAtribuidos { get; set; }

        //[NotMapped]
        //public PostedDocentes PostedDocentes { get; set; }

        public Proposta()
        {
            Alunos = new HashSet<Aluno>();
            DocentesAtribuidos = new HashSet<Docente>();
        }
    }

    //[NotMapped]
    //public class PostedDocentes
    //{
    //    [NotMapped]
    //    public string[] DocentesIds { get; set; }
    //}
}