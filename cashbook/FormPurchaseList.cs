using Cashbook.common;
using Cashbook.dto;
using MySqlConnector;
using System.Data;
using static Cashbook.common.ComControl;
using static Cashbook.dao.MManagerDao;
using static Cashbook.dao.MOfficeDao;
using static Cashbook.FormPurchaseListDao;
using static Cashbook.FormPurchaseListDto.PurchaseListColumns;

namespace Cashbook
{
    /// <summary>伝票一覧-FormPurchaseList</summary>
    public partial class FormPurchaseList : Form
    {
        private readonly DataTable officeDataTable = new();
        private readonly DataTable managerDataTable = new();

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
            ComboBoxParam comboManagerParam = new()
            {
                dt = managerDataTable,
                query = GetSelectManager(DateFrom.Value),
                comboBox = ComboManager,
                displayMember = "name",
                valueMember = "id"
            };
            SetComboBox(comboManagerParam);

            ComboBoxParam comboOfficeParam = new()
            {
                dt = officeDataTable,
                query = GetSelectOffice(),
                comboBox = ComboOffice,
                displayMember = "name",
                valueMember = "id",
            };
            SetComboBox(comboOfficeParam);

            // 初期値設定
            ComboManager.SelectedValue = 0;
            ComboOffice.SelectedValue = 0;

            DateFrom.Value = DateTime.Now;
            DateTo.TextYear = string.Empty;
            DateTo.TextMonth = string.Empty;
            DateTo.TextDay = string.Empty;
        }

        /// <summary>検索ボタン押下時</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_Click(object sender, EventArgs e)
        {
            // データグリッドに表示させる
            DateTo.DateTimeFrom = DateFrom.Value;
            PurchaseList.DataSource = GetPurchases();
            ComControl.Columns(PurchaseList, id).HeaderText = "ID";
            ComControl.Columns(PurchaseList, id).Width = 40;
            ComControl.Columns(PurchaseList, payDate).HeaderText = "日付";
            ComControl.Columns(PurchaseList, payDate).Width = 80;
            ComControl.Columns(PurchaseList, destinationId).Visible = false;
            ComControl.Columns(PurchaseList, destination).HeaderText = "相手先名称";
            ComControl.Columns(PurchaseList, destination).Width = 240;
            ComControl.Columns(PurchaseList, managerId).Visible = false;
            ComControl.Columns(PurchaseList, manager).HeaderText = "担当者";
            ComControl.Columns(PurchaseList, manager).Width = 70;
            ComControl.Columns(PurchaseList, slipNumber).HeaderText = "伝票番号";
            ComControl.Columns(PurchaseList, slipNumber).Width = 80;
        }

        private void PurchaseList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                _ = MessageBox.Show("先頭行です");
            }
            else
            {
                DataGridViewRow row = PurchaseList.Rows[e.RowIndex];

                TPurchaseDto purchaseDto = new()
                {
                    Id = (int)ComControl.Cells(row, id).Value,
                    PayDate = (DateTime)ComControl.Cells(row, payDate).Value,
                    Destination = (int)ComControl.Cells(row, destinationId).Value,
                    Manager = (int)ComControl.Cells(row, managerId).Value,
                    SlipNumber = (string)ComControl.Cells(row, slipNumber).Value,
                    Memo = ComControl.Cells(row, memo).Value is DBNull ? string.Empty : (string)ComControl.Cells(row, memo).Value
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
            Combo_KeyDown(officeDataTable, ComboOffice);
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