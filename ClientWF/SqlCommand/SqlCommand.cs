using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWF.SqlCommand
{
    abstract class SqlCommand
    {
        public string Table
        {
            get; private set;
        }

        public Dictionary<string, object> WhereCondiotions;
    }
}
