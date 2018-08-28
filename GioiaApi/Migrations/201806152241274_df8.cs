namespace GioiaApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class df8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Friends", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Friends", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Friends", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Friends", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Friends", "ApplicationUser_Id");
            AddForeignKey("dbo.Friends", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
