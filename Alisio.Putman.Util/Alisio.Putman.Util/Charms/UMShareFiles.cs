using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Alisio.Putman.UtilMethods.Charms
{
    public class UMShareFiles
    {
        public List<string> FileTypes { get; set; }

        public List<IStorageItem> Files { get; set; }

        public UMShareFiles(List<string> fileTypes, List<IStorageItem> files)
        {
            this.FileTypes = fileTypes;
            this.Files = files;
        }
    }
}
