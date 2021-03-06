﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Context : ApplicationDbContext
    {
        public Context() /*: base("name=DefaultConnection")*/
        {

        }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Docente> Docentes { get; set; }
        public DbSet<Proposta> Propostas { get; set; }
        public DbSet<Mensagem> Mensagens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Aluno>()
                .HasOptional(a => a.AlunoPropostaAtribuida)
                .WithOptionalPrincipal(p => p.PropostaAlunoAtribuido);

        }
    }
}