namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Docentes", "Nome", c => c.String(nullable: false));
            AddColumn("dbo.Empresas", "Nome", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Empresas", "Nome");
            DropColumn("dbo.Docentes", "Nome");
        }
    }
}
