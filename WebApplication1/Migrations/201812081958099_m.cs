namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alunos",
                c => new
                    {
                        AlunoId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Ramo = c.Int(nullable: false),
                        DisciplinasPorFazer = c.String(nullable: false),
                        DisciplinasFeitas = c.String(nullable: false),
                        PropostaId = c.Int(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AlunoId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Propostas",
                c => new
                    {
                        PropostaId = c.Int(nullable: false),
                        Descricao = c.String(nullable: false),
                        LocalEstagio = c.String(nullable: false),
                        TipoProposta = c.Int(nullable: false),
                        Ramo = c.Int(nullable: false),
                        Estado = c.Boolean(nullable: false),
                        DataInicio = c.DateTime(nullable: false),
                        DataFim = c.DateTime(nullable: false),
                        Objetivos = c.String(),
                        AlunoId = c.Int(),
                    })
                .PrimaryKey(t => t.PropostaId)
                .ForeignKey("dbo.Alunos", t => t.PropostaId)
                .Index(t => t.PropostaId);
            
            CreateTable(
                "dbo.Docentes",
                c => new
                    {
                        DocenteId = c.Int(nullable: false, identity: true),
                        PertenceComissao = c.Boolean(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DocenteId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Empresas",
                c => new
                    {
                        EmpresaId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.EmpresaId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.PropostaAlunoes",
                c => new
                    {
                        Proposta_PropostaId = c.Int(nullable: false),
                        Aluno_AlunoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Proposta_PropostaId, t.Aluno_AlunoId })
                .ForeignKey("dbo.Propostas", t => t.Proposta_PropostaId, cascadeDelete: true)
                .ForeignKey("dbo.Alunos", t => t.Aluno_AlunoId, cascadeDelete: true)
                .Index(t => t.Proposta_PropostaId)
                .Index(t => t.Aluno_AlunoId);
            
            CreateTable(
                "dbo.DocentePropostas",
                c => new
                    {
                        Docente_DocenteId = c.Int(nullable: false),
                        Proposta_PropostaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Docente_DocenteId, t.Proposta_PropostaId })
                .ForeignKey("dbo.Docentes", t => t.Docente_DocenteId, cascadeDelete: true)
                .ForeignKey("dbo.Propostas", t => t.Proposta_PropostaId, cascadeDelete: true)
                .Index(t => t.Docente_DocenteId)
                .Index(t => t.Proposta_PropostaId);
            
            CreateTable(
                "dbo.EmpresaPropostas",
                c => new
                    {
                        Empresa_EmpresaId = c.Int(nullable: false),
                        Proposta_PropostaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Empresa_EmpresaId, t.Proposta_PropostaId })
                .ForeignKey("dbo.Empresas", t => t.Empresa_EmpresaId, cascadeDelete: true)
                .ForeignKey("dbo.Propostas", t => t.Proposta_PropostaId, cascadeDelete: true)
                .Index(t => t.Empresa_EmpresaId)
                .Index(t => t.Proposta_PropostaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.EmpresaPropostas", "Proposta_PropostaId", "dbo.Propostas");
            DropForeignKey("dbo.EmpresaPropostas", "Empresa_EmpresaId", "dbo.Empresas");
            DropForeignKey("dbo.Empresas", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DocentePropostas", "Proposta_PropostaId", "dbo.Propostas");
            DropForeignKey("dbo.DocentePropostas", "Docente_DocenteId", "dbo.Docentes");
            DropForeignKey("dbo.Docentes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PropostaAlunoes", "Aluno_AlunoId", "dbo.Alunos");
            DropForeignKey("dbo.PropostaAlunoes", "Proposta_PropostaId", "dbo.Propostas");
            DropForeignKey("dbo.Propostas", "PropostaId", "dbo.Alunos");
            DropForeignKey("dbo.Alunos", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.EmpresaPropostas", new[] { "Proposta_PropostaId" });
            DropIndex("dbo.EmpresaPropostas", new[] { "Empresa_EmpresaId" });
            DropIndex("dbo.DocentePropostas", new[] { "Proposta_PropostaId" });
            DropIndex("dbo.DocentePropostas", new[] { "Docente_DocenteId" });
            DropIndex("dbo.PropostaAlunoes", new[] { "Aluno_AlunoId" });
            DropIndex("dbo.PropostaAlunoes", new[] { "Proposta_PropostaId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Empresas", new[] { "UserId" });
            DropIndex("dbo.Docentes", new[] { "UserId" });
            DropIndex("dbo.Propostas", new[] { "PropostaId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Alunos", new[] { "UserId" });
            DropTable("dbo.EmpresaPropostas");
            DropTable("dbo.DocentePropostas");
            DropTable("dbo.PropostaAlunoes");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Empresas");
            DropTable("dbo.Docentes");
            DropTable("dbo.Propostas");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Alunos");
        }
    }
}
