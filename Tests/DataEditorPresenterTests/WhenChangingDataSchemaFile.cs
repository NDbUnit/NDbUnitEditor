using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using NDbUnitDataEditor;
using Rhino.Mocks;

namespace Tests.DataEditorPresenterTests
{
	[TestFixture]
	public class WhenChangingDataAndSchemaFile: PresenterTestBase
	{
		[SetUp]
		public void TestSetup()
		{
			GenerateStubs();
		}

		[Test]
		public void ShouldSetNewSchemaFileNameInMainView()
		{
			var newSchemaFileName = "Schema1.xsd";
			fileDialogCreator.Stub(d => d.ShowFileOpen("")).IgnoreArguments().Return(new FileDialogResult { Accepted = true, SelectedFileName = newSchemaFileName });
			var presenter = new DataEditorPresenter(applicationController, view, fileDialogCreator, messageCreator, settingsRepositoru, projectRepository, datasetProvider);
			presenter.SelectSchemaFile();
			Assert.AreEqual(newSchemaFileName, view.SchemaFileName);
		}

		[Test]
		public void ShouldSetNewDataFileNameInMainView()
		{
			var newDataFileName = "Data.xml";
			fileDialogCreator.Stub(d => d.ShowFileOpen("")).IgnoreArguments().Return(new FileDialogResult { Accepted = true, SelectedFileName = newDataFileName });
			var presenter = new DataEditorPresenter(applicationController, view, fileDialogCreator, messageCreator, settingsRepositoru, projectRepository, datasetProvider);
			presenter.SelectDataFile();
			Assert.AreEqual(newDataFileName, view.DataFileName);
		}

		[Test]
		public void ShouldNotChangeSchemaFileNameInMainViewWhenSelectionCanceled()
		{
			var oldSchemaFileName = "Schema1";
			view.SchemaFileName = oldSchemaFileName;
			fileDialogCreator.Stub(d => d.ShowFileOpen("")).IgnoreArguments().Return(new FileDialogResult { Accepted = false});
			var presenter = new DataEditorPresenter(applicationController, view, fileDialogCreator, messageCreator, settingsRepositoru, projectRepository, datasetProvider);
			presenter.SelectSchemaFile();
			Assert.AreEqual(oldSchemaFileName, view.SchemaFileName);
		}

		[Test]
		public void ShouldNotChangeDataFileNameInMainViewWhenSelectionCanceled()
		{
			var oldSchemaFileName = "Data.xml";
			view.DataFileName = oldSchemaFileName;
			fileDialogCreator.Stub(d => d.ShowFileOpen("")).IgnoreArguments().Return(new FileDialogResult { Accepted = false });
			var presenter = new DataEditorPresenter(applicationController, view, fileDialogCreator, messageCreator, settingsRepositoru, projectRepository, datasetProvider);
			presenter.SelectDataFile();
			Assert.AreEqual(oldSchemaFileName, view.DataFileName);
		}
	}
}
