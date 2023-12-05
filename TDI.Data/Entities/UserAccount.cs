using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDI.Data.Entities
{
    public class UserAccountModel: UserModel
    {
        public string UserName { get; set; }
        public string UserType { get; set; }
        public string PassWord { get; set; }
        public string Role { get; set; }
        //public string NormalizedUserName { get; set; }
        //public string Email { get; set; }
        //public string NormalizedEmail { get; set; }
        //public bool EmailConfirmed { get; set; }

    }
}
