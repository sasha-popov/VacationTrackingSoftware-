using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.IRepositories;
using BLL.Models;

namespace DAL_ADO._.Repositories
{
    public class CompanyHolidayRepository : GenericRepository<CompanyHoliday>, ICompanyHolidayRepository
    {
        public List<CompanyHoliday> FindByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public CompanyHoliday FindByDateAndDescription(string description, DateTime date)
        {
            throw new NotImplementedException();
        }

        public List<CompanyHoliday> GetAllHolidaysForCurrentYear()
        {
            throw new NotImplementedException();
        }
    }
}
