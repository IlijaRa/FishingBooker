namespace FishingBooker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedColumnPassword : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "PasswordHash", newName: "Password");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "Password", newName: "PasswordHash");
        }
    }
}
