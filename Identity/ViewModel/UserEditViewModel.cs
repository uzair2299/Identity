using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.ViewModel
{
    public class UserEditViewModel
    {
       public UserEditViewModel()
        {
            userRoleViewModel = new List<UserRoleViewModel>();
        }
        public string Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfilePicture { get; set; }
        public List<UserRoleViewModel> userRoleViewModel { get; set; }
    }
}
