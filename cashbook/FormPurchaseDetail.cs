﻿using cashbook.common;
using cashbook.dto;
using MySqlConnector;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using static cashbook.common.ComConst.Mode;
using static cashbook.dao.MManagerDao;
using static cashbook.dao.MOfficeDao;
using static cashbook.dao.TPurchaseDao;
using static cashbook.dao.TPurchaseDetailDao;

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
        public FormPurchaseDetail()
        {
            InitializeComponent();
            managerId = 0;
            officeId = 0;
            Insert.Enabled = true;
            Change.Enabled = false;
        }

        public FormPurchaseDetail(Param param)
        {
            InitializeComponent();
            purchaseId = param.id;
            DatePicker.Value = param.payDate;
            managerId = param.managerId;
            officeId = param.officeId;
            SlipNumber.Text = param.slipNumber;
            Memo.Text = param.memo.ToString();
            Insert.Enabled = false;
            Change.Enabled = true;
        }
        #endregion コンストラクタ

        #region イベント
        private void FormPurchaseDetail_Load(object sender, EventArgs e)
        {
            // コンボボックスの設定
            ComControl.ComboBoxParam comboManager;
            comboManager.dt = manager;
            comboManager.query = GetSelectManager(DatePicker.Value);
            comboManager.comboBox = ComboManager;
            comboManager.displayMember = "name";
            comboManager.valueMember = "id";
            ComControl.SetComboBox(comboManager);

            ComControl.ComboBoxParam comboOffice;
            comboOffice.dt = office;
            comboOffice.query = GetSelectOffice();
            comboOffice.comboBox = ComboOffice;
            comboOffice.displayMember = "name";
            comboOffice.valueMember = "id";
            ComControl.SetComboBox(comboOffice);

            SetDetailList();

            ComboManager.SelectedValue = managerId;
            ComboOffice.SelectedValue = officeId;

            SumData.AllowUserToAddRows = false;
            _ = SumData.Rows.Add(SumAmount((int)DetailListColumns.receivable), SumAmount((int)DetailListColumns.payable));
            //SumData[(int)DataSumColumns.receivableSum, 0].Value = SumAmount((int)DetailListColumns.receivable);
            SumData.Columns[(int)DataSumColumns.receivableSum].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            SumData.Columns[(int)DataSumColumns.receivableSum].DefaultCellStyle.Format = "c";

            //SumData[(int)DataSumColumns.payableSum, 0].Value = SumAmount((int)DetailListColumns.payable);
            SumData.Columns[(int)DataSumColumns.payableSum].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            SumData.Columns[(int)DataSumColumns.payableSum].DefaultCellStyle.Format = "c";

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
                    CultureInfo provider = new("ja-JP");
                    NumberStyles styles = NumberStyles.Integer
                                          | NumberStyles.AllowCurrencySymbol
                                          | NumberStyles.AllowThousands;
                    if (int.TryParse(e.FormattedValue.ToString(), styles, provider, out _))
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
            ComControl.Combo_KeyDown(office, ComboOffice);
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            // MessageAreaの初期化
            MessageArea.Text = string.Empty;
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

        private void OfficeSelect_Click(object sender, EventArgs e)
        {
            FormOfficeList formOfficeList = new(edit);
            formOfficeList.Show();
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

        private void SetDetailList()
        {
            DetailList.AllowUserToAddRows = false;
            DetailList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DetailList.DataSource = GetPurchaseDetails();
            DetailList.Columns["description"].Width = 200;
            DetailList.Columns["description"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DetailList.Columns["receivable"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DetailList.Columns["receivable"].DefaultCellStyle.Format = "c";
            DetailList.Columns["payable"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DetailList.Columns["payable"].DefaultCellStyle.Format = "c";
        }

        private DataTable GetPurchaseDetails()
        {
            using MySqlConnection conn = new(ComConst.connStr);

            // 接続を開く
            conn.Open();

            // 明細行の取得
            // データを取得するテーブル
            DataTable dt = new();

            // クエリを作成する
            string query = GetSelectPurchaseDetail(purchaseId);

            MySqlDataAdapter dataAdp = new(query, conn);
            _ = dataAdp.Fill(dt);

            return dt;

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
            //TPurchaseDto purchaseDto = new(
            //    DatePicker.Value,
            //    ComboOffice.SelectedValue,
            //    ComboManager.SelectedValue,
            //    SlipNumber.Text,
            //    Memo.Text);
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
                    //purchaseDetailDtos.Add(new(
                    //    purchaseId: purchaseId,
                    //    branchId: (sbyte)rows.Cells[0].Value,
                    //    description: (string)rows.Cells[1].Value,
                    //    receivable: (int)rows.Cells[2].Value,
                    //    payable: (int)rows.Cells[3].Value,
                    //    useForFood: (bool)rows.Cells[4].Value));
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
        private bool CheckValid()
        {
            bool ret = true;
            // 日付
            // 未来日付はNG
            if (DatePicker.Value > DateTime.Now)
            {
                ComControl.SetErrorLabelColor(DatePickerLabel);
                MessageArea.Text += string.Empty;
                ret = false;
            }
            else
            {
                ComControl.SetClearLabelColor(DatePickerLabel);
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
