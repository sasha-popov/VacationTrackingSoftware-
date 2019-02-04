using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.IRepositories;
using DAL.Data;

namespace DAL.Repositories
{
    public class AppUserRepository: GenericRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(ProjectContext context) : base(context) { }

    }
}
