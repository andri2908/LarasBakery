﻿namespace AlphaSoft
{
    partial class deliveryOrderForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.errorLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.customerNameTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelAsal_1 = new System.Windows.Forms.Label();
            this.labelAsal = new System.Windows.Forms.Label();
            this.doInvoiceTextBox = new System.Windows.Forms.TextBox();
            this.noInvoiceTextBox = new System.Windows.Forms.TextBox();
            this.DODtPicker = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelNo = new System.Windows.Forms.Label();
            this.detailGridView = new System.Windows.Forms.DataGridView();
            this.reprintButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.instructionTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.detailGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // errorLabel
            // 
            this.errorLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.errorLabel.AutoSize = true;
            this.errorLabel.BackColor = System.Drawing.Color.White;
            this.errorLabel.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(5, 5);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(23, 18);
            this.errorLabel.TabIndex = 49;
            this.errorLabel.Text = "   ";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.errorLabel);
            this.panel1.Location = new System.Drawing.Point(2, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(993, 29);
            this.panel1.TabIndex = 46;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.instructionTextBox);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.customerNameTextBox);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.labelAsal_1);
            this.groupBox1.Controls.Add(this.labelAsal);
            this.groupBox1.Controls.Add(this.doInvoiceTextBox);
            this.groupBox1.Controls.Add(this.noInvoiceTextBox);
            this.groupBox1.Controls.Add(this.DODtPicker);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.labelNo);
            this.groupBox1.Location = new System.Drawing.Point(2, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(993, 188);
            this.groupBox1.TabIndex = 59;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(255, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 18);
            this.label1.TabIndex = 56;
            this.label1.Text = ":";
            // 
            // customerNameTextBox
            // 
            this.customerNameTextBox.Enabled = false;
            this.customerNameTextBox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customerNameTextBox.Location = new System.Drawing.Point(276, 117);
            this.customerNameTextBox.Name = "customerNameTextBox";
            this.customerNameTextBox.ReadOnly = true;
            this.customerNameTextBox.Size = new System.Drawing.Size(346, 27);
            this.customerNameTextBox.TabIndex = 55;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label11.Location = new System.Drawing.Point(20, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(221, 18);
            this.label11.TabIndex = 20;
            this.label11.Text = "TANGGAL PENGIRIMAN ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(255, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 18);
            this.label6.TabIndex = 12;
            this.label6.Text = ":";
            // 
            // labelAsal_1
            // 
            this.labelAsal_1.AutoSize = true;
            this.labelAsal_1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAsal_1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelAsal_1.Location = new System.Drawing.Point(255, 117);
            this.labelAsal_1.Name = "labelAsal_1";
            this.labelAsal_1.Size = new System.Drawing.Size(14, 18);
            this.labelAsal_1.TabIndex = 20;
            this.labelAsal_1.Text = ":";
            // 
            // labelAsal
            // 
            this.labelAsal.AutoSize = true;
            this.labelAsal.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAsal.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelAsal.Location = new System.Drawing.Point(20, 117);
            this.labelAsal.Name = "labelAsal";
            this.labelAsal.Size = new System.Drawing.Size(109, 18);
            this.labelAsal.TabIndex = 7;
            this.labelAsal.Text = "CUSTOMER";
            // 
            // doInvoiceTextBox
            // 
            this.doInvoiceTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.doInvoiceTextBox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.doInvoiceTextBox.Location = new System.Drawing.Point(276, 17);
            this.doInvoiceTextBox.Name = "doInvoiceTextBox";
            this.doInvoiceTextBox.ReadOnly = true;
            this.doInvoiceTextBox.Size = new System.Drawing.Size(158, 27);
            this.doInvoiceTextBox.TabIndex = 16;
            this.doInvoiceTextBox.TextChanged += new System.EventHandler(this.doInvoiceTextBox_TextChanged);
            // 
            // noInvoiceTextBox
            // 
            this.noInvoiceTextBox.Enabled = false;
            this.noInvoiceTextBox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noInvoiceTextBox.Location = new System.Drawing.Point(276, 84);
            this.noInvoiceTextBox.Name = "noInvoiceTextBox";
            this.noInvoiceTextBox.ReadOnly = true;
            this.noInvoiceTextBox.Size = new System.Drawing.Size(158, 27);
            this.noInvoiceTextBox.TabIndex = 16;
            // 
            // DODtPicker
            // 
            this.DODtPicker.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DODtPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DODtPicker.Location = new System.Drawing.Point(276, 50);
            this.DODtPicker.Name = "DODtPicker";
            this.DODtPicker.Size = new System.Drawing.Size(201, 27);
            this.DODtPicker.TabIndex = 39;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label12.Location = new System.Drawing.Point(20, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(158, 18);
            this.label12.TabIndex = 29;
            this.label12.Text = "NO PENGIRIMAN";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(255, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 18);
            this.label4.TabIndex = 10;
            this.label4.Text = ":";
            // 
            // labelNo
            // 
            this.labelNo.AutoSize = true;
            this.labelNo.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelNo.Location = new System.Drawing.Point(20, 87);
            this.labelNo.Name = "labelNo";
            this.labelNo.Size = new System.Drawing.Size(118, 18);
            this.labelNo.TabIndex = 19;
            this.labelNo.Text = "NO INVOICE";
            // 
            // detailGridView
            // 
            this.detailGridView.AllowUserToAddRows = false;
            this.detailGridView.AllowUserToDeleteRows = false;
            this.detailGridView.BackgroundColor = System.Drawing.Color.FloralWhite;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.detailGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.detailGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.detailGridView.DefaultCellStyle = dataGridViewCellStyle14;
            this.detailGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.detailGridView.Location = new System.Drawing.Point(2, 228);
            this.detailGridView.Name = "detailGridView";
            this.detailGridView.RowHeadersVisible = false;
            this.detailGridView.Size = new System.Drawing.Size(993, 372);
            this.detailGridView.TabIndex = 60;
            this.detailGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.detailGridView_CellBeginEdit);
            this.detailGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.detailGridView_CellEndEdit);
            this.detailGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.detailGridView_CellValueChanged);
            this.detailGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.detailGridView_CurrentCellDirtyStateChanged);
            // 
            // reprintButton
            // 
            this.reprintButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.reprintButton.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reprintButton.Location = new System.Drawing.Point(516, 612);
            this.reprintButton.Name = "reprintButton";
            this.reprintButton.Size = new System.Drawing.Size(122, 37);
            this.reprintButton.TabIndex = 63;
            this.reprintButton.Text = "REPRINT";
            this.reprintButton.UseVisualStyleBackColor = true;
            this.reprintButton.Visible = false;
            this.reprintButton.Click += new System.EventHandler(this.reprintButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.saveButton.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(356, 612);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(122, 37);
            this.saveButton.TabIndex = 61;
            this.saveButton.Text = "SAVE ";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(276, 151);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(346, 27);
            this.textBox1.TabIndex = 59;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(255, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 18);
            this.label2.TabIndex = 58;
            this.label2.Text = ":";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(20, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 18);
            this.label3.TabIndex = 57;
            this.label3.Text = "USER";
            // 
            // instructionTextBox
            // 
            this.instructionTextBox.Location = new System.Drawing.Point(640, 84);
            this.instructionTextBox.MaxLength = 200;
            this.instructionTextBox.Multiline = true;
            this.instructionTextBox.Name = "instructionTextBox";
            this.instructionTextBox.Size = new System.Drawing.Size(347, 94);
            this.instructionTextBox.TabIndex = 60;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(637, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 18);
            this.label5.TabIndex = 61;
            this.label5.Text = "INSTRUKSI :";
            // 
            // deliveryOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(995, 661);
            this.Controls.Add(this.reprintButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.detailGridView);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.MinimizeBox = false;
            this.Name = "deliveryOrderForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DELIVERY ORDER";
            this.Activated += new System.EventHandler(this.deliveryOrderForm_Activated);
            this.Load += new System.EventHandler(this.deliveryOrderForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.detailGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelAsal_1;
        private System.Windows.Forms.Label labelAsal;
        private System.Windows.Forms.TextBox doInvoiceTextBox;
        private System.Windows.Forms.TextBox noInvoiceTextBox;
        private System.Windows.Forms.DateTimePicker DODtPicker;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox customerNameTextBox;
        private System.Windows.Forms.DataGridView detailGridView;
        private System.Windows.Forms.Button reprintButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox instructionTextBox;
    }
}