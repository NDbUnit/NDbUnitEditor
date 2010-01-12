using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;

namespace NDbUnitDataEditor.ZeusSchemaBuilder.Tests
{
    public class TableListBuilderTests
    {
        [TestFixture]
        public class When_Querying_For_Tables : IntegrationTestBase
        {
            [Test]
            public void Can_Return_Expected_Tables()
            {
                TableListBuilder tables = new TableListBuilder(_connectionString);
                var tableList = tables.BuildTableList();

                Assert.AreElementsEqualIgnoringOrder(_tablesToProcess, tableList);
            }

        }
    }
}
