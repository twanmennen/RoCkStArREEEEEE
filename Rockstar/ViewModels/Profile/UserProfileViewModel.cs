using System;
using System.Collections.Generic;

namespace Rockstar.ViewModels.Account
{
    public class UserProfileViewModel
    {	
		
        public Models.Account Account { get; set; }
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }
        public string PassWord { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public DateTime Birthdate { get; set; }
		public List<Models.Review> Reviews { get; set; }
        public string Image { get; set; }

    }
}
