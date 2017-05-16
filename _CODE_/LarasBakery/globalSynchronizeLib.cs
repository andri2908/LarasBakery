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
    class globalSynchronizeLib
    {
        private globalUtilities gUtil = new globalUtilities();
        private Data_Access DS = new Data_Access();
        private CultureInfo culture = new CultureInfo("id-ID");

        private void writeTableContentToInsertStatement(string tableName, StreamWriter sw, Data_Access DAccess,
            bool skipAddBranchID = false, string sqlParam = "", string customTableName = "", string fieldForPK = "", string fieldValueForPK = "")
        {
            string sqlCommand = "";
            MySqlDataReader rdr;
            string valueStatement = "";
            int rdrFieldIndex = 0;
            int startIndex = 0;
            DateTime tempDateTime;
            string dateTimeValue;
            object DBValue = null;
            int branchID = 0;
            string namaCabang = "";
            int editFlag = 1;

            string commandStatement = "";
            string fieldName = "";

            branchID = gUtil.loadbranchID(2, out namaCabang);

            if (sqlParam.Length <= 0) // EMPTY SQL PARAM MEANS, READ FROM EDIT FLAG
            { 
                sqlCommand = "SELECT * FROM " + tableName + " WHERE EDITED <> 0";

                if (fieldValueForPK.Length > 0)
                    sqlCommand = sqlCommand + " AND " + fieldForPK + " = '" + fieldValueForPK + "'";
            }
            else
                sqlCommand = sqlParam;

            sw.WriteLine("");
            using (rdr = DAccess.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        if (sqlParam.Length <= 0)
                            editFlag = rdr.GetInt32("EDITED");

                        if (editFlag == 1)
                            commandStatement = "INSERT INTO " + tableName + "(";
                        else
                            commandStatement = "UPDATE " + tableName + " SET ";

                        valueStatement = "";

                        for (rdrFieldIndex = startIndex; rdrFieldIndex < rdr.FieldCount; rdrFieldIndex++)
                        {
                            DBValue = rdr.GetValue(rdrFieldIndex);

                            if (DBValue.ToString().Length > 0)
                            {
                                fieldName = rdr.GetName(rdrFieldIndex);

                                if (editFlag == 2 && fieldName == fieldForPK)
                                    continue;

                                if (editFlag == 1) // INSERT STATEMENT
                                    commandStatement = commandStatement + fieldName + ", ";
                                else // UPDATE STATEMENT
                                    valueStatement = valueStatement + fieldName + " = ";

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
                                {
                                    valueStatement = valueStatement + "'" + Convert.ToString(rdr.GetValue(rdrFieldIndex)) + "', ";
                                }
                            }
                        }

                        if (!skipAddBranchID)
                        {
                            if (editFlag == 1)
                            {
                                commandStatement = commandStatement + "BRANCH_ID) VALUES(";
                                valueStatement = valueStatement + branchID + ");";
                            }
                            else
                            {
                                valueStatement = valueStatement + "BRANCH_ID = " + branchID;
                            }
                        }
                        else
                        {
                            if (editFlag == 1)
                            {
                                commandStatement = commandStatement.Substring(0, commandStatement.Length - 2);
                                commandStatement = commandStatement + ") VALUES(";

                                valueStatement = valueStatement.Substring(0, valueStatement.Length - 2);
                                valueStatement = valueStatement + ");";
                            }
                            else
                            {
                                valueStatement = valueStatement.Substring(0, valueStatement.Length - 2);
                            }
                        }

                        commandStatement = commandStatement + valueStatement;

                        if (editFlag == 2)
                            commandStatement = commandStatement + " WHERE " + fieldForPK + " = '" + fieldValueForPK + "';";
                        //else
                        //    commandStatement = commandStatement + ";";

                        sw.WriteLine(commandStatement);
                    }
                }
                rdr.Close();
            }
            sw.WriteLine("");
        }

        private void startExportData(string fileName, string tableName = "", string PKField = "", string keyword = "")
        {
           StreamWriter sw = null;
           string dateFrom = String.Format(culture, "{0:yyyyMMdd}", DateTime.Now);

           if (!File.Exists(fileName))
               sw = File.CreateText(fileName);
           else
           {
               File.Delete(fileName);
               sw = File.CreateText(fileName);
           }

            writeTableContentToInsertStatement(tableName, sw, DS, false, "", "", PKField, keyword);

            sw.Close();
        }

        private void startExportDataCloseShop(string fileName)
        {
            string sqlCommand;
            StreamWriter sw = null;
            string dateFrom = String.Format(culture, "{0:yyyyMMdd}", DateTime.Now);

            if (!File.Exists(fileName))
                sw = File.CreateText(fileName);
            else
            {
                File.Delete(fileName);
                sw = File.CreateText(fileName);
            }

            // EXPORT SALES HEADER
            sqlCommand = "SELECT * FROM SALES_HEADER WHERE SALES_PAID = 1 AND REFERENCE_SO = ''";
            writeTableContentToInsertStatement("SALES_HEADER", sw, DS, false, sqlCommand);

            // EXPORT SALES DETAIL
            sqlCommand = "SELECT SD.* FROM SALES_DETAIL SD, SALES_HEADER SH WHERE SH.SALES_PAID = 1 AND SD.SALES_INVOICE = SH.SALES_INVOICE AND SH.REFERENCE_SO = ''";
            writeTableContentToInsertStatement("SALES_DETAIL", sw, DS, false, sqlCommand);

            // EXPORT CREDIT
            sqlCommand = "SELECT C.* FROM CREDIT C, SALES_HEADER SH WHERE SH.SALES_PAID = 1 AND C.SALES_INVOICE = SH.SALES_INVOICE AND C.CREDIT_PAID = 1 AND SH.REFERENCE_SO = ''";
            writeTableContentToInsertStatement("CREDIT", sw, DS, false, sqlCommand);

            // EXPORT PAYMENT_CREDIT
            sqlCommand = "SELECT PC.* FROM PAYMENT_CREDIT PC, CREDIT C, SALES_HEADER SH WHERE SH.SALES_PAID = 1 AND C.SALES_INVOICE = SH.SALES_INVOICE AND PC.CREDIT_ID = C.CREDIT_ID AND C.CREDIT_PAID  = 1 AND SH.REFERENCE_SO = ''";
            writeTableContentToInsertStatement("PAYMENT_CREDIT", sw, DS, false, sqlCommand);

            // EXPORT DAILY JOURNAL
            sqlCommand = "SELECT * FROM DAILY_JOURNAL";// WHERE DATE_FORMAT(JOURNAL_DATETIME, '%Y%m%d')  = '" + dateFrom + "'";
            writeTableContentToInsertStatement("DAILY_JOURNAL", sw, DS, false, sqlCommand);

            // EXPORT DEBT
            sqlCommand = "SELECT D.* FROM DEBT D, PURCHASE_HEADER PH WHERE PH.PURCHASE_PAID = 1 AND D.PURCHASE_INVOICE = PH.PURCHASE_INVOICE AND D.DEBT_PAID = 1";
            writeTableContentToInsertStatement("DEBT", sw, DS, false, sqlCommand);

            // EXPORT PAYMENT DEBT
            sqlCommand = "SELECT PD.* FROM PAYMENT_DEBT PD, DEBT D, PURCHASE_HEADER PH WHERE PH.PURCHASE_PAID = 1 AND D.PURCHASE_INVOICE = PH.PURCHASE_INVOICE AND PD.DEBT_ID = D.DEBT_ID AND D.DEBT_PAID  = 1";
            writeTableContentToInsertStatement("PAYMENT_DEBT", sw, DS, false, sqlCommand);
            //writeTableContentToInsertStatement(tableName, sw, DS, false, "", "", PKField, keyword);

            sw.Close();
        }

        private void exportData(string fileName, string tableName = "", string PKField = "", string keyword = "")
        {
            smallPleaseWait pleaseWait = new smallPleaseWait();
            pleaseWait.Show();

            //  ALlow main UI thread to properly display please wait form.
            Application.DoEvents();
            startExportData(fileName, tableName, PKField, keyword);

            pleaseWait.Close();
        }

        private void exportDataCloseShop(string fileName)
        {
            smallPleaseWait pleaseWait = new smallPleaseWait();
            pleaseWait.Show();

            //  ALlow main UI thread to properly display please wait form.
            Application.DoEvents();
            startExportDataCloseShop(fileName);

            pleaseWait.Close();
        }

        private bool syncLocalDataToServer(Data_Access localDS, string fileName, int serverToConnect = 1)
        {
            // SEND DATA TO SERVER
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            string sqlCommand = "";
            MySqlException internalEX = null;
            bool result = false;

            localDS.beginTransaction(serverToConnect);

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
                gUtil.saveSystemDebugLog(0, "[SYNC] FAILED TO SYNC LOCAL DATA TO SERVER [" + ex.Message + "]");
            }

            return result;
        }

        public bool sendDataToServer(string tableName = "", string PKField = "", string keyword = "")
        {
            bool result = false;

            Data_Access DS_HQ = new Data_Access();
            string localDate = "";
            string fileName = "";

            localDate = String.Format(culture, "{0:ddMMyyyy}", DateTime.Now);
            fileName = "EXPORT_LOCAL_DATA_" + tableName + "_" + localDate + ".sql";

            // EXPORT LOCAL DATA
            exportData(fileName, tableName, PKField, keyword);

            if (DS_HQ.HQ_mySQLConnect(true))
            {
                gUtil.saveSystemDebugLog(0, "[SYNC] CONNECTION TO SERVER CREATED");

                result = syncLocalDataToServer(DS_HQ, fileName, Data_Access.HQ_SERVER);

                try
                {
                    File.Delete(fileName);
                }
                catch (Exception ex)
                {
                    gUtil.saveSystemDebugLog(0, "[SYNC] FAILED TO DELETE EXPORT FILE [" + ex.Message + "]");
                }
            }
            else
            {
                MessageBox.Show("KONEKSI KE PUSAT GAGAL");
                gUtil.saveSystemDebugLog(0, "[SYNC] FAILED TO CONNECT TO SERVER");

                result = false;
            }

            return result;
        }

        public bool syncDataForCloseShop()
        {
            bool result = false;

            Data_Access DS_SS = new Data_Access();
            string localDate = "";
            string fileName = "";

            localDate = String.Format(culture, "{0:ddMMyyyy}", DateTime.Now);
            fileName = "EXPORT_LOCAL_DATA" + "_" + localDate + ".sql";

            // EXPORT LOCAL DATA
            exportDataCloseShop(fileName);

            if (DS_SS.SS_mySQLConnect(true))
            {
                gUtil.saveSystemDebugLog(0, "[SYNC] CONNECTION TO SERVER CREATED");

                result = syncLocalDataToServer(DS_SS, fileName, Data_Access.SS_SERVER);

                try
                {
                    //File.Delete(fileName);
                }
                catch (Exception ex)
                {
                    gUtil.saveSystemDebugLog(0, "[SYNC] FAILED TO DELETE EXPORT FILE [" + ex.Message + "]");
                }
            }
            else
            {
                MessageBox.Show("KONEKSI KE PUSAT GAGAL");
                gUtil.saveSystemDebugLog(0, "[SYNC] FAILED TO CONNECT TO SERVER");

                result = false;
            }

            return result;
        }

        public void updateSyncFlag(string tableName, string PKField = "", string PKFieldValue = "")
        {
            string sqlCommand = "";
            MySqlException internalEX = null;

            DS.beginTransaction();

            try
            {
                sqlCommand = "UPDATE " + tableName + " SET SYNCHRONIZED = 1, EDITED = 0";

                if (PKField.Length>0)
                    sqlCommand = sqlCommand + " WHERE " + PKField + " = '" + PKFieldValue + "'";

                if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                    throw internalEX;

                DS.commit();
            }
            catch (Exception ex)
            {
                gUtil.saveSystemDebugLog(0, "FAILED TO SET SYNC FIELD [" + ex.Message + "]");
            }
        }

        private void updateLastSuccessDate()
        {
            
        }
    }
}
