namespace TravelAssistApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTouristPlaceModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TouristPlaces", "ImagePath", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TouristPlaces", "ImagePath", c => c.String());
        }
    }
}
