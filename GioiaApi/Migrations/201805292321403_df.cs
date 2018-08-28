namespace GioiaApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class df : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Memos",
                c => new
                    {
                        memoId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        title = c.String(),
                        userId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.memoId)
                .ForeignKey("dbo.AspNetUsers", t => t.userId)
                .Index(t => t.userId);
            
            CreateTable(
                "dbo.Musics",
                c => new
                    {
                        moodId = c.Int(nullable: false),
                        MusicId = c.Int(nullable: false),
                        MusicName = c.String(),
                        MusicLink = c.String(),
                        MusicRate = c.String(),
                    })
                .PrimaryKey(t => new { t.moodId, t.MusicId })
                .ForeignKey("dbo.Moods", t => t.moodId, cascadeDelete: true)
                .Index(t => t.moodId);
            
            CreateTable(
                "dbo.Moods",
                c => new
                    {
                        MoodId = c.Int(nullable: false, identity: true),
                        moodName = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.MoodId);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        photoId = c.Int(nullable: false, identity: true),
                        photo = c.String(),
                        userId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.photoId)
                .ForeignKey("dbo.AspNetUsers", t => t.userId)
                .Index(t => t.userId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        postId = c.Int(nullable: false, identity: true),
                        post = c.String(),
                        time = c.String(),
                        moodId = c.Int(nullable: false),
                        userId = c.String(maxLength: 128),
                        photoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.postId)
                .ForeignKey("dbo.AspNetUsers", t => t.userId)
                .ForeignKey("dbo.Moods", t => t.moodId, cascadeDelete: true)
                .ForeignKey("dbo.Photos", t => t.photoId, cascadeDelete: true)
                .Index(t => t.moodId)
                .Index(t => t.userId)
                .Index(t => t.photoId);
            
            CreateTable(
                "dbo.userMoods",
                c => new
                    {
                        moodId = c.Int(nullable: false),
                        userId = c.String(nullable: false, maxLength: 128),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.moodId, t.userId, t.Date })
                .ForeignKey("dbo.AspNetUsers", t => t.userId, cascadeDelete: true)
                .ForeignKey("dbo.Moods", t => t.moodId, cascadeDelete: true)
                .Index(t => t.moodId)
                .Index(t => t.userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.userMoods", "moodId", "dbo.Moods");
            DropForeignKey("dbo.userMoods", "userId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "photoId", "dbo.Photos");
            DropForeignKey("dbo.Posts", "moodId", "dbo.Moods");
            DropForeignKey("dbo.Posts", "userId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Photos", "userId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Musics", "moodId", "dbo.Moods");
            DropForeignKey("dbo.Memos", "userId", "dbo.AspNetUsers");
            DropIndex("dbo.userMoods", new[] { "userId" });
            DropIndex("dbo.userMoods", new[] { "moodId" });
            DropIndex("dbo.Posts", new[] { "photoId" });
            DropIndex("dbo.Posts", new[] { "userId" });
            DropIndex("dbo.Posts", new[] { "moodId" });
            DropIndex("dbo.Photos", new[] { "userId" });
            DropIndex("dbo.Musics", new[] { "moodId" });
            DropIndex("dbo.Memos", new[] { "userId" });
            DropTable("dbo.userMoods");
            DropTable("dbo.Posts");
            DropTable("dbo.Photos");
            DropTable("dbo.Moods");
            DropTable("dbo.Musics");
            DropTable("dbo.Memos");
        }
    }
}
