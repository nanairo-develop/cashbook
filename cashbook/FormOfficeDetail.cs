using cashbook.common;
using cashbook.dto;
using MySqlConnector;
using static cashbook.common.ComControl;
using static cashbook.common.ComDataBaseAccess;
using static cashbook.common.ComValidation;
using static cashbook.dao.MOfficeDao;

namespace cashbook
{
    public partial class FormOfficeDetail : Form
    {
        #region メンバ変数
        private MOfficeDto? officeDto;
        public List<Label> labels = new();

        /// <summary>コンストラクタで渡されるパラメータ</summary>
        public struct Param
        {
            /// <summary>m_officeの主キー</summary>
            public int id;
            /// <summary>名称</summary>
            public string officeName;
            /// <summary>並び順</summary>
            public object displayOrder;
        }
        #endregion メンバ変数

        #region コンストラクタ
        /// <summary>
        /// 新規登録時のコンストラクタ
        /// </summary>
        public FormOfficeDetail()
        {
            InitializeComponent();
            Insert.Enabled = true;
            Change.Enabled = false;
        }

        /// <summary>
        /// 更新時のコンストラクタ
        /// </summary>
        /// <param name="param"></param>
        public FormOfficeDetail(Param param)
        {
            InitializeComponent();
            Id.Text = param.id.ToString();
            OfficeName.Text = param.officeName;
            // 名称の変更は許可しない
            OfficeName.ReadOnly = true;
            Order.Text = param.displayOrder.ToString();
            Insert.Enabled = false;
            Change.Enabled = true;
        }
        #endregion コンストラクタ

        #region イベント
        private void FormOfficeDetail_Load(object sender, EventArgs e)
        {
            labels.Add(OfficeNameLabel);
            labels.Add(OrderLabel);
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            if (HasSimpleError(labels))
            {
                // エラーがあるため、処理を中断する
                return;
            }
            else
            {
                // メッセージエリア初期化
                MessageArea.Text = string.Empty;
            }

            // 通常色設定
            SetClearColor(labels);

            // パラメーター設定
            officeDto = GetOfficeDto();
            if (officeDto is null)
            {
                MessageArea.Text += "パラメーターエラー";
                return;
            }
            // 重複チェック処理
            // 名称 重複は許可しない
            string query = GetSelectOffice(officeDto);
            if (IsDuplicateOffice(query))
            {
                MessageArea.Text += "名称が重複しているため登録できません";
                SetErrorLabelColor(OfficeNameLabel);
                return;
            }
            InsertOffice();
        }

        private void Change_Click(object sender, EventArgs e)
        {
            if (HasSimpleError(labels))
            {
                return;
            }
            else
            {
                // メッセージエリア初期化
                MessageArea.Text = string.Empty;
            }

            // 通常色設定
            SetClearColor(labels);

            // パラメーター設定
            officeDto = GetOfficeDto();
            officeDto.Id = int.Parse(Id.Text);
            if (officeDto is null)
            {
                MessageArea.Text += "パラメーターエラー";
                return;
            }
            UpdateOffice();
        }

        /// <summary>
        /// 名称の入力チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OfficeName_Leave(object sender, EventArgs e)
        {
            // 名称は空白NG
            MessageArea.Text = IsEmpty(OfficeName, OfficeNameLabel);
        }

        /// <summary>
        /// 並び順の入力チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Order_Leave(object sender, EventArgs e)
        {
            // 空白の場合はチェックしない
            if (Order.Text != string.Empty)
            {
                MessageArea.Text = IsInt(Order, OrderLabel);
            }
        }
        #endregion イベント

        #region メソッド
        private void InsertOffice()
        {
            using MySqlConnection conn = new(ComConst.connStr);
            // 接続を開く
            conn.Open();
            using MySqlTransaction transaction = conn.BeginTransaction();
            try
            {
                using MySqlCommand command = conn.CreateCommand();
                if (officeDto is not null)
                {
                    command.CommandText = GetInsertOffice(officeDto);
                }
                command.Transaction = transaction;

                int result = command.ExecuteNonQuery();
                // 挿入されなかった場合
                if (result != 1)
                {
                    _ = MessageBox.Show("insert ERROR");
                }
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

        private void UpdateOffice()
        {
            using MySqlConnection conn = new(ComConst.connStr);
            // 接続を開く
            conn.Open();
            using MySqlTransaction transaction = conn.BeginTransaction();
            try
            {
                using MySqlCommand command = conn.CreateCommand();
                if (officeDto is not null)
                {
                    command.CommandText = GetUpdateOffice(officeDto);
                }
                command.Transaction = transaction;

                int result = command.ExecuteNonQuery();
                // 更新されなかった場合
                if (result != 1)
                {
                    _ = MessageBox.Show("update ERROR");
                }
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

        private MOfficeDto GetOfficeDto()
        {
            MOfficeDto officeDto = new(OfficeName.Text)
            {
                DisplayOrder = Order.Text == string.Empty ? null : int.Parse(Order.Text)
            };
            if (officeDto.Error != string.Empty)
            {
                _ = MessageBox.Show(officeDto.Error);
            }
            return officeDto;
        }
        #endregion メソッド
    }
}
