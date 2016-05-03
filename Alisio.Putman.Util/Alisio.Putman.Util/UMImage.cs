using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Alisio.Putman.UtilMethods
{
    public class UMImage
    {
        public static async Task<string> ImageToBase64(StorageFile MyImageFile)
        {
            Stream ms = await MyImageFile.OpenStreamForReadAsync();
            byte[] imageBytes = new byte[(int)ms.Length];
            ms.Read(imageBytes, 0, (int)ms.Length);
            return Convert.ToBase64String(imageBytes);
        }
    }
}
