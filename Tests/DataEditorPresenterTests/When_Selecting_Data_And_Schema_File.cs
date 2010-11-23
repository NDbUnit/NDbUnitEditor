using System;
using MbUnit.Framework;
using System.Collections;
using Rhino.Mocks;
using NDbUnitDataEditor;
using NDbUnitDataEditor.UI;
using NDbUnit.Utility;
using System.IO;
using Rhino.Mocks.Interfaces;

namespace Tests.DataEditorPresenterTests
{
	[TestFixture]
	public class When_Selecting_Data_And_Schema_File: PresenterTestBase
	{
		[SetUp]
		public void TestSetup()
		{
			GenerateStubs();
		}

		[Test]
		public void ShouldShowFileDialogToSelectDataFile()
		{
			fileDialogCreator.Stub(c => c.ShowFileOpen(null)).IgnoreArguments().Return(new FileDialogResult());
			IEventRaiser eventRaiser = view.GetEventRaiser(v => v.BrowseForDataFile += null);
			var presenter = CreatePresenter();
			eventRaiser.Raise();
			fileDialogCreator.AssertWasCalled(c => c.ShowFileOpen(null), o => o.IgnoreArguments());			
		}

		[Test]
		public void ShouldShowFileDialogToSelectSchemaFile()
		{
			fileDialogCreator.Stub(c => c.ShowFileOpen(null)).IgnoreArguments().Return(new FileDialogResult());
			IEventRaiser eventRaiser = view.GetEventRaiser(v => v.BrowseForSchemaFile += null);
			var presenter = CreatePresenter();
			eventRaiser.Raise();
			fileDialogCreator.AssertWasCalled(c => c.ShowFileOpen(null), o => o.IgnoreArguments());
		}

		[Test]
		public void ShouldUpdateDataFileName()
		{
			var dataFileName = "DataFile.xml";
			fileDialogCreator.Stub(c => c.ShowFileOpen(null)).IgnoreArguments().Return(new FileDialogResult { Accepted = true, SelectedFileName = dataFileName });
			IEventRaiser eventRaiser = view.GetEventRaiser(v => v.BrowseForDataFile += null);
			var presenter = CreatePresenter();
			eventRaiser.Raise();
			Assert.AreEqual(dataFileName, view.DataFileName);
		}

		[Test]
		public void ShouldUpdateSchemaFileName()
		{
			var schemaFileName = "SchemaFile.xsd";
			fileDialogCreator.Stub(c => c.ShowFileOpen(null)).IgnoreArguments().Return(new FileDialogResult { Accepted = true, SelectedFileName = schemaFileName });
			IEventRaiser eventRaiser = view.GetEventRaiser(v => v.BrowseForSchemaFile += null);
			var presenter = CreatePresenter();
			eventRaiser.Raise();
			Assert.AreEqual(schemaFileName, view.SchemaFileName);
		}
	}
}
