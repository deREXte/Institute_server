using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientWF.SqlCreater;
using ClientWF;
using System.Collections.Generic;

namespace ClientTests
{
    [TestClass]
    public class SqlLibTest
    {
        [TestMethod]
        public void TestSqlSelectCreator()
        {
            string expected = "SELECT * FROM Test";
            string result = SqlCommandCreater.CreateSelectCommand("Test");
            Assert.AreEqual(result, expected);
            expected = "SELECT field1,field2,field3 FROM Test";
            List<string> fields = new List<string>
            {
                "field1",
                "field2",
                "field3"
            };
            result = SqlCommandCreater.CreateSelectCommand("Test", fields);
            Assert.AreEqual(result, expected);
            expected = "SELECT field1,field2,field3 " +
                "FROM Test " +
                "WHERE field1=10 AND field2=20 OR field3=40";
            List<SqlFieldValuePair> fieldvaluepairs = new List<SqlFieldValuePair>()
            {
                new SqlFieldValuePair(
                    new KeyValuePair<string, object>("field1", 10), 
                                                     SqlCompareOperator.Equal),
                new SqlFieldValuePair(
                    new KeyValuePair<string, object>("field2", 20), 
                                                     SqlCompareOperator.Equal),
                new SqlFieldValuePair(
                    new KeyValuePair<string, object>("field3", 40), 
                                                     SqlCompareOperator.Equal),
            };
            List<SqlConditions> conditions = new List<SqlConditions>()
            {
                SqlConditions.AND,
                SqlConditions.OR,
            };
            result = SqlCommandCreater.CreateSelectCommand("Test", 
                                                            fields, 
                                                            fieldvaluepairs, 
                                                            conditions);
            Assert.AreEqual(expected, result);
            expected = "SELECT field1,field2,field3 " +
                "FROM Test " +
                "WHERE field1=10 AND field2=20 AND field3=40";
            result = SqlCommandCreater.CreateSelectCommand("Test", fields, fieldvaluepairs, SqlConditions.AND);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestSqlInsertCreater()
        {
            string expected = "INSERT INTO Test (field1,field2) VALUES (10,20)";
            List<SqlFieldValuePair> fieldValuePairs = new List<SqlFieldValuePair>()
            {
                new SqlFieldValuePair(
                    new KeyValuePair<string, object>("field1", 10), SqlCompareOperator.Equal),
                new SqlFieldValuePair(
                    new KeyValuePair<string, object>("field2", 20), SqlCompareOperator.Equal),
            };
            string result = SqlCommandCreater.CreateInsertCommand("Test", fieldValuePairs);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestSqlUpdateCreater()
        {
            string expected = "UPDATE Test SET field1=10,field2=20";
            List<SqlFieldValuePair> fieldValuePairs = new List<SqlFieldValuePair>()
            {
                new SqlFieldValuePair(
                    new KeyValuePair<string, object>("field1", 10), SqlCompareOperator.Equal),
                new SqlFieldValuePair(
                    new KeyValuePair<string, object>("field2", 20), SqlCompareOperator.Equal),
            };
            List<SqlFieldValuePair> whereConditions = new List<SqlFieldValuePair>()
            {
                new SqlFieldValuePair(
                    new KeyValuePair<string, object>("field1", 20), SqlCompareOperator.Bigger),
                new SqlFieldValuePair(
                    new KeyValuePair<string, object>("field2", 30), SqlCompareOperator.NotEqual),
            };
            string result = SqlCommandCreater.CreateUpdateCommand("Test", fieldValuePairs);
            Assert.AreEqual(expected, result);
            expected = "UPDATE Test SET field1=10,field2=20 WHERE field1>20 OR field2!=30";
            result = SqlCommandCreater.CreateUpdateCommand("Test", fieldValuePairs, whereConditions, SqlConditions.OR);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestSqlJoiner()
        {
            string expected = "field1=20 AND field2=30 AND field3=50";
            List<SqlFieldValuePair> fieldvaluepairs = new List<SqlFieldValuePair>()
            {
                new SqlFieldValuePair(
                    new KeyValuePair<string, object>("field1", 20), 
                                                     SqlCompareOperator.Equal),
                new SqlFieldValuePair(
                    new KeyValuePair<string, object>("field2", 30), 
                                                     SqlCompareOperator.Equal),
                new SqlFieldValuePair(
                    new KeyValuePair<string, object>("field3", 50), 
                                                     SqlCompareOperator.Equal),
            };
            string result = SqlConditionJoiner.Join(fieldvaluepairs, SqlConditions.AND);
            Assert.AreEqual(expected, result);
            expected = "field1=20 AND field2=30 OR field3=50";
            List<SqlConditions> list = new List<SqlConditions>()
            {
                SqlConditions.AND,
                SqlConditions.OR,
            };
            result = SqlConditionJoiner.Join(fieldvaluepairs, list);
            Assert.AreEqual(expected, result);
        }
    }
}
