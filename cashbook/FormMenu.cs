using static cashbook.common.ComConst.Mode;

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
            FormOfficeList formOfficeList = new(edit);
            formOfficeList.Show();
        }
    }
}
