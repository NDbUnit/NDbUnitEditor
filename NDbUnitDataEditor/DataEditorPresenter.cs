using System;
using NDbUnit.Utility;
using NDbUnitDataEditor.Commands;
using System.IO;
using NDbUnitDataEditor.Events;

namespace NDbUnitDataEditor
{

	public class DataEditorPresenter
	{

		private IDataSetProvider _datasetProvider;
		private IApplicationController _applicationController;
        private const string DEFAULT_PROJECT_FILE_NAME = "DefaultProject.xml";

		private IFileDialogCreator _fileDialogCreator;
		private IMessageCreator _messageCreator;
		private IFileService _fileService;
        public const string RECENT_PROJECT_FILE_KEY = "RecentProjectFileName";

		private IDataEditorView _dataEditor;

		private IProjectRepository _projectRepository;
		private IUserSettingsRepository _userSettingsRepository;

		/// <summary>
		/// Initializes a new instance of the DataEditorPresenter class.
		/// </summary>
		public DataEditorPresenter(IApplicationController applicationController, 
			IDataEditorView dataEditor, 
			IFileDialogCreator fileDialogCreator, 
			IMessageCreator messageCreator, 
			IUserSettingsRepository userSettingsRepository, 
			IProjectRepository projectRepository, 
			IDataSetProvider datasetProvider,
			IFileService fileService)
		{
			_fileService = fileService;
            _messageCreator = messageCreator;
            _fileDialogCreator = fileDialogCreator;
            _applicationController = applicationController;
            _datasetProvider = datasetProvider;
			_projectRepository = projectRepository;
			_userSettingsRepository = userSettingsRepository;
			_dataEditor = dataEditor;
			_dataEditor.Initialize += OnInitializeView;
			_dataEditor.ReloadData += () => _applicationController.ExecuteCommand<ReloadDataCommand>();
			_dataEditor.BrowseForDataFile += SelectDataFile;
			_dataEditor.BrowseForSchemaFile += SelectSchemaFile;
			_dataEditor.CreateGuid += CreateGuid;
			_dataEditor.GetDataSetFromDatabase += GetDataSetFromDatabase;
			_dataEditor.SaveData += OnSaveData;
			_dataEditor.SaveDataAs += OnSaveDataAs;
			_dataEditor.DataViewChanged += HandleDataSetChange;
			_dataEditor.SaveProject += SaveEditorSettings;
			_dataEditor.SaveProjectAs += SaveEditorSettingsAs;
			_dataEditor.LoadProject += LoadEditorSettings;
			_dataEditor.NewProject += new Action(OnNewProject);
			_dataEditor.ExitApp += OnExitingApplication;
			_dataEditor.TableTreeNodeDblClicked += OnOpenTable;
			_dataEditor.TabSelected+=OnTabSelected;
			_dataEditor.DataFileChanged += OnDataFileChanged;
			_dataEditor.SchemaFileChanged += OnSchemaFileChanged;
			_applicationController.Subscribe<ReinitializeMainViewRequested>((e) =>OnReinitializeMainView());
		}

		private void OnDataFileChanged()
		{
			_dataEditor.IsReloadEnabled = ShouldEnableReload();
		}

		private void OnSchemaFileChanged()
		{
			_dataEditor.IsReloadEnabled = ShouldEnableReload();
		}

		private bool ShouldEnableReload()
		{
			if (!String.IsNullOrEmpty(_dataEditor.DataFileName) && !String.IsNullOrEmpty(_dataEditor.SchemaFileName))
				return true;
			return false;
		}

		private void OnOpenTable(string tableName)
		{
			var table = _datasetProvider.GetTable(tableName);
			if (table == null)
				return;
			_dataEditor.OpenTableView(table);
		}

		private void OnTabSelected(string tableName)
		{
			var statusText="";
			var tableInfo = _datasetProvider.GetTableInfo(tableName);
			if (tableInfo != null)
				statusText = String.Format("Rows: {0}",tableInfo.NumberOfRows);
			_dataEditor.StatusLabel = statusText;
		}

		public void OnExitingApplication()
		{
			if (_datasetProvider.IsDirty())
			{
				if (_messageCreator.AskUser("Do you want to save changes before closing?"))
					_datasetProvider.SaveDataToFile(_dataEditor.DataFileName);
			}
			SaveSettings();
			_dataEditor.CloseApplication();
		}

		private void OnReinitializeMainView()
		{
			try
			{
				_applicationController.ExecuteCommand<ReloadSchemaCommand>();
				_applicationController.ExecuteCommand<ReloadDataCommand>();
			}
			catch (ReloadDataCommandException ex)
			{
				_messageCreator.ShowError(ex.Message);
			}
			catch (ReloadSchemaCommandException ex)
			{
				_messageCreator.ShowError(ex.Message);
			}
			catch (Exception ex)
			{
				_messageCreator.ShowError(String.Format("Unable to reinitialize main view. Exception: {0}",ex.ToString()));
			}
		}

		public void HandleDataSetChange(string tabName)
		{
			if (!string.IsNullOrEmpty(tabName) && _datasetProvider.IsDirty())
			{
				if (_dataEditor.TabIsMarkedAsEdited(tabName))
					return; //tab was already marked
				if (_datasetProvider.HasTableChanged(tabName))
					_dataEditor.MarkTabAsEdited(tabName);
			}
		}

		public void SelectDataFile()
		{
			var dialogResult = _fileDialogCreator.ShowFileOpen("XML Data Files (*.xml)|*.xml");
			if (dialogResult.Accepted)
				SetDataFileName(dialogResult.SelectedFileName);
		}

		public void SelectSchemaFile()
		{
			var dialogResult = _fileDialogCreator.ShowFileOpen("XSD Schema Files (*.xsd)|*.xsd");
			if (dialogResult.Accepted)
			{
				_dataEditor.SchemaFileName = dialogResult.SelectedFileName;
				_datasetProvider.ResetSchema();
				_applicationController.ExecuteCommand<ReloadSchemaCommand>();
			}
		}

		public void Start()
		{
			_dataEditor.Run();
		}

		private void CreateGuid()
		{
			_dataEditor.NewGuid = Guid.NewGuid().ToString("D");
		}

		private void GetDataSetFromDatabase()
		{
			
			_applicationController.ExecuteCommand<GetDataFromDatabaseCommand>();
		}

		private void SetDataFileName(string dataFileName)
		{
			_dataEditor.DataFileName = dataFileName;
			if (String.IsNullOrEmpty(dataFileName))
				_dataEditor.DisableDataSetFromDatabaseButton();
			else
				_dataEditor.EnableDataSetFromDatabaseButton();
		}

		void OnNewProject()
		{
			_dataEditor.SchemaFileName = "";
			_dataEditor.DataFileName = "";
			_dataEditor.DatabaseConnectionString = "";
			_dataEditor.DatabaseClientType = "";
			_dataEditor.CloseAllTabs();
			_datasetProvider.CreateNewDataset();
			_dataEditor.ClearTableTree();
		}

		public void OpenProject(string fileName)
		{
			try
			{
				NdbUnitEditorProject project = _projectRepository.LoadProject(fileName);
				_dataEditor.SchemaFileName = project.SchemaFilePath;
				SetDataFileName(project.XMLDataFilePath);
				_dataEditor.DatabaseConnectionString = project.DatabaseConnectionString;
				_dataEditor.DatabaseClientType = project.DatabaseClientType;
				_dataEditor.ProjectFileName = fileName;
				if (!CanReloadSchema())
					return;
				_applicationController.ExecuteCommand<ReloadSchemaCommand>();
				if (CanReloadData())
					_applicationController.ExecuteCommand<ReloadDataCommand>();
				foreach (var tabName in project.OpenedTabs)
					OnOpenTable(tabName);

			}
			catch (ReloadDataCommandException ex)
			{
				_messageCreator.ShowError(ex.Message);
			}
			catch (ReloadSchemaCommandException ex)
			{
				_messageCreator.ShowError(ex.Message);
			}
			catch (Exception ex)
			{
				_messageCreator.ShowError(String.Format("Unable to load project. Error: {0}", ex.Message));
			}
		}

		private bool CanReloadSchema()
		{
			return !String.IsNullOrEmpty(_dataEditor.SchemaFileName);
		}

		private bool CanReloadData()
		{
			return CanReloadSchema() && !String.IsNullOrEmpty(_dataEditor.DataFileName);
		}

		

		public void OnInitializeView()
		{
			var projectFileName = _userSettingsRepository.GetSetting(RECENT_PROJECT_FILE_KEY);
			if (_fileService.FileExists(projectFileName))
				OpenProject(projectFileName);            
		}


		private string GetSaveAsFileName()
		{
			var dlgResult = _fileDialogCreator.ShowFileSave("xml files|*.xml");
			if (dlgResult.Accepted)
				return dlgResult.SelectedFileName;
			return null;
		}

		private void OnSaveDataAs()
		{
			var newFileName = GetSaveAsFileName();
			SaveData(newFileName);
			_dataEditor.DataFileName = newFileName;
		}


		void OnSaveData()
		{
			string fileName = _dataEditor.DataFileName;
			if (String.IsNullOrEmpty(fileName))
			{
				fileName = GetSaveAsFileName();
				_dataEditor.DataFileName = fileName;
			}
			SaveData(fileName);
		}

		private void SaveData(string fileName)
		{
			try
			{
				string path = Path.GetDirectoryName(fileName);
				if (!Directory.Exists(path))
				{
					_messageCreator.ShowError("Cannot save specified file");
					return;
				}
				_datasetProvider.SaveDataToFile(fileName);
				_dataEditor.DataFileName = fileName;
				_dataEditor.RemoveEditedMarksFromAllTabs();
			}
			catch (Exception ex)
			{
				_messageCreator.ShowError(String.Format("Unable to save file. Exception: {0}", ex.Message));
			}
		}

		private void SaveSettings()
		{
			var projectFile = _dataEditor.ProjectFileName;
			if (String.IsNullOrEmpty(projectFile))
				projectFile = DEFAULT_PROJECT_FILE_NAME;
			NdbUnitEditorProject project = GetProjectData();
			_projectRepository.SaveProject(project, projectFile);
			_userSettingsRepository.SaveSetting(RECENT_PROJECT_FILE_KEY, projectFile);
		}

		public void SaveEditorSettings()
		{
			string filePath = _dataEditor.ProjectFileName;
			if (string.IsNullOrEmpty(filePath))
			{
				SaveEditorSettingsAs();
				return;    
			}

			NdbUnitEditorProject settings = GetProjectData();
			_projectRepository.SaveProject(settings, filePath);
								
		}

		public void SaveEditorSettingsAs()
		{
			var dialogResult = _fileDialogCreator.ShowFileSave("XML files|*.xml");

			if (!dialogResult.Accepted)
				return;
			NdbUnitEditorProject settings = GetProjectData();
			_projectRepository.SaveProject(settings, dialogResult.SelectedFileName);
			_dataEditor.ProjectFileName = dialogResult.SelectedFileName;
		}

		public void LoadEditorSettings()
		{
			var dialogResult = _fileDialogCreator.ShowFileOpen("XML files|*.xml");
			if (!dialogResult.Accepted)
				return;
			OpenProject(dialogResult.SelectedFileName);
		}



		public virtual NdbUnitEditorProject GetProjectData()
		{
			NdbUnitEditorProject settings = new NdbUnitEditorProject
			{
				XMLDataFilePath = _dataEditor.DataFileName,
				SchemaFilePath = _dataEditor.SchemaFileName,
				DatabaseClientType = _dataEditor.DatabaseClientType,
				DatabaseConnectionString = _dataEditor.DatabaseConnectionString,
				OpenedTabs=_dataEditor.OpenedTabNames
			};
			return settings;
		}
	}
}
