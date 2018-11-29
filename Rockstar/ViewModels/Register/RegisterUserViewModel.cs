using System.ComponentModel.DataAnnotations;

namespace Rockstar.ViewModels.Account
{
    public class RegisterUserViewModel
    {
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])", ErrorMessage = "this is not a valid email address")]
        [Required(ErrorMessage = "this field is required")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "this field is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "this field is required")]
        public string PassWord { get; set; }
      
        public int Role { get; set; }
        public int RoleId { get; set; }
        public RegisterUserViewModel() { }
        public RegisterUserViewModel(int roleId)
        {
            RoleId = roleId;
        }
    }
}
