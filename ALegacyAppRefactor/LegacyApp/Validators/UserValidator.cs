﻿using LegacyApp.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Validators
{
  public class UserValidator
  {
    private readonly IDateTimeProvider _dateTimeProvider;

    public UserValidator(IDateTimeProvider dateTimeProvider)
    {
      _dateTimeProvider = dateTimeProvider;
    }

    public bool IsAgeLessThan21(DateTime dateOfBirth)
    {
      var now = _dateTimeProvider.DateTimeNow;
      int age = now.Year - dateOfBirth.Year;

      if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
      {
        age--;
      }

      return age < 21;
    }

    public bool IsInValidEmailAddress(string email)
    {
      return email.Contains("@") && !email.Contains(".");
    }

    public bool IsFirNameOrSurnameInValid(string firname, string surname)
    {
      return string.IsNullOrEmpty(firname) || string.IsNullOrEmpty(surname);
    }

    public bool IsUserDetailsValid(string firname, string surname, string email, DateTime dateOfBirth)
    {
      if (IsFirNameOrSurnameInValid(firname, surname))
      {
        return false;
      }

      if (IsInValidEmailAddress(email))
      {
        return false;
      }

      if (IsAgeLessThan21(dateOfBirth))
      {
        return false;
      }
      return true;
    }
  }
}