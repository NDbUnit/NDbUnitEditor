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
        public class When_Creating_Schema : IntegrationTestBase
        {

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
