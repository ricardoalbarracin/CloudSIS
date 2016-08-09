using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLComponents
{
    public class InterceptContext : ContextBoundObject { }

    public class TransactionResult 
    {
        public TransactionStatus Status { get; set; }
        public dynamic DataObject { get; set; }
        public string Message { get; set; }
    }
}
