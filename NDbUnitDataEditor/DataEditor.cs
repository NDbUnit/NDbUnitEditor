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
    public partial class DataEditor : Form, IDataEditorView
    {
        string _dataFileName = null;

        private DataSet _dataSet = null;

        private bool _dataSetLoadedFromDatabase;

        private string _newGuid;

        string _schemaFileName = null;

        public event EditorEventHandler ApplicationClose;

        public event EditorEventHandler BrowseForDataFile;

        public event EditorEventHandler BrowseForSchemaFile;

        public event EditorEventHandler CreateGuid;

        public event TableViewEventHandler DataViewChanged;

        public event EditorEventHandler GetDataSetFromDatabase;

        public event EditorEventHandler Initialize;

        public event EditorEventHandler ReloadData;

        public event EditorEventHandler SaveData;

        public DataEditor()
        {
            InitializeComponent();
            _dataSet = new DataSet();
            Text += String.Format(" v{0}", Application.ProductVersion);
        }

        public DataSet Data
        {
            get
            {
                return _dataSet;
            }
            set
            {
                _dataSet = value;
            }
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
                txtDataFileName.Text = System.IO.Path.GetFileName(value);
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
                txtSchemaFileName.Text = System.IO.Path.GetFileName(value);
                txtSchemaFileName.ToolTipText = value;
                _schemaFileName = value;
            }
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

        public void BindTableTree()
        {
            treeView1.Nodes.Clear();

            TreeNode root = treeView1.Nodes.Add("dataset", _dataSet.DataSetName);
            if (_dataSet.Tables != null && _dataSet.Tables.Count > 0)
            {
                foreach (DataTable table in _dataSet.Tables)
                {
                    string key = String.Format("Table-{0}", table.TableName);
                    root.Nodes.Add(key, table.TableName);
                }
                treeView1.ExpandAll();
                treeView1.Sort();
            }
        }

        public void CloseAllTabs()
        {
            tbTableViews.TabPages.Clear();
        }

        public void CreateInitialPage()
        {
            if (_dataSet.Tables == null) return;

            if (_dataSet.Tables.Count > 0)
            {
                DataTable table = _dataSet.Tables[0];
                AddTabPage(table.TableName);
                BindDataTable(table);
            }
        }

        public bool DataSetHasChanges()
        {
            return (_dataSetLoadedFromDatabase || _dataSet.HasChanges());
        }

        public void EnableSaveButton()
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

        public string SelectFile(string initialFilename, string selectionFilter)
        {
            openFileDialog1.FileName = initialFilename;
            openFileDialog1.Filter = selectionFilter;

            string selectedFileName;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                selectedFileName = openFileDialog1.FileName;
            else
                selectedFileName = String.Empty;
            return selectedFileName;
        }

        public void SetDataSetChanged()
        {
            _dataSetLoadedFromDatabase = true;
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
            view.TableViewChanged += new TableViewEventHandler(TabPageEdited);
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
            if (DataSetHasChanges() && MessageBox.Show("Do you want to save changes before closing?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                _dataSet.WriteXml(DataFileName);
            Close();
            ApplicationClose();
        }

        private void btnCloseTab_Click(object sender, EventArgs e)
        {
            if (tbTableViews.SelectedTab != null)
                tbTableViews.TabPages.Remove(tbTableViews.SelectedTab);
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

        private void OpenTab(string tbName)
        {

            //search for existing opentable
            TabPage page = GetTabPage(tbName);
            if (page == null)
                page = AddTabPage(tbName);
            tbTableViews.SelectedTab = page;

            foreach (DataTable table in _dataSet.Tables)
                if (tbName == table.TableName)
                    BindDataTable(table);
        }

        private void TabPageEdited(TableViewEventArguments args)
        {
            DataViewChanged(args);
        }

        private void tbTableViews_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip1.Show(this.tbTableViews, e.Location);
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            if (node == null)
                return;
            string tbName = node.Text;
            OpenTab(tbName);

        }

        private void txtSchemaFileName_TextChanged(object sender, EventArgs e)
        {
            if (SchemaFileName != "")
            {
                btnReload.Enabled = true;
            }
        }

    }
}
