namespace GioiaApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class df7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        SenderId = c.String(nullable: false, maxLength: 128),
                        ReceiverId = c.String(nullable: false, maxLength: 128),
                        state = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        RequestedBy_Id = c.String(maxLength: 128),
                        RequestedTo_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.SenderId, t.ReceiverId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.RequestedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.RequestedTo_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.RequestedBy_Id)
                .Index(t => t.RequestedTo_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friends", "RequestedTo_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "RequestedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Friends", new[] { "RequestedTo_Id" });
            DropIndex("dbo.Friends", new[] { "RequestedBy_Id" });
            DropIndex("dbo.Friends", new[] { "ApplicationUser_Id" });
            DropTable("dbo.Friends");
        }
    }
}
