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
using Rhino.Mocks.Interfaces;
using NDbUnitDataEditor.Commands;


namespace Tests.DataEditorPresenterTests
{
	public class When_saving_loading_ndbunitEditor_project : PresenterTestBase
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
			view.SchemaFileName = "schema.xsd";
			var fileOpenResult = new FileDialogResult { Accepted = true, SelectedFileName = _settingsFileName };
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
			var openedTabsNames = new List<string> { "Tab1", "Tab2", "Tab3" };
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
			projectRepository.Stub(r => r.LoadProject(projectFileName)).Return(new NdbUnitEditorProject { SchemaFilePath="schema.xml", OpenedTabs = openedTabsNames });
			foreach (var tabName in openedTabsNames)
				datasetProvider.Stub(d => d.GetTable(tabName)).Return(new DataTable { TableName = tabName });
			var presenter = CreatePresenter();
			presenter.OpenProject(projectFileName);

			view.AssertWasCalled(v => v.OpenTableView(null), o => o.IgnoreArguments().Repeat.Times(3));
		}

		[Test]
		public void ShouldCreateNewProjectAfterExistingHasBeenOpened()
		{
			var projectFileName = "testProject.xml";
			projectRepository.Stub(r => r.LoadProject(projectFileName))
				.Return(new NdbUnitEditorProject
				{
					DatabaseClientType = DatabaseClientType.SqlClient.ToString(),
					DatabaseConnectionString="Data Source=localhost; initial catalog=test",
					OpenedTabs=new List<string>{"Table1", "Table2", "Table3"},
					SchemaFilePath="schema1.xsd",
					XMLDataFilePath="data.xml"					
				});

			var presenter = CreatePresenter();
			presenter.OpenProject(projectFileName);
			IEventRaiser eventRaiser = view.GetEventRaiser(v => v.NewProject += null);
			eventRaiser.Raise();

			Assert.IsEmpty(view.SchemaFileName);
			Assert.IsEmpty(view.DataFileName);
			Assert.IsEmpty(view.DatabaseClientType);
			Assert.IsEmpty(view.DatabaseConnectionString);
			Assert.IsNull(view.OpenedTabNames);
			datasetProvider.AssertWasCalled(d => d.CreateNewDataset());			
		}

		[Test]
		public void ShouldNotReloadSchemaFileWhenFilenameIsEmptyOrNul()
		{
			var projectFileName = "testProject.xml";
			projectRepository.Stub(r => r.LoadProject(projectFileName))
				.Return(new NdbUnitEditorProject
				{
					SchemaFilePath = ""
				});
			var presenter = CreatePresenter();
			presenter.OpenProject(projectFileName);
			applicationController.AssertWasNotCalled(c => c.ExecuteCommand<ReloadSchemaCommand>());
		}

		[Test]
		public void ShouldReloadSchemaWhenFileNameIsNotEmpty()
		{
			var projectFileName = "testProject.xml";
			projectRepository.Stub(r => r.LoadProject(projectFileName))
				.Return(new NdbUnitEditorProject
				{
					SchemaFilePath = "schema.xsd"
				});
			var presenter = CreatePresenter();
			presenter.OpenProject(projectFileName);
			applicationController.AssertWasCalled(c => c.ExecuteCommand<ReloadSchemaCommand>());
		}

		[Test]
		[Row("", "data.xml")]
		[Row("schema.xsd", "")]
		[Row("","")]
		public void ShouldNotReloadDataWhenSchemaFilenameIsEmpty(string schemaFileName, string dataFileName)
		{
			var projectFileName = "testProject.xml";
			projectRepository.Stub(r => r.LoadProject(projectFileName))
				.Return(new NdbUnitEditorProject
				{
					SchemaFilePath = schemaFileName,
					XMLDataFilePath=dataFileName
				});
			var presenter = CreatePresenter();
			presenter.OpenProject(projectFileName);
			applicationController.AssertWasNotCalled(c => c.ExecuteCommand<ReloadDataCommand>());
		}

		[Test]
		public void ShouldReloadDataWhenSchemaFileAndDataFilenameAreNotEmpty()
		{
			var projectFileName = "testProject.xml";
			projectRepository.Stub(r => r.LoadProject(projectFileName))
				.Return(new NdbUnitEditorProject
				{
					SchemaFilePath = "schema.xsd",
					XMLDataFilePath = "data.xml"
				});
			var presenter = CreatePresenter();
			presenter.OpenProject(projectFileName);
			applicationController.AssertWasCalled(c => c.ExecuteCommand<ReloadDataCommand>());
		}
	}
}
