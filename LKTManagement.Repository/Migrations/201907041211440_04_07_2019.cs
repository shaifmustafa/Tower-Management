namespace LKTManagement.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _04_07_2019 : DbMigration
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
            
            CreateTable(
                "dbo.BillingInfoPerMonths",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantInfoId = c.Long(nullable: false),
                        Month = c.String(),
                        Year = c.String(),
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
                        RentBill = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CommonBill = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ElectricityBill = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EmElectricityBill = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WasaBill = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        UpdatedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TenantInfoes", t => t.TenantInfoId, cascadeDelete: true)
                .Index(t => t.TenantInfoId);
            
            CreateTable(
                "dbo.TenantInfoes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Note = c.String(),
                        LessAITPercent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AdvancePay = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RentAIT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CommonAIT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsCurrent = c.Boolean(),
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
                        Id = c.Long(nullable: false, identity: true),
                        TenantInfoId = c.Long(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Purpose = c.String(),
                        Note = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        UpdatedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TenantInfoes", t => t.TenantInfoId, cascadeDelete: true)
                .Index(t => t.TenantInfoId);
            
            CreateTable(
                "dbo.FloorRentInfoes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantInfoId = c.Long(nullable: false),
                        CommonBillRateSFT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LessAITPercent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AdvancePay = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AdvanceAdjustment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EffectiveDate = c.DateTime(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        FloorLevel = c.Long(nullable: false),
                        RentSpaceSFT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SftRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RentAIT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CommonAIT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsCurrent = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        UpdatedBy = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TenantInfoes", t => t.TenantInfoId, cascadeDelete: true)
                .Index(t => t.TenantInfoId);
            
            CreateTable(
                "dbo.CompanyUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        MobileNo = c.String(),
                        LogoName = c.String(),
                        ProfilePicture = c.String(),
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
            DropForeignKey("dbo.FloorRentInfoes", "TenantInfoId", "dbo.TenantInfoes");
            DropForeignKey("dbo.BillingReceivedInfoes", "TenantInfoId", "dbo.TenantInfoes");
            DropForeignKey("dbo.BillingInfoPerMonths", "TenantInfoId", "dbo.TenantInfoes");
            DropIndex("dbo.FloorRentInfoes", new[] { "TenantInfoId" });
            DropIndex("dbo.BillingReceivedInfoes", new[] { "TenantInfoId" });
            DropIndex("dbo.BillingInfoPerMonths", new[] { "TenantInfoId" });
            DropTable("dbo.CompanyUsers");
            DropTable("dbo.FloorRentInfoes");
            DropTable("dbo.BillingReceivedInfoes");
            DropTable("dbo.TenantInfoes");
            DropTable("dbo.BillingInfoPerMonths");
            DropTable("dbo.ApplicationUsers");
        }
    }
}
