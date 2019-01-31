﻿using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;

namespace BLL.Services
{
    public interface ICompanyHolidayService
    {
        CompanyHoliday AddHoliday(CompanyHoliday newHoliday);
        CompanyHoliday UpdateHoliday(CompanyHoliday holiday);
    }
}
