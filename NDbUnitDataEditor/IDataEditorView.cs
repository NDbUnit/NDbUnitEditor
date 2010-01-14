using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using NDbUnit.Utility;

namespace NDbUnitDataEditor
{
    public delegate void EditorEventHandler();

    public delegate void TableViewEventHandler(TableViewEventArguments args);

    public interface IDataEditorView
    {
        event EditorEventHandler SaveEditorSettingsAs;
        string ProjectFileName { get; set; }
        event EditorEventHandler LoadEditorSettings;
        bool DataSetHasChanges();
        void SetDataSetChanged();
        bool TabIsMarkedAsEdited(string tabName);
        void RemoveEditedMarksFromAllTabs();
        void MarkTabAsEdited(string tabName);
        event TableViewEventHandler DataViewChanged;
        void EnableSave();
        event EditorEventHandler SaveData;
        event EditorEventHandler GetDataSetFromDatabase;
        event EditorEventHandler CreateGuid;
        event EditorEventHandler ApplicationClose;
        event EditorEventHandler BrowseForSchemaFile;
        event EditorEventHandler SaveEditorSettings;
        string NewGuid { get; set; }
        string SelectFile(string initialFilename, string selectionFilter);
        event EditorEventHandler BrowseForDataFile;
        DataSet Data { get; set; }
        void BindTableTree();
        void BindDataTable(DataTable table);
        void CloseAllTabs();
        event EditorEventHandler Initialize;
        event EditorEventHandler ReloadData;
        void CreateInitialPage();
        void Run();
        string SchemaFileName { get; set; }
        string DataFileName { get; set; }
        string DatabaseConnectionString { get; set; }
        string DatabaseClientType { get; set; }
        NdbUnitEditorSettings GetEditorSettings();

    }
}
