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
    }
}
