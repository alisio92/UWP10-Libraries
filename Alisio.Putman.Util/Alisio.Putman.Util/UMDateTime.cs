using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisio.Putman.UtilMethods
{
    /// <summary>
    /// This class add methods to the <seealso cref="System.DateTime"/> class.
    /// </summary>
    public static class UMDateTime
    {
        /// <summary>
        /// This method gets the day in the specified language. This method returns the day in string format.
        /// </summary>
        /// <param name="to">The language you want the day in. ==> ex. nl-NL</param>
        /// <param name="day">The day.</param>
        /// <returns>day in string format</returns>
        public static string ChangeLanguage(string to, DayOfWeek day)
        {
            CultureInfo lan = new CultureInfo(to);
            return lan.DateTimeFormat.GetDayName(day);
        }
    }
}
