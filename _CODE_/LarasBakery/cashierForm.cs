using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hotkeys;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.Drawing.Printing;
using System.Reflection;

namespace AlphaSoft
{
    public partial class cashierForm : Form
    {
        //private const string posTitle = "\"" + "ROYALE PETZ" + "\"" + " A Royale Shop For Your Pets";
        private const string posTitle = "LARAS BAKERY";
        private string selectedsalesinvoice = "";
        private string selectedsalesinvoiceTax = "";
        public static int objCounter = 1;
        private DateTime localDate = DateTime.Now;
        private double globalTotalValue = 0;
        private double discValue = 0;
        private int selectedPelangganID = 0;
        private int selectedPaymentMethod = 0;
        private bool isLoading = false;
        private double bayarAmount = 0;
        private string bayarAmountText = "0";
        // private double discAmount = 0;
        private string discAmountText = "0";
        private bool forceUpOneLevel = false;
        private double SQDPAmount = 0;

        private bool SOisPesanan = false;
        private string referenceSO = "";

        private double sisaBayar = 0;
        private int originModuleID = 0;
        private int subOriginModuleID = 0;
        private bool navKeyRegistered = false;
        private bool delKeyRegistered = false;

        private double previousSQ_DP = 0; // LARAS : TO STORE PREVIOUS DP AMOUNT
        private string selectedSQInvoice = ""; // LARAS : TO STORE SQ INVOICE

        private Data_Access DS = new Data_Access();

        private globalUtilities gutil = new globalUtilities();
        private CultureInfo culture = new CultureInfo("id-ID");
        private List<string> salesQty = new List<string>();
        private List<string> productPriceList = new List<string>();
        private List<string> jumlahList = new List<string>();
        private List<string> disc1 = new List<string>();
        private List<string> disc2 = new List<string>();
        private List<string> discRP = new List<string>();

        private Hotkeys.GlobalHotkey ghk_F1;
        private Hotkeys.GlobalHotkey ghk_F2;
        private Hotkeys.GlobalHotkey ghk_F3;
        private Hotkeys.GlobalHotkey ghk_F4;
        private Hotkeys.GlobalHotkey ghk_F5;
        private Hotkeys.GlobalHotkey ghk_F7;
        private Hotkeys.GlobalHotkey ghk_F8;
        private Hotkeys.GlobalHotkey ghk_F9;
        private Hotkeys.GlobalHotkey ghk_F10;
        private Hotkeys.GlobalHotkey ghk_F11;
        private Hotkeys.GlobalHotkey ghk_F12;
        private Hotkeys.GlobalHotkey ghk_DEL;

        private Hotkeys.GlobalHotkey ghk_CTRL_DEL;
        private Hotkeys.GlobalHotkey ghk_CTRL_Enter;
        private Hotkeys.GlobalHotkey ghk_CTRL_C;
        private Hotkeys.GlobalHotkey ghk_CTRL_U;

        private Hotkeys.GlobalHotkey ghk_ALT_F4;
        private Hotkeys.GlobalHotkey ghk_Add;
        private Hotkeys.GlobalHotkey ghk_Substract;

        private Hotkeys.GlobalHotkey ghk_UP;
        private Hotkeys.GlobalHotkey ghk_DOWN;

        //private adminForm parentForm;
        barcodeForm displayBarcodeForm = null;
        dataProdukForm browseProdukForm = null;
        dataPelangganForm browsePelangganForm = null;
        dataSalesInvoice browseSOForm = null;

        public cashierForm()
        {
            InitializeComponent();
        }

        public cashierForm(int counter)
        {
            InitializeComponent();

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : MULTIPLE CASHIER INSTANCE [" + counter + "]");

            label1.Text = "Struk # : " + counter;
            titleLabel.Text = posTitle;

            objCounter = counter + 1;
        }

        public cashierForm(int moduleID, bool flag)
        {
            InitializeComponent();
            originModuleID = moduleID;
            titleLabel.Text = posTitle;
        }

        public cashierForm(string noInvoice)
        {
            InitializeComponent();
            originModuleID = globalConstants.COPY_NOTA;
            selectedsalesinvoice = noInvoice;
            titleLabel.Text = posTitle;
        }

        public cashierForm(int moduleID, string noInvoice)
        {
            InitializeComponent();
            originModuleID = moduleID;
            selectedsalesinvoice = noInvoice;
            titleLabel.Text = posTitle;
        }

        private void captureAll(Keys key)
        {
            int posindex = 0;
            int rowcount = 0;
            switch (key)
            {
                case Keys.F1:
                    cashierHelpForm displayHelp = new cashierHelpForm();
                    displayHelp.ShowDialog(this);
                    break;

                case Keys.F2:
                    if (originModuleID != globalConstants.COPY_NOTA && originModuleID != globalConstants.COPY_NOTA_SQ)
                    {
                        totalAfterDiscTextBox.Focus();
                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : DISPLAY BARCODE FORM");

                        if (null == displayBarcodeForm || displayBarcodeForm.IsDisposed)
                        {
                            displayBarcodeForm = new barcodeForm(this, globalConstants.CASHIER_MODULE);

                            displayBarcodeForm.Top = this.Top + 5;// - displayBarcodeForm.Height;
                            displayBarcodeForm.Left = this.Left + 5;// (Screen.PrimaryScreen.Bounds.Width / 2) - (displayBarcodeForm.Width / 2);
                        }

                        displayBarcodeForm.Show();
                        displayBarcodeForm.WindowState = FormWindowState.Normal;

                        //cashierDataGridView.Focus();
                    }
                    break;

                case Keys.F3:
                    if (originModuleID == 0)
                    {
                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : CREATE NEW INSTANCE [" + objCounter + "]");

                        cashierForm displayForm = new cashierForm(objCounter);
                        displayForm.Show();
                    }
                    break;

                case Keys.F4:
                    if (originModuleID != globalConstants.COPY_NOTA && originModuleID != globalConstants.COPY_NOTA_SQ)
                    {
                        //MessageBox.Show("F4");
                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : DISPLAY PELANGGAN FORM");
                        if (null == browsePelangganForm || browsePelangganForm.IsDisposed)
                            browsePelangganForm = new dataPelangganForm(globalConstants.CASHIER_MODULE, this);

                        browsePelangganForm.Show();
                        browsePelangganForm.WindowState = FormWindowState.Normal;
                    }
                    break;

                case Keys.F5: // NOT USED
                    if (originModuleID != globalConstants.COPY_NOTA && originModuleID != globalConstants.COPY_NOTA_SQ)
                        if (DialogResult.Yes == MessageBox.Show("HAPUS DATA DATA DI LAYAR ?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                            clearUpScreen();
                    break;

                case Keys.F7:
                    if (originModuleID == globalConstants.COPY_NOTA || originModuleID == globalConstants.COPY_NOTA_SQ)
                        if (selectedsalesinvoice != "")
                            if (DialogResult.Yes == MessageBox.Show("REPRINT INVOICE ?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                                reprintInvoice();
                    break;

                case Keys.F8: // USED
                    if (originModuleID != globalConstants.COPY_NOTA && originModuleID != globalConstants.COPY_NOTA_SQ)
                    {
                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : HOTKEY TO ADD NEW ROW PRESSED");
                        //focus to row below
                        rowcount = cashierDataGridView.RowCount;
                        cashierDataGridView.CurrentCell = cashierDataGridView.Rows[rowcount - 1].Cells["productID"];
                        //if (rowcount > 1)
                        //{
                        //    posindex = cashierDataGridView.CurrentCell.RowIndex;
                        //    if (posindex < rowcount - 1)
                        //    {
                        //        cashierDataGridView.CurrentCell = cashierDataGridView.Rows[posindex + 1].Cells["productID"];
                        //    }
                        //}
                        //addNewRow();
                    }
                    break;

                case Keys.F9:
                    if (originModuleID != globalConstants.COPY_NOTA && originModuleID != globalConstants.COPY_NOTA_SQ)
                    {
                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : HOTKEY TO SAVE AND PRINT OUT INVOICE PRESSED");

                        saveAndPrintOutInvoice();
                    }
                    break;

                case Keys.F11:
                    if (originModuleID != globalConstants.COPY_NOTA && originModuleID != globalConstants.COPY_NOTA_SQ)
                    {
                        totalAfterDiscTextBox.Focus();

                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : HOTKEY TO OPEN PRODUK SEARCH FORM PRESSED");

                        if (null == browseProdukForm || browseProdukForm.IsDisposed)
                            browseProdukForm = new dataProdukForm(globalConstants.CASHIER_MODULE, this);

                        browseProdukForm.Show();
                        browseProdukForm.WindowState = FormWindowState.Normal;

                        //cashierDataGridView.Focus();
                    }
                    break;

                case Keys.Add:
                    bayarTextBox.Focus();
                    break;

                case Keys.Subtract:
                    discJualMaskedTextBox.Focus();
                    break;

                case Keys.Delete:
                    //if (cashierDataGridView.Focused)
                    //if (!bayarTextBox.Focused && !discJualMaskedTextBox.Focused && 
                    //    !tempoMaskedTextBox.Focused && !paymentComboBox.Focused &&
                    //    !customerComboBox.Focused 
                    //    )
                    {
                        if (originModuleID != globalConstants.COPY_NOTA && originModuleID != globalConstants.COPY_NOTA_SQ)
                            if (cashierDataGridView.Rows.Count > 1)
                                if (DialogResult.Yes == MessageBox.Show("DELETE CURRENT ROW?", "WARNING", MessageBoxButtons.YesNo))
                                {
                                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : cashierDataGridView_KeyDown ATTEMPT TO DELETE ROW");
                                    deleteCurrentRow();
                                    updateRowNumber();
                                    calculateTotal();
                                }
                    }
                    break;

                case Keys.F10:
                    //browseReferenceSO();
                    //MessageBox.Show("F10");
                    break;
                case Keys.F12:
                    MessageBox.Show("F12");
                    break;

                case Keys.Up:
                    SendKeys.Send("+{TAB}");
                    break;

                case Keys.Down:
                    SendKeys.Send("{TAB}");
                    break;

            }
        }

        private void captureAltModifier(Keys key)
        {
            switch (key)
            {
                case Keys.F4: // ALT + F4
                    //MessageBox.Show("ALT+F4");
                    this.Close();
                    this.Dispose();
                    break;
            }
        }

        private void captureCtrlModifier(Keys key)
        {
            switch (key)
            {
                case Keys.Delete: // CTRL + DELETE
                                  //MessageBox.Show("CTRL+DELETE");
                    if (originModuleID != globalConstants.COPY_NOTA && originModuleID != globalConstants.COPY_NOTA_SQ)
                        if (DialogResult.Yes == MessageBox.Show("DELETE CURRENT ROW?", "WARNING", MessageBoxButtons.YesNo))
                        {
                            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : cashierDataGridView_KeyDown ATTEMPT TO DELETE ROW");
                            deleteCurrentRow();
                            updateRowNumber();
                            calculateTotal();
                        }
                    break;

                case Keys.Enter:
                    if (originModuleID != globalConstants.COPY_NOTA && originModuleID != globalConstants.COPY_NOTA_SQ)
                    {
                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : HOTKEY TO SAVE AND PRINT OUT INVOICE PRESSED");

                        saveAndPrintOutInvoice();
                    }
                    break;

                case Keys.C: // CTRL + C
                    MessageBox.Show("CTRL+C");
                    break;
                case Keys.U: // CTRL + U
                    MessageBox.Show("CTRL+U");
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
                else if (modifier == Constants.ALT)
                    captureAltModifier(key);
                else if (modifier == Constants.CTRL)
                    captureCtrlModifier(key);
            }

            base.WndProc(ref m);
        }

        private void registerGlobalHotkey()
        {
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : REGISTER HOTKEY");

            ghk_F1 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F1, this);
            ghk_F1.Register();

            ghk_F2 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F2, this);
            ghk_F2.Register();

            ghk_F3 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F3, this);
            ghk_F3.Register();

            ghk_F4 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F4, this);
            ghk_F4.Register();

            //ghk_F5 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F5, this);
            //ghk_F5.Register();

            ghk_F7 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F7, this);
            ghk_F7.Register();

            ghk_F8 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F8, this);
            ghk_F8.Register();

            ghk_F9 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F9, this);
            ghk_F9.Register();

            //ghk_F10 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F10, this);
            //ghk_F10.Register();

            ghk_F11 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F11, this);
            ghk_F11.Register();

            if (originModuleID != globalConstants.SALES_QUOTATION)
            {
                ghk_Add = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.Add, this);
                ghk_Add.Register();
            }

            ghk_Substract = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.Subtract, this);
            ghk_Substract.Register();

            //ghk_DEL = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.Delete, this);
            //ghk_DEL.Register();

            //ghk_CTRL_DEL = new Hotkeys.GlobalHotkey(Constants.CTRL, Keys.Delete, this);
            //ghk_CTRL_DEL.Register();

            ghk_CTRL_Enter = new Hotkeys.GlobalHotkey(Constants.CTRL, Keys.Enter, this);
            ghk_CTRL_Enter.Register();

            //// ## F12 doesn't work yet ##
            ////ghk_F12 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F12, this);
            ////ghk_F12.Register();



            //ghk_CTRL_C = new Hotkeys.GlobalHotkey(Constants.CTRL, Keys.C, this);
            //ghk_CTRL_C.Register();

            //ghk_CTRL_U = new Hotkeys.GlobalHotkey(Constants.CTRL, Keys.U, this);
            //ghk_CTRL_U.Register();

            //ghk_ALT_F4 = new Hotkeys.GlobalHotkey(Constants.ALT, Keys.F4, this);
            //ghk_ALT_F4.Register();
            registerNavigationKey();
        }

        private void registerNavigationKey()
        {
            ghk_UP = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.Up, this);
            ghk_UP.Register();

            ghk_DOWN = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.Down, this);
            ghk_DOWN.Register();

            navKeyRegistered = true;
        }

        private void unregisterNavigationKey()
        {
            ghk_UP.Unregister();
            ghk_DOWN.Unregister();

            navKeyRegistered = false;
        }

        private void unregisterGlobalHotkey()
        {
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : UNREGISTER HOTKEY");

            ghk_F1.Unregister();
            ghk_F2.Unregister();
            ghk_F3.Unregister();
            ghk_F4.Unregister();
            //ghk_F5.Unregister();
            ghk_F7.Unregister();
            ghk_F8.Unregister();
            ghk_F9.Unregister();
            //ghk_F10.Unregister();
            ghk_F11.Unregister();


            if (originModuleID != globalConstants.SALES_QUOTATION)
            {
                ghk_Add.Unregister();
            }

            ghk_Substract.Unregister();
            //ghk_DEL.Unregister();

            //ghk_CTRL_DEL.Unregister();
            ghk_CTRL_Enter.Unregister();


            ////ghk_F12.Unregister();


            //ghk_CTRL_C.Unregister();
            //ghk_CTRL_U.Unregister();

            //ghk_ALT_F4.Unregister();
            unregisterNavigationKey();
        }

        private void registerDelKey()
        {
            ghk_DEL = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.Delete, this);
            ghk_DEL.Register();

            delKeyRegistered = true;
        }

        private void unregisterDelKey()
        {
            ghk_DEL.Unregister();

            delKeyRegistered = false;
        }

        private void browseReferenceSO()
        {
            if (originModuleID == globalConstants.SO_FULFILLMENT)
            {
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : DISPLAY SALES INVOICE");
                if (null == browseSOForm || browseSOForm.IsDisposed)
                    browseSOForm = new dataSalesInvoice(globalConstants.CASHIER_MODULE, this);

                browseSOForm.Show();
                browseSOForm.WindowState = FormWindowState.Normal;
            }
        }

        public void setReferenceSO(string refSO)
        {
            referenceSO = refSO;
            refSOTextBox.Text = refSO;
        }

        private void reprintInvoice()
        {
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "RE-PRINT INVOICE");
            PrintReceipt();
        }

        public void clearUpScreen()
        {
            isLoading = true;

            //while (cashierDataGridView.Rows.Count > 0 )
            //    cashierDataGridView.Rows.Remove(cashierDataGridView.Rows[0]);
            cashierDataGridView.Rows.Clear();
            isLoading = false;

            salesQty.Clear();
            disc1.Clear();
            disc2.Clear();
            discRP.Clear();

            salesQty.Add("0");
            disc1.Add("0");
            disc2.Add("0");
            discRP.Add("0");

            selectedPelangganID = 0;
            globalTotalValue = 0;
            discValue = 0;
            sisaBayar = 0;
            bayarAmount = 0;
            bayarAmountText = "0";
            discAmountText = "0";

            totalLabel.Text = globalTotalValue.ToString("C0", culture);
            gutil.ResetAllControls(this);

            totalPenjualanTextBox.Text = globalTotalValue.ToString("C0", culture);
            totalAfterDiscTextBox.Text = globalTotalValue.ToString("C0", culture);
            uangKembaliTextBox.Text = "0";

            customerComboBox.SelectedIndex = 0;
            customerComboBox.Text = customerComboBox.Items[0].ToString();

            paymentComboBox.SelectedIndex = 0;
            paymentComboBox.Text = paymentComboBox.Items[0].ToString();

            cashRadioButton.Checked = true;
            creditRadioButton.Checked = false;

            // RESET TO NORMAL TRANSACTION MODE
            originModuleID = 0; // NORMAL TRANSACTION
            approvalButton.Visible = false;
            rejectButton.Visible = false;

            deliveredCheckbox.Visible = false;
            addressTextBox.Visible = false;

            //labelJamKirim.Visible = false;
            jobStartDateTimePicker.Visible = false;
            DPTextBox.Visible = false;
        }

        public void setCustomerID(int ID)
        {
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : SELECTED CUSTOMER ID [" + ID + "]");

            selectedPelangganID = ID;

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ATTEMPT TO SET CUSTOMER PROFILE");
            setCustomerProfile();
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : FINISHED SET CUSTOMER PROFILE");

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ATTEMPT TO REFRESH PRODUCT PRICE");
            refreshProductPrice();
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : PRODUCT PRICE REFRESHED");
        }

        private void setCustomerProfile()
        {
            MySqlDataReader rdr;
            string sqlCommand = "";

            //DS.mySqlConnect();
            sqlCommand = "SELECT * FROM MASTER_CUSTOMER WHERE CUSTOMER_ID = " + selectedPelangganID;
            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    rdr.Read();

                    pelangganTextBox.Text = rdr.GetString("CUSTOMER_FULL_NAME");
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : SET CUSTOMER PROFILE [" + pelangganTextBox.Text + "]");

                    isLoading = true;
                    customerComboBox.SelectedIndex = rdr.GetInt32("CUSTOMER_GROUP") - 1;
                    customerComboBox.Text = customerComboBox.Items[customerComboBox.SelectedIndex].ToString();
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : CUSTOMER GROUP [" + customerComboBox.Text + "]");
                    isLoading = false;
                }
                else
                {
                    // RESET CUSTOMER 
                    pelangganTextBox.Text = "";
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : RESET CUSTOMER PROFILE");

                    isLoading = true;
                    customerComboBox.SelectedIndex = 0;
                    customerComboBox.Text = customerComboBox.Items[customerComboBox.SelectedIndex].ToString();
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : RESET CUSTOMER GROUP [" + customerComboBox.Text + "]");
                    isLoading = false;
                }
            }
            rdr.Close();
        }

        private void refreshProductPrice()
        {
            double productPrice = 0;
            string productID = "";
            MySqlDataReader rdr;

            for (int i = 0; i < cashierDataGridView.Rows.Count; i++)
            {
                if (null != cashierDataGridView.Rows[i].Cells["productID"].Value)
                {
                    productID = cashierDataGridView.Rows[i].Cells["productID"].Value.ToString();
                    productPrice = getProductPriceValue(productID, customerComboBox.SelectedIndex);

                    cashierDataGridView.Rows[i].Cells["productPrice"].Value = productPrice;

                    if (originModuleID != globalConstants.SALES_QUOTATION && originModuleID != globalConstants.EDIT_SALES_QUOTATION)
                    {
                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : CHECK IF THERE'S ANY ENTRY IN THE PRODUCT DISC");

                        if (Convert.ToInt32(DS.getDataSingleValue("SELECT COUNT(1) FROM CUSTOMER_PRODUCT_DISC WHERE CUSTOMER_ID = " + selectedPelangganID + " AND PRODUCT_ID = '" + productID + "'")) > 0)
                        {
                            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ENTRY IN THE PRODUCT DISC FOUND, RETRIEVE THE DISC VALUE");

                            // DATA EXIST, LOAD DISC VALUE
                            using (rdr = DS.getData("SELECT * FROM CUSTOMER_PRODUCT_DISC WHERE CUSTOMER_ID = " + selectedPelangganID + " AND PRODUCT_ID = '" + productID + "'"))
                            {
                                if (rdr.HasRows)
                                {
                                    rdr.Read();

                                    cashierDataGridView.Rows[i].Cells["disc1"].Value = rdr.GetString("DISC_1");
                                    disc1[i] = rdr.GetString("DISC_1");

                                    cashierDataGridView.Rows[i].Cells["disc2"].Value = rdr.GetString("DISC_2");
                                    disc2[i] = rdr.GetString("DISC_2");

                                    cashierDataGridView.Rows[i].Cells["discRP"].Value = rdr.GetString("DISC_RP");
                                    discRP[i] = rdr.GetString("DISC_RP");

                                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : DISC FOUND [" + rdr.GetString("DISC_1") + ", " + rdr.GetString("DISC_2") + ", " + rdr.GetString("DISC_RP") + "]");
                                }

                                rdr.Close();
                            }
                        }
                        else
                        {
                            cashierDataGridView.Rows[i].Cells["disc1"].Value = 0;
                            disc1[i] = "0";

                            cashierDataGridView.Rows[i].Cells["disc2"].Value = 0;
                            disc2[i] = "0";

                            cashierDataGridView.Rows[i].Cells["discRP"].Value = 0;
                            discRP[i] = "0";
                        }
                    }
                    cashierDataGridView.Rows[i].Cells["jumlah"].Value = calculateSubTotal(i, productPrice);
                }
            }

            calculateTotal();
        }

        private void updateLabel()
        {
            localDate = DateTime.Now;
            dateTimeStampLabel.Text = String.Format(culture, "{0:dddd, dd-MM-yyyy - HH:mm}", localDate);
        }

        private void updateRowNumber()
        {
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : UPDATE ROW NUMBER, TOTAL ROW [" + cashierDataGridView.Rows.Count + "]");
            for (int i = 0; i < cashierDataGridView.Rows.Count; i++)
                cashierDataGridView.Rows[i].Cells["F8"].Value = i + 1;
        }

        private void addNewRow(bool isActive = true)
        {

            int prevValue = 0;
            bool allowToAdd = true;
            int newRowIndex = 0;

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ADD NEW ROW,  isActive [" + isActive.ToString() + "]");
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ADD NEW ROW,  ROW COUNT [" + cashierDataGridView.Rows.Count + "]");

            //for (int i = 0; i < cashierDataGridView.Rows.Count && allowToAdd; i++)
            //{
            //    if (null != cashierDataGridView.Rows[i].Cells["productID"].Value)
            //    {
            //        if (!productIDValid(cashierDataGridView.Rows[i].Cells["productID"].Value.ToString()))
            //        {
            //            allowToAdd = false;
            //            newRowIndex = i;
            //        }
            //    }
            //    else
            //    {
            //        allowToAdd = false;
            //        newRowIndex = i;
            //    }
            //}

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ADD NEW ROW,  ALLOW TO ADD [" + allowToAdd.ToString() + "]");

            if (allowToAdd)
            {
                if (cashierDataGridView.Rows.Count > 0)
                    prevValue = Convert.ToInt32(cashierDataGridView.Rows[cashierDataGridView.Rows.Count - 1].Cells["F8"].Value);


                cashierDataGridView.Rows.Add();

                salesQty.Add("0");
                disc1.Add("0");
                disc2.Add("0");
                discRP.Add("0");
                productPriceList.Add("0");
                jumlahList.Add("0");

                cashierDataGridView.Rows[cashierDataGridView.Rows.Count - 1].Cells["F8"].Value = prevValue + 1;
                cashierDataGridView.Rows[cashierDataGridView.Rows.Count - 1].Cells["hpp"].Value = "0";
                cashierDataGridView.Rows[cashierDataGridView.Rows.Count - 1].Cells["productPrice"].Value = "0";
                cashierDataGridView.Rows[cashierDataGridView.Rows.Count - 1].Cells["qty"].Value = "0";
                //cashierDataGridView.Rows[cashierDataGridView.Rows.Count - 1].Cells["disc1"].Value = "0";
                //cashierDataGridView.Rows[cashierDataGridView.Rows.Count - 1].Cells["disc2"].Value = "0";
                //cashierDataGridView.Rows[cashierDataGridView.Rows.Count - 1].Cells["discRP"].Value = "0";
                cashierDataGridView.Rows[cashierDataGridView.Rows.Count - 1].Cells["jumlah"].Value = "0";
                newRowIndex = cashierDataGridView.Rows.Count - 1;
            }
            else
            {
                DataGridViewRow selectedRow = cashierDataGridView.Rows[newRowIndex];
                clearUpSomeRowContents(selectedRow, newRowIndex);
            }

            if (isActive)
            {
                cashierDataGridView.Focus();
                cashierDataGridView.CurrentCell = cashierDataGridView.Rows[newRowIndex].Cells["productID"];
            }
        }

        public void addNewRowFromBarcode(string productID, string productName, int rowIndex = -1)
        {
            int i = 0;
            bool found = false;
            bool foundEmptyRow = false;
            int emptyRowIndex = 0;
            int rowSelectedIndex = 0;
            double currQty;

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ADD NEW ROW FROM BARCODE [" + productName + "]");

            cashierDataGridView.AllowUserToAddRows = false;

            if (rowIndex >= 0)
            {
                rowSelectedIndex = rowIndex;
            }
            else
            {
                // CHECK FOR EXISTING SELECTED ITEM
                for (i = 0; i < cashierDataGridView.Rows.Count && !found && !foundEmptyRow; i++)
                {
                    if (null != cashierDataGridView.Rows[i].Cells["productName"].Value && null != cashierDataGridView.Rows[i].Cells["productID"].Value && productIDValid(cashierDataGridView.Rows[i].Cells["productID"].Value.ToString()))
                    {
                        if (cashierDataGridView.Rows[i].Cells["productName"].Value.ToString() == productName)
                        {
                            found = true;
                            rowSelectedIndex = i;

                            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : EXISTING ROW FOUND [" + rowSelectedIndex + "]");
                        }
                    }
                    else
                    {
                        foundEmptyRow = true;
                        emptyRowIndex = i;
                    }
                }

                if (!found)
                {
                    if (!foundEmptyRow)
                    {
                        addNewRow(false);
                        rowSelectedIndex = cashierDataGridView.Rows.Count - 1;

                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : NEW ROW ADDED [" + rowSelectedIndex + "]");
                    }
                    else
                    {
                        rowSelectedIndex = emptyRowIndex;
                    }
                }
            }

            DataGridViewRow selectedRow = cashierDataGridView.Rows[rowSelectedIndex];

            updateSomeRowContents(selectedRow, rowSelectedIndex, productID);

            if (!found)
            {
                if (stockIsEnough(productID, 1))
                {
                    selectedRow.Cells["qty"].Value = 1;
                    salesQty[rowSelectedIndex] = "1";
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : SET QTY TO 1 BECAUSE OF NEW ROW");
                }
                else
                {
                    selectedRow.Cells["qty"].Value = 0;
                    salesQty[rowSelectedIndex] = "0";
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : SET QTY TO 0 BECAUSE OF NEW ROW BUT STOCK IS NOT ENOUGH");
                }
            }
            else
            {
                currQty = Convert.ToDouble(salesQty[rowSelectedIndex]) + 1;

                if (stockIsEnough(productID, currQty))
                {
                    selectedRow.Cells["qty"].Value = currQty;
                    salesQty[rowSelectedIndex] = currQty.ToString();
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : QTY FOR THE EXISTING ROW [" + currQty.ToString() + "]");
                }
                else
                {
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : QTY FOR THE EXISTING ROW NOT CHANGED");
                }
            }

            selectedRow.Cells["jumlah"].Value = calculateSubTotal(rowSelectedIndex, Convert.ToDouble(selectedRow.Cells["productPrice"].Value));
            jumlahList[rowSelectedIndex] = selectedRow.Cells["jumlah"].Value.ToString();
            calculateTotal();

            cashierDataGridView.CurrentCell = selectedRow.Cells["qty"];
            cashierDataGridView.AllowUserToAddRows = true;
            //comboSelectedIndexChangeMethod(rowSelectedIndex, i, selectedRow);
            //cashierDataGridView.CurrentCell = cashierDataGridView.Rows[rowSelectedIndex].Cells["qty"];

            cashierDataGridView.Select();
            cashierDataGridView.BeginEdit(true);
        }

        private bool productIDValid(string productID)
        {
            bool result = false;

            if (isLoading)
                result = true;
            else if (0 < Convert.ToInt32(DS.getDataSingleValue("SELECT COUNT(1) FROM MASTER_PRODUCT WHERE PRODUCT_ID = '" + productID + "'")))
                result = true;

            return result;
        }

        //private string getSalesQuotationID()
        //{
        //    string salesInvoice = "";
        //    DateTime localDate = DateTime.Now;
        //    string maxSalesInvoice = "";
        //    double maxSalesInvoiceValue = 0;
        //    string salesInvPrefix;
        //    string sqlCommand = "";

        //    salesInvPrefix = "SQ-";//String.Format(culture, "{0:yyyyMMdd}", localDate);

        //    sqlCommand = "SELECT IFNULL(MAX(CONVERT(SUBSTRING(SQ_INVOICE, INSTR(SQ_INVOICE,'-')+1), UNSIGNED INTEGER)),'0') AS SQ_INVOICE FROM SALES_QUOTATION_HEADER WHERE SQ_INVOICE LIKE '" + salesInvPrefix + "%'";

        //    maxSalesInvoice = DS.getDataSingleValue(sqlCommand).ToString();
        //    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : MAX SALES INVOICE [" + maxSalesInvoice + "]");

        //    maxSalesInvoiceValue = Convert.ToInt32(maxSalesInvoice);
        //    if (maxSalesInvoiceValue > 0)
        //    {
        //        maxSalesInvoiceValue += 1;
        //        maxSalesInvoice = maxSalesInvoiceValue.ToString();
        //    }
        //    else
        //    {
        //        maxSalesInvoice = "1";
        //    }

        //    salesInvoice = salesInvPrefix + maxSalesInvoice;
        //    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : SALES INVOICE [" + salesInvoice + "]");

        //    return salesInvoice;
        //}

        private bool dataValidated()
        {
            double paymentAmount = 0;

            if (originModuleID != globalConstants.SALES_QUOTATION && originModuleID != globalConstants.EDIT_SALES_QUOTATION && originModuleID != globalConstants.SQ_TO_SO)
            {
                if (globalTotalValue <= 0)
                {
                    errorLabel.Text = "NILAI TRANSAKSI 0";
                    return false;
                }
            }

            if (cashierDataGridView.Rows.Count <= 1)
            {
                errorLabel.Text = "TIDAK ADA BARANG";
                return false;
            }

            for (int i = 0; i < cashierDataGridView.Rows.Count - 1; i++)
            {
                if (
                    ((null == cashierDataGridView.Rows[i].Cells["qty"].Value) ||
                    (0 == Convert.ToDouble(cashierDataGridView.Rows[i].Cells["qty"].Value))
                    ) && null != cashierDataGridView.Rows[i].Cells["productID"].Value)
                {
                    errorLabel.Text = "JUMLAH PRODUK DI BARIS " + (i + 1) + " = 0";
                    return false;
                }

                if (!stockIsEnough(cashierDataGridView.Rows[i].Cells["productID"].Value.ToString(), Convert.ToDouble(cashierDataGridView.Rows[i].Cells["qty"].Value)))
                {
                    errorLabel.Text = "JUMLAH PRODUK DI BARIS " + (i + 1) + " TIDAK CUKUP";
                    return false;
                }

                if (
                    ((null == cashierDataGridView.Rows[i].Cells["jumlah"].Value) ||
                    (0 >= Convert.ToDouble(cashierDataGridView.Rows[i].Cells["jumlah"].Value))
                    ) && null != cashierDataGridView.Rows[i].Cells["productID"].Value)
                {
                    errorLabel.Text = "PEMBELIAN DI BARIS " + (i + 1) + " TIDAK VALID";
                    return false;
                }

                if (null == cashierDataGridView.Rows[i].Cells["productID"].Value)
                {
                    cashierDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    //errorLabel.Text = "KODE PRODUK DI BARIS " + (i + 1) + " TIDAK VALID";
                    //return false;
                }
                else if (!productIDValid(cashierDataGridView.Rows[i].Cells["productID"].Value.ToString()))
                {
                    cashierDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    //errorLabel.Text = "KODE PRODUK DI BARIS " + (i + 1) + " TIDAK VALID";
                    //return false;
                }
            }

            if (selectedPelangganID == 0)
                if (DialogResult.No == MessageBox.Show("PELANGGAN KOSONG, LANJUTKAN ?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    return false;

            if (originModuleID != globalConstants.SALES_QUOTATION && originModuleID != globalConstants.EDIT_SALES_QUOTATION && referenceSO.Length<=0)
            {
                if (cashRadioButton.Checked)
                {
                    // CHECK PAYMENT AMOUNT FOR CASH PAYMENT
                    if (bayarTextBox.Text.Length <= 0)
                    {
                        errorLabel.Text = "JUMLAH PEMBAYARAN 0";
                        return false;
                    }

                    // CHECK PAYMENT AMOUNT MUST BE MORE OR EQUALS THAN THE BILL
                    paymentAmount = Convert.ToDouble(bayarAmount);
                    if (paymentAmount < globalTotalValue - discValue)
                    {
                        errorLabel.Text = "JUMLAH PEMBAYARAN LEBIH KECIL DARI NOTA";
                        return false;
                    }
                }
            }

            if (originModuleID == globalConstants.EDIT_SALES_ORDER)
            {
                paymentAmount = Convert.ToDouble(bayarAmount);
                if (paymentAmount > globalTotalValue - discValue)
                {
                    if (DialogResult.No == MessageBox.Show("JUMLAH DP LEBIH BESAR DARI TRANSAKSI, LANJUTKAN ?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                        return false;
                }
            }

            if (creditRadioButton.Checked)
            {
                if (selectedPelangganID == 0)
                {
                    errorLabel.Text = "PELANGGAN TIDAK BOLEH KOSONG UNTUK PESANAN";
                    return false;
                }
            }

            errorLabel.Text = "";
            return true;
        }

        private bool saveDataTransaction()
        {
            bool result = false;
            string sqlCommand = "";

            string salesInvoice = "0";
            string salesInvoiceTax = "0";

            string SODateTime = "";
            DateTime SODueDateTimeValue;
            string SODueDateTime = "";
            string salesDiscountFinal = "0";
            int salesTop = 1;
            int salesPaid = 0;
            MySqlException internalEX = null;

            double disc1 = 0;
            double disc2 = 0;
            double discRP = 0;
            string productID = "";
            //int paymentMethod = 0;

            double currentTaxTotal = 0;
            double currentSalesTotal = 0;
            double taxLimitValue = 0;
            double parameterCalculation = 0;
            int taxLimitType = 0; // 0 - percentage, 1 - amount
            string salesDateValue = "";
            bool addToTaxTable = false;

            string SOOrderDate = "";
            int delivered = 0;
            double DPAmount = 0;
            string userName = "";

            int editedFlag = 0;
            int syncFlag = 0;
            string tempSalesID = "";

            //SODateTime = String.Format(culture, "{0:dd-MM-yyyy HH:mm}", DateTime.Now);
            //SODateTime = gutil.getCustomStringFormatDate(DateTime.Now);
            SODateTime = gutil.getCustomStringFormatDate(SODateTimePicker.Value); // LARAS : Transaction Date is based on user input

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ATTEMPT TO SAVE SALES DATA [" + SODateTime + "]");

            if (discJualMaskedTextBox.Text.Length > 0)
            {
                salesDiscountFinal = discValue.ToString();
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : SALES DISC FINAL [" + discJualMaskedTextBox.Text + "]");
            }

            if (bayarAmount < globalTotalValue - discValue)
                salesPaid = 0;
            else
                salesPaid = 1;

            if (cashRadioButton.Checked)
            {
                salesTop = 1;

                //SODueDateTime = String.Format(culture, "{0:dd-MM-yyyy}", DateTime.Now);
                SODueDateTime = String.Format(culture, "{0:dd-MM-yyyy}", SODateTimePicker.Value); // LARAS : Transaction Date is based on user input
                SOOrderDate = gutil.getCustomStringFormatDate(SODateTimePicker.Value, true);

                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : CASH SALES");
                //paymentMethod = paymentComboBox.SelectedIndex;
            }
            else
            {
                salesTop = 0;
                SOisPesanan = true;
                userName = DS.getDataSingleValue("SELECT IFNULL(USER_FULL_NAME, 0) FROM MASTER_USER WHERE ID = " + gutil.getUserID()).ToString();
                SODueDateTimeValue = jobStartDateTimePicker.Value;
                SOOrderDate = gutil.getCustomStringFormatDate(jobStartDateTimePicker.Value);

                if (deliveredCheckbox.Checked)
                    delivered = 1;
                else
                    delivered = 0;
                //SODueDateTimeValue.AddDays(Convert.ToInt32(tempoMaskedTextBox.Text));
                //SODueDateTimeValue = SODueDateTimeValue.AddDays(Convert.ToInt32(tempoMaskedTextBox.Text));
                SODueDateTime = String.Format(culture, "{0:dd-MM-yyyy}", SODueDateTimeValue);

                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : NON CASH - SALES DUE DATE [" + SODueDateTime + "]");
            }


            if (originModuleID == 0)
            {
                // TAX LIMIT CALCULATION
                // ----------------------------------------------------------------------
                salesDateValue = String.Format(culture, "{0:yyyyMMdd}", DateTime.Now);
                currentTaxTotal = Convert.ToDouble(DS.getDataSingleValue("SELECT IFNULL(SUM(SALES_TOTAL), 0) AS TOTAL FROM SALES_HEADER_TAX WHERE DATE_FORMAT(SALES_DATE, '%Y%m%d') = '" + salesDateValue + "'"));
                currentSalesTotal = Convert.ToDouble(DS.getDataSingleValue("SELECT IFNULL(SUM(SALES_TOTAL), 0) AS TOTAL FROM SALES_HEADER WHERE DATE_FORMAT(SALES_DATE, '%Y%m%d') = '" + salesDateValue + "'"));

                // CHECK WHETHER THE PARAMETER FOR TAX CALCULATION HAS BEEN SET
                taxLimitValue = Convert.ToDouble(DS.getDataSingleValue("SELECT IFNULL(PERSENTASE_PENJUALAN, 0) FROM SYS_CONFIG_TAX WHERE ID = 1"));
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CHECK IF TAX LIMIT SET FOR PERCENTAGE PURCHASE [" + taxLimitValue + "]");

                if (taxLimitValue == 0)
                {
                    taxLimitType = 1;
                    taxLimitValue = Convert.ToDouble(DS.getDataSingleValue("SELECT IFNULL(AVERAGE_PENJUALAN_HARIAN, 0) FROM SYS_CONFIG_TAX WHERE ID = 1"));
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CHECK IF TAX LIMIT SET FOR AVERAGE DAILY PURCHASE [" + taxLimitValue + "]");

                    if (taxLimitValue != 0)
                        addToTaxTable = true;
                }
                else
                    addToTaxTable = true;

                // CHECK WHETHER THE PARAMETER HAS BEEN FULFILLED
                if (addToTaxTable)
                {
                    if (taxLimitType == 0) // PERCENTAGE CALCULATION
                    {
                        parameterCalculation = currentSalesTotal * taxLimitValue / 100;
                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "PERCENTAGE CALCULATION [" + parameterCalculation + "]");

                        if (currentTaxTotal > parameterCalculation)
                        {
                            addToTaxTable = false;
                            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CURRENT TAX TOTAL IS BIGGER THAN PARAMETER CALCULATION");
                        }
                    }
                    else // AMOUNT CALCULATION
                    {
                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "AMOUNT CALCULATION [" + taxLimitValue + "]");
                        if (currentTaxTotal > taxLimitValue)
                        {
                            addToTaxTable = false;
                            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CURRENT TAX TOTAL IS BIGGER THAN AMOUNT CALCULATION");
                        }
                    }
                }
            }
            else if (originModuleID == globalConstants.DUMMY_TRANSACTION_TAX)
                addToTaxTable = true;
            // ----------------------------------------------------------------------

            DS.beginTransaction();

           // double realGlobalTotalValue = globalTotalValue + bayarAmount;

            try
            {
                DS.mySqlConnect();

                // SAVE TO HEADER TABLE
                // ======================================================================================================
                if (originModuleID == 0 || originModuleID == globalConstants.SO_FULFILLMENT)   // NORMAL TRANSACTION 
                {
                    salesInvoice = getSalesInvoiceID();
                    //pass thru to receipt generator

                    selectedsalesinvoice = salesInvoice;

                    //SAVE HEADER TABLE
                    if (!SOisPesanan && originModuleID != globalConstants.SO_FULFILLMENT)
                    {
                        //if (gutil.isServerApp() == 0)
                            sqlCommand = "INSERT INTO SALES_HEADER (SALES_INVOICE, CUSTOMER_ID, SALES_DATE, SALES_TOTAL, SALES_DISCOUNT_FINAL, SALES_TOP, SALES_TOP_DATE, SALES_PAID, SALES_PAYMENT, SALES_PAYMENT_CHANGE, SALES_PAYMENT_METHOD, REFERENCE_SO) " +
                                                "VALUES " +
                                                "('" + salesInvoice + "', " + selectedPelangganID + ", STR_TO_DATE('" + SODateTime + "', '%d-%m-%Y %H:%i'), " + gutil.validateDecimalNumericInput(globalTotalValue) + ", " + gutil.validateDecimalNumericInput(Convert.ToDouble(salesDiscountFinal)) + ", " + salesTop + ", STR_TO_DATE('" + SODueDateTime + "', '%d-%m-%Y'), " + salesPaid + ", " + gutil.validateDecimalNumericInput(bayarAmount) + ", " + gutil.validateDecimalNumericInput(sisaBayar) + ", " + selectedPaymentMethod + ", '" + referenceSO + "')";
                        //else
                        //{
                        //    tempSalesID = gutil.getAutoGenerateID("SALES_HEADER", "", "", "ID");

                        //    sqlCommand = "INSERT INTO SALES_HEADER (ID, SALES_INVOICE, CUSTOMER_ID, SALES_DATE, SALES_TOTAL, SALES_DISCOUNT_FINAL, SALES_TOP, SALES_TOP_DATE, SALES_PAID, SALES_PAYMENT, SALES_PAYMENT_CHANGE, SALES_PAYMENT_METHOD, REFERENCE_SO) " +
                        //                            "VALUES " +
                        //                            "('" + tempSalesID + "', '" + salesInvoice + "', " + selectedPelangganID + ", STR_TO_DATE('" + SODateTime + "', '%d-%m-%Y %H:%i'), " + gutil.validateDecimalNumericInput(globalTotalValue) + ", " + gutil.validateDecimalNumericInput(Convert.ToDouble(salesDiscountFinal)) + ", " + salesTop + ", STR_TO_DATE('" + SODueDateTime + "', '%d-%m-%Y'), " + salesPaid + ", " + gutil.validateDecimalNumericInput(bayarAmount) + ", " + gutil.validateDecimalNumericInput(sisaBayar) + ", " + selectedPaymentMethod + ", '" + referenceSO + "')";
                        //}
                    }
                    else
                    {
                        //if (gutil.isServerApp() == 0)
                        sqlCommand = "INSERT INTO SALES_HEADER (SALES_INVOICE, CUSTOMER_ID, SALES_DATE, SALES_TOTAL, SALES_DISCOUNT_FINAL, SALES_TOP, SALES_TOP_DATE, SALES_PAID, SALES_PAYMENT, SALES_PAYMENT_CHANGE, SALES_PAYMENT_METHOD, SALES_ORDER_DATE, SALES_ORDER_DELIVERED, SALES_ORDER_DELIVERED_ADDRESS, REFERENCE_SO, SYNCHRONIZED, EDITED, USER_NAME) " +
                                                "VALUES " +
                                                "('" + salesInvoice + "', " + selectedPelangganID + ", STR_TO_DATE('" + SODateTime + "', '%d-%m-%Y %H:%i'), " + gutil.validateDecimalNumericInput(globalTotalValue) + ", " + gutil.validateDecimalNumericInput(Convert.ToDouble(salesDiscountFinal)) + ", " + salesTop + ", STR_TO_DATE('" + SODueDateTime + "', '%d-%m-%Y'), " + salesPaid + ", " + gutil.validateDecimalNumericInput(bayarAmount) + ", " + gutil.validateDecimalNumericInput(sisaBayar) + ", " + selectedPaymentMethod + ", STR_TO_DATE('" + SOOrderDate + "', '%d-%m-%Y %H:%i'), " + delivered + ", '" + addressTextBox.Text + "', '" + referenceSO + "', 0, 1, '" + userName + "')";
                        //else
                        //{
                        //    tempSalesID = gutil.getAutoGenerateID("SALES_HEADER", "", "", "ID");
                        //    sqlCommand = "INSERT INTO SALES_HEADER (ID, SALES_INVOICE, CUSTOMER_ID, SALES_DATE, SALES_TOTAL, SALES_DISCOUNT_FINAL, SALES_TOP, SALES_TOP_DATE, SALES_PAID, SALES_PAYMENT, SALES_PAYMENT_CHANGE, SALES_PAYMENT_METHOD, SALES_ORDER_DATE, SALES_ORDER_DELIVERED, SALES_ORDER_DELIVERED_ADDRESS, REFERENCE_SO, SYNCHRONIZED, EDITED) " +
                        //                            "VALUES " +
                        //                            "('" + tempSalesID + "', '" + salesInvoice + "', " + selectedPelangganID + ", STR_TO_DATE('" + SODateTime + "', '%d-%m-%Y %H:%i'), " + gutil.validateDecimalNumericInput(globalTotalValue) + ", " + gutil.validateDecimalNumericInput(Convert.ToDouble(salesDiscountFinal)) + ", " + salesTop + ", STR_TO_DATE('" + SODueDateTime + "', '%d-%m-%Y'), " + salesPaid + ", " + gutil.validateDecimalNumericInput(bayarAmount) + ", " + gutil.validateDecimalNumericInput(sisaBayar) + ", " + selectedPaymentMethod + ", STR_TO_DATE('" + SOOrderDate + "', '%d-%m-%Y'), " + delivered + ", '" + addressTextBox.Text + "', '" + referenceSO + "', 0, 1)";
                        //}
                    }

                    syncFlag = 0;
                    editedFlag = 1;

                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "INSERT INTO SALES HEADER [" + salesInvoice + "]");
                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;

                    if (SOisPesanan && originModuleID != globalConstants.SO_FULFILLMENT && gutil.isServerApp() == 0)
                    {
                        sqlCommand = "UPDATE NOTIF_TABLE SET NEW_ORDER = 1, EDITED = 2";

                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "UPDATE NOTIF_TABLE [" + salesInvoice + "]");
                        if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                            throw internalEX;
                    }
                }
                else if (originModuleID == globalConstants.EDIT_SALES_ORDER)
                {
                    salesInvoice = selectedsalesinvoice;
                    syncFlag = Convert.ToInt32(DS.getDataSingleValue("SELECT SYNCHRONIZED FROM SALES_HEADER WHERE SALES_INVOICE = '" + selectedsalesinvoice + "'"));

                    if (syncFlag == 0) // NEW DATA THAT HAS NOT BEEN SYNCHRONIZED
                        editedFlag = 1;
                    else
                        editedFlag = 2;

                    // UPDATE HEADER DATA
                    sqlCommand = "UPDATE SALES_HEADER SET " +
                                          "CUSTOMER_ID = " + selectedPelangganID + ", " +
                                          "SALES_DATE = STR_TO_DATE('" + SODateTime + "', '%d-%m-%Y %H:%i'), " +
                                          "SALES_TOTAL = " + gutil.validateDecimalNumericInput(globalTotalValue) + ", " +
                                          "SALES_DISCOUNT_FINAL = " + gutil.validateDecimalNumericInput(Convert.ToDouble(salesDiscountFinal)) + ", " +
                                          "SALES_TOP = " + salesTop + ", " +
                                          "SALES_TOP_DATE = STR_TO_DATE('" + SODueDateTime + "', '%d-%m-%Y'), " +
                                          "SALES_PAID = " + salesPaid + ", " +
                                          "SALES_PAYMENT = " + gutil.validateDecimalNumericInput(bayarAmount) + ", " +
                                          "SALES_PAYMENT_CHANGE = " + gutil.validateDecimalNumericInput(sisaBayar) + ", " +
                                          "SALES_PAYMENT_METHOD = " + selectedPaymentMethod + ", " +
                                          "SALES_ORDER_DATE = STR_TO_DATE('" + SOOrderDate + "', '%d-%m-%Y %H:%i'), " +
                                          "SALES_ORDER_DELIVERED = " + delivered + ", " +
                                          "SALES_ORDER_DELIVERED_ADDRESS = '" + addressTextBox.Text + "', " +
                                          "REFERENCE_SO = '" + referenceSO + "', " +
                                          "EDITED = " + editedFlag + ", " +
                                          "USER_NAME = '" + userName + "' " +
                                          "WHERE SALES_INVOICE = '" + selectedsalesinvoice + "'";

                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "UPDATE SALES HEADER [" + salesInvoice + "]");
                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;

                    // DELETE DETAIL DATA
                    sqlCommand = "DELETE FROM SALES_DETAIL WHERE SALES_INVOICE = '" + selectedsalesinvoice + "'";
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "DELETE SALES DETAIL [" + salesInvoice + "]");
                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;
                }

                if (addToTaxTable)
                {
                    salesInvoiceTax = getSalesInvoiceID(false);
                    selectedsalesinvoiceTax = salesInvoiceTax;
                    sqlCommand = "INSERT INTO SALES_HEADER_TAX (SALES_INVOICE, ORIGIN_SALES_INVOICE, CUSTOMER_ID, SALES_DATE, SALES_TOTAL, SALES_DISCOUNT_FINAL, SALES_TOP, SALES_TOP_DATE, SALES_PAID, SALES_PAYMENT, SALES_PAYMENT_CHANGE, SALES_PAYMENT_METHOD, ORIGIN_SALES_INVOICE) " +
                                    "VALUES " +
                                    "('" + salesInvoiceTax + "', '" + salesInvoice + "', " + selectedPelangganID + ", STR_TO_DATE('" + SODateTime + "', '%d-%m-%Y %H:%i'), " + gutil.validateDecimalNumericInput(globalTotalValue) + ", " + gutil.validateDecimalNumericInput(Convert.ToDouble(salesDiscountFinal)) + ", " + salesTop + ", STR_TO_DATE('" + SODueDateTime + "', '%d-%m-%Y'), " + salesPaid + ", " + gutil.validateDecimalNumericInput(bayarAmount) + ", " + gutil.validateDecimalNumericInput(sisaBayar) + ", " + selectedPaymentMethod + ", " + selectedsalesinvoice + ")";

                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "INSERT INTO SALES HEADER TAX [" + salesInvoice + "]");
                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;
                }
                // ======================================================================================================


                // SAVE TO DETAIL TABLE
                // ======================================================================================================
                for (int i = 0; i < cashierDataGridView.Rows.Count; i++)
                {
                    if (null != cashierDataGridView.Rows[i].Cells["productID"].Value && productIDValid(cashierDataGridView.Rows[i].Cells["productID"].Value.ToString()))
                    {
                        disc1 = Convert.ToDouble(cashierDataGridView.Rows[i].Cells["disc1"].Value);
                        disc2 = Convert.ToDouble(cashierDataGridView.Rows[i].Cells["disc2"].Value);
                        discRP = Convert.ToDouble(cashierDataGridView.Rows[i].Cells["discRP"].Value);
                        productID = cashierDataGridView.Rows[i].Cells["productID"].Value.ToString();

                        if (originModuleID == 0 || originModuleID == globalConstants.EDIT_SALES_ORDER || originModuleID == globalConstants.SO_FULFILLMENT) // NORMAL TRANSACTION 
                        {
                            if (!SOisPesanan && originModuleID != globalConstants.SO_FULFILLMENT)
                                sqlCommand = "INSERT INTO SALES_DETAIL (SALES_INVOICE, PRODUCT_ID, PRODUCT_PRICE, PRODUCT_SALES_PRICE, PRODUCT_QTY, PRODUCT_DISC1, PRODUCT_DISC2, PRODUCT_DISC_RP, SALES_SUBTOTAL) " +
                                                    "VALUES " +
                                                    "('" + salesInvoice + "', '" + productID + "', " + Convert.ToDouble(cashierDataGridView.Rows[i].Cells["hpp"].Value) + ", " + Convert.ToDouble(cashierDataGridView.Rows[i].Cells["productPrice"].Value) + ", " +
                                                    gutil.validateDecimalNumericInput(Convert.ToDouble(cashierDataGridView.Rows[i].Cells["qty"].Value)) + ", " + gutil.validateDecimalNumericInput(disc1) + ", " + gutil.validateDecimalNumericInput(disc2) + ", " + gutil.validateDecimalNumericInput(discRP) + ", " + gutil.validateDecimalNumericInput(Convert.ToDouble(cashierDataGridView.Rows[i].Cells["jumlah"].Value)) + ")";
                            else
                                sqlCommand = "INSERT INTO SALES_DETAIL (SALES_INVOICE, PRODUCT_ID, PRODUCT_PRICE, PRODUCT_SALES_PRICE, PRODUCT_QTY, PRODUCT_DISC1, PRODUCT_DISC2, PRODUCT_DISC_RP, SALES_SUBTOTAL, SYNCHRONIZED, EDITED) " +
                                                    "VALUES " +
                                                    "('" + salesInvoice + "', '" + productID + "', " + Convert.ToDouble(cashierDataGridView.Rows[i].Cells["hpp"].Value) + ", " + Convert.ToDouble(cashierDataGridView.Rows[i].Cells["productPrice"].Value) + ", " +
                                                    gutil.validateDecimalNumericInput(Convert.ToDouble(cashierDataGridView.Rows[i].Cells["qty"].Value)) + ", " + gutil.validateDecimalNumericInput(disc1) + ", " + gutil.validateDecimalNumericInput(disc2) + ", " + gutil.validateDecimalNumericInput(discRP) + ", " + gutil.validateDecimalNumericInput(Convert.ToDouble(cashierDataGridView.Rows[i].Cells["jumlah"].Value)) + ", " + syncFlag + ", " + editedFlag + ")";

                            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "INSERT INTO SALES DETAIL[" + productID + ", " + Convert.ToDouble(cashierDataGridView.Rows[i].Cells["productPrice"].Value) + ", " +
                                                gutil.validateDecimalNumericInput(Convert.ToDouble(cashierDataGridView.Rows[i].Cells["qty"].Value)) + "]");

                            if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                                throw internalEX;

                            if (originModuleID == globalConstants.SO_FULFILLMENT)
                            {
                                sqlCommand = "INSERT INTO SALES_DETAIL_FULFILLMENT (SALES_INVOICE, FULFILLMENT_DATE, PRODUCT_ID, PRODUCT_QTY, SYNCHRONIZED, EDITED) " +
                                                    "VALUES " +
                                                    "('" + salesInvoice + "', STR_TO_DATE('" + SODateTime + "', '%d-%m-%Y %H:%i'), '" + productID + "', " + gutil.validateDecimalNumericInput(Convert.ToDouble(cashierDataGridView.Rows[i].Cells["qty"].Value)) + ", 0, 1)";

                                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "INSERT INTO SALES DETAIL FULFILLMENT [" + productID + ", " + gutil.validateDecimalNumericInput(Convert.ToDouble(cashierDataGridView.Rows[i].Cells["qty"].Value)) + "]");

                                if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                                    throw internalEX;
                            }
                            //else
                            {
                                if (!gutil.productIsService(cashierDataGridView.Rows[i].Cells["productID"].Value.ToString()))
                                {
                                    // ONLY DIRECTLY REDUCE STOCK IF CASH TRANSACTION
                                    if (salesTop == 1)
                                    {
                                        // REDUCE STOCK QTY AT MASTER PRODUCT
                                        sqlCommand = "UPDATE MASTER_PRODUCT SET PRODUCT_STOCK_QTY = PRODUCT_STOCK_QTY - " + Convert.ToDouble(cashierDataGridView.Rows[i].Cells["qty"].Value) +
                                                            " WHERE PRODUCT_ID = '" + cashierDataGridView.Rows[i].Cells["productID"].Value.ToString() + "'";

                                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "REDUCE STOCK AT MASTER PRODUCT [" + cashierDataGridView.Rows[i].Cells["productID"].Value.ToString() + "]");
                                        if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                                            throw internalEX;
                                    }
                                }

                                // SAVE OR UPDATE TO CUSTOMER_PRODUCT_DISC
                                if (selectedPelangganID != 0 && originModuleID != globalConstants.SO_FULFILLMENT)
                                {
                                    if (Convert.ToInt32(DS.getDataSingleValue("SELECT COUNT(1) FROM CUSTOMER_PRODUCT_DISC WHERE CUSTOMER_ID = " + selectedPelangganID + " AND PRODUCT_ID = '" + cashierDataGridView.Rows[i].Cells["productID"].Value.ToString() + "'")) > 0)
                                    {
                                        // UPDATE VALUE
                                        sqlCommand = "UPDATE CUSTOMER_PRODUCT_DISC SET DISC_1 = " + gutil.validateDecimalNumericInput(disc1) + ", DISC_2 = " + gutil.validateDecimalNumericInput(disc2) + ", DISC_RP = " + gutil.validateDecimalNumericInput(discRP) + " WHERE CUSTOMER_ID = " + selectedPelangganID + " AND PRODUCT_ID = '" + productID + "'";
                                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "UPDATE CUSTOMER PRODUCT DISC [" + productID + "]");
                                    }
                                    else
                                    {
                                        // INSERT VALUE
                                        sqlCommand = "INSERT INTO CUSTOMER_PRODUCT_DISC (CUSTOMER_ID, PRODUCT_ID, DISC_1, DISC_2 , DISC_RP) VALUES " +
                                                            "(" + selectedPelangganID + ", '" + productID + "', " + gutil.validateDecimalNumericInput(disc1) + ", " + gutil.validateDecimalNumericInput(disc2) + ", " + gutil.validateDecimalNumericInput(discRP) + ")";
                                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "INSERT CUSTOMER PRODUCT DISC [" + productID + "]");
                                    }

                                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                                        throw internalEX;
                                }
                            }
                        }

                        if (addToTaxTable)
                        {
                            sqlCommand = "INSERT INTO SALES_DETAIL_TAX (SALES_INVOICE, PRODUCT_ID, PRODUCT_PRICE, PRODUCT_SALES_PRICE, PRODUCT_QTY, PRODUCT_DISC1, PRODUCT_DISC2, PRODUCT_DISC_RP, SALES_SUBTOTAL) " +
                                                "VALUES " +
                                                "('" + salesInvoiceTax + "', '" + productID + "', " + Convert.ToDouble(cashierDataGridView.Rows[i].Cells["hpp"].Value) + ", " + Convert.ToDouble(cashierDataGridView.Rows[i].Cells["productPrice"].Value) + ", " +
                                                gutil.validateDecimalNumericInput(Convert.ToDouble(cashierDataGridView.Rows[i].Cells["qty"].Value)) + ", " + gutil.validateDecimalNumericInput(disc1) + ", " + gutil.validateDecimalNumericInput(disc2) + ", " + gutil.validateDecimalNumericInput(discRP) + ", " + gutil.validateDecimalNumericInput(Convert.ToDouble(cashierDataGridView.Rows[i].Cells["jumlah"].Value)) + ")";

                            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "INSERT INTO SALES DETAIL TAX [" + productID + "]");
                            if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                                throw internalEX;
                        }
                    }
                }
                // ======================================================================================================

                // INSERT INTO DAILY JOURNAL FOR CASH TRANSACTION
                // ======================================================================================================
                if (originModuleID == 0)  // NORMAL TRANSACTION 
                {
                    double journalAmount = 0;
                    string journalDesc = "";
                    // SAVE TO CREDIT TABLE
                    if (gutil.isServerApp() == 0)
                        sqlCommand = "INSERT INTO CREDIT (SALES_INVOICE, CREDIT_DUE_DATE, CREDIT_NOMINAL, CREDIT_PAID) VALUES " +
                                            "('" + salesInvoice + "', STR_TO_DATE('" + SODueDateTime + "', '%d-%m-%Y'), " + gutil.validateDecimalNumericInput(globalTotalValue-discValue) + ", " + salesPaid + ")";
                    else
                    {
                        tempSalesID = gutil.getAutoGenerateID("CREDIT", "", "", "CREDIT_ID");

                        sqlCommand = "INSERT INTO CREDIT (CREDIT_ID, SALES_INVOICE, CREDIT_DUE_DATE, CREDIT_NOMINAL, CREDIT_PAID) VALUES " +
                                            "('" + tempSalesID + "', '" + salesInvoice + "', STR_TO_DATE('" + SODueDateTime + "', '%d-%m-%Y'), " + gutil.validateDecimalNumericInput(globalTotalValue - discValue) + ", " + salesPaid + ")";
                    }

                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "INSERT TO CREDIT TABLE [" + salesInvoice + "]");
                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;

                    if (salesTop == 1 && originModuleID == 0) // PENJUALAN TUNAI
                    {
                        // TOTAL AMOUNT - DISC
                        journalAmount = globalTotalValue - discValue;
                        journalDesc = "PEMBAYARAN " + salesInvoice;
                    }
                    else if (salesTop == 0)
                    {
                        // ONLY SAVE DP
                        if (bayarAmount <= globalTotalValue - discValue)
                            journalAmount = bayarAmount;
                        else
                            journalAmount = globalTotalValue - discValue;

                        journalDesc = "PEMBAYARAN DP " + salesInvoice;
                    }

                    // PAYMENT IN CASH THEREFORE ADDING THE AMOUNT OF CASH IN THE CASH REGISTER
                    // ADD A NEW ENTRY ON THE DAILY JOURNAL TO KEEP TRACK THE ADDITIONAL CASH AMOUNT 
                    if (gutil.isServerApp() == 0)
                        sqlCommand = "INSERT INTO DAILY_JOURNAL (ACCOUNT_ID, JOURNAL_DATETIME, JOURNAL_NOMINAL, JOURNAL_DESCRIPTION, USER_ID, PM_ID) " +
                                                           "VALUES (1, STR_TO_DATE('" + SODateTime + "', '%d-%m-%Y %H:%i')" + ", " + gutil.validateDecimalNumericInput(journalAmount) + ", '" + journalDesc + "', '" + gutil.getUserID() + "', 1)";
                    else
                    {
                        tempSalesID = gutil.getAutoGenerateID("DAILY_JOURNAL", "", "", "JOURNAL_ID");

                        sqlCommand = "INSERT INTO DAILY_JOURNAL (JOURNAL_ID, ACCOUNT_ID, JOURNAL_DATETIME, JOURNAL_NOMINAL, JOURNAL_DESCRIPTION, USER_ID, PM_ID) " +
                                                           "VALUES ('" +tempSalesID+ "', 1, STR_TO_DATE('" + SODateTime + "', '%d-%m-%Y %H:%i')" + ", " + gutil.validateDecimalNumericInput(journalAmount) + ", '" + journalDesc + "', '" + gutil.getUserID() + "', 1)";

                    }
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "INSERT TO DAILY JOURNAL TABLE [" + gutil.validateDecimalNumericInput(journalAmount) + "]");

                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;
                    //else if (originModuleID == globalConstants.SQ_TO_SO)
                    //{

                    //    //// CALCULATE DIFFERENCE WITH PREVIOUS DP AMOUNT
                    //    ////DPAmount = bayarAmount - previousSQ_DP;

                    //    //if (DPAmount != 0)
                    //    //{
                    //    //    // ONLY SALES QUOTATION ABLE TO ADD DP TO JOURNAL
                    //    //    sqlCommand = "INSERT INTO DAILY_JOURNAL (ACCOUNT_ID, JOURNAL_DATETIME, JOURNAL_NOMINAL, JOURNAL_DESCRIPTION, USER_ID, PM_ID) " +
                    //    //                                   "VALUES (1, STR_TO_DATE('" + SODateTime + "', '%d-%m-%Y %H:%i')" + ", " + gutil.validateDecimalNumericInput(DPAmount) + ", 'PEMBAYARAN " + salesInvoice + "', '" + gutil.getUserID() + "', 1)";
                    //    //    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "INSERT TO DAILY JOURNAL TABLE [" + gutil.validateDecimalNumericInput(DPAmount) + "]");

                    //    //    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                    //    //        throw internalEX;
                    //    //}
                    //}
                }
                //else if (originModuleID == globalConstants.SALES_QUOTATION || originModuleID == globalConstants.EDIT_SALES_QUOTATION)
                //{
                //    //string journalDate = "";

                //    //if (originModuleID == globalConstants.SALES_QUOTATION)   // JOURNAL DATE IS THE SAME AS SQ DATE IF NEW SALES QUOTATION
                //    //    journalDate = SODateTime;
                //    //else if (originModuleID == globalConstants.EDIT_SALES_QUOTATION) // TAKE CURRENT DATE FOR EDIT SALES QUOTATION
                //    //    journalDate = String.Format(culture, "{0:dd-MM-yyyy}", DateTime.Now);

                //    //// CALCULATE DIFFERENCE WITH PREVIOUS DP AMOUNT
                //    //DPAmount = bayarAmount - previousSQ_DP;

                //    //if (DPAmount != 0)
                //    //{
                //    //    // ONLY SALES QUOTATION ABLE TO ADD DP TO JOURNAL
                //    //    sqlCommand = "INSERT INTO DAILY_JOURNAL (ACCOUNT_ID, JOURNAL_DATETIME, JOURNAL_NOMINAL, JOURNAL_DESCRIPTION, USER_ID, PM_ID) " +
                //    //                                   "VALUES (1, STR_TO_DATE('" + SODateTime + "', '%d-%m-%Y %H:%i')" + ", " + gutil.validateDecimalNumericInput(DPAmount) + ", 'PEMBAYARAN DP " + salesInvoice + "', '" + gutil.getUserID() + "', 1)";
                //    //    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "INSERT TO DAILY JOURNAL TABLE [" + gutil.validateDecimalNumericInput(DPAmount) + "]");

                //    //    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                //    //        throw internalEX;
                //    //}
                //}
                //// ======================================================================================================

                DS.commit();
                result = true;
            }
            catch (Exception e)
            {
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "EXCEPTION THROWN [" + e.Message + "]");

                try
                {
                    DS.rollBack(ref internalEX);
                }
                catch (MySqlException ex)
                {
                    if (DS.getMyTransConnection() != null)
                    {
                        gutil.showDBOPError(ex, "ROLLBACK");
                    }
                }

                gutil.showDBOPError(e, "INSERT");
                result = false;
            }
            finally
            {
                DS.mySqlClose();
            }

            return result;
        }

        private bool saveData()
        {
            bool result = false;
            if (dataValidated())
            {
                smallPleaseWait pleaseWait = new smallPleaseWait();
                pleaseWait.Show();

                //  ALlow main UI thread to properly display please wait form.
                Application.DoEvents();
                result = saveDataTransaction();

                pleaseWait.Close();

                return result;
            }

            return result;
        }

        private bool saveDPToPaymentCredit(bool fullyPaid = false)
        {
            bool result = false;
            string sqlCommand = "";
            int creditID = 0;
            double DPAmount = 0;
            //double prevDPAmount = 0;
            string paymentDate = String.Format(culture, "{0:dd-MM-yyyy}", DateTime.Now);
            MySqlException internalEX = null;
            //string SQCreatedDate = "";
            string invoiceNo = "";
            int isDPPayment = 0;
            string paymentDesc = "";
            double paymentAmount = 0;
            string tempID = "";

            DS.beginTransaction();

            try
            {
                if (!SOisPesanan) // CASH TRANSACTION, SAVE AS NORMAL PAYMENT
                {
                    isDPPayment = 0;
                    paymentDesc = "PEMBAYARAN " + selectedsalesinvoice;
                    paymentAmount = globalTotalValue - discValue;
                }
                else // PESANAN, SAVE AS DP
                {
                    isDPPayment = 1;
                    paymentDesc = "PEMBAYARAN DP " + selectedsalesinvoice;

                    if (bayarAmount <= (globalTotalValue - discValue))
                        paymentAmount = bayarAmount;
                    else
                        paymentAmount = globalTotalValue - discValue;
                }

                // GET CREDIT ID
                sqlCommand = "SELECT COUNT(1) FROM CREDIT WHERE SALES_INVOICE = '" + selectedsalesinvoice + "'";
                creditID = Convert.ToInt32(DS.getDataSingleValue(sqlCommand));

                if (creditID > 0)
                {
                    sqlCommand = "SELECT CREDIT_ID FROM CREDIT WHERE SALES_INVOICE = '" + selectedsalesinvoice + "'";
                    creditID = Convert.ToInt32(DS.getDataSingleValue(sqlCommand));

                    // CHECK WHETHER INVOICE HAS BEEN FULLY PAID
                    //sqlCommand = "SELECT CREDIT_PAID FROM CREDIT WHERE CREDIT_ID = " + creditID;
                    //if (DS.getDataSingleValue(sqlCommand).ToString() == "0")
                    {
                        // INSERT INTO PAYMENT CREDIT
                        if (gutil.isServerApp() == 0)
                            sqlCommand = "INSERT INTO PAYMENT_CREDIT (CREDIT_ID, PAYMENT_DATE, PM_ID, PAYMENT_NOMINAL, PAYMENT_DESCRIPTION, PAYMENT_CONFIRMED, PAYMENT_CONFIRMED_DATE, PAYMENT_DUE_DATE, PAYMENT_IS_DP) " +
                                                   "VALUES (" + creditID + ", STR_TO_DATE('" + paymentDate + "', '%d-%m-%Y %H:%i'), 1, " + paymentAmount + ", '" + paymentDesc + "', 1, STR_TO_DATE('" + paymentDate + "', '%d-%m-%Y %H:%i'), STR_TO_DATE('" + paymentDate + "', '%d-%m-%Y %H:%i'), " +isDPPayment+ ")";
                        else
                        {
                            tempID = gutil.getAutoGenerateID("PAYMENT_CREDIT", "", "", "PAYMENT_ID");

                            sqlCommand = "INSERT INTO PAYMENT_CREDIT (PAYMENT_ID, CREDIT_ID, PAYMENT_DATE, PM_ID, PAYMENT_NOMINAL, PAYMENT_DESCRIPTION, PAYMENT_CONFIRMED, PAYMENT_CONFIRMED_DATE, PAYMENT_DUE_DATE, PAYMENT_IS_DP) " +
                                                   "VALUES ('" + tempID + "', " + creditID + ", STR_TO_DATE('" + paymentDate + "', '%d-%m-%Y %H:%i'), 1, " + paymentAmount + ", '" + paymentDesc + "', 1, STR_TO_DATE('" + paymentDate + "', '%d-%m-%Y %H:%i'), STR_TO_DATE('" + paymentDate + "', '%d-%m-%Y %H:%i'), " + isDPPayment + ")";
                        }

                        if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                            throw internalEX;
                    }
                }

                DS.commit();
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



            //if (originModuleID == globalConstants.SALES_QUOTATION || originModuleID == globalConstants.EDIT_SALES_QUOTATION)
            //{
            //    sqlCommand = "SELECT IFNULL(SQ_DP, 0) AS DP FROM SALES_QUOTATION_HEADER WHERE SQ_INVOICE = '" + selectedsalesinvoice + "'";
            //    DPAmount = Convert.ToDouble(DS.getDataSingleValue(sqlCommand));
            //    invoiceNo = selectedsalesinvoice;
            //}
            //else if (originModuleID == globalConstants.SQ_TO_SO)
            //{
            //    DPAmount = previousSQ_DP;
            //    invoiceNo = selectedSQInvoice;
            //}

            //if (originModuleID == globalConstants.SALES_QUOTATION || originModuleID == globalConstants.EDIT_SALES_QUOTATION || originModuleID == globalConstants.SQ_TO_SO)
            //{ 
            //    sqlCommand = "SELECT COUNT(1) FROM DAILY_JOURNAL WHERE JOURNAL_DESCRIPTION = 'PEMBAYARAN DP [" + invoiceNo + "]' "; 

            //    if (Convert.ToInt32(DS.getDataSingleValue(sqlCommand)) > 0)
            //    {
            //        // UPDATE DAILY JOURNAL                                     
            //        sqlCommand = "UPDATE DAILY_JOURNAL SET " +
            //                                "ACCOUNT_ID = 1, " +
            //                                "JOURNAL_DATETIME = STR_TO_DATE('" + paymentDate + "', '%d-%m-%Y %H:%i'), " +
            //                                "JOURNAL_NOMINAL = " + gutil.validateDecimalNumericInput(DPAmount) + ", " +
            //                                "USER_ID = " + gutil.getUserID() + ", " +
            //                                "PM_ID = 1 " +
            //                                "WHERE JOURNAL_DESCRIPTION = 'PEMBAYARAN DP [" + invoiceNo + "]' ";

            //        if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
            //            throw internalEX;
            //    }
            //    else
            //    {
            //        // INSERT INTO DAILY JOURNAL                                     
            //        sqlCommand = "INSERT INTO DAILY_JOURNAL (ACCOUNT_ID, JOURNAL_DATETIME, JOURNAL_NOMINAL, JOURNAL_DESCRIPTION, USER_ID, PM_ID) " +
            //                                "VALUES (1, STR_TO_DATE('" + paymentDate + "', '%d-%m-%Y %H:%i')" + ", " + gutil.validateDecimalNumericInput(DPAmount) + ", 'PEMBAYARAN DP [" + invoiceNo + "]'  , '" + gutil.getUserID() + "', 1)";

            //        if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
            //            throw internalEX;
            //    }
            //}

            //if (originModuleID == globalConstants.SQ_TO_SO || originModuleID == 0)
            //{
            //    int paymentMethod = 1;

            //    // GET CREDIT ID
            //    sqlCommand = "SELECT COUNT(1) FROM CREDIT WHERE SALES_INVOICE = '" + selectedsalesinvoice + "'";
            //    creditID = Convert.ToInt32(DS.getDataSingleValue(sqlCommand));

            //    if (creditID > 0)
            //    {
            //        sqlCommand = "SELECT CREDIT_ID FROM CREDIT WHERE SALES_INVOICE = '" + selectedsalesinvoice + "'";
            //        creditID = Convert.ToInt32(DS.getDataSingleValue(sqlCommand));

            //        if (originModuleID == globalConstants.SQ_TO_SO)                      
            //        {
            //            // INSERT INTO PAYMENT CREDIT FOR DP
            //            sqlCommand = "INSERT INTO PAYMENT_CREDIT (CREDIT_ID, PAYMENT_DATE, PM_ID, PAYMENT_NOMINAL, PAYMENT_DESCRIPTION, PAYMENT_CONFIRMED, PAYMENT_CONFIRMED_DATE, PAYMENT_DUE_DATE, PAYMENT_IS_DP) " +
            //                                    "VALUES (" + creditID + ", STR_TO_DATE('" + paymentDate + "', '%d-%m-%Y %H:%i'), " + paymentMethod + ", " + DPAmount + ", 'DP " + invoiceNo + "', 1, STR_TO_DATE('" + paymentDate + "', '%d-%m-%Y %H:%i'), STR_TO_DATE('" + paymentDate + "', '%d-%m-%Y %H:%i'), 1)";

            //            if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
            //                throw internalEX;
            //        }

            //        //INSERT PAYMENT AMOUNT
            //        sqlCommand = "INSERT INTO PAYMENT_CREDIT (CREDIT_ID, PAYMENT_DATE, PM_ID, PAYMENT_NOMINAL, PAYMENT_DESCRIPTION, PAYMENT_CONFIRMED, PAYMENT_CONFIRMED_DATE, PAYMENT_DUE_DATE, PAYMENT_IS_DP) " +
            //                                "VALUES (" + creditID + ", STR_TO_DATE('" + paymentDate + "', '%d-%m-%Y %H:%i'), " + paymentMethod + ", " + (globalTotalValue - DPAmount - discValue) + ", 'PEMBAYARAN " + selectedsalesinvoice + "', 1, STR_TO_DATE('" + paymentDate + "', '%d-%m-%Y %H:%i'), STR_TO_DATE('" + paymentDate + "', '%d-%m-%Y %H:%i'), 0)";

            //        if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
            //            throw internalEX;
            //    }
            //}

            //SQCreatedDate = gutil.getCustomStringFormatDate(Convert.ToDateTime(DS.getDataSingleValue("SELECT SQ_DATE FROM SALES_QUOTATION_HEADER WHERE SQ_INVOICE = '" + selectedSQInvoice + "'")));

            //// GET CREDIT ID
            //sqlCommand = "SELECT COUNT(1) FROM CREDIT WHERE SALES_INVOICE = '" + selectedsalesinvoice + "'";
            //creditID = Convert.ToInt32(DS.getDataSingleValue(sqlCommand));

            ////if (creditID > 0)
            //{
            //    //sqlCommand = "SELECT CREDIT_ID FROM CREDIT WHERE SALES_INVOICE = '" + selectedsalesinvoice + "'";
            //    //creditID = Convert.ToInt32(DS.getDataSingleValue(sqlCommand));

            //    //// CHECK WHETHER INVOICE HAS BEEN FULLY PAID
            //    //sqlCommand = "SELECT CREDIT_PAID FROM CREDIT WHERE CREDIT_ID = " + creditID;
            //   // if (DS.getDataSingleValue(sqlCommand).ToString() == "0")
            //    {
            //        sqlCommand = "SELECT IFNULL(SQ_DP, 0) AS DP FROM SALES_QUOTATION_HEADER WHERE SQ_INVOICE = '" + selectedSQInvoice + "'";
            //        DPAmount = Convert.ToDouble(DS.getDataSingleValue(sqlCommand));

            //        if (DPAmount > 0)
            //        {
            //            int paymentMethod = 0;

            //            //if (cashRadioButton.Checked == true) // CASH PAYMENT
            //            //{
            //            //    paymentMethod = paymentComboBox.SelectedIndex + 1;
            //            //}
            //            //else
            //            //{
            //            //    paymentMethod = 4;
            //            //}

            //            sqlCommand = "SELECT IFNULL(PAYMENT_NOMINAL, -1) FROM PAYMENT_CREDIT WHERE CREDIT_ID = " + creditID + " AND PAYMENT_IS_DP = 1";

            //            prevDPAmount = Convert.ToDouble(DS.getDataSingleValue(sqlCommand));

            //            if (prevDPAmount > -1)
            //            {
            //                // UPDATE PAYMENT CREDIT
            //                sqlCommand = "UPDATE PAYMENT_CREDIT SET " +
            //                                       "PAYMENT_DATE = STR_TO_DATE('" + paymentDate + "', '%d-%m-%Y %H:%i'), " +
            //                                       "PM_ID = " + paymentMethod + ", " +
            //                                       "PAYMENT_NOMINAL = " + DPAmount + ", " +
            //                                       "PAYMENT_DESCRIPTION = 'DP " + selectedSQInvoice + "', " +
            //                                       "PAYMENT_CONFIRMED = 1, " +
            //                                       "PAYMENT_CONFIRMED_DATE = STR_TO_DATE('" + SQCreatedDate + "', '%d-%m-%Y %H:%i'), " +
            //                                       "PAYMENT_DUE_DATE = STR_TO_DATE('" + SQCreatedDate + "', '%d-%m-%Y %H:%i'), " +
            //                                       "PAYMENT_IS_DP = 1 " +
            //                                       "WHERE CREDIT_ID = " + creditID;

            //                if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
            //                    throw internalEX;

            //                // UPDATE DAILY JOURNAL                                     
            //                sqlCommand = "UPDATE DAILY_JOURNAL SET " +
            //                                        "ACCOUNT_ID = 1, " +
            //                                        "JOURNAL_DATETIME = STR_TO_DATE('" + paymentDate + "', '%d-%m-%Y %H:%i'), " +
            //                                        "JOURNAL_NOMINAL = " + gutil.validateDecimalNumericInput(DPAmount) + ", " +
            //                                        "USER_ID = " + gutil.getUserID() + ", " +
            //                                        "PM_ID = 1 " +
            //                                        "WHERE JOURNAL DESCRIPTION = 'PEMBAYARAN DP [" + selectedSQInvoice + "]' "; 

            //                if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
            //                    throw internalEX;
            //            }
            //            else
            //            {
            //                // INSERT INTO PAYMENT CREDIT
            //                sqlCommand = "INSERT INTO PAYMENT_CREDIT (CREDIT_ID, PAYMENT_DATE, PM_ID, PAYMENT_NOMINAL, PAYMENT_DESCRIPTION, PAYMENT_CONFIRMED, PAYMENT_CONFIRMED_DATE, PAYMENT_DUE_DATE, PAYMENT_IS_DP) " +
            //                                       "VALUES (" + creditID + ", STR_TO_DATE('" + paymentDate + "', '%d-%m-%Y %H:%i'), " + paymentMethod + ", " + DPAmount + ", 'DP " + selectedSQInvoice + "', 1, STR_TO_DATE('" + SQCreatedDate + "', '%d-%m-%Y %H:%i'), STR_TO_DATE('" + SQCreatedDate + "', '%d-%m-%Y %H:%i'), 1)";

            //                if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
            //                    throw internalEX;


            //                // INSERT INTO DAILY JOURNAL                                     
            //                sqlCommand = "INSERT INTO DAILY_JOURNAL (ACCOUNT_ID, JOURNAL_DATETIME, JOURNAL_NOMINAL, JOURNAL_DESCRIPTION, USER_ID, PM_ID) " +
            //                                        "VALUES (1, STR_TO_DATE('" + paymentDate + "', '%d-%m-%Y %H:%i')" + ", " + gutil.validateDecimalNumericInput(DPAmount) + ", 'PEMBAYARAN DP [" + selectedSQInvoice + "]'  , '" + gutil.getUserID() + "', 1)";

            //                if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
            //                    throw internalEX;
            //            }
            //        }
            //    }

            //}

            return result;
        }

        //private bool setQuotationToCompleted()
        //{
        //    bool result = false;
        //    string sqlCommand = "";
        //    MySqlException internalEX = null;

        //    DS.beginTransaction();

        //    try
        //    {
        //        // INSERT INTO PAYMENT CREDIT
        //        sqlCommand = "UPDATE SALES_QUOTATION_HEADER SET COMPLETED = 1 WHERE SQ_INVOICE = '" + selectedSQInvoice + "'";

        //        if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
        //            throw internalEX;

        //        DS.commit();
        //        result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }

        //    return result;
        //}

        private bool setQuotationToApproved(string selectedSO)
        {
            bool result = true;
            string sqInvoice = "";
            string sqlCommand = "";
            MySqlException internalEX = null;
            string SQApprovedDate = "";
            string SQCreatedDate = "";
            string paymentDate = String.Format(culture, "{0:dd-MM-yyyy}", DateTime.Now);
            // STORE SALES QUOTATION INVOICE NO
            sqInvoice = selectedsalesinvoice;

            SQApprovedDate = gutil.getCustomStringFormatDate(DateTime.Now); //String.Format(culture, "{0:dd-MM-yyyy HH:mm}", DateTime.Now);
            SQCreatedDate = gutil.getCustomStringFormatDate(Convert.ToDateTime(DS.getDataSingleValue("SELECT SQ_DATE FROM SALES_QUOTATION_HEADER WHERE SQ_INVOICE = '" + sqInvoice + "'")));

            DS.beginTransaction();

            try
            {
                DS.mySqlConnect();

                // UPDATE SALES QUOTATION TABLE
                sqlCommand = "UPDATE SALES_QUOTATION_HEADER SET SQ_APPROVED = 1, SQ_APPROVED_DATE = STR_TO_DATE('" + SQApprovedDate + "', '%d-%m-%Y %H:%i') WHERE SQ_INVOICE = '" + sqInvoice + "'";
                if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                    throw internalEX;

                DS.commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DS.mySqlClose();
            }

            return result;
        }

        private void sendDataPesananToPabrik()
        {
            globalSynchronizeLib gSync = new globalSynchronizeLib();

            if (gSync.sendDataToServer("SALES_HEADER", "SALES_INVOICE", selectedsalesinvoice))
                gSync.updateSyncFlag("SALES_HEADER", "SALES_INVOICE", selectedsalesinvoice);

            if (gSync.sendDataToServer("SALES_DETAIL", "SALES_INVOICE", selectedsalesinvoice))
                gSync.updateSyncFlag("SALES_DETAIL", "SALES_INVOICE", selectedsalesinvoice);

            if (gSync.sendDataToServer("SALES_DETAIL_FULFILLMENT", "SALES_INVOICE", selectedsalesinvoice))
                gSync.updateSyncFlag("SALES_DETAIL_FULFILLMENT", "SALES_INVOICE", selectedsalesinvoice);

            if (gSync.sendDataToServer("NOTIF_TABLE"))
                gSync.updateSyncFlag("NOTIF_TABLE");
        }

        private bool saveAndPrintOutInvoice()
        {
            string message = "";
            bool result = false;

            if (printoutCheckBox.Checked == false)
                message = "SAVE AND PRINT OUT ?";
            else
                message = "SAVE DATA ?";

            if (DialogResult.Yes == MessageBox.Show(message, "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "ATTEMPT TO SAVE AND PRINT OUT INVOICE");

                totalPenjualanTextBox.Focus();
                if (saveData())
                {
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "TRANSACTION SAVED");

                    if (originModuleID != globalConstants.EDIT_SALES_ORDER)
                    {
                        if (!saveDPToPaymentCredit())
                        {
                            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "SAVE DP PAYMENT TO PAYMENT CREDIT FAILED");
                        }
                    }

                    if ((originModuleID == globalConstants.SO_FULFILLMENT || SOisPesanan) && gutil.isServerApp() == 0)
                        sendDataPesananToPabrik();

                    //if (originModuleID == globalConstants.SQ_TO_SO)
                    //{
                    //    if (DialogResult.Yes == MessageBox.Show("PESANAN SUDAH LENGKAP SEMUA?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    //    {
                    //        if (!setQuotationToCompleted())
                    //            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "SQ [" + selectedSQInvoice + "] FAILED TO SET TO COMPLETE");
                    //        else
                    //            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "SQ [" + selectedSQInvoice + "] SET TO COMPLETE");
                    //    }

                    //    if (!saveDPToPaymentCredit())
                    //    {
                    //        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "SAVE DP PAYMENT TO PAYMENT CREDIT FAILED");
                    //    }
                    //}
                    //else if (originModuleID == globalConstants.SALES_QUOTATION || originModuleID == globalConstants.EDIT_SALES_QUOTATION)
                    //{
                    //    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "SAVE DP PAYMENT TO PAYMENT CREDIT");
                    //    if (!saveDPToPaymentCredit())
                    //    {
                    //        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "SAVE DP PAYMENT TO PAYMENT CREDIT FAILED");
                    //    }
                    //}
                    //else if (originModuleID == 0)
                    //{
                    //    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "SAVE PAYMENT TO PAYMENT CREDIT");
                    //    if (!saveDPToPaymentCredit())
                    //    {
                    //        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "SAVE PAYMENT TO PAYMENT CREDIT FAILED");
                    //    }
                    //}

                    gutil.saveUserChangeLog(globalConstants.MENU_PENJUALAN, globalConstants.CHANGE_LOG_INSERT, "NEW TRANSAKSI PENJUALAN [" + selectedsalesinvoice + "]");

                    if (printoutCheckBox.Checked == false)
                    {
                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "PRINT OUT INVOICE");
                        PrintReceipt();
                    }

                    gutil.showSuccess(gutil.INS);

                    if (originModuleID == 0 || originModuleID == globalConstants.SO_FULFILLMENT)
                        clearUpScreen();

                    result = true;
                }
            }
            else
                result = false;

            return result;
        }

        private string getNoFaktur()
        {
            string rsult = "";

            rsult = DS.getDataSingleValue("SELECT IFNULL(NO_FAKTUR, '') FROM SYS_CONFIG WHERE ID = 1").ToString();

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : NO FAKTUR [" + rsult + "]");

            return rsult;
        }

        private string getSalesInvoiceID(bool mainSalesTable = true)
        {
            string salesInvoice = "";
            DateTime localDate = DateTime.Now;
            string maxSalesInvoice = "";
            double maxSalesInvoiceValue = 0;
            string salesInvPrefix;
            string sqlCommand = "";

            salesInvPrefix = getNoFaktur() + "-";//String.Format(culture, "{0:yyyyMMdd}", localDate);

            if (mainSalesTable == true)
                sqlCommand = "SELECT IFNULL(MAX(CONVERT(SUBSTRING(SALES_INVOICE, INSTR(SALES_INVOICE,'-')+1), UNSIGNED INTEGER)),'0') AS SALES_INVOICE FROM SALES_HEADER WHERE SALES_INVOICE LIKE '" + salesInvPrefix + "%'";
            else
                sqlCommand = "SELECT IFNULL(MAX(CONVERT(SUBSTRING(SALES_INVOICE, INSTR(SALES_INVOICE,'-')+1), UNSIGNED INTEGER)),'0') AS SALES_INVOICE FROM SALES_HEADER_TAX WHERE SALES_INVOICE LIKE '" + salesInvPrefix + "%'";

            maxSalesInvoice = DS.getDataSingleValue(sqlCommand).ToString();
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : MAX SALES INVOICE [" + maxSalesInvoice + "]");

            maxSalesInvoiceValue = Convert.ToInt32(maxSalesInvoice);
            if (maxSalesInvoiceValue > 0)
            {
                maxSalesInvoiceValue += 1;
                maxSalesInvoice = maxSalesInvoiceValue.ToString();
            }
            else
            {
                maxSalesInvoice = "1";
            }

            salesInvoice = salesInvPrefix + maxSalesInvoice;
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : SALES INVOICE [" + salesInvoice + "]");

            return salesInvoice;
        }

        private double getProductPriceValue(string productID, int customerType = 0, bool hppValue = false)
        {
            double result = 0;
            string priceType = "";

            //DS.mySqlConnect();

            if (customerType == 0)
                priceType = "PRODUCT_RETAIL_PRICE";
            else if (customerType == 1)
                priceType = "PRODUCT_BULK_PRICE";
            else
                priceType = "PRODUCT_WHOLESALE_PRICE";

            if (hppValue)
                priceType = "PRODUCT_BASE_PRICE";

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : PRODUCT PRICE TYPE [" + priceType + "]");

            result = Convert.ToDouble(DS.getDataSingleValue("SELECT IFNULL(" + priceType + ", 0) FROM MASTER_PRODUCT WHERE PRODUCT_ID = '" + productID + "'"));

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : PRODUCT PRICE [" + result + "]");

            return result;
        }

        private double calculateSubTotal(int rowSelectedIndex, double productPrice)
        {
            double subTotal = 0;
            double productQty = 0;
            double hppValue = 0;
            double disc1Value = 0;
            double disc2Value = 0;
            double discRPValue = 0;

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : CALCULATE SUBTOTAL");

            try
            {
                productQty = Convert.ToDouble(salesQty[rowSelectedIndex]);

                if (productQty > 0)
                {
                    hppValue = productPrice;

                    disc1Value = Convert.ToDouble(disc1[rowSelectedIndex]);
                    disc2Value = Convert.ToDouble(disc2[rowSelectedIndex]);
                    discRPValue = Convert.ToDouble(discRP[rowSelectedIndex]);

                    subTotal = Math.Round((hppValue * productQty), 2);

                    if (disc1Value > 0)
                        subTotal = Math.Round(subTotal - (subTotal * disc1Value / 100), 2);

                    if (disc2Value > 0)
                        subTotal = Math.Round(subTotal - (subTotal * disc2Value / 100), 2);

                    if (discRPValue > 0)
                        subTotal = Math.Round(subTotal - discRPValue, 2);
                }

            }
            catch (Exception ex)
            {
                subTotal = 0;
            }

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : SUBTOTAL [" + subTotal + "]");
            return subTotal;
        }

        private void calculateTotal()
        {
            double total = 0;
            double discJual = 0;
            double totalAfterDisc = 0;

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : calculateTotal");
            for (int i = 0; i < cashierDataGridView.Rows.Count; i++)
            {
                if (null != cashierDataGridView.Rows[i].Cells["jumlah"].Value)
                    total = total + Convert.ToDouble(jumlahList[i]);
                //total = total + Convert.ToDouble(cashierDataGridView.Rows[i].Cells["jumlah"].Value);
            }

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : calculateTotal [" + total + "]");

            globalTotalValue = total;
            totalAfterDisc = total;
            totalLabel.Text = total.ToString("c0", culture);

            totalPenjualanTextBox.Text = total.ToString("c0", culture);

            if (discJualMaskedTextBox.Text.Length > 0)
            {
                discJual = discValue;// Convert.ToDouble(discJualMaskedTextBox.Text);
                totalAfterDisc = Math.Round(totalAfterDisc - discJual, 2);
            }
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : calculateTotal, totalAfterDisc [" + totalAfterDisc + "]");

            totalAfterDiscTextBox.Text = totalAfterDisc.ToString("c0", culture);

            calculateChangeValue();
        }

        private void calculateChangeValue()
        {
            double totalAfterDisc = 0;
            if (bayarTextBox.Text.Length > 0)
            {
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : calculateChangeValue, bayarTextBox.Text [" + bayarTextBox.Text + "]");
                //bayarAmount = Convert.ToDouble(bayarTextBox.Text);
                totalAfterDisc = globalTotalValue - discValue;
                if (bayarAmount > totalAfterDisc)
                    sisaBayar = bayarAmount - totalAfterDisc;
                else
                    sisaBayar = 0;

                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : calculateChangeValue, sisaBayar [" + sisaBayar + "]");
                uangKembaliTextBox.Text = sisaBayar.ToString("C0", culture);
            }
        }

        private bool stockIsEnough(string productID, double qtyRequested)
        {
            bool result = false;

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : CHECK STOCK QTY IS ENOUGH [" + productID + "]");

            if (SOisPesanan) // PESANAN TIDAK PERLU CEK STOCK
                return true;

            if (productID.Length <= 0)
                result = true; // NO PRODUCT SELECTED YET
            else
            {
                if (gutil.productIsService(productID))
                    result = true;
                else
                {
                    double stockQty = 0;
                    double limitQty = 0;

                    stockQty = Convert.ToDouble(DS.getDataSingleValue("SELECT PRODUCT_STOCK_QTY FROM MASTER_PRODUCT WHERE PRODUCT_ID = '" + productID + "'"));
                    limitQty = Convert.ToDouble(DS.getDataSingleValue("SELECT PRODUCT_LIMIT_STOCK FROM MASTER_PRODUCT WHERE PRODUCT_ID = '" + productID + "'"));

                    if (stockQty >= qtyRequested)
                        result = true;

                    if (stockQty - qtyRequested <= limitQty)
                    {
                        errorLabel.Text = productID + " MENCAPAI LIMIT";
                    }
                    else
                    {
                        errorLabel.Text = "";
                    }
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : CHECK STOCK QTY IS ENOUGH [" + stockQty + ", " + qtyRequested + "]");
                }
            }

            return result;
        }

        private void cashierDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // +=
            if ((cashierDataGridView.CurrentCell.OwningColumn.Name == "productID")
                && e.Control is TextBox)
            {
                TextBox productIDTextBox = e.Control as TextBox;
                productIDTextBox.CharacterCasing = CharacterCasing.Upper;
                productIDTextBox.PreviewKeyDown -= Combobox_previewKeyDown;
                productIDTextBox.PreviewKeyDown += Combobox_previewKeyDown;
                productIDTextBox.KeyUp += Combobox_KeyUp;
                productIDTextBox.AutoCompleteMode = AutoCompleteMode.None;
            }

            if ((cashierDataGridView.CurrentCell.OwningColumn.Name == "productName")
                && e.Control is TextBox)
            {
                TextBox productNameTextBox = e.Control as TextBox;
                productNameTextBox.CharacterCasing = CharacterCasing.Upper;
                productNameTextBox.PreviewKeyDown -= productName_previewKeyDown;
                productNameTextBox.PreviewKeyDown += productName_previewKeyDown;
                productNameTextBox.AutoCompleteMode = AutoCompleteMode.None;//SuggestAppend;

                productNameTextBox.KeyUp += Combobox_KeyUp;
            }
        }

        private void clearUpSomeRowContents(DataGridViewRow selectedRow, int rowSelectedIndex)
        {
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : clearUpSomeRowContents");

            selectedRow.Cells["productName"].Value = "";
            selectedRow.Cells["hpp"].Value = "0";
            selectedRow.Cells["productPrice"].Value = "0";
            productPriceList[rowSelectedIndex] = "0";
            selectedRow.Cells["qty"].Value = "0";
            salesQty[rowSelectedIndex] = "0";

            selectedRow.Cells["disc1"].Value = "0";
            disc1[rowSelectedIndex] = "0";

            selectedRow.Cells["disc2"].Value = "0";
            disc2[rowSelectedIndex] = "0";

            selectedRow.Cells["discRP"].Value = "0";
            discRP[rowSelectedIndex] = "0";

            selectedRow.Cells["jumlah"].Value = "0";
            jumlahList[rowSelectedIndex] = "0";

            calculateTotal();
        }

        private void updateSomeRowContents(DataGridViewRow selectedRow, int rowSelectedIndex, string currentValue, bool isProductID = true)
        {

            int numRow = 0;
            string selectedProductID = "";
            string selectedProductName = "";

            double hpp = 0;
            MySqlDataReader rdr;
            string currentProductID = "";
            string currentProductName = "";
            bool changed = false;

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : updateSomeRowContents");

            if (isProductID)
                numRow = Convert.ToInt32(DS.getDataSingleValue("SELECT COUNT(1) FROM MASTER_PRODUCT WHERE PRODUCT_ID = '" + currentValue + "'"));
            else
                numRow = Convert.ToInt32(DS.getDataSingleValue("SELECT COUNT(1) FROM MASTER_PRODUCT WHERE PRODUCT_NAME = '" + MySqlHelper.EscapeString(currentValue) + "'"));

            if (numRow > 0)
            {
                if (isProductID)
                {
                    selectedProductID = currentValue;
                    selectedProductName = DS.getDataSingleValue("SELECT IFNULL(PRODUCT_NAME,'') FROM MASTER_PRODUCT WHERE PRODUCT_ID = '" + MySqlHelper.EscapeString(currentValue) + "'").ToString();
                }
                else
                {
                    selectedProductName = currentValue;
                    selectedProductID = DS.getDataSingleValue("SELECT IFNULL(PRODUCT_ID,'') FROM MASTER_PRODUCT WHERE PRODUCT_NAME = '" + MySqlHelper.EscapeString(currentValue) + "'").ToString();
                }

                if (null != selectedRow.Cells["productID"].Value)
                    currentProductID = selectedRow.Cells["productID"].Value.ToString();

                if (null != selectedRow.Cells["productName"].Value)
                    currentProductName = selectedRow.Cells["productName"].Value.ToString();


                selectedRow.Cells["productId"].Value = selectedProductID;
                selectedRow.Cells["productName"].Value = selectedProductName;

                if (selectedProductID != currentProductID)
                    changed = true;

                if (selectedProductName != currentProductName)
                    changed = true;

                if (!changed)
                    return;

                hpp = getProductPriceValue(selectedProductID, customerComboBox.SelectedIndex, true);
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ComboBox_SelectedIndexChanged, PRODUCT_BASE_PRICE [" + hpp + "]");
                selectedRow.Cells["hpp"].Value = hpp;

                hpp = getProductPriceValue(selectedProductID, customerComboBox.SelectedIndex);
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ComboBox_SelectedIndexChanged, PRODUCT_SALES_PRICE [" + hpp + "]");
                selectedRow.Cells["productPrice"].Value = hpp;
                productPriceList[rowSelectedIndex] = hpp.ToString();

                selectedRow.Cells["qty"].Value = 0;
                salesQty[rowSelectedIndex] = "0";

                if (selectedPelangganID != 0)
                {
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ComboBox_SelectedIndexChanged, CHECK FOR CUST PRODUCT DISC, selectedPelangganID [" + selectedPelangganID + "]");
                    if (Convert.ToInt32(DS.getDataSingleValue("SELECT COUNT(1) FROM CUSTOMER_PRODUCT_DISC WHERE CUSTOMER_ID = " + selectedPelangganID + " AND PRODUCT_ID = '" + selectedProductID + "'")) > 0)
                    {
                        // DATA EXIST, LOAD DISC VALUE
                        using (rdr = DS.getData("SELECT * FROM CUSTOMER_PRODUCT_DISC WHERE CUSTOMER_ID = " + selectedPelangganID + " AND PRODUCT_ID = '" + selectedProductID + "'"))
                        {
                            if (rdr.HasRows)
                            {
                                rdr.Read();

                                selectedRow.Cells["disc1"].Value = rdr.GetString("DISC_1");
                                disc1[rowSelectedIndex] = rdr.GetString("DISC_1");

                                selectedRow.Cells["disc2"].Value = rdr.GetString("DISC_2");
                                disc2[rowSelectedIndex] = rdr.GetString("DISC_2");

                                selectedRow.Cells["discRP"].Value = rdr.GetString("DISC_RP");
                                discRP[rowSelectedIndex] = rdr.GetString("DISC_RP");

                                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ComboBox_SelectedIndexChanged, LOAD DISC [" + rdr.GetString("DISC_1") + ", " + rdr.GetString("DISC_2") + ", " + rdr.GetString("DISC_RP") + "]");
                            }

                            rdr.Close();
                        }
                    }
                    else
                    {
                        selectedRow.Cells["disc1"].Value = 0;
                        disc1[rowSelectedIndex] = "0";

                        selectedRow.Cells["disc2"].Value = 0;
                        disc2[rowSelectedIndex] = "0";

                        selectedRow.Cells["discRP"].Value = 0;
                        discRP[rowSelectedIndex] = "0";
                    }
                }

                selectedRow.Cells["jumlah"].Value = "0";
                jumlahList[rowSelectedIndex] = "0";
                calculateTotal();
            }
            else
            {
                clearUpSomeRowContents(selectedRow, rowSelectedIndex);
            }
        }

        private void Combobox_KeyUp(object sender, KeyEventArgs e)
        {
            //gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : Combobox_KeyUp, cashierDataGridView.CurrentCell.OwningColumn.Name  [" + cashierDataGridView.CurrentCell.OwningColumn.Name + "]");

            //if (forceUpOneLevel && e.KeyCode == Keys.Enter)
            //{
            //    int pos = cashierDataGridView.CurrentCell.RowIndex;

            //    if (pos > 0)
            //        cashierDataGridView.CurrentCell = cashierDataGridView.Rows[pos - 1].Cells["qty"];

            //    forceUpOneLevel = false;
            //}
        }

        private void Combobox_previewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            string currentValue = "";
            int rowSelectedIndex = 0;
            DataGridViewTextBoxEditingControl dataGridViewComboBoxEditingControl = sender as DataGridViewTextBoxEditingControl;

            if (cashierDataGridView.CurrentCell.OwningColumn.Name != "productID")
                return;

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : Combobox_previewKeyDown, cashierDataGridView.CurrentCell.OwningColumn.Name  [" + cashierDataGridView.CurrentCell.OwningColumn.Name + "]");

            if (e.KeyCode == Keys.Enter)
            {
                currentValue = dataGridViewComboBoxEditingControl.Text;
                rowSelectedIndex = cashierDataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = cashierDataGridView.Rows[rowSelectedIndex];

                if (currentValue.Length > 0)
                {
                    forceUpOneLevel = true;

                    // CALL DATA PRODUK FORM WITH PARAMETER 
                    dataProdukForm browseProduk = new dataProdukForm(globalConstants.CASHIER_MODULE, this, currentValue, "", rowSelectedIndex);
                    browseProduk.ShowDialog(this);
                    //updateSomeRowContents(selectedRow, rowSelectedIndex, currentValue);
                    //cashierDataGridView.CurrentCell = selectedRow.Cells["qty"];

                    //Force_KeyUp(rowSelectedIndex);
                }
                else
                {
                    //clearUpSomeRowContents(selectedRow, rowSelectedIndex);
                }
            }
        }

        private void productName_previewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            string currentValue = "";
            int rowSelectedIndex = 0;
            DataGridViewTextBoxEditingControl dataGridViewComboBoxEditingControl = sender as DataGridViewTextBoxEditingControl;

            if (cashierDataGridView.CurrentCell.OwningColumn.Name != "productName")
                return;

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : productName_previewKeyDown, cashierDataGridView.CurrentCell.OwningColumn.Name  [" + cashierDataGridView.CurrentCell.OwningColumn.Name + "]");

            if (e.KeyCode == Keys.Enter)
            {
                currentValue = dataGridViewComboBoxEditingControl.Text;
                rowSelectedIndex = cashierDataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = cashierDataGridView.Rows[rowSelectedIndex];

                if (currentValue.Length > 0)
                {
                    forceUpOneLevel = true;

                    // CALL DATA PRODUK FORM WITH PARAMETER 
                    dataProdukForm browseProduk = new dataProdukForm(globalConstants.CASHIER_MODULE, this, "", currentValue, rowSelectedIndex);
                    browseProduk.ShowDialog(this);
                    //updateSomeRowContents(selectedRow, rowSelectedIndex, currentValue, false);
                    //cashierDataGridView.CurrentCell = selectedRow.Cells["qty"];

                    //Force_KeyUp(rowSelectedIndex);
                }
                else
                {
//                    clearUpSomeRowContents(selectedRow, rowSelectedIndex);
                }
            }
        }

        private void cashierForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            unregisterGlobalHotkey();
        }

        private void creditRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (creditRadioButton.Checked == true)
            {
                labelBayar.Text = "D P [+]";
                jobStartDateTimePicker.Visible = true;
                //labelJamKirim.Visible = true;
                labelTglKirim.Visible = true;
                //timeStartDateTimePicker.Visible = true;
                SOisPesanan = true;
                deliveredCheckbox.Visible = true;

                if (deliveredCheckbox.Checked)
                    addressTextBox.Visible = true;
                //paymentComboBox.Visible = false;
                //tempoMaskedTextBox.Visible = true;
                //bayarTextBox.Enabled = false;
                //labelCaraBayar.Text = "Tempo            :";
                //labelCaraBayar.Visible = true;
            }
        }

        private void loadNoFaktur()
        {
            string noFakturValue;

            noFakturValue = DS.getDataSingleValue("SELECT NO_FAKTUR FROM SYS_CONFIG").ToString();
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : loadNoFaktur, noFakturValue [" + noFakturValue + "]");
            noFakturLabel.Text = noFakturValue;
        }

        private void loadInvoiceData()
        {
            string sqlCommand;
            int TOPValue;
            int TOPDuration;
            MySqlDataReader rdr;
            int rowPos = 0;
            double DPAmount = 0;

            //salesQty.Clear();
            isLoading = true;

            switch (originModuleID)
            {
                //case globalConstants.EDIT_SALES_QUOTATION:
                //case globalConstants.SQ_TO_SO:
                //case globalConstants.COPY_NOTA_SQ:
                //    selectedSQInvoice = selectedsalesinvoice;
                //    // PULL DETAIL DATA               
                //    sqlCommand = "SELECT M.PRODUCT_ID AS KODE_PRODUK, M.PRODUCT_NAME AS NAMA_PRODUK, SD.PRODUCT_SALES_PRICE AS HARGA_PRODUK, SD.PRODUCT_QTY AS QTY, SD.SQ_SUBTOTAL AS SUBTOTAL " +
                //                           "FROM SALES_QUOTATION_DETAIL SD, MASTER_PRODUCT M WHERE SD.SQ_INVOICE = '" + selectedsalesinvoice + "' AND SD.PRODUCT_ID = M.PRODUCT_ID";
                //    rdr = DS.getData(sqlCommand);
                //    if (rdr.HasRows)
                //    {
                //        while (rdr.Read())
                //        {
                //            addNewRow();
                //            cashierDataGridView.Rows[rowPos].Cells["productID"].Value = rdr.GetString("KODE_PRODUK");
                //            cashierDataGridView.Rows[rowPos].Cells["productName"].Value = rdr.GetString("NAMA_PRODUK");
                //            cashierDataGridView.Rows[rowPos].Cells["productPrice"].Value = rdr.GetString("HARGA_PRODUK");
                //            cashierDataGridView.Rows[rowPos].Cells["qty"].Value = rdr.GetString("QTY");
                //            salesQty[rowPos] = rdr.GetString("QTY");
                //            productPriceList[rowPos] = rdr.GetString("HARGA_PRODUK");
                //            cashierDataGridView.Rows[rowPos].Cells["jumlah"].Value = rdr.GetString("SUBTOTAL");
                //            jumlahList[rowPos] = rdr.GetString("SUBTOTAL");

                //            rowPos += 1;
                //        }
                //    }
                //    rdr.Close();

                //    // PULL HEADER DATA
                //    sqlCommand = "SELECT SH.SQ_DATE, SH.SQ_ORDER_DATE, SH.SQ_DP, SH.SQ_ORDER_ADDRESS, SH.DELIVERED, SH.SQ_INVOICE AS NO_INVOICE, IFNULL(M.CUSTOMER_ID, 0) AS PELANGGAN_ID, IFNULL(M.CUSTOMER_FULL_NAME, '') AS NAMA, IFNULL(M.CUSTOMER_GROUP, 1) AS CUSTOMER_GROUP, SH.SQ_TOTAL AS TOTAL, SH.SALES_DISCOUNT_FINAL AS DISC_FINAL, SH.SQ_TOP AS TOP, DATEDIFF(SH.SQ_TOP_DATE, SH.SQ_DATE) AS TOP_DURATION " +
                //                           "FROM SALES_QUOTATION_HEADER SH LEFT OUTER JOIN MASTER_CUSTOMER M ON (SH.CUSTOMER_ID = M.CUSTOMER_ID) WHERE SH.SQ_INVOICE = '" + selectedsalesinvoice + "'";
                //    rdr = DS.getData(sqlCommand);
                //    if (rdr.HasRows)
                //    {
                //        while (rdr.Read())
                //        {
                //            pelangganTextBox.Text = rdr.GetString("NAMA");

                //            if (originModuleID == globalConstants.EDIT_SALES_QUOTATION)
                //                noFakturLabel.Text = noFakturLabel.Text + " " + rdr.GetString("NO_INVOICE");

                //            customerComboBox.SelectedIndex = rdr.GetInt32("CUSTOMER_GROUP") - 1;
                //            customerComboBox.Text = customerComboBox.Items[customerComboBox.SelectedIndex].ToString();
                //            selectedPelangganID = rdr.GetInt32("PELANGGAN_ID");

                //            discValue = rdr.GetDouble("DISC_FINAL");
                //            discJualMaskedTextBox.Text = discValue.ToString();

                //            SQDPAmount = rdr.GetDouble("SQ_DP");
                //            previousSQ_DP = rdr.GetDouble("SQ_DP");

                //            if (originModuleID == globalConstants.SQ_TO_SO)
                //                DPTextBox.Text = previousSQ_DP.ToString("C2", culture);
                //            else
                //            {
                //                bayarTextBox.Text = previousSQ_DP.ToString("C2", culture);
                //                bayarAmount = previousSQ_DP;
                //            }
                //            globalTotalValue = rdr.GetDouble("TOTAL");
                //            totalPenjualanTextBox.Text = (globalTotalValue).ToString("C2", culture);

                //            calculateChangeValue();

                //            if (originModuleID == globalConstants.SQ_TO_SO)
                //                bayarAmount = 0;
                //            //int tempVal = 0;

                //            //if (previousSQ_DP <= (globalTotalValue - discValue))
                //            //{ 
                //            //    //totalLabel.Text = (globalTotalValue - discValue - bayarAmount).ToString("C2", culture);
                //            //    //globalTotalValue = globalTotalValue - bayarAmount;
                //            //    bayarAmount = 0;
                //            //    totalPenjualanTextBox.Text = globalTotalValue.ToString("C2", culture);
                //            //}
                //            //else
                //            //{ 
                //            //    totalLabel.Text = tempVal.ToString("C2", culture);
                //            //    totalPenjualanTextBox.Text = tempVal.ToString("C2", culture);
                //            //    globalTotalValue = 0;
                //            //}

                //            TOPValue = rdr.GetInt32("TOP");
                //            if (TOPValue == 1)
                //                cashRadioButton.Checked = true;
                //            else
                //            {
                //                creditRadioButton.Checked = true;
                //                TOPDuration = rdr.GetInt32("TOP_DURATION");
                //                tempoMaskedTextBox.Text = TOPDuration.ToString();
                //            }
                //            totalAfterDiscTextBox.Text = (globalTotalValue - discValue).ToString("C2", culture);
                //            totalLabel.Text = (globalTotalValue - previousSQ_DP - discValue).ToString("C2", culture);

                //            addressTextBox.Text = rdr.GetString("SQ_ORDER_ADDRESS").ToString();

                //            if (1 == Convert.ToInt32(rdr.GetInt32("DELIVERED")))
                //                deliveredCheckbox.Checked = true;
                //            else
                //                deliveredCheckbox.Checked = false;

                //            jobStartDateTimePicker.Value = rdr.GetDateTime("SQ_ORDER_DATE");
                //            SODateTimePicker.Value = rdr.GetDateTime("SQ_DATE");
                //        }
                //    }
                //    rdr.Close();

                //    break;

                case globalConstants.EDIT_SALES_ORDER:
                case globalConstants.SO_FULFILLMENT:
                    // PULL HEADER DATA
                    sqlCommand = "SELECT SH.SALES_ORDER_DELIVERED, SH.SALES_ORDER_DELIVERED_ADDRESS, SH.SALES_DATE, SH.SALES_INVOICE AS NO_INVOICE, IFNULL(M.CUSTOMER_ID, 0) AS PELANGGAN_ID, IFNULL(M.CUSTOMER_FULL_NAME, '') AS NAMA, IFNULL(M.CUSTOMER_GROUP, 1) AS CUSTOMER_GROUP, SH.SALES_TOTAL AS TOTAL, SH.SALES_DISCOUNT_FINAL AS DISC_FINAL, SH.SALES_TOP AS TOP, SALES_ORDER_DATE " +
                                           "FROM SALES_HEADER SH, MASTER_CUSTOMER M WHERE SH.CUSTOMER_ID = M.CUSTOMER_ID AND SH.SALES_INVOICE = '" + selectedsalesinvoice + "'";

                    rdr = DS.getData(sqlCommand);
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            pelangganTextBox.Text = rdr.GetString("NAMA");

                            if (originModuleID == globalConstants.EDIT_SALES_ORDER)
                                noFakturLabel.Text = selectedsalesinvoice;

                            if (originModuleID == globalConstants.SO_FULFILLMENT)
                            {
                                refSOTextBox.Text = selectedsalesinvoice;
                                referenceSO = selectedsalesinvoice;
                            }

                            customerComboBox.SelectedIndex = rdr.GetInt32("CUSTOMER_GROUP") - 1;
                            customerComboBox.Text = customerComboBox.Items[customerComboBox.SelectedIndex].ToString();
                            selectedPelangganID = rdr.GetInt32("PELANGGAN_ID");

                            globalTotalValue = rdr.GetDouble("TOTAL");
                            totalPenjualanTextBox.Text = globalTotalValue.ToString("C0", culture);
                            discValue = rdr.GetDouble("DISC_FINAL");
                            discJualMaskedTextBox.Text = discValue.ToString();
                            totalLabel.Text = (globalTotalValue - discValue).ToString("C0", culture);

                            if (rdr.GetInt32("SALES_ORDER_DELIVERED") == 1)
                            {
                                deliveredCheckbox.Checked = true;
                                addressTextBox.Text = rdr.GetString("SALES_ORDER_DELIVERED_ADDRESS");
                            }
                            //TOPValue = rdr.GetInt32("TOP");

                            //if (TOPValue == 1)
                            //{
                            //}
                            //else
                            //{
                            //    creditRadioButton.Checked = true;
                            //    //TOPDuration = rdr.GetInt32("TOP_DURATION");
                            //    //tempoMaskedTextBox.Text = TOPDuration.ToString();
                            //    bayarAmount = 0;
                            //    bayarTextBox.Text = bayarAmount.ToString("C0", culture);
                            //}

                            totalAfterDiscTextBox.Text = (globalTotalValue - discValue).ToString("C0", culture);
                        }
                        SODateTimePicker.Value = rdr.GetDateTime("SALES_DATE");
                        jobStartDateTimePicker.Value = rdr.GetDateTime("SALES_ORDER_DATE");
                    }
                    rdr.Close();

                    // GET DP AMOUNT
                    sqlCommand = "SELECT PAYMENT_NOMINAL FROM PAYMENT_CREDIT PC, CREDIT WHERE CREDIT.SALES_INVOICE = '" + selectedsalesinvoice + "' AND PC.CREDIT_ID = CREDIT.CREDIT_ID AND PC.PAYMENT_IS_DP = 1";
                    DPAmount = Convert.ToDouble(DS.getDataSingleValue(sqlCommand));
                    bayarAmount = DPAmount;
                    bayarTextBox.Text = bayarAmount.ToString("C0", culture);

                    // PULL DETAIL DATA               
                    if (originModuleID == globalConstants.EDIT_SALES_ORDER)
                        sqlCommand = "SELECT M.PRODUCT_ID AS KODE_PRODUK, M.PRODUCT_NAME AS NAMA_PRODUK, SD.PRODUCT_PRICE AS HPP, SD.PRODUCT_SALES_PRICE AS HARGA_PRODUK, SD.PRODUCT_QTY AS QTY, SD.PRODUCT_DISC1, SD.PRODUCT_DISC2, SD.PRODUCT_DISC_RP, SD.SALES_SUBTOTAL AS SUBTOTAL " +
                                "FROM SALES_DETAIL SD, MASTER_PRODUCT M WHERE SD.SALES_INVOICE = '" + selectedsalesinvoice + "' AND SD.PRODUCT_ID = M.PRODUCT_ID";
                    else if (originModuleID == globalConstants.SO_FULFILLMENT)
                    {
                        sqlCommand = "SELECT M.PRODUCT_ID AS KODE_PRODUK, M.PRODUCT_NAME AS NAMA_PRODUK, SD.PRODUCT_PRICE AS HPP, SD.PRODUCT_SALES_PRICE AS HARGA_PRODUK, (SD.PRODUCT_QTY- IFNULL(SDF.QTY, 0)) AS QTY, SD.PRODUCT_DISC1, SD.PRODUCT_DISC2, SD.PRODUCT_DISC_RP, SD.SALES_SUBTOTAL AS SUBTOTAL " +
                                "FROM SALES_DETAIL SD LEFT OUTER JOIN (SELECT SD.PRODUCT_ID, IFNULL(SUM(SD.PRODUCT_QTY), 0) AS QTY FROM SALES_DETAIL_FULFILLMENT SD, SALES_HEADER SH WHERE SD.SALES_INVOICE = SH.SALES_INVOICE AND SH.REFERENCE_SO = '" + selectedsalesinvoice + "' GROUP BY PRODUCT_ID) SDF ON SDF.PRODUCT_ID = SD.PRODUCT_ID, " + 
                                "MASTER_PRODUCT M WHERE SD.SALES_INVOICE = '" + selectedsalesinvoice + "' AND SD.PRODUCT_ID = M.PRODUCT_ID";
                    }


                    rdr = DS.getData(sqlCommand);
                    if (rdr.HasRows)
                    {
                        rowPos = 0;
                        while (rdr.Read())
                        {
                            addNewRow();

                            cashierDataGridView.Rows[rowPos].Cells["productID"].Value = rdr.GetString("KODE_PRODUK");
                            cashierDataGridView.Rows[rowPos].Cells["productName"].Value = rdr.GetString("NAMA_PRODUK");
                            cashierDataGridView.Rows[rowPos].Cells["productPrice"].Value = rdr.GetString("HARGA_PRODUK");
                            cashierDataGridView.Rows[rowPos].Cells["qty"].Value = rdr.GetString("QTY");
                            cashierDataGridView.Rows[rowPos].Cells["disc1"].Value = rdr.GetString("PRODUCT_DISC1");
                            cashierDataGridView.Rows[rowPos].Cells["disc2"].Value = rdr.GetString("PRODUCT_DISC2");
                            cashierDataGridView.Rows[rowPos].Cells["discRP"].Value = rdr.GetString("PRODUCT_DISC_RP");
                            cashierDataGridView.Rows[rowPos].Cells["hpp"].Value = rdr.GetString("HPP");

                            salesQty[rowPos] = rdr.GetString("QTY");
                            productPriceList[rowPos] = rdr.GetString("HARGA_PRODUK");

                            cashierDataGridView.Rows[rowPos].Cells["jumlah"].Value = rdr.GetString("SUBTOTAL");

                            rowPos += 1;
                        }
                    }
                    rdr.Close();
                    break;

                case globalConstants.COPY_NOTA:
                case globalConstants.CASHIER_MODULE:
                    // PULL HEADER DATA
                    sqlCommand = "SELECT SH.SALES_DATE, SH.SALES_INVOICE AS NO_INVOICE, IFNULL(M.CUSTOMER_ID, 0) AS PELANGGAN_ID, IFNULL(M.CUSTOMER_FULL_NAME, '') AS NAMA, IFNULL(M.CUSTOMER_GROUP, 1) AS CUSTOMER_GROUP, SH.SALES_TOTAL AS TOTAL, SH.SALES_DISCOUNT_FINAL AS DISC_FINAL, SH.SALES_TOP AS TOP, DATEDIFF(SH.SALES_TOP_DATE, SH.SALES_DATE) AS TOP_DURATION " +
                                           "FROM SALES_HEADER SH LEFT OUTER JOIN MASTER_CUSTOMER M ON (SH.CUSTOMER_ID = M.CUSTOMER_ID) WHERE SH.SALES_INVOICE = '" + selectedsalesinvoice + "'";
                    rdr = DS.getData(sqlCommand);
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            pelangganTextBox.Text = rdr.GetString("NAMA");
                            noFakturLabel.Text = selectedsalesinvoice;

                            customerComboBox.SelectedIndex = rdr.GetInt32("CUSTOMER_GROUP") - 1;
                            customerComboBox.Text = customerComboBox.Items[customerComboBox.SelectedIndex].ToString();
                            selectedPelangganID = rdr.GetInt32("PELANGGAN_ID");

                            globalTotalValue = rdr.GetDouble("TOTAL");
                            totalPenjualanTextBox.Text = globalTotalValue.ToString("C0", culture);
                            discValue = rdr.GetDouble("DISC_FINAL");
                            discJualMaskedTextBox.Text = discValue.ToString();
                            totalLabel.Text = (globalTotalValue - discValue).ToString("C0", culture);
                            TOPValue = rdr.GetInt32("TOP");

                            if (TOPValue == 1)
                            {
                                cashRadioButton.Checked = true;
                                bayarTextBox.Text = (globalTotalValue - discValue).ToString("C0", culture);
                                bayarAmount = globalTotalValue - discValue;
                            }
                            else
                            {
                                creditRadioButton.Checked = true;
                                TOPDuration = rdr.GetInt32("TOP_DURATION");
                                tempoMaskedTextBox.Text = TOPDuration.ToString();
                                bayarAmount = 0;
                                bayarTextBox.Text = bayarAmount.ToString("C0", culture);
                            }

                            totalAfterDiscTextBox.Text = (globalTotalValue - discValue).ToString("C0", culture);
                        }
                        SODateTimePicker.Value = rdr.GetDateTime("SALES_DATE");
                    }
                    rdr.Close();

                    // PULL DETAIL DATA               
                    sqlCommand = "SELECT M.PRODUCT_ID AS KODE_PRODUK, M.PRODUCT_NAME AS NAMA_PRODUK, SD.PRODUCT_PRICE AS HPP, SD.PRODUCT_SALES_PRICE AS HARGA_PRODUK, SD.PRODUCT_QTY AS QTY, SD.PRODUCT_DISC1, SD.PRODUCT_DISC2, SD.PRODUCT_DISC_RP, SD.SALES_SUBTOTAL AS SUBTOTAL " +
                            "FROM SALES_DETAIL SD, MASTER_PRODUCT M WHERE SD.SALES_INVOICE = '" + selectedsalesinvoice + "' AND SD.PRODUCT_ID = M.PRODUCT_ID";
                    rdr = DS.getData(sqlCommand);
                    if (rdr.HasRows)
                    {
                        rowPos = 0;
                        while (rdr.Read())
                        {
                            addNewRow();

                            //salesQty.Add("0");
                            //disc1.Add("0");
                            //disc2.Add("0");
                            //discRP.Add("0");
                            //productPriceList.Add("0");
                            //jumlahList.Add("0");

                            cashierDataGridView.Rows[rowPos].Cells["productID"].Value = rdr.GetString("KODE_PRODUK");
                            cashierDataGridView.Rows[rowPos].Cells["productName"].Value = rdr.GetString("NAMA_PRODUK");
                            cashierDataGridView.Rows[rowPos].Cells["productPrice"].Value = rdr.GetString("HARGA_PRODUK");
                            cashierDataGridView.Rows[rowPos].Cells["qty"].Value = rdr.GetString("QTY");
                            cashierDataGridView.Rows[rowPos].Cells["disc1"].Value = rdr.GetString("PRODUCT_DISC1");
                            cashierDataGridView.Rows[rowPos].Cells["disc2"].Value = rdr.GetString("PRODUCT_DISC2");
                            cashierDataGridView.Rows[rowPos].Cells["discRP"].Value = rdr.GetString("PRODUCT_DISC_RP");
                            cashierDataGridView.Rows[rowPos].Cells["hpp"].Value = rdr.GetString("HPP");

                            salesQty[rowPos] = rdr.GetString("QTY");
                            productPriceList[rowPos] = rdr.GetString("HARGA_PRODUK");

                            cashierDataGridView.Rows[rowPos].Cells["jumlah"].Value = rdr.GetString("SUBTOTAL");

                            rowPos += 1;
                        }
                    }
                    rdr.Close();

                    if (originModuleID == globalConstants.COPY_NOTA)
                    {
                        string localRefSO = "";
                        // CHECK WHETHER IT'S A SO FULFILLMENT OR NOT
                        sqlCommand = "SELECT REFERENCE_SO FROM SALES_HEADER WHERE SALES_INVOICE =  '" + selectedsalesinvoice + "'";

                        localRefSO = DS.getDataSingleValue(sqlCommand).ToString();
                        if ( localRefSO.Length > 0)
                        { 
                            subOriginModuleID = globalConstants.SO_FULFILLMENT;
                            refSOTextBox.Text = localRefSO;
                            referenceSO = localRefSO;
                        }
                    }
                    break;
            }

            isLoading = false;
        }

        private void cashierForm_Load(object sender, EventArgs e)
        {
            int userAccessOption = 0;
            registerGlobalHotkey();

            salesQty.Add("0");
            disc1.Add("0");
            disc2.Add("0");
            discRP.Add("0");
            productPriceList.Add("0");
            jumlahList.Add("0");

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : cashierForm_Load, ATTEMPT TO LOAD NO FAKTUR");
            loadNoFaktur();
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : cashierForm_Load, ATTEMPT TO addColumnToDataGrid");
            addColumnToDataGrid();

            customerComboBox.SelectedIndex = 0;
            customerComboBox.Text = customerComboBox.Items[0].ToString();

            cashierDataGridView.EditingControlShowing += cashierDataGridView_EditingControlShowing;

            gutil.reArrangeTabOrder(this);
            errorLabel.Text = "";

            SODateTimePicker.Format = DateTimePickerFormat.Custom;
            SODateTimePicker.CustomFormat = globalUtilities.CUSTOM_DATE_FORMAT;

            jobStartDateTimePicker.Format = DateTimePickerFormat.Custom;
            jobStartDateTimePicker.CustomFormat = globalUtilities.CUSTOM_DATE_TIME_FORMAT;

            userStatusLabel.Text = "Welcome, " + DS.getDataSingleValue("SELECT IFNULL(USER_FULL_NAME, 0) FROM MASTER_USER WHERE ID = " + gutil.getUserID()).ToString();

            if (originModuleID == globalConstants.COPY_NOTA && selectedsalesinvoice != "")
            {
                cashierDataGridView.AllowUserToAddRows = false;

                loadInvoiceData();

                errorLabel.Text = "COPY NOTA";

                cashierDataGridView.ReadOnly = true;
                cashierDataGridView.AllowUserToAddRows = false;

                customerComboBox.Enabled = false;
                cashRadioButton.Enabled = false;
                creditRadioButton.Enabled = false;
                tempoMaskedTextBox.ReadOnly = true;
                paymentComboBox.Enabled = false;
                printoutCheckBox.Enabled = false;

                discJualMaskedTextBox.ReadOnly = true;
                bayarTextBox.ReadOnly = true;

                jobStartDateTimePicker.Enabled = false;
                SODateTimePicker.Enabled = false;
                deliveredCheckbox.Visible = false;

                if (subOriginModuleID == globalConstants.SO_FULFILLMENT)
                {
                    refSOLabel.Visible = true;
                    refSOTextBox.Visible = true;
                    cashRadioButton.Enabled = false;
                    creditRadioButton.Enabled = false;
                    bayarTextBox.ReadOnly = true;
                    discJualMaskedTextBox.ReadOnly = true;
                    panel6.Visible = false;
                    totalLabel.Visible = false;
                    label4.Visible = false;
                    cashRadioButton.Visible = false;
                    creditRadioButton.Visible = false;

                    cashierDataGridView.Columns["productPrice"].Visible = false;
                    cashierDataGridView.Columns["disc1"].Visible = false;
                    cashierDataGridView.Columns["disc2"].Visible = false;
                    cashierDataGridView.Columns["discRP"].Visible = false;
                    cashierDataGridView.Columns["jumlah"].Visible = false;
                    cashierDataGridView.Columns["hpp"].Visible = false;
                }
            }
            else if (originModuleID == globalConstants.CASHIER_MODULE && selectedsalesinvoice != "")
            {
                loadInvoiceData();
            }
            else if (originModuleID == globalConstants.EDIT_SALES_ORDER)
            {
                loadInvoiceData();

                creditRadioButton.Checked = true;
                bayarTextBox.ReadOnly = true;
                cashRadioButton.Enabled = false;
                creditRadioButton.Enabled = false;
            }
            else if (originModuleID == globalConstants.SO_FULFILLMENT)
            {
                loadInvoiceData();

                refSOLabel.Visible = true;
                refSOTextBox.Visible = true;
                cashRadioButton.Enabled = false;
                creditRadioButton.Enabled = false;
                bayarTextBox.ReadOnly = true;
                discJualMaskedTextBox.ReadOnly = true;
                panel6.Visible = false;
                totalLabel.Visible = false;
                label4.Visible = false;
                cashRadioButton.Visible = false;
                creditRadioButton.Visible = false;
            }
            //else if (originModuleID == globalConstants.SALES_QUOTATION)
            //{
            //    //noFakturLabel.Text = "SALES QUOTATION";
            //    approvalButton.Visible = false;
            //    rejectButton.Visible = false;

            //    deliveredCheckbox.Visible = true;

            //    labelTglOrder.Visible = true;
            //    jobStartDateTimePicker.Visible = true;

            //    labelBayar.Text = "Total DP";

            //    jobStartDateTimePicker.Format = DateTimePickerFormat.Custom;
            //    jobStartDateTimePicker.CustomFormat = globalUtilities.CUSTOM_DATE_FORMAT;
            //}
            //else if (originModuleID == globalConstants.EDIT_SALES_QUOTATION)
            //{
            //    //noFakturLabel.Text = "SALES QUOTATION";

            //    loadInvoiceData();

            //    userAccessOption = DS.getUserAccessRight(globalConstants.MENU_SALES_QUOTATION, gutil.getUserGroupID());

            //    if (userAccessOption == 1)
            //    {
            //        approvalButton.Visible = true;
            //        rejectButton.Visible = true;
            //    }
            //    else
            //    {
            //        approvalButton.Visible = false;
            //        rejectButton.Visible = false;
            //    }

            //    deliveredCheckbox.Visible = true;

            //    labelBayar.Text = "Total DP";

            //    labelTglOrder.Visible = true;
            //    jobStartDateTimePicker.Visible = true;

            //    jobStartDateTimePicker.Format = DateTimePickerFormat.Custom;
            //    jobStartDateTimePicker.CustomFormat = globalUtilities.CUSTOM_DATE_FORMAT;
            //}
            //else if (originModuleID == globalConstants.SQ_TO_SO)
            //{
            //    loadInvoiceData();

            //    approvalButton.Visible = false;
            //    rejectButton.Visible = false;

            //    deliveredCheckbox.Visible = true;

            //    labelTglOrder.Visible = true;
            //    labelTglOrder.Text = "Total DP";
            //    jobStartDateTimePicker.Visible = false;
            //    DPTextBox.Visible = true;

            //    jobStartDateTimePicker.Enabled = false;

            //    deliveredCheckbox.Enabled = false;
            //    addressTextBox.ReadOnly = true;

            //    jobStartDateTimePicker.Format = DateTimePickerFormat.Custom;
            //    jobStartDateTimePicker.CustomFormat = globalUtilities.CUSTOM_DATE_FORMAT;
            //}
            //else if (originModuleID == globalConstants.COPY_NOTA_SQ)
            //{
            //    loadInvoiceData();

            //    approvalButton.Visible = false;
            //    rejectButton.Visible = false;

            //    deliveredCheckbox.Visible = true;

            //    labelTglOrder.Visible = true;
            //    jobStartDateTimePicker.Visible = true;

            //    jobStartDateTimePicker.Enabled = false;
            //    deliveredCheckbox.Enabled = false;
            //    addressTextBox.ReadOnly = true;

            //    jobStartDateTimePicker.Format = DateTimePickerFormat.Custom;
            //    jobStartDateTimePicker.CustomFormat = globalUtilities.CUSTOM_DATE_FORMAT;

            //    cashierDataGridView.ReadOnly = true;
            //    cashierDataGridView.AllowUserToAddRows = false;

            //    customerComboBox.Enabled = false;
            //    cashRadioButton.Enabled = false;
            //    creditRadioButton.Enabled = false;
            //    tempoMaskedTextBox.ReadOnly = true;
            //    paymentComboBox.Enabled = false;
            //    printoutCheckBox.Enabled = false;

            //    discJualMaskedTextBox.ReadOnly = true;
            //    bayarTextBox.ReadOnly = true;
            //}

            //add double buffer
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(cashierDataGridView, true, null);
        }

        private void cashierForm_Activated(object sender, EventArgs e)
        {
            //if need something
            registerGlobalHotkey();

            updateLabel();
            timer1.Start();

            if (cashierDataGridView.Focused)
                registerDelKey();
        }

        private void cashierForm_Deactivate(object sender, EventArgs e)
        {
            timer1.Stop();
            unregisterGlobalHotkey();

            if (delKeyRegistered)
                unregisterDelKey();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            updateLabel();
        }

        private void addColumnToDataGrid()
        {
            MySqlDataReader rdr;
            int userAccessOption = 0;
            bool discVisible = true;

            DataGridViewTextBoxColumn F8Column = new DataGridViewTextBoxColumn();

            DataGridViewTextBoxColumn productIdColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn productNameColumn = new DataGridViewTextBoxColumn();

            DataGridViewTextBoxColumn productHPPColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn productPriceColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn stockQtyColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn disc1Column = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn disc2Column = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn discRPColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn subTotalColumn = new DataGridViewTextBoxColumn();

            // F8 COLUMN
            F8Column.HeaderText = "F8";
            F8Column.Name = "F8";
            F8Column.Width = 44;
            F8Column.ReadOnly = true;
            cashierDataGridView.Columns.Add(F8Column);

            productIdColumn.HeaderText = "KODE PRODUK";
            productIdColumn.Name = "productID";
            productIdColumn.Width = 150;
            cashierDataGridView.Columns.Add(productIdColumn);

            cashierDataGridView.CurrentCell = cashierDataGridView.Rows[cashierDataGridView.Rows.Count - 1].Cells["productID"];

            // PRODUCT NAME COLUMN
            productNameColumn.HeaderText = "NAMA PRODUK";
            productNameColumn.Name = "productName";
            productNameColumn.Width = 200;
            //productNameColumn.ReadOnly = true;
            cashierDataGridView.Columns.Add(productNameColumn);

            productPriceColumn.HeaderText = "HARGA";
            productPriceColumn.Name = "productPrice";
            productPriceColumn.Width = 100;

            // USER WHO HAS ACCESS TO PENGATURAN HARGA CAN EDIT THE PRODUCT PRICE MANUALLY
            userAccessOption = DS.getUserAccessRight(globalConstants.MENU_PENGATURAN_HARGA, gutil.getUserGroupID());
            if (userAccessOption != 1)
                productPriceColumn.ReadOnly = true;

            cashierDataGridView.Columns.Add(productPriceColumn);

            stockQtyColumn.HeaderText = "QTY";
            stockQtyColumn.Name = "qty";
            stockQtyColumn.Width = 80;
            cashierDataGridView.Columns.Add(stockQtyColumn);

            // LARAS : DISC PER ITEM IS DISABLED
            //if (originModuleID == globalConstants.SALES_QUOTATION || originModuleID == globalConstants.EDIT_SALES_QUOTATION)
            if (originModuleID == globalConstants.SO_FULFILLMENT)
            {
                discVisible = false;
            }

            disc1Column.HeaderText = "DISC 1 (%)";
            disc1Column.Name = "disc1";
            disc1Column.Width = 75;
            disc1Column.MaxInputLength = 5;
            disc1Column.Visible = discVisible;
            cashierDataGridView.Columns.Add(disc1Column);

            disc2Column.HeaderText = "DISC 2 (%)";
            disc2Column.Name = "disc2";
            disc2Column.Width = 75;
            disc2Column.MaxInputLength = 5;
            disc2Column.Visible = discVisible;
            cashierDataGridView.Columns.Add(disc2Column);

            discRPColumn.HeaderText = "DISC RP";
            discRPColumn.Name = "discRP";
            discRPColumn.Width = 100;
            discRPColumn.Visible = discVisible;
            cashierDataGridView.Columns.Add(discRPColumn);

            subTotalColumn.HeaderText = "JUMLAH";
            subTotalColumn.Name = "jumlah";
            subTotalColumn.Width = 150;
            subTotalColumn.ReadOnly = true;
            cashierDataGridView.Columns.Add(subTotalColumn);

            productHPPColumn.HeaderText = "HPP";
            productHPPColumn.Name = "hpp";
            productHPPColumn.Width = 200;
            productHPPColumn.Visible = false;
            cashierDataGridView.Columns.Add(productHPPColumn);

            if (originModuleID == globalConstants.SO_FULFILLMENT)
            {
                productHPPColumn.Visible = false;
                subTotalColumn.Visible = false;
                productPriceColumn.Visible = false;
            }
        }

        private void deleteCurrentRow()
        {
            if (cashierDataGridView.SelectedCells.Count > 0)
            {
                int rowSelectedIndex = cashierDataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = cashierDataGridView.Rows[rowSelectedIndex];
                cashierDataGridView.CurrentCell = selectedRow.Cells["productName"];

                if (null != selectedRow && rowSelectedIndex != cashierDataGridView.Rows.Count - 1)
                {
                    for (int i = rowSelectedIndex; i < cashierDataGridView.Rows.Count - 1; i++)
                    {
                        salesQty[i] = salesQty[i + 1];
                        productPriceList[i] = productPriceList[i + 1];
                        jumlahList[i] = jumlahList[i + 1];
                        disc1[i] = disc1[i + 1];
                        disc2[i] = disc2[i + 1];
                        discRP[i] = discRP[i + 1];
                    }

                    isLoading = true;
                    cashierDataGridView.Rows.Remove(selectedRow);
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : deleteCurrentRow [" + rowSelectedIndex + "]");
                    isLoading = false;
                }
            }
        }

        private void customerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading)
                return;

            refreshProductPrice();
        }

        private void cashRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (cashRadioButton.Checked)
            {
                labelBayar.Text = "B a y a r [+]";
                jobStartDateTimePicker.Visible = false;
                //labelJamKirim.Visible = false;
                labelTglKirim.Visible = false;
                //timeStartDateTimePicker.Visible = false;
                SOisPesanan = false;
                deliveredCheckbox.Visible = false;
                addressTextBox.Visible = false;

                //tempoMaskedTextBox.Visible = false;
                //labelCaraBayar.Visible = false;
                //labelCaraBayar.Text = "Cara Bayar       :";
                //paymentComboBox.Visible = true;
                //paymentComboBox.SelectedIndex = 0;
                //paymentComboBox.Text = paymentComboBox.Items[0].ToString();
                //bayarTextBox.Enabled = true;
            }
        }

        //private void calculateTotalAfterDP()
        //{
        //    double totalAfterDP = globalTotalValue;
        //    double localChangeValue = 0;

        //    if (bayarAmount > 0)
        //    {
        //        if (bayarAmount <= globalTotalValue - discValue)
        //            totalAfterDP = globalTotalValue - bayarAmount - discValue;
        //        else
        //        {
        //            totalAfterDP = 0;
        //            localChangeValue = bayarAmount - (globalTotalValue - discValue);
        //        }
        //    }
            
        //    totalLabel.Text = (totalAfterDP).ToString("C0", culture);
        //    uangKembaliTextBox.Text = localChangeValue.ToString("C0", culture);
        //}

        private void bayarTextBox_TextChanged(object sender, EventArgs e)
        {
            string tempString;

            if (isLoading)
                return;

            isLoading = true;
            if (bayarTextBox.Text.Length == 0)
            {
                // IF TEXTBOX IS EMPTY, SET THE VALUE TO 0 AND EXIT THE CHECKING
                bayarAmountText = "0";
                bayarTextBox.Text = "0";

                bayarTextBox.SelectionStart = bayarTextBox.Text.Length;
                isLoading = false;

                return;
            }
            // CHECKING TO PREVENT PREFIX "0" IN A NUMERIC INPUT WHILE ALLOWING A DECIMAL VALUE STARTED WITH "0"
            else if (bayarTextBox.Text.IndexOf('0') == 0 && bayarTextBox.Text.Length > 1 && bayarTextBox.Text.IndexOf("0.") < 0)
            {
                tempString = bayarTextBox.Text;
                bayarTextBox.Text = tempString.Remove(0, 1);
            }

            if (gutil.matchRegEx(bayarTextBox.Text, globalUtilities.REGEX_NUMBER_ONLY))
                bayarAmountText = bayarTextBox.Text;
            else
                bayarTextBox.Text = bayarAmountText;

            bayarTextBox.SelectionStart = bayarTextBox.Text.Length;
            isLoading = false;
            bayarAmount = Convert.ToDouble(bayarTextBox.Text);

            ////if (originModuleID == 0)
            //if (originModuleID == globalConstants.EDIT_SALES_QUOTATION)
            //    calculateTotalAfterDP();
            //else
                calculateChangeValue();
        }

        private void paymentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedPaymentMethod = paymentComboBox.SelectedIndex;
        }

        private void ChangePrinterButton_Click(object sender, EventArgs e)
        {
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ChangePrinterButton_Click, DISPLAY PRINTER SELECTION FORM");
            SetPrinterForm displayedForm = new SetPrinterForm();
            displayedForm.ShowDialog(this);
        }

        private void loadInfoToko(int opt, out string namatoko, out string almt, out string telepon, out string email)
        {
            MySqlDataReader rdr;
            DataTable dt = new DataTable();
            namatoko = ""; almt = ""; telepon = ""; email = "";
            DS.mySqlConnect();
            //1 load default 2 setting user
            using (rdr = DS.getData("SELECT IFNULL(BRANCH_ID,0) AS 'BRANCH_ID', IFNULL(HQ_IP4,'') AS 'IP', IFNULL(STORE_NAME,'') AS 'NAME', IFNULL(STORE_ADDRESS,'') AS 'ADDRESS', IFNULL(STORE_PHONE,'') AS 'PHONE', IFNULL(STORE_EMAIL,'') AS 'EMAIL' FROM SYS_CONFIG WHERE ID =  " + opt))
            {
                if (rdr.HasRows)
                {
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : loadInfoToko");
                    while (rdr.Read())
                    {
                        if (!String.IsNullOrEmpty(rdr.GetString("NAME")))
                        {
                            namatoko = rdr.GetString("NAME");
                        }
                        if (!String.IsNullOrEmpty(rdr.GetString("ADDRESS")))
                        {
                            almt = rdr.GetString("ADDRESS");
                        }
                        if (!String.IsNullOrEmpty(rdr.GetString("PHONE")))
                        {
                            telepon = rdr.GetString("PHONE");
                        }
                        if (!String.IsNullOrEmpty(rdr.GetString("EMAIL")))
                        {
                            email = rdr.GetString("EMAIL");
                        }
                    }
                }
            }
        }

        private void loadNamaUser(int user_id, out string nama)
        {
            nama = "";
            MySqlDataReader rdr;
            DataTable dt = new DataTable();
            DS.mySqlConnect();
            //1 load default 2 setting user
            using (rdr = DS.getData("SELECT USER_NAME AS 'NAME' FROM MASTER_USER WHERE ID=" + user_id))
            {
                if (rdr.HasRows)
                {
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : loadNamaUser");

                    rdr.Read();
                    nama = rdr.GetString("NAME");
                }
            }
        }

        private void PrintReceipt()
        {
            //pdoc = new PrintDocument();
            //pd.Document = pdoc;
            //pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
            //    PrintPreviewDialog pp = new PrintPreviewDialog();
            //Font font = new Font("Courier New", 15);

            //cek paper mode
            int papermode = gutil.getPaper();
            int paperLength = 0;

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : PrintReceipt");


            if (papermode == 0) //kertas POS
            {
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : PrintReceipt, POS size Paper");
                //width, height
                paperLength = calculatePageLength();
                PaperSize psize = new PaperSize("Custom", 255, paperLength);//820); //change paper size receipt width x height
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
            else
            {
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : PrintReceipt, papermode [" + papermode + "]");

                //kertas 1/2 kwarto atau kwarto using crystal report
                //preview laporan
                DS.mySqlConnect();
                string sqlCommandx = "";

                if (originModuleID == 0 || originModuleID == globalConstants.EDIT_SALES_ORDER || originModuleID == globalConstants.COPY_NOTA)
                {
                    // NORMAL TRANSACTION
                    sqlCommandx = "SELECT MU.USER_NAME, SD.ID, SH.SALES_DATE AS 'DATE', SD.SALES_INVOICE AS 'INVOICE', MC.CUSTOMER_FULL_NAME AS 'CUSTOMER', M.PRODUCT_NAME AS 'PRODUCT', PRODUCT_QTY AS 'QTY', " +
                    "PRODUCT_SALES_PRICE AS 'PRICE', ROUND((PRODUCT_QTY * PRODUCT_SALES_PRICE) - SALES_SUBTOTAL, 2) AS 'POTONGAN', SALES_SUBTOTAL AS 'SUBTOTAL', SH.SALES_PAYMENT AS 'PAYMENT', SH.SALES_PAYMENT_CHANGE AS 'CHANGE', SH.SALES_DISCOUNT_FINAL " +
                    "FROM MASTER_USER MU, SALES_HEADER SH, SALES_DETAIL SD, MASTER_PRODUCT M, MASTER_CUSTOMER MC WHERE MU.ID = " + gutil.getUserID() + " AND SD.PRODUCT_ID = M.PRODUCT_ID AND SD.SALES_INVOICE = SH.SALES_INVOICE AND SH.CUSTOMER_ID = MC.CUSTOMER_ID AND SH.SALES_INVOICE='" + selectedsalesinvoice + "'" +
                    "UNION " +
                    "SELECT MU.USER_NAME, SD.ID, SH.SALES_DATE AS 'DATE', SD.SALES_INVOICE AS 'INVOICE', 'P-UMUM' AS 'CUSTOMER', M.PRODUCT_NAME AS 'PRODUCT', PRODUCT_QTY AS 'QTY', PRODUCT_SALES_PRICE AS 'PRICE', " +
                    "ROUND((PRODUCT_QTY * PRODUCT_SALES_PRICE) - SALES_SUBTOTAL, 2) AS 'POTONGAN', SALES_SUBTOTAL AS 'SUBTOTAL', SH.SALES_PAYMENT AS 'PAYMENT', SH.SALES_PAYMENT_CHANGE AS 'CHANGE', SH.SALES_DISCOUNT_FINAL " +
                    "FROM MASTER_USER MU, SALES_HEADER SH, SALES_DETAIL SD, MASTER_PRODUCT M WHERE MU.ID = " + gutil.getUserID() + " AND SD.PRODUCT_ID = M.PRODUCT_ID AND SD.SALES_INVOICE = SH.SALES_INVOICE AND SH.CUSTOMER_ID = 0 AND SH.SALES_INVOICE='" + selectedsalesinvoice + "'";
                }
                //else if (originModuleID == globalConstants.SALES_QUOTATION || originModuleID == globalConstants.EDIT_SALES_QUOTATION || originModuleID == globalConstants.COPY_NOTA_SQ)
                //{
                //    // SALES QUOTATION
                //    sqlCommandx = "SELECT MU.USER_NAME, SD.ID, SH.SQ_ORDER_DATE AS 'ORDER DATE', SH.SQ_DATE AS 'DATE', SD.SQ_INVOICE AS 'INVOICE', MC.CUSTOMER_FULL_NAME AS 'CUSTOMER', M.PRODUCT_NAME AS 'PRODUCT', PRODUCT_QTY AS 'QTY', " +
                //    "PRODUCT_SALES_PRICE AS 'PRICE', SQ_SUBTOTAL AS 'SUBTOTAL', SH.SQ_DP AS 'PAYMENT', SH.SQ_ORDER_ADDRESS, SH.DELIVERED, SH.SALES_DISCOUNT_FINAL " +
                //    "FROM MASTER_USER MU, SALES_QUOTATION_HEADER SH, SALES_QUOTATION_DETAIL SD, MASTER_PRODUCT M, MASTER_CUSTOMER MC WHERE MU.ID = " + gutil.getUserID() + " AND SD.PRODUCT_ID = M.PRODUCT_ID AND SD.SQ_INVOICE = SH.SQ_INVOICE AND SH.CUSTOMER_ID = MC.CUSTOMER_ID AND SH.SQ_INVOICE='" + selectedsalesinvoice + "'" +
                //    "UNION " +
                //    "SELECT MU.USER_NAME, SD.ID, SH.SQ_ORDER_DATE AS 'ORDER DATE', SH.SQ_DATE AS 'DATE', SD.SQ_INVOICE AS 'INVOICE', 'P-UMUM' AS 'CUSTOMER', M.PRODUCT_NAME AS 'PRODUCT', PRODUCT_QTY AS 'QTY', PRODUCT_SALES_PRICE AS 'PRICE', " +
                //    "SQ_SUBTOTAL AS 'SUBTOTAL', SH.SQ_DP AS 'PAYMENT', SH.SQ_ORDER_ADDRESS, SH.DELIVERED, SH.SALES_DISCOUNT_FINAL " +
                //    "FROM MASTER_USER MU, SALES_QUOTATION_HEADER SH, SALES_QUOTATION_DETAIL SD, MASTER_PRODUCT M WHERE MU.ID = " + gutil.getUserID() + " AND SD.PRODUCT_ID = M.PRODUCT_ID AND SD.SQ_INVOICE = SH.SQ_INVOICE AND SH.CUSTOMER_ID = 0 AND SH.SQ_INVOICE='" + selectedsalesinvoice + "'";
                //}
                else
                {
                    // GET DUMMY DATA
                    sqlCommandx = "SELECT MU.USER_NAME, SD.ID, SH.SALES_DATE AS 'DATE', SD.SALES_INVOICE AS 'INVOICE', MC.CUSTOMER_FULL_NAME AS 'CUSTOMER', M.PRODUCT_NAME AS 'PRODUCT', PRODUCT_QTY AS 'QTY', " +
                    "PRODUCT_SALES_PRICE AS 'PRICE', ROUND((PRODUCT_QTY * PRODUCT_SALES_PRICE) - SALES_SUBTOTAL, 2) AS 'POTONGAN', SALES_SUBTOTAL AS 'SUBTOTAL', SH.SALES_PAYMENT AS 'PAYMENT', SH.SALES_PAYMENT_CHANGE AS 'CHANGE' " +
                    "FROM MASTER_USER MU, SALES_HEADER_TAX SH, SALES_DETAIL_TAX SD, MASTER_PRODUCT M, MASTER_CUSTOMER MC WHERE MU.ID = " + gutil.getUserID() + " AND SD.PRODUCT_ID = M.PRODUCT_ID AND SD.SALES_INVOICE = SH.SALES_INVOICE AND SH.CUSTOMER_ID = MC.CUSTOMER_ID AND SH.SALES_INVOICE='" + selectedsalesinvoiceTax + "'" +
                    "UNION " +
                    "SELECT MU.USER_NAME, SD.ID, SH.SALES_DATE AS 'DATE', SD.SALES_INVOICE AS 'INVOICE', 'P-UMUM' AS 'CUSTOMER', M.PRODUCT_NAME AS 'PRODUCT', PRODUCT_QTY AS 'QTY', PRODUCT_SALES_PRICE AS 'PRICE', " +
                    "ROUND((PRODUCT_QTY * PRODUCT_SALES_PRICE) - SALES_SUBTOTAL, 2) AS 'POTONGAN', SALES_SUBTOTAL AS 'SUBTOTAL', SH.SALES_PAYMENT AS 'PAYMENT', SH.SALES_PAYMENT_CHANGE AS 'CHANGE' " +
                    "FROM MASTER_USER MU, SALES_HEADER_TAX SH, SALES_DETAIL_TAX SD, MASTER_PRODUCT M WHERE MU.ID = " + gutil.getUserID() + " AND SD.PRODUCT_ID = M.PRODUCT_ID AND SD.SALES_INVOICE = SH.SALES_INVOICE AND SH.CUSTOMER_ID = 0 AND SH.SALES_INVOICE='" + selectedsalesinvoiceTax + "'";
                }

                //if (originModuleID == globalConstants.SALES_QUOTATION || originModuleID == globalConstants.EDIT_SALES_QUOTATION || originModuleID == globalConstants.COPY_NOTA_SQ)
                //{
                //    DS.writeXML(sqlCommandx, globalConstants.salesQuotationReceiptXML);
                //    if (gutil.getPaper() == 2) // kuarto
                //    {
                //        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : PrintReceipt, kuarto paper, display SalesQuotationReceiptKuartoForm");
                //        salesQuotationKuartoPrintOutForm displayedform = new salesQuotationKuartoPrintOutForm();
                //        displayedform.ShowDialog(this); ;
                //    }
                //    else
                //    {
                //        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : PrintReceipt, 1/2 kuarto display SalesQuotationReceiptForm");
                //        salesQuotationPrintOutForm displayedform = new salesQuotationPrintOutForm();
                //        displayedform.ShowDialog(this); ;
                //    }
                //}
                //else
                {
                    DS.writeXML(sqlCommandx, globalConstants.SalesReceiptXML);
                    if (gutil.getPaper() == 2) // kuarto
                    {
                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : PrintReceipt, kuarto paper, display SalesReceiptKuartoForm");
                        SalesReceiptKuartoForm displayedform = new SalesReceiptKuartoForm();
                        displayedform.ShowDialog(this); ;
                    } else
                    {
                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : PrintReceipt, display SalesReceiptForm");
                        SalesReceiptForm displayedform = new SalesReceiptForm();
                        displayedform.ShowDialog(this);
                    }
                }
            }
        }

        private int calculatePageLength()
        {
            String ucapan = "";
            string nm, almt, tlpn, email;

            //event printing

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : printDocument1_PrintPage, print POS size receipt");

            int startX = 0;
            int startY = 0;
            int colxwidth = 85; //old 75
            int totrowwidth = 255; //old 250
            Font font = new Font("Courier New", 9);
            int rowheight = (int)Math.Ceiling(font.GetHeight());
            //int inlineheader = 12;
            //int inlinetext = 10;
            int add_offset = rowheight;
            int Offset = 5;
            int offset_plus = 3;
            int fontSize = 8;

            int totalLengthPage = startY + Offset;


            //HEADER


            loadInfoToko(2, out nm, out almt, out tlpn, out email);

            Offset = Offset + add_offset;

            Offset = Offset + add_offset;

            if (!email.Equals(""))
            {
                Offset = Offset + add_offset;
            }

            Offset = Offset + add_offset;

            //start of content
            MySqlDataReader rdr;
            DataTable dt = new DataTable();

            DS.mySqlConnect();
            //load customer id
            string customer = "";
            string tgl = "";
            string group = "";
            double total = 0;
            string sqlCommand = "";

            if (originModuleID == 0 || originModuleID == globalConstants.EDIT_SALES_ORDER || originModuleID == globalConstants.SO_FULFILLMENT || originModuleID == globalConstants.COPY_NOTA)  // NORMAL TRANSACTION
            {
                sqlCommand = "SELECT S.SALES_INVOICE AS 'INVOICE', C.CUSTOMER_FULL_NAME AS 'CUSTOMER',DATE_FORMAT(S.SALES_DATE, '%d-%M-%Y') AS 'DATE',S.SALES_TOTAL AS 'TOTAL', IF(C.CUSTOMER_GROUP=1,'RETAIL',IF(C.CUSTOMER_GROUP=2,'GROSIR','PARTAI')) AS 'GROUP' FROM SALES_HEADER S,MASTER_CUSTOMER C WHERE S.CUSTOMER_ID = C.CUSTOMER_ID AND S.SALES_INVOICE = '" + selectedsalesinvoice + "'" +
                    " UNION " +
                    "SELECT S.SALES_INVOICE AS 'INVOICE', 'P-UMUM' AS 'CUSTOMER', DATE_FORMAT(S.SALES_DATE, '%d-%M-%Y') AS 'DATE', S.SALES_TOTAL AS 'TOTAL', 'RETAIL' AS 'GROUP' FROM SALES_HEADER S WHERE S.CUSTOMER_ID = 0 AND S.SALES_INVOICE = '" + selectedsalesinvoice + "'" +
                    "ORDER BY DATE ASC";
            }
            else if (originModuleID == globalConstants.DUMMY_TRANSACTION_TAX)
            {
                // GET DUMMY TAX DATA
                sqlCommand = "SELECT S.SALES_INVOICE AS 'INVOICE', C.CUSTOMER_FULL_NAME AS 'CUSTOMER',DATE_FORMAT(S.SALES_DATE, '%d-%M-%Y') AS 'DATE',S.SALES_TOTAL AS 'TOTAL', IF(C.CUSTOMER_GROUP=1,'RETAIL',IF(C.CUSTOMER_GROUP=2,'GROSIR','PARTAI')) AS 'GROUP' FROM SALES_HEADER_TAX S,MASTER_CUSTOMER C WHERE S.CUSTOMER_ID = C.CUSTOMER_ID AND S.SALES_INVOICE = '" + selectedsalesinvoiceTax + "'" +
                                        " UNION " +
                                        "SELECT S.SALES_INVOICE AS 'INVOICE', 'P-UMUM' AS 'CUSTOMER', DATE_FORMAT(S.SALES_DATE, '%d-%M-%Y') AS 'DATE', S.SALES_TOTAL AS 'TOTAL', 'RETAIL' AS 'GROUP' FROM SALES_HEADER_TAX S WHERE S.CUSTOMER_ID = 0 AND S.SALES_INVOICE = '" + selectedsalesinvoiceTax + "'" +
                                        "ORDER BY DATE ASC";
            }
            //else if (originModuleID == globalConstants.SALES_QUOTATION || originModuleID == globalConstants.EDIT_SALES_QUOTATION)
            //{
            //    sqlCommand = "SELECT S.SQ_INVOICE AS 'INVOICE', C.CUSTOMER_FULL_NAME AS 'CUSTOMER',DATE_FORMAT(S.SQ_ORDER_DATE, '%d-%M-%Y') AS 'DATE',S.SQ_TOTAL AS 'TOTAL', IF(C.CUSTOMER_GROUP=1,'RETAIL',IF(C.CUSTOMER_GROUP=2,'GROSIR','PARTAI')) AS 'GROUP' FROM SALES_QUOTATION_HEADER S,MASTER_CUSTOMER C WHERE S.CUSTOMER_ID = C.CUSTOMER_ID AND S.SQ_INVOICE = '" + selectedsalesinvoice + "'" +
            //                            " UNION " +
            //                            "SELECT S.SQ_INVOICE AS 'INVOICE', 'P-UMUM' AS 'CUSTOMER', DATE_FORMAT(S.SQ_ORDER_DATE, '%d-%M-%Y') AS 'DATE', S.SQ_TOTAL AS 'TOTAL', 'RETAIL' AS 'GROUP' FROM SALES_QUOTATION_HEADER S WHERE S.CUSTOMER_ID = 0 AND S.SQ_INVOICE = '" + selectedsalesinvoice + "'" +
            //                            "ORDER BY DATE ASC";
            //}

            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    rdr.Read();
                    customer = rdr.GetString("CUSTOMER");
                    tgl = rdr.GetString("DATE");
                    total = rdr.GetDouble("TOTAL");
                    group = rdr.GetString("GROUP");
                }
            }
            DS.mySqlClose();

            //1. PAYMENT METHOD
            Offset = Offset + add_offset;

            //2. CUSTOMER NAME
            Offset = Offset + add_offset;

            Offset = Offset + add_offset;

            Offset = Offset + add_offset;

            Offset = Offset + add_offset + offset_plus;

            Offset = Offset + add_offset;

            Offset = Offset + add_offset;

            Offset = Offset + add_offset;

            if (originModuleID == globalConstants.SQ_TO_SO)
            {
                Offset = Offset + add_offset;
            }
            
            //DETAIL PENJUALAN
            //read sales_detail

            //DS.mySqlConnect();
            float startRightX = totrowwidth - colxwidth - startX;

            if (originModuleID == 0 || originModuleID == globalConstants.EDIT_SALES_ORDER || originModuleID == globalConstants.SO_FULFILLMENT || originModuleID == globalConstants.COPY_NOTA)
            {
                // NORMAL TRANSACTION
                sqlCommand = "SELECT S.ID, S.PRODUCT_ID AS 'P-ID', P.PRODUCT_NAME AS 'NAME', S.PRODUCT_QTY AS 'QTY',ROUND(S.SALES_SUBTOTAL/S.PRODUCT_QTY) AS 'PRICE', S.SALES_SUBTOTAL  FROM sales_detail S, master_product P WHERE S.PRODUCT_ID=P.PRODUCT_ID AND S.SALES_INVOICE='" + selectedsalesinvoice + "'";
            }
            else if (originModuleID == globalConstants.DUMMY_TRANSACTION_TAX)
            {
                // GET DUMMY DATA
                sqlCommand = "SELECT S.ID, S.PRODUCT_ID AS 'P-ID', P.PRODUCT_NAME AS 'NAME', S.PRODUCT_QTY AS 'QTY',ROUND(S.SALES_SUBTOTAL/S.PRODUCT_QTY) AS 'PRICE', S.SALES_SUBTOTAL FROM sales_detail_tax S, master_product P WHERE S.PRODUCT_ID=P.PRODUCT_ID AND S.SALES_INVOICE='" + selectedsalesinvoiceTax + "'";
            }
            //else if (originModuleID == globalConstants.SALES_QUOTATION || originModuleID == globalConstants.EDIT_SALES_QUOTATION)
            //{
            //    sqlCommand = "SELECT S.ID, S.PRODUCT_ID AS 'P-ID', P.PRODUCT_NAME AS 'NAME', S.PRODUCT_QTY AS 'QTY', S.PRODUCT_SALES_PRICE AS 'PRICE', S.SQ_SUBTOTAL as SALES_SUBTOTAL FROM sales_quotation_detail S, master_product P WHERE S.PRODUCT_ID=P.PRODUCT_ID AND S.SQ_INVOICE='" + selectedsalesinvoice + "'";
            //}

            using (rdr = DS.getData(sqlCommand))//+ "group by s.product_id") )
            {
                if (rdr.HasRows)
                {
                    int i = 0;
                    while (rdr.Read())
                    {
                        if (i == 0)
                        {
                            Offset = Offset + add_offset;
                        }
                        else
                        {
                            i = 1;
                            Offset = Offset + add_offset + offset_plus;
                        }

                        //new line
                        Offset = Offset + add_offset;
                        //
                    }
                }
            }
            DS.mySqlClose();

            if (originModuleID != globalConstants.SO_FULFILLMENT && subOriginModuleID != globalConstants.SO_FULFILLMENT)
            { 
                Offset = Offset + add_offset;

                Offset = Offset + add_offset;

                if (cashRadioButton.Checked == true)
                {
                    Offset = Offset + add_offset;

                    Offset = Offset + add_offset;
                }
            }

            Offset = Offset + add_offset + offset_plus + offset_plus; ;
            //eNd of content

            //FOOTER

            Offset = Offset + add_offset;

            Offset = Offset + add_offset;

            Offset = Offset + add_offset;

            Offset = Offset + add_offset;

            Offset = Offset + add_offset;

            totalLengthPage = totalLengthPage + Offset + add_offset + add_offset;

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : calculatePageLength, totalLengthPage [" + totalLengthPage + "]");
            return totalLengthPage;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            String ucapan = "";
            string nm, almt, tlpn, email;

            //event printing

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : printDocument1_PrintPage, print POS size receipt");

            Graphics graphics = e.Graphics;
            int startX = 0;
            int startY = 0;
            int colxwidth = 85; //old 75
            int totrowwidth = 255; //old 250
            Font font = new Font("Courier New", 9);
            int rowheight = (int)Math.Ceiling(font.GetHeight());
            //int inlineheader = 12;
            //int inlinetext = 10;
            int add_offset = rowheight;
            int Offset = 5;
            int offset_plus = 3;
            int fontSize = 8;
            //HEADER

            //set allignemnt
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            //set whole printing area
            System.Drawing.RectangleF rect = new System.Drawing.RectangleF(startX, startY + Offset, totrowwidth, rowheight);
            //set right print area
            System.Drawing.RectangleF rectright = new System.Drawing.RectangleF(totrowwidth - colxwidth - startX, startY + Offset, colxwidth, rowheight);
            //set middle print area
            System.Drawing.RectangleF rectcenter = new System.Drawing.RectangleF((startX + totrowwidth), startY + Offset, colxwidth, rowheight);

            loadInfoToko(2, out nm, out almt, out tlpn, out email);

            graphics.DrawString(nm, new Font("Courier New", 9),
                                new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            graphics.DrawString(almt,
                     new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            graphics.DrawString(tlpn,
                     new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);

            if (!email.Equals(""))
            {
                Offset = Offset + add_offset;
                rect.Y = startY + Offset;
                graphics.DrawString(email,
                         new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);
            }

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            //String underLine = "--------------------------------";  //32 character
            String underLine = "------------------------------";  //32 character
            graphics.DrawString(underLine, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), rect, sf);
            //end of header

            //start of content
            MySqlDataReader rdr;
            DataTable dt = new DataTable();

            DS.mySqlConnect();
            //load customer id
            string customer = "";
            string tgl = "";
            string group = "";
            double total = 0;
            string sqlCommand = "";

            if (originModuleID == 0 || originModuleID == globalConstants.EDIT_SALES_ORDER || originModuleID == globalConstants.SO_FULFILLMENT || originModuleID == globalConstants.COPY_NOTA)  // NORMAL TRANSACTION
            {
                sqlCommand = "SELECT S.SALES_INVOICE AS 'INVOICE', C.CUSTOMER_FULL_NAME AS 'CUSTOMER',DATE_FORMAT(S.SALES_DATE, '%d-%M-%Y') AS 'DATE',S.SALES_TOTAL AS 'TOTAL', IF(C.CUSTOMER_GROUP=1,'RETAIL',IF(C.CUSTOMER_GROUP=2,'GROSIR','PARTAI')) AS 'GROUP' FROM SALES_HEADER S,MASTER_CUSTOMER C WHERE S.CUSTOMER_ID = C.CUSTOMER_ID AND S.SALES_INVOICE = '" + selectedsalesinvoice + "'" +
                    " UNION " +
                    "SELECT S.SALES_INVOICE AS 'INVOICE', 'P-UMUM' AS 'CUSTOMER', DATE_FORMAT(S.SALES_DATE, '%d-%M-%Y') AS 'DATE', S.SALES_TOTAL AS 'TOTAL', 'RETAIL' AS 'GROUP' FROM SALES_HEADER S WHERE S.CUSTOMER_ID = 0 AND S.SALES_INVOICE = '" + selectedsalesinvoice + "'" +
                    "ORDER BY DATE ASC";
            }
            else if (originModuleID == globalConstants.DUMMY_TRANSACTION_TAX)
            {
                // GET DUMMY TAX DATA
                sqlCommand = "SELECT S.SALES_INVOICE AS 'INVOICE', C.CUSTOMER_FULL_NAME AS 'CUSTOMER',DATE_FORMAT(S.SALES_DATE, '%d-%M-%Y') AS 'DATE',S.SALES_TOTAL AS 'TOTAL', IF(C.CUSTOMER_GROUP=1,'RETAIL',IF(C.CUSTOMER_GROUP=2,'GROSIR','PARTAI')) AS 'GROUP' FROM SALES_HEADER_TAX S,MASTER_CUSTOMER C WHERE S.CUSTOMER_ID = C.CUSTOMER_ID AND S.SALES_INVOICE = '" + selectedsalesinvoiceTax + "'" +
                                        " UNION " +
                                        "SELECT S.SALES_INVOICE AS 'INVOICE', 'P-UMUM' AS 'CUSTOMER', DATE_FORMAT(S.SALES_DATE, '%d-%M-%Y') AS 'DATE', S.SALES_TOTAL AS 'TOTAL', 'RETAIL' AS 'GROUP' FROM SALES_HEADER_TAX S WHERE S.CUSTOMER_ID = 0 AND S.SALES_INVOICE = '" + selectedsalesinvoiceTax + "'" +
                                        "ORDER BY DATE ASC";
            }
            //else if (originModuleID == globalConstants.SALES_QUOTATION || originModuleID == globalConstants.EDIT_SALES_QUOTATION)
            //{
            //    sqlCommand = "SELECT S.SQ_INVOICE AS 'INVOICE', C.CUSTOMER_FULL_NAME AS 'CUSTOMER',DATE_FORMAT(S.SQ_ORDER_DATE, '%d-%M-%Y') AS 'DATE',S.SQ_TOTAL AS 'TOTAL', IF(C.CUSTOMER_GROUP=1,'RETAIL',IF(C.CUSTOMER_GROUP=2,'GROSIR','PARTAI')) AS 'GROUP' FROM SALES_QUOTATION_HEADER S,MASTER_CUSTOMER C WHERE S.CUSTOMER_ID = C.CUSTOMER_ID AND S.SQ_INVOICE = '" + selectedsalesinvoice + "'" +
            //                            " UNION " +
            //                            "SELECT S.SQ_INVOICE AS 'INVOICE', 'P-UMUM' AS 'CUSTOMER', DATE_FORMAT(S.SQ_ORDER_DATE, '%d-%M-%Y') AS 'DATE', S.SQ_TOTAL AS 'TOTAL', 'RETAIL' AS 'GROUP' FROM SALES_QUOTATION_HEADER S WHERE S.CUSTOMER_ID = 0 AND S.SQ_INVOICE = '" + selectedsalesinvoice + "'" +
            //                            "ORDER BY DATE ASC";
            //}

            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    rdr.Read();
                    customer = rdr.GetString("CUSTOMER");
                    tgl = rdr.GetString("DATE");
                    total = rdr.GetDouble("TOTAL");
                    group = rdr.GetString("GROUP");
                }
            }
            DS.mySqlClose();

            //1. PAYMENT METHOD
            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            rect.X = startX + 10;
            rect.Width = totrowwidth;
            //SET TO LEFT MARGIN
            sf.LineAlignment = StringAlignment.Near;
            sf.Alignment = StringAlignment.Near;

            if (originModuleID == globalConstants.SO_FULFILLMENT || subOriginModuleID == globalConstants.SO_FULFILLMENT)
            {
                ucapan = "PENGAMBILAN SEBAGIAN";
            }
            else
            {
                if (creditRadioButton.Checked)
                {
                    //ucapan = "JUAL CREDIT KEPADA";
                    ucapan = "PEMESANAN OLEH";
                }
                else
                {
                    ucapan = "JUAL TUNAI KEPADA";
                }
            }

            graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);

            //2. CUSTOMER NAME
            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "PELANGGAN : " + customer + " [" + group + "]";
            graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            rect.X = startX;
            rect.Width = totrowwidth;
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            graphics.DrawString(underLine, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            rect.Width = totrowwidth;

            //if (originModuleID == globalConstants.EDIT_SALES_QUOTATION || originModuleID == globalConstants.SALES_QUOTATION)
            if (SOisPesanan || originModuleID == globalConstants.SO_FULFILLMENT || subOriginModuleID == globalConstants.SO_FULFILLMENT)
                ucapan = "BUKTI PEMESANAN";
            else
                ucapan = "BUKTI PEMBAYARAN";
            graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset + offset_plus;
            rect.Y = startY + Offset;
            rect.X = startX + 10;
            rect.Width = totrowwidth;
            sf.LineAlignment = StringAlignment.Near;
            sf.Alignment = StringAlignment.Near;
            if (originModuleID != globalConstants.DUMMY_TRANSACTION_TAX)
                ucapan = "NO. NOTA : " + selectedsalesinvoice;
            else
                ucapan = "NO. NOTA : " + selectedsalesinvoiceTax;
            graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            //if (originModuleID == globalConstants.SALES_QUOTATION || originModuleID == globalConstants.EDIT_SALES_QUOTATION)
            if (SOisPesanan)
                ucapan = "TGL PESAN: " + tgl;
            else
                ucapan = "TANGGAL  : " + tgl;
            graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            string nama = "";
            loadNamaUser(gutil.getUserID(), out nama);
            ucapan = "OPERATOR : " + nama;
            graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            rect.X = startX;
            rect.Width = totrowwidth;
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            graphics.DrawString(underLine, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), rect, sf);

            //DETAIL PENJUALAN
            sf.LineAlignment = StringAlignment.Near;
            sf.Alignment = StringAlignment.Near;
            //read sales_detail

            //DS.mySqlConnect();
            string product_id = "";
            string product_name = "";
            double total_qty = 0;
            double product_qty = 0;
            double product_price = 0;
            float startRightX = totrowwidth - colxwidth - startX;

            if (originModuleID == 0 || originModuleID == globalConstants.EDIT_SALES_ORDER || originModuleID == globalConstants.SO_FULFILLMENT || originModuleID == globalConstants.COPY_NOTA)
            {
                // NORMAL TRANSACTION
                sqlCommand = "SELECT S.ID, S.PRODUCT_ID AS 'P-ID', P.PRODUCT_NAME AS 'NAME', S.PRODUCT_QTY AS 'QTY',ROUND(S.SALES_SUBTOTAL/S.PRODUCT_QTY) AS 'PRICE', S.SALES_SUBTOTAL  FROM sales_detail S, master_product P WHERE S.PRODUCT_ID=P.PRODUCT_ID AND S.SALES_INVOICE='" + selectedsalesinvoice + "'";
            }
            else if (originModuleID == globalConstants.DUMMY_TRANSACTION_TAX)
            {
                // GET DUMMY DATA
                sqlCommand = "SELECT S.ID, S.PRODUCT_ID AS 'P-ID', P.PRODUCT_NAME AS 'NAME', S.PRODUCT_QTY AS 'QTY',ROUND(S.SALES_SUBTOTAL/S.PRODUCT_QTY) AS 'PRICE', S.SALES_SUBTOTAL FROM sales_detail_tax S, master_product P WHERE S.PRODUCT_ID=P.PRODUCT_ID AND S.SALES_INVOICE='" + selectedsalesinvoiceTax + "'";
            }
            //else if (originModuleID == globalConstants.SALES_QUOTATION || originModuleID == globalConstants.EDIT_SALES_QUOTATION)
            //{
            //    sqlCommand = "SELECT S.ID, S.PRODUCT_ID AS 'P-ID', P.PRODUCT_NAME AS 'NAME', S.PRODUCT_QTY AS 'QTY', S.PRODUCT_SALES_PRICE AS 'PRICE', S.SQ_SUBTOTAL as SALES_SUBTOTAL FROM sales_quotation_detail S, master_product P WHERE S.PRODUCT_ID=P.PRODUCT_ID AND S.SQ_INVOICE='" + selectedsalesinvoice + "'";
            //}

            using (rdr = DS.getData(sqlCommand))//+ "group by s.product_id") )
            {
                if (rdr.HasRows)
                {
                    int i = 0;
                    while (rdr.Read())
                    {
                        product_id = rdr.GetString("P-ID");
                        product_name = rdr.GetString("NAME");
                        product_qty = rdr.GetDouble("QTY");
                        product_price = rdr.GetDouble("PRICE");
                        double subtotal = rdr.GetDouble("SALES_SUBTOTAL");
                        if (i == 0)
                        {
                            Offset = Offset + add_offset;
                        }
                        else
                        {
                            i = 1;
                            Offset = Offset + add_offset + offset_plus;
                        }
                        rect.Y = startY + Offset;
                        rect.X = startX + 10;
                        rect.Width = totrowwidth;
                        sf.LineAlignment = StringAlignment.Near;
                        sf.Alignment = StringAlignment.Near;
                        //ucapan = product_qty + "X " + product_name;
                        ucapan = product_name;
                        if (ucapan.Length > 30)
                        {
                            ucapan = ucapan.Substring(0, 30); //maximum 30 character
                        }
                        //
                        graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                                 new SolidBrush(Color.Black), rect, sf);

                        //new line
                        Offset = Offset + add_offset;
                        rect.Y = startY + Offset;
                        rect.X = startX + 20;
                        rect.Width = totrowwidth;
                        sf.LineAlignment = StringAlignment.Near;
                        sf.Alignment = StringAlignment.Near;
                        ucapan = product_qty + "X";
                        graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                                 new SolidBrush(Color.Black), rect, sf);
                        //

                        if (originModuleID != globalConstants.SO_FULFILLMENT && subOriginModuleID != globalConstants.SO_FULFILLMENT)
                        {
                            rect.Y = startY + Offset;
                            rect.X = startX + 50;
                            rect.Width = totrowwidth;
                            sf.LineAlignment = StringAlignment.Near;
                            sf.Alignment = StringAlignment.Near;
                            ucapan = product_price.ToString("C2", culture);
                            graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                                     new SolidBrush(Color.Black), rect, sf);

                            rectright.X = startRightX - 35;
                            rectright.Y = rect.Y;

                            rectright.Width = colxwidth;// - 5;
                            sf.LineAlignment = StringAlignment.Far;
                            sf.Alignment = StringAlignment.Far;
                            ucapan = subtotal.ToString("C2", culture);//" Rp." + product_price;
                            graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                                     new SolidBrush(Color.Black), rectright, sf);
                        }

                    }
                }
            }
            DS.mySqlClose();

            if (originModuleID != globalConstants.SO_FULFILLMENT && subOriginModuleID != globalConstants.SO_FULFILLMENT)
            {
                Offset = Offset + add_offset;
                rect.Y = startY + Offset;
                rect.X = startX;
                rect.Width = totrowwidth;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                graphics.DrawString(underLine, new Font("Courier New", 9),
                         new SolidBrush(Color.Black), rect, sf);

                Offset = Offset + add_offset;
                rect.Y = startY + Offset;
                rect.X = rect.X + 70;//95;
                //rectcenter.X = rectcenter.X + 15;
                //rectcenter.Width = colxwidth;
                sf.LineAlignment = StringAlignment.Near;
                sf.Alignment = StringAlignment.Near;
                ucapan = "JUMLAH  : " + total.ToString("C2", culture);
                //rectcenter.Y = rect.Y;
                graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                         new SolidBrush(Color.Black), rect, sf);
                //sf.LineAlignment = StringAlignment.Far;
                //sf.Alignment = StringAlignment.Far;
                //ucapan = total.ToString("C2", culture);
                //rectright.X = rectright.X - 5;
                //rectright.Y = rect.Y-2;
                //graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                //         new SolidBrush(Color.Black), rectright, sf);

                Offset = Offset + add_offset;
                rect.Y = startY + Offset;
                //rectcenter.X = rectcenter.X + 15;
                //rectcenter.Width = colxwidth;
                sf.LineAlignment = StringAlignment.Near;
                sf.Alignment = StringAlignment.Near;
                ucapan = "Disc    : (" + discValue.ToString("C2", culture) + ")";

                graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                         new SolidBrush(Color.Black), rect, sf);
                sf.LineAlignment = StringAlignment.Far;
                sf.Alignment = StringAlignment.Far;

                //if (originModuleID == globalConstants.SQ_TO_SO)
                if (SOisPesanan)
                {
                    Offset = Offset + add_offset;
                    rect.Y = startY + Offset;
                    //rectcenter.X = rectcenter.X + 15;
                    //rectcenter.Width = colxwidth;
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Near;
                    //ucapan = "DP      : (" + previousSQ_DP.ToString("C2", culture) + ")";
                    ucapan = "DP      : (" + bayarAmount.ToString("C2", culture) + ")";

                    graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                             new SolidBrush(Color.Black), rect, sf);
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Far;
                }

                if (cashRadioButton.Checked == true)
                {
                    Offset = Offset + add_offset;
                    rect.Y = startY + Offset;
                    //rectcenter.X = rectcenter.X + 15;
                    //rectcenter.Width = colxwidth;
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Near;

                    double jumlahBayar = 0;

                    jumlahBayar = Convert.ToDouble(bayarAmount);

                    //if (originModuleID == globalConstants.SALES_QUOTATION || originModuleID == globalConstants.EDIT_SALES_QUOTATION)
                    //    ucapan = "DP      : (" + jumlahBayar.ToString("C2", culture) + ")";
                    //else
                    ucapan = "TUNAI   : (" + jumlahBayar.ToString("C2", culture) + ")";
                    //rectcenter.Y = rect.Y;
                    graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                             new SolidBrush(Color.Black), rect, sf);
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Far;

                    //ucapan = jumlahBayar.ToString("C2", culture);//"Rp." + String.Format("{0:C2}", bayarTextBox.Text);
                    //rectright.X = rectright.X - 5;
                    //rectright.Y = rect.Y-2;
                    //graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                    //         new SolidBrush(Color.Black), rectright, sf);

                    Offset = Offset + add_offset;
                    rect.Y = startY + Offset;
                    //rectcenter.X = rectcenter.X + 15;
                    //rectcenter.Width = colxwidth;
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Near;
                    ucapan = "KEMBALI : " + uangKembaliTextBox.Text + ",00";
                    //rectcenter.Y = rect.Y;
                    graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                             new SolidBrush(Color.Black), rect, sf);
                    //sf.LineAlignment = StringAlignment.Far;
                    //sf.Alignment = StringAlignment.Far;
                    //ucapan = uangKembaliTextBox.Text;
                    //rectright.X = rectright.X - 5;
                    //rectright.Y = rect.Y-2;
                    //graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                    //         new SolidBrush(Color.Black), rectright, sf);
                }
            }

            if (originModuleID == 0 || originModuleID == globalConstants.EDIT_SALES_ORDER)
            {
                // NORMAL TRANSACTION
                sqlCommand = "SELECT IFNULL(SUM(PRODUCT_QTY), 0) FROM SALES_DETAIL S, MASTER_PRODUCT P WHERE S.PRODUCT_ID = P.PRODUCT_ID AND S.SALES_INVOICE = '" + selectedsalesinvoice + "'";
            }
            else if (originModuleID == globalConstants.DUMMY_TRANSACTION_TAX)
            {
                // GET DUMMY DATA
                sqlCommand = "SELECT IFNULL(SUM(PRODUCT_QTY), 0) FROM SALES_DETAIL_TAX S, MASTER_PRODUCT P WHERE S.PRODUCT_ID = P.PRODUCT_ID AND S.SALES_INVOICE = '" + selectedsalesinvoiceTax + "'";
            }
            //else if (originModuleID == globalConstants.SALES_QUOTATION || originModuleID == globalConstants.EDIT_SALES_QUOTATION)
            //{
            //    // NORMAL TRANSACTION
            //    sqlCommand = "SELECT IFNULL(SUM(PRODUCT_QTY), 0) FROM SALES_QUOTATION_DETAIL S, MASTER_PRODUCT P WHERE S.PRODUCT_ID = P.PRODUCT_ID AND S.SQ_INVOICE = '" + selectedsalesinvoice + "'";
            //}

            total_qty = Convert.ToDouble(DS.getDataSingleValue(sqlCommand));

            Offset = Offset + add_offset + offset_plus + offset_plus; ;
            rect.Y = startY + Offset;
            rect.X = startX + 10;
            rect.Width = totrowwidth;
            sf.LineAlignment = StringAlignment.Near;
            sf.Alignment = StringAlignment.Near;
            ucapan = "TOTAL BARANG : " + total_qty;
            graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);
            //eNd of content

            //FOOTER

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            rect.X = startX;
            rect.Width = totrowwidth;
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            graphics.DrawString(underLine, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "TERIMA KASIH ATAS KUNJUNGAN ANDA";
            graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "MAAF BARANG YANG SUDAH DIBELI";
            graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "TIDAK DAPAT DITUKAR/ DIKEMBALIKKAN";
            graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);
            //end of footer
        }

        private void tempoMaskedTextBox_Enter(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                tempoMaskedTextBox.SelectAll();
            });
        }

        private void discJualMaskedTextBox_Enter(object sender, EventArgs e)
        {
            discJualMaskedTextBox.Text = discValue.ToString();
            BeginInvoke((Action)delegate
            {
                discJualMaskedTextBox.SelectAll();
            });
        }

        private void bayarTextBox_Enter(object sender, EventArgs e)
        {
            bayarTextBox.Text = bayarAmount.ToString();
            BeginInvoke((Action)delegate
            {
                bayarTextBox.SelectAll();
            });
        }

        private void cashierDataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //var cell = cashierDataGridView[e.ColumnIndex, e.RowIndex];
            //DataGridViewRow selectedRow = cashierDataGridView.Rows[e.RowIndex];

            //if (isLoading)
            //    return;

            //if (cell.OwningColumn.Name == "productID")
            //{
            //    if (null != cell.Value)
            //    {
            //        if (cell.Value.ToString().Length > 0)
            //        {
            //            updateSomeRowContents(selectedRow, e.RowIndex, cell.Value.ToString());
            //        }
            //        else
            //        {
            //            clearUpSomeRowContents(selectedRow, e.RowIndex);
            //        }
            //    }
            //}
            //else if (cell.OwningColumn.Name == "productName")
            //{
            //    if (null != cell.Value)
            //    {
            //        if (cell.Value.ToString().Length > 0)
            //        {
            //            updateSomeRowContents(selectedRow, e.RowIndex, cell.Value.ToString(), false);
            //        }
            //        else
            //        {
            //            clearUpSomeRowContents(selectedRow, e.RowIndex);
            //        }
            //    }
            //}

        }

        private void cashierDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

            updateRowNumber();

            //if (cashierDataGridView.Rows.Count > 0)
            //{
            //    cashierDataGridView.Rows[cashierDataGridView.Rows.Count - 1].Cells["disc1"].Value = "0";
            //    cashierDataGridView.Rows[cashierDataGridView.Rows.Count - 1].Cells["disc2"].Value = "0";
            //    cashierDataGridView.Rows[cashierDataGridView.Rows.Count - 1].Cells["discRP"].Value = "0";
            //}
            //if (cashierDataGridView.ColumnCount > 1)
            //    cashierDataGridView.CurrentCell = cashierDataGridView.Rows[cashierDataGridView.Rows.Count-1].Cells["productID"];

            salesQty.Add("0");
            disc1.Add("0");
            disc2.Add("0");
            discRP.Add("0");
            productPriceList.Add("0");
            jumlahList.Add("0");

            //if (cashierDataGridView.Focused)
            //    if (navKeyRegistered)
            //    { 
            //        unregisterNavigationKey();
            //        registerDelKey();
            //    }
        }

        private void bayarTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            { 
                if (originModuleID != globalConstants.COPY_NOTA)
                {
                    bayarAmount = Convert.ToDouble(bayarTextBox.Text);
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : TRIGGER SAVE INVOICE FROM BAYAR TEXTBOX");

                    saveAndPrintOutInvoice();
                }
            }
        }

        private void bayarTextBox_Leave(object sender, EventArgs e)
        {
            isLoading = true;
            bayarAmount = Convert.ToDouble(bayarTextBox.Text);
            bayarTextBox.Text = bayarAmount.ToString("C0", culture);
            isLoading = false;

        }

        private void discJualMaskedTextBox_Leave(object sender, EventArgs e)
        {
            isLoading = true;
            discValue = Convert.ToDouble(discJualMaskedTextBox.Text);
            discJualMaskedTextBox.Text = discValue.ToString("C0", culture);
            isLoading = false;
        }

        private void discJualMaskedTextBox_TextChanged(object sender, EventArgs e)
        {
            string tempString;

            if (isLoading)
                return;

            isLoading = true;
            if (discJualMaskedTextBox.Text.Length == 0)
            {
                // IF TEXTBOX IS EMPTY, SET THE VALUE TO 0 AND EXIT THE CHECKING
                discAmountText = "0";
                discJualMaskedTextBox.Text = "0";

                discJualMaskedTextBox.SelectionStart = discJualMaskedTextBox.Text.Length;
                isLoading = false;

                return;
            }
            // CHECKING TO PREVENT PREFIX "0" IN A NUMERIC INPUT WHILE ALLOWING A DECIMAL VALUE STARTED WITH "0"
            else if (discJualMaskedTextBox.Text.IndexOf('0') == 0 && discJualMaskedTextBox.Text.Length > 1 && discJualMaskedTextBox.Text.IndexOf("0.") < 0)
            {
                tempString = discJualMaskedTextBox.Text;
                discJualMaskedTextBox.Text = tempString.Remove(0, 1);
            }

            if (gutil.matchRegEx(discJualMaskedTextBox.Text, globalUtilities.REGEX_NUMBER_ONLY))
                discAmountText = discJualMaskedTextBox.Text;
            else
                discJualMaskedTextBox.Text = discAmountText;

            discJualMaskedTextBox.SelectionStart = discJualMaskedTextBox.Text.Length;
            isLoading = false;

            double totalAfterDisc = 0;

            if (discJualMaskedTextBox.Text.Length > 0)
            {
                totalAfterDisc = globalTotalValue - Convert.ToDouble(discJualMaskedTextBox.Text);
                discValue = Convert.ToDouble(discJualMaskedTextBox.Text);

                if (originModuleID != globalConstants.EDIT_SALES_ORDER)
                    totalLabel.Text = (globalTotalValue - discValue - bayarAmount).ToString("C0", culture);
                else
                    totalLabel.Text = (globalTotalValue - discValue).ToString("C0", culture);
            }
            else
            {
                totalAfterDisc = globalTotalValue;
                discValue = 0;
                if (originModuleID != globalConstants.EDIT_SALES_ORDER)
                    totalLabel.Text = (globalTotalValue - discValue - bayarAmount).ToString("C0", culture);
                else
                    totalLabel.Text = (globalTotalValue - discValue).ToString("C0", culture);
            }

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : discJualMaskedTextBox_Validating, totalAfterDisc [" + totalAfterDisc + "]");
            totalAfterDiscTextBox.Text = totalAfterDisc.ToString("C0", culture);
            calculateChangeValue();
        }

        private void discJualMaskedTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                bayarTextBox.Focus();
        }

        private void cashierDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((cashierDataGridView.Columns[e.ColumnIndex].Name == "productPrice" || cashierDataGridView.Columns[e.ColumnIndex].Name == "qty" || cashierDataGridView.Columns[e.ColumnIndex].Name == "discRP" || cashierDataGridView.Columns[e.ColumnIndex].Name == "jumlah")
                && e.RowIndex != this.cashierDataGridView.NewRowIndex && null != e.Value)
            {
                isLoading = true;
                double d = double.Parse(e.Value.ToString());
                e.Value = d.ToString(globalUtilities.CELL_FORMATTING_NUMERIC_FORMAT);
                isLoading = false;    
            }
        }

        private void cashierDataGridView_Enter(object sender, EventArgs e)
        {
            if (navKeyRegistered)
            {
                unregisterNavigationKey();
            }
            registerDelKey();
        }

        private void cashierDataGridView_Leave(object sender, EventArgs e)
        {
            if (!navKeyRegistered)
            {
                registerNavigationKey();
            }

            unregisterDelKey();
        }

        private void customerComboBox_Enter(object sender, EventArgs e)
        {
            unregisterNavigationKey();
        }

        private void customerComboBox_Leave(object sender, EventArgs e)
        {
            registerNavigationKey();
        }

        private void paymentComboBox_Enter(object sender, EventArgs e)
        {
            unregisterNavigationKey();
        }

        private void paymentComboBox_Leave(object sender, EventArgs e)
        {
            registerNavigationKey();
        }

        private void cashierDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var cell = cashierDataGridView[e.ColumnIndex, e.RowIndex];
            DataGridViewRow selectedRow = cashierDataGridView.Rows[e.RowIndex];
            string productID = "";
            string cellValue = "";
            string previousInput = "";
            int rowSelectedIndex = e.RowIndex;
            string tempString = "";
            double productPrice = 0;
            double subTotal = 0;
            string columnName = "";

            if (isLoading)
                return;

           columnName = cell.OwningColumn.Name;
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : cashierDataGridView_CellValueChanged [" + columnName + "]");

            if (null != selectedRow.Cells[columnName].Value)
                cellValue = selectedRow.Cells[columnName].Value.ToString();
            else
                cellValue = "";

            if (columnName == "productName")
            {
                if (cellValue.Length > 0)
                {
                    //updateSomeRowContents(selectedRow, rowSelectedIndex, cellValue, false);
                    //int pos = cashierDataGridView.CurrentCell.RowIndex;

                    //if (pos > 0)
                    //    cashierDataGridView.CurrentCell = cashierDataGridView.Rows[pos - 1].Cells["qty"];

                    //forceUpOneLevel = true;
                }
            }
            else if (columnName == "qty" || columnName == "productPrice" ||
                    columnName == "disc1" || columnName == "disc2" || columnName == "discRP"
                    )
            {
                if (cellValue.Length <= 0)
                {
                    // IF TEXTBOX IS EMPTY, DEFAULT THE VALUE TO 0 AND EXIT THE CHECKING
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : cashierDataGridView_CellValueChanged, empty texbox, reset [" + columnName + "] value to 0");
                    isLoading = true;

                    switch (columnName)
                    {
                        case "qty":
                            salesQty[rowSelectedIndex] = "0";
                            break;
                        case "disc1":
                            disc1[rowSelectedIndex] = "0";
                            break;
                        case "disc2":
                            disc2[rowSelectedIndex] = "0";
                            break;
                        case "discRP":
                            discRP[rowSelectedIndex] = "0";
                            break;
                        case "productPrice":
                            productPriceList[rowSelectedIndex] = "0";
                            break;
                    }

                    selectedRow.Cells[columnName].Value = "0";

                    if (columnName == "qty" || columnName == "productPrice")
                    {
                        // reset subTotal Value and recalculate total
                        selectedRow.Cells["jumlah"].Value = 0;
                        jumlahList[rowSelectedIndex] = "0";
                    }
                    else
                    {
                        productPrice = Convert.ToDouble(selectedRow.Cells["productPrice"].Value);

                        subTotal = calculateSubTotal(rowSelectedIndex, productPrice);
                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : TextBox_TextChanged, subtotal value [" + subTotal + "]");
                        selectedRow.Cells["jumlah"].Value = subTotal;
                        jumlahList[rowSelectedIndex] = subTotal.ToString();
                    }

                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : TextBox_TextChanged, recalculate total value");
                    calculateTotal();

                    isLoading = false;

                    return;
                }

                if (null != selectedRow.Cells["productID"].Value)
                    productID = selectedRow.Cells["productID"].Value.ToString();

                switch (columnName)
                {
                    case "qty":
                        previousInput = salesQty[rowSelectedIndex];
                        break;
                    case "disc1":
                        previousInput = disc1[rowSelectedIndex];
                        break;
                    case "disc2":
                        previousInput = disc2[rowSelectedIndex];
                        break;
                    case "discRP":
                        previousInput = discRP[rowSelectedIndex];
                        break;
                    case "productPrice":
                        previousInput = productPriceList[rowSelectedIndex];
                        break;
                }

                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : cashierDataGridView_CellValueChanged, previousInput [" + previousInput + "]");

                if (previousInput == cellValue)
                    return;

                isLoading = true;
                if (previousInput == "0")
                {
                    tempString = cellValue;
                    if (tempString.IndexOf('0') == 0 && tempString.Length > 1 && tempString.IndexOf("0.") < 0)
                        selectedRow.Cells[columnName].Value = tempString.Remove(tempString.IndexOf('0'), 1);

                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : cashierDataGridView_CellValueChanged, cellValue [" + cellValue + "]");
                }

                if (gutil.matchRegEx(cellValue, globalUtilities.REGEX_NUMBER_WITH_2_DECIMAL)
                    && (cellValue.Length > 0 && cellValue != ".")
                    )
                {
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : cashierDataGridView_CellValueChanged, dataGridViewTextBoxEditingControl.Text pass REGEX Checking");

                    switch (columnName)
                    {
                        case "qty":
                            if (stockIsEnough(productID, Convert.ToDouble(cellValue)))
                                salesQty[rowSelectedIndex] = cellValue;
                            else
                                selectedRow.Cells["qty"].Value = salesQty[rowSelectedIndex];
                            break;
                        case "disc1":
                            disc1[rowSelectedIndex] = cellValue;
                            break;
                        case "disc2":
                            disc2[rowSelectedIndex] = cellValue;
                            break;
                        case "discRP":
                            discRP[rowSelectedIndex] = cellValue;
                            break;
                        case "productPrice":
                            productPriceList[rowSelectedIndex] = cellValue;
                            break;
                    }
                }
                else
                {
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : cashierDataGridView_CellValueChanged, dataGridViewTextBoxEditingControl.Text did not pass REGEX Checking");
                    selectedRow.Cells[columnName].Value = previousInput;
                }

                productPrice = Convert.ToDouble(productPriceList[rowSelectedIndex]);

                subTotal = calculateSubTotal(rowSelectedIndex, productPrice);
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : cashierDataGridView_CellValueChanged, subtotal value [" + subTotal + "]");
                selectedRow.Cells["jumlah"].Value = subTotal;
                jumlahList[rowSelectedIndex] = subTotal.ToString();

                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : cashierDataGridView_CellValueChanged, attempt to calculate total value");
                calculateTotal();
            }

            isLoading = false;
        }

        private void cashierDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (cashierDataGridView.IsCurrentCellDirty)
            {
                cashierDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void cashierDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            cashierDataGridView.SuspendLayout();

            if (navKeyRegistered)
            {
                unregisterNavigationKey();
            }

            if (!delKeyRegistered)
                registerDelKey();
        }

        private void cashierDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            cashierDataGridView.ResumeLayout();
        }

        private void cashierDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
  
        }

        private void rejectSQ(string selectedSO)
        {
            string sqInvoice = "";
            string sqlCommand = "";
            MySqlException internalEX = null;

            sqInvoice = selectedsalesinvoice;

            DS.beginTransaction();

            try
            {
                DS.mySqlConnect();

                // UPDATE SALES QUOTATION TABLE
                sqlCommand = "UPDATE SALES_QUOTATION_HEADER SET SQ_APPROVED = -1 WHERE SQ_INVOICE = '" + sqInvoice + "'";
                if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                    throw internalEX;

                DS.commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void approveSQ(string selectedSO)
        {
            if (saveAndPrintOutInvoice()) // SAVE ALL THE LATEST CHANGES
            { 
                setQuotationToApproved(selectedSO);
                approvalButton.Enabled = false;
                rejectButton.Enabled = false;
            }
        }

        private void approvalButton_Click(object sender, EventArgs e)
        {
            approveSQ(selectedsalesinvoice);
        }

        private void rejectButton_Click(object sender, EventArgs e)
        {
            rejectSQ(selectedsalesinvoice);
            approvalButton.Enabled = false;
            rejectButton.Enabled = false;
        }

        private void selfCollectCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (deliveredCheckbox.Checked == true)
                addressTextBox.Visible = true;
            else
                addressTextBox.Visible = false;
        }

        private void DPTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void totalAfterDiscTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void jobStartDateTimePicker_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
