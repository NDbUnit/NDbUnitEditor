using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;

namespace NDbUnitDataEditor.ZeusSchemaBuilder.Tests
{
    public class BuilderSettingsTest
    {

        [TestFixture]
        public class When_Creating_BuilderSettings
        {
            private const string CONN_STRING = "connectionString";

            private const string DATASET_NAME = "dataSetName";

            private const string DB_NAME = "databaseName";

            private const string DB_TARGET_TYPE = "databaseTargetType";

            [Test]
            public void Can_Set_Properties_At_Construction()
            {
                List<string> tables = new List<string>() { "1,", "2", "3" };
                ZeusBuilderSettings settings = new ZeusBuilderSettings(CONN_STRING,
                                               DB_NAME,
                                               DB_TARGET_TYPE,
                                               DATASET_NAME,
                                               tables);

                Assert.AreEqual(CONN_STRING, settings.ConnectionString);
                Assert.AreEqual(DB_NAME, settings.DatabaseName);
                Assert.AreEqual(DB_TARGET_TYPE, settings.DatabaseTargetType);
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
                Assert.Throws<ArgumentException>(() => new ZeusBuilderSettings(CONN_STRING, DB_TARGET_TYPE, string.Empty,DATASET_NAME, new List<string>() { "1" }));
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
    }
}
