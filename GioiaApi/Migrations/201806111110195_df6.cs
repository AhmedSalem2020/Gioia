namespace GioiaApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class df6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromId = c.String(nullable: false),
                        ToId = c.String(nullable: false),
                        IsSeen = c.Boolean(nullable: false),
                        MsgDate = c.DateTime(nullable: false),
                        MsgTime = c.Time(nullable: false, precision: 7),
                        IsDeleted = c.Boolean(nullable: false),
                        MessageText = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserConnections",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ConnectionId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Id, t.ConnectionId })
                .ForeignKey("dbo.AspNetUsers", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.MessageGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromId = c.String(nullable: false),
                        ToIdGroup = c.Int(nullable: false),
                        IsSeen = c.Boolean(nullable: false),
                        MsgDate = c.DateTime(nullable: false),
                        MsgTime = c.Time(nullable: false, precision: 7),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Group_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Group_Id");
            AddForeignKey("dbo.AspNetUsers", "Group_Id", "dbo.Groups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserConnections", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Group_Id", "dbo.Groups");
            DropIndex("dbo.UserConnections", new[] { "Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Group_Id" });
            DropColumn("dbo.AspNetUsers", "Group_Id");
            DropTable("dbo.MessageGroups");
            DropTable("dbo.UserConnections");
            DropTable("dbo.Messages");
            DropTable("dbo.Groups");
        }
    }
}
