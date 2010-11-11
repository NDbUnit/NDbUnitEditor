using System;
using NDbUnit.Utility;
using Rhino.Commons;
using NDbUnitDataEditor.UI;
using System.IO;
using NDbUnitDataEditor.Commands;

namespace NDbUnitDataEditor
{

	public class DataEditorPresenter
	{

		private IDataSetProvider _datasetProvider;
		private IApplicationController _applicationController;
        private const string DEFAULT_PROJECT_FILE_NAME = "DefaultProject.xml";

		private IFileDialogCreator _fileDialogCreator;
		private IMessageCreator _messageCreator;
        private const string RECENT_PROJECT_FILE_KEY = "RecentProjectFileName";

		private IDataEditorView _dataEditor;

		private IProjectRepository _projectRepository;
		private IUserSettingsRepository _userSettingsRepository;

		/// <summary>
		/// Initializes a new instance of the DataEditorPresenter class.
		/// </summary>
		public DataEditorPresenter(IApplicationController applicationController, IDataEditorView dataEditor, IFileDialogCreator fileDialogCreator, IMessageCreator messageCreator, IUserSettingsRepository userSettingsRepository, IProjectRepository projectRepository, IDataSetProvider datasetProvider)
		{
			_messageCreator = messageCreator;
            _fileDialogCreator = fileDialogCreator;
            _applicationController = applicationController;
            _datasetProvider = datasetProvider;
			_projectRepository = projectRepository;
			_userSettingsRepository = userSettingsRepository;
			_dataEditor = dataEditor;


			_dataEditor.Initialize += OnInitializeView;
			_dataEditor.ReloadData += ReloadData;
			_dataEditor.BrowseForDataFile += SelectDataFile;
			_dataEditor.BrowseForSchemaFile += SelectSchemaFile;
			_dataEditor.CreateGuid += CreateGuid;
			_dataEditor.GetDataSetFromDatabase += GetDataSetFromDatabase;
			_dataEditor.SaveData += SaveData;
			_dataEditor.DataViewChanged += HandleDataSetChange;
			_dataEditor.SaveProject += SaveEditorSettings;
			_dataEditor.SaveProjectAs += SaveEditorSettingsAs;
			_dataEditor.LoadProject += LoadEditorSettings;
			_dataEditor.ExitApp += OnExitingApplication;
			_dataEditor.TableTreeNodeDblClicked += OnOpenTable;

			_applicationController.Subscribe<ReinitializeMainViewRequested>((e) => ReInitializeView());
		}

		private void OnOpenTable(string tableName)
		{
			var table = _datasetProvider.GetTable(tableName);
			if (table == null)
				return;
			_dataEditor.OpenTableView(table);
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

		public void CreateTableTree()
		{
			try
			{
				string schemaFileName = _dataEditor.SchemaFileName;

				if (File.Exists(schemaFileName))
				{
					_datasetProvider.ReadSchemaFromFile(schemaFileName);

					_dataEditor.BindTableTree(_datasetProvider.DataSetName, _datasetProvider.GetTableNames());

					_dataEditor.EnableSave();
				}
			}
			catch (Exception ex)
			{
				_messageCreator.ShowError(String.Format("Unable to create schema tree. Exception:{0}", ex));
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


		public void ReloadData()
		{
			_applicationController.ExecuteCommand<ReloadDataCommand>();
		}

		public void SelectDataFile()
		{
			var dialogResult = _fileDialogCreator.ShowFileOpen("XML Data Files (*.xml)|*.xml");
			if (dialogResult.Accepted)
				_dataEditor.DataFileName = dialogResult.SelectedFileName;
		}

		public void SelectSchemaFile()
		{
			var dialogResult = _fileDialogCreator.ShowFileOpen("XSD Schema Files (*.xsd)|*.xsd");
			if (dialogResult.Accepted)
			{
				_dataEditor.SchemaFileName = dialogResult.SelectedFileName;
				_datasetProvider.ResetSchema();
				CreateTableTree();
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

		private void ReInitializeView()
		{
			CreateTableTree();

			if (!String.IsNullOrEmpty(_dataEditor.DataFileName))
			{
				//read data if exists
				_datasetProvider.ReadDataFromFile(_dataEditor.DataFileName);
			}
			OpenFirstTable();
		}

		private void OpenFirstTable()
		{
			var table = _datasetProvider.GetFirstTable();
			if (table == null)
				return;
			_dataEditor.OpenTableView(table);
		}

		private void OnInitializeView()
		{
			var projectFileName = _userSettingsRepository.GetSetting(RECENT_PROJECT_FILE_KEY);
			if (File.Exists(projectFileName))
				LoadEditorSettings(projectFileName);

			PopulateWithData();            
		}

		private void PopulateWithData()
		{
			CreateTableTree();

			if (!String.IsNullOrEmpty(_dataEditor.DataFileName))
			{
				//read data if exists
				_datasetProvider.ReadDataFromFile(_dataEditor.DataFileName);
			}
			OpenFirstTable();
		}

		void SaveData()
		{
			_applicationController.ExecuteCommand<SaveDataCommand>();
		}

		private void SaveSettings()
		{
			var projectFile = _dataEditor.ProjectFileName;
			if (String.IsNullOrEmpty(projectFile))
				projectFile = DEFAULT_PROJECT_FILE_NAME;
			NdbUnitEditorProject settings = GetEditorSettings();
			_projectRepository.SaveProject(settings, projectFile);
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

			NdbUnitEditorProject settings = GetEditorSettings();
			_projectRepository.SaveProject(settings, filePath);
								
		}

		public void SaveEditorSettingsAs()
		{
			var dialogResult = _fileDialogCreator.ShowFileSave("XML files|*.xml");

			if (!dialogResult.Accepted)
				return;
			NdbUnitEditorProject settings = GetEditorSettings();
			_projectRepository.SaveProject(settings, dialogResult.SelectedFileName);
			_dataEditor.ProjectFileName = dialogResult.SelectedFileName;
		}

		public void LoadEditorSettings()
		{
			var dialogResult = _fileDialogCreator.ShowFileOpen("XML files|*.xml");
			if (!dialogResult.Accepted)
				return;
			LoadEditorSettings(dialogResult.SelectedFileName);
			PopulateWithData();
		}

		public void LoadEditorSettings(string fileName)
		{
			try
			{
				NdbUnitEditorProject settings = _projectRepository.LoadProject(fileName);
				_dataEditor.SchemaFileName = settings.SchemaFilePath;
				_dataEditor.DataFileName = settings.XMLDataFilePath;
				_dataEditor.DatabaseConnectionString = settings.DatabaseConnectionString;
				_dataEditor.DatabaseClientType = settings.DatabaseClientType;
				_dataEditor.ProjectFileName = fileName;
			}
			catch (Exception ex)
			{
				_messageCreator.ShowError(String.Format("Unable to load project. Exception: {0}", ex.Message));
			}
		}

		public virtual NdbUnitEditorProject GetEditorSettings()
		{
			NdbUnitEditorProject settings = new NdbUnitEditorProject
			{
				XMLDataFilePath = _dataEditor.DataFileName,
				SchemaFilePath = _dataEditor.SchemaFileName,
				DatabaseClientType = _dataEditor.DatabaseClientType,
				DatabaseConnectionString = _dataEditor.DatabaseConnectionString
			};
			return settings;
		}

	}
}
