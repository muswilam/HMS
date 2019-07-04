namespace HMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BasicEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccommodationPackages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NoOfRoom = c.Int(nullable: false),
                        FeePerNight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccommodationTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccommodationTypes", t => t.AccommodationTypeId, cascadeDelete: true)
                .Index(t => t.AccommodationTypeId);
            
            CreateTable(
                "dbo.AccommodationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Accommodations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        AccommodationPackageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccommodationPackages", t => t.AccommodationPackageId, cascadeDelete: true)
                .Index(t => t.AccommodationPackageId);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromDate = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                        AccommodationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accommodations", t => t.AccommodationId, cascadeDelete: true)
                .Index(t => t.AccommodationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "AccommodationId", "dbo.Accommodations");
            DropForeignKey("dbo.Accommodations", "AccommodationPackageId", "dbo.AccommodationPackages");
            DropForeignKey("dbo.AccommodationPackages", "AccommodationTypeId", "dbo.AccommodationTypes");
            DropIndex("dbo.Bookings", new[] { "AccommodationId" });
            DropIndex("dbo.Accommodations", new[] { "AccommodationPackageId" });
            DropIndex("dbo.AccommodationPackages", new[] { "AccommodationTypeId" });
            DropTable("dbo.Bookings");
            DropTable("dbo.Accommodations");
            DropTable("dbo.AccommodationTypes");
            DropTable("dbo.AccommodationPackages");
        }
    }
}
