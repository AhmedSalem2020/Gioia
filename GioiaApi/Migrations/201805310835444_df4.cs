namespace GioiaApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class df4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "time", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "time", c => c.String());
        }
    }
}
