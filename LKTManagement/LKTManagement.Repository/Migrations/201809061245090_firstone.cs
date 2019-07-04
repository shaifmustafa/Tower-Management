namespace LKTManagement.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstone : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BillingInfoPerMonths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantInfoId = c.Int(nullable: false),
                        Month = c.String(),
                        OpeningBalanceRent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentDuesRent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RemarksRent = c.String(),
                        OpeningBalanceCommon = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentDuesCommon = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RemarksCommon = c.String(),
                        OpeningBalanceElectricity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentDuesElectricity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RemarksElectricity = c.String(),
                        OpeningBalanceWasa = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentDuesWasa = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RemarksWasa = c.String(),
                        OpeningBalanceEmElectricity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentDuesEmElectricity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RemarksEmElectricity = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        UpdatedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        TenantInfo_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TenantInfoes", t => t.TenantInfo_Id)
                .Index(t => t.TenantInfo_Id);
            
            CreateTable(
                "dbo.TenantInfoes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Note = c.String(),
                        CommonBillRateSFT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LessAITPercent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        UpdatedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BillingReceivedInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantInfoId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Purpose = c.String(),
                        Note = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        UpdatedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        TenantInfo_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TenantInfoes", t => t.TenantInfo_Id)
                .Index(t => t.TenantInfo_Id);
            
            CreateTable(
                "dbo.FloorRentInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantInfoId = c.Int(nullable: false),
                        EffectiveDate = c.DateTime(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        FloorLevel = c.String(),
                        RentSpaceSFT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SftRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsCurrent = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        UpdatedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        TenantInfo_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TenantInfoes", t => t.TenantInfo_Id)
                .Index(t => t.TenantInfo_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FloorRentInfoes", "TenantInfo_Id", "dbo.TenantInfoes");
            DropForeignKey("dbo.BillingReceivedInfoes", "TenantInfo_Id", "dbo.TenantInfoes");
            DropForeignKey("dbo.BillingInfoPerMonths", "TenantInfo_Id", "dbo.TenantInfoes");
            DropIndex("dbo.FloorRentInfoes", new[] { "TenantInfo_Id" });
            DropIndex("dbo.BillingReceivedInfoes", new[] { "TenantInfo_Id" });
            DropIndex("dbo.BillingInfoPerMonths", new[] { "TenantInfo_Id" });
            DropTable("dbo.FloorRentInfoes");
            DropTable("dbo.BillingReceivedInfoes");
            DropTable("dbo.TenantInfoes");
            DropTable("dbo.BillingInfoPerMonths");
        }
    }
}
