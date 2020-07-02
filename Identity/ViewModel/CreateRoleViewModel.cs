using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.ViewModel
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }
        
       
        [Display(Name = "Role Description")]
        public string Description { get; set; }
    }
}
