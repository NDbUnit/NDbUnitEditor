namespace NDbUnitDataEditor
{
    partial class DataEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataEditor));
            this.btnClose = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAbout = new System.Windows.Forms.ToolStripButton();
            this.btnCloseTab = new System.Windows.Forms.ToolStripButton();
            this.btnSaveData = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtSchemaFileName = new System.Windows.Forms.ToolStripTextBox();
            this.btnBrowseSchemaFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lblDataFileName = new System.Windows.Forms.ToolStripLabel();
            this.txtDataFileName = new System.Windows.Forms.ToolStripTextBox();
            this.btnBrowseDataFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnReload = new System.Windows.Forms.ToolStripButton();
            this.btnNewGuid = new System.Windows.Forms.ToolStripButton();
            this.btnDataSetFromDatabase = new System.Windows.Forms.ToolStripButton();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tbTableViews = new System.Windows.Forms.TabControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeAllTabsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllButThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeActiveTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(785, 533);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAbout,
            this.btnCloseTab,
            this.btnSaveData,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.txtSchemaFileName,
            this.btnBrowseSchemaFile,
            this.toolStripSeparator2,
            this.lblDataFileName,
            this.txtDataFileName,
            this.btnBrowseDataFile,
            this.toolStripSeparator3,
            this.btnReload,
            this.btnNewGuid,
            this.btnDataSetFromDatabase});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(872, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAbout
            // 
            this.btnAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAbout.Image = ((System.Drawing.Image)(resources.GetObject("btnAbout.Image")));
            this.btnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(23, 22);
            this.btnAbout.Text = "toolStripButton1";
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnCloseTab
            // 
            this.btnCloseTab.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnCloseTab.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCloseTab.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseTab.Image = ((System.Drawing.Image)(resources.GetObject("btnCloseTab.Image")));
            this.btnCloseTab.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCloseTab.Name = "btnCloseTab";
            this.btnCloseTab.Size = new System.Drawing.Size(23, 22);
            this.btnCloseTab.Text = "X";
            this.btnCloseTab.ToolTipText = "Close Active Tab...";
            this.btnCloseTab.Click += new System.EventHandler(this.btnCloseTab_Click);
            // 
            // btnSaveData
            // 
            this.btnSaveData.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnSaveData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveData.Enabled = false;
            this.btnSaveData.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveData.Image")));
            this.btnSaveData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveData.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.btnSaveData.Name = "btnSaveData";
            this.btnSaveData.Size = new System.Drawing.Size(23, 22);
            this.btnSaveData.Text = "Save Data";
            this.btnSaveData.Click += new System.EventHandler(this.btnSaveData_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(104, 22);
            this.toolStripLabel1.Text = "Schema file name:";
            // 
            // txtSchemaFileName
            // 
            this.txtSchemaFileName.BackColor = System.Drawing.SystemColors.Window;
            this.txtSchemaFileName.Name = "txtSchemaFileName";
            this.txtSchemaFileName.ReadOnly = true;
            this.txtSchemaFileName.Size = new System.Drawing.Size(100, 25);
            this.txtSchemaFileName.TextChanged += new System.EventHandler(this.txtSchemaFileName_TextChanged);
            // 
            // btnBrowseSchemaFile
            // 
            this.btnBrowseSchemaFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnBrowseSchemaFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBrowseSchemaFile.Name = "btnBrowseSchemaFile";
            this.btnBrowseSchemaFile.Size = new System.Drawing.Size(23, 22);
            this.btnBrowseSchemaFile.Text = "...";
            this.btnBrowseSchemaFile.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnBrowseSchemaFile.Click += new System.EventHandler(this.btnBrowseSchemaFile_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // lblDataFileName
            // 
            this.lblDataFileName.Name = "lblDataFileName";
            this.lblDataFileName.Size = new System.Drawing.Size(86, 22);
            this.lblDataFileName.Text = "Data file name:";
            // 
            // txtDataFileName
            // 
            this.txtDataFileName.BackColor = System.Drawing.SystemColors.Window;
            this.txtDataFileName.Name = "txtDataFileName";
            this.txtDataFileName.ReadOnly = true;
            this.txtDataFileName.Size = new System.Drawing.Size(100, 25);
            // 
            // btnBrowseDataFile
            // 
            this.btnBrowseDataFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnBrowseDataFile.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseDataFile.Image")));
            this.btnBrowseDataFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBrowseDataFile.Name = "btnBrowseDataFile";
            this.btnBrowseDataFile.Size = new System.Drawing.Size(23, 22);
            this.btnBrowseDataFile.Text = "...";
            this.btnBrowseDataFile.Click += new System.EventHandler(this.btnBrowseDataFile_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnReload
            // 
            this.btnReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnReload.Enabled = false;
            this.btnReload.Image = ((System.Drawing.Image)(resources.GetObject("btnReload.Image")));
            this.btnReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(23, 22);
            this.btnReload.Text = "load/reload files";
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnNewGuid
            // 
            this.btnNewGuid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewGuid.Image = ((System.Drawing.Image)(resources.GetObject("btnNewGuid.Image")));
            this.btnNewGuid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewGuid.Name = "btnNewGuid";
            this.btnNewGuid.Size = new System.Drawing.Size(23, 22);
            this.btnNewGuid.Text = "Insert new Guid into cell";
            this.btnNewGuid.Click += new System.EventHandler(this.btnNewGuid_Click);
            // 
            // btnDataSetFromDatabase
            // 
            this.btnDataSetFromDatabase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDataSetFromDatabase.Image = ((System.Drawing.Image)(resources.GetObject("btnDataSetFromDatabase.Image")));
            this.btnDataSetFromDatabase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDataSetFromDatabase.Name = "btnDataSetFromDatabase";
            this.btnDataSetFromDatabase.Size = new System.Drawing.Size(23, 22);
            this.btnDataSetFromDatabase.Text = "toolStripButton1";
            this.btnDataSetFromDatabase.ToolTipText = "Get DataSet from Database";
            this.btnDataSetFromDatabase.Click += new System.EventHandler(this.btnDataSetFromDatabase_Click);
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(243, 489);
            this.treeView1.TabIndex = 8;
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // tbTableViews
            // 
            this.tbTableViews.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTableViews.Location = new System.Drawing.Point(3, 3);
            this.tbTableViews.Name = "tbTableViews";
            this.tbTableViews.SelectedIndex = 0;
            this.tbTableViews.Size = new System.Drawing.Size(613, 493);
            this.tbTableViews.TabIndex = 9;
            this.tbTableViews.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbTableViews_MouseClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbTableViews);
            this.splitContainer1.Size = new System.Drawing.Size(872, 499);
            this.splitContainer1.SplitterDistance = 249;
            this.splitContainer1.TabIndex = 10;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeActiveTabToolStripMenuItem,
            this.closeAllButThisToolStripMenuItem,
            this.closeAllTabsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(162, 70);
            // 
            // closeAllTabsToolStripMenuItem
            // 
            this.closeAllTabsToolStripMenuItem.Name = "closeAllTabsToolStripMenuItem";
            this.closeAllTabsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.closeAllTabsToolStripMenuItem.Text = "Close all tabs";
            // 
            // closeAllButThisToolStripMenuItem
            // 
            this.closeAllButThisToolStripMenuItem.Name = "closeAllButThisToolStripMenuItem";
            this.closeAllButThisToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.closeAllButThisToolStripMenuItem.Text = "Close all but this";
            // 
            // closeActiveTabToolStripMenuItem
            // 
            this.closeActiveTabToolStripMenuItem.Name = "closeActiveTabToolStripMenuItem";
            this.closeActiveTabToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.closeActiveTabToolStripMenuItem.Text = "Close active tab";
            // 
            // DataEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 568);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.btnClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "DataEditor";
            this.Text = "NDbUnit Data Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSaveData;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TabControl tbTableViews;
        private System.Windows.Forms.ToolStripButton btnReload;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton btnCloseTab;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox txtSchemaFileName;
        private System.Windows.Forms.ToolStripButton btnBrowseSchemaFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel lblDataFileName;
        private System.Windows.Forms.ToolStripTextBox txtDataFileName;
        private System.Windows.Forms.ToolStripButton btnBrowseDataFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnAbout;
        private System.Windows.Forms.ToolStripButton btnNewGuid;
        private System.Windows.Forms.ToolStripButton btnDataSetFromDatabase;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem closeAllTabsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeActiveTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllButThisToolStripMenuItem;
    }
}

