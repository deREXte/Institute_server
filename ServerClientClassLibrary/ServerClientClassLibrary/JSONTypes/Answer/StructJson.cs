using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClientClassLibrary.JSONTypes
{
    public abstract class StructJson : Msg
    {

        public Dependences Dependence;

        public StructJson() 
        {
        }
       
        public StructJson(OperationCode code, string msg) : base(code, msg)
        {
        }
    }

    public class Dependences
    {
        public Dictionary<string, List<object>> Dependence;
    }
}
