using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.IRepositories;
using BLL.Models;
using BLL.Result;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VacationTrackingSoftware.Helpers;

namespace VacationTrackingSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class HolidayController : Controller
    {
        private ICompanyHolidayRepository _companyHolidayRepository;
        private ICompanyHolidayService _companyHolidayService;

        public HolidayController(
            ICompanyHolidayRepository companyHolidayRepository, 
            ICompanyHolidayService companyHolidayService)
        {
            _companyHolidayRepository = companyHolidayRepository;
            _companyHolidayService = companyHolidayService;
        }
        [HttpGet("[action]")]
        public List<CompanyHoliday> GetForCurrentYear()
        {
            var result = _companyHolidayRepository.GetAllHolidaysForCurrentYear();
            return result;
        }

        [HttpDelete("[action]/{holidayId}")]
        [Authorize(Roles = "HrUser")]
        public ResponseForRequest DeleteHoliday(int holidayId)
        {
            var currentHoliday = _companyHolidayRepository.GetById(holidayId);
            if (currentHoliday != null)
            {
                try
                {
                    _companyHolidayRepository.Delete(currentHoliday);
                    _companyHolidayRepository.Save();
                    return new ResponseForRequest() { Successful = true };
                }
                catch {
                    return new ResponseForRequest() { Successful = false, Errors = new List<string>() { "This holiday have already deleted" } };
                }
            }
            return new ResponseForRequest() { Successful = false, Errors = new List<string>() { "This holiday have already deleted" } };
        }

        [HttpPost("[action]")]
        //[Authorize(Roles = "HrUser")]
        public ResponseForRequest AddHoliday(/*CompanyHoliday newHoliday*/)
        {
            //test
            CompanyHoliday newHoliday = new CompanyHoliday();
            newHoliday.Date = DateTime.Now;
            newHoliday.Description = "parametrs";
            //var response;
            if (ModelState.IsValid)
            {
                try { return _companyHolidayService.AddOrUpdateHoliday(newHoliday, (int)Holidays.New); }
                catch {
                    return new ResponseForRequest() { Successful = false, Errors = new List<string>() { "Inavalid data.Please try again" } };
                }
            }
                return new ResponseForRequest() { Successful = false, Errors = new List<string>() { "Inavalid data.Please try again" } };
        }

        [HttpPut("[action]")]
        //[Authorize(Roles = "HrUser")]
        public ResponseForRequest UpdateHoliday(/*CompanyHoliday companyHoliday*/)
        {
            //test
            CompanyHoliday companyHoliday = new CompanyHoliday();
            companyHoliday.Id = 3017;
            companyHoliday.Date = DateTime.Now;
            companyHoliday.Description = "parametrsWork";
            if (ModelState.IsValid)
            {
                try { return _companyHolidayService.AddOrUpdateHoliday(companyHoliday, (int)Holidays.Update); }
                catch
                {
                    return new ResponseForRequest() { Successful = false, Errors = new List<string>() { "Inavalid data.Please try again" } };
                }
                
            }
                return new ResponseForRequest() { Successful = false, Errors = new List<string>() { "Inavalid data.Please try again" } };
        }
    }
}