using cashbook.common;
using MySqlConnector;
using System.Data;
using static cashbook.FormPurchaseListDao;

namespace cashbook
{
    public partial class FormPurchaseList : Form
    {
        #region コンストラクタ
        public FormPurchaseList()
        {
            InitializeComponent();
        }
        #endregion コンストラクタ

        #region イベント
        private void Search_Click(object sender, EventArgs e)
        {

            // データグリッドに表示させる
            DateTo.DateTimeFrom = DateFrom.Value;
            PurchaseList.DataSource = GetPurchases();
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
        #endregion イベント
        #region メソッド
        private DataTable GetPurchases()
        {
            // データを取得するテーブル
            DataTable dt = new();

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

                _ = adapter.Fill(dt);
            }
            catch (MySqlException mse)
            {
                _ = MessageBox.Show(mse.Message, "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
        #endregion メソッド

        private void FormPurchaseList_Load(object sender, EventArgs e)
        {
            DateFrom.Value = DateTime.Now;
            DateTo.TextYear = string.Empty;
            DateTo.TextMonth = string.Empty;
            DateTo.TextDay = string.Empty;
        }
    }
}