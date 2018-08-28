namespace GioiaApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class df15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostLikes", "like", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostLikes", "like");
        }
    }
}
