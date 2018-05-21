using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.NewOperations.Operations
{
    [Flags]
    enum Rights
    {
        Admin = 4,
        User = 2,
        Other = 1,
    }
}
