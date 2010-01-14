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
            
            INdbUnitEditorSettingsManager manager = new NdbUnitEditorSettingsManager();
            NdbUnitEditorSettings settings = new NdbUnitEditorSettings
            {
                XMLDataFilePath=@"C:\Settings\dataFile.xml",
                SchemaFilePath = @"C:\Settings\schemaFile.xml",
                DatabaseClientType="SQLLite",
                DatabaseConnectionString="connection string"
            };
            manager.SaveSettings(settings, _settingsFileName);

            Assert.IsTrue(File.Exists(_settingsFileName));
        }

        [Test]
        public void CanSaveAndLoadApplicationSettings()
        {
            INdbUnitEditorSettingsManager manager = new NdbUnitEditorSettingsManager();
            NdbUnitEditorSettings settings = new NdbUnitEditorSettings
            {
                XMLDataFilePath = @"C:\Settings\dataFile.xml",
                SchemaFilePath = @"C:\Settings\schemaFile.xml",
                DatabaseClientType = "SQLLite",
                DatabaseConnectionString = "connection string"
            };
            manager.SaveSettings(settings, _settingsFileName);
            var storedSettings = manager.LoadSettings(_settingsFileName);


            Assert.AreEqual(settings, storedSettings, new StructuralEqualityComparer<NdbUnitEditorSettings>
            {
                {x=>x.DatabaseClientType},
                {x=>x.DatabaseConnectionString},
                {x=>x.SchemaFilePath},
                {x=>x.XMLDataFilePath}
            });
        }
    }
}
