namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Propostas", "NotaAlunoAvaliade", c => c.Int());
            AddColumn("dbo.Propostas", "NotaEmpresaAvaliada", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Propostas", "NotaEmpresaAvaliada");
            DropColumn("dbo.Propostas", "NotaAlunoAvaliade");
        }
    }
}
