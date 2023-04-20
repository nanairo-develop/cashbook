using cashbook.common;
using MySqlConnector;
using System.Data;
using static cashbook.FormPurchaseListDao;

namespace cashbook
{
    public partial class FormPurchaseList : Form
    {
        public FormPurchaseList()
        {
            InitializeComponent();
        }

        private void Search_Click(object sender, EventArgs e)
        {
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

                // �N�G�����쐬����
                //string query = GetSelectPurchase();

                // SQL�����s����
                //MySqlDataAdapter dataAdp = new(query, conn);

                // �f�[�^���擾����e�[�u��
                DataTable tbl = new();

                //_ = dataAdp.Fill(tbl);
                _ = adapter.Fill(tbl);

                // �f�[�^�O���b�h�ɕ\��������
                PurchaseList.DataSource = tbl;
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
            catch (MySqlException mse)
            {
                _ = MessageBox.Show(mse.Message, "�f�[�^�擾�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
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
    }
}