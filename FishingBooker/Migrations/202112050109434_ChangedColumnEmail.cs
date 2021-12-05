namespace FishingBooker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedColumnEmail : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "Email", newName: "EmailAddress");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "EmailAddress", newName: "Email");
        }
    }
}
