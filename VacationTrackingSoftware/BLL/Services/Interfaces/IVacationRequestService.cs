using System;
using System.Collections.Generic;
using System.Text;
using BLL.DTO;
using BLL.Models;

namespace BLL.Services
{
   public interface IVacationRequestService
    {
        UserVacationRequestDTO CreateVacationRequest(UserVacationRequestDTO userVacationRequestDTO);
        List<UserVacationRequestDTO> ShowUserVacationRequest(string id);

        List<UserVacationRequestDTO> ShowUserVacationRequestForManager(AppUser user);
    }
}
