namespace cashbook
{
    public partial class FormOfficeDetail : Form
    {
        public struct Param
        {
            public int id;
            public string officeName;
            public object displayOrder;
        }

        public FormOfficeDetail()
        {
            InitializeComponent();
            Insert.Enabled = true;
            Change.Enabled = false;

        }
        public FormOfficeDetail(Param param)
        {
            InitializeComponent();
            Id.Text = param.id.ToString();
            OfficeName.Text = param.officeName;
            Order.Text = param.displayOrder.ToString();
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
