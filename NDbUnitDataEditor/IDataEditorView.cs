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
		event Action NewData;
		bool HasOpenedTables { get; }
		event Action TableClosed;
		bool NewGuidEnabled { get; set; }
		bool SaveEnabled { get; set; }
		bool DataSetFromDatabaseEnabled { get; set; }
		bool ReloadEnabled { get; set; }
		event Action DataFileChanged;
		event Action SchemaFileChanged;
		event Action SaveDataAs;
		void ClearTableTree();
		event Action NewProject;
		string StatusLabel { get; set; }
		event Action<string> TabSelected;
		List<string> OpenedTabNames { get; }
        void OpenTableView(DataTable table);
		event Action<string> TableTreeNodeDblClicked;
		void CloseApplication();
		event Action ExitApp;
		event Action SaveProjectAs;
		string ProjectFileName { get; set; }
		event Action LoadProject;
		bool TabIsMarkedAsEdited(string tabName);
		void RemoveEditedMarksFromAllTabs();
		void MarkTabAsEdited(string tabName);
		event Action<string> DataViewChanged;
		event Action SaveData;
		event Action GetDataSetFromDatabase;
		event Action CreateGuid;
		event Action BrowseForSchemaFile;
		event Action SaveProject;
		string NewGuid { get; set; }
		event Action BrowseForDataFile;
		void BindTableTree(string rootNodeName, IEnumerable<string> tableNames);
		void BindDataTable(DataTable table);
		void CloseAllTabs();
		event Action Initialize;
		event Action ReloadData;
		void Run();
		string SchemaFileName { get; set; }
		string DataFileName { get; set; }
		string DatabaseConnectionString { get; set; }
		string DatabaseClientType { get; set; }

	}
}
