namespace GioiaApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class df11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Friends", "SenderId", "dbo.AspNetUsers");
            DropPrimaryKey("dbo.Friends");
            AddColumn("dbo.Friends", "ReceiverId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Friends", new[] { "SenderId", "ReceiverId" });
            CreateIndex("dbo.Friends", "ReceiverId");
            AddForeignKey("dbo.Friends", "ReceiverId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Friends", "SenderId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friends", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "ReceiverId", "dbo.AspNetUsers");
            DropIndex("dbo.Friends", new[] { "ReceiverId" });
            DropPrimaryKey("dbo.Friends");
            DropColumn("dbo.Friends", "ReceiverId");
            AddPrimaryKey("dbo.Friends", "SenderId");
            AddForeignKey("dbo.Friends", "SenderId", "dbo.AspNetUsers", "Id");
        }
    }
}
