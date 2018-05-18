using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerClientClassLibrary.JSONTypes;

namespace Server.DataTableInfo
{
    static class DataTableDependeces
    {
        private static Dictionary<string, Dependences> TableDependences;

        public static void InitializeDependences(IDependencesInitializer initializer)
        {
            TableDependences = initializer.InitializeDependences();
        }

        public static Dependences GetTableDependence(string tableName)
        {
            return TableDependences[tableName];
        }
    }
}
