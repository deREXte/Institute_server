using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClientClassLibrary.JSONTypes
{
    public class SelectJson : StructJson
    {
        public TableSchema TableSchema { get; set; }
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

        public RowJson()
        {
            Row = new List<string>();
        }
    }

}
