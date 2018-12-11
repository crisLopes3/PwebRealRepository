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

        public Aluno AlunoCriador { get; set; }
        public Docente DocenteCriador { get; set; }
        public Empresa EmpresaCriador { get; set; }

        public Aluno AlunoRecetor { get; set; }
        public Docente DocenteRecetor { get; set; }
        public Empresa EmpresaRecetor { get; set; }


        public Mensagem()
        {
            Data = DateTime.Now;
        }
    }
}