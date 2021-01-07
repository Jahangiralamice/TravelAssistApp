namespace TravelAssistApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTouristPlaceModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TouristPlaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TouristPlaces");
        }
    }
}
