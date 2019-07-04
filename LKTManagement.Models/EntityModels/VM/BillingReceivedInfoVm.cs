using LKTManagement.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LKTManagement.Models.VM
{
    public class BillingReceivedInfoInput
    {
        public Int64 Id { get; set; }
        public Int64 TenantInfoId { get; set; }
        public DateTime Date { get; set; }
        public decimal[] Amount { get; set; }
        public string[] Purpose { get; set; }
        public string[] Note { get; set; }
    }
    [NotMapped]
    public class BillingReceivedInfoList : BillingReceivedInfo
    {
        public string TenantName { get; set; }
        public string BillingDate { get { return Date.ToString("yyyy-MM-dd"); } }
    }
}