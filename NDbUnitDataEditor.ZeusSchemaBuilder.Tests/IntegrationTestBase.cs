using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using System.Data;
using NDbUnitDataEditor.Abstractions;
using NDbUnitDataEditor.ZeusSchemaBuilder;

namespace NDbUnitDataEditor.ZeusSchemaBuilder.Tests
{
    public abstract class IntegrationTestBase
    {
        protected string _connectionString;

        protected IBuilderSettings _settings;

        protected IEnumerable<string> _tablesToProcess;

        [SetUp]
        public void _Setup()
        {
            _connectionString = "Provider=SQLOLEDB;User ID=sa; password=password;Initial Catalog=testdb";
            string databaseName = "testdb";
            string databaseTargetType = "i will be ignored!";
            string dataSetName = "GeneratedDataSet";
            _tablesToProcess = new List<string> { "Role", "UserRole", "User" };
            _settings = new ZeusBuilderSettings(_connectionString, databaseName, databaseTargetType, dataSetName, _tablesToProcess, new ConnectionStringValidator(), new ConnectionStringProviderBuilder());
        }

    }
}
