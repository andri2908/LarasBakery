﻿namespace AlphaSoft
{
    partial class dataReturForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dataReturForm));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.noPOInvoiceTextBox = new System.Windows.Forms.TextBox();
            this.PODtPicker_1 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.PODtPicker_2 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.supplierHiddenCombo = new System.Windows.Forms.ComboBox();
            this.showAllCheckBox = new System.Windows.Forms.CheckBox();
            this.newButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.supplierCombo = new System.Windows.Forms.ComboBox();
            this.displayButton = new System.Windows.Forms.Button();
            this.dataPurchaseOrder = new System.Windows.Forms.DataGridView();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.comboPrintOut = new System.Windows.Forms.ComboBox();
            this.labelPrintOut = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataPurchaseOrder)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // noPOInvoiceTextBox
            // 
            this.noPOInvoiceTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.noPOInvoiceTextBox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noPOInvoiceTextBox.Location = new System.Drawing.Point(162, 15);
            this.noPOInvoiceTextBox.Name = "noPOInvoiceTextBox";
            this.noPOInvoiceTextBox.Size = new System.Drawing.Size(317, 27);
            this.noPOInvoiceTextBox.TabIndex = 36;
            // 
            // PODtPicker_1
            // 
            this.PODtPicker_1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PODtPicker_1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.PODtPicker_1.Location = new System.Drawing.Point(162, 48);
            this.PODtPicker_1.Name = "PODtPicker_1";
            this.PODtPicker_1.Size = new System.Drawing.Size(144, 27);
            this.PODtPicker_1.TabIndex = 38;
            this.PODtPicker_1.Enter += new System.EventHandler(this.genericControl_Enter);
            this.PODtPicker_1.Leave += new System.EventHandler(this.genericControl_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FloralWhite;
            this.label5.Location = new System.Drawing.Point(312, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 18);
            this.label5.TabIndex = 44;
            this.label5.Text = "-";
            // 
            // PODtPicker_2
            // 
            this.PODtPicker_2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PODtPicker_2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.PODtPicker_2.Location = new System.Drawing.Point(334, 48);
            this.PODtPicker_2.Name = "PODtPicker_2";
            this.PODtPicker_2.Size = new System.Drawing.Size(145, 27);
            this.PODtPicker_2.TabIndex = 43;
            this.PODtPicker_2.Enter += new System.EventHandler(this.genericControl_Enter);
            this.PODtPicker_2.Leave += new System.EventHandler(this.genericControl_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FloralWhite;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 18);
            this.label1.TabIndex = 35;
            this.label1.Text = "No Retur";
            // 
            // supplierHiddenCombo
            // 
            this.supplierHiddenCombo.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.supplierHiddenCombo.FormattingEnabled = true;
            this.supplierHiddenCombo.Location = new System.Drawing.Point(566, 272);
            this.supplierHiddenCombo.Name = "supplierHiddenCombo";
            this.supplierHiddenCombo.Size = new System.Drawing.Size(311, 26);
            this.supplierHiddenCombo.TabIndex = 61;
            this.supplierHiddenCombo.Visible = false;
            // 
            // showAllCheckBox
            // 
            this.showAllCheckBox.AutoSize = true;
            this.showAllCheckBox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showAllCheckBox.ForeColor = System.Drawing.Color.FloralWhite;
            this.showAllCheckBox.Location = new System.Drawing.Point(162, 112);
            this.showAllCheckBox.Name = "showAllCheckBox";
            this.showAllCheckBox.Size = new System.Drawing.Size(101, 22);
            this.showAllCheckBox.TabIndex = 47;
            this.showAllCheckBox.Text = "Show All";
            this.showAllCheckBox.UseVisualStyleBackColor = true;
            // 
            // newButton
            // 
            this.newButton.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.newButton.Location = new System.Drawing.Point(449, 140);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(154, 37);
            this.newButton.TabIndex = 60;
            this.newButton.Text = "NEW RETUR";
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FloralWhite;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 18);
            this.label2.TabIndex = 37;
            this.label2.Text = "Tanggal Retur";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FloralWhite;
            this.label3.Location = new System.Drawing.Point(6, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 18);
            this.label3.TabIndex = 39;
            this.label3.Text = "Supplier";
            // 
            // supplierCombo
            // 
            this.supplierCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.supplierCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.supplierCombo.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.supplierCombo.FormattingEnabled = true;
            this.supplierCombo.Location = new System.Drawing.Point(162, 81);
            this.supplierCombo.Name = "supplierCombo";
            this.supplierCombo.Size = new System.Drawing.Size(317, 26);
            this.supplierCombo.TabIndex = 40;
            this.supplierCombo.SelectedIndexChanged += new System.EventHandler(this.supplierCombo_SelectedIndexChanged);
            this.supplierCombo.Enter += new System.EventHandler(this.genericControl_Enter);
            this.supplierCombo.Leave += new System.EventHandler(this.genericControl_Leave);
            // 
            // displayButton
            // 
            this.displayButton.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.displayButton.Location = new System.Drawing.Point(296, 140);
            this.displayButton.Name = "displayButton";
            this.displayButton.Size = new System.Drawing.Size(95, 37);
            this.displayButton.TabIndex = 59;
            this.displayButton.Text = "DISPLAY";
            this.displayButton.UseVisualStyleBackColor = true;
            this.displayButton.Click += new System.EventHandler(this.displayButton_Click);
            // 
            // dataPurchaseOrder
            // 
            this.dataPurchaseOrder.AllowUserToAddRows = false;
            this.dataPurchaseOrder.AllowUserToDeleteRows = false;
            this.dataPurchaseOrder.BackgroundColor = System.Drawing.Color.FloralWhite;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataPurchaseOrder.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataPurchaseOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataPurchaseOrder.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataPurchaseOrder.Location = new System.Drawing.Point(0, 210);
            this.dataPurchaseOrder.MultiSelect = false;
            this.dataPurchaseOrder.Name = "dataPurchaseOrder";
            this.dataPurchaseOrder.RowHeadersVisible = false;
            this.dataPurchaseOrder.Size = new System.Drawing.Size(921, 427);
            this.dataPurchaseOrder.TabIndex = 58;
            this.dataPurchaseOrder.DoubleClick += new System.EventHandler(this.dataPurchaseOrder_DoubleClick);
            this.dataPurchaseOrder.Enter += new System.EventHandler(this.dataPurchaseOrder_Enter);
            this.dataPurchaseOrder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataPurchaseOrder_KeyDown);
            this.dataPurchaseOrder.Leave += new System.EventHandler(this.dataPurchaseOrder_Leave);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // comboPrintOut
            // 
            this.comboPrintOut.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboPrintOut.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboPrintOut.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboPrintOut.FormattingEnabled = true;
            this.comboPrintOut.Items.AddRange(new object[] {
            "POS Receipt",
            "Kuarto"});
            this.comboPrintOut.Location = new System.Drawing.Point(705, 21);
            this.comboPrintOut.Name = "comboPrintOut";
            this.comboPrintOut.Size = new System.Drawing.Size(188, 26);
            this.comboPrintOut.TabIndex = 63;
            this.comboPrintOut.Visible = false;
            // 
            // labelPrintOut
            // 
            this.labelPrintOut.AutoSize = true;
            this.labelPrintOut.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPrintOut.ForeColor = System.Drawing.Color.FloralWhite;
            this.labelPrintOut.Location = new System.Drawing.Point(554, 24);
            this.labelPrintOut.Name = "labelPrintOut";
            this.labelPrintOut.Size = new System.Drawing.Size(145, 18);
            this.labelPrintOut.TabIndex = 64;
            this.labelPrintOut.Text = "Print Out Paper";
            this.labelPrintOut.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboPrintOut);
            this.groupBox1.Controls.Add(this.newButton);
            this.groupBox1.Controls.Add(this.labelPrintOut);
            this.groupBox1.Controls.Add(this.displayButton);
            this.groupBox1.Controls.Add(this.showAllCheckBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.supplierCombo);
            this.groupBox1.Controls.Add(this.PODtPicker_2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.PODtPicker_1);
            this.groupBox1.Controls.Add(this.noPOInvoiceTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FloralWhite;
            this.groupBox1.Location = new System.Drawing.Point(9, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(899, 193);
            this.groupBox1.TabIndex = 65;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FILTER";
            // 
            // dataReturForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(920, 637);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.supplierHiddenCombo);
            this.Controls.Add(this.dataPurchaseOrder);
            this.MaximizeBox = false;
            this.Name = "dataReturForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DATA RETUR";
            this.Activated += new System.EventHandler(this.dataReturForm_Activated);
            this.Deactivate += new System.EventHandler(this.dataReturForm_Deactivate);
            this.Load += new System.EventHandler(this.dataReturForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataPurchaseOrder)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox noPOInvoiceTextBox;
        private System.Windows.Forms.DateTimePicker PODtPicker_1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker PODtPicker_2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox supplierHiddenCombo;
        private System.Windows.Forms.CheckBox showAllCheckBox;
        private System.Windows.Forms.Button newButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox supplierCombo;
        private System.Windows.Forms.Button displayButton;
        private System.Windows.Forms.DataGridView dataPurchaseOrder;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ComboBox comboPrintOut;
        private System.Windows.Forms.Label labelPrintOut;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}