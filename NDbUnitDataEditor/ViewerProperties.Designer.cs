namespace TestDataViewer
{
    partial class ViewerProperties
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpenDataFile = new System.Windows.Forms.Button();
            this.txtDataFileName = new System.Windows.Forms.TextBox();
            this.btnOpenSchemaFile = new System.Windows.Forms.Button();
            this.txtSchemaFileName = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Data file:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Schema file:";
            // 
            // btnOpenDataFile
            // 
            this.btnOpenDataFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenDataFile.Location = new System.Drawing.Point(361, 37);
            this.btnOpenDataFile.Name = "btnOpenDataFile";
            this.btnOpenDataFile.Size = new System.Drawing.Size(27, 23);
            this.btnOpenDataFile.TabIndex = 11;
            this.btnOpenDataFile.Text = "...";
            this.btnOpenDataFile.UseVisualStyleBackColor = true;
            this.btnOpenDataFile.Click += new System.EventHandler(this.btnOpenDataFile_Click);
            // 
            // txtDataFileName
            // 
            this.txtDataFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDataFileName.Location = new System.Drawing.Point(79, 39);
            this.txtDataFileName.Name = "txtDataFileName";
            this.txtDataFileName.Size = new System.Drawing.Size(276, 20);
            this.txtDataFileName.TabIndex = 10;
            // 
            // btnOpenSchemaFile
            // 
            this.btnOpenSchemaFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenSchemaFile.Location = new System.Drawing.Point(361, 10);
            this.btnOpenSchemaFile.Name = "btnOpenSchemaFile";
            this.btnOpenSchemaFile.Size = new System.Drawing.Size(27, 23);
            this.btnOpenSchemaFile.TabIndex = 9;
            this.btnOpenSchemaFile.Text = "...";
            this.btnOpenSchemaFile.UseVisualStyleBackColor = true;
            this.btnOpenSchemaFile.Click += new System.EventHandler(this.btnOpenSchemaFile_Click);
            // 
            // txtSchemaFileName
            // 
            this.txtSchemaFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSchemaFileName.Location = new System.Drawing.Point(79, 12);
            this.txtSchemaFileName.Name = "txtSchemaFileName";
            this.txtSchemaFileName.Size = new System.Drawing.Size(276, 20);
            this.txtSchemaFileName.TabIndex = 8;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(322, 89);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(241, 89);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 15;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Properties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 124);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOpenDataFile);
            this.Controls.Add(this.txtDataFileName);
            this.Controls.Add(this.btnOpenSchemaFile);
            this.Controls.Add(this.txtSchemaFileName);
            this.Name = "Properties";
            this.Text = "Properties";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpenDataFile;
        private System.Windows.Forms.TextBox txtDataFileName;
        private System.Windows.Forms.Button btnOpenSchemaFile;
        private System.Windows.Forms.TextBox txtSchemaFileName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}