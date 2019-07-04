using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace LKTManagement.Models.EntityModels
{
    public class FloorRentInfo : TrackInfo
    {
        public Int64 Id { get; set; }

        public Int64 TenantInfoId { get; set; }

        public TenantInfo TenantInfo { get; set; }
        
        public decimal CommonBillRateSFT { get; set; }

        public decimal LessAITPercent { get; set; }

        public decimal AdvancePay { get; set; }

        public decimal AdvanceAdjustment { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public Int64 FloorLevel { get; set; }

        public decimal RentSpaceSFT { get; set; }

        public decimal SftRate { get; set; }
        public decimal RentAIT { get; set; }
        public decimal CommonAIT { get; set; }

        public bool IsCurrent { get; set; }
    }
}
