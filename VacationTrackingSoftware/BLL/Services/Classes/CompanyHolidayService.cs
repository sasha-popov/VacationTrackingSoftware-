using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.IRepositories;
using BLL.Models;
using BLL.Result;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace BLL.Services
{
    public class CompanyHolidayService : ICompanyHolidayService
    {
        private ICompanyHolidayRepository _companyHolidayRepository;

        public CompanyHolidayService(ICompanyHolidayRepository companyHolidayService)
        {
            _companyHolidayRepository = companyHolidayService;
        }

        private static Logger logger = LogManager.GetCurrentClassLogger();
        public ResponseForRequest AddOrUpdateHoliday(CompanyHoliday holiday, int status)
        {
            var checkDublicate = _companyHolidayRepository.FindByDate(holiday.Date);
            if (status == (int)Holidays.Update) checkDublicate = checkDublicate.Where(x => x.Id != holiday.Id).ToList();
            if (!checkDublicate.Any() && holiday.Date.DayOfWeek != DayOfWeek.Saturday && holiday.Date.DayOfWeek != DayOfWeek.Sunday)
            {
                if (status == (int)Holidays.Update) _companyHolidayRepository.Update(holiday);
                if (status == (int)Holidays.New) _companyHolidayRepository.Create(holiday);
                _companyHolidayRepository.Save();
                return CheckResult(holiday);
            }
            return new ResponseForRequest() { Successful = false, Errors = new List<string>() { "Inavalid data.Please try again" } };
        }

        public ResponseForRequest DeleteById(int id)
        {
            var currentHoliday = _companyHolidayRepository.GetById(id);
                try
                {
                    _companyHolidayRepository.Delete(currentHoliday);
                    _companyHolidayRepository.Save();
                    return new ResponseForRequest() { Successful = true };
                }
                catch(Exception ex)
                {
                    logger.Error(ex.Message);
                    return new ResponseForRequest() { Successful = false, Errors = new List<string>() { "This holiday have already deleted" } };
                }
        }

        private ResponseForRequest CheckResult(CompanyHoliday result) {
            if (result == null)
            {
                return new ResponseForRequest() {Successful=false,Errors=new List<string>() {"Inavalid data.Please try again"} };
            }
            else
            {
                return new ResponseForRequest() { Successful = true };
            }
        }
    }
}
