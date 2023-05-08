using cashbook.common;
using MySqlConnector;
using System.Data;
using static cashbook.FormPurchaseListDao;

namespace cashbook
{
    public partial class FormPurchaseList : Form
    {
        #region �R���X�g���N�^
        public FormPurchaseList()
        {
            InitializeComponent();
        }
        #endregion �R���X�g���N�^

        #region �C�x���g
        private void Search_Click(object sender, EventArgs e)
        {

            // �f�[�^�O���b�h�ɕ\��������
            DateTo.DateTimeFrom = DateFrom.Value;
            PurchaseList.DataSource = GetPurchases();
            PurchaseList.Columns["id"].HeaderText = "ID";
            PurchaseList.Columns["id"].Width = 40;
            PurchaseList.Columns["payDate"].HeaderText = "���t";
            PurchaseList.Columns["payDate"].Width = 80;
            PurchaseList.Columns["destinationId"].Visible = false;
            PurchaseList.Columns["destination"].HeaderText = "����於��";
            PurchaseList.Columns["destination"].Width = 240;
            PurchaseList.Columns["managerId"].Visible = false;
            PurchaseList.Columns["manager"].HeaderText = "�S����";
            PurchaseList.Columns["manager"].Width = 70;
            PurchaseList.Columns["slipNumber"].HeaderText = "�`�[�ԍ�";
            PurchaseList.Columns["slipNumber"].Width = 80;
        }

        private void SlipList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                MessageBox.Show("�擪�s�ł�");
            }
            else
            {
                FormPurchaseDetail formPurchaseDetail = new(
                    id: (int)PurchaseList.Rows[e.RowIndex].Cells[0].Value,
                    payDate: (DateTime)PurchaseList.Rows[e.RowIndex].Cells[1].Value,
                    officeId: (int)PurchaseList.Rows[e.RowIndex].Cells[2].Value,
                    managerId: (int)PurchaseList.Rows[e.RowIndex].Cells[4].Value,
                    slipNumber: (string)PurchaseList.Rows[e.RowIndex].Cells[6].Value
                    );
                formPurchaseDetail.Show();
            }
        }

        private void Create_Click(object sender, EventArgs e)
        {
            FormPurchaseDetail formPurchaseDetail = new();
            formPurchaseDetail.Show();
        }
        #endregion �C�x���g
        #region ���\�b�h
        private DataTable GetPurchases()
        {
            // �f�[�^���擾����e�[�u��
            DataTable dt = new();

            using MySqlConnection conn = new(ComConst.connStr);
            using MySqlDataAdapter adapter = new();
            using MySqlCommand command = new();
            try
            {
                // �ڑ����J��
                conn.Open();
                command.CommandText = GetSelectPurchase(DateFrom.Value, DateTo.Value);
                command.Connection = conn;
                adapter.SelectCommand = command;

                _ = adapter.Fill(dt);
            }
            catch (MySqlException mse)
            {
                _ = MessageBox.Show(mse.Message, "�f�[�^�擾�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
        #endregion ���\�b�h

        private void FormPurchaseList_Load(object sender, EventArgs e)
        {
            DateFrom.Value = DateTime.Now;
            DateTo.TextYear = string.Empty;
            DateTo.TextMonth = string.Empty;
            DateTo.TextDay = string.Empty;
        }
    }
}