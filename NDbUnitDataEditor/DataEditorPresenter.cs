using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using NDbUnit.Utility;
using Rhino.Commons;
using NDbUnitDataEditor.UI;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace NDbUnitDataEditor
{

    public class DataEditorPresenter
    {
        private const string DATA_FILE_SETTINGS_KEY = "DataFilePath";

        private const string DATABASE_CLIENTTYPE_SETTINGS_KEY = "DatabaseClientType";

        private const string DATABASE_CONNECTION_STRING_SETTINGS_KEY = "DatabaseConnectionString";

        private const string SCHEMA_FILE_SETTINGS_KEY = "SchemaFilePath";

        private IDataEditorView _dataEditor;

        private IDialogFactory _dialogFactory;

        private INdbUnitEditorSettingsManager _settingsManager;
        private IUserSettings _userSettings;

        /// <summary>
        /// Initializes a new instance of the DataEditorPresenter class.
        /// </summary>
        public DataEditorPresenter(IDataEditorView dataEditor, IDialogFactory dialogFactory, IUserSettings userSettings, INdbUnitEditorSettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
            _userSettings = userSettings;
            _dialogFactory = dialogFactory;
            _dataEditor = dataEditor;
            _dataEditor.Initialize += InitializeView;
            _dataEditor.ReloadData += ReloadData;
            _dataEditor.BrowseForDataFile += SelectDataFile;
            _dataEditor.BrowseForSchemaFile += SelectSchemaFile;
            _dataEditor.ApplicationClose += SaveSettings;
            _dataEditor.CreateGuid += CreateGuid;
            _dataEditor.GetDataSetFromDatabase += GetDataSetFromDatabase;
            _dataEditor.SaveData += SaveData;
            _dataEditor.DataViewChanged += HandleDataSetChange;
            _dataEditor.SaveEditorSettings += SaveEditorSettings;
            _dataEditor.SaveEditorSettingsAs += SaveEditorSettingsAs;
            _dataEditor.LoadEditorSettings+=LoadEditorSettings;

        }

        public void CreateTableTree()
        {
            IMessageDialog messageDialog = _dialogFactory.CreateMessageDialog();
            try
            {
                string schemaFileName = _dataEditor.SchemaFileName;

                if (File.Exists(schemaFileName))
                {
                    DataSet dataSet = _dataEditor.Data;
                    dataSet.ReadXmlSchema(schemaFileName);
                    _dataEditor.BindTableTree();

                    _dataEditor.EnableSave();
                }
            }
            catch (Exception ex)
            {
                messageDialog.ShowError(String.Format("Unable to create schema tree. Exception:{0}", ex));
            }
        }

        public void HandleDataSetChange(TableViewEventArguments args)
        {
            DataSet dataSet = _dataEditor.Data;
            var tabName = args.TabName;
            if (!string.IsNullOrEmpty(tabName) && _dataEditor.DataSetHasChanges())
            {
                if (_dataEditor.TabIsMarkedAsEdited(tabName))
                    return; //tab was already marked
                DataTable table = dataSet.Tables[tabName];
                var changes = table.GetChanges();
                if (changes != null)
                    _dataEditor.MarkTabAsEdited(tabName);
            }
        }

        public void ReloadData()
        {
            IMessageDialog messageDialog = _dialogFactory.CreateMessageDialog();
            try
            {
                DataSet dataSet = _dataEditor.Data;
                string dataFileName = _dataEditor.DataFileName;

                string errorMessage = ValidateInputBeforeReload(dataSet, dataFileName);
                if (errorMessage != string.Empty)
                {
                    messageDialog.ShowError(errorMessage, "Error loading DataSet");
                    return;
                }

                _dataEditor.CloseAllTabs();
                
                dataSet.Clear();
                dataSet.ReadXml(dataFileName);
            }
            catch (Exception ex)
            {
                messageDialog.ShowError(String.Format("Unspecified error occured. Exception: {0}", ex.Message));
                //TODO: add some sort of error logging here
            }

        }

        public void ResetSchema()
        {
            DataSet dataSet = _dataEditor.Data;
            dataSet.Clear();
            dataSet.Dispose();
            _dataEditor.Data = new DataSet();
        }

        public void SelectDataFile()
        {
            string fileName = _dataEditor.SelectFile(_dataEditor.DataFileName, "XML Data Files (*.xml)|*.xml");
            if (!String.IsNullOrEmpty(fileName))
                _dataEditor.DataFileName = fileName;
        }

        public void SelectSchemaFile()
        {
            string fileName = _dataEditor.SelectFile(_dataEditor.SchemaFileName, "XSD Schema Files (*.xsd)|*.xsd");
            if (!String.IsNullOrEmpty(fileName))
            {
                _dataEditor.SchemaFileName = fileName;
                ResetSchema();
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
            var presenter = IoC.Resolve<DataSetFromDatabasePresenter>();
            presenter.XsdFilePath = _dataEditor.SchemaFileName;
            presenter.XmlFilePath = _dataEditor.DataFileName;
            presenter.DatabaseConnectionString = _dataEditor.DatabaseConnectionString;
            //TODO: set database client type the same way

            presenter.Start();

            if (presenter.DataSetFromDatabaseResult == true)
            {
                _dataEditor.Data = presenter.DataSet;
                _dataEditor.SetDataSetChanged();
                _dataEditor.CloseAllTabs();
            }

            _dataEditor.DatabaseConnectionString = presenter.DatabaseConnectionString;
            //TODO: retreive selected database client type the same way
        }

        private void InitializeView()
        {
            _dataEditor.SchemaFileName = _userSettings.GetSetting(SCHEMA_FILE_SETTINGS_KEY);
            _dataEditor.DataFileName = _userSettings.GetSetting(DATA_FILE_SETTINGS_KEY);
            _dataEditor.DatabaseClientType = _userSettings.GetSetting(DATABASE_CLIENTTYPE_SETTINGS_KEY);
            _dataEditor.DatabaseConnectionString = _userSettings.GetSetting(DATABASE_CONNECTION_STRING_SETTINGS_KEY);

            CreateTableTree();
            if (!String.IsNullOrEmpty(_dataEditor.DataFileName) && _dataEditor.Data != null)
            {
                //read data if exists

                DataSet dataSet = _dataEditor.Data;
                dataSet.ReadXml(_dataEditor.DataFileName);
            }
            _dataEditor.CreateInitialPage();

        }

        void SaveData()
        {
            DataSet dataSet = _dataEditor.Data;
            IMessageDialog messageDialog = _dialogFactory.CreateMessageDialog();
            try
            {
                if (dataSet == null)
                {
                    messageDialog.ShowError("Cannot find schema. Please make sure that there is a database schema loaded.");
                    return;
                }
                string fileName = _dataEditor.DataFileName;
                if (fileName == null || fileName == "")
                {
                    IFileDialog saveDlg = _dialogFactory.CreateFileDialog(FileDialogType.SaveFileDilaog, "xml files|*.xml");
                    if (saveDlg.Show() == FileDialogResult.OK)
                        fileName = saveDlg.FileName;
                    else
                        return;
                }
                //verify if path is correct
                string path = Path.GetDirectoryName(fileName);
                if (!Directory.Exists(path))
                {
                    messageDialog.ShowError("Cannot save specified file");
                    return;
                }
                dataSet.WriteXml(fileName);
                _dataEditor.DataFileName = fileName;
                _dataEditor.RemoveEditedMarksFromAllTabs();
            }
            catch (Exception ex)
            {
                messageDialog.ShowError(String.Format("Unable to save file. Exception: {0}", ex.Message));
            }
        }

        private void SaveSettings()
        {
            _userSettings.SaveSetting(DATA_FILE_SETTINGS_KEY, _dataEditor.DataFileName);
            _userSettings.SaveSetting(SCHEMA_FILE_SETTINGS_KEY, _dataEditor.SchemaFileName);
            _userSettings.SaveSetting(DATABASE_CONNECTION_STRING_SETTINGS_KEY, _dataEditor.DatabaseConnectionString);
            _userSettings.SaveSetting(DATABASE_CLIENTTYPE_SETTINGS_KEY, _dataEditor.DatabaseClientType);
        }
        public void SaveEditorSettings()
        {
            string filePath = _dataEditor.ProjectFileName;
            if (string.IsNullOrEmpty(filePath))
            {
                var dialog = _dialogFactory.CreateFileDialog(FileDialogType.SaveFileDilaog, "XML files|*.xml");
                if (dialog.Show() != FileDialogResult.OK)
                    return;
                filePath = dialog.FileName;
                _dataEditor.ProjectFileName = filePath;   
            }            
            NdbUnitEditorSettings settings = _dataEditor.GetEditorSettings();
            _settingsManager.SaveSettings(settings, filePath);
             
        }

        public void SaveEditorSettingsAs()
        {
            var dialog = _dialogFactory.CreateFileDialog(FileDialogType.SaveFileDilaog, "XML files|*.xml");
            if (dialog.Show() != FileDialogResult.OK)
                return;
            NdbUnitEditorSettings settings = _dataEditor.GetEditorSettings();
            _settingsManager.SaveSettings(settings, dialog.FileName);
            _dataEditor.ProjectFileName = dialog.FileName;

        }

        public void LoadEditorSettings()
        {
            var dialog = _dialogFactory.CreateFileDialog(FileDialogType.OpenFileDialog, "XML files|*.xml");
            if (dialog.Show() != FileDialogResult.OK)
                return;
            NdbUnitEditorSettings settings = _settingsManager.LoadSettings(dialog.FileName);
            _dataEditor.SchemaFileName = settings.SchemaFilePath;
            _dataEditor.DataFileName = settings.XMLDataFilePath;
            _dataEditor.DatabaseConnectionString = settings.DatabaseConnectionString;
        }



        private string ValidateInputBeforeReload(DataSet dataSet, string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
            {
                return "Cannot load data. Please select data file name first";
            }
            if (dataSet == null)
            {
                return "Cannot find schema. Please make sure that there is a database schema loaded.";
            }

            if (!File.Exists(fileName))
            {
                return "Specified file does not exists.";
            }

            return string.Empty;
        }

    }
}
