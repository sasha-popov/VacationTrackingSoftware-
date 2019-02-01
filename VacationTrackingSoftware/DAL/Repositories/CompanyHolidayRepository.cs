using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.IRepositories;
using BLL.Models;
using DAL.Data;

namespace DAL.Repositories
{
    public class CompanyHolidayRepository: GenericRepository<CompanyHoliday>, ICompanyHolidayRepository
    {
        public CompanyHolidayRepository(ProjectContext context) : base(context) { }

        public IEnumerable<CompanyHoliday> FindByDate(DateTime date)
        {
            return RepositoryContext.CompanyHolidays.Where(x => x.Date==date);
        }

        public CompanyHoliday FindByDateAndDescription(string description, DateTime date)
        {
            return RepositoryContext.CompanyHolidays.Where(x => x.Description == description && x.Date == date).FirstOrDefault();
        }

        public IEnumerable<CompanyHoliday> GetAllHolidaysForCurrentYear()
        {
            return RepositoryContext.CompanyHolidays.Where(x => x.Date.Year == DateTime.Now.Year);
        }
    }
}
