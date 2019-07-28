namespace HMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPicturesTableWithRelationToAccPacAndAccTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccommodationPackagePictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccommodationPackageId = c.Int(nullable: false),
                        PictureId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pictures", t => t.PictureId, cascadeDelete: true)
                .ForeignKey("dbo.AccommodationPackages", t => t.AccommodationPackageId, cascadeDelete: true)
                .Index(t => t.AccommodationPackageId)
                .Index(t => t.PictureId);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        URL = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AccommodationPictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccommodationId = c.Int(nullable: false),
                        PictureId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pictures", t => t.PictureId, cascadeDelete: true)
                .ForeignKey("dbo.Accommodations", t => t.AccommodationId, cascadeDelete: true)
                .Index(t => t.AccommodationId)
                .Index(t => t.PictureId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccommodationPictures", "AccommodationId", "dbo.Accommodations");
            DropForeignKey("dbo.AccommodationPictures", "PictureId", "dbo.Pictures");
            DropForeignKey("dbo.AccommodationPackagePictures", "AccommodationPackageId", "dbo.AccommodationPackages");
            DropForeignKey("dbo.AccommodationPackagePictures", "PictureId", "dbo.Pictures");
            DropIndex("dbo.AccommodationPictures", new[] { "PictureId" });
            DropIndex("dbo.AccommodationPictures", new[] { "AccommodationId" });
            DropIndex("dbo.AccommodationPackagePictures", new[] { "PictureId" });
            DropIndex("dbo.AccommodationPackagePictures", new[] { "AccommodationPackageId" });
            DropTable("dbo.AccommodationPictures");
            DropTable("dbo.Pictures");
            DropTable("dbo.AccommodationPackagePictures");
        }
    }
}
