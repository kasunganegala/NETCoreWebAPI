using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Common
{
    public class Error
    {
        public Error(string element, string message)
        {
            this.Element = element;
            this.Message = message;
        }
        public Error(string message)
        {
            this.Element = new Guid().ToString();
            this.Message = message;
        }

        public Error()
        {
            this.Element = new Guid().ToString();
            this.Message = "An Error Occured While Verifying Submitted Data";
        }

        public string Element { get; private set; }
        public string Message { get; private set; }
    }
}
