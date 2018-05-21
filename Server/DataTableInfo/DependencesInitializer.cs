using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerClientClassLibrary.JSONTypes;

namespace Server.DataTableInfo
{
    class DependencesInitializer : IDependencesInitializer
    {
        public Dictionary<string, Dependences> InitializeDependences()
        {
            Dictionary<string, Dependences> dependences = new Dictionary<string, Dependences>();
            Dependences dependence = new Dependences();
            dependence.Dependence = new Dictionary<string, List<object>>();
            dependence.Dependence.Add("Privilege", new List<object>() { "Admin", "User", "Other" });
            dependences["Users"] = dependence;
            return dependences;
        }
    }
}
