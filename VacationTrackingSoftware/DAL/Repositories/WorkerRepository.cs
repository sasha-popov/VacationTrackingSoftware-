using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.IRepositories;
using BLL.Models;
using DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class WorkerRepository : GenericRepository<Worker>, IWorkerRepository
    {
        public WorkerRepository(ProjectContext context) : base(context) { }
        public async Task CreateWorkerAsync(Worker worker)
        {
            await RepositoryContext.AddAsync(worker);
        }

        public Worker GetWithUser(string userId)
        {
            return RepositoryContext.Workers.Include(x => x.User).Where(x => x.User.Id == userId).FirstOrDefault();
        }
    }
}
