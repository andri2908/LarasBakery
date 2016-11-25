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
    public partial class dataSalesInvoice : Form
    {
        private globalUtilities gUtil = new globalUtilities();
        private Data_Access DS = new Data_Access();
        private CultureInfo culture = new CultureInfo("id-ID");
        private int customerID = 0;

        private int originModuleID = 0;

        private Hotkeys.GlobalHotkey ghk_UP;
        private Hotkeys.GlobalHotkey ghk_DOWN;
        private bool navKeyRegistered = false;

        public dataSalesInvoice()
        {
            InitializeComponent();
        }

        public dataSalesInvoice(int moduleID)
        {
            InitializeComponent();
            originModuleID = moduleID;
        }

        private void captureAll(Keys key)
        {
            switch (key)
            {
                case Keys.Up:
                    SendKeys.Send("+{TAB}");
                    break;
                case Keys.Down:
                    SendKeys.Send("{TAB}");
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
            ghk_UP = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.Up, this);
            ghk_UP.Register();

            ghk_DOWN = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.Down, this);
            ghk_DOWN.Register();

            navKeyRegistered = true;
        }

        private void unregisterGlobalHotkey()
        {
            ghk_UP.Unregister();
            ghk_DOWN.Unregister();

            navKeyRegistered = false;
        }

        private void fillInCustomerCombo()
        {
            MySqlDataReader rdr;
            string sqlCommand;

            sqlCommand = "SELECT CUSTOMER_ID, CUSTOMER_FULL_NAME FROM MASTER_CUSTOMER WHERE CUSTOMER_ACTIVE = 1";

            customerCombo.Items.Clear();
            customerHiddenCombo.Items.Clear();

            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        customerCombo.Items.Add(rdr.GetString("CUSTOMER_FULL_NAME"));
                        customerHiddenCombo.Items.Add(rdr.GetString("CUSTOMER_ID"));
                    }
                }
            }
        }

        private void loadInvoiceData()
        {
            MySqlDataReader rdr;
            DataTable dt = new DataTable();
            string sqlCommand = "";
            string dateFrom, dateTo;
            string noInvoiceParam = "";
            string whereClause1 = "";
            string sqlClause1 = "";
            string sqlClause2 = "";

            DS.mySqlConnect();

            if (originModuleID == globalConstants.SALES_QUOTATION)
            {
                sqlClause1 = "SELECT IF(SQ_APPROVED = 1, 'APPROVED', IF(SQ_APPROVED = -1, 'REJECTED', 'PENDING')) AS STATUS, ID, SQ_INVOICE AS 'NO INVOICE', CUSTOMER_FULL_NAME AS 'CUSTOMER', DATE_FORMAT(SQ_DATE, '%d-%M-%Y')  AS 'TGL INVOICE', (SQ_TOTAL - SALES_DISCOUNT_FINAL) AS 'TOTAL', SQ_APPROVED " +
                                       "FROM SALES_QUOTATION_HEADER SQ, MASTER_CUSTOMER MC " +
                                       "WHERE SQ.CUSTOMER_ID = MC.CUSTOMER_ID";

                sqlClause2 = "SELECT IF(SQ_APPROVED = 1, 'APPROVED', IF(SQ_APPROVED = -1, 'REJECTED', 'PENDING')) AS STATUS, ID, SQ_INVOICE AS 'NO INVOICE', '' AS 'CUSTOMER', DATE_FORMAT(SQ_DATE, '%d-%M-%Y') AS 'TGL INVOICE', (SQ_TOTAL - SALES_DISCOUNT_FINAL) AS 'TOTAL', SQ_APPROVED " +
                                       "FROM SALES_QUOTATION_HEADER SQ " +
                                       "WHERE SQ.CUSTOMER_ID = 0";
            }
            else if (originModuleID == globalConstants.SQ_TO_SO)
            {
                sqlClause1 = "SELECT IF(SQ_APPROVED = 1, 'APPROVED', IF(SQ_APPROVED = -1, 'REJECTED', 'PENDING')) AS STATUS, ID, SQ_INVOICE AS 'NO INVOICE', CUSTOMER_FULL_NAME AS 'CUSTOMER', DATE_FORMAT(SQ_DATE, '%d-%M-%Y')  AS 'TGL INVOICE', (SQ_TOTAL - SALES_DISCOUNT_FINAL) AS 'TOTAL', SQ_APPROVED " +
                                       "FROM SALES_QUOTATION_HEADER SQ, MASTER_CUSTOMER MC " +
                                       "WHERE SQ.CUSTOMER_ID = MC.CUSTOMER_ID AND SQ_APPROVED = 1";

                sqlClause2 = "SELECT IF(SQ_APPROVED = 1, 'APPROVED', IF(SQ_APPROVED = -1, 'REJECTED', 'PENDING')) AS STATUS, ID, SQ_INVOICE AS 'NO INVOICE', '' AS 'CUSTOMER', DATE_FORMAT(SQ_DATE, '%d-%M-%Y') AS 'TGL INVOICE', (SQ_TOTAL - SALES_DISCOUNT_FINAL) AS 'TOTAL', SQ_APPROVED " +
                                       "FROM SALES_QUOTATION_HEADER SQ " +
                                       "WHERE SQ.CUSTOMER_ID = 0 AND SQ_APPROVED = 1";
            }
            else
            { 
                sqlClause1 = "SELECT ID, SALES_INVOICE AS 'NO INVOICE', CUSTOMER_FULL_NAME AS 'CUSTOMER', DATE_FORMAT(SALES_DATE, '%d-%M-%Y')  AS 'TGL INVOICE', (SALES_TOTAL - SALES_DISCOUNT_FINAL) AS 'TOTAL' " +
                                           "FROM SALES_HEADER SH, MASTER_CUSTOMER MC " +
                                           "WHERE SH.CUSTOMER_ID = MC.CUSTOMER_ID";

               sqlClause2 = "SELECT ID, SALES_INVOICE AS 'NO INVOICE', '' AS 'CUSTOMER', DATE_FORMAT(SALES_DATE, '%d-%M-%Y') AS 'TGL INVOICE', (SALES_TOTAL - SALES_DISCOUNT_FINAL) AS 'TOTAL' " +
                                           "FROM SALES_HEADER SH " +
                                           "WHERE SH.CUSTOMER_ID = 0";
            }

            if (!showAllCheckBox.Checked)
            {
                if (noInvoiceTextBox.Text.Length > 0)
                {
                    if (originModuleID == globalConstants.SALES_QUOTATION || originModuleID == globalConstants.SQ_TO_SO)
                        whereClause1 = whereClause1 + " AND SQ.SQ_INVOICE LIKE '%" + noInvoiceParam + "%'";
                    else
                        whereClause1 = whereClause1 + " AND SH.SALES_INVOICE LIKE '%" + noInvoiceParam + "%'";
                }

                dateFrom = String.Format(culture, "{0:yyyyMMdd}", Convert.ToDateTime(PODtPicker_1.Value));
                dateTo = String.Format(culture, "{0:yyyyMMdd}", Convert.ToDateTime(PODtPicker_2.Value));

                if (originModuleID == globalConstants.SALES_QUOTATION || originModuleID == globalConstants.SQ_TO_SO)
                    whereClause1 = whereClause1 + " AND DATE_FORMAT(SQ.SQ_DATE, '%Y%m%d')  >= '" + dateFrom + "' AND DATE_FORMAT(SQ.SQ_DATE, '%Y%m%d')  <= '" + dateTo + "'";
                else
                    whereClause1 = whereClause1 + " AND DATE_FORMAT(SH.SALES_DATE, '%Y%m%d')  >= '" + dateFrom + "' AND DATE_FORMAT(SH.SALES_DATE, '%Y%m%d')  <= '" + dateTo + "'";

                if (customerID > 0)
                {
                    if (originModuleID == globalConstants.SALES_QUOTATION || originModuleID == globalConstants.SQ_TO_SO)
                        sqlCommand = sqlClause1 + whereClause1 + " AND AND SQ.CUSTOMER_ID = " + customerID;
                    else
                        sqlCommand = sqlClause1 + whereClause1 + " AND AND SH.CUSTOMER_ID = " + customerID;
                }
                else
                {
                    sqlCommand = sqlClause1 + whereClause1 + " UNION " + sqlClause2 + whereClause1;
                }
            }
            else
            {
                sqlCommand = sqlClause1 + " UNION " + sqlClause2;
            }

            using (rdr = DS.getData(sqlCommand))
            {
                dataPenerimaanBarang.DataSource = null;
                if (rdr.HasRows)
                {
                    dt.Load(rdr);
                    dataPenerimaanBarang.DataSource = dt;
                    dataPenerimaanBarang.Columns["ID"].Visible = false;

                    if (originModuleID == globalConstants.SALES_QUOTATION)
                        dataPenerimaanBarang.Columns["SQ_APPROVED"].Visible = false;

                    dataPenerimaanBarang.Columns["NO INVOICE"].Width = 200;
                    dataPenerimaanBarang.Columns["TGL INVOICE"].Width = 200;
                    dataPenerimaanBarang.Columns["CUSTOMER"].Width = 200;
                    dataPenerimaanBarang.Columns["TOTAL"].Width = 200;
                }

                rdr.Close();
            }
        }

        private void dataSalesInvoice_Load(object sender, EventArgs e)
        {
            int userAccessOption = 0;
            Button[] arrButton = new Button[2];

            PODtPicker_1.CustomFormat = globalUtilities.CUSTOM_DATE_FORMAT;
            PODtPicker_2.CustomFormat = globalUtilities.CUSTOM_DATE_FORMAT;
            fillInCustomerCombo();

            arrButton[0] = displayButton;
            arrButton[1] = newInvoiceButton;

            gUtil.reArrangeButtonPosition(arrButton, arrButton[0].Top, this.Width);

            gUtil.reArrangeTabOrder(this, 1);
        }

        private void displayButton_Click(object sender, EventArgs e)
        {
            loadInvoiceData();
        }

        private void customerCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            customerID = Convert.ToInt32(customerHiddenCombo.Items[customerCombo.SelectedIndex].ToString());
        }

        private void displaySpecificForm(string noInvoice, int status = 0)
        {
            //int salesActiveStatus = 0;
            //string dialogMessage = "";
            switch (originModuleID)
            {
                case globalConstants.SALES_QUOTATION:
                    if (status == 0)
                    { 
                        cashierForm displayedForm = new cashierForm(globalConstants.EDIT_SALES_QUOTATION, noInvoice);
                        displayedForm.ShowDialog(this);
                        displayedForm.Dispose();
                    }
                    break;

                case globalConstants.SQ_TO_SO:
                    cashierForm displayedFormCashier = new cashierForm(globalConstants.SQ_TO_SO, noInvoice);
                    displayedFormCashier.ShowDialog(this);
                    displayedFormCashier.Dispose();
                    break;

                default:
                    cashierForm cashierFormDisplay = new cashierForm(noInvoice);
                    cashierFormDisplay.ShowDialog(this);
                    cashierFormDisplay.Dispose();
                    break;

                //case globalConstants.DELIVERY_ORDER:
                //    salesActiveStatus = Convert.ToInt32(DS.getDataSingleValue("SELECT SALES_ACTIVE FROM SALES_HEADER WHERE SALES_INVOICE = '" + noInvoice + "' AND REV_NO = '" + revNo + "'"));
                //    if (salesActiveStatus == 1)
                //    {
                //        dialogMessage = "TERBITKAN DELIVERY ORDER ?";
                //    }
                //    else
                //    {
                //        dialogMessage = "TERBITKAN COPY DELIVERY ORDER ?";
                //    }

                //    if (DialogResult.Yes == MessageBox.Show(dialogMessage, "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                //    {
                //        // UPDATE SALES HEADER SET TO NON ACTIVE AND REDUCE STOCK
                //        if (processSalesOrderToDO(noInvoice, revNo, salesActiveStatus))
                //            printOutDeliveryOrder(noInvoice, revNo, salesActiveStatus);
                //    }
                //    break;
            }
        }

        private void dataPenerimaanBarang_DoubleClick(object sender, EventArgs e)
        {
            string noInvoice = "";
            int status = 0;

            if (dataPenerimaanBarang.Rows.Count <= 0)
                return;

            int rowSelectedIndex = (dataPenerimaanBarang.SelectedCells[0].RowIndex);
            DataGridViewRow selectedRow = dataPenerimaanBarang.Rows[rowSelectedIndex];
            noInvoice = selectedRow.Cells["NO INVOICE"].Value.ToString();
            status = Convert.ToInt32(selectedRow.Cells["SQ_APPROVED"].Value);

            displaySpecificForm(noInvoice, status);
        }

        private void dataPenerimaanBarang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string noInvoice = "";
                int status = 0;

                if (dataPenerimaanBarang.Rows.Count <= 0)
                    return;

                int rowSelectedIndex = (dataPenerimaanBarang.SelectedCells[0].RowIndex);
                DataGridViewRow selectedRow = dataPenerimaanBarang.Rows[rowSelectedIndex];
                noInvoice = selectedRow.Cells["NO INVOICE"].Value.ToString();

                status = Convert.ToInt32(selectedRow.Cells["SQ_APPROVED"].Value);
                displaySpecificForm(noInvoice, status);
            }
        }

        private void genericControl_Leave(object sender, EventArgs e)
        {
            registerGlobalHotkey();
        }

        private void genericControl_Enter(object sender, EventArgs e)
        {
            if (navKeyRegistered)
                unregisterGlobalHotkey();
        }

        private void dataSalesInvoice_Activated(object sender, EventArgs e)
        {
            registerGlobalHotkey();

            if (dataPenerimaanBarang.Rows.Count > 0)
                displayButton.PerformClick();

        }

        private void dataSalesInvoice_Deactivate(object sender, EventArgs e)
        {
            if (navKeyRegistered)
                unregisterGlobalHotkey();
        }

        private void dataPenerimaanBarang_Enter(object sender, EventArgs e)
        {
            if (navKeyRegistered)
                unregisterGlobalHotkey();
        }

        private void dataPenerimaanBarang_Leave(object sender, EventArgs e)
        {
            if (!navKeyRegistered)
                registerGlobalHotkey();
        }

        private void newInvoiceButton_Click(object sender, EventArgs e)
        {
            if (originModuleID == globalConstants.SALES_QUOTATION)
            {
                cashierForm displayedForm = new cashierForm(originModuleID, true);
                displayedForm.ShowDialog(this);
            }
        }
    }
}
