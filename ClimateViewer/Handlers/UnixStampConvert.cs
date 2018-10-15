using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimateViewer.Handlers
{
    /// <summary>
    /// Simple convert from unix datestamp to datetime
    /// </summary>

    public class UnixStampConvert
    {
        /// <summary>
        /// Convert unix datestamp to datetime
        /// </summary>
        /// <param name="unixtime">unix datestamp in seconds</param>
        /// <returns>return datetime value</returns>
        public static DateTime UnixTimeToDateTime(int unixtime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return sTime.AddSeconds(unixtime);
        }
    }
}
