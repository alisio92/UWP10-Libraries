using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace Alisio.Putman.UtilMethods
{
    /// <summary>
    /// This class contains methods regarding internet connection.
    /// </summary>
    public class UMInternet
    {
        public static int WLAN = 0;
        public static int WWAN = 1;

        /// <summary>
        /// This method checks if there is internet.
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsInternet()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            if (connections != null)
                if (connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess)
                    return true;

            return false;
        }

        /// <summary>
        /// This method checks the type of connection.
        /// </summary>
        /// <returns></returns>
        public static int GetConnectionType()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            if (connections != null)
            {
                if (connections.IsWlanConnectionProfile)
                    return WLAN;

                if (connections.IsWwanConnectionProfile)
                    return WWAN;
            }

            return -1;
        }

        /// <summary>
        /// This method gets the SSID.
        /// </summary>
        /// <returns></returns>
        public static string GetSSID()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            if (connections != null)
                return connections.ProfileName;

            return "";
        }
    }
}
