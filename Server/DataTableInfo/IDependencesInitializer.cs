using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerClientClassLibrary;
        
namespace Server.DataTableInfo
{
    interface IDependencesInitializer
    {
        Dictionary<string, Dependences> InitializeDependences();
    }
}
