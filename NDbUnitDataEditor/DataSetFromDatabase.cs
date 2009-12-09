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
        public event DataSetFromDatabaseEvent GetDataSetFromDatabase;

        public event DataSetFromDatabaseEvent PutDataSetToDatabase;

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
                    MessageBox.Show(string.Format("Unable to connect to database!\n{0}", ErrorMessage), "Connection Test Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string DatabaseConnectionString
        {
            get
            {
                return txtConnectionString.Text;
            }
            set
            {
                txtConnectionString.Text = value;
            }
        }

        public IList<DatabaseClientType> DatabaseConnectionTypes
        {
            set
            {
                cboDatabaseType.DataSource = value;
            }
        }

        public int DatabaseTypeSelectedIndex
        {
            get
            {
                return cboDatabaseType.SelectedIndex;
            }
            set
            {
                cboDatabaseType.SelectedIndex = value;
            }
        }

        public string ErrorMessage { get; set; }

        public bool GetDataSetFromDatabaseResult
        {
            set
            {
                if (value)
                    MessageBox.Show("DataSet Filled from Database Successfully.", "DataSet Update Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(string.Format("Unable to Fill DataSet from Database!\n{0}", ErrorMessage), "DataSet Update Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool PutDataSetToDatabaseResult
        {
            set
            {
                if (value)
                    MessageBox.Show("Database Successfully updated with Current DataSet.", "Database Update Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(string.Format("Unable to update Database with current DataSet!\n{0}", ErrorMessage), "DataSet Update Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DatabaseClientType SelectedDatabaseConnectionType
        {
            get { return (DatabaseClientType)cboDatabaseType.SelectedValue; }
        }

        public string XmlFilePathName { get; set; }

        public string XsdFilePathName { get; set; }

        public void Run()
        {
            txtConnectionString.Text = DatabaseConnectionString;
            this.ShowDialog();
        }

        private void btnGetDataSetFromDatabase_Click(object sender, EventArgs e)
        {
            GetDataSetFromDatabase();
        }

        private void btnPutDataSetToDatabase_Click(object sender, EventArgs e)
        {
            PutDataSetToDatabase();
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
