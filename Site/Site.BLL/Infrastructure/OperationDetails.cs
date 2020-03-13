using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.BLL.Infrastructure
{
    public class OperationDetails
    {
        public bool Succeded { get; private set; }
        public string Message { get; private set; }
        public string Property { get; private set; }
        public OperationDetails(bool succedeed, string message, string prop)
        {
            Succeded = succedeed;
            Message = message;
            Property = prop;
        }
    }
}
