namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mensagems", "Destinatario", c => c.String(nullable: false));
            AddColumn("dbo.Mensagems", "Assunto", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mensagems", "Assunto");
            DropColumn("dbo.Mensagems", "Destinatario");
        }
    }
}
