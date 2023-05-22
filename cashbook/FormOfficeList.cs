using cashbook.common;
using MySqlConnector;
using System.Data;
using static cashbook.common.ComConst.Mode;
using static cashbook.dao.MOfficeDao;

namespace cashbook
{
    public partial class FormOfficeList : Form
    {
        /// <summary>
        /// 0:編集, 1:選択
        /// </summary>
        public int Mode { get; set; }
        public FormOfficeList()
        {
            InitializeComponent();
        }
        public FormOfficeList(int mode)
        {
            InitializeComponent();
            Mode = mode;
        }


        #region イベント
        private void FormOfficeList_Activated(object sender, EventArgs e)
        {
            // アクティブになった時にリストの行が存在すれば同じ条件で再度検索する
            if (OfficeList.RowCount > 0)
            {
                SearchOffice();
            }
        }

        private void Search_Click(object sender, EventArgs e)
        {
            SearchOffice();
        }

        private void OfficeList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                _ = MessageBox.Show("先頭行です");
            }
            else
            {
                if (Mode == edit)
                {
                    FormOfficeDetail.Param param = new()
                    {
                        id = (int)OfficeList.Rows[e.RowIndex].Cells[0].Value,
                        officeName = (string)OfficeList.Rows[e.RowIndex].Cells[1].Value,
                        displayOrder = OfficeList.Rows[e.RowIndex].Cells[2].Value ?? string.Empty
                    };
                    FormOfficeDetail formOfficeDetail = new(param);
                    formOfficeDetail.Show();
                }
                else if (Mode == select)
                {
                    FormPurchaseDetail? parentForm = (FormPurchaseDetail?)Owner;
                    if (parentForm is not null)
                    {
                        parentForm.OfficeId = (int)OfficeList.Rows[e.RowIndex].Cells[0].Value;
                    }
                    // 選択後、画面を閉じる
                    Close();
                }
            }
        }

        /// <summary>
        /// Enter押下時に検索する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OfficeName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchOffice();
            }
        }

        /// <summary>
        /// コントロールを離れた際に検索する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OfficeName_Leave(object sender, EventArgs e)
        {
            SearchOffice();
        }

        private void Create_Click(object sender, EventArgs e)
        {
            FormOfficeDetail formOfficeDetail = new();
            formOfficeDetail.Show();
        }

        #endregion イベント

        #region メソッド
        private void SearchOffice()
        {

            MySqlConnection conn = new(ComConst.connStr);
            try
            {
                // 接続を開く
                conn.Open();

                // データを取得するテーブル
                DataTable tbl = new();

                string query = GetSelectOffice(OfficeName.Text);

                // SQLを実行する
                MySqlDataAdapter dataAdp = new(query, conn);
                _ = dataAdp.Fill(tbl);

                // データグリッドに表示させる
                OfficeList.DataSource = tbl;
                OfficeList.Columns["id"].Visible = false;
                OfficeList.Columns["name"].Width = 240;

                conn.Close();

            }
            catch (MySqlException mse)
            {
                _ = MessageBox.Show(mse.Message, "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion メソッド

    }
}
