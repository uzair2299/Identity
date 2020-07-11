﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.ViewModel
{
    public class UserViewModel
    {
      public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public  string Email { get; set; }
        public string ProfilePicture { get; set; }
        public IList<string> RoleNames { get; set; }
    }
}
