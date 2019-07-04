using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKTManagement.Models.EntityModels.VM
{
    [NotMapped]
    public class TenantInfoVm : FloorRentInfo
    {
        public Int64 TenantId { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public Int64 FloorInfoId { get; set; }  
        public Int64 FloorInfoLevel { get; set; }
        public string EffectiveDateText { get { return EffectiveDate.ToString("yyyy-MM-dd"); } }
        public string ExpiryDateText { get { return ExpiryDate.ToString("yyyy-MM-dd"); } }


        //public decimal CommonBillRateSFT { get; set; }

        //public decimal LessAITPercent { get; set; }

        //public decimal AdvancePay { get; set; }

        //public decimal AdvanceAdjustment { get; set; }

        //public String FloorLevel { get; set; }
        //public decimal RentSpaceSFT { get; set; }
        //public decimal SftRate{ get; set; }

        //public DateTime EffectiveDate{ get; set; }
        //public DateTime ExpiryDate { get; set; }
    }
}
