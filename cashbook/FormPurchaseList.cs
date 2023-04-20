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
                // 接続を開く
                conn.Open();
                command.CommandText = GetSelectPurchase(DateFrom.Value, DateTo.Value);
                command.Connection = conn;
                adapter.SelectCommand = command;

                // クエリを作成する
                //string query = GetSelectPurchase();

                // SQLを実行する
                //MySqlDataAdapter dataAdp = new(query, conn);

                // データを取得するテーブル
                DataTable tbl = new();

                //_ = dataAdp.Fill(tbl);
                _ = adapter.Fill(tbl);

                // データグリッドに表示させる
                PurchaseList.DataSource = tbl;
                PurchaseList.Columns["id"].HeaderText = "ID";
                PurchaseList.Columns["id"].Width = 40;
                PurchaseList.Columns["payDate"].HeaderText = "日付";
                PurchaseList.Columns["payDate"].Width = 80;
                PurchaseList.Columns["destinationId"].Visible = false;
                PurchaseList.Columns["destination"].HeaderText = "相手先名称";
                PurchaseList.Columns["destination"].Width = 240;
                PurchaseList.Columns["managerId"].Visible = false;
                PurchaseList.Columns["manager"].HeaderText = "担当者";
                PurchaseList.Columns["manager"].Width = 70;
                PurchaseList.Columns["slipNumber"].HeaderText = "伝票番号";
                PurchaseList.Columns["slipNumber"].Width = 80;


            }
            catch (MySqlException mse)
            {
                _ = MessageBox.Show(mse.Message, "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("先頭行です");
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