using System;

namespace LegacyApp.Providers
{
  public interface IDateTimeProvider
  {
    public DateTime DateTimeNow { get; }
  }
}