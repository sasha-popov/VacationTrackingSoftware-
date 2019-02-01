using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.IRepositories;
using BLL.Models;
using DAL.Data;

namespace DAL.Repositories
{
    public class VacationTypeRepository : GenericRepository<VacationType>, IVacationTypeRepository
    {
        public VacationTypeRepository(ProjectContext context) : base(context) { }

        public VacationType FindByName(string name)
        {
            return RepositoryContext.VacationTypes.Where(x => x.Name == name).First();
        }
    }
}