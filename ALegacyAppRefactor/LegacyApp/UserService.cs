using LegacyApp.DataAccess;
using LegacyApp.Models;
using LegacyApp.Providers;
using LegacyApp.Providers.CreditLimit;
using LegacyApp.Repository;
using LegacyApp.Validators;
using System;

namespace LegacyApp
{
  public class UserService
  {
    private readonly IClientRepository _clientRepository;
    private readonly IUserDataAccess _userDataAccess;
    private readonly IClientCreditProviderFactory _clientProviderFactory;
    private readonly UserValidator _userValidator;

    public UserService() : this(new ClientRepository(), new UserDataAccessProxy(), new UserValidator(new DateTimeProvider()), new ClientCreditProviderFactory(new UserCreditServiceClient()))
    {
    }

    public UserService(IClientRepository clientRepository, IUserDataAccess userDataAccess, UserValidator userValidator, IClientCreditProviderFactory clientProviderFactory)
    {
      _clientRepository = clientRepository;
      _userValidator = userValidator;
      _userDataAccess = userDataAccess;
      _clientProviderFactory = clientProviderFactory;
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

      var clientProvider = _clientProviderFactory.GetClientProvider(client.Name);
      (user.HasCreditLimit, user.CreditLimit) = clientProvider.GetCreditLimit(user);


      if (_userValidator.HasLimitAndLessThan500(user))
      {
        return false;
      }

      _userDataAccess.AddUser(user);

      return true;
    }

  }
}