using LegacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Providers.CreditLimit
{
  public class VeryImportantClientCreditProvider : IClientCreditProvider
  {
    public string Name => "VeryImportantClient";

    public (bool hasLimit, int creditLimt) GetCreditLimit(User user)
    {
      return (false, 0);
    }
  }
}
