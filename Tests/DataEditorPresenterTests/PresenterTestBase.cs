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
	public class PresenterTestBase
	{
		protected IMessageDialog messageDialog;
		protected IDataEditorView view;
		protected IDataSetProvider datasetProvider;
		protected IDialogFactory dialogFactory;
		protected IUserSettings settings;
		protected INdbUnitEditorSettingsManager settingsManger;

		protected void GenerateStubs()
		{
			datasetProvider = MockRepository.GenerateStub<IDataSetProvider>();
			view = MockRepository.GenerateStub<IDataEditorView>();
			dialogFactory = MockRepository.GenerateStub<IDialogFactory>();
			messageDialog = MockRepository.GenerateStub<IMessageDialog>();
			settings = MockRepository.GenerateStub<IUserSettings>();
			settingsManger = MockRepository.GenerateStub<INdbUnitEditorSettingsManager>();
		}

	}
}
