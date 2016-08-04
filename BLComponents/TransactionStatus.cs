using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLComponents
{
    public enum TransactionStatus
    {
        // The flag for SunRoof is 0001.
        Success = 0x00,
        // The flag for Spoiler is 0010.
        Fail = 0x01,
    }
}
