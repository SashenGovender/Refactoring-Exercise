using System;

namespace LegacyApp.Providers
{
  public class DateTimeProvider : IDateTimeProvider
  {
    public DateTime DateTimeNow => DateTime.Now;
  }
}
