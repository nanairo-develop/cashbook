using cashbook.common;
using cashbook.dto;
using MySqlConnector;
using System.Data;
using System.Diagnostics;
using static cashbook.FormPurchaseDetailDao;

namespace cashbook
{
    public partial class FormPurchaseDetail : Form
    {
        private int purchaseId;
        private readonly int managerId;
        private readonly int officeId;
        private readonly DataTable office = new();
        private readonly DataTable manager = new();

        private enum DataSumColumns
        {
            receivableSum = 0,
            payableSum
        }
        private enum DetailListColumns
        {
            blanchId = 0,
            description,
            receivable,
            payable,
            useforfood
        }

        #region コンストラクタ
        public FormPurchaseDetail()
        {
            InitializeComponent();
            managerId = 0;
            officeId = 0;
            Insert.Enabled = true;
            Change.Enabled = false;
        }

        public FormPurchaseDetail(int id, DateTime payDate, int officeId, int managerId, string slipNumber)
        {
            InitializeComponent();
            purchaseId = id;
            DatePicker.Value = payDate;
            this.managerId = managerId;
            this.officeId = officeId;
            SlipNumber.Text = slipNumber;
            Insert.Enabled = false;
            Change.Enabled = true;
        }
        #endregion コンストラクタ

        #region イベント
        private void FormPurchaseDetail_Load(object sender, EventArgs e)
        {
            // コンボボックスの設定
            string selectManager = GetSelectManager(DatePicker.Value);
            SetComboBox(manager, selectManager, ComboManager, "name", "id");

            string selectOffice = GetSelectOffice();
            SetComboBox(office, selectOffice, ComboOffice, "name", "id");

            SetDetailList();

            ComboManager.SelectedValue = managerId;
            ComboOffice.SelectedValue = officeId;
            SumData[(int)DataSumColumns.receivableSum, 0].Value = SumAmount((int)DetailListColumns.receivable);
            SumData[(int)DataSumColumns.payableSum, 0].Value = SumAmount((int)DetailListColumns.payable);

        }

        private void DetailList_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex is ((int)DetailListColumns.receivable) or ((int)DetailListColumns.payable))
            {

                if (e.FormattedValue is not null)
                {
                    if (e.FormattedValue.ToString() == string.Empty)
                    {
                        DetailList[e.ColumnIndex, e.RowIndex].Value = 0;
                    }

                    if (int.TryParse(e.FormattedValue.ToString(), out _))
                    {
                        // 合計値を計算
                        SumData[e.ColumnIndex - 2, 0].Value = SumAmount(e.ColumnIndex);
                    }
                    else
                    {
                        _ = MessageBox.Show("整数を入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                    }
                }
            }
        }
        private void DetailList_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // IMEモードの設定
            switch (e.ColumnIndex)
            {
                case ((int)DetailListColumns.blanchId) or ((int)DetailListColumns.receivable) or ((int)DetailListColumns.payable):
                    DetailList.ImeMode = ImeMode.Alpha;
                    break;
                case (int)DetailListColumns.description:
                    DetailList.ImeMode = ImeMode.Hiragana;
                    break;
                default:
                    break;
            }
        }
        private void ComboOffice_KeyDown(object sender, KeyEventArgs e)
        {
            //コンボボックスのデータの更新の為
            DataView dv = office.DefaultView;
            //コンボボックスに入力された文字列でフィルター
            dv.RowFilter = "name LIKE '*" + ComboOffice.Text + "*'";
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            // MessageAreaの初期化
            MessageArea.Text = "";
            // チェック処理
            if (!CheckValid())
            {
                return;
            };

            // Insert
            ExecInsert();

            // 実行結果メッセージ表示

            // 画面再読み込み

        }

        #endregion イベント

        #region メソッド
        #region 検索処理
        /// <summary>
        /// コンボボックスの選択候補をDBから設定する
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="query"></param>
        /// <param name="comboBox"></param>
        /// <param name="displayMember"></param>
        /// <param name="valueMember"></param>
        private static void SetComboBox(
            DataTable dt,
            string query,
            ComboBox comboBox,
            string displayMember,
            string valueMember)
        {
            MySqlConnection conn = new(ComConst.connStr);

            // 接続を開く
            conn.Open();

            // コンボボックスの設定
            DataTable combo = dt;

            MySqlDataAdapter dataAdapter = new(query, conn);
            _ = dataAdapter.Fill(combo);

            // テキストフィルターを実装するため、DataViewを経由する
            DataView dv = combo.DefaultView;

            comboBox.DataSource = dv;
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = valueMember;

        }

        private int SumAmount(int col)
        {
            int sum = 0;
            for (int i = 0; i < DetailList.RowCount - 1; i++)
            {
                sum += int.TryParse(DetailList[col, i].Value.ToString(), out int a) ? a : 0;
            }
            return sum;
        }
        private void ExecInsert()
        {
            using MySqlConnection conn = new(ComConst.connStr);
            // 接続を開く
            conn.Open();
            using MySqlTransaction transaction = conn.BeginTransaction();
            try
            {
                // ヘダー行Insert
                InsertPurchase(conn, transaction);

                // ヘダー行を検索して明細テーブルのpurchaseIdを取得
                purchaseId = SelectPurchaseId(conn, transaction);

                // 明細行Insert
                InsertPurchaseDetail(purchaseId, conn, transaction);
                transaction.Commit();

            }
            catch (Exception me)
            {
                transaction.Rollback();
                _ = MessageBox.Show("ERROR: " + me.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        private void SetDetailList()
        {
            using MySqlConnection conn = new(ComConst.connStr);

            // 接続を開く
            conn.Open();

            // 明細行の取得
            // データを取得するテーブル
            DataTable tbl = new();

            // クエリを作成する
            string query = GetSelectPurchaseDetail(purchaseId);

            MySqlDataAdapter dataAdp = new(query, conn);
            _ = dataAdp.Fill(tbl);

            DetailList.DataSource = tbl;

        }
        #endregion 検索処理

        #region 更新処理
        private void InsertPurchase(MySqlConnection conn, MySqlTransaction transaction)
        {
            TPurchaseDto tPurchaseDto = new(
                DatePicker.Value,
                ComboOffice.SelectedValue,
                ComboManager.SelectedValue,
                SlipNumber.Text);
            string query = GetPurchaseInsert(tPurchaseDto);

            using MySqlCommand command = conn.CreateCommand();
            command.CommandText = query;
            command.Transaction = transaction;

            int result = command.ExecuteNonQuery();
            // 挿入されなかった場合
            if (result != 1)
            {
                _ = MessageBox.Show("insert ERROR");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>エラーの場合は0が返却される</returns>
        private static int SelectPurchaseId(MySqlConnection conn, MySqlTransaction transaction)
        {
            int ret = 0;
            using MySqlCommand command = conn.CreateCommand();
            try
            {
                command.CommandText = GetSelectPurchaseId();
                command.Transaction = transaction;

                object? scalar = command.ExecuteScalar();
                ret = scalar is not null ? (int)scalar : throw new Exception(message: "ヘダーInsertが失敗している");
            }
            catch (MySqlException mse)
            {
                _ = MessageBox.Show(mse.Message, "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ret;
        }
        private void InsertPurchaseDetail(int purchaseId, MySqlConnection conn, MySqlTransaction transaction)
        {
            List<TPurchaseDetailDto> purchaseDetailDtos = new();
            for (int i = 0; i < DetailList.RowCount - 1; i++)
            {
                DataGridViewRow rows = DetailList.Rows[i];
                if (rows.Cells[0].Value is null)
                {
                    break;
                }
                else
                {
                    purchaseDetailDtos.Add(new(
                        purchaseId: purchaseId,
                        branchId: (int)rows.Cells[0].Value,
                        description: (string)rows.Cells[1].Value,
                        receivable: (int)rows.Cells[2].Value,
                        payable: (int)rows.Cells[3].Value,
                        useForFood: (bool)rows.Cells[4].Value));
                }
            }

            using MySqlCommand command = conn.CreateCommand();
            command.CommandText = GetInsertPurchaseDetail(purchaseDetailDtos);
            command.Transaction = transaction;

            int result = command.ExecuteNonQuery();
            // 挿入されなかった場合
            if (result != 1)
            {
                _ = MessageBox.Show("insert ERROR");
            }

        }
        #endregion 更新処理

        #region チェック処理
        private bool CheckValid()
        {
            bool ret = true;
            // 日付
            // 未来日付はNG
            if (DatePicker.Value > DateTime.Now)
            {
                ComControl.SetErrorLabelColor(PayDatePickerLabel);
                MessageArea.Text += "";
                ret = false;
            }
            else
            {
                ComControl.SetClearLabelColor(PayDatePickerLabel);
            }

            // 担当者
            // 担当者指定なしはNG
            if (ComboManager.SelectedValue == null)
            {
                ComControl.SetErrorLabelColor(ComboManagerLabel);
                ret = false;
            }
            else
            {
                ComControl.SetClearLabelColor(ComboManagerLabel);
            }

            // 相手先
            // 相手先指定なしはNG
            if (ComboOffice.SelectedValue == null)
            {
                ComControl.SetErrorLabelColor(ComboOfficeLabel);
                ret = false;
            }
            else
            {
                ComControl.SetClearLabelColor(ComboOfficeLabel);
            }

            // 伝票番号
            // 伝票番号無しはNG
            if (SlipNumber.Text == string.Empty)
            {
                ComControl.SetErrorLabelColor(SlipNumberLabel);
                ret = false;
            }
            else
            {
                ComControl.SetClearLabelColor(SlipNumberLabel);
            }


            // 明細
            // 件数0はNG
            if (DetailList.RowCount == 1)
            {
                ComControl.SetErrorGridColor(DetailList.DefaultCellStyle);
                ret = false;
            }
            else
            {
                ComControl.SetClearGridColor(DetailList.DefaultCellStyle);
            }
            // 内容無しはNG
            if (!IsExistDescription())
            {
                ret = false;
            }
            return ret;
        }

        private bool IsExistDescription()
        {
            bool ret = true;
            for (int i = 0; i < DetailList.RowCount - 1; i++)
            {
                DataGridViewRow row = DetailList.Rows[i];
                // 内容無しはNG
                if (row.Cells[1].Value.ToString() == string.Empty)
                {
                    ComControl.SetErrorGridColor(row.Cells[1].Style);
                    ret = false;
                    Debug.WriteLine("未記載");
                }
                else
                {
                    ComControl.SetClearGridColor(row.Cells[1].Style);
                }
            }
            return ret;
        }
        #endregion チェック処理
        #endregion メソッド

    }
}
