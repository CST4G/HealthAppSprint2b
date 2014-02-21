namespace healthApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Services", "ClientFirstName", c => c.String(nullable: false, maxLength: 12));
            AddColumn("dbo.Services", "ClientLastName", c => c.String(nullable: false, maxLength: 12));
            AddColumn("dbo.Tasks", "ClientFirstName", c => c.String());
            AddColumn("dbo.Tasks", "ClientLastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "ClientLastName");
            DropColumn("dbo.Tasks", "ClientFirstName");
            DropColumn("dbo.Services", "ClientLastName");
            DropColumn("dbo.Services", "ClientFirstName");
        }
    }
}
