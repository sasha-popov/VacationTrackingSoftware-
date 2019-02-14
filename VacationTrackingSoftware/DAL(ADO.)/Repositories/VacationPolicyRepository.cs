using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.IRepositories;
using BLL.Models;

namespace DAL_ADO._.Repositories
{
    public class VacationPolicyRepository : IVacationPolicyRepository
    {
        public void Create(VacationPolicy entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(VacationPolicy entity)
        {
            throw new NotImplementedException();
        }

        public List<VacationPolicy> FindCurrentVacationPolicy(UserVacationRequest userVacationRequest)
        {
            throw new NotImplementedException();
        }

        public VacationPolicy FindForDelete(int years, string vacationType, int payments)
        {
            throw new NotImplementedException();
        }

        public List<VacationPolicy> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<VacationPolicy> GetAllVacationPoliciesWithTypes()
        {
            throw new NotImplementedException();
        }

        public VacationPolicy GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(VacationPolicy entity)
        {
            throw new NotImplementedException();
        }
    }
}
