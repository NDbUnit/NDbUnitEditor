using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NDbUnitDataEditor
{
    public partial class DataSetFromDatabase : Form, IDataSetFromDatabaseView
    {
        public event DataSetFromDatabaseEvent FillDataSetFromDatabase;

        public event DataSetFromDatabaseEvent SelectDatabaseType;

        public event DataSetFromDatabaseEvent TestDatabaseConnection;

        public DataSetFromDatabase()
        {
            InitializeComponent();
        }

        public bool ConnectionTestResult
        {
            set
            {
                if (value)
                    MessageBox.Show("Connection Successful", "Connection Test Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Unable to connect to database!", "Connection Test Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string DatabaseConnectionString
        {
            get { return txtConnectionString.Text; }
        }

        public IList<DataSetFromDatabasePresenter.DatabaseClientType> DatabaseConnectionTypes
        {
            set
            {
                cboDatabaseType.DataSource = value;
            }
        }

        public DataSetFromDatabasePresenter.DatabaseClientType SelectedDatabaseConnectionType
        {
            get { return (DataSetFromDatabasePresenter.DatabaseClientType)cboDatabaseType.SelectedValue; }
        }

        public void Run()
        {
            this.ShowDialog();
        }

        private void btnFillDataSet_Click(object sender, EventArgs e)
        {
            FillDataSetFromDatabase();
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            TestDatabaseConnection();
        }

        private void cboDatabaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectDatabaseType();
        }

    }
}
