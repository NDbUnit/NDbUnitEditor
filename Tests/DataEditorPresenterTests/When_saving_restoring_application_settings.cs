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
    public class When_saving_restoring_application_settings: PresenterTestBase
    {
		private string _settingsFileName = "ndbUserSettings.xml";

        [SetUp]
        public void TestSetup()
        {
			GenerateStubs();
        }

        [Test]
        public void CanSaveApplicationSettings()       
        {  
            var editorsettings = new NdbUnitEditorSettings();
            view.Stub(v => v.GetEditorSettings()).Return(editorsettings); 
            
         	var fileOpenResult = new FileDialogResult{ Accepted=true, SelectedFileName=_settingsFileName};
			fileDialogCreator.Stub(d => d.ShowFileSave("XML files|*.xml")).Return(fileOpenResult);
			var presenter = new DataEditorPresenter(applicationController, view, fileDialogCreator, messageCreator, settings, settingsManger, datasetProvider);     
            presenter.SaveEditorSettings();
            settingsManger.AssertWasCalled(m => m.SaveSettings(editorsettings, _settingsFileName));            
        }


    }
}
