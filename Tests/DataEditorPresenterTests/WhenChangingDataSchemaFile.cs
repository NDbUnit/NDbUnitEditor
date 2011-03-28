using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using NDbUnitDataEditor;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;
using System.Threading;

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
			var presenter = CreatePresenter();
			presenter.SelectSchemaFile();
			Assert.AreEqual(newSchemaFileName, view.SchemaFileName);
		}

		[Test]
		public void ShouldSetNewDataFileNameInMainView()
		{
			var newDataFileName = "Data.xml";
			fileDialogCreator.Stub(d => d.ShowFileOpen("")).IgnoreArguments().Return(new FileDialogResult { Accepted = true, SelectedFileName = newDataFileName });
			var presenter = CreatePresenter();
			presenter.SelectDataFile();
			Assert.AreEqual(newDataFileName, view.DataFileName);
		}

		[Test]
		public void ShouldNotChangeSchemaFileNameInMainViewWhenSelectionCanceled()
		{
			var oldSchemaFileName = "Schema1";
			view.SchemaFileName = oldSchemaFileName;
			fileDialogCreator.Stub(d => d.ShowFileOpen("")).IgnoreArguments().Return(new FileDialogResult { Accepted = false});
			var presenter = CreatePresenter();
			presenter.SelectSchemaFile();
			Assert.AreEqual(oldSchemaFileName, view.SchemaFileName);
		}

		[Test]
		public void ShouldNotChangeDataFileNameInMainViewWhenSelectionCanceled()
		{
			var oldSchemaFileName = "Data.xml";
			view.DataFileName = oldSchemaFileName;
			fileDialogCreator.Stub(d => d.ShowFileOpen("")).IgnoreArguments().Return(new FileDialogResult { Accepted = false });
			var presenter = CreatePresenter();
			presenter.SelectDataFile();
			Assert.AreEqual(oldSchemaFileName, view.DataFileName);
		}

		[Test]
		public void ShouldSaveExistingDataFileAs()
		{
			var newDataFileName = "newDataFile.xml";
			view.DataFileName = "olddataFile.xml";
			fileDialogCreator.Stub(c => c.ShowFileSave(null)).IgnoreArguments().Return(new FileDialogResult { Accepted = true, SelectedFileName = newDataFileName });
			IEventRaiser eventRaiser = view.GetEventRaiser(v => v.SaveDataAs += null);
			var presenter = CreatePresenter();
			eventRaiser.Raise();
			Assert.AreEqual(newDataFileName, view.DataFileName);
		}

		[Test]
		[Row("schemaFile.xsd", "dataFile.xml", true)]
		[Row("schemaFile.xsd", "", false)]
		[Row("", "dataFile.xml", false)]
		[Row("", "", false)]
		public void ShoulEnableOrDisableReloadButtonWhenDataFileIsChanged(string schemaFileName, string dataFileName, bool isReloadEnabled)
		{
			view.SchemaFileName = schemaFileName;
			view.DataFileName = dataFileName;
			IEventRaiser eventRaiser = view.GetEventRaiser(v => v.DataFileChanged += null);
			var presenter = CreatePresenter();
			eventRaiser.Raise();
			Assert.AreEqual(isReloadEnabled, view.IsReloadEnabled);
		}

		[Test]
		[Row("schemaFile.xsd", "dataFile.xml", true)]
		[Row("schemaFile.xsd", "", false)]
		[Row("", "dataFile.xml", false)]
		[Row("", "", false)]
		public void ShoulEnableOrDisableReloadButtonWhenSchemaFileIsChanged(string schemaFileName, string dataFileName, bool isReloadEnabled)
		{
			view.SchemaFileName = schemaFileName;
			view.DataFileName = dataFileName;
			IEventRaiser eventRaiser = view.GetEventRaiser(v => v.SchemaFileChanged += null);
			var presenter = CreatePresenter();
			eventRaiser.Raise();
			Assert.AreEqual(isReloadEnabled, view.IsReloadEnabled);
		}


	}
}
