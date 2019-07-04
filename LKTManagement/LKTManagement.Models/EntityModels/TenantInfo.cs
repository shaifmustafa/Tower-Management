using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKTManagement.Models.EntityModels
{
    public class TenantInfo : TrackInfo
    {
        public Int64 Id { get; set; }
        public String Name { get; set; }
        public String Note { get; set; }
        public decimal CommonBillRateSFT { get; set; }
        public decimal LessAITPercent { get; set; }
        public virtual List<FloorRentInfo> FloorRentInfos { get; set; }
        public virtual List<BillingInfoPerMonth> BillingInfoPerMonths { get; set; }
        public virtual List<BillingReceivedInfo> BillingReceivedInfos { get; set; } 
    }
}
