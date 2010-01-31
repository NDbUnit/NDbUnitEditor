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
        public event TableViewEventHandler TableViewChanged;

        public TableView(string name)
        {
            InitializeComponent();
            Name = name;
        }

        public object DataSource
        {
            get { return bindingSource1.DataSource; }
            set
            {
                bindingSource1.DataSource = value;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            TableViewEventArguments args = new TableViewEventArguments(Name);
            TableViewChanged(args);
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception is System.FormatException)
                MessageBox.Show("Please input value with correct type for this column", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void TableView_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = true;            
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.DataSource = bindingSource1;
        }

    }
}
