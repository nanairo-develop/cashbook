using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cashbook
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }

        private void PurchaseDetail_Click(object sender, EventArgs e)
        {
            FormPurchaseDetail formPurchaseDetail = new();
            formPurchaseDetail.Show();
        }

        private void PurchaseList_Click(object sender, EventArgs e)
        {
            FormPurchaseList formPurchaseList = new();
            formPurchaseList.Show();
        }

        private void OfficeList_Click(object sender, EventArgs e)
        {
            FormOfficeList formOfficeList = new();
            formOfficeList.Show();
        }
    }
}
