namespace GioiaApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class df10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        SenderId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.SenderId)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId)
                .Index(t => t.SenderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friends", "SenderId", "dbo.AspNetUsers");
            DropIndex("dbo.Friends", new[] { "SenderId" });
            DropTable("dbo.Friends");
        }
    }
}
