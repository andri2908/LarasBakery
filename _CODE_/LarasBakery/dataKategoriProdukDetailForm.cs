﻿using System;
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

using Hotkeys;

namespace AlphaSoft
{
    public partial class dataKategoriProdukDetailForm : Form
    {
        private int originModuleID = 0;
        private int selectedCategoryID = 0;
        private globalUtilities gutil = new globalUtilities();
        private int options = 0;
        Data_Access DS = new Data_Access();

        private Hotkeys.GlobalHotkey ghk_UP;
        private Hotkeys.GlobalHotkey ghk_DOWN;

        public dataKategoriProdukDetailForm()
        {
            InitializeComponent();
        }

        public dataKategoriProdukDetailForm(int moduleID)
        {
            InitializeComponent();

            originModuleID = moduleID;            
        }

        public dataKategoriProdukDetailForm(int moduleID, int categoryID)
        {
            InitializeComponent();

            originModuleID = moduleID;
            selectedCategoryID = categoryID;
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
        }

        private void unregisterGlobalHotkey()
        {
            ghk_UP.Unregister();
            ghk_DOWN.Unregister();
        }


        private void loadDataKategori()
        {
            MySqlDataReader rdr;
            DataTable dt = new DataTable();

            DS.mySqlConnect();

            using (rdr = DS.getData("SELECT * FROM MASTER_CATEGORY WHERE CATEGORY_ID =  " + selectedCategoryID))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        categoryNameTextBox.Text = rdr.GetString("CATEGORY_NAME");
                        categoryDescriptionTextBox.Text = rdr.GetString("CATEGORY_DESCRIPTION");

                        if (rdr.GetString("CATEGORY_ACTIVE").Equals("1"))
                            nonAktifCheckbox.Checked = false;
                        else
                            nonAktifCheckbox.Checked = true;
                    }
                }
            }
        }

        private void dataKategoriProdukDetailForm_Load(object sender, EventArgs e)
        {
            int userAccessOption = 0;
            Button[] arrButton = new Button[2];
            userAccessOption = DS.getUserAccessRight(globalConstants.MENU_KATEGORI, gutil.getUserGroupID());

            if (originModuleID == globalConstants.NEW_CATEGORY)
            {
                if (userAccessOption != 2 && userAccessOption != 6)
                {
                    gutil.setReadOnlyAllControls(this);
                }
            }
            else if (originModuleID == globalConstants.EDIT_CATEGORY)
            {
                if (userAccessOption != 4 && userAccessOption != 6)
                {
                    gutil.setReadOnlyAllControls(this);
                }
            }

            arrButton[0] = saveButton;
            arrButton[1] = button1;
            gutil.reArrangeButtonPosition(arrButton, saveButton.Top, this.Width);

            gutil.reArrangeTabOrder(this);
            categoryNameTextBox.Select();
        }

        private bool dataValidated()
        {
            if (categoryNameTextBox.Text.Trim().Equals(""))
            {
                errorLabel.Text = "NAMA KATEGORI TIDAK BOLEH KOSONG";
                return false;
            }

            if (categoryDescriptionTextBox.Text.Trim().Equals(""))
            {
                errorLabel.Text = "DESKRIPSI KATEGORI TIDAK BOLEH KOSONG";
                return false;
            }

            return true;
        }

        private bool saveDataTransaction()
        {
            bool result = false;
            string sqlCommand = "";
            MySqlException internalEX = null;
            string categoryID = "";

            string categoryName = MySqlHelper.EscapeString(categoryNameTextBox.Text.Trim());
            string categoryDesc = MySqlHelper.EscapeString(categoryDescriptionTextBox.Text.Trim());           

            byte categoryStatus = 0;

            if (nonAktifCheckbox.Checked)
                categoryStatus = 0;
            else
                categoryStatus = 1;

            DS.beginTransaction();

            try
            {
                DS.mySqlConnect();

                switch (originModuleID)
                {
                    case globalConstants.NEW_CATEGORY:
                        //if (gutil.isSS_ServerApp() == 1)
                        //{
                        //    categoryID = gutil.getAutoGenerateID("MASTER_CATEGORY", "", "", "CATEGORY_ID");
                        //    sqlCommand = "INSERT INTO MASTER_CATEGORY (CATEGORY_ID, CATEGORY_NAME, CATEGORY_DESCRIPTION, CATEGORY_ACTIVE) " +
                        //                "VALUES ('" + categoryID + "', '" + categoryName + "', '" + categoryDesc + "', " + categoryStatus + ")";
                        //}
                        //else
                            sqlCommand = "INSERT INTO MASTER_CATEGORY (CATEGORY_NAME, CATEGORY_DESCRIPTION, CATEGORY_ACTIVE) " +
                                            "VALUES ('" + categoryName + "', '" + categoryDesc + "', " + categoryStatus + ")";
                        gutil.saveSystemDebugLog(globalConstants.MENU_KATEGORI, "ADD NEW CATEGORY [" + categoryName + "]");
                        break;
                    case globalConstants.EDIT_CATEGORY:
                        sqlCommand = "UPDATE MASTER_CATEGORY SET " +
                                            "CATEGORY_NAME = '" + categoryName + "', " +
                                            "CATEGORY_DESCRIPTION = '" + categoryDesc + "', " +
                                            "CATEGORY_ACTIVE = " + categoryStatus + " " +
                                            "WHERE CATEGORY_ID = " + selectedCategoryID;
                        gutil.saveSystemDebugLog(globalConstants.MENU_KATEGORI, "UPDATE CATEGORY [" + selectedCategoryID + "]");
                        break;
                }

                if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                    throw internalEX;

                DS.commit();
                result = true;
            }
            catch (Exception e)
            {
                gutil.saveSystemDebugLog(globalConstants.MENU_KATEGORI, "EXCEPTION THROWN [" + e.Message + "]");
                try
                {
                    DS.rollBack();
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

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                switch (originModuleID)
                {
                    case globalConstants.NEW_CATEGORY:
                        gutil.saveUserChangeLog(globalConstants.MENU_KATEGORI, globalConstants.CHANGE_LOG_INSERT, "INSERT NEW CATEGORY [" + categoryNameTextBox.Text + "]");
                        break;
                    case globalConstants.EDIT_CATEGORY:
                        if (nonAktifCheckbox.Checked == true)
                            gutil.saveUserChangeLog(globalConstants.MENU_KATEGORI, globalConstants.CHANGE_LOG_UPDATE, "UPDATE CATEGORY [" + categoryNameTextBox.Text + "] STATUS CATEGORY NON-AKTIF");
                        else
                            gutil.saveUserChangeLog(globalConstants.MENU_KATEGORI, globalConstants.CHANGE_LOG_UPDATE, "UPDATE CATEGORY [" + categoryNameTextBox.Text + "] STATUS CATEGORY AKTIF");
                        break;
                }
                gutil.showSuccess(options);
                gutil.ResetAllControls(this);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gutil.ResetAllControls(this);
        }

        private void dataKategoriProdukDetailForm_Activated(object sender, EventArgs e)
        {
            errorLabel.Text = "";
            switch (originModuleID)
            {
                case globalConstants.NEW_CATEGORY:
                    options = gutil.INS;
                    nonAktifCheckbox.Enabled = false;
                    break;
                case globalConstants.EDIT_CATEGORY:
                    nonAktifCheckbox.Enabled = true;
                    options = gutil.UPD;
                    loadDataKategori();
                    break;
            }

            registerGlobalHotkey();
        }

        private void dataKategoriProdukDetailForm_Deactivate(object sender, EventArgs e)
        {
            unregisterGlobalHotkey();
        }
    }
}
