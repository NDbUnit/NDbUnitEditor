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
		private string _connectionString = "Data Source=database; Integrated Security = true";
		private string _databaseType = "SQLLite";
		private string _dataFileName = @"C:\testData\data.xml";
		private string _schemaFileName = @"C:\testData\schema.xsd";
		private string _settingsFileName = "ndbUserSettings.xml";

		IDataEditorView view;
		IDialogFactory dialogFactory;
		IUserSettings userSettings;
		INdbUnitEditorSettingsManager settingsManager;
		IFileDialog fileDialog;

        [SetUp]
        public void TestSetup()
        {
			view = MockRepository.GenerateStub<IDataEditorView>();
			dialogFactory = MockRepository.GenerateStub<IDialogFactory>();
			userSettings = MockRepository.GenerateStub<IUserSettings>();
			settingsManager = MockRepository.GenerateStub<INdbUnitEditorSettingsManager>();
			fileDialog = MockRepository.GenerateStub<IFileDialog>();
        }

        [Test]
        public void CanSaveApplicationSettings()       
        {  
            var settings = new NdbUnitEditorSettings();
            view.Stub(v => v.GetEditorSettings()).Return(settings);                      
            fileDialog.Stub(d => d.FileName).Return(_settingsFileName);
            fileDialog.Stub(d => d.Show()).Return(FileDialogResult.OK);
            dialogFactory.Stub(f => f.CreateFileDialog(FileDialogType.SaveFileDilaog, "XML files|*.xml")).Return(fileDialog);
            
			var presenter = new DataEditorPresenter(view, dialogFactory, userSettings, settingsManager, null);     
            presenter.SaveEditorSettings();
            settingsManager.AssertWasCalled(m => m.SaveSettings(settings, _settingsFileName));
            
        }
    }
}
