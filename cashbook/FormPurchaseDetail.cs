using cashbook.common;
using static cashbook.FormPurchaseDetailDao;
using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Reflection;

namespace cashbook
{
    public partial class FormPurchaseDetail : Form
    {
        int purchaseId;
        int managerId;
        int officeId;

        DataTable office = new();
        DataTable manager = new();

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
            PayDatePicker.Value = DateTime.Now;
            managerId = 0;
            officeId = 0;
            Insert.Enabled = true;
            Change.Enabled = false;
        }

        public FormPurchaseDetail(int id, DateTime payDate, int officeId, int managerId, string slipNumber)
        {
            InitializeComponent();
            purchaseId = id;
            PayDatePicker.Value = payDate;
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

            MySqlConnection conn = new(ComConst.connStr);

            // コンボボックスの設定
            string selectManager = GetSelectManager(PayDatePicker.Value);
            SetComboBox(manager, selectManager, ComboManager, "name", "id");
            string selectOffice = GetSelectOffice();
            SetComboBox(office, selectOffice, ComboOffice, "name", "id");

            // 接続を開く
            conn.Open();

            // 明細行の取得

            // データを取得するテーブル
            DataTable tbl = new();

            // クエリを作成する
            var query = GetSelectPurchaseDetail(purchaseId);

            MySqlDataAdapter dataAdp = new(query, conn);
            _ = dataAdp.Fill(tbl);

            DetailList.DataSource = tbl;

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
        #endregion イベント

        #region メソッド
        /// <summary>
        /// コンボボックスの選択候補をDBから設定する
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="query"></param>
        /// <param name="comboBox"></param>
        /// <param name="displayMember"></param>
        /// <param name="valueMember"></param>
        private static void SetComboBox(DataTable dt, string query, ComboBox comboBox, string displayMember, string valueMember)
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

        private void Insert_Click(object sender, EventArgs e)
        {
            // チェック処理
            if (!CheckValid())
            {
                return;
            };

            // Insert
            var query = ExecInsert();

            // 実行結果メッセージ表示

            // 画面再読み込み

        }

        private bool CheckValid()
        {
            bool ret = true;
            // 日付
            // 未来日付はNG
            if (PayDatePicker.Value > DateTime.Now)
            {
                ComControl.SetErrorLabelColor(PayDatePickerLabel);
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
            // 内容無しはNG
            // 金額無しはNG

            return ret;
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
        #endregion メソッド

        private void ComboOffice_KeyDown(object sender, KeyEventArgs e)
        {
            //コンボボックスのデータの更新の為
            DataView dv = office.DefaultView;
            //コンボボックスに入力された文字列でフィルター
            dv.RowFilter = "name LIKE '*" + ComboOffice.Text + "*'";
        }

        private string ExecInsert()
        {
            // ヘダー行Insert
            InsertPurchase();

            // ヘダー行を検索して明細テーブルのpurchaseIdを取得
            purchaseId = SelectPurchaseId();

            // 明細行Insert
            InsertPurchaseDetail();
            string query = $"""

                ;
                """;
            return query;
        }

        private void InsertPurchase()
        {
            MySqlConnection conn = new(ComConst.connStr);
            try
            {
                // 接続を開く
                conn.Open();
                string query = GetPurchaseInsert(
                    PayDatePicker.Value,
                    ComboOffice.SelectedValue,
                    ComboManager.SelectedValue,
                    SlipNumber.Text
                    );
                MySqlCommand command = new(query, conn);
                var result = command.ExecuteNonQuery();
                // 挿入されなかった場合
                if (result != 1)
                {
                    _ = MessageBox.Show("insert ERROR");
                }

                //クローズ
                conn.Close();
            }
            catch(MySqlException me)
            {
                _ = MessageBox.Show("ERROR: " + me.Message);
            }
        }

        private int SelectPurchaseId()
        {
            MySqlConnection conn = new(ComConst.connStr);

            // 接続を開く
            conn.Open();

            string query = GetSelectPurchaseId();

            DataTable dt = new();

            MySqlDataAdapter dataAdapter = new(query, conn);
            _ = dataAdapter.Fill(dt);

            return (int)dt.Rows[0]["id"];
        }
        private void InsertPurchaseDetail()
        {

        }
    }
}
