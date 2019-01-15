//using BLL.IRepositories;
//using BLL.Models;
//using DAL.Data;
//using System.Linq;
//using Microsoft.EntityFrameworkCore;

//namespace DAL.Repositories
//{
//    public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
//    {
//        public UserRoleRepository(ProjectContext context) : base(context) { }

//        public UserRole GetWithAllObjects(int id)
//        {
//            return RepositoryContext.UserRoles.Include(x => x.Role).Include(x=>x.User).Where(x=>x.User.Id==id).FirstOrDefault();
//        }
//    }
//}
