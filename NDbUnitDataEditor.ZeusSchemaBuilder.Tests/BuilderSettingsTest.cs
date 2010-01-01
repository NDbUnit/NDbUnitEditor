﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using NDbUnitDataEditor.Abstractions;

namespace NDbUnitDataEditor.ZeusSchemaBuilder.Tests
{
    public class BuilderSettingsTest
    {
        private const string CONN_STRING = "connectionString";

        private const string DATASET_NAME = "dataSetName";

        private const string DB_NAME = "databaseName";

        private const string DB_TARGET_TYPE = "databaseTargetType";

        private static IEnumerable<string> tables = new List<string>() { "1,", "2", "3" };

        private static IBuilderSettings ConstructValidInstance()
        {
            return new ZeusBuilderSettings(CONN_STRING,
                                           DB_NAME,
                                           DB_TARGET_TYPE,
                                           DATASET_NAME,
                                           tables);
        }

        [TestFixture]
        public class When_Creating_BuilderSettings
        {
            [Test]
            public void Can_Set_Properties_At_Construction()
            {
                IBuilderSettings settings = BuilderSettingsTest.ConstructValidInstance();

                Assert.AreEqual(CONN_STRING, settings.ConnectionString);
                Assert.AreEqual(DB_NAME, settings.DatabaseName);
                Assert.AreEqual(DATASET_NAME, settings.DataSetName);
                Assert.AreEqual(tables, settings.TablesToProcess);
            }

            [Test]
            public void Can_Prevent_Invalid_Connection_String()
            {
                Assert.Throws<ArgumentException>(() => new ZeusBuilderSettings(string.Empty, DB_NAME, DB_TARGET_TYPE, DATASET_NAME, new List<string>() { "1" }));
            }

            [Test]
            public void Can_Prevent_Invalid_Database_Name()
            {
                Assert.Throws<ArgumentException>(() => new ZeusBuilderSettings(CONN_STRING, string.Empty, DB_TARGET_TYPE, DATASET_NAME, new List<string>() { "1" }));
            }

            [Test]
            public void Can_Prevent_Invalid_Database_Target_Type()
            {
                Assert.Throws<ArgumentException>(() => new ZeusBuilderSettings(CONN_STRING, DB_TARGET_TYPE, string.Empty, DATASET_NAME, new List<string>() { "1" }));
            }

            [Test]
            public void Can_Prevent_Invalid_Dataset_Name()
            {
                Assert.Throws<ArgumentException>(() => new ZeusBuilderSettings(CONN_STRING, DB_NAME, DB_TARGET_TYPE, string.Empty, new List<string>() { "1" }));
            }

            [Test]
            public void Can_Prevent_Invalid_Table_Names()
            {
                Assert.Throws<ArgumentException>(() => new ZeusBuilderSettings(CONN_STRING, DB_NAME, DB_TARGET_TYPE, DATASET_NAME, new List<string>()));
                Assert.Throws<ArgumentException>(() => new ZeusBuilderSettings(CONN_STRING, DB_NAME, DB_TARGET_TYPE, DATASET_NAME, null));
            }
        }
        [TestFixture]
        public class When_BuilderSettings_Is_Valid
        {
            [Test]
            public void DatabaseType_Is_Expected()
            {
                var settings = BuilderSettingsTest.ConstructValidInstance();
                Assert.AreEqual("SqlClient", settings.DatabaseTargetType);
            }

            [Test]
            public void DatabaseDriver_Is_Expected()
            {
                var settings = BuilderSettingsTest.ConstructValidInstance();
                Assert.AreEqual("SQL", settings.DatabaseDriver);
            }
        }
    }
}
