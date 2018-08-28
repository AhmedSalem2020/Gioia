namespace GioiaApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class df3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "photoId", "dbo.Photos");
            DropIndex("dbo.Posts", new[] { "photoId" });
            AlterColumn("dbo.Posts", "photoId", c => c.Int());
            CreateIndex("dbo.Posts", "photoId");
            AddForeignKey("dbo.Posts", "photoId", "dbo.Photos", "photoId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "photoId", "dbo.Photos");
            DropIndex("dbo.Posts", new[] { "photoId" });
            AlterColumn("dbo.Posts", "photoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "photoId");
            AddForeignKey("dbo.Posts", "photoId", "dbo.Photos", "photoId", cascadeDelete: true);
        }
    }
}
