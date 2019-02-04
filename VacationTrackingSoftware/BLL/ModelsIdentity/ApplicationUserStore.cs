using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BLL.ModelsIdentity
{
    public class ApplicationUserStore : UserStore<AppUser>
    {
        public ApplicationUserStore(DbContext context)
            : base(context)
        {
        }

        //public override System.Threading.Tasks.Task<AppUser> GetUsersInRoleAsync(string roleName)
        //{

        //    return Users.Include(u => u.Teams);
        //}
    }
}
