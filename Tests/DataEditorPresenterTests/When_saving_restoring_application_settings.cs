using System;
using MbUnit.Framework;
using System.Collections;
using Rhino.Mocks;
using NDbUnitDataEditor;
using NDbUnitDataEditor.UI;
using NDbUnit.Utility;
using System.IO;


namespace Tests.DataEditorPresenterTests
{
    public class When_saving_restoring_application_settings
    {
        private string _connectionString;
        private string _databaseType;
        private string _dataFileName;
        private string _schemaFileName;
        private string _settingsFileName;

        [SetUp]
        public void TestSetup()
        {
            _settingsFileName = "ndbUserSettings.xml";
            _dataFileName = @"C:\testData\data.xml";
            _schemaFileName = @"C:\testData\schema.xsd";
            _databaseType = "SQLLite";
            _connectionString = "Data Source=database; Integrated Security = true";
        }

        [Test]
        public void CanSaveApplicationSettings()       
        {
            var view = MockRepository.GenerateStub<IDataEditorView>();
           
            var settings = new NdbUnitEditorSettings();
            view.Stub(v => v.GetEditorSettings()).Return(settings);

            var dialogFactory = MockRepository.GenerateStub<IDialogFactory>();
            var fileDialog = MockRepository.GenerateStub<IFileDialog>();
            fileDialog.Stub(d => d.FileName).Return(_settingsFileName);
            fileDialog.Stub(d => d.Show()).Return(FileDialogResult.OK);
            dialogFactory.Stub(f => f.CreateFileDialog(FileDialogType.SaveFileDilaog, "XML files|*.xml")).Return(fileDialog);

            var userSettings = MockRepository.GenerateStub<IUserSettings>();
            var settingsManager = MockRepository.GenerateStub<INdbUnitEditorSettingsManager>();

            var presenter = new DataEditorPresenter(view, dialogFactory, userSettings, settingsManager);     
            presenter.SaveEditorSettings();
            settingsManager.AssertWasCalled(m => m.SaveSettings(settings, _settingsFileName));
            
        }



        private IDataEditorView CreateViewStub()
        {
            var view = MockRepository.GenerateStub<IDataEditorView>();
            view.DataFileName=_dataFileName;
            view.SchemaFileName=_schemaFileName;
            view.DatabaseClientType=_databaseType;
            view.DatabaseConnectionString=_connectionString;
            return view;
        }
    }
}
