using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NDbUnitDataEditor
{
    public delegate void TableViewEventHandler();

    public partial class TableView : UserControl
    {
        

        public TableView()
        {
            InitializeComponent();
        }

        public event TableViewEventHandler TableViewChanged;

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

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception is System.FormatException)
                MessageBox.Show("Please input value with correct type for this column", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            TableViewChanged();
        }

    }
}
