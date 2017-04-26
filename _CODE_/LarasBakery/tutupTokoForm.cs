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

namespace AlphaSoft
{
    public partial class tutupTokoForm : Form
    {
        private globalUtilities gUtil = new globalUtilities();
        private Data_Access DS = new Data_Access();
        private CultureInfo culture = new CultureInfo("id-ID");

        public tutupTokoForm()
        {
            InitializeComponent();
        }

        private void dailyStockTake()
        {
            dailyStockTakeDetailForm displayedForm = new dailyStockTakeDetailForm();
            displayedForm.ShowDialog(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // LOG OUT CURRENT CASHIER


            // STOCK TAKE HARIAN
            dailyStockTake();

            // LAPORAN PENJUALAN

            // LAPORAN SUMMARY

            // LAPORAN PENERIMAAN

            // LAPORAN PENGELUARAN TUNAI

            // JURNAL

            // CLEAR TRANSAKSI LUNAS
        }
    }
}
