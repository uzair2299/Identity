using System;
using System.Collections.Generic;
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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<UserRoleViewModel> userRoleViewModel { get; set; }
    }
}
