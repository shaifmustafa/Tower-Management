namespace LKTManagement.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        UserName = c.String(),
                        PhoneNumber = c.String(maxLength: 13),
                        Password = c.String(maxLength: 32),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        UpdatedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ApplicationUsers");
        }
    }
}
