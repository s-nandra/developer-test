namespace OrangeBricks.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddViewingAppointmentModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ViewingAppointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PropertyId = c.Int(nullable: false),
                        ViewingAppointmentDateTime = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        BuyerId = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Properties", t => t.PropertyId, cascadeDelete: true)
                .Index(t => t.PropertyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ViewingAppointments", "PropertyId", "dbo.Properties");
            DropIndex("dbo.ViewingAppointments", new[] { "PropertyId" });
            DropTable("dbo.ViewingAppointments");
        }
    }
}
