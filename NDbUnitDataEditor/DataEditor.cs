using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using NDbUnit.Utility;
using System.IO;

namespace NDbUnitDataEditor
{
	public partial class DataEditor : Form, IDataEditorView
	{
		string _dataFileName = null;

		private string _newGuid;

		string _schemaFileName = null;

		public string ProjectFileName { get; set; }

		public event Action BrowseForDataFile;

		public event Action BrowseForSchemaFile;

		public event Action CreateGuid;

		public event Action<string> DataViewChanged;

		public event Action GetDataSetFromDatabase;

		public event Action Initialize;

		public event Action ReloadData;

		public event Action SaveData;

		public event Action SaveProject;

		public event Action SaveProjectAs;

		public event Action LoadProject;

		public event Action ExitApp;

		public event Action<string> TableTreeNodeDblClicked;

		public DataEditor()
		{
			InitializeComponent();
			Text += String.Format(" v{0}", Application.ProductVersion);
		}


		public string DatabaseClientType { get; set; }

		public string DatabaseConnectionString { get; set; }

		public string DataFileName
		{
			get
			{
				return _dataFileName;
			}
			set
			{
				txtDataFileName.Text = Path.GetFileName(value);
				txtDataFileName.ToolTipText = value;
				_dataFileName = value;
			}
		}


		public string FileSelectFilter
		{
			get
			{
				return openFileDialog1.Filter;
			}
			set
			{
				openFileDialog1.Filter = value;
			}
		}

		public string NewGuid
		{
			get
			{
				return _newGuid;
			}
			set
			{
				_newGuid = value;
			}
		}

		public string SchemaFileName
		{
			get
			{
				return _schemaFileName;
			}

			set
			{
				txtSchemaFileName.Text = Path.GetFileName(value);
				txtSchemaFileName.ToolTipText = value;
				_schemaFileName = value;
			}
		}

		public void CloseApplication()
		{
			this.Close();
		}

		public void BindDataTable(DataTable table)
		{
			if (table != null)
			{
				TabPage page = tbTableViews.SelectedTab;
				TableView tbView = GetTabView(page);
				tbView.DataSource = null;
				tbView.DataSource = table;
			}
		}

		public void BindTableTree(string rootNodeName, IEnumerable<string> tableNames)
		{
			treeView1.Nodes.Clear();
			TreeNode root = treeView1.Nodes.Add("dataset", rootNodeName);
			foreach (var table in tableNames)
			{
				string key = String.Format("Table-{0}", table);
				root.Nodes.Add(key, table);
			}
			treeView1.ExpandAll();
			treeView1.Sort();
		}

		public void CloseAllTabs()
		{
			tbTableViews.TabPages.Clear();
		}

		public void EnableSave()
		{
			btnSaveData.Enabled = true;
		}

		public void MarkTabAsEdited(string tabName)
		{
			var selectedTab = tbTableViews.TabPages[tabName];
			string title = selectedTab.Text;
			selectedTab.Text = string.Format("{0} *", title);
		}

		public void RemoveEditedMarksFromAllTabs()
		{
			if (tbTableViews.TabCount == 0)
				return;

			foreach (TabPage tab in tbTableViews.TabPages)
			{
				string title = tab.Text;
				if (title.EndsWith(" *"))
				{
					title = title.Remove(title.LastIndexOf(" *"));
				}
				tab.Text = title;
			}
		}

		public void Run()
		{
			Application.Run(this);
		}


		public bool TabIsMarkedAsEdited(string tabName)
		{
			var selectedTab = tbTableViews.TabPages[tabName];
			string title = selectedTab.Text;
			if (title.EndsWith("*"))
				return true;
			return false;
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == (Keys.Control | Keys.S))
				SaveData();
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private TabPage AddTabPage(string tabName)
		{
			tbTableViews.TabPages.Add(tabName, tabName);
			TabPage page = GetTabPage(tabName);
			//page.ContextMenuStrip = contextMenuStrip1;
			TableView view = new TableView(tabName) { Dock = DockStyle.Fill };
			view.TableViewChanged += TabPageEdited;
			page.Controls.Add(view);

			return page;
		}

		private void btnAbout_Click(object sender, EventArgs e)
		{
			using (Form about = new About())
			{
				about.ShowDialog();
			}
		}

		private void btnBrowseDataFile_Click(object sender, EventArgs e)
		{
			BrowseForDataFile();
		}

		private void btnBrowseSchemaFile_Click(object sender, EventArgs e)
		{
			BrowseForSchemaFile();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			ExitApp();
		}

		private void btnCloseTab_Click(object sender, EventArgs e)
		{
			CloseSelectedTab();
		}

		private void btnDataSetFromDatabase_Click(object sender, EventArgs e)
		{
			GetDataSetFromDatabase();
		}

		private void btnNewGuid_Click(object sender, EventArgs e)
		{
			CreateGuid();
			DataGridView theGrid = (tbTableViews.SelectedTab.Controls.Find("dataGridView1", true))[0] as DataGridView;

			if (theGrid != null)
			{
				if (theGrid.CurrentCell.IsInEditMode)
					theGrid.EndEdit();

				theGrid.CurrentCell.Value = this.NewGuid;
			}
		}

		private void btnReload_Click(object sender, EventArgs e)
		{
			ReloadData();
		}

		private void btnSaveData_Click(object sender, EventArgs e)
		{
			SaveData();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			try
			{
				Initialize();
			}
			catch (Exception ex)
			{
				MessageBox.Show(String.Format("Unable to load data. Exception:{0}", ex));
			}
		}

		private TabPage GetTabPage(string name)
		{
			TabPage found = null;
			foreach (TabPage page in tbTableViews.TabPages)
				if (page.Text == name)
					found = page;
			return found;
		}

		private TableView GetTabView(TabPage tabPage)
		{
			foreach (Control ctrl in tabPage.Controls)
				if (ctrl is TableView)
					return ctrl as TableView;
			return null;
		}

		public void OpenTableView(DataTable table)
		{
			//search for existing opentable
			var tabName = table.TableName;
			TabPage page = GetTabPage(table.TableName);
			if (page == null)
				page = AddTabPage(tabName);
			tbTableViews.SelectedTab = page;
			BindDataTable(table);
			btnNewGuid.Enabled = true;
		}

		private void TabPageEdited(string tabName)
		{
			DataViewChanged(tabName);
		}

		private void tbTableViews_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				this.contextMenuStrip1.Show(this.tbTableViews, e.Location);
				var tabPage = FindTabPageByClickPoint(e.Location);
				if (tabPage != null)
					tbTableViews.SelectedTab = tabPage;
			}
		}

		private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			TreeNode node = e.Node;
			if (node == null)
				return;
			string tbName = node.Text;
			TableTreeNodeDblClicked(tbName);

		}

		private void txtSchemaFileName_TextChanged(object sender, EventArgs e)
		{
			if (SchemaFileName != "")
			{
				btnReload.Enabled = true;
			}
		}

		private void closeActiveTabToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CloseSelectedTab();
		}



		private void CloseSelectedTab()
		{
			if (tbTableViews.SelectedTab != null)
				tbTableViews.TabPages.Remove(tbTableViews.SelectedTab);
		}

		private void closeAllButThisToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (tbTableViews.TabPages == null || tbTableViews.TabPages.Count == 0)
				return;
			if (tbTableViews.TabPages.Count == 1) return;
			var tabsToRemove = new List<TabPage>();
			foreach (TabPage tab in tbTableViews.TabPages)
				if (tab != tbTableViews.SelectedTab)
					tabsToRemove.Add(tab);
			foreach (TabPage tab in tabsToRemove)
				tbTableViews.TabPages.Remove(tab);
		}

		private void closeAllTabsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (tbTableViews.TabPages == null || tbTableViews.TabPages.Count == 0)
				return;
			CloseAllTabs();
		}

		private TabPage FindTabPageByClickPoint(Point point)
		{
			if (tbTableViews.TabPages == null || tbTableViews.TabPages.Count == 0)
				return null;
			for (int i = 0; i < tbTableViews.TabPages.Count; i++)
			{

				if (tbTableViews.GetTabRect(i).Contains(point))
					return tbTableViews.TabPages[i];
			}
			return null;
		}

		private void loadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LoadProject();
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveProject();
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveProjectAs();
		}

		public NdbUnitEditorProject GetEditorSettings()
		{
			NdbUnitEditorProject settings = new NdbUnitEditorProject
			{
				XMLDataFilePath = DataFileName,
				SchemaFilePath = SchemaFileName,
				DatabaseClientType = DatabaseClientType,
				DatabaseConnectionString = DatabaseConnectionString
			};
			return settings;
		}



	}
}
