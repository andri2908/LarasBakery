using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Globalization;

using Hotkeys;

namespace AlphaSoft
{
    public partial class dailyStockTakeDetailForm : Form
    {
        private const int NEW_DAILY_STOCK_TAKE = 1;
        private const int EDIT_DAILY_STOCK_TAKE = 2;

        private globalUtilities gUtil = new globalUtilities();
        private Data_Access DS = new Data_Access();
        private CultureInfo culture = new CultureInfo("id-ID");
        private List<string> BSQty = new List<string>();
        private bool allowToEdit = false;
        private bool dataSaved = false;
        private int moduleID = NEW_DAILY_STOCK_TAKE;
        private string globalProductAdjustmentID = "";

        private bool isLoading = false;

        private Hotkeys.GlobalHotkey ghk_F9;

        public dailyStockTakeDetailForm()
        {
            InitializeComponent();
        }

        private void captureAll(Keys key)
        {
            switch (key)
            {
                case Keys.F9:
                    saveData();
                    break;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constants.WM_HOTKEY_MSG_ID)
            {
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                int modifier = (int)m.LParam & 0xFFFF;

                if (modifier == Constants.NOMOD)
                    captureAll(key);
            }

            base.WndProc(ref m);
        }

        private void registerGlobalHotkey()
        {
            ghk_F9 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F9, this);
            ghk_F9.Register();
        }

        private void unregisterGlobalHotkey()
        {
            ghk_F9.Unregister();
        }

        private void addDataGridColumn()
        {
            DataGridViewTextBoxColumn productIDColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn rotiColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn awalColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn produksiColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn remarkColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn BSColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn akhirColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn lakuColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn noColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn akhirRiilColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn penyesuaianColumn = new DataGridViewTextBoxColumn();

            productIDColumn.Name = "productID";
            productIDColumn.Visible = false;
            detailDataGrid.Columns.Add(productIDColumn);

            noColumn.Name = "no";
            noColumn.HeaderText = "NO";
            noColumn.ReadOnly = true;
            noColumn.Width = 30;
            detailDataGrid.Columns.Add(noColumn);

            rotiColumn.Name = "roti";
            rotiColumn.HeaderText = "ROTI";
            rotiColumn.ReadOnly = true;
            rotiColumn.Width = 200;
            detailDataGrid.Columns.Add(rotiColumn);

            awalColumn.Name = "awal";
            awalColumn.HeaderText = "AWAL";
            awalColumn.ReadOnly = true;
            detailDataGrid.Columns.Add(awalColumn);

            produksiColumn.Name = "produksi";
            produksiColumn.HeaderText = "PRODUKSI";
            produksiColumn.ReadOnly = true;
            detailDataGrid.Columns.Add(produksiColumn);

            remarkColumn.Name = "remark";
            remarkColumn.HeaderText = "REMARK";
            remarkColumn.ReadOnly = false;
            remarkColumn.Width = 180;
            remarkColumn.DefaultCellStyle.BackColor = Color.AliceBlue;
            detailDataGrid.Columns.Add(remarkColumn);

            BSColumn.Name = "BS";
            BSColumn.HeaderText = "BS";
            BSColumn.ReadOnly = false;
            BSColumn.DefaultCellStyle.BackColor = Color.AliceBlue;
            detailDataGrid.Columns.Add(BSColumn);

            akhirColumn.Name = "akhir";
            akhirColumn.HeaderText = "AKHIR";
            akhirColumn.ReadOnly = true;
//            akhirColumn.Visible = false;
            detailDataGrid.Columns.Add(akhirColumn);

            lakuColumn.Name = "laku";
            lakuColumn.HeaderText = "LAKU";
            lakuColumn.ReadOnly = true;
            detailDataGrid.Columns.Add(lakuColumn);

            penyesuaianColumn.Name = "penyesuaian";
            penyesuaianColumn.HeaderText = "PENYESUAIAN";
            penyesuaianColumn.ReadOnly = true;
            detailDataGrid.Columns.Add(penyesuaianColumn);

            akhirRiilColumn.Name = "akhirRiil";
            akhirRiilColumn.HeaderText = "AKHIR RIIL";
            akhirRiilColumn.ReadOnly = false;
            akhirRiilColumn.DefaultCellStyle.BackColor = Color.AliceBlue;
            detailDataGrid.Columns.Add(akhirRiilColumn);
        }

        private void loadDataStockTake()
        {
            string selectedDate = String.Format(culture, "{0:yyyyMMdd}", Convert.ToDateTime(stockTakeDateTimePicker.Value));
            string sqlCommand = "";
            MySqlDataReader rdr;
            string revID = "";
            DataTable dt = new DataTable();

            // CHECK WHETHER DATA HAS BEEN SAVED BEFORE OR NOT
            sqlCommand = "SELECT COUNT(1) FROM PRODUCT_DAILY_ADJUSTMENT_HEADER WHERE DATE_FORMAT(PRODUCT_ADJUSTMENT_DATE, '%Y%m%d')  = '" + selectedDate + "'";
            if (Convert.ToInt32(DS.getDataSingleValue(sqlCommand)) > 0)
            {
                int userAccessOption = DS.getUserAccessRight(globalConstants.REVISI_STOCK_TAKE_HARIAN, gUtil.getUserGroupID());

                if (userAccessOption == 1)
                {
                    revID = getRevisiNo();
                    moduleID = EDIT_DAILY_STOCK_TAKE;
                    revisionLabel.Visible = true;
                    revisionLabel.Text = "REV : " + revID;
                    revisionRemark.Visible = true;
                    allowToEdit = true;

                    sqlCommand = "SELECT PRODUCT_ADJUSTMENT_ID FROM PRODUCT_DAILY_ADJUSTMENT_HEADER WHERE DATE_FORMAT(PRODUCT_ADJUSTMENT_DATE, '%Y%m%d')  = '" + selectedDate + "'";
                    globalProductAdjustmentID = DS.getDataSingleValue(sqlCommand).ToString();
                }
                else
                {
                    moduleID = EDIT_DAILY_STOCK_TAKE;
                    allowToEdit = false;
                    detailDataGrid.ReadOnly = true;
                }
            }
            else
            { 
                revisionLabel.Visible = false;
                revisionRemark.Visible = false;
            }
            switch (moduleID)
            {
                case NEW_DAILY_STOCK_TAKE:
                    sqlCommand = "SELECT MP.PRODUCT_ID, MP.PRODUCT_NAME AS ROTI, MP.PRODUCT_STOCK_AWAL AS AWAL, (IFNULL(TAB1.TOTAL_RECEIVED, 0)-IFNULL(TAB4.TOTAL_RECEIVED_RETURN, 0)) AS PRODUKSI, '' AS REMARK, '0' AS BS, MP.PRODUCT_STOCK_QTY AS AKHIR, (IFNULL(TAB2.TOTAL_SALES, 0)-IFNULL(TAB3.TOTAL_SALES_RETURN, 0)) AS LAKU, '0' AS PENYESUAIAN, '0' AS RIILQTY " +
                                           "FROM MASTER_PRODUCT MP LEFT OUTER JOIN " +
                                           "(SELECT PRODUCT_ID, SUM(PRODUCT_ACTUAL_QTY) AS TOTAL_RECEIVED FROM PRODUCTS_RECEIVED_HEADER PRH, PRODUCTS_RECEIVED_DETAIL PRD WHERE PRD.PR_INVOICE = PRH.PR_INVOICE AND DATE_FORMAT(PRH.PR_DATE , '%Y%m%d')  = '" + selectedDate + "' GROUP BY PRODUCT_ID) TAB1 ON TAB1.PRODUCT_ID = MP.PRODUCT_ID " +
                                           "LEFT OUTER JOIN(SELECT PRODUCT_ID, SUM(PRODUCT_QTY) AS TOTAL_SALES FROM SALES_HEADER SH, SALES_DETAIL SD WHERE SD.SALES_INVOICE = SH.SALES_INVOICE AND DATE_FORMAT(SH.SALES_DATE, '%Y%m%d')  = '" + selectedDate + "' GROUP BY PRODUCT_ID) TAB2 ON TAB2.PRODUCT_ID = MP.PRODUCT_ID " +
                                           "LEFT OUTER JOIN(SELECT PRODUCT_ID, SUM(PRODUCT_RETURN_QTY) AS TOTAL_SALES_RETURN FROM RETURN_SALES_HEADER RSH, RETURN_SALES_DETAIL RSD WHERE RSD.RS_INVOICE = RSH.RS_INVOICE AND DATE_FORMAT(RSH.RS_DATETIME, '%Y%m%d')  = '" + selectedDate + "' GROUP BY PRODUCT_ID) TAB3 ON TAB3.PRODUCT_ID = MP.PRODUCT_ID " +
                                           "LEFT OUTER JOIN(SELECT PRODUCT_ID, SUM(PRODUCT_QTY) AS TOTAL_RECEIVED_RETURN FROM RETURN_PURCHASE_HEADER RPH, RETURN_PURCHASE_DETAIL RPD WHERE RPD.RP_ID = RPH.RP_ID AND DATE_FORMAT(RPH.RP_DATE, '%Y%m%d')  = '" + selectedDate + "' GROUP BY PRODUCT_ID) TAB4 ON TAB4.PRODUCT_ID = MP.PRODUCT_ID " +
                                           "WHERE MP.PRODUCT_ACTIVE = 1";
                    break;

                case EDIT_DAILY_STOCK_TAKE:
                    sqlCommand = "SELECT PAD.PRODUCT_ID, MP.PRODUCT_NAME AS ROTI, PAD.PRODUCT_LAST_STOCK_QTY AS AWAL, PAD.PRODUCT_RECEIVED_QTY AS PRODUKSI, PAD.REMARKS AS REMARK, PAD.PRODUCT_BS_QTY AS BS, PAD.PRODUCT_LEFTOVER_QTY AS AKHIR, PAD.PRODUCT_SOLD_QTY AS LAKU, PAD.PRODUCT_ADJUSTMENT_QTY AS PENYESUAIAN, PAD.PRODUCT_RIIL_QTY AS RIILQTY " +
                                           "FROM PRODUCT_DAILY_ADJUSTMENT_HEADER PAH, PRODUCT_DAILY_ADJUSTMENT_DETAIL AS PAD, MASTER_PRODUCT MP " +
                                           "WHERE PAD.PRODUCT_ADJUSTMENT_ID = PAH.PRODUCT_ADJUSTMENT_ID AND PAD.PRODUCT_ID = MP.PRODUCT_ID AND DATE_FORMAT(PAH.PRODUCT_ADJUSTMENT_DATE, '%Y%m%d')  = '" + selectedDate + "'";
                    break;
            }

            using (rdr = DS.getData(sqlCommand))
            {
                detailDataGrid.DataSource = null;
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        detailDataGrid.Rows.Add(rdr.GetString("PRODUCT_ID"), detailDataGrid.Rows.Count + 1, rdr.GetString("ROTI"), rdr.GetString("AWAL"), rdr.GetString("PRODUKSI"), rdr.GetString("REMARK"), rdr.GetString("BS"), rdr.GetString("AKHIR"), rdr.GetString("LAKU"), rdr.GetString("PENYESUAIAN"), rdr.GetString("RIILQTY"));
                        BSQty.Add(rdr.GetString("BS"));
                        calculateAkhirValue(detailDataGrid.Rows.Count-1);
                    }
                }
            }
            rdr.Close();
        }

        private void dailyStockTakeDetailForm_Load(object sender, EventArgs e)
        {
            stockTakeDateTimePicker.Format = DateTimePickerFormat.Custom;
            stockTakeDateTimePicker.CustomFormat = globalUtilities.CUSTOM_DATE_FORMAT;

            stockTakeDateTimePicker.Value = DateTime.Now;

            isLoading = true;
            addDataGridColumn();
            loadDataStockTake();
            isLoading = false;

            errorLabel.Text = "";

            registerGlobalHotkey();
        }

        private void calculateAkhirValue(int rowIndex)
        {
            DataGridViewRow selectedRow = detailDataGrid.Rows[rowIndex];
            int awal = 0;
            int produksi = 0;
            int bs = 0;
            int akhir = 0;
            int laku = 0;

            awal = Convert.ToInt32(selectedRow.Cells["AWAL"].Value);
            produksi = Convert.ToInt32(selectedRow.Cells["PRODUKSI"].Value);
            laku = Convert.ToInt32(selectedRow.Cells["LAKU"].Value);
            bs = Convert.ToInt32(BSQty[rowIndex]);

            akhir = awal + produksi - bs - laku;

            selectedRow.Cells["AKHIR"].Value = akhir;
        }

        private void detailDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var cell = detailDataGrid[e.ColumnIndex, e.RowIndex];
            DataGridViewRow selectedRow = detailDataGrid.Rows[e.RowIndex];
            string cellValue = "";
            string previousInput = "";
            int rowSelectedIndex = e.RowIndex;
            string tempString = "";
            string columnName = "";

            if (isLoading)
                return;

            columnName = cell.OwningColumn.Name;
            gUtil.saveSystemDebugLog(0, "DAILY STOCK TAKE : detailDataGrid_CellValueChanged [" + columnName + "]");

            if (null != selectedRow.Cells[columnName].Value)
                cellValue = selectedRow.Cells[columnName].Value.ToString();
            else
                cellValue = "";

            if (columnName == "BS")
            {
                if (cellValue.Length <= 0)
                {
                    // IF TEXTBOX IS EMPTY, DEFAULT THE VALUE TO 0 AND EXIT THE CHECKING
                    gUtil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "DAILY STOCK TAKE : detailDataGrid_CellValueChanged, empty texbox, reset [" + columnName + "] value to 0");
                    isLoading = true;

                    selectedRow.Cells[columnName].Value = "0";

                    isLoading = false;

                    return;
                }

                previousInput = BSQty[rowSelectedIndex];
                gUtil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "DAILY STOCK TAKE : detailDataGrid_CellValueChanged, previousInput [" + previousInput + "]");

                if (previousInput == cellValue)
                    return;

                isLoading = true;
                if (previousInput == "0")
                {
                    tempString = cellValue;
                    if (tempString.IndexOf('0') == 0 && tempString.Length > 1 && tempString.IndexOf("0.") < 0)
                        selectedRow.Cells[columnName].Value = tempString.Remove(tempString.IndexOf('0'), 1);

                    gUtil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "DAILY STOCK TAKE : detailDataGrid_CellValueChanged, cellValue [" + cellValue + "]");
                }

                if (gUtil.matchRegEx(cellValue, globalUtilities.REGEX_NUMBER_ONLY)
                    && (cellValue.Length > 0 && cellValue != ".")
                    )
                {
                    gUtil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "DAILY STOCK TAKE : detailDataGrid_CellValueChanged, dataGridViewTextBoxEditingControl.Text pass REGEX Checking");
                    BSQty[rowSelectedIndex] = cellValue;
                }
                else
                {
                    gUtil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "DAILY STOCK TAKE : detailDataGrid_CellValueChanged, dataGridViewTextBoxEditingControl.Text did not pass REGEX Checking");
                    selectedRow.Cells[columnName].Value = previousInput;
                }

                calculateAkhirValue(rowSelectedIndex);
            }

            isLoading = false;
        }

        private void detailDataGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (detailDataGrid.IsCurrentCellDirty)
            {
                detailDataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void detailDataGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            detailDataGrid.SuspendLayout();
        }

        private void detailDataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            detailDataGrid.ResumeLayout();
        }

        private bool dataValidated()
        {
            bool result = true;
            int pos = 0;

            errorLabel.Text = "";
            for (int i =0;i<detailDataGrid.Rows.Count && result;i++)
            {
                if (detailDataGrid.Rows[i].Cells["REMARK"].Value.ToString().Length <= 0)
                {
                    pos = i + 1;
                    errorLabel.Text = "REMARK PADA BARIS KE " + pos + " KOSONG"; 
                    result = false;
                }
            }

            if (moduleID == EDIT_DAILY_STOCK_TAKE)
            {
                if (revisionRemark.Text.Length <= 0)
                {
                    errorLabel.Text = "REMARK UNTUK REVISI HARUS DIISI";
                    result = false;
                }
            }

            return result;
        }

        private string getProductAdjustmentID()
        {
            string productAdjustmentID = "";

            productAdjustmentID = (Convert.ToInt32(DS.getDataSingleValue("SELECT IFNULL(MAX(CONVERT(PRODUCT_ADJUSTMENT_ID, UNSIGNED INTEGER)), 0) FROM PRODUCT_DAILY_ADJUSTMENT_HEADER")) + 1).ToString();

            return productAdjustmentID;
        }

        private string getRevisiNo()
        {
            string revisionNo = "";

            revisionNo = (Convert.ToInt32(DS.getDataSingleValue("SELECT IFNULL(MAX(CONVERT(PRODUCT_REVISION_NO, UNSIGNED INTEGER)), 0) FROM PRODUCT_DAILY_ADJUSTMENT_HISTORY WHERE PRODUCT_ADJUSTMENT_ID = " + globalProductAdjustmentID)) + 1).ToString();

            return revisionNo;
        }


        private bool saveDataTransaction()
        {
            bool result = false;
            string sqlCommand = "";
            MySqlException internalEX = null;
            int userID = 0;
            string productAdjustmentDate = "";
            string revisiNo = "";

            DS.beginTransaction();

            try
            {
                DS.mySqlConnect();

                userID = gUtil.getUserID();
                productAdjustmentDate = gUtil.getCustomStringFormatDate(DateTime.Now);

                switch(moduleID)
                {
                    case NEW_DAILY_STOCK_TAKE:
                        globalProductAdjustmentID = getProductAdjustmentID();
                        // SAVE DATA HEADER
                        sqlCommand = "INSERT INTO PRODUCT_DAILY_ADJUSTMENT_HEADER (PRODUCT_ADJUSTMENT_ID, PRODUCT_ADJUSTMENT_DATE, USER_ID) VALUES (" + globalProductAdjustmentID + ", STR_TO_DATE('" + productAdjustmentDate + "', '%d-%m-%Y %H:%i'), " + userID + ")";

                        if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                            throw internalEX;
                        break;

                    case EDIT_DAILY_STOCK_TAKE:
                        // UPDATE DATA HEADER
                        sqlCommand = "UPDATE PRODUCT_DAILY_ADJUSTMENT_HEADER SET " + 
                                                "USER_ID = " + userID + " WHERE PRODUCT_ADJUSTMENT_ID = " + globalProductAdjustmentID;
                        if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                            throw internalEX;

                        // CLEAR UP DATA DETAIL
                        sqlCommand = "DELETE FROM PRODUCT_DAILY_ADJUSTMENT_DETAIL WHERE PRODUCT_ADJUSTMENT_ID = " + globalProductAdjustmentID;
                        if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                            throw internalEX;

                        // SAVE DATA REVISI
                        revisiNo = getRevisiNo();
                        sqlCommand = "INSERT INTO PRODUCT_DAILY_ADJUSTMENT_HISTORY (PRODUCT_ADJUSTMENT_ID, PRODUCT_REVISION_NO, REVISION_REMARK) VALUES(" +
                                               globalProductAdjustmentID + ", " + revisiNo + ", '" + revisionRemark.Text + "')";
                        if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                            throw internalEX;

                        break;
                }

                // SAVE DATA DETAIL
                for (int i =0;i<detailDataGrid.Rows.Count;i++)
                {
                    sqlCommand = "INSERT INTO PRODUCT_DAILY_ADJUSTMENT_DETAIL (PRODUCT_ADJUSTMENT_ID, PRODUCT_ID, PRODUCT_LAST_STOCK_QTY, PRODUCT_RECEIVED_QTY, PRODUCT_BS_QTY, PRODUCT_SOLD_QTY, PRODUCT_LEFTOVER_QTY, REMARKS, PRODUCT_RIIL_QTY, PRODUCT_ADJUSTMENT_QTY) VALUES (" +
                                            globalProductAdjustmentID + ", '" + 
                                            detailDataGrid.Rows[i].Cells["productID"].Value.ToString() + "', " +
                                            detailDataGrid.Rows[i].Cells["awal"].Value.ToString() + ", " +
                                            detailDataGrid.Rows[i].Cells["produksi"].Value.ToString() + ", " +
                                            detailDataGrid.Rows[i].Cells["BS"].Value.ToString() + ", " +
                                            detailDataGrid.Rows[i].Cells["laku"].Value.ToString() + ", " +
                                            detailDataGrid.Rows[i].Cells["akhir"].Value.ToString() + ", '" +
                                            detailDataGrid.Rows[i].Cells["remark"].Value.ToString() + "', " +
                                            detailDataGrid.Rows[i].Cells["penyesuaian"].Value.ToString() + ", " +
                                            detailDataGrid.Rows[i].Cells["akhirRiil"].Value.ToString() + ")";

                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;
                }

                DS.commit();
                result = true;
            }
            catch (Exception e)
            {
                try
                {
                    DS.rollBack();
                }
                catch (MySqlException ex)
                {
                    if (DS.getMyTransConnection() != null)
                    {
                        gUtil.showDBOPError(ex, "ROLLBACK");
                    }
                }

                gUtil.showDBOPError(e, "INSERT");
                result = false;
            }

            return result;
        }

        private void saveData()
        {
            if (moduleID == EDIT_DAILY_STOCK_TAKE && allowToEdit == false && dataSaved)
                return;


            if (DialogResult.Yes == MessageBox.Show("SAVE DATA", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            { 
                if (dataValidated())
                {
                    if (saveDataTransaction())
                    {
                        MessageBox.Show("SUCCESS");
                        dataSaved = true;
                        gUtil.setReadOnlyAllControls(this);
                    }
                    else
                        MessageBox.Show("FAILED TO SAVE");
                }
                else
                    MessageBox.Show("FAILED TO VALIDATE");
            }
        }

        private void dailyStockTakeDetailForm_Activated(object sender, EventArgs e)
        {
            registerGlobalHotkey();
        }

        private void dailyStockTakeDetailForm_Deactivate(object sender, EventArgs e)
        {
            unregisterGlobalHotkey();
        }

        private void dailyStockTakeDetailForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            unregisterGlobalHotkey();
        }
    }
}
