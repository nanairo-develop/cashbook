using cashbook.common;
using cashbook.dto;
using MySqlConnector;
using System.Data;
using System.Globalization;
using static cashbook.common.ComConst.Mode;
using static cashbook.common.ComConst.FormPurchaseDetail.DataSumColumns;
using static cashbook.common.ComConst.FormPurchaseDetail.DetailListColumns;
using static cashbook.common.ComControl;
using static cashbook.common.ComValidation;
using static cashbook.dao.MManagerDao;
using static cashbook.dao.MOfficeDao;
using static cashbook.dao.TPurchaseDao;
using static cashbook.dao.TPurchaseDetailDao;
using static cashbook.common.ComConst.FormPurchaseDetail;

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
            _ = SumData.Rows.Add(SumAmount(receivable), SumAmount(payable));
            ComControl.Columns(SumData, receivableSum).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            ComControl.Columns(SumData, receivableSum).DefaultCellStyle.Format = "c";

            ComControl.Columns(SumData, payableSum).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            ComControl.Columns(SumData, payableSum).DefaultCellStyle.Format = "c";

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

            // TODO: 30日以上前の日付の場合警告を出す
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

        /// <summary>
        /// 担当者選択を離れたときのエラーチェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboManager_Leave(object sender, EventArgs e)
        {
            // 担当者指定なしはNG
            MessageArea.Text += NoSelection(ComboManager, ComboManagerLabel);
        }
        #endregion ComboManager

        #region ComboOffice
        /// <summary>
        /// ComboBox絞り込み処理を呼び出す
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboOffice_KeyDown(object sender, KeyEventArgs e)
        {
            Combo_KeyDown(office, ComboOffice);
        }

        /// <summary>
        /// 相手先選択を離れたときのエラーチェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboOffice_Leave(object sender, EventArgs e)
        {
            // 相手先指定なしはNG
            MessageArea.Text += NoSelection(ComboOffice, ComboOfficeLabel);
        }
        #endregion ComboOffice

        /// <summary>
        /// 伝票番号を離れたときのエラーチェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SlipNumber_Leave(object sender, EventArgs e)
        {
            // 伝票番号無しはNG
            MessageArea.Text += CheckEmpty(SlipNumber, SlipNumberLabel);
        }

        /// <summary>
        /// 行追加ボタンクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RowAdd_Click(object sender, EventArgs e)
        {
            _ = DetailListDataTable.Rows.Add();
        }

        #region DetailList

        /// <summary>
        /// カラムによってIMEモードを切り替える
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailList_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DetailListColumns col = (DetailListColumns)e.ColumnIndex;
            // IMEモードの設定
            switch (col)
            {
                case blanchId or receivable or payable:
                    DetailList.ImeMode = ImeMode.Alpha;
                    break;
                case description:
                    DetailList.ImeMode = ImeMode.Hiragana;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// セルを離れたときに計算などおこなう
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailList_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DetailListColumns col = (DetailListColumns)e.ColumnIndex;
            if (col is receivable or payable)
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
                            SumData[e.ColumnIndex - 2, 0].Value = SumAmount(col);
                        }
                    }
                    else
                    {
                        // 合計値を計算
                        SumData[e.ColumnIndex - 2, 0].Value = SumAmount(col);
                    }
                }
                catch (Exception ex)
                {
                    _ = MessageBox.Show(ex.Message);
                }
            }
        }

        #endregion DetailList
        /// <summary>
        /// 相手先を選択するための画面を開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OfficeSelect_Click(object sender, EventArgs e)
        {
            Wait = true;
            FormOfficeList formOfficeList = new(select);
            formOfficeList.Show(this);
        }

        /// <summary>
        /// 登録ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            if (IsWarning())
            {
                return;
            }

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
        private int SumAmount(DetailListColumns col)
        {
            int sum = 0;
            for (int i = 0; i < DetailList.RowCount; i++)
            {
                sum += int.TryParse(ComControl.Cell(DetailList, col, i).Value.ToString(), out int a) ? a : 0;
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
            ComControl.Columns(DetailList, description).Width = 200;
            ComControl.Columns(DetailList, description).DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            ComControl.Columns(DetailList, receivable).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            ComControl.Columns(DetailList, payable).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        /// <summary>
        /// 伝票明細を取得する
        /// </summary>
        /// <remarks>
        /// 戻り値はないが、メソッド内でメンバ変数に設定している
        /// </remarks>
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
        /// <summary>
        /// 更新処理まとめ
        /// </summary>
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

        /// <summary>
        /// 伝票の登録
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="transaction"></param>
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

        /// <summary>
        /// 伝票明細の登録
        /// </summary>
        /// <param name="purchaseId"></param>
        /// <param name="conn"></param>
        /// <param name="transaction"></param>
        private void InsertPurchaseDetail(int purchaseId, MySqlConnection conn, MySqlTransaction transaction)
        {
            List<TPurchaseDetailDto> purchaseDetailDtos = new();
            for (int i = 0; i < DetailList.RowCount - 1; i++)
            {
                DataGridViewRow rows = DetailList.Rows[i];
                if (ComControl.Cells(rows, blanchId).Value is null)
                {
                    break;
                }
                else
                {
                    TPurchaseDetailDto purchaseDetailDto = new()
                    {
                        PurchaseId = purchaseId,
                        BranchId = (sbyte)ComControl.Cells(rows, blanchId).Value,
                        Description = (string)ComControl.Cells(rows, description).Value,
                        Receivable = (int)ComControl.Cells(rows, receivable).Value,
                        Payable = (int)ComControl.Cells(rows, payable).Value,
                        UseForFood = (bool)ComControl.Cells(rows, useforfood).Value
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
        /// <summary>
        /// 更新前に実施するチェック処理
        /// </summary>
        /// <returns>true:エラーなし</returns>
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
            MessageArea.Text += CheckRowCount(DetailList);
            // 内容無しはNG
            MessageArea.Text += CheckExistDescription();

            return MessageArea.Text == string.Empty;
        }

        /// <summary>
        /// 内容が存在するかチェックする
        /// </summary>
        /// <returns></returns>
        private string CheckExistDescription()
        {
            string errorMessage = string.Empty;
            for (int i = 0; i < DetailList.RowCount; i++)
            {
                DataGridViewRow row = DetailList.Rows[i];
                // 内容無しはNG
                if (ComControl.Cells(row, description).Value.ToString()?.Trim() == string.Empty)
                {
                    SetErrorGridColor(ComControl.Cells(row, description).Style);
                    errorMessage = "内容は入力してください";
                }
            }
            return errorMessage;
        }

        /// <summary>
        /// 警告処理
        /// </summary>
        /// <returns>true:警告中断</returns>
        private bool IsWarning()
        {
            bool ret = false;
            // 30日以上前の日付の場合警告を出す
            // TODO: 

            // 同じ日付、同じ相手先、同じ伝票番号の場合警告を出す

            return ret;
        }
        #endregion チェック処理
        #endregion メソッド
    }
}
