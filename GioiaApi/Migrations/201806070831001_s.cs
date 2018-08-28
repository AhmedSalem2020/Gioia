namespace GioiaApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class s : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sounds",
                c => new
                    {
                        MusicId = c.Int(nullable: false, identity: true),
                        moodId = c.Int(nullable: false),
                        MusicLink = c.String(),
                    })
                .PrimaryKey(t => t.MusicId)
                .ForeignKey("dbo.Moods", t => t.moodId, cascadeDelete: true)
                .Index(t => t.moodId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sounds", "moodId", "dbo.Moods");
            DropIndex("dbo.Sounds", new[] { "moodId" });
            DropTable("dbo.Sounds");
        }
    }
}
