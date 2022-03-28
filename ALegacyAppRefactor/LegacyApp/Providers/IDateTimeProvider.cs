using System;

namespace LegacyApp.Providers
{
  internal interface IDateTimeProvider
  {
    public DateTime DateTimeNow { get; }
  }
}