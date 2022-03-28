using LegacyApp.DataAccess;
using LegacyApp.Repository;
using LegacyApp.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;

namespace LegacyApp.Tests.TestHelpers
{
  public class TestDoubles
  {
    public IClientRepository ClientRepository { get; set; }
    public IDateTimeProvider DateTimeProvider { get; set; }
    public IUserDataAccess UserDataAccess { get; set; }
    public IUserCreditService UserCreditService { get; set; }

    public TestDoubles()
    {
      ClientRepository = Substitute.For<IClientRepository>();
      DateTimeProvider = Substitute.For<IDateTimeProvider>();
      UserDataAccess = Substitute.For<IUserDataAccess>();
      UserCreditService = Substitute.For<IUserCreditService>();

      DateTimeProvider.DateTimeNow.Returns(new DateTime(2022, 03, 28));
    }

    public static TestDoubles GetDoubles()
    {
      return new TestDoubles();
    }
  }
}
