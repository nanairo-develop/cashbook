using cashbook.common;
using MySqlConnector;
using System.Data;
using static cashbook.FormPurchaseListDao;
using static cashbook.dao.MManagerDao;
using static cashbook.dao.MOfficeDao;
using cashbook.dto;

namespace cashbook
{
    public partial class FormPurchaseList : Form
    {
        private readonly DataTable office = new();
        private readonly DataTable manager = new();


        #region コンストラクタ
        public FormPurchaseList()
        {
            InitializeComponent();
        }
        #endregion コンストラクタ

        #region イベント
        private void FormPurchaseList_Load(object sender, EventArgs e)
        {
            // コンボボックスの設定
            ComControl.ComboBoxParam comboManagerParam = new()
            {
                dt = manager,
                query = GetSelectManager(DateFrom.Value),
                comboBox = ComboManager,
                displayMember = "name",
                valueMember = "id"
            };
            ComControl.SetComboBox(comboManagerParam);

            ComControl.ComboBoxParam comboOfficeParam = new()
            {
                dt = office,
                query = GetSelectOffice(),
                comboBox = ComboOffice,
                displayMember = "name",
                valueMember = "id",
            };
            ComControl.SetComboBox(comboOfficeParam);

            // 初期値設定
            ComboManager.SelectedValue = 0;
            ComboOffice.SelectedValue = 0;

            DateFrom.Value = DateTime.Now;
            DateTo.TextYear = string.Empty;
            DateTo.TextMonth = string.Empty;
            DateTo.TextDay = string.Empty;
        }
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

        private void PurchaseList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                _ = MessageBox.Show("先頭行です");
            }
            else
            {
                TPurchaseDto purchaseDto = new()
                {
                    Id = (int)PurchaseList.Rows[e.RowIndex].Cells[0].Value,
                    PayDate = (DateTime)PurchaseList.Rows[e.RowIndex].Cells[1].Value,
                    Destination = (int)PurchaseList.Rows[e.RowIndex].Cells[2].Value,
                    Manager = (int)PurchaseList.Rows[e.RowIndex].Cells[4].Value,
                    SlipNumber = (string)PurchaseList.Rows[e.RowIndex].Cells[6].Value,
                    Memo = (string)PurchaseList.Rows[e.RowIndex].Cells[7].Value ?? string.Empty
                };

                FormPurchaseDetail formPurchaseDetail = new(purchaseDto);
                formPurchaseDetail.Show();
            }

        }

        private void Create_Click(object sender, EventArgs e)
        {
            FormPurchaseDetail formPurchaseDetail = new();
            formPurchaseDetail.Show();
        }

        private void ComboOffice_KeyDown(object sender, KeyEventArgs e)
        {
            ComControl.Combo_KeyDown(office, ComboOffice);
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
                FormPurchaseListDto formPurchaseListDto = new()
                {
                    OfficeId = (int?)ComboOffice.SelectedValue ?? 0,
                    ManagerId = (int?)ComboManager.SelectedValue ?? 0,
                    PayDateFrom = DateFrom.Value,
                    PayDateTo = DateTo.Value
                };
                command.CommandText = GetSelectPurchase(formPurchaseListDto);
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

    }
}