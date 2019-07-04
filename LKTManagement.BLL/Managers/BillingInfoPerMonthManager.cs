using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using LKTManagement.BLL.Base;
using LKTManagement.Models.EntityModels;
using LKTManagement.Repository.Base;
using LKTManagement.Repository.Repositories;

namespace LKTManagement.BLL.Managers
{
    public class BillingInfoPerMonthManager : Manager<BillingInfoPerMonth>
    {
        LoginInfo Log = new LoginInfo();
        public BillingInfoPerMonthManager() : base(new BillingInfoPerMonthRepository())
        {
        }
        public bool Saved(Int64 UpdatedId, decimal[] Amount, string[] Note)
        {
            var exist = Repository.GetById(UpdatedId);
            if (exist == null) return false;
            exist.UpdatedBy = Log.UserId.ToString();
            exist.UpdatedOn = new LoginInfo().CurrentTime;
            exist.CurrentDuesElectricity = Amount[2];
            exist.CurrentDuesWasa = Amount[3];
            exist.CurrentDuesEmElectricity = Amount[4];
            exist.RemarksRent = Note[0];
            exist.RemarksCommon = Note[1];
            exist.RemarksElectricity = Note[2];
            exist.RemarksWasa = Note[3];
            exist.RemarksEmElectricity = Note[4];
            Repository.SaveOrUpdate(exist);
            return Repository.Done();
        }
    }
}
