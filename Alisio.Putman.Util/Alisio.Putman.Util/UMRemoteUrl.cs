#define DEBUG
using Alisio.Putman.UtilMethods.ErrorManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Alisio.Putman.UtilMethods
{

    /// <summary>
    /// This class adds methds for remote url.
    /// </summary>
    public static class UMRemoteUrl
    {
        private static UMErrorHandler ErrorMessage { get; set; }

        /// <summary>
        /// This method returns the error. If everything was succesful this will return null.
        /// </summary>
        /// <returns>string</returns>

        public static String GetErrorMessage()
        {
            if (ErrorMessage != null)
                return ErrorMessage.Error;

            return null;
        }

        /// <summary>
        /// This method checks if remote url exists. This method returns a <seealso cref="System.Boolean"/>.
        /// </summary>
        /// <param name="url">The full url of a page.</param>
        /// <returns><seealso cref="System.Boolean"/></returns>
        public static async Task<Boolean> RemoteFileExists(string url)
        {
            if (url == "")
                return false;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage message = null;
                try {
                    message = client.GetAsync(url).Result;
                    if (message.StatusCode == HttpStatusCode.OK)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    ErrorMessage = new UMErrorHandler();
                    ErrorMessage.Error = e.Message;
                    return false;
                }
            };
        }
    }
}
