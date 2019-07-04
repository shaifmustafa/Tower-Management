using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace LKTManagement.Models.EntityModels
{
    public class FloorRentInfo : TrackInfo
    {
        public int Id { get; set; }
        public int TenantInfoId { get; set; }
        public TenantInfo TenantInfo { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public String FloorLevel { get; set; }
        public decimal RentSpaceSFT { get; set; }
        public decimal SftRate { get; set; }
        public bool IsCurrent { get; set; }        
    }
}
