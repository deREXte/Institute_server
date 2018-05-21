using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerClientClassLibrary.JSONTypes;
        
namespace Server.DataTableInfo
{
    public interface IDependencesInitializer
    {
        Dictionary<string, Dependences> InitializeDependences();
    }
}
