﻿using LegacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.DataAccess
{
  public interface IUserDataAccess
  {
    public void AddUser(User user);
  }
}
