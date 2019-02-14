using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL_ADO._.Data
{
   public class AppUserContext: IdentityDbContext<AppUser>
    {
        public AppUserContext(DbContextOptions options) : base(options) { }
    }
}
