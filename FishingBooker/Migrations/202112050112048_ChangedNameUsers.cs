namespace FishingBooker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedNameUsers : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AspNetUsers", newName: "RegUsers");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.RegUsers", newName: "AspNetUsers");
        }
    }
}
