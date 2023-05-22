using MySqlConnector;

namespace cashbook.common
{
    internal class ComDataBaseAccess
    {
        #region メソッド
        #region チェック処理
        /// <summary>
        /// 重複している場合trueを返す
        /// </summary>
        /// <param name="query">select文</param>
        /// <returns>true:重複あり, false:重複なし</returns>
        public static bool IsDuplicateOffice(string query)
        {
            bool ret = false;

            MySqlConnection conn = new(ComConst.connStr);
            try
            {
                // 接続を開く
                conn.Open();

                // SQLを実行する
                using MySqlCommand command = conn.CreateCommand();
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    ret = true;
                }
                conn.Close();
            }
            catch (MySqlException mse)
            {
                _ = MessageBox.Show(mse.Message, "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ret;
        }
        #endregion チェック処理
        #endregion メソッド
    }
}
