using LegacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Providers.CreditLimit
{
  public interface IClientCreditProvider
  {
    public (bool hasLimit, int creditLimt) GetCreditLimit(User user);

    public string Name { get; }
  }
}
