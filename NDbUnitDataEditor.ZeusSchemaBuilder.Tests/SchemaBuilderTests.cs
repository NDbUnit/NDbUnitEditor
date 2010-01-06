using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using System.Data;
using NDbUnitDataEditor.Abstractions;

namespace NDbUnitDataEditor.ZeusSchemaBuilder.Tests
{
    public class SchemaBuilderTests
    {
        [TestFixture]
        public class When_Constructing_New_Instance
        {
            [Test]
            public void Can_Create_Instance()
            {
                SchemaBuilder builder = new SchemaBuilder();
            }

        }

        [TestFixture]
        [Category("Integration")]
        public class When_Creating_Schema
        {
            private IBuilderSettings _settings;
            [SetUp]
            public void _Setup()
            {
                string connectionString = "Provider=SQLOLEDB;User ID=sa; password=password;";
                string databaseName = "testdb";
                string databaseTargetType = "i will be ignored!";
                string dataSetName = "GeneratedDataSet";
                IEnumerable<string> tablesToProcess = new List<string>() { "Role", "UserRole", "User" };
                _settings = new   ZeusBuilderSettings(connectionString, databaseName, databaseTargetType, dataSetName, tablesToProcess, new ConnectionStringValidator(), new ConnectionStringProviderBuilder());
            }

            [Test]
            public void New_DataSet_Is_Returned()
            {
                SchemaBuilder builder = new SchemaBuilder();

                DataSet dataset = builder.GetSchema(_settings);
                Assert.IsNotNull(dataset);
            }
        }
    }
}
