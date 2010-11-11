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
            view.SchemaFileName="schema.xsd"; 
            
         	var fileOpenResult = new FileDialogResult{ Accepted=true, SelectedFileName=_settingsFileName};
			fileDialogCreator.Stub(d => d.ShowFileSave("XML files|*.xml")).Return(fileOpenResult);
			var presenter = CreatePresenter();
			presenter.Stub(p => p.GetEditorSettings()).Return(editorsettings);
			presenter.SaveEditorSettings();
            projectRepository.AssertWasCalled(m => m.SaveProject(editorsettings, _settingsFileName));            
        }

		[Test]
		public void ShouldShowErrorMessageWhenThereIsAnExceptionWhenLoadingProject()
		{
			projectRepository.Stub(r => r.LoadProject("")).IgnoreArguments().Throw(new Exception());
			var fileDialogResult = new FileDialogResult { Accepted = true };
			fileDialogCreator.Stub(c => c.ShowFileOpen("")).IgnoreArguments().Return(fileDialogResult);

			var presenter = CreatePresenter();
			
			presenter.LoadEditorSettings();
			messageCreator.AssertWasCalled(c => c.ShowError(""), o => o.IgnoreArguments());
		}

		private DataEditorPresenter CreatePresenter()
		{
			var presenter = MockRepository.GeneratePartialMock<DataEditorPresenter>(applicationController, view, fileDialogCreator, messageCreator, settingsRepositoru, projectRepository, datasetProvider);
			return presenter;     
            
		}

    }
}
