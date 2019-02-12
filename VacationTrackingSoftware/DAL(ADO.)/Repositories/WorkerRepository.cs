using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.IRepositories;
using BLL.Models;

namespace DAL_ADO._.Repositories
{
    public class WorkerRepository : GenericRepository<Worker>, IWorkerRepository
    {
        public Task CreateWorkerAsync(Worker worker)
        {
            throw new NotImplementedException();
        }

        public Worker GetWithUser(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
