using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerClientClassLibrary;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace UnitTestUniversalJson
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestUniversalNotError()
        {
            ClassWithNAttributes[] c = new ClassWithNAttributes[2];
            c[0] = new ClassWithNAttributes();
            c[1] = new ClassWithNAttributes();
            c[0].Attributes["login"] = "deREXte";
            c[0].Attributes["password"] = "cjcjht";
            c[1].Attributes["login"] = "User";
            c[1].Attributes["password"] = "123";
            UnivarsalJson uj = new UnivarsalJson();
            uj.Add("list", c);
            string json = uj.Serialize();
            Dictionary<string, object> s = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            Console.WriteLine(s.Values);
        }
    }
}
