namespace FishingBooker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewFieldInAplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegUsers", "Biography", c => c.String());
            AddColumn("dbo.RegUsers", "TotalScalePoints", c => c.Single(nullable: false));
            AddColumn("dbo.RegUsers", "Rating", c => c.Single(nullable: false));
            AddColumn("dbo.RegUsers", "RatingSum", c => c.Single(nullable: false));
            AddColumn("dbo.RegUsers", "RatingCount", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegUsers", "RatingCount");
            DropColumn("dbo.RegUsers", "RatingSum");
            DropColumn("dbo.RegUsers", "Rating");
            DropColumn("dbo.RegUsers", "TotalScalePoints");
            DropColumn("dbo.RegUsers", "Biography");
        }
    }
}
