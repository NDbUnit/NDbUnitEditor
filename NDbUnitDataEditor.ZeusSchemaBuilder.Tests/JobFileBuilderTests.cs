using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using System.IO;
using System.Xml;
using System.Diagnostics;
using Rhino.Mocks;
using NDbUnitDataEditor.Abstractions;

namespace NDbUnitDataEditor.ZeusSchemaBuilder.Tests
{
    public class JobFileBuilderTests
    {
        [TestFixture]
        public class When_Creating_JobFile
        {
            private const string OUTPUT_FILENAME = @"..\..\TestingFolderRoot\JobSettings.xml";

            [SetUp]
            public void _Setup()
            {
                if (File.Exists(OUTPUT_FILENAME))
                    File.Delete(OUTPUT_FILENAME);

                Assert.IsFalse(File.Exists(OUTPUT_FILENAME), String.Format("Test precondition not satisfied: file {0} should not exist!", OUTPUT_FILENAME));

                IBuilderSettings settings = new   ZeusBuilderSettings("Provider=SQLOLEDB;User ID=sa;password=password;", "testdb", "SqlClient", "MyDataSet", new List<string> { "table1", "table2", "table3" }, new ConnectionStringValidator(), new ConnectionStringProviderBuilder());

                var fileBuilder = new ZeusJobSettingsFileBuilder(settings);
                fileBuilder.BuildJobSettingsFile(OUTPUT_FILENAME);
            }

            [Test]
            public void File_Is_Created_in_Expected_Path()
            {
                Assert.IsTrue(File.Exists(OUTPUT_FILENAME));
            }

            [Test]
            public void File_Contains_Expected_Xml_Content()
            {
                using (var reader = XmlReader.Create(OUTPUT_FILENAME))
                {
                    while (!reader.EOF)
                    {
                        reader.Read();
                        Debug.WriteLine(reader.AttributeCount);
                    }
                }

            }
        }
    }
}
