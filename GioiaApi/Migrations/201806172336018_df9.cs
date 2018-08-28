namespace GioiaApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class df9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Friends", "RequestedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "RequestedTo_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Friends", new[] { "RequestedBy_Id" });
            DropIndex("dbo.Friends", new[] { "RequestedTo_Id" });
            DropTable("dbo.Friends");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        SenderId = c.String(nullable: false, maxLength: 128),
                        ReceiverId = c.String(nullable: false, maxLength: 128),
                        state = c.String(),
                        RequestedBy_Id = c.String(maxLength: 128),
                        RequestedTo_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.SenderId, t.ReceiverId });
            
            CreateIndex("dbo.Friends", "RequestedTo_Id");
            CreateIndex("dbo.Friends", "RequestedBy_Id");
            AddForeignKey("dbo.Friends", "RequestedTo_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Friends", "RequestedBy_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
