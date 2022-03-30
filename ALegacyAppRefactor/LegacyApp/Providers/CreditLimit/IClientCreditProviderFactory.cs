namespace LegacyApp.Providers.CreditLimit
{
  public interface IClientCreditProviderFactory
  {
    IClientCreditProvider GetClientProvider(string name);
  }
}