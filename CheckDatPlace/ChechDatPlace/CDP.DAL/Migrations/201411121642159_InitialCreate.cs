namespace CDP.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Place",
                c => new
                    {
                        PlaceID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                        Address_Number = c.Int(),
                        Address_StreetName = c.String(),
                        Address_CityName = c.String(),
                        Address_CodePostal = c.Int(nullable: false),
                        Rate = c.Int(),
                    })
                .PrimaryKey(t => t.PlaceID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstName = c.String(),
                        Password = c.String(),
                        Login = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User");
            DropTable("dbo.Place");
        }
    }
}
