namespace cashbook.common
{
    internal class ComControl
    {
        public static void SetErrorLabelColor(Label label)
        {
            label.BackColor = Color.Red;
        }
        public static void SetClearLabelColor(Label label)
        {
            label.BackColor = SystemColors.Control;
        }

    }
}
