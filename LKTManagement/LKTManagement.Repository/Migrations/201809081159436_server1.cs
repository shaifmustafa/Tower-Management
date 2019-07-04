namespace LKTManagement.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class server1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BillingInfoPerMonths", "TenantInfo_Id", "dbo.TenantInfoes");
            DropForeignKey("dbo.BillingReceivedInfoes", "TenantInfo_Id", "dbo.TenantInfoes");
            DropForeignKey("dbo.FloorRentInfoes", "TenantInfo_Id", "dbo.TenantInfoes");
            DropIndex("dbo.BillingInfoPerMonths", new[] { "TenantInfo_Id" });
            DropIndex("dbo.BillingReceivedInfoes", new[] { "TenantInfo_Id" });
            DropIndex("dbo.FloorRentInfoes", new[] { "TenantInfo_Id" });
            DropColumn("dbo.BillingInfoPerMonths", "TenantInfoId");
            DropColumn("dbo.BillingReceivedInfoes", "TenantInfoId");
            DropColumn("dbo.FloorRentInfoes", "TenantInfoId");
            RenameColumn(table: "dbo.BillingInfoPerMonths", name: "TenantInfo_Id", newName: "TenantInfoId");
            RenameColumn(table: "dbo.BillingReceivedInfoes", name: "TenantInfo_Id", newName: "TenantInfoId");
            RenameColumn(table: "dbo.FloorRentInfoes", name: "TenantInfo_Id", newName: "TenantInfoId");
            DropPrimaryKey("dbo.BillingInfoPerMonths");
            DropPrimaryKey("dbo.BillingReceivedInfoes");
            DropPrimaryKey("dbo.FloorRentInfoes");
            AlterColumn("dbo.BillingInfoPerMonths", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.BillingInfoPerMonths", "TenantInfoId", c => c.Long(nullable: false));
            AlterColumn("dbo.BillingInfoPerMonths", "TenantInfoId", c => c.Long(nullable: false));
            AlterColumn("dbo.BillingReceivedInfoes", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.BillingReceivedInfoes", "TenantInfoId", c => c.Long(nullable: false));
            AlterColumn("dbo.BillingReceivedInfoes", "TenantInfoId", c => c.Long(nullable: false));
            AlterColumn("dbo.FloorRentInfoes", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.FloorRentInfoes", "TenantInfoId", c => c.Long(nullable: false));
            AlterColumn("dbo.FloorRentInfoes", "TenantInfoId", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.BillingInfoPerMonths", "Id");
            AddPrimaryKey("dbo.BillingReceivedInfoes", "Id");
            AddPrimaryKey("dbo.FloorRentInfoes", "Id");
            CreateIndex("dbo.BillingInfoPerMonths", "TenantInfoId");
            CreateIndex("dbo.BillingReceivedInfoes", "TenantInfoId");
            CreateIndex("dbo.FloorRentInfoes", "TenantInfoId");
            AddForeignKey("dbo.BillingInfoPerMonths", "TenantInfoId", "dbo.TenantInfoes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BillingReceivedInfoes", "TenantInfoId", "dbo.TenantInfoes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FloorRentInfoes", "TenantInfoId", "dbo.TenantInfoes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FloorRentInfoes", "TenantInfoId", "dbo.TenantInfoes");
            DropForeignKey("dbo.BillingReceivedInfoes", "TenantInfoId", "dbo.TenantInfoes");
            DropForeignKey("dbo.BillingInfoPerMonths", "TenantInfoId", "dbo.TenantInfoes");
            DropIndex("dbo.FloorRentInfoes", new[] { "TenantInfoId" });
            DropIndex("dbo.BillingReceivedInfoes", new[] { "TenantInfoId" });
            DropIndex("dbo.BillingInfoPerMonths", new[] { "TenantInfoId" });
            DropPrimaryKey("dbo.FloorRentInfoes");
            DropPrimaryKey("dbo.BillingReceivedInfoes");
            DropPrimaryKey("dbo.BillingInfoPerMonths");
            AlterColumn("dbo.FloorRentInfoes", "TenantInfoId", c => c.Long());
            AlterColumn("dbo.FloorRentInfoes", "TenantInfoId", c => c.Int(nullable: false));
            AlterColumn("dbo.FloorRentInfoes", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.BillingReceivedInfoes", "TenantInfoId", c => c.Long());
            AlterColumn("dbo.BillingReceivedInfoes", "TenantInfoId", c => c.Int(nullable: false));
            AlterColumn("dbo.BillingReceivedInfoes", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.BillingInfoPerMonths", "TenantInfoId", c => c.Long());
            AlterColumn("dbo.BillingInfoPerMonths", "TenantInfoId", c => c.Int(nullable: false));
            AlterColumn("dbo.BillingInfoPerMonths", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.FloorRentInfoes", "Id");
            AddPrimaryKey("dbo.BillingReceivedInfoes", "Id");
            AddPrimaryKey("dbo.BillingInfoPerMonths", "Id");
            RenameColumn(table: "dbo.FloorRentInfoes", name: "TenantInfoId", newName: "TenantInfo_Id");
            RenameColumn(table: "dbo.BillingReceivedInfoes", name: "TenantInfoId", newName: "TenantInfo_Id");
            RenameColumn(table: "dbo.BillingInfoPerMonths", name: "TenantInfoId", newName: "TenantInfo_Id");
            AddColumn("dbo.FloorRentInfoes", "TenantInfoId", c => c.Int(nullable: false));
            AddColumn("dbo.BillingReceivedInfoes", "TenantInfoId", c => c.Int(nullable: false));
            AddColumn("dbo.BillingInfoPerMonths", "TenantInfoId", c => c.Int(nullable: false));
            CreateIndex("dbo.FloorRentInfoes", "TenantInfo_Id");
            CreateIndex("dbo.BillingReceivedInfoes", "TenantInfo_Id");
            CreateIndex("dbo.BillingInfoPerMonths", "TenantInfo_Id");
            AddForeignKey("dbo.FloorRentInfoes", "TenantInfo_Id", "dbo.TenantInfoes", "Id");
            AddForeignKey("dbo.BillingReceivedInfoes", "TenantInfo_Id", "dbo.TenantInfoes", "Id");
            AddForeignKey("dbo.BillingInfoPerMonths", "TenantInfo_Id", "dbo.TenantInfoes", "Id");
        }
    }
}
