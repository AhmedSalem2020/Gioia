namespace GioiaApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class df14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostLikes", "comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostLikes", "comment");
        }
    }
}
