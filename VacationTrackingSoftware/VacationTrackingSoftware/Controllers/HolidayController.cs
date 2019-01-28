﻿    using System;
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
using VacationTrackingSoftware.Helpers;

namespace VacationTrackingSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            var currentHoliday = _companyHolidayRepository.FindByCondition(x=>x.Description==name && x.Date==date).FirstOrDefault();
            if (currentHoliday != null)
            {
                _companyHolidayRepository.Delete(currentHoliday);
                _companyHolidayRepository.Save();
            }
        }

        [HttpPost("[action]")]
        public IActionResult AddHoliday(CompanyHoliday newHoliday)
        {
            //var response;
            if (ModelState.IsValid)
            {
                 var result=_companyHolidayService.AddHoliday(newHoliday);
                //result == null? response = BadRequest(Errors.AddErrorToModelState("holidayError", "This DateTime is not available", ModelState)): response= new OkObjectResult("Holidays create");
                if (result == null)
                {
                    return BadRequest(Errors.AddErrorToModelState("holidayError", "This DateTime is not available", ModelState));
                }
                else {
                    return new OkObjectResult("Holidays create");
                }
            }
            else {
                return BadRequest(Errors.AddErrorToModelState("holidayError", "Invalid dates", ModelState));
            }
        }


    }
}