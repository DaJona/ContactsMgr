using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Formatting
    {
        public static string timeZonedDate(DateTime originalDate, string languageCode, int timeZoneMinsOffset = 0)
        {
            string formatPattern = "dd/MMM/yyyy";
            string formatedDate;

            try
            {
                DateTime newDate;

                if (timeZoneMinsOffset > 0)
                {
                    newDate = originalDate.AddMinutes((double)(-1 * timeZoneMinsOffset));
                }
                else
                {
                    newDate = originalDate.ToLocalTime();
                }

                formatedDate = newDate.ToString(formatPattern, CultureInfo.CreateSpecificCulture(languageCode));
            }
            catch (Exception)
            {
                formatedDate = originalDate.ToString(formatPattern);
            }

            return formatedDate;
        }

        public static string timeZonedDateTime(DateTime originalDateTime, string languageCode, int timeZoneMinsOffset = 0)
        {
            string formatPattern = "dd/MMM/yyyy hh:mm tt";
            string formatedDateTime;

            try
            {
                DateTime newDateTime;

                if (timeZoneMinsOffset > 0)
                {
                    newDateTime = originalDateTime.AddMinutes((double)(-1 * timeZoneMinsOffset));
                }
                else
                {
                    newDateTime = originalDateTime.ToLocalTime();
                }

                formatedDateTime = newDateTime.ToString(formatPattern, CultureInfo.CreateSpecificCulture(languageCode));
            }
            catch (Exception)
            {
                formatedDateTime = originalDateTime.ToString(formatPattern);
            }

            return formatedDateTime;
        }
    }
}
