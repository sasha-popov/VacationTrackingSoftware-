using System;
using System.Collections.Generic;
using System.Text;
using BLL.IRepositories;
using BLL.Models;

namespace DAL_ADO._.Repositories
{
    public class VacationTypeRepository : GenericRepository<VacationType>, IVacationTypeRepository
    {
        public VacationType FindByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
