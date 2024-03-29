﻿using static Cashbook.common.ComControl;


namespace Cashbook.common
{
    internal class ComValidation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="object">フォームオブジェクトを設定</param>
        /// <param name="label"></param>
        /// <returns></returns>
        public static string CheckEmpty(object @object, Label label)
        {
            string errorMessage = string.Empty;
            bool empty = false;
            // 型のチェック
            if (@object.GetType() == typeof(TextBox))
            {
                TextBox textBox = (TextBox)@object;
                empty = textBox.Text == string.Empty;
            }

            // 空白のチェック
            if (empty)
            {
                SetErrorLabelColor(label);
                errorMessage = label.Text + "は必須です.";
            }
            // 通常色に戻す処理はエラーになったものを
            // 別のエラー処理で通常色に戻してしまう恐れがあるためここでは行わない

            return errorMessage;
        }

        public static string CheckInt(object @object, Label label)
        {
            string errorMessage = string.Empty;
            bool tryInt = false;
            // 型のチェック
            if (@object.GetType() == typeof(TextBox))
            {
                TextBox textBox = (TextBox)@object;
                tryInt = !int.TryParse(textBox.Text, out _);
            }

            // 整数のチェック
            if (tryInt)
            {
                SetErrorLabelColor(label);
                errorMessage = label.Text + "は整数で入力してください";
            }
            // 通常色に戻す処理はエラーになったものを
            // 別のエラー処理で通常色に戻してしまう恐れがあるためここでは行わない

            return errorMessage;
        }

        public static string CheckInt(ComboBox comboBox, string name)
        {
            string errorMessage = string.Empty;
            // 型のチェック
            bool tryInt = !int.TryParse(comboBox.Text, out _);

            // 整数のチェック
            if (tryInt)
            {
                SetErrorComboColor(comboBox);
                errorMessage = name + "は整数で入力してください";
            }
            // 通常色に戻す処理はエラーになったものを
            // 別のエラー処理で通常色に戻してしまう恐れがあるためここでは行わない

            return errorMessage;
        }

        public static string CheckGreaterThan(DateTime datetimeFirst, DateTime dateTimeSecond, Label label)
        {
            string errorMessage = string.Empty;

            if (datetimeFirst > dateTimeSecond)
            {
                SetErrorLabelColor(label);
                errorMessage = label.Text + "は右の日付より過去の日付を入力してください";
            }

            return errorMessage;
        }
        public static string CheckFuture(DateTime datetime, Label label)
        {
            string errorMessage = string.Empty;
            if (CheckGreaterThan(datetime, DateTime.Now, label) != string.Empty)
            {
                errorMessage = label.Text + "は本日より過去の日付を入力してください";
            }
            return errorMessage;
        }
        public static string CheckPast(DateTime datetime, Label label)
        {
            string warningMessage = string.Empty;
            // TODO: CheckFutureのコピー、手直し必要
            TimeSpan subst = new(30, 0, 0, 0);
            DateTime pastDate = DateTime.Now - subst;
            if (CheckGreaterThan(pastDate, datetime, label) != string.Empty)
            {
                warningMessage = label.Text + "が本日より30日以上古いです";
            }
            return warningMessage;
        }
        public static string NoSelection(ComboBox comboBox, Label label)
        {
            string errorMessage = string.Empty;
            if (comboBox.SelectedValue == null)
            {
                SetErrorLabelColor(label);
                errorMessage = label.Text + "を選択してください";
            }
            return errorMessage;
        }

        public static string CheckRowCount(DataGridView dataGridView)
        {
            string errorMessage = string.Empty;
            if (dataGridView.RowCount == 1)
            {
                SetErrorGridColor(dataGridView.DefaultCellStyle);
                errorMessage = "登録には1行以上入力してください";
            }
            return errorMessage;
        }

        /// <summary>
        /// エラーがある場合trueを返す
        /// </summary>
        /// <returns></returns>
        public static bool HasSimpleError(List<Label> labels)
        {
            bool ret = false;
            labels.ForEach(label => ret = ret || label.BackColor == Color.Red);
            return ret;
        }
    }
}
