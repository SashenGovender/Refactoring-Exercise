using LegacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Providers.CreditLimit
{
  public class ImportantClientCreditProvider : IClientCreditProvider
  {
    private readonly IUserCreditService _userCreditService;

    public ImportantClientCreditProvider(IUserCreditService userCreditService)
    {
      _userCreditService = userCreditService;
    }

    public string Name => "ImportantClient";

    public (bool hasLimit, int creditLimt) GetCreditLimit(User user)
    {
      var creditLimit = _userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
      return (true, creditLimit * 2);
    }
  }
}
