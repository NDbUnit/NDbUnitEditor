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

        private string _newGuid;

        string _schemaFileName = null;

        DataSet dataSet1 = null;

        public event EditorEventHandler ApplicationClose;

        public event EditorEventHandler BrowseForDataFile;

        public event EditorEventHandler BrowseForSchemaFile;

        public event EditorEventHandler CreateGuid;

        public event EditorEventHandler GetDataSetFromDatabase;

        public event EditorEventHandler Initialize;

        public event EditorEventHandler ReloadData;

        public event EditorEventHandler SaveData;

        public event TableViewEventHandler DataViewChanged;

        public DataEditor()
        {
            InitializeComponent();
            dataSet1 = new DataSet();
            Text += String.Format(" v{0}", Application.ProductVersion);
        }

        public DataSet Data
        {
            get
            {
                return dataSet1;
            }
            set
            {
                dataSet1 = value;
            }
        }

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

            TreeNode root = treeView1.Nodes.Add("dataset", dataSet1.DataSetName);
            if (dataSet1.Tables != null && dataSet1.Tables.Count > 0)
            {
                foreach (DataTable table in dataSet1.Tables)
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
            if (dataSet1.Tables != null && dataSet1.Tables.Count > 0)
            {
                DataTable table = dataSet1.Tables[0];
                AddTabPage(table.TableName);
                BindDataTable(table);
            }
        }

        public void Run()
        {
            Application.Run(this);
        }

        public string SelectFile()
        {
            string selectedFileName;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                selectedFileName = openFileDialog1.FileName;
            else
                selectedFileName = String.Empty;
            return selectedFileName;
        }


        private TabPage AddTabPage(string tabName)
        {
            tbTableViews.TabPages.Add(tabName);
            TabPage page = GetTabPage(tabName);

            TableView view = new TableView(tabName) { Dock = DockStyle.Fill };
            view.TableViewChanged+=new TableViewEventHandler(TabPageEdited);
            page.Controls.Add(view);

            return page;
        }

        private void TabPageEdited(TableViewEventArguments args)
        {
            DataViewChanged(args);
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
            if (dataSet1.HasChanges() && MessageBox.Show("Do you want to save changes before closing?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                dataSet1.WriteXml(DataFileName);
            Close();
            ApplicationClose();
        }

        private void btnCloseTab_Click(object sender, EventArgs e)
        {
            if (tbTableViews.SelectedTab != null)
                tbTableViews.TabPages.Remove(tbTableViews.SelectedTab);
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

            foreach (DataTable table in dataSet1.Tables)
                if (tbName == table.TableName)
                    BindDataTable(table);
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            if (node == null)
                return;
            string tbName = node.Text;
            OpenTab(tbName);

        }

        private void btnDataSetFromDatabase_Click(object sender, EventArgs e)
        {
            GetDataSetFromDatabase();
        }

        private void txtSchemaFileName_TextChanged(object sender, EventArgs e)
        {
            if (SchemaFileName != "")
            {
                btnReload.Enabled = true;
            }
        }

        public void EnableSaveButton()
        {
            btnSaveData.Enabled = true;
        }



        public void MarkTabAsEdited(string tabName)
        {
            var selectedTab = tbTableViews.TabPages[tabName];
            string title = selectedTab.Text;
            if (!title.EndsWith("*"))
                title = string.Format("{0} *", title);
            selectedTab.Text = title;
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




    }
}
