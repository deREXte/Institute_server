using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Xml;
using ServerClientClassLibrary;

namespace ServerClientClassLibrary.JSONTypes
{
    public class DataTableJson : StructJson
    {
        public string DataTable { get; set; }

        public DataTableJson() { }

        public DataTableJson(OperationCode code, string dataTable)
        {
            Code = code;
            DataTable = dataTable;
        }

        public static DataTable Deserialize(string tableName, DataTableJson dataTable)
        {
            DataSet dataSet = new DataSet();
            using (var xmlreader = new StringReader(dataTable.DataTable))
                dataSet.ReadXml(xmlreader);
            return dataSet.Tables[tableName];
        }

        public static DataTableJson Serialize(DataTable dataTable)
        {
            StringBuilder xmlString = new StringBuilder();
            using (var xmlWriter = XmlWriter.Create(xmlString))
                dataTable.WriteXml(xmlWriter);
            DataTableJson dataTableJson = new DataTableJson(OperationCode.AnswerOK, xmlString.ToString());
            return dataTableJson;
        }
    }
}
