using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NDbUnitDataEditor
{
    public partial class TableView : UserControl
    {
        public TableView()
        {
            InitializeComponent();
        }

        public object DataSource
        {
            get { return bindingSource1.DataSource; }
            set
            {
                bindingSource1.DataSource = value;
            }
        }

        private void TableView_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = bindingSource1;
        }

    }
}
