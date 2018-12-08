using System;
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
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Docente> Docentes { get; set; }
        public DbSet<Proposta> Propostas { get; set; }


        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Aluno>().HasMany<Proposta>(p => p.Preferencias)
        //        .WithMany(a => a.Alunos)
        //        .Map(cs =>
        //        {
        //            cs.MapLeftKey("AlunoId").MapRightKey("PropostaId").ToTable("AlunosPropostas");
        //        });
        //}

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Aluno>().HasOptional(a => a.PropostaAtribuida).
        //        WithRequired(p => p.AlunoAtribuido);
        //}

    }
}