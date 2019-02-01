using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;

namespace BLL.IRepositories
{
    public interface ICompanyHolidayRepository: IGenericRepository<CompanyHoliday>
    {
        IEnumerable<CompanyHoliday> GetAllHolidaysForCurrentYear();
        CompanyHoliday FindByDateAndDescription(string description, DateTime date);
        IEnumerable<CompanyHoliday> FindByDate(DateTime date);
    }
}
