using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisio.Putman.UtilMethods
{
    public class UMMethod
    {
        public string Name { get; set; }

        public string Parent { get; set; }

        public UMMethod(string name, string parent)
        {
            this.Name = name;
            this.Parent = parent;
        }
    }
}
