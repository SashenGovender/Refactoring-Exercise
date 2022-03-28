using LegacyApp.DataAccess;
using LegacyApp.Models;
using LegacyApp.Providers;
using LegacyApp.Repository;
using LegacyApp.Validators;
using System;

namespace LegacyApp
{
  public class UserService
  {
    private readonly IClientRepository _clientRepository;
    private readonly UserValidator _userValidator;
    private readonly IUserDataAccess _userDataAccess;
    private readonly IUserCreditService _userCreditService;

    public UserService() : this(new ClientRepository(), new UserValidator(new DateTimeProvider()), new UserDataAccessProxy(), new UserCreditServiceClient())
    {

    }

    public UserService(IClientRepository clientRepository, UserValidator userValidator, IUserDataAccess userDataAccess, IUserCreditService userCreditService)
    {
      _clientRepository = clientRepository;
      _userValidator = userValidator;
      _userDataAccess = userDataAccess;
      _userCreditService = userCreditService;
    }

    public bool AddUser(string firname, string surname, string email, DateTime dateOfBirth, int clientId)
    {
      if (!_userValidator.IsUserDetailsValid(firname, surname, email, dateOfBirth))
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