using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Alisio.Putman.UtilMethods
{
    /// <summary>
    /// This class has methods regarding scrollviewers.
    /// </summary>
    public class UMScrollViewer
    {
        /// <summary>
        /// This method gets the scrollviewer from control.
        /// </summary>
        /// <param name="depObj">Dependency Object</param>
        /// <returns>Scrollviewer</returns>
        public static ScrollViewer GetScrollViewer(DependencyObject depObj)
        {
            if (depObj is ScrollViewer)
                return depObj as ScrollViewer;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = GetScrollViewer(child);
                if (result != null)
                    return result;
            }
            return null;
        }
    }
}
