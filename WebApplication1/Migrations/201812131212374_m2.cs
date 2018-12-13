namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mensagems",
                c => new
                    {
                        MensagemId = c.Int(nullable: false, identity: true),
                        Corpo = c.String(nullable: false),
                        Data = c.DateTime(nullable: false),
                        Criador_Id = c.String(maxLength: 128),
                        Recetor_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MensagemId)
                .ForeignKey("dbo.AspNetUsers", t => t.Criador_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Recetor_Id)
                .Index(t => t.Criador_Id)
                .Index(t => t.Recetor_Id);
            
            AddColumn("dbo.Propostas", "Justificacao", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mensagems", "Recetor_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Mensagems", "Criador_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Mensagems", new[] { "Recetor_Id" });
            DropIndex("dbo.Mensagems", new[] { "Criador_Id" });
            DropColumn("dbo.Propostas", "Justificacao");
            DropTable("dbo.Mensagems");
        }
    }
}
