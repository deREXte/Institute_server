using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClientClassLibrary.JSONTypes
{
    public class TableSchema
    {
        public string TableName { get; set; }
        public List<Column> Columns { get; set; }

        public TableSchema()
        {
            Columns = new List<Column>();
        }
    }
}
