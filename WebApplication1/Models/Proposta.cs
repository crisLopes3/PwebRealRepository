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
        //[ForeignKey("AlunoAtribuido")]
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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataInicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataFim { get; set; }

        [DataType(DataType.MultilineText)]
        public string Objetivos { get; set; }

        //public int? AlunoId { get; set; }
        //[ForeignKey("AlunoPropostaAtribuida")]
        //public int? AlunoPropostaAtribuidaId { get; set; }
        public virtual Aluno PropostaAlunoAtribuido { get; set; }
        //[InverseProperty("Aluno")]
        //public virtual ICollection<Aluno> AlunoAtribuidos { get; set; }


        [InverseProperty("Preferencias")]
        public virtual ICollection<Aluno> Alunos { get; set; }
        public virtual ICollection<Docente> Docentes { get; set; }
        public virtual ICollection<Empresa> Empresas { get; set; }

        public Proposta()
        {
            Alunos = new HashSet<Aluno>();
            Docentes = new HashSet<Docente>();
            Empresas = new HashSet<Empresa>();
        }
    }
}