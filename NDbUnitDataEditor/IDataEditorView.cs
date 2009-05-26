using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace NDbUnitDataEditor
{
    public delegate void EditorEventHandler();

    public interface IDataEditorView
    {
        event EditorEventHandler BrowseForSchemaFile;
        string SelectFile();
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
    }
}
