using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using NDbUnitDataEditor;
using NDbUnitDataEditor.UI;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;
using NDbUnit.Utility;
using System.Threading;

namespace Tests.DataEditorPresenterTests
{

	[TestFixture]
	public class WhenExitingApplication: PresenterTestBase
	{
		[SetUp]
		public void TestSetup()
		{
			GenerateStubs();
		}

		[Test]
		public void ShouldPromptToSaveUnsavedData()
		{
			view.Stub(v => v.GetEditorSettings()).Return(new NdbUnitEditorSettings());
			var presenter = new DataEditorPresenter(view, dialogFactory,settings, settingsManger, datasetProvider);
			datasetProvider.DataSetLoadedFromDatabase = true;
			dialogFactory.Stub(f => f.CreateMessageDialog()).Return(messageDialog);
			RaiseExitAppEvent();
			messageDialog.AssertWasCalled(d => d.ShowYesNo(null), o => o.IgnoreArguments());
		}

		[Test]
		public void ShouldNotPromptWhenNothingWasChanged()
		{			
			view.Stub(v => v.GetEditorSettings()).Return(new NdbUnitEditorSettings());
			var presenter = new DataEditorPresenter(view, dialogFactory, settings, settingsManger, datasetProvider);
			dialogFactory.Stub(f => f.CreateMessageDialog()).Return(messageDialog);
			RaiseExitAppEvent();

			messageDialog.AssertWasNotCalled(d => d.ShowYesNo(null), o => o.IgnoreArguments());
		}

		private void RaiseExitAppEvent()
		{
			IEventRaiser eventRaiser = view.GetEventRaiser(v => v.ExitApp += null);
			eventRaiser.Raise();
		}

	}
}
