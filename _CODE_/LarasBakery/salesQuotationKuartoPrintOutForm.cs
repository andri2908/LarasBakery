﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace AlphaSoft
{
    public partial class salesQuotationKuartoPrintOutForm : Form
    {
        private globalUtilities gUtil = new globalUtilities();

        public salesQuotationKuartoPrintOutForm()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            DataSet dsTempReport = new DataSet();
            try
            {
                string appPath = Directory.GetCurrentDirectory() + "\\" + globalConstants.salesQuotationReceiptXML;
                dsTempReport.ReadXml(@appPath);

                //prepare report for preview
                SalesQuotationKuarto rptXMLReport = new SalesQuotationKuarto();
                CrystalDecisions.CrystalReports.Engine.TextObject txtReportHeader1, txtReportHeader2;
                txtReportHeader1 = rptXMLReport.ReportDefinition.ReportObjects["NamaTokoLabel"] as TextObject;
                txtReportHeader2 = rptXMLReport.ReportDefinition.ReportObjects["InfoTokoLabel"] as TextObject;
                String nama, alamat, telepon, email;
                if (!gUtil.loadinfotoko(2, out nama, out alamat, out telepon, out email))
                {
                    //reset default optsi = 1
                    if (!gUtil.loadinfotoko(1, out nama, out alamat, out telepon, out email))
                    {
                        nama = "TOKO DEFAULT";
                        alamat = "ALAMAT DEFAULT";
                        telepon = "0271-XXXXXXX";
                        email = "A@B.COM";
                    }
                }
                txtReportHeader1.Text = nama;
                txtReportHeader2.Text = alamat + Environment.NewLine + telepon + Environment.NewLine + email;
                rptXMLReport.Database.Tables[0].SetDataSource(dsTempReport.Tables[0]);

                globalPrinterUtility gPrinter = new globalPrinterUtility();
                rptXMLReport.PrintOptions.PrinterName = gPrinter.getConfigPrinterName(2);
                rptXMLReport.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)gPrinter.getReportPaperSize(globalPrinterUtility.LETTER_PAPER_SIZE);
                rptXMLReport.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;

                crystalReportViewer1.ReportSource = rptXMLReport;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
