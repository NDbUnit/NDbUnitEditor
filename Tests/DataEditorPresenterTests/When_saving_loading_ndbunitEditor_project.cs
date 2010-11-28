using System;
using MbUnit.Framework;
using System.Collections;
using Rhino.Mocks;
using NDbUnitDataEditor;
using NDbUnitDataEditor.UI;
using NDbUnit.Utility;
using System.IO;
using System.Collections.Generic;
using System.Data;


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
			presenter.Stub(p => p.GetProjectData()).Return(editorsettings);
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

		[Test]
		public void ShouldSaveListOfOpenedTabs()
		{
			var openedTabsNames = new List<string>{"Tab1", "Tab2", "Tab3"};
			view.Stub(v => v.OpenedTabNames).Return(openedTabsNames);
			view.ProjectFileName = "Project.xml";
			var presenter = CreatePresenter();
			presenter.SaveEditorSettings();

			var projectSettings = projectRepository.GetArgumentsForCallsMadeOn(r => r.SaveProject(null, null))[0][0] as NdbUnitEditorProject;
			Assert.AreElementsEqual(openedTabsNames, projectSettings.OpenedTabs);

		}

		[Test]
		public void ShouldLoadListOfOpenedTabs()
		{
			var openedTabsNames = new List<string> { "Tab1", "Tab2", "Tab3" };			
			var projectFileName = "Project.xml";
			settingsRepositoru.Stub(r => r.GetSetting(DataEditorPresenter.RECENT_PROJECT_FILE_KEY)).Return(projectFileName);
			projectRepository.Stub(r => r.LoadProject(projectFileName)).Return(new NdbUnitEditorProject { OpenedTabs = openedTabsNames });
			foreach(var tabName in openedTabsNames)
				datasetProvider.Stub(d=>d.GetTable(tabName)).Return(new DataTable{TableName=tabName});
			var presenter = CreatePresenter();
			presenter.OpenProject(projectFileName);

			view.AssertWasCalled(v => v.OpenTableView(null), o => o.IgnoreArguments().Repeat.Times(3));		
		}

		[Test]
		public void ShouldEnableGetDatasetFromDatabaseButton()
		{
			var projectFileName = "Project.xml";
			settingsRepositoru.Stub(r => r.GetSetting(DataEditorPresenter.RECENT_PROJECT_FILE_KEY)).Return(projectFileName);
			projectRepository.Stub(r => r.LoadProject(projectFileName)).Return(new NdbUnitEditorProject { XMLDataFilePath=@"C:\Data.xml"});
			var presenter = CreatePresenter();
			presenter.OpenProject(projectFileName);
			view.AssertWasCalled(v => v.EnableDataSetFromDatabaseButton());
		}

		[Test]
		public void ShouldDisableGetDatasetFromDatabaseButtonForDataFileNotSpecified()
		{
			var projectFileName = "Project.xml";
			settingsRepositoru.Stub(r => r.GetSetting(DataEditorPresenter.RECENT_PROJECT_FILE_KEY)).Return(projectFileName);
			projectRepository.Stub(r => r.LoadProject(projectFileName)).Return(new NdbUnitEditorProject {});
			var presenter = CreatePresenter();
			presenter.OpenProject(projectFileName);
			view.AssertWasCalled(v => v.DisableDataSetFromDatabaseButton());
		}
	}
}
