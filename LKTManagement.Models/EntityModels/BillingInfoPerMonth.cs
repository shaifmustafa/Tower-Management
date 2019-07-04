using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKTManagement.Models.EntityModels
{
    public class BillingInfoPerMonth : TrackInfo
    {
        public Int64 Id { get; set; }

        public Int64 TenantInfoId { get; set; }//

        public TenantInfo TenantInfo { get; set; }

        public string Month { get; set; }//

        public string Year { get; set; }

        public decimal OpeningBalanceRent { get; set; }//

        public decimal CurrentDuesRent { get; set; }//

        public string RemarksRent { get; set; }

        public decimal OpeningBalanceCommon { get; set; }//

        public decimal CurrentDuesCommon { get; set; }//

        public string RemarksCommon { get; set; }

        public decimal OpeningBalanceElectricity { get; set; }//

        public decimal CurrentDuesElectricity { get; set; }//

        public string RemarksElectricity { get; set; }

        public decimal OpeningBalanceWasa { get; set; }//

        public decimal CurrentDuesWasa { get; set; }//

        public string RemarksWasa { get; set; }

        public decimal OpeningBalanceEmElectricity { get; set; }//

        public decimal CurrentDuesEmElectricity { get; set; }//

        public string RemarksEmElectricity { get; set; }
        public decimal RentBill { get; set; }
        public decimal CommonBill { get; set; }
        public decimal ElectricityBill { get; set; }
        public decimal EmElectricityBill { get; set; }
        public decimal WasaBill { get; set; }

    }
}
