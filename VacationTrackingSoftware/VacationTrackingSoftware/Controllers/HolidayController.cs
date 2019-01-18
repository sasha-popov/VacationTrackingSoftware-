using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.IRepositories;
using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VacationTrackingSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : Controller
    {
        private ICompanyHolidayRepository _companyHolidayRepository;
        private ICompanyHolidayService _companyHolidayService;

        public HolidayController(ICompanyHolidayRepository companyHolidayRepository, ICompanyHolidayService companyHolidayService)
        {
            _companyHolidayRepository = companyHolidayRepository;
            _companyHolidayService = companyHolidayService;
        }
        [Authorize]
        //[Authorize(Policy = "ApiUser")]
        [HttpGet("[action]")]
        public List<CompanyHoliday> GetForCurrentYear()
        {
            return _companyHolidayRepository.FindByCondition(x => x.Date.Year == 2019).ToList();
        }

        [HttpDelete("[action]/{name}/{date}")]
        public void DeleteHoliday(string name,DateTime date) {
            var currentHoliday = _companyHolidayRepository.FindByCondition(x=>x.Description==name && x.Date==date).First();
            _companyHolidayRepository.Delete(currentHoliday);
            _companyHolidayRepository.SaveAsync();
        }

        [HttpPost("[action]")]
        public void AddHoliday(CompanyHoliday newHoliday)
        {
            if (ModelState.IsValid )
            {
                _companyHolidayService.AddHoliday(newHoliday);
            }   
        }


    }
}