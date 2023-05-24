using MySqlConnector;
using System.Data;

namespace cashbook.common
{
    internal class ComControl
    {
        #region Label
        public static void SetErrorLabelColor(Label label)
        {
            label.BackColor = Color.Red;
        }
        public static void SetClearLabelColor(Label label)
        {
            label.BackColor = SystemColors.Control;
        }
        public static void SetClearColor(List<Label> labels)
        {
            labels.ForEach(SetClearLabelColor);
        }
        #endregion Label

        #region DataGridViewCellStyle
        public static void SetErrorGridColor(DataGridViewCellStyle dataGridViewCellStyle)
        {
            dataGridViewCellStyle.BackColor = Color.Red;
        }
        public static void SetClearGridColor(DataGridViewCellStyle dataGridViewCellStyle)
        {
            dataGridViewCellStyle.BackColor = SystemColors.Window;
        }
        #endregion DataGridViewCellStyle

        #region ComboBox
        public struct ComboBoxParam
        {
            public DataTable dt;
            public string query;
            public ComboBox comboBox;
            public string displayMember;
            public string valueMember;
        }

        public static void SetComboBox(ComboBoxParam param)
        {
            MySqlConnection conn = new(ComConst.connStr);

            // 接続を開く
            conn.Open();

            // コンボボックスの設定
            DataTable combo = param.dt;

            MySqlDataAdapter dataAdapter = new(param.query, conn);
            _ = dataAdapter.Fill(combo);

            // テキストフィルターを実装するため、DataViewを経由する
            DataView dv = combo.DefaultView;

            param.comboBox.DataSource = dv;
            param.comboBox.DisplayMember = param.displayMember;
            param.comboBox.ValueMember = param.valueMember;

        }

        public static void Combo_KeyDown(DataTable dt, ComboBox comboBox)
        {
            // コンボボックスのデータの更新の為
            DataView dv = dt.DefaultView;
            // コンボボックスに入力された文字列でフィルター
            dv.RowFilter = "name LIKE '*" + comboBox.Text + "*'";
        }

        public static void SetErrorComboColor(ComboBox comboBox)
        {
            comboBox.BackColor = Color.Red;
        }
        public static void SetClearComboColor(ComboBox comboBox)
        {
            comboBox.BackColor = SystemColors.Window;
        }
        #endregion ComboBox
        #region NumericUpDown
        public static void SetErrorNumericUpDownColor(NumericUpDown numericUpDown)
        {
            numericUpDown.BackColor = Color.Red;
        }

        public static void SetClearNumericUpDownColor(NumericUpDown numericUpDown)
        {
            numericUpDown.BackColor = SystemColors.Window;
        }
        #endregion NumericUpDown

    }
}
