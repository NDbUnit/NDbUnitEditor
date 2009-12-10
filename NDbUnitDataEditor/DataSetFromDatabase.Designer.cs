﻿namespace NDbUnitDataEditor
{
    partial class DataSetFromDatabase
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
            this.lblConnectionString = new System.Windows.Forms.Label();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.cboDatabaseType = new System.Windows.Forms.ComboBox();
            this.lblDatabaseType = new System.Windows.Forms.Label();
            this.btnGetDataSetFromDatabase = new System.Windows.Forms.Button();
            this.btnPutDataSetToDatabase = new System.Windows.Forms.Button();
            this.btnGetSchemaFromDatabase = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.AutoSize = true;
            this.lblConnectionString.Location = new System.Drawing.Point(12, 13);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(140, 13);
            this.lblConnectionString.TabIndex = 0;
            this.lblConnectionString.Text = "Database Connection String";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(158, 10);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(374, 20);
            this.txtConnectionString.TabIndex = 1;
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(542, 10);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(169, 23);
            this.btnTestConnection.TabIndex = 2;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // cboDatabaseType
            // 
            this.cboDatabaseType.FormattingEnabled = true;
            this.cboDatabaseType.Location = new System.Drawing.Point(158, 37);
            this.cboDatabaseType.Name = "cboDatabaseType";
            this.cboDatabaseType.Size = new System.Drawing.Size(245, 21);
            this.cboDatabaseType.TabIndex = 3;
            this.cboDatabaseType.SelectedIndexChanged += new System.EventHandler(this.cboDatabaseType_SelectedIndexChanged);
            // 
            // lblDatabaseType
            // 
            this.lblDatabaseType.AutoSize = true;
            this.lblDatabaseType.Location = new System.Drawing.Point(12, 40);
            this.lblDatabaseType.Name = "lblDatabaseType";
            this.lblDatabaseType.Size = new System.Drawing.Size(80, 13);
            this.lblDatabaseType.TabIndex = 4;
            this.lblDatabaseType.Text = "Database Type";
            // 
            // btnGetDataSetFromDatabase
            // 
            this.btnGetDataSetFromDatabase.Location = new System.Drawing.Point(542, 70);
            this.btnGetDataSetFromDatabase.Name = "btnGetDataSetFromDatabase";
            this.btnGetDataSetFromDatabase.Size = new System.Drawing.Size(169, 23);
            this.btnGetDataSetFromDatabase.TabIndex = 5;
            this.btnGetDataSetFromDatabase.Text = "Get DataSet from Database";
            this.btnGetDataSetFromDatabase.UseVisualStyleBackColor = true;
            this.btnGetDataSetFromDatabase.Click += new System.EventHandler(this.btnGetDataSetFromDatabase_Click);
            // 
            // btnPutDataSetToDatabase
            // 
            this.btnPutDataSetToDatabase.Location = new System.Drawing.Point(542, 99);
            this.btnPutDataSetToDatabase.Name = "btnPutDataSetToDatabase";
            this.btnPutDataSetToDatabase.Size = new System.Drawing.Size(169, 23);
            this.btnPutDataSetToDatabase.TabIndex = 6;
            this.btnPutDataSetToDatabase.Text = "Put DataSet to Database";
            this.btnPutDataSetToDatabase.UseVisualStyleBackColor = true;
            this.btnPutDataSetToDatabase.Click += new System.EventHandler(this.btnPutDataSetToDatabase_Click);
            // 
            // btnGetSchemaFromDatabase
            // 
            this.btnGetSchemaFromDatabase.Location = new System.Drawing.Point(542, 128);
            this.btnGetSchemaFromDatabase.Name = "btnGetSchemaFromDatabase";
            this.btnGetSchemaFromDatabase.Size = new System.Drawing.Size(169, 23);
            this.btnGetSchemaFromDatabase.TabIndex = 7;
            this.btnGetSchemaFromDatabase.Text = "Get Schema from Database";
            this.btnGetSchemaFromDatabase.UseVisualStyleBackColor = true;
            this.btnGetSchemaFromDatabase.Click += new System.EventHandler(this.btnGetSchemaFromDatabase_Click);
            // 
            // DataSetFromDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 165);
            this.Controls.Add(this.btnGetSchemaFromDatabase);
            this.Controls.Add(this.btnPutDataSetToDatabase);
            this.Controls.Add(this.btnGetDataSetFromDatabase);
            this.Controls.Add(this.lblDatabaseType);
            this.Controls.Add(this.cboDatabaseType);
            this.Controls.Add(this.btnTestConnection);
            this.Controls.Add(this.txtConnectionString);
            this.Controls.Add(this.lblConnectionString);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataSetFromDatabase";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sync DataSet and Schema with Database";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblConnectionString;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.ComboBox cboDatabaseType;
        private System.Windows.Forms.Label lblDatabaseType;
        private System.Windows.Forms.Button btnGetDataSetFromDatabase;
        private System.Windows.Forms.Button btnPutDataSetToDatabase;
        private System.Windows.Forms.Button btnGetSchemaFromDatabase;
    }
}