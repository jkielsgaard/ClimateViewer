using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimateViewer.Handlers
{
    public class UnixStampConvert
    {
        //public static long DateTimeToUnixTime(DateTime datetime)
        //{
        //    DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        //    return (long)(datetime - sTime).TotalSeconds;
        //}


        public static DateTime UnixTimeToDateTime(int unixtime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return sTime.AddSeconds(unixtime);
        }
    }
}
