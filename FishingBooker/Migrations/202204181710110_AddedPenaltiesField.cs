namespace FishingBooker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPenaltiesField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegUsers", "Penalties", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegUsers", "Penalties");
        }
    }
}
