using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClientClassLibrary
{
    public class SelectJson : StructJson
    {
        public List<string> ColumnName { get; set; }
        public List<RowJson> Rows { get; set; }

        public SelectJson()
        {
        }

        public SelectJson(Code.OperationCode code, string msg) : base(code, msg)
        {
        }
    }

    public class RowJson
    {
        public List<string> Row { get; set; }
    }

}
