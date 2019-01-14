using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Models;

namespace VacationTrackingSoftware.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            CreateMap<VacationPolicy, VacationPolicyDTO>()
            .ForMember("VacationType", opt => opt.MapFrom(c => c.VacationType.Name));
            CreateMap<VacationPolicyDTO, VacationPolicy>()
             .ForMember("VacationType", opt => opt.MapFrom(c => new VacationType())); ;
        }       
    }
}
