using System;
using MbUnit.Framework;
using NDbUnit.Utility;
using System.IO;


namespace UserSettingsTest
{
    public class When_saving_restoring_application_settings
    {
        string _settingsFileName = "testSettings.xml";
        [SetUp]
        public void TestSetup()
        {
            if (File.Exists(_settingsFileName))
                File.Delete(_settingsFileName);
        }

        [Test]
        public void CanSaveApplicationSettings()
        {
            
            IProjectRepository manager = new ProjectRepository();
            NdbUnitEditorProject settings = new NdbUnitEditorProject
            {
                XMLDataFilePath=@"C:\Settings\dataFile.xml",
                SchemaFilePath = @"C:\Settings\schemaFile.xml",
                DatabaseClientType="SQLLite",
                DatabaseConnectionString="connection string"
            };
            manager.SaveProject(settings, _settingsFileName);

            Assert.IsTrue(File.Exists(_settingsFileName));
        }

        [Test]
        public void CanSaveAndLoadApplicationSettings()
        {
            IProjectRepository manager = new ProjectRepository();
            NdbUnitEditorProject settings = new NdbUnitEditorProject
            {
                XMLDataFilePath = @"C:\Settings\dataFile.xml",
                SchemaFilePath = @"C:\Settings\schemaFile.xml",
                DatabaseClientType = "SQLLite",
                DatabaseConnectionString = "connection string"
            };
            manager.SaveProject(settings, _settingsFileName);
            var storedSettings = manager.LoadProject(_settingsFileName);


            Assert.AreEqual(settings, storedSettings, new StructuralEqualityComparer<NdbUnitEditorProject>
            {
                {x=>x.DatabaseClientType},
                {x=>x.DatabaseConnectionString},
                {x=>x.SchemaFilePath},
                {x=>x.XMLDataFilePath}
            });
        }
    }
}
