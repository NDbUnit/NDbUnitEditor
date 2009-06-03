using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using NDbUnit.Utility;

namespace NDbUnitDataEditor
{
    public class DataEditorPresenter
    {
        private const string DATA_FILE_SETTINGS_KEY = "DataFilePath";

        private const string SCHEMA_FILE_SETTINGS_KEY = "SchemaFilePath";

        private IDataEditorView _dataEditor;

        private IMessageDialog _messageBox;

        private IUserSettings _userSettings;

        /// <summary>
        /// Initializes a new instance of the DataEditorPresenter class.
        /// </summary>
        public DataEditorPresenter(IDataEditorView dataEditor, IMessageDialog messageBox, IUserSettings userSettings)
        {
            _userSettings = userSettings;
            _messageBox = messageBox;
            _dataEditor = dataEditor;
            _dataEditor.Initialize += InitializeView;
            _dataEditor.ReloadData += ReloadData;
            _dataEditor.BrowseForDataFile += SelectDataFile;
            _dataEditor.BrowseForSchemaFile += SelectSchemaFile;
            _dataEditor.ApplicationClose += SaveSettings;
        }

        public void LoadData()
        {
            try
            {
                string schemaFileName = _dataEditor.SchemaFileName;
                string dataFileName = _dataEditor.DataFileName;
                if (System.IO.File.Exists(schemaFileName) && System.IO.File.Exists(dataFileName))
                {
                    DataSet dataSet = _dataEditor.Data;
                    dataSet.ReadXmlSchema(schemaFileName);
                    _dataEditor.BindTableTree();
                    dataSet.ReadXml(dataFileName);
                }
            }
            catch (Exception ex)
            {
                _messageBox.Show(String.Format("Unable to load data. Exception:{0}", ex));
            }
        }

        public void ReloadData()
        {
            ResetSchema();
            LoadData();
            _dataEditor.CloseAllTabs();
            _dataEditor.CreateInitialPage();
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
            string fileName = _dataEditor.SelectFile();
            if (!String.IsNullOrEmpty(fileName))
                _dataEditor.DataFileName = fileName;
        }

        public void SelectSchemaFile()
        {
            string fileName = _dataEditor.SelectFile();
            if (!String.IsNullOrEmpty(fileName))
                _dataEditor.SchemaFileName = fileName;
        }

        public void Start()
        {
            _dataEditor.Run();
        }

        private void InitializeView()
        {
            _dataEditor.SchemaFileName = _userSettings.GetSetting(SCHEMA_FILE_SETTINGS_KEY);
            _dataEditor.DataFileName = _userSettings.GetSetting(DATA_FILE_SETTINGS_KEY);
            LoadData();
            _dataEditor.CreateInitialPage();
        }

        private void SaveSettings()
        {
            _userSettings.SaveSetting(DATA_FILE_SETTINGS_KEY, _dataEditor.DataFileName);
            _userSettings.SaveSetting(SCHEMA_FILE_SETTINGS_KEY, _dataEditor.SchemaFileName);
        }

    }
}
