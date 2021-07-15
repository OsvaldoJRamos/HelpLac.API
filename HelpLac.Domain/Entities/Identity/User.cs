using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace HelpLac.Domain.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public List<UserRole> UserRoles { get; set; }
    }

    public class UserRole : IdentityUserRole<int>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }

    public class Role : IdentityRole<int>
    {
        public List<UserRole> UserRoles { get; set; }
    }
}
