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
             .ForMember("VacationType", opt => opt.MapFrom(c => new VacationType()));

            CreateMap<UserVacationRequest, UserVacationRequestDTO>()
            .ForMember("VacationType", opt => opt.MapFrom(c => c.VacationType.Name))
            .ForMember("UserId", opt => opt.MapFrom(user => user.User.Id))
            .ForMember("UserName", opt => opt.MapFrom(user => user.User.UserName))
            .ForMember("Status", opt => opt.MapFrom(statuses => statuses.Status));
        //    .ForMember("Status", opt => opt.MapFrom(statuses => Enum.GetName(typeof(RequestStatuses), statuses.Status)));
        }
    }
}
