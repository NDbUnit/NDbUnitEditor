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
    public class When_saving_loading_ndbunitEditor_project: PresenterTestBase
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
            var editorsettings = new NdbUnitEditorProject();
            view.Stub(v => v.GetEditorSettings()).Return(editorsettings); 
            
         	var fileOpenResult = new FileDialogResult{ Accepted=true, SelectedFileName=_settingsFileName};
			fileDialogCreator.Stub(d => d.ShowFileSave("XML files|*.xml")).Return(fileOpenResult);
			var presenter = new DataEditorPresenter(applicationController, view, fileDialogCreator, messageCreator, settingsRepositoru, projectRepository, datasetProvider);     
            presenter.SaveEditorSettings();
            projectRepository.AssertWasCalled(m => m.SaveProject(editorsettings, _settingsFileName));            
        }


    }
}
