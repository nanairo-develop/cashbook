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
    public partial class FormOfficeDetail : Form
    {
        public FormOfficeDetail()
        {
            InitializeComponent();
            Insert.Enabled = true;
            Change.Enabled = false;

        }
        public FormOfficeDetail(int id, string officeName, object displayOrder)
        {
            InitializeComponent();
            Id.Text = id.ToString();
            OfficeName.Text = officeName;
            Order.Text = displayOrder.ToString();
            Insert.Enabled = false;
            Change.Enabled = true;
        }

        private void FormOfficeDetail_Load(object sender, EventArgs e)
        {

        }

        private void Insert_Click(object sender, EventArgs e)
        {
            // チェック処理

        }
    }
}
