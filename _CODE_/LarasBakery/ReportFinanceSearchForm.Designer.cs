﻿namespace AlphaSoft
{
    partial class ReportFinanceSearchForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MonthPicker = new System.Windows.Forms.DateTimePicker();
            this.CariButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.datetoPicker = new System.Windows.Forms.DateTimePicker();
            this.datefromPicker = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.MonthPicker);
            this.groupBox1.Controls.Add(this.CariButton);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.datetoPicker);
            this.groupBox1.Controls.Add(this.datefromPicker);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(663, 123);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kriteria Pencarian Data Keuangan";
            // 
            // MonthPicker
            // 
            this.MonthPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.MonthPicker.Location = new System.Drawing.Point(205, 25);
            this.MonthPicker.Name = "MonthPicker";
            this.MonthPicker.Size = new System.Drawing.Size(234, 27);
            this.MonthPicker.TabIndex = 5;
            this.MonthPicker.Visible = false;
            // 
            // CariButton
            // 
            this.CariButton.Location = new System.Drawing.Point(294, 72);
            this.CariButton.Name = "CariButton";
            this.CariButton.Size = new System.Drawing.Size(75, 34);
            this.CariButton.TabIndex = 4;
            this.CariButton.Text = "Cari";
            this.CariButton.UseVisualStyleBackColor = true;
            this.CariButton.Click += new System.EventHandler(this.CariButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Black", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(411, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 41);
            this.label2.TabIndex = 2;
            this.label2.Text = "-";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tanggal Transaksi";
            // 
            // datetoPicker
            // 
            this.datetoPicker.Location = new System.Drawing.Point(444, 25);
            this.datetoPicker.Name = "datetoPicker";
            this.datetoPicker.Size = new System.Drawing.Size(200, 27);
            this.datetoPicker.TabIndex = 1;
            // 
            // datefromPicker
            // 
            this.datefromPicker.Location = new System.Drawing.Point(204, 25);
            this.datefromPicker.Name = "datefromPicker";
            this.datefromPicker.Size = new System.Drawing.Size(200, 27);
            this.datefromPicker.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(684, 29);
            this.panel1.TabIndex = 47;
            // 
            // ReportFinanceSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(688, 169);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ReportFinanceSearchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Laporan Keuangan";
            this.Load += new System.EventHandler(this.ReportFinanceSearchForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button CariButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker datetoPicker;
        private System.Windows.Forms.DateTimePicker datefromPicker;
        private System.Windows.Forms.DateTimePicker MonthPicker;
        private System.Windows.Forms.Panel panel1;
    }
}