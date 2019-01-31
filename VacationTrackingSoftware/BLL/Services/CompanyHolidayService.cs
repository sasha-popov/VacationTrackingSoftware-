using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.IRepositories;
using BLL.Models;

namespace BLL.Services
{
    public class CompanyHolidayService: ICompanyHolidayService
    {
        private ICompanyHolidayRepository _companyHolidayRepository;

        public CompanyHolidayService(ICompanyHolidayRepository companyHolidayService) {
            _companyHolidayRepository = companyHolidayService;
        }

        public CompanyHoliday AddHoliday(CompanyHoliday newHoliday)
        {
            CompanyHoliday result;
            var checkDublicate = _companyHolidayRepository.FindByCondition(x => x.Date == newHoliday.Date);
            if (checkDublicate.Count() == 0 && newHoliday.Date.DayOfWeek.ToString() != "Saturday" && newHoliday.Date.DayOfWeek.ToString() != "Sunday")
            {
                _companyHolidayRepository.Create(newHoliday);
                _companyHolidayRepository.Save();
                result = newHoliday;
            }
            else {
                result = null;
            }
            return result;
    }
        public CompanyHoliday UpdateHoliday(CompanyHoliday holiday)
        {
            CompanyHoliday result;
            var checkDublicate = _companyHolidayRepository.FindByCondition(x => x.Date == holiday.Date).ToList();
            checkDublicate = checkDublicate.Where(x => x.Id != holiday.Id).ToList(); ;
            if (checkDublicate.Count() == 0 && holiday.Date.DayOfWeek.ToString() != "Saturday" && holiday.Date.DayOfWeek.ToString() != "Sunday")
            {
                _companyHolidayRepository.Update(holiday);
                _companyHolidayRepository.Save();
                result = holiday;
            }
            else
            {
                result = null;
            }
            return result;
        }
    }
}
