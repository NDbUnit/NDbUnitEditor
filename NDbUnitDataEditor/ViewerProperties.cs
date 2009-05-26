using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestDataViewer
{
    public partial class ViewerProperties : Form
    {
        public ViewerProperties()
        {
            InitializeComponent();
        }

        public string SchemaFileName
        {
            get
            {
                return txtSchemaFileName.Text;
            }

            set
            {
                txtSchemaFileName.Text = value;
            }
        }

        public string DataFileName
        {
            get
            {
                return txtDataFileName.Text;
            }
            set
            {
                txtDataFileName.Text = value;
            }
        }

        

        private void btnOpenDataFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                DataFileName = openFileDialog1.FileName;
        }

        private void btnOpenSchemaFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                SchemaFileName = openFileDialog1.FileName;
        }
    }
}