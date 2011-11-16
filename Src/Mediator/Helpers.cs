using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediator
{
    public static class MediatorCommonHelpers
    {
        public static long ToUnixTicks(this DateTime current)
        {
            DateTime epoch = new DateTime(1970, 1, 1);
            TimeSpan ts = current.ToUniversalTime() - epoch;
            return (long)ts.TotalMilliseconds;
        }

        public static DateTime ToUtcDateTime(long unixticks)
        {
            DateTime epoch = new DateTime(1970, 1, 1);
            return epoch.AddMilliseconds(unixticks);
        }
    }
}
