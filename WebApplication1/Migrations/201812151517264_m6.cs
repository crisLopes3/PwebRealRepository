namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mensagems", "remetente", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mensagems", "remetente");
        }
    }
}
