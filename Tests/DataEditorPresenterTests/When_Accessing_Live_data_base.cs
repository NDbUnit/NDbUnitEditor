using System;
using MbUnit.Framework;
using Rhino.Mocks;
using NDbUnitDataEditor.Commands;
using System.IO;

namespace Tests.DataEditorPresenterTests
{
	[TestFixture]
	public class When_Accessing_Live_data_base: PresenterTestBase
	{
		string promptText = "If you want to save your changes and continue please click Yes, otherwise click No.";

		[SetUp]
		public void TestSetup()
		{
			GenerateStubs();
		}

		[Test]
		[Description("Should prompt the user to save unsaved data")]
		public void ShouldPromptTheUserToSaveUnsaveddata()
		{
			datasetProvider.Stub(p => p.IsDirty()).Return(true);
			var presenter = CreatePresenter();
			var eventRaiser = view.GetEventRaiser(v => v.GetDataSetFromDatabase += null);
			eventRaiser.Raise();
			messageCreator.AssertWasCalled(m => m.AskUser(promptText));

		}

		[Test]
		[Description("Should not prompt the user to save unsaved data when there are no changes")]
		public void ShouldNotPromptTheUserToSaveUnsaveddata()
		{
			datasetProvider.Stub(p => p.IsDirty()).Return(false);
			var presenter = CreatePresenter();
			var eventRaiser = view.GetEventRaiser(v => v.GetDataSetFromDatabase += null);
			eventRaiser.Raise();
			messageCreator.AssertWasNotCalled(m => m.AskUser(promptText));
		}

		[Test]
		public void ShouldPromptTheUserToSaveUnsaveddataAndProceedWhenSaysYes()
		{

			datasetProvider.Stub(p => p.IsDirty()).Return(true);
			messageCreator.Stub(m=>m.AskUser(promptText)).Return(true);
			view.DataFileName = @"C:\TestData\data.xml";
			var directory = Path.GetDirectoryName(view.DataFileName);
			fileService.Stub(s => s.DirectoryExists(directory)).Return(true);
			var presenter = CreatePresenter();
			var eventRaiser = view.GetEventRaiser(v => v.GetDataSetFromDatabase += null);
			eventRaiser.Raise();
			messageCreator.AssertWasCalled(m => m.AskUser(promptText));
			applicationController.AssertWasCalled(c => c.ExecuteCommand<GetDataFromDatabaseCommand>());
			datasetProvider.AssertWasCalled(p => p.SaveDataToFile(view.DataFileName));
		}

		[Test]
		public void ShouldPromptTheUserToSaveUnsaveddataAndDoNotProceedWhenUserSaysNO()
		{
			datasetProvider.Stub(p => p.IsDirty()).Return(true);
			messageCreator.Stub(m => m.AskUser(promptText)).Return(false);
			
			var presenter = CreatePresenter();
			var eventRaiser = view.GetEventRaiser(v => v.GetDataSetFromDatabase += null);
			eventRaiser.Raise();
			messageCreator.AssertWasCalled(m => m.AskUser(promptText));
			applicationController.AssertWasNotCalled(c => c.ExecuteCommand<GetDataFromDatabaseCommand>());
			datasetProvider.AssertWasNotCalled(p => p.SaveDataToFile(null),o=>o.IgnoreArguments());
		}
	}
}
