using LegacyApp.Models;

namespace LegacyApp.Providers.CreditLimit
{
  public interface IClientCreditProvider
  {
    public (bool hasLimit, int creditLimt) GetCreditLimit(User user);

    public string Name { get; }
  }
}
