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
    public interface IDataEditorView
    {
        event Action SaveEditorSettingsAs;
        string ProjectFileName { get; set; }
        event Action LoadEditorSettings;
        bool DataSetHasChanges();
        void SetDataSetChanged();
        bool TabIsMarkedAsEdited(string tabName);
        void RemoveEditedMarksFromAllTabs();
        void MarkTabAsEdited(string tabName);
        event Action<string> DataViewChanged;
        void EnableSave();
        event Action SaveData;
        event Action GetDataSetFromDatabase;
        event Action CreateGuid;
        event Action ApplicationClose;
        event Action BrowseForSchemaFile;
        event Action SaveEditorSettings;
        string NewGuid { get; set; }
        string SelectFile(string initialFilename, string selectionFilter);
        event Action BrowseForDataFile;
        DataSet Data {set; }
        void BindTableTree();
        void BindDataTable(DataTable table);
        void CloseAllTabs();
        event Action Initialize;
        event Action ReloadData;
        void CreateInitialPage();
        void Run();
        string SchemaFileName { get; set; }
        string DataFileName { get; set; }
        string DatabaseConnectionString { get; set; }
        string DatabaseClientType { get; set; }
        NdbUnitEditorSettings GetEditorSettings();

    }
}
