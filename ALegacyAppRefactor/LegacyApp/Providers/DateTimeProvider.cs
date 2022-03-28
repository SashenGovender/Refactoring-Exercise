using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Providers
{
  public class DateTimeProvider : IDateTimeProvider
  {
    public DateTime DateTimeNow => DateTime.Now;
  }
}
