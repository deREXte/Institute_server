using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClientClassLibrary
{
    public class SelectJson : StructJson
    {
        public List<string> NameTable { get; set; }
        public List<RowJson> Rows { get; set; }
    }

    public class RowJson
    {
        public List<string> Row { get; set; }
    }

}
