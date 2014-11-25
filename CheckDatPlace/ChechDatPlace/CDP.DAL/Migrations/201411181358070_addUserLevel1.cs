namespace CDP.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserLevel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Level", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Level");
        }
    }
}
