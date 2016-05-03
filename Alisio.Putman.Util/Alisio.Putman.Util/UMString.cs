using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Alisio.Putman.UtilMethods
{
    /// <summary>
    /// This class adds methods for <seealso cref="System.String"/>.
    /// </summary>
    public class UMString
    {
        /// <summary>
        /// This method extracts all strings between two strings. This method returns a <seealso cref="System.Collections.Generic.List<System.String>"/>.
        /// </summary>
        /// <param name="text">The text from where you extract from.</param>
        /// <param name="startString">The start string.</param>
        /// <param name="endString">The end string.</param>
        /// <returns><seealso cref="System.Collections.Generic.List<System.String>"/></returns>
        public static List<string> ExtractFromString(string text, string startString, string endString)
        {
            List<string> matched = new List<string>();
            int indexStart = 0, indexEnd = 0;
            bool exit = false;
            while (!exit)
            {
                indexStart = text.IndexOf(startString);
                indexEnd = text.IndexOf(endString);
                if (indexStart != -1 && indexEnd != -1)
                {
                    matched.Add(text.Substring(indexStart + startString.Length, indexEnd - indexStart - startString.Length));
                    text = text.Substring(indexEnd + endString.Length);
                }
                else
                    exit = true;
            }
            return matched;
        }

        /// <summary>
        /// This method checks if a string is an email.
        /// </summary>
        /// <param name="email">String</param>
        /// <returns>Boolean</returns>
        public static Boolean CheckIfEmail(string email)
        {
            string strRegex = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
            Regex myRegex = new Regex(strRegex, RegexOptions.None);
            return myRegex.IsMatch(email);
        }
    }
}
