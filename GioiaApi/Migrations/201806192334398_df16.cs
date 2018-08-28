namespace GioiaApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class df16 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostComments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        comment = c.String(),
                        postId = c.Int(nullable: false),
                        userId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.userId)
                .ForeignKey("dbo.Posts", t => t.postId, cascadeDelete: true)
                .Index(t => t.postId)
                .Index(t => t.userId);
            
            DropColumn("dbo.PostLikes", "comment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PostLikes", "comment", c => c.String());
            DropForeignKey("dbo.PostComments", "postId", "dbo.Posts");
            DropForeignKey("dbo.PostComments", "userId", "dbo.AspNetUsers");
            DropIndex("dbo.PostComments", new[] { "userId" });
            DropIndex("dbo.PostComments", new[] { "postId" });
            DropTable("dbo.PostComments");
        }
    }
}
