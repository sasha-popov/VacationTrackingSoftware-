using System;
using System.Collections.Generic;
using System.Text;
using BLL.IRepositories;
using BLL.Models;

namespace DAL_ADO._.Repositories
{
    public class VacationPolicyRepository : GenericRepository<VacationPolicy>, IVacationPolicyRepository
    {
        public List<VacationPolicy> FindCurrentVacationPolicy(UserVacationRequest userVacationRequest)
        {
            throw new NotImplementedException();
        }

        public VacationPolicy FindForDelete(int years, string vacationType, int payments)
        {
            throw new NotImplementedException();
        }

        public List<VacationPolicy> GetAllVacationPoliciesWithTypes()
        {
            throw new NotImplementedException();
        }
    }
}
