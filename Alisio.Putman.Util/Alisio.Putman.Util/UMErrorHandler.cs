using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisio.Putman.UtilMethods.ErrorManager
{
    public class UMErrorHandler
    {
        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; }
        }
    }
}
