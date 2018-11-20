namespace PdmMigrateFromExcel
{
    partial class Form1
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
            this.btnMigrateData = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.listViewErrors = new System.Windows.Forms.ListView();
            this.Column0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblFolder = new System.Windows.Forms.Label();
            this.textBoxFolderPath = new System.Windows.Forms.TextBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMigrateData
            // 
            this.btnMigrateData.Location = new System.Drawing.Point(30, 12);
            this.btnMigrateData.Name = "btnMigrateData";
            this.btnMigrateData.Size = new System.Drawing.Size(194, 80);
            this.btnMigrateData.TabIndex = 0;
            this.btnMigrateData.Text = "MigrateData";
            this.btnMigrateData.UseVisualStyleBackColor = true;
            this.btnMigrateData.Click += new System.EventHandler(this.btnMigrateData_Click);
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(44, 146);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(381, 264);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // listViewErrors
            // 
            this.listViewErrors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Column0});
            this.listViewErrors.Location = new System.Drawing.Point(562, 146);
            this.listViewErrors.Name = "listViewErrors";
            this.listViewErrors.Size = new System.Drawing.Size(226, 273);
            this.listViewErrors.TabIndex = 3;
            this.listViewErrors.UseCompatibleStateImageBehavior = false;
            // 
            // Column0
            // 
            this.Column0.Text = "Error List";
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(275, 12);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(88, 17);
            this.lblFolder.TabIndex = 4;
            this.lblFolder.Text = "Base Folder:";
            // 
            // textBoxFolderPath
            // 
            this.textBoxFolderPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFolderPath.Location = new System.Drawing.Point(369, 12);
            this.textBoxFolderPath.Name = "textBoxFolderPath";
            this.textBoxFolderPath.Size = new System.Drawing.Size(343, 19);
            this.textBoxFolderPath.TabIndex = 5;
            this.textBoxFolderPath.Text = "C:\\CDI Controlled Documents\\Drawings\\Harrison Drawings";
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectFolder.Location = new System.Drawing.Point(715, 7);
            this.btnSelectFolder.Margin = new System.Windows.Forms.Padding(0);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(59, 31);
            this.btnSelectFolder.TabIndex = 6;
            this.btnSelectFolder.Text = "...";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSelectFolder);
            this.Controls.Add(this.textBoxFolderPath);
            this.Controls.Add(this.lblFolder);
            this.Controls.Add(this.listViewErrors);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnMigrateData);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMigrateData;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ListView listViewErrors;
        private System.Windows.Forms.ColumnHeader Column0;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.TextBox textBoxFolderPath;
        private System.Windows.Forms.Button btnSelectFolder;
    }
}

