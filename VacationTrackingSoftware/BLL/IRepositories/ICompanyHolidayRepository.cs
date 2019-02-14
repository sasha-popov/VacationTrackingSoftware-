﻿using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;

namespace BLL.IRepositories
{
    public interface ICompanyHolidayRepository: IGenericRepository<CompanyHoliday>
    {
        List<CompanyHoliday> GetAllHolidaysForCurrentYear();
        List<CompanyHoliday> FindByDate(DateTime date);
    }
}
