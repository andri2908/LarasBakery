using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlphaSoft
{
    public partial class newOrderForm : Form
    {
        Form parentForm = null;

        public newOrderForm (Form originForm)
        {
            InitializeComponent();
            parentForm = originForm;
        }

        private void closeForm()
        {
            adminForm originForm;
            this.Hide();

            originForm = (adminForm)parentForm;
            originForm.setNewOrderFormExist(false);

            dataSalesInvoice displayedForm = new dataSalesInvoice(globalConstants.DELIVERY_ORDER);
            displayedForm.ShowDialog(this);

            this.Close();
        }

        private void newOrderForm_Click(object sender, EventArgs e)
        {
            closeForm();
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            closeForm();
        }

        private void newOrderForm_DoubleClick(object sender, EventArgs e)
        {
            closeForm();
        }

        private void newOrderForm_Load(object sender, EventArgs e)
        {

        }
    }
}
