using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.Reflection;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.IO;

namespace AlphaSoft
{
    class globalCloseShopUtilities
    {
        private globalUtilities gUtil = new globalUtilities();
        private globalSynchronizeLib gSync = new globalSynchronizeLib();
        private Data_Access DS = new Data_Access();
        private CultureInfo culture = new CultureInfo("id-ID");
        private System.Drawing.Printing.PrintDocument printDocument1 = new System.Drawing.Printing.PrintDocument();
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
        private System.Windows.Forms.SaveFileDialog saveFileDialog1 = new SaveFileDialog();

        private bool localSuccess = false;
        private bool serverSuccess = false;
        private bool firstTimeClose = false;

        string localSuccessDate;
        string serverSuccessDate;

        int paperLength = 500;

        private int calculatePaperLength()
        {
            int result = 0;

            MySqlDataReader rdr;

            //event printing
            string dateFrom = String.Format(culture, "{0:yyyyMMdd}", DateTime.Now);

            Font font = new Font("Courier New", 9);
            int rowheight = (int)Math.Ceiling(font.GetHeight());
            int add_offset = rowheight;
            int Offset = 5;
            int totalLength = Offset;
            string sqlCommand = "";


            Offset = Offset + add_offset;

            Offset = Offset + add_offset;

            Offset = Offset + add_offset;

            Offset = Offset + add_offset;

            DS.mySqlConnect();

            sqlCommand = "SELECT MP.PRODUCT_NAME, IFNULL(PD.PRODUCT_SOLD_QTY, 0) AS PRODUCT_SOLD_QTY, IFNULL((PRODUCT_LAST_STOCK_QTY - PRODUCT_RIIL_QTY), 0) AS SELISIH " +
                                    "FROM MASTER_PRODUCT MP, PRODUCT_DAILY_ADJUSTMENT_HEADER PH, PRODUCT_DAILY_ADJUSTMENT_DETAIL PD " +
                                    "WHERE PD.PRODUCT_ID = MP.PRODUCT_ID AND DATE_FORMAT(PH.PRODUCT_ADJUSTMENT_DATE, '%Y%m%d')  = '" + dateFrom + "' AND PD.PRODUCT_ADJUSTMENT_ID = PH.PRODUCT_ADJUSTMENT_ID";

            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    int i = 0;
                    while (rdr.Read())
                    {
                        Offset = Offset + add_offset;
                    }
                }
            }
            DS.mySqlClose();

            Offset = Offset + add_offset;
            Offset = Offset + add_offset;

            Offset = Offset + add_offset;

            Offset = Offset + add_offset;

            // =========================================================================
            Offset = Offset + add_offset;

            Offset = Offset + add_offset;
            // =========================================================================

            Offset = Offset + add_offset;

            Offset = Offset + add_offset;
            // =========================================================================
            Offset = Offset + add_offset;

            Offset = Offset + add_offset;
            // =========================================================================
            Offset = Offset + add_offset;
            Offset = Offset + add_offset;
            // =========================================================================

            Offset = Offset + add_offset;
            Offset = Offset + add_offset;
            // =========================================================================

            Offset = Offset + add_offset;

            Offset = Offset + add_offset;
            Offset = Offset + add_offset;

            Offset = Offset + add_offset;
            Offset = Offset + add_offset;
            Offset = Offset + add_offset;
            Offset = Offset + add_offset;
            Offset = Offset + add_offset;
            Offset = Offset + add_offset;

            //DS.mySqlConnect();

            //sqlCommand = "SELECT DJ.JOURNAL_DESCRIPTION, IFNULL(DJ.JOURNAL_NOMINAL, 0) AS NOMINAL " +
            //                       "FROM DAILY_JOURNAL DJ, MASTER_ACCOUNT MA " +
            //                       "WHERE DATE_FORMAT(DJ.JOURNAL_DATETIME, '%Y%m%d')  = '" + dateFrom + "' AND DJ.ACCOUNT_ID > 3 AND MA.ACCOUNT_TYPE_ID = 2 AND DJ.ACCOUNT_ID = MA.ACCOUNT_ID";

            //using (rdr = DS.getData(sqlCommand))
            //{
            //    if (rdr.HasRows)
            //    {
            //        int i = 0;
            //        while (rdr.Read())
            //        {
            //            Offset = Offset + add_offset;
            //        }
            //    }
            //}
            //DS.mySqlClose();

            Offset = Offset + add_offset;

            Offset = Offset + add_offset;
            Offset = Offset + add_offset;
            Offset = Offset + add_offset;

            result = totalLength  + Offset + add_offset + add_offset;

            return result;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            String ucapan = "";
            MySqlDataReader rdr;

            //event printing
            Graphics graphics = e.Graphics;
            int startX = 0;
            int startY = 0;
            int colxwidth = 85; //old 75
            int totrowwidth = 255; //old 250
            Font font = new Font("Courier New", 9);
            int rowheight = (int)Math.Ceiling(font.GetHeight());

            int add_offset = rowheight;
            int Offset = 5;
            int offset_plus = 3;
            int fontSize = 8;
            String underLine = "------------------------------";  //32 character
            string sqlCommand = "";
            string whereClause = "";

            //set allignemnt
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Near;
            sf.Alignment = StringAlignment.Near;

            //set whole printing area
            System.Drawing.RectangleF rect = new System.Drawing.RectangleF(startX, startY + Offset, totrowwidth, rowheight);
            //set right print area
            System.Drawing.RectangleF rectright = new System.Drawing.RectangleF(totrowwidth - colxwidth - startX, startY + Offset, colxwidth, rowheight);
            //set middle print area
            System.Drawing.RectangleF rectcenter = new System.Drawing.RectangleF((startX + totrowwidth), startY + Offset, colxwidth, rowheight);
            ///string dateFrom = String.Format(culture, "{0:yyyyMMdd}", DateTime.Now);

            ucapan = gUtil.getCustomStringFormatDate(DateTime.Now);
            graphics.DrawString(ucapan, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            graphics.DrawString(underLine, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "LAPORAN STOK";
            graphics.DrawString(ucapan, new Font("Courier New", 9),
                                new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            graphics.DrawString(underLine, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "PENJUALAN DAN SELISIH";
            graphics.DrawString(ucapan,
                     new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);

            //DETAIL PENJUALAN
            sf.LineAlignment = StringAlignment.Near;
            sf.Alignment = StringAlignment.Near;

            float startRightX = totrowwidth - colxwidth - startX;
            string soldQty, selisihQty;

            DS.mySqlConnect();

            sqlCommand = "SELECT MP.PRODUCT_NAME, IFNULL(SUM(PD.PRODUCT_SOLD_QTY), 0) AS PRODUCT_SOLD_QTY, IFNULL(SUM(PRODUCT_LEFTOVER_QTY - PRODUCT_RIIL_QTY), 0) AS SELISIH " +
                                    "FROM MASTER_PRODUCT MP, PRODUCT_DAILY_ADJUSTMENT_HEADER PH, PRODUCT_DAILY_ADJUSTMENT_DETAIL PD " +
                                    "WHERE PD.PRODUCT_ID = MP.PRODUCT_ID AND PD.PRODUCT_ADJUSTMENT_ID = PH.PRODUCT_ADJUSTMENT_ID";

            if (!firstTimeClose)
                whereClause = " AND DATE_FORMAT(PH.PRODUCT_ADJUSTMENT_DATE, '%Y%m%d')  > '" + localSuccessDate + "'";

            sqlCommand = sqlCommand + " " + whereClause + " GROUP BY MP.PRODUCT_ID"; 

            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    int i = 0;
                    while (rdr.Read())
                    {
                        Offset = Offset + add_offset;
                        rect.Y = startY + Offset;

                        ucapan = rdr.GetString("PRODUCT_NAME");
                        if (ucapan.Length > 25)
                        {
                            ucapan = ucapan.Substring(0, 25); //maximum 25 character
                        }
                        else
                            ucapan = ucapan.PadRight(25, ' ');

                        soldQty = rdr.GetString("PRODUCT_SOLD_QTY");
                        selisihQty = rdr.GetString("SELISIH");

                        soldQty = soldQty.PadLeft(3, ' ');
                        selisihQty = selisihQty.PadLeft(3, ' ');

                        ucapan = ucapan + "|" + soldQty + "|" + selisihQty + "|";

                        //
                        graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                                 new SolidBrush(Color.Black), rect, sf);
                    }
                }
            }
            DS.mySqlClose();

            Offset = Offset + add_offset;
            Offset = Offset + add_offset;
            rect.Y = startY + Offset;

            graphics.DrawString(underLine, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "LAPORAN KAS";
            graphics.DrawString(ucapan, new Font("Courier New", 9),
                                new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            graphics.DrawString(underLine, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), rect, sf);

            // =========================================================================
            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "Penjualan Tunai";
            graphics.DrawString(ucapan, new Font("Courier New", 9),
                                new SolidBrush(Color.Black), rect, sf);

            //sqlCommand = "SELECT IFNULL(SUM(SALES_TOTAL - SALES_DISCOUNT_FINAL), 0) AS TOTAL_SALES " +
            //                        "FROM SALES_HEADER " +
            //                        "WHERE SALES_TOP = 1 AND SALES_PAID = 1";
            sqlCommand = "SELECT IFNULL(SUM(PAYMENT_NOMINAL), 0) AS TOTAL_CREDIT " +
                                    "FROM PAYMENT_CREDIT " +
                                    "WHERE PAYMENT_CONFIRMED = 1 AND PM_ID = 1 AND PAYMENT_IS_DP = 0";

            if (!firstTimeClose)
                //whereClause = " AND DATE_FORMAT(SALES_DATE, '%Y%m%d')  > '" + localSuccessDate + "'";
                whereClause = " AND DATE_FORMAT(PAYMENT_DATE, '%Y%m%d')  > '" + localSuccessDate + "'";

            sqlCommand = sqlCommand + " " + whereClause;

            double totalCashSales;
            totalCashSales = Convert.ToDouble(DS.getDataSingleValue(sqlCommand));

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = totalCashSales.ToString("C", culture);
            graphics.DrawString(ucapan, new Font("Courier New", 9),
                                new SolidBrush(Color.Black), rect, sf);
            // =========================================================================

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "Pendapatan Tunai non Penjualan";
            graphics.DrawString(ucapan, new Font("Courier New", 9),
                                new SolidBrush(Color.Black), rect, sf);

            sqlCommand = "SELECT IFNULL(SUM(DJ.JOURNAL_NOMINAL), 0) AS TOTAL_NON_SALES " +
                                   "FROM DAILY_JOURNAL DJ, MASTER_ACCOUNT MA " +
                                   "WHERE DJ.ACCOUNT_ID > 3 AND MA.ACCOUNT_TYPE_ID = 1 AND DJ.ACCOUNT_ID = MA.ACCOUNT_ID";

            if (!firstTimeClose)
                whereClause = " AND DATE_FORMAT(DJ.JOURNAL_DATETIME, '%Y%m%d')  > '" + localSuccessDate + "'";

            sqlCommand = sqlCommand + " " + whereClause;

            double totalCashNonSales;
            totalCashNonSales = Convert.ToDouble(DS.getDataSingleValue(sqlCommand));

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = totalCashNonSales.ToString("C", culture);
            graphics.DrawString(ucapan, new Font("Courier New", 9),
                                new SolidBrush(Color.Black), rect, sf);
            // =========================================================================

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "Pendapatan DP";
            graphics.DrawString(ucapan, new Font("Courier New", 9),
                                new SolidBrush(Color.Black), rect, sf);

            sqlCommand = "SELECT IFNULL(SUM(PAYMENT_NOMINAL), 0) AS TOTAL_DP " +
                                    "FROM PAYMENT_CREDIT " +
                                    "WHERE PAYMENT_CONFIRMED = 1 AND PM_ID = 1 AND PAYMENT_IS_DP = 1";

            //sqlCommand = "SELECT IFNULL(SUM(DJ.JOURNAL_NOMINAL), 0) AS NOMINAL " +
            //                     "FROM DAILY_JOURNAL DJ " +
            //                     "WHERE JOURNAL_DESCRIPTION LIKE '%PEMBAYARAN DP [%'";

            if (!firstTimeClose)
                whereClause = " AND DATE_FORMAT(PAYMENT_DATE, '%Y%m%d')  > '" + localSuccessDate + "'";

            sqlCommand = sqlCommand + " " + whereClause;

            double totalDP;
            totalDP = Convert.ToDouble(DS.getDataSingleValue(sqlCommand));

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = totalDP.ToString("C", culture);
            graphics.DrawString(ucapan, new Font("Courier New", 9),
                                new SolidBrush(Color.Black), rect, sf);
            // =========================================================================

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "Pendapatan Pesanan";
            graphics.DrawString(ucapan, new Font("Courier New", 9),
                                new SolidBrush(Color.Black), rect, sf);

            //sqlCommand = "SELECT IFNULL(SUM(PAYMENT_NOMINAL), 0) AS TOTAL_CREDIT " +
            //                        "FROM PAYMENT_CREDIT " +
            //                        "WHERE PAYMENT_CONFIRMED = 1 AND PM_ID = 1 AND PAYMENT_IS_DP = 0";

            sqlCommand = "SELECT IFNULL(SUM(SALES_TOTAL - SALES_DISCOUNT_FINAL), 0) AS TOTAL_SALES " +
                                    "FROM SALES_HEADER " +
                                    "WHERE SALES_TOP = 0";

            if (!firstTimeClose)
                whereClause = " AND DATE_FORMAT(SALES_DATE, '%Y%m%d')  > '" + localSuccessDate + "'";

            sqlCommand = sqlCommand + " " + whereClause;

            double totalCreditPayment = 0;
            totalCreditPayment = Convert.ToDouble(DS.getDataSingleValue(sqlCommand));

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = totalCreditPayment.ToString("C", culture);
            graphics.DrawString(ucapan, new Font("Courier New", 9),
                                new SolidBrush(Color.Black), rect, sf);
            // =========================================================================

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            graphics.DrawString(underLine, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), rect, sf);

            double totalIncome;
            totalIncome = totalCashSales + totalCashNonSales + totalDP + totalCreditPayment;

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "TOTAL : " + totalIncome.ToString("C", culture); ;
            graphics.DrawString(ucapan, new Font("Courier New", 9),
                                new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            graphics.DrawString(underLine, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), rect, sf);
            // =========================================================================

            Offset = Offset + add_offset;
            Offset = Offset + add_offset;

            rect.Y = startY + Offset;
            ucapan = "Pengeluaran";
            graphics.DrawString(ucapan, new Font("Courier New", 9),
                                new SolidBrush(Color.Black), rect, sf);

            sqlCommand = "SELECT IFNULL(SUM(DJ.JOURNAL_NOMINAL), 0) AS NOMINAL " +
                                   "FROM DAILY_JOURNAL DJ, MASTER_ACCOUNT MA " +
                                   "WHERE DJ.ACCOUNT_ID > 3 AND MA.ACCOUNT_TYPE_ID = 2 AND DJ.ACCOUNT_ID = MA.ACCOUNT_ID";

            if (!firstTimeClose)
                whereClause = " AND DATE_FORMAT(DJ.JOURNAL_DATETIME, '%Y%m%d')  > '" + localSuccessDate + "'";

            sqlCommand = sqlCommand + " " + whereClause;

            double expenseAmt = 0;
            expenseAmt = Convert.ToDouble(DS.getDataSingleValue(sqlCommand));

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = expenseAmt.ToString("C", culture);
            graphics.DrawString(ucapan, new Font("Courier New", 9),
                                new SolidBrush(Color.Black), rect, sf);
            // =========================================================================

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "Pembayaran Supplier";
            graphics.DrawString(ucapan, new Font("Courier New", 9),
                                new SolidBrush(Color.Black), rect, sf);

            sqlCommand = "SELECT IFNULL(SUM(PAYMENT_NOMINAL), 0) AS TOTAL_PAYMENT_DEBT " +
                                    "FROM PAYMENT_DEBT " +
                                    "WHERE PAYMENT_CONFIRMED = 1 AND PM_ID = 1";

            if (!firstTimeClose)
                whereClause = " AND DATE_FORMAT(PAYMENT_CONFIRMED_DATE, '%Y%m%d')  > '" + localSuccessDate + "'";

            sqlCommand = sqlCommand + " " + whereClause;

            double totalDebtPayment;
            totalDebtPayment = Convert.ToDouble(DS.getDataSingleValue(sqlCommand));

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = totalDebtPayment.ToString("C", culture);
            graphics.DrawString(ucapan, new Font("Courier New", 9),
                                new SolidBrush(Color.Black), rect, sf);
            // =========================================================================

            //DS.mySqlConnect();

            //sqlCommand = "SELECT DJ.JOURNAL_DESCRIPTION, IFNULL(DJ.JOURNAL_NOMINAL, 0) AS NOMINAL " +
            //                       "FROM DAILY_JOURNAL DJ, MASTER_ACCOUNT MA " +
            //                       "WHERE DATE_FORMAT(DJ.JOURNAL_DATETIME, '%Y%m%d')  = '" + dateFrom + "' AND DJ.ACCOUNT_ID > 3 AND MA.ACCOUNT_TYPE_ID = 2 AND DJ.ACCOUNT_ID = MA.ACCOUNT_ID";

            //using (rdr = DS.getData(sqlCommand))
            //{
            //    if (rdr.HasRows)
            //    {
            //        int i = 0;
            //        while (rdr.Read())
            //        {
            //            Offset = Offset + add_offset;
            //            rect.Y = startY + Offset;

            //            ucapan = rdr.GetString("JOURNAL_DESCRIPTION");
            //            if (ucapan.Length > 20)
            //            {
            //                ucapan = ucapan.Substring(0, 20); //maximum 20 character
            //            }
            //            else
            //                ucapan = ucapan.PadRight(20, ' ');

            //            expenseAmt = rdr.GetDouble("JOURNAL_NOMINAL");
            //            totalExpense = totalExpense + expenseAmt;

            //            ucapan = ucapan + "|" + expenseAmt.ToString("C", culture);

            //            //
            //            graphics.DrawString(ucapan, new Font("Courier New", fontSize),
            //                     new SolidBrush(Color.Black), rect, sf);
            //        }
            //    }
            //}
            //DS.mySqlClose();

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            graphics.DrawString(underLine, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), rect, sf);

            double totalExpense = 0;
            totalExpense = expenseAmt + totalDebtPayment;

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "TOTAL : " + totalExpense.ToString("C", culture);
            graphics.DrawString(ucapan, new Font("Courier New", 9),
                                new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            graphics.DrawString(underLine, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), rect, sf);

            double totalFinalCash = 0;
            totalFinalCash = totalIncome + totalExpense;

            Offset = Offset + add_offset;
            Offset = Offset + add_offset;

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            graphics.DrawString(underLine, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "KAS GRANDTOTAL : " + totalFinalCash.ToString("C", culture);
            graphics.DrawString(ucapan, new Font("Courier New", 9),
                                new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            graphics.DrawString(underLine, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), rect, sf);
        }

        private void printOutSummary()
        {
            printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument1_PrintPage);
            printPreviewDialog1.Document = printDocument1;

            paperLength = calculatePaperLength();

            PaperSize psize = new PaperSize("Custom", 255, paperLength);
            printDocument1.DefaultPageSettings.PaperSize = psize;
            DialogResult result;
            printPreviewDialog1.Width = 512;
            printPreviewDialog1.Height = 768;

            result = printPreviewDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void writeTableContentToInsertStatement(string tableName, StreamWriter sw, Data_Access DAccess, bool isHQConnection = false, bool skipFirstField = false, bool skipAddBranchID = false, string sqlParam = "", string customTableName = "")
        {
            string sqlCommand = "";
            MySqlDataReader rdr;
            string insertStatement = "";
            string valueStatement = "";
            int rdrFieldIndex = 0;
            int startIndex = 0;
            DateTime tempDateTime;
            string dateTimeValue;
            object DBValue = null;
            int branchID = 0;
            string namaCabang = "";

            branchID = gUtil.loadbranchID(2, out namaCabang);

            if (sqlParam.Length <= 0)
                sqlCommand = "SELECT * FROM " + tableName;
            else
                sqlCommand = sqlParam;

            if (skipFirstField)
                startIndex = 1;

            sw.WriteLine("");
            using (rdr = DAccess.getData(sqlCommand, isHQConnection))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        if (customTableName.Length > 0)
                            insertStatement = "INSERT INTO " + customTableName + "(";
                        else
                            insertStatement = "INSERT INTO " + tableName + "(";

                        valueStatement = "";

                        for (rdrFieldIndex = startIndex; rdrFieldIndex < rdr.FieldCount; rdrFieldIndex++)
                        {
                            DBValue = rdr.GetValue(rdrFieldIndex);

                            if (DBValue.ToString().Length > 0)
                            {
                                insertStatement = insertStatement + rdr.GetName(rdrFieldIndex) + ", ";

                                if (rdr.GetDataTypeName(rdrFieldIndex) == "DATE")
                                {
                                    tempDateTime = rdr.GetDateTime(rdrFieldIndex);
                                    dateTimeValue = String.Format(culture, "{0:dd-MM-yyyy}", tempDateTime);

                                    valueStatement = valueStatement + "STR_TO_DATE('" + dateTimeValue + "', '%d-%m-%Y'), ";
                                }
                                else if (rdr.GetDataTypeName(rdrFieldIndex) == "DATETIME")
                                {
                                    tempDateTime = rdr.GetDateTime(rdrFieldIndex);
                                    dateTimeValue = gUtil.getCustomStringFormatDate(tempDateTime);

                                    valueStatement = valueStatement + "STR_TO_DATE('" + dateTimeValue + "', '%d-%m-%Y %H:%i'), ";
                                }
                                else
                                    valueStatement = valueStatement + "'" + Convert.ToString(rdr.GetValue(rdrFieldIndex)) + "', ";
                            }
                        }

                        if (!skipAddBranchID)
                        {
                            insertStatement = insertStatement + "BRANCH_ID) VALUES(";
                            valueStatement = valueStatement + branchID + ");";
                        }
                        else
                        {
                            insertStatement = insertStatement.Substring(0, insertStatement.Length - 2);
                            insertStatement = insertStatement + ") VALUES(";

                            valueStatement = valueStatement.Substring(0, valueStatement.Length - 2);
                            valueStatement = valueStatement + ");";
                        }

                        insertStatement = insertStatement + valueStatement;

                        sw.WriteLine(insertStatement);
                    }
                }
                rdr.Close();
            }
            sw.WriteLine("");
        }

        private void startExportData(string fileName, Data_Access DAccess)
        {
            StreamWriter sw = null;
            string sqlCommand = "";
            string strCmdText = "USE 'sys_pos_larasbakery_server';";
            string dateFrom = String.Format(culture, "{0:yyyyMMdd}", DateTime.Now);

            if (!File.Exists(fileName))
                sw = File.CreateText(fileName);
            else
            {
                File.Delete(fileName);
                sw = File.CreateText(fileName);
            }
            //sw.WriteLine(strCmdText);

            // EXPORT SALES HEADER
            sqlCommand = "SELECT * FROM SALES_HEADER WHERE SALES_PAID = 1";
            writeTableContentToInsertStatement("SALES_HEADER", sw, DAccess, false, false, false, sqlCommand);

            // EXPORT SALES DETAIL
            sqlCommand = "SELECT SD.* FROM SALES_DETAIL SD, SALES_HEADER SH WHERE SH.SALES_PAID = 1 AND SD.SALES_INVOICE = SH.SALES_INVOICE";
            writeTableContentToInsertStatement("SALES_DETAIL", sw, DAccess, false, false, false, sqlCommand);

            // EXPORT CREDIT
            sqlCommand = "SELECT C.* FROM CREDIT C, SALES_HEADER SH WHERE SH.SALES_PAID = 1 AND C.SALES_INVOICE = SH.SALES_INVOICE AND C.CREDIT_PAID = 1";
            writeTableContentToInsertStatement("CREDIT", sw, DAccess, false, false, false, sqlCommand);

            // EXPORT PAYMENT_CREDIT
            sqlCommand = "SELECT PC.* FROM PAYMENT_CREDIT PC, CREDIT C, SALES_HEADER SH WHERE SH.SALES_PAID = 1 AND C.SALES_INVOICE = SH.SALES_INVOICE AND PC.CREDIT_ID = C.CREDIT_ID AND C.CREDIT_PAID  = 1";
            writeTableContentToInsertStatement("PAYMENT_CREDIT", sw, DAccess, false, false, false, sqlCommand);

            // EXPORT DAILY JOURNAL
            sqlCommand = "SELECT * FROM DAILY_JOURNAL";// WHERE DATE_FORMAT(JOURNAL_DATETIME, '%Y%m%d')  = '" + dateFrom + "'";
            writeTableContentToInsertStatement("DAILY_JOURNAL", sw, DAccess, false, false, false, sqlCommand);

            // EXPORT DEBT
            sqlCommand = "SELECT D.* FROM DEBT D, PURCHASE_HEADER PH WHERE PH.PURCHASE_PAID = 1 AND D.PURCHASE_INVOICE = PH.PURCHASE_INVOICE AND D.DEBT_PAID = 1";
            writeTableContentToInsertStatement("DEBT", sw, DAccess, false, false, false, sqlCommand);

            // EXPORT PAYMENT DEBT
            sqlCommand = "SELECT PD.* FROM PAYMENT_DEBT PD, DEBT D, PURCHASE_HEADER PH WHERE PH.PURCHASE_PAID = 1 AND D.PURCHASE_INVOICE = PH.PURCHASE_INVOICE AND PD.DEBT_ID = D.DEBT_ID AND D.DEBT_PAID  = 1";
            writeTableContentToInsertStatement("PAYMENT_DEBT", sw, DAccess, false, false, false, sqlCommand);

            sw.Close();
        }

        private void exportData(string fileName)
        {
            //saveFileDialog1.FileName = fileName;
            //saveFileDialog1.AddExtension = true;
            //saveFileDialog1.DefaultExt = "sql";
            //saveFileDialog1.Filter = "SQL File (.sql)|*.sql";

            //if (DialogResult.OK == saveFileDialog1.ShowDialog())
            {
                smallPleaseWait pleaseWait = new smallPleaseWait();
                pleaseWait.Show();

                //  ALlow main UI thread to properly display please wait form.
                Application.DoEvents();
                startExportData(fileName, DS);

                pleaseWait.Close();

                //gUtil.saveSystemDebugLog(globalConstants.MENU_SINKRONISASI_INFORMASI, "EXPORTED FILE NAME = " + saveFileDialog1.FileName);
            }
        }

        private bool syncLocalDataToServer(Data_Access localDS, string fileName)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            string sqlCommand = "";
            MySqlException internalEX = null;
            bool result = false;

            localDS.beginTransaction(Data_Access.HQ_SERVER);

            try
            {
                while ((sqlCommand = file.ReadLine()) != null)
                {
                    if (sqlCommand.Length > 0)
                        if (!localDS.executeNonQueryCommand(sqlCommand, ref internalEX))
                            throw internalEX;
                }

                file.Close();

                localDS.commit();

                result = true;
            }
            catch (Exception ex)
            {
                gUtil.saveSystemDebugLog(0, "[TUTUP TOKO] FAILED TO SYNC LOCAL DATA TO SERVER [" + ex.Message + "]");
            }

            return result;
        }

        private bool synchronizeDataToServer()
        {
            bool result = false;
            //Data_Access DS_HQ = new Data_Access();
            //string localDate = "";
            //string fileName = "";

            //localDate = String.Format(culture, "{0:ddMMyyyy}", DateTime.Now);
            //fileName = "EXPORT_DATA_" + localDate + ".sql";

            //// EXPORT LOCAL DATA
            //exportData(fileName);

            //if (DS_HQ.HQ_mySQLConnect(true))
            //{
            //    gUtil.saveSystemDebugLog(0, "[TUTUP TOKO] CONNECTION TO CENTRAL HQ CREATED");

            //    result = syncLocalDataToServer(DS_HQ, fileName);

            //    try
            //    {
            //        File.Delete(fileName);
            //    }
            //    catch (Exception ex)
            //    {
            //        gUtil.saveSystemDebugLog(0, "[TUTUP TOKO] FAILED TO DELETE EXPORT FILE [" +ex.Message+ "]");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("KONEKSI KE PUSAT GAGAL");
            //    gUtil.saveSystemDebugLog(0, "[TUTUP TOKO] FAILED TO CONNECT TO CENTRAL HQ");

            //    result = false;
            //}

            return result;
        }

        private void clearDailyTransaction()
        {
            string sqlCommand = "";

            MySqlDataReader rdr;
            MySqlException internalEX = null;
            int creditID = 0;
            int debtID = 0;
            string salesInvoice = "";

            DS.beginTransaction();
            try
            {
                // GET A LIST OF CREDIT_ID 
                sqlCommand = "SELECT SH.SALES_INVOICE, C.CREDIT_ID FROM CREDIT C, SALES_HEADER SH WHERE C.SALES_INVOICE = SH.SALES_INVOICE AND SH.SALES_PAID = 1 AND C.CREDIT_PAID = 1";
                using (rdr = DS.getData(sqlCommand))
                {
                    while (rdr.Read())
                    {
                        salesInvoice = rdr.GetString("SALES_INVOICE");
                        creditID = rdr.GetInt32("CREDIT_ID");

                        // CLEAR SALES DETAIL
                        sqlCommand = "DELETE FROM SALES_DETAIL WHERE SALES_INVOICE = '" + salesInvoice + "'";
                        DS.executeNonQueryCommand(sqlCommand, ref internalEX);

                        // CLEAR SALES HEADER
                        sqlCommand = "DELETE FROM SALES_HEADER WHERE SALES_INVOICE = '" + salesInvoice + "'";
                        DS.executeNonQueryCommand(sqlCommand, ref internalEX);

                        // CLEAR PAYMENT_CREDIT
                        sqlCommand = "DELETE FROM PAYMENT_CREDIT WHERE CREDIT_ID = " + creditID;
                        DS.executeNonQueryCommand(sqlCommand, ref internalEX);

                        // CLEAR CREDIT
                        sqlCommand = "DELETE FROM CREDIT WHERE CREDIT_ID = " + creditID;
                        DS.executeNonQueryCommand(sqlCommand, ref internalEX);
                    }
                }
                rdr.Close();

                // GET A LIST OF DEBT_ID 
                sqlCommand = "SELECT PH.PURCHASE_INVOICE, D.DEBT_ID FROM DEBT D, PURCHASE_HEADER PH WHERE D.PURCHASE_INVOICE = PH.PURCHASE_INVOICE AND PH.PURCHASE_PAID = 1 AND D.DEBT_PAID = 1";
                using (rdr = DS.getData(sqlCommand))
                {
                    while (rdr.Read())
                    {
                        debtID = rdr.GetInt32("DEBT_ID");

                        // CLEAR PAYMENT_CREDIT
                        sqlCommand = "DELETE FROM PAYMENT_DEBT WHERE DEBT_ID = " + debtID;
                        DS.executeNonQueryCommand(sqlCommand, ref internalEX);

                        // CLEAR CREDIT
                        sqlCommand = "DELETE FROM DEBT WHERE DEBT_ID = " + debtID;
                        DS.executeNonQueryCommand(sqlCommand, ref internalEX);
                    }
                }
                rdr.Close();


                DS.commit();
            }
            catch (Exception ex)
            {
                gUtil.saveSystemDebugLog(0, "[TUTUP TOKO] FAIL TO CLEAR DATA SALES [" + ex.Message + "]");
            }
        }

        private void clearDailyJournal()
        {
            string sqlCommand;
            MySqlException internalEX = null;
            string dateFrom = String.Format(culture, "{0:yyyyMMdd}", DateTime.Now);

            DS.beginTransaction();
            try
            {
                sqlCommand = "DELETE FROM DAILY_JOURNAL WHERE DATE_FORMAT(JOURNAL_DATETIME, '%Y%m%d')  = '" + dateFrom + "'";
                DS.executeNonQueryCommand(sqlCommand, ref internalEX);

                DS.commit();
            }
            catch (Exception ex)
            {
                gUtil.saveSystemDebugLog(0, "[TUTUP TOKO] FAIL TO CLEAR DATA JOURNAL [" + ex.Message + "]");
            }
        }

        private void updateLastSuccessDate()
        {
            string sqlCommand= "";
            string localDate = String.Format(culture, "{0:dd-MM-yyyy}", DateTime.Now);
            MySqlException internalEX = null;

            DS.beginTransaction();

            try
            {
                if (firstTimeClose)
                {
                    if (serverSuccess)
                    {
                        sqlCommand = "INSERT INTO CLOSE_SHOP_HISTORY (LAST_SUCCESS_LOCAL_DATE, LAST_SUCCESS_SERVER_DATE) VALUES (" +
                                               "STR_TO_DATE('" + localDate + "', '%d-%m-%Y'), STR_TO_DATE('" + localDate + "', '%d-%m-%Y'))"; 
                    }
                    else
                    {
                        sqlCommand = "INSERT INTO CLOSE_SHOP_HISTORY (LAST_SUCCESS_LOCAL_DATE) VALUES ("+
                                            "STR_TO_DATE('" + localDate + "', '%d-%m-%Y'))";
                    }
                }
                else
                {
                    if (serverSuccess)
                        sqlCommand = "UPDATE CLOSE_SHOP_HISTORY SET LAST_SUCCESS_LOCAL_DATE = STR_TO_DATE('" + localDate + "', '%d-%m-%Y'), LAST_SUCCESS_SERVER_DATE = STR_TO_DATE('" + localDate + "', '%d-%m-%Y')";
                    else
                        sqlCommand = "UPDATE CLOSE_SHOP_HISTORY SET LAST_SUCCESS_LOCAL_DATE = STR_TO_DATE('" + localDate + "', '%d-%m-%Y')";
                }

                if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                     throw internalEX;

                DS.commit();
            }
            catch (Exception ex)
            {
                gUtil.saveSystemDebugLog(0, "[TUTUP TOKO] FAILED TO UPDATE LAST SUCCESS DATE [" + ex.Message + "]");
            }
        }

        public void closeShopProcedure()
        {
            string sqlCommand;
            DateTime defaultSuccessDate = DateTime.Now;
            defaultSuccessDate = defaultSuccessDate.AddDays(-1);

            sqlCommand = "SELECT COUNT(1) FROM CLOSE_SHOP_HISTORY";
            if (Convert.ToInt32(DS.getDataSingleValue(sqlCommand)) <= 0)
                firstTimeClose = true;
            else
            {
                sqlCommand = "SELECT IFNULL(DATE_FORMAT(LAST_SUCCESS_LOCAL_DATE, '%Y%m%d'), '') FROM CLOSE_SHOP_HISTORY LIMIT 1";
                localSuccessDate = DS.getDataSingleValue(sqlCommand).ToString();
                if (localSuccessDate.Length <= 0)
                    localSuccessDate = String.Format(culture, "{0:yyyyMMdd}", defaultSuccessDate);

                sqlCommand = "SELECT IFNULL(DATE_FORMAT(LAST_SUCCESS_SERVER_DATE, '%Y%m%d'), '') FROM CLOSE_SHOP_HISTORY LIMIT 1";
                serverSuccessDate = DS.getDataSingleValue(sqlCommand).ToString();
                if (serverSuccessDate.Length <= 0)
                    serverSuccessDate = String.Format(culture, "{0:yyyyMMdd}", defaultSuccessDate);

                firstTimeClose = false;
            }

            // PRINT OUT SUMMARY
            printOutSummary();

            localSuccess = true;

            if (gUtil.isServerApp() == 0)
            {
                // SEND DATA PESANAN TO PABRIK 
                // ===================================================
                if (gSync.sendDataToServer("MASTER_CUSTOMER"))
                    gSync.updateSyncFlag("MASTER_CUSTOMER");

                if (gSync.sendDataToServer("SALES_HEADER"))
                    gSync.updateSyncFlag("SALES_HEADER");

                if (gSync.sendDataToServer("SALES_DETAIL"))
                    gSync.updateSyncFlag("SALES_DETAIL");

                if (gSync.sendDataToServer("SALES_DETAIL_FULFILLMENT"))
                    gSync.updateSyncFlag("SALES_DETAIL_FULFILLMENT");
                // ===================================================
            }

            // SYNCHRONIZE TO SERVER
            //if (synchronizeDataToServer())
            if (gSync.syncDataForCloseShop())
                serverSuccess = true;

            // EXPORT TO FILE
            //exportData();

            if (serverSuccess)
            {
                // CLEAR DATA PENJUALAN TUNAI LUNAS
                clearDailyTransaction();

                // CLEAR DATA JURNAL HARIAN
                clearDailyJournal();
            }

            updateLastSuccessDate();
        }
    }
}
