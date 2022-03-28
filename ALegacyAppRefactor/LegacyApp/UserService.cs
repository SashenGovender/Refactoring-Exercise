using LegacyApp.DataAccess;
using LegacyApp.Models;
using LegacyApp.Providers;
using LegacyApp.Repository;
using System;

namespace LegacyApp
{
  public class UserService
  {
    private readonly IClientRepository _clientRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUserDataAccess _userDataAccess;
    private readonly IUserCreditService _userCreditService;

    public UserService() : this(new ClientRepository(), new DateTimeProvider(), new UserDataAccessProxy(), new UserCreditServiceClient())
    {

    }

    public UserService(IClientRepository clientRepository, DateTimeProvider dateTimeProvider, IUserDataAccess userDataAccess, IUserCreditService userCreditService)
    {
      _clientRepository = clientRepository;
      _dateTimeProvider = dateTimeProvider;
      _userDataAccess = userDataAccess;
      _userCreditService = userCreditService;
    }

    public bool AddUser(string firname, string surname, string email, DateTime dateOfBirth, int clientId)
    {
      if (string.IsNullOrEmpty(firname) || string.IsNullOrEmpty(surname))
      {
        return false;
      }

      if (email.Contains("@") && !email.Contains("."))
      {
        return false;
      }

      var now = _dateTimeProvider.DateTimeNow;
      int age = now.Year - dateOfBirth.Year;

      if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
      {
        age--;
      }

      if (age < 21)
      {
        return false;
      }

      var client = _clientRepository.GetById(clientId);

      var user = new User
      {
        Client = client,
        DateOfBirth = dateOfBirth,
        EmailAddress = email,
        Firstname = firname,
        Surname = surname
      };

      if (client.Name == "VeryImportantClient")
      {
        // Skip credit chek
        user.HasCreditLimit = false;
      }
      else if (client.Name == "ImportantClient")
      {
        // Do credit check and double credit limit
        user.HasCreditLimit = true;

        var creditLimit = _userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
        creditLimit = creditLimit * 2;
        user.CreditLimit = creditLimit;

      }
      else
      {
        // Do credit check
        user.HasCreditLimit = true;

        var creditLimit = _userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
        user.CreditLimit = creditLimit;

      }

      if (user.HasCreditLimit && user.CreditLimit < 500)
      {
        return false;
      }

      _userDataAccess.AddUser(user);

      return true;
    }
  }
}