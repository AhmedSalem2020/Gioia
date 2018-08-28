namespace GioiaApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class df12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Friends", "state", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Friends", "state");
        }
    }
}
