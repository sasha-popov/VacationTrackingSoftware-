using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.IRepositories;
using BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VacationTrackingSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationTypeController : ControllerBase
    {
        IVacationTypeRepository _vacationTypeRepository;
        public VacationTypeController(IVacationTypeRepository vacationTypeRepository) {
            _vacationTypeRepository = vacationTypeRepository;
        }
        [HttpGet("[action]")]
        public List<VacationType> GetAllVacationType() {
            var result = _vacationTypeRepository.GetAll();
            return result;
        }
    }
}