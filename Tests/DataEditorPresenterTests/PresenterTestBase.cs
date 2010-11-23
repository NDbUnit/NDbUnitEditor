using System;
using NDbUnitDataEditor;
using Rhino.Mocks;
using NDbUnit.Utility;

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
		protected IFileService fileService;

		protected void GenerateStubs()
		{
			datasetProvider = MockRepository.GenerateStub<IDataSetProvider>();
			view = MockRepository.GenerateStub<IDataEditorView>();
			messageCreator = MockRepository.GenerateStub<IMessageCreator>();
			fileDialogCreator = MockRepository.GenerateStub<IFileDialogCreator>();
			settingsRepositoru = MockRepository.GenerateStub<IUserSettingsRepository>();
			projectRepository = MockRepository.GenerateStub<IProjectRepository>();
			applicationController = MockRepository.GenerateStub<IApplicationController>();
			fileService = MockRepository.GenerateStub<IFileService>();
		}

		protected DataEditorPresenter CreatePresenter()
		{
			var presenter = MockRepository.GeneratePartialMock<DataEditorPresenter>(applicationController, view, fileDialogCreator, messageCreator, settingsRepositoru, projectRepository, datasetProvider, fileService);
			return presenter;

		}

	}
}
