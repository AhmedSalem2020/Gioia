namespace GioiaApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class df13 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostLikes",
                c => new
                    {
                        postId = c.Int(nullable: false),
                        userId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.postId, t.userId })
                .ForeignKey("dbo.AspNetUsers", t => t.userId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.postId, cascadeDelete: true)
                .Index(t => t.postId)
                .Index(t => t.userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostLikes", "postId", "dbo.Posts");
            DropForeignKey("dbo.PostLikes", "userId", "dbo.AspNetUsers");
            DropIndex("dbo.PostLikes", new[] { "userId" });
            DropIndex("dbo.PostLikes", new[] { "postId" });
            DropTable("dbo.PostLikes");
        }
    }
}
