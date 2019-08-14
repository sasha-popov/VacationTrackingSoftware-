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
        //[Authorize(Roles = "HrUser")]
        public ResponseForRequest DeleteHoliday(int holidayId)
        {
           return _companyHolidayService.DeleteById(holidayId);
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "HrUser")]
        public ResponseForRequest AddHoliday(CompanyHoliday newHoliday)
        {
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
        [Authorize(Roles = "HrUser")]
        public ResponseForRequest UpdateHoliday(CompanyHoliday companyHoliday)
        {
 
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