using LegacyApp.Models;

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
