using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LKTManagement.Models.EntityModels.VM
{
    public class CompanyUserVm
    {
        public Int64 Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string MobileNo { get; set; }
        public string LogoName { get; set; }

        public string ProfilePicture { get; set; }        
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
