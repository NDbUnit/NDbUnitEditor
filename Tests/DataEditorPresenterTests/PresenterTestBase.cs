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
		protected IMessageCreator messageCreator;
		protected IFileDialogCreator fileDialogCreator;
		protected IDataEditorView view;
		protected IDataSetProvider datasetProvider;
		protected IUserSettingsRepository settingsRepositoru;
		protected IProjectRepository projectRepository;
		protected IApplicationController applicationController;

		protected void GenerateStubs()
		{
			datasetProvider = MockRepository.GenerateStub<IDataSetProvider>();
			view = MockRepository.GenerateStub<IDataEditorView>();
			messageCreator = MockRepository.GenerateStub<IMessageCreator>();
			fileDialogCreator = MockRepository.GenerateStub<IFileDialogCreator>();
			settingsRepositoru = MockRepository.GenerateStub<IUserSettingsRepository>();
			projectRepository = MockRepository.GenerateStub<IProjectRepository>();
			applicationController = MockRepository.GenerateStub<IApplicationController>();
		}

	}
}
