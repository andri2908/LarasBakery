namespace AlphaSoft
{
    partial class dailyStockTakeDetailForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.detailDataGrid = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.errorLabel = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.exportDate = new System.Windows.Forms.Label();
            this.stockTakeDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.revisionLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.detailDataGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // detailDataGrid
            // 
            this.detailDataGrid.AllowUserToAddRows = false;
            this.detailDataGrid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.detailDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.detailDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.detailDataGrid.DefaultCellStyle = dataGridViewCellStyle4;
            this.detailDataGrid.Location = new System.Drawing.Point(1, 91);
            this.detailDataGrid.Name = "detailDataGrid";
            this.detailDataGrid.RowHeadersVisible = false;
            this.detailDataGrid.Size = new System.Drawing.Size(1102, 572);
            this.detailDataGrid.TabIndex = 52;
            this.detailDataGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.detailDataGrid_CellBeginEdit);
            this.detailDataGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.detailDataGrid_CellEndEdit);
            this.detailDataGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.detailDataGrid_CellValueChanged);
            this.detailDataGrid.CurrentCellDirtyStateChanged += new System.EventHandler(this.detailDataGrid_CurrentCellDirtyStateChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.errorLabel);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1102, 29);
            this.panel1.TabIndex = 51;
            // 
            // errorLabel
            // 
            this.errorLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.errorLabel.AutoSize = true;
            this.errorLabel.BackColor = System.Drawing.Color.White;
            this.errorLabel.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(4, 6);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(23, 18);
            this.errorLabel.TabIndex = 26;
            this.errorLabel.Text = "   ";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // exportDate
            // 
            this.exportDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.exportDate.AutoSize = true;
            this.exportDate.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportDate.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.exportDate.Location = new System.Drawing.Point(11, 135);
            this.exportDate.Name = "exportDate";
            this.exportDate.Size = new System.Drawing.Size(87, 18);
            this.exportDate.TabIndex = 54;
            this.exportDate.Text = "FILE CSV";
            // 
            // stockTakeDateTimePicker
            // 
            this.stockTakeDateTimePicker.Enabled = false;
            this.stockTakeDateTimePicker.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stockTakeDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.stockTakeDateTimePicker.Location = new System.Drawing.Point(96, 44);
            this.stockTakeDateTimePicker.Name = "stockTakeDateTimePicker";
            this.stockTakeDateTimePicker.Size = new System.Drawing.Size(144, 27);
            this.stockTakeDateTimePicker.TabIndex = 56;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(11, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 18);
            this.label2.TabIndex = 55;
            this.label2.Text = "Tanggal";
            // 
            // revisionLabel
            // 
            this.revisionLabel.AutoSize = true;
            this.revisionLabel.Font = new System.Drawing.Font("Verdana", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.revisionLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.revisionLabel.Location = new System.Drawing.Point(666, 37);
            this.revisionLabel.Name = "revisionLabel";
            this.revisionLabel.Size = new System.Drawing.Size(295, 18);
            this.revisionLabel.TabIndex = 58;
            this.revisionLabel.Text = "Tekan F9 untuk menyimpan data";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(654, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(318, 18);
            this.label1.TabIndex = 59;
            this.label1.Text = "Tekan F10 untuk mencetak laporan";
            // 
            // dailyStockTakeDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(1103, 664);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.revisionLabel);
            this.Controls.Add(this.stockTakeDateTimePicker);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.detailDataGrid);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.exportDate);
            this.Name = "dailyStockTakeDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "STOCK TAKE HARIAN";
            this.Activated += new System.EventHandler(this.dailyStockTakeDetailForm_Activated);
            this.Deactivate += new System.EventHandler(this.dailyStockTakeDetailForm_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.dailyStockTakeDetailForm_FormClosed);
            this.Load += new System.EventHandler(this.dailyStockTakeDetailForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.detailDataGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView detailDataGrid;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label exportDate;
        private System.Windows.Forms.DateTimePicker stockTakeDateTimePicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Label revisionLabel;
        private System.Windows.Forms.Label label1;
    }
}