﻿using LegacyApp.Enums;
using LegacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Tests.TestHelpers
{
  public static class TestConstants
  {
    public const string Firname = "sashen";
    public const string Surname = "govender";
    public const string Email = "sashen@google.co.za";
    public static DateTime DateOfBirth = new DateTime(1992, 04, 04);
    public const int ClientId = 42;

    public static Client GetClient(string clientName)
    {
      return new Client()
      {

        ClientStatus = ClientStatus.none,
        Id = 42,
        Name = clientName
      };
    }
  }
}
