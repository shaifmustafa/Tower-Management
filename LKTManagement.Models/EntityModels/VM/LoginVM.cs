using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKTManagement.Models.EntityModels.VM
{
    public class LoginVM
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string ReturnURL { get; set; }

        public bool isRemember { get; set; }
    }
}
