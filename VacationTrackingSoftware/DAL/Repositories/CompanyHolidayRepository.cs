using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.IRepositories;
using BLL.Models;
using DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CompanyHolidayRepository: GenericRepository<CompanyHoliday>, ICompanyHolidayRepository
    {
        public CompanyHolidayRepository(ProjectContext context) : base(context) { }

        public List<CompanyHoliday> FindByDate(DateTime date)
        {
            return RepositoryContext.CompanyHolidays.AsNoTracking().Where(x => x.Date==date).ToList();
        }

        public List<CompanyHoliday> GetAllHolidaysForCurrentYear()
        {
            return RepositoryContext.CompanyHolidays.AsNoTracking().Where(x => x.Date.Year == DateTime.Now.Year).ToList();
        }
    }
}
