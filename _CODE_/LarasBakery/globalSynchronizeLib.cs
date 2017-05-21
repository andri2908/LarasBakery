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
        private List<string> fieldToSkip = new List<string>();
        private List<string> fileToExecute = new List<string>();

        private void writeTableContentToInsertStatement(string tableName, StreamWriter sw, Data_Access DAccess,
            bool skipAddBranchID = false, string sqlParam = "", string customTableName = "", string fieldForPK = "", string fieldValueForPK = "", bool forcedInsert = false)
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
                if (forcedInsert == false)
                {
                    sqlCommand = "SELECT * FROM " + tableName + " WHERE EDITED <> 0";
                }
                else
                {
                    sqlCommand = "SELECT * FROM " + tableName + " WHERE 1 = 1";
                }

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
                        if (forcedInsert == false)
                        { 
                            if (sqlParam.Length <= 0)
                                editFlag = rdr.GetInt32("EDITED");
                        }

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

                                if (fieldToSkip.Contains(fieldName))
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

                        if (editFlag == 2 && fieldForPK.Length > 0 && fieldValueForPK.Length > 0)
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

        private void startExportData(string fileName, string tableName = "", string PKField = "", string keyword = "", string sqlParam = "", bool forcedInsert = false, bool skipBranchID = false)
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

            writeTableContentToInsertStatement(tableName, sw, DS, skipBranchID, sqlParam, "", PKField, keyword, forcedInsert);

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

        private void exportData(string fileName, string tableName = "", string PKField = "", string keyword = "", string sqlParam = "", bool forcedInsert = false, bool skipBranchID = false)
        {
            smallPleaseWait pleaseWait = new smallPleaseWait();
            pleaseWait.Show();

            //  ALlow main UI thread to properly display please wait form.
            Application.DoEvents();
            startExportData(fileName, tableName, PKField, keyword, sqlParam, forcedInsert, skipBranchID);

            pleaseWait.Close();
        }

        //private void exportDataSelective(string fileName, List<string> fieldToSkip, string tableName = "", string PKField = "", string keyword = "", string sqlParam = "", bool forcedInsert = false)
        //{
        //    smallPleaseWait pleaseWait = new smallPleaseWait();
        //    pleaseWait.Show();

        //    //  ALlow main UI thread to properly display please wait form.
        //    Application.DoEvents();
        //    startExportData(fileName, tableName, PKField, keyword, sqlParam);

        //    pleaseWait.Close();
        //}

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
                gUtil.saveSystemDebugLog(0, "[SYNC] FAILED TO SYNC LOCAL DATA ["+ fileName + "] TO SERVER [" + ex.Message + "]");
            }

            return result;
        }

        private bool syncLocalDataToServerMultipleFiles(Data_Access localDS, int serverToConnect = 1)
        {
            // SEND DATA TO SERVER
            System.IO.StreamReader file;// = new System.IO.StreamReader(fileName);
            string sqlCommand = "";
            MySqlException internalEX = null;
            bool result = false;

            localDS.beginTransaction(serverToConnect);

            try
            {
                for (int i =0;i<fileToExecute.Count;i++)
                {
                    file = new System.IO.StreamReader(fileToExecute[i]);

                    while ((sqlCommand = file.ReadLine()) != null)
                    {
                        if (sqlCommand.Length > 0)
                            if (!localDS.executeNonQueryCommand(sqlCommand, ref internalEX))
                                throw internalEX;
                    }

                    file.Close();
                }

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

        private bool clearDataCabang(string tableName, int branchID, string ipServerBranch = "")
        {
            Data_Access DS_BRANCH = new Data_Access();
            bool result = false;
            string sqlCommand = "";
            MySqlException internalEX = null;

            // CONNECT TO BRANCH
            if (DS_BRANCH.Branch_mySQLConnect(branchID, ipServerBranch))
            {
                DS_BRANCH.beginTransaction(Data_Access.BRANCH_SERVER);

                try
                {
                    sqlCommand = "DELETE FROM " + tableName;

                    if (!DS_BRANCH.executeNonQueryCommand(sqlCommand))
                        throw internalEX;

                    DS_BRANCH.commit();
                    result = true;
                }
                catch (Exception ex)
                {
                    gUtil.saveSystemDebugLog(0, "[SYNC CABANG] FAILED TO CLEAR DATA CABANG [" + ex.Message + "]");
                }

                // CLOSE BRANCH CONNECTION
                DS_BRANCH.Branch_mySqlClose();
            }
            else
            {
                gUtil.saveSystemDebugLog(0, "[SYNC CABANG] FAILED TO CONNECT TO CABANG");
            }

            return result;
        }

        private string createUpdateQueryDataCabang(int branchID, string tableName, string PKField, List<string> fieldToBackup, string ipServerBranch = "")
        {
            Data_Access DS_BRANCH = new Data_Access();
            //bool result = false;
            string sqlCommand = "";
            
            MySqlDataReader rdr;
            string fieldName;
            object DBValue = null;
            int rdrFieldIndex = 0;
            int startIndex = 0;
            StreamWriter sw = null;
            string fileName = "";
            string commandStatement = "";
            string valueStatement = "";
            string fieldValueForPK = "";

            fileName = "EXPORT_" + branchID + "_DATA_" + tableName + ".sql";

            if (!File.Exists(fileName))
                sw = File.CreateText(fileName);
            else
            {
                File.Delete(fileName);
                sw = File.CreateText(fileName);
            }

            // CONNECT TO BRANCH
            if (DS_BRANCH.Branch_mySQLConnect(branchID, ipServerBranch))
            {
                sqlCommand = "SELECT * FROM " + tableName;

                using (rdr = DS_BRANCH.getData(sqlCommand, false, true))
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            commandStatement = "UPDATE " + tableName + " SET ";
                            valueStatement = "";
                            for (rdrFieldIndex = startIndex; rdrFieldIndex < rdr.FieldCount; rdrFieldIndex++)
                            {
                                DBValue = rdr.GetValue(rdrFieldIndex);

                                if (DBValue.ToString().Length > 0)
                                {
                                    fieldName = rdr.GetName(rdrFieldIndex);

                                    if (!fieldToSkip.Contains(fieldName) && fieldName != PKField)
                                        continue;

                                    if (fieldName != PKField)
                                        valueStatement = valueStatement + fieldName + " = " + "'" + Convert.ToString(rdr.GetValue(rdrFieldIndex)) + "', ";
                                    else
                                        fieldValueForPK = Convert.ToString(rdr.GetValue(rdrFieldIndex));
                                }
                            }

                            valueStatement = valueStatement.Substring(0, valueStatement.Length - 2);
                            commandStatement = commandStatement + valueStatement;
                            commandStatement = commandStatement + " WHERE " + PKField + " = '" + fieldValueForPK + "';";

                            sw.WriteLine(commandStatement);
                        }
                    }
                }
                rdr.Close();

                // CLOSE BRANCH CONNECTION
                DS_BRANCH.Branch_mySqlClose();

                //result = true;
            }
            else
            {
                gUtil.saveSystemDebugLog(0, "[SYNC CABANG] FAILED TO CONNECT TO CABANG");
            }

            sw.Close();
            return fileName;
        }

        private bool sendDataToCabang(string fileName, int branchID, string ipServerBranch = "")
        {
            Data_Access DS_BRANCH = new Data_Access();
            bool result = false;
            // CONNECT TO BRANCH

            if (DS_BRANCH.Branch_mySQLConnect(branchID, ipServerBranch))
            {
                result = syncLocalDataToServer(DS_BRANCH, fileName, Data_Access.BRANCH_SERVER);

                // CLOSE BRANCH CONNECTION
                DS_BRANCH.Branch_mySqlClose();
            }

            return result;
        }

        private bool sendDataToCabangMultipleFiles(int branchID, string ipServerBranch = "")
        {
            Data_Access DS_BRANCH = new Data_Access();
            bool result = false;
            // CONNECT TO BRANCH

            if (DS_BRANCH.Branch_mySQLConnect(branchID, ipServerBranch))
            {
                result = syncLocalDataToServerMultipleFiles(DS_BRANCH, Data_Access.BRANCH_SERVER);

                // CLOSE BRANCH CONNECTION
                DS_BRANCH.Branch_mySqlClose();
            }

            return result;
        }

        public void syncDataCabang(string tableName, string PKField = "", string keyword = "")
        {
            string sqlCommand = "";
            List<int> branchIDList = new List<int>();
            MySqlDataReader rdr;

            // EXPORT DATA TO FILE
            string localDate = "";
            string fileName = "";

            localDate = String.Format(culture, "{0:ddMMyyyy}", DateTime.Now);
            fileName = "EXPORT_SS_DATA_" + tableName + "_" + localDate + ".sql";

            // EXPORT LOCAL DATA
            exportData(fileName, tableName, PKField, keyword, "", true, true);

            sqlCommand = "SELECT BRANCH_ID FROM MASTER_BRANCH WHERE BRANCH_ACTIVE = 1";

            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                        branchIDList.Add(rdr.GetInt32("BRANCH_ID"));
                }
            }
            rdr.Close();

            string HQ_IP_ADDRESS = DS.getHQ_IPServer();
            // START CONNECTION TO PABRIK
            clearDataCabang(tableName, 0, HQ_IP_ADDRESS);
            sendDataToCabang(fileName, 0, HQ_IP_ADDRESS);

            // START CONNECTION TO EACH BRANCH
            for (int i =0;i<branchIDList.Count;i++)
            {
                clearDataCabang(tableName, branchIDList[i]);
                sendDataToCabang(fileName, branchIDList[i]);
            }
        }

        private void syncDataProdukSS(string PKField = "", string keyword = "")
        {
            string sqlCommand = "";
            List<int> branchIDList = new List<int>();
            MySqlDataReader rdr;

            // EXPORT DATA TO FILE
            string localDate = "";
            string fileName = "";

            localDate = String.Format(culture, "{0:ddMMyyyy}", DateTime.Now);
            fileName = "EXPORT_SS_PRODUCT_DATA_" + localDate + ".sql";

            fieldToSkip.Clear();
            fieldToSkip.Add("PRODUCT_STOCK_AWAL");
            fieldToSkip.Add("PRODUCT_STOCK_QTY");
            fieldToSkip.Add("PRODUCT_BS_QTY");
            fieldToSkip.Add("PRODUCT_LIMIT_STOCK");

            // EXPORT LOCAL DATA
            exportData(fileName, "MASTER_PRODUCT", PKField, keyword, "", true, true);

            sqlCommand = "SELECT BRANCH_ID FROM MASTER_BRANCH WHERE BRANCH_ACTIVE = 1";

            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                        branchIDList.Add(rdr.GetInt32("BRANCH_ID"));
                }
            }
            rdr.Close();

            string HQ_IP_SERVER = DS.getHQ_IPServer();
            string updateFileName = "";

            fileToExecute.Clear();

            // START CONNECTION TO PABRIK
            updateFileName = createUpdateQueryDataCabang(0, "MASTER_PRODUCT", "ID", fieldToSkip, HQ_IP_SERVER);
            clearDataCabang("MASTER_PRODUCT", 0, HQ_IP_SERVER);
            gUtil.sleep(10);
            fileToExecute.Add(fileName);
            if (updateFileName.Length > 0)
                fileToExecute.Add(updateFileName);

            sendDataToCabangMultipleFiles(0, HQ_IP_SERVER);

            // START CONNECTION TO EACH BRANCH
            for (int i = 0; i < branchIDList.Count; i++)
            {
                updateFileName = createUpdateQueryDataCabang(branchIDList[i], "MASTER_PRODUCT", "ID", fieldToSkip);
                clearDataCabang("MASTER_PRODUCT", branchIDList[i]);
                sendDataToCabang(fileName, branchIDList[i]);
                if (updateFileName.Length > 0)
                    sendDataToCabang(updateFileName, branchIDList[i]);
            }
        }

        public void syncDataProduk(string PKField = "", string keyword = "")
        {
            if (DialogResult.Yes == MessageBox.Show("SYNC DATA TO CABANG?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            { 
                fieldToSkip.Clear();
                fieldToSkip.Add("SYNC_ID");
                fieldToSkip.Add("BRANCH_ID");

                syncDataCabang("MASTER_UNIT", "", "");
                gUtil.saveSystemDebugLog(0, "[SYNC] FINISHED SYNC DATA MASTER_UNIT");
                syncDataCabang("MASTER_CATEGORY", "", "");
                gUtil.saveSystemDebugLog(0, "[SYNC] FINISHED SYNC DATA MASTER_CATEGORY");
                syncDataCabang("PRODUCT_CATEGORY", "", "");
                gUtil.saveSystemDebugLog(0, "[SYNC] FINISHED SYNC DATA PRODUCT_CATEGORY");
                syncDataProdukSS("", "");
                gUtil.saveSystemDebugLog(0, "[SYNC] FINISHED SYNC DATA MASTER_PRODUCT");

                MessageBox.Show("DONE");
            }
        }
    }
}
