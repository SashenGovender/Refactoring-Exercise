namespace LegacyApp.Providers.CreditLimit
{
  public class ClientCreditProviderFactory : IClientCreditProviderFactory
  {
    private readonly IUserCreditService _userCreditService;

    public ClientCreditProviderFactory(IUserCreditService userCreditService)
    {
      _userCreditService = userCreditService;
    }

    public IClientCreditProvider GetClientProvider(string name)
    {
      return name switch
      {
        "VeryImportantClient" => new VeryImportantClientCreditProvider(),
        "ImportantClient" => new ImportantClientCreditProvider(_userCreditService),
        _ => new DefaultClientCreditProvider(_userCreditService)
      };
    }
  }
}
