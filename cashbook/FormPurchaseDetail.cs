using cashbook.common;
using cashbook.dto;
using MySqlConnector;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using static cashbook.common.ComConst.Mode;
using static cashbook.common.ComControl;
using static cashbook.common.ComValidation;
using static cashbook.dao.MManagerDao;
using static cashbook.dao.MOfficeDao;
using static cashbook.dao.TPurchaseDao;
using static cashbook.dao.TPurchaseDetailDao;

namespace cashbook
{
    public partial class FormPurchaseDetail : Form
    {
        #region メンバ変数
        private int purchaseId;
        private readonly int managerId;

        private readonly DataTable office = new();
        private readonly DataTable manager = new();
        private readonly DataTable DetailListDataTable = new();
        #endregion メンバ変数

        #region プロパティ
        public int OfficeId { get; set; }

        /// <summary>
        /// 子画面値設定待ち状態 true:待ち
        /// </summary>
        public bool Wait { get; set; }
        #endregion プロパティ

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

        /// <summary>
        /// コンストラクタ用のパラメータ構造体
        /// </summary>
        public struct Param
        {
            public int id;
            public DateTime payDate;
            public int officeId;
            public int managerId;
            public string slipNumber;
            public object memo;
        }

        #region コンストラクタ
        /// <summary>
        /// 新規伝票のコンストラクタ
        /// </summary>
        public FormPurchaseDetail()
        {
            InitializeComponent();
            managerId = 0;
            OfficeId = 0;
            Insert.Enabled = true;
            Change.Enabled = false;
            Wait = false;
        }

        /// <summary>
        /// 明細をクリックしたときのコンストラクタ
        /// </summary>
        /// <param name="param"></param>
        public FormPurchaseDetail(Param param)
        {
            InitializeComponent();
            purchaseId = param.id;
            DatePicker.Value = param.payDate;
            managerId = param.managerId;
            OfficeId = param.officeId;
            SlipNumber.Text = param.slipNumber;
            Memo.Text = param.memo.ToString();
            Insert.Enabled = false;
            Change.Enabled = true;
            Wait = false;
        }
        #endregion コンストラクタ

        #region イベント
        #region FormPurchaseDetail
        /// <summary>
        /// 画面ロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormPurchaseDetail_Load(object sender, EventArgs e)
        {
            // コンボボックスの設定
            ComboBoxParam comboManager = new()
            {
                dt = manager,
                query = GetSelectManager(DatePicker.Value),
                comboBox = ComboManager,
                displayMember = "name",
                valueMember = "id"
            };
            SetComboBox(comboManager);

            ComboBoxParam comboOffice = new()
            {
                dt = office,
                query = GetSelectOffice(),
                comboBox = ComboOffice,
                displayMember = "name",
                valueMember = "id"
            };
            SetComboBox(comboOffice);

            ComboManager.SelectedValue = managerId;
            ComboOffice.SelectedValue = OfficeId;

            // 伝票明細の設定
            SetDetailList();

            // 合計欄の設定
            SumData.AllowUserToAddRows = false;
            _ = SumData.Rows.Add(SumAmount((int)DetailListColumns.receivable), SumAmount((int)DetailListColumns.payable));
            SumData.Columns[(int)DataSumColumns.receivableSum].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            SumData.Columns[(int)DataSumColumns.receivableSum].DefaultCellStyle.Format = "c";

            SumData.Columns[(int)DataSumColumns.payableSum].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            SumData.Columns[(int)DataSumColumns.payableSum].DefaultCellStyle.Format = "c";

        }

        /// <summary>
        /// 画面がActiveになった時、コンボオフィスの値を設定する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormPurchaseDetail_Activated(object sender, EventArgs e)
        {
            if (Wait)
            {
                ComboOffice.SelectedValue = OfficeId;
            }
            Wait = false;
        }
        #endregion FormPurchaseDetail

        /// <summary>
        /// 日付を離れたときのエラーチェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatePicker_Leave(object sender, EventArgs e)
        {
            // 未来日付はNG
            MessageArea.Text += CheckGreaterThan(DatePicker.Value, DateTime.Now, DatePickerLabel);
        }

        #region ComboManager
        /// <summary>
        /// ComboBox絞り込み処理を呼び出す
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboManager_KeyDown(object sender, KeyEventArgs e)
        {
            Combo_KeyDown(manager, ComboManager);
        }

        private void ComboManager_Leave(object sender, EventArgs e)
        {
            // 担当者指定なしはNG
            MessageArea.Text += NoSelection(ComboManager, ComboManagerLabel);
        }
        #endregion ComboManager

        #region ComboOffice
        private void ComboOffice_KeyDown(object sender, KeyEventArgs e)
        {
            Combo_KeyDown(office, ComboOffice);
        }
        private void ComboOffice_Leave(object sender, EventArgs e)
        {
            // 相手先指定なしはNG
            MessageArea.Text += NoSelection(ComboOffice, ComboOfficeLabel);
        }
        #endregion ComboOffice

        private void SlipNumber_Leave(object sender, EventArgs e)
        {
            // 伝票番号無しはNG
            MessageArea.Text += CheckEmpty(SlipNumber, SlipNumberLabel);
        }

        private void RowAdd_Click(object sender, EventArgs e)
        {
            _ = DetailListDataTable.Rows.Add();
        }

        #region DetailList
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

        private void DetailList_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex is ((int)DetailListColumns.receivable) or ((int)DetailListColumns.payable))
            {
                try
                {
                    // 空欄の場合強制的に0にする
                    DataGridViewCell cell = DetailList[e.ColumnIndex, e.RowIndex];
                    if (cell.Value.ToString() == string.Empty)
                    {
                        cell.Value = 0;
                    }

                    // 整数値変換できる値かチェックする
                    CultureInfo provider = new("ja-JP");
                    NumberStyles styles = NumberStyles.Integer
                                            | NumberStyles.AllowCurrencySymbol
                                            | NumberStyles.AllowThousands;
                    if (DetailList.EditingControl is not null)
                    {
                        if (!int.TryParse(DetailList.EditingControl.Text, styles, provider, out _))
                        {
                            _ = MessageBox.Show("整数を入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            // 合計値を計算
                            SumData[e.ColumnIndex - 2, 0].Value = SumAmount(e.ColumnIndex);
                        }
                    }
                    else
                    {
                        // 合計値を計算
                        SumData[e.ColumnIndex - 2, 0].Value = SumAmount(e.ColumnIndex);
                    }
                }
                catch (Exception ex)
                {
                    _ = MessageBox.Show(ex.Message);
                }
            }
        }

        #endregion DetailList
        private void OfficeSelect_Click(object sender, EventArgs e)
        {
            Wait = true;
            FormOfficeList formOfficeList = new(select);
            formOfficeList.Show(this);
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            // MessageAreaの初期化
            MessageArea.Text = string.Empty;

            // チェック処理
            if (!IsValidation())
            {
                return;
            };

            // 警告処理
            // 30日以上前の日付の場合警告を出す

            // 同じ日付、同じ相手先、同じ伝票番号の場合警告を出す

            // Insert
            ExecInsert();

            // 実行結果メッセージ表示

            // 画面再読み込み

        }

        private void Change_Click(object sender, EventArgs e)
        {
            // Delete-Insertで登録する
        }

        #endregion イベント

        #region メソッド
        /// <summary>
        /// 指令された列の合計を出力する
        /// </summary>
        /// <param name="col">売掛列か買掛列か/param>
        /// <returns>指定された列の合計</returns>
        private int SumAmount(int col)
        {
            int sum = 0;
            for (int i = 0; i < DetailList.RowCount; i++)
            {
                sum += int.TryParse(DetailList[col, i].Value.ToString(), out int a) ? a : 0;
            }
            return sum;
        }

        #region 検索処理

        /// <summary>
        /// Selectした内容をDataGridViewに設定する
        /// </summary>
        private void SetDetailList()
        {
            DetailList.AllowUserToAddRows = false;
            DetailList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            GetPurchaseDetails();
            DetailList.DataSource = DetailListDataTable;
            DetailList.Columns[(int)DetailListColumns.description].Width = 200;
            DetailList.Columns[(int)DetailListColumns.description].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DetailList.Columns[(int)DetailListColumns.receivable].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DetailList.Columns[(int)DetailListColumns.receivable].DefaultCellStyle.Format = "c";
            DetailList.Columns[(int)DetailListColumns.payable].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DetailList.Columns[(int)DetailListColumns.payable].DefaultCellStyle.Format = "c";
        }

        private void GetPurchaseDetails()
        {
            using MySqlConnection conn = new(ComConst.connStr);

            // 接続を開く
            conn.Open();

            // 明細行の取得

            // クエリを作成する
            string query = GetSelectPurchaseDetail(purchaseId);

            MySqlDataAdapter dataAdp = new(query, conn);
            _ = dataAdp.Fill(DetailListDataTable);
        }

        /// <summary>
        /// 新しい伝票番号を取得する
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
                ret = scalar is not null ? (int)scalar : throw new InvalidOperationException(message: "ヘダーInsertが失敗している");
            }
            catch (MySqlException mse)
            {
                _ = MessageBox.Show(mse.Message, "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ret;
        }

        #endregion 検索処理

        #region 更新処理
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

        private void InsertPurchase(MySqlConnection conn, MySqlTransaction transaction)
        {
            TPurchaseDto purchaseDto = new(ComboOffice.SelectedValue, ComboManager.SelectedValue)
            {
                PayDate = DatePicker.Value,
                SlipNumber = SlipNumber.Text,
                Memo = Memo.Text
            };
            string query = GetInsertPurchase(purchaseDto);

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
                    TPurchaseDetailDto purchaseDetailDto = new()
                    {
                        PurchaseId = purchaseId,
                        BranchId = (sbyte)rows.Cells[0].Value,
                        Description = (string)rows.Cells[1].Value,
                        Receivable = (int)rows.Cells[2].Value,
                        Payable = (int)rows.Cells[3].Value,
                        UseForFood = (bool)rows.Cells[4].Value
                    };
                    purchaseDetailDtos.Add(purchaseDetailDto);
                }
            }

            using MySqlCommand command = conn.CreateCommand();
            command.CommandText = GetInsertPurchaseDetails(purchaseDetailDtos);
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
        private bool IsValidation()
        {
            MessageArea.Text = string.Empty;

            // 日付
            // 未来日付はNG
            MessageArea.Text = CheckGreaterThan(DatePicker.Value, DateTime.Now, DatePickerLabel);

            // 担当者
            // 担当者指定なしはNG
            MessageArea.Text += NoSelection(ComboManager, ComboManagerLabel);

            // 相手先
            // 相手先指定なしはNG
            MessageArea.Text += NoSelection(ComboOffice, ComboOfficeLabel);

            // 伝票番号
            // 伝票番号無しはNG
            MessageArea.Text += CheckEmpty(SlipNumber, SlipNumberLabel);

            // 明細
            // 件数0はNG
            if (DetailList.RowCount == 1)
            {
                SetErrorGridColor(DetailList.DefaultCellStyle);
            }
            else
            {
                SetClearGridColor(DetailList.DefaultCellStyle);
            }
            // 内容無しはNG
            if (!IsExistDescription())
            {
            }

            return MessageArea.Text == string.Empty;
        }

        private bool IsExistDescription()
        {
            bool ret = true;
            for (int i = 0; i < DetailList.RowCount; i++)
            {
                DataGridViewRow row = DetailList.Rows[i];
                // 内容無しはNG
                if (row.Cells[1].Value.ToString() == string.Empty)
                {
                    SetErrorGridColor(row.Cells[1].Style);
                    ret = false;
                    Debug.WriteLine("未記載");
                }
                else
                {
                    SetClearGridColor(row.Cells[1].Style);
                }
            }
            return ret;
        }
        #endregion チェック処理
        #endregion メソッド
    }
}
