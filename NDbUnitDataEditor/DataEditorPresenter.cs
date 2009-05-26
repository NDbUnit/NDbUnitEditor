using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;

namespace NDbUnitDataEditor
{
    public class DataEditorPresenter
    {
        private IMessageDialog _messageBox;
        private IDataEditorView _dataEditor;
        /// <summary>
        /// Initializes a new instance of the DataEditorPresenter class.
        /// </summary>
        public DataEditorPresenter(IDataEditorView dataEditor, IMessageDialog messageBox)
        {
            _messageBox = messageBox;
            _dataEditor = dataEditor;
            _dataEditor.Initialize +=new EditorEventHandler(InitializeView);
            _dataEditor.ReloadData+=new EditorEventHandler(ReloadData);
            _dataEditor.BrowseForDataFile+=new EditorEventHandler(SelectDataFile);
            _dataEditor.BrowseForSchemaFile+=new EditorEventHandler(SelectSchemaFile);
        }

        public void SelectDataFile()
        {
            string fileName =_dataEditor.SelectFile();
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
            _dataEditor.SchemaFileName = ConfigurationManager.AppSettings["SchemaFilePath"];
            _dataEditor.DataFileName = ConfigurationManager.AppSettings["DataFilePath"];
            LoadData();
            _dataEditor.CreateInitialPage();
        }

        public void ReloadData()
        {
            ResetSchema();
            LoadData();
            _dataEditor.CloseAllTabs();
            _dataEditor.CreateInitialPage();
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

        public void ResetSchema()
        {
            DataSet dataSet = _dataEditor.Data;
            dataSet.Clear();
            dataSet.Dispose();
            _dataEditor.Data = new DataSet();
        }
    }
}
