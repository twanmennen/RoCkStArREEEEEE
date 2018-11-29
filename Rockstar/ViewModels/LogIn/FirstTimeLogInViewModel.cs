using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rockstar.ViewModels.Register
{
    public class FirstTimeLogInViewModel
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public DateTime Birthdate { get; set; }
        public string Gender { get; set; }

        public FirstTimeLogInViewModel() { }

        public FirstTimeLogInViewModel(int roleId)
        {
            RoleId = roleId;
        }
    }
}
