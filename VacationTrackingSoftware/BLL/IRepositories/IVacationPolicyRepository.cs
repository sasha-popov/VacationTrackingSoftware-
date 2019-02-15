using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;

namespace BLL.IRepositories
{
    public interface IVacationPolicyRepository : IGenericRepository<VacationPolicy>
    {
        List<VacationPolicy> GetAllVacationPoliciesWithTypes();
        List<VacationPolicy> FindCurrentVacationPolicy(UserVacationRequest userVacationRequest);

    }
}
