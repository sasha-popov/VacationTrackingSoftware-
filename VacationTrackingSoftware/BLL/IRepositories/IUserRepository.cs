﻿using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;

namespace BLL.IRepositories
{
    public interface IUserRepository: IGenericRepository<User>
    {
        User GetWithUserRoles(string name, string password);
    }
}
