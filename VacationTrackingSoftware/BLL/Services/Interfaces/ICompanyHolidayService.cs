using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;
using BLL.Result;

namespace BLL.Services
{
    public interface ICompanyHolidayService
    {
        ResponseForRequest AddOrUpdateHoliday(CompanyHoliday newHoliday, int status);
    }
}
