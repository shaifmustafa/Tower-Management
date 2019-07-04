using LKTManagement.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Web;

namespace LKTManagement.Models.VM
{
    public class FloorBasic
    {
        public Int64 FloorLevel { get; set; }
        public decimal FloorSpace { get; set; }
        public decimal RentPerSft { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string EffectiveDateText { get { return EffectiveDate.ToString("yyyy-MM-dd"); } }
        public string ExpiryDateText { get { return ExpiryDate.ToString("yyyy-MM-dd"); } }
        public decimal Total { get { return FloorSpace * RentPerSft; } }

        public decimal CommonBillRateSFT { get; set; }

        public decimal LessAITPercent { get; set; }

        public decimal AdvancePay { get; set; }

        public decimal AdvanceAdjustment { get; set; }
    }
    public class BillingReceivedPerMonth
    { 
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Purpose { get; set; }
    }
    public class MonthlyBillingReceivedInfoReportVm
    {
        public Int64 TenantId { get; set; }
        public string TenantName { get; set; }
        public int Month { get; set; }
        public string MonthName { get { return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month); } }
        public int Year { get; set; }

        public decimal RentBill { get; set; }
        public decimal CommonBill { get; set; }
        public decimal ElectricityBill { get; set; }
        public decimal EmElectricityBill { get; set; }
        public decimal WasaBill { get; set; }
        public Int64 MonthlyBillingId { get; set; }

        public List<FloorBasic> FloorBasics { get; set; }

        public List<BillingReceivedPerMonth> ReceivedBillsPerMonth{ get; set; }

        public decimal MonthlyRentReceivable { get { return FloorBasics.Sum(x => x.Total); } }
        public decimal LessAITPercent { get; set; }
        public decimal AdvanceAdjustment { get; set; }
        public decimal LessAITAmount { get { return (MonthlyRentReceivable * LessAITPercent) / 100; } }
        public decimal NetMonthlyRentReceivable { get { return MonthlyRentReceivable - LessAITAmount - AdvanceAdjustment * FloorBasics.Count; } }

        public decimal RentAIT { get; set; }
        public decimal CommonAIT { get; set; }
        public decimal OpeningBalanceRent { get; set; }
        public decimal CurrentDuesRent { get; set; }
        public decimal ReceiveableRent { get { return OpeningBalanceRent + CurrentDuesRent; } }
        public decimal ReceivedRent { get { return ReceivedBillsPerMonth.Where(w => w.Purpose == "Rent").Sum(x => x.Amount); } }
        public string RemarksRent { get; set; }
        public decimal EndingRent { get { return ReceiveableRent - RentBill; } }

        public decimal OpeningBalanceCommon { get; set; }
        public decimal CurrentDuesCommon { get; set; }
        public decimal ReceiveableCommon { get { return OpeningBalanceCommon + CurrentDuesCommon; } }
        public decimal ReceivedCommon { get { return ReceivedBillsPerMonth.Where(w => w.Purpose == "Common Bill").Sum(x => x.Amount); } }
        public string RemarksCommon { get; set; }
        public decimal EndingCommon { get { return ReceiveableCommon - CommonBill; } }

        public decimal OpeningBalanceElectricity { get; set; }
        public decimal CurrentDuesElectricity { get; set; }
        public decimal ReceiveableElectricity { get { return OpeningBalanceElectricity + CurrentDuesElectricity; } }
        public decimal ReceivedElectricity { get { return ReceivedBillsPerMonth.Where(w => w.Purpose == "Electricity Bill").Sum(x => x.Amount); } }
        public string RemarksElectricity { get; set; }
        public decimal EndingElectricity { get { return ReceiveableElectricity - ElectricityBill; } }

        public decimal OpeningBalanceWasa { get; set; }
        public decimal CurrentDuesWasa { get; set; }
        public decimal ReceiveableWasa { get { return OpeningBalanceWasa + CurrentDuesWasa; } }
        public decimal ReceivedWasa { get { return ReceivedBillsPerMonth.Where(w => w.Purpose == "WASA Bill").Sum(x => x.Amount); } }
        public string RemarksWasa { get; set; }
        public decimal EndingWasa { get { return ReceiveableWasa - WasaBill; } }

        public decimal OpeningBalanceEmElectricity { get; set; }
        public decimal CurrentDuesEmElectricity { get; set; }
        public decimal ReceiveableEmElectricity { get { return OpeningBalanceEmElectricity + CurrentDuesEmElectricity; } }
        public decimal ReceivedEmElectricity { get { return ReceivedBillsPerMonth.Where(w => w.Purpose == "Emergency Electricity Bill").Sum(x => x.Amount); } }
        public string RemarksEmElectricity { get; set; }
        public decimal EndingEmElectricity { get { return ReceiveableEmElectricity - EmElectricityBill; } }

        public decimal TotalOpening { get { return OpeningBalanceRent + OpeningBalanceCommon + OpeningBalanceElectricity + OpeningBalanceWasa + OpeningBalanceEmElectricity; } }
        public decimal TotalCurrentDues { get { return CurrentDuesRent + CurrentDuesCommon + CurrentDuesElectricity + CurrentDuesWasa + CurrentDuesEmElectricity; } }
        public decimal TotalBillReceivable { get { return ReceiveableRent + ReceiveableCommon + ReceiveableElectricity + ReceiveableWasa + ReceivedEmElectricity; } }
        public decimal TotalReceived { get { return ReceivedRent + ReceivedCommon + ReceivedElectricity + ReceivedWasa + ReceivedEmElectricity; } }
        public decimal TotalEnding { get { return EndingRent + EndingCommon + EndingElectricity + EndingWasa + EndingEmElectricity; } }

    }
}