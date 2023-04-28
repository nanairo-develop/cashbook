namespace cashbook
{
    partial class UserDateCombo
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            Year = new ComboBox();
            Month = new ComboBox();
            Day = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            DatePicker = new DateTimePicker();
            SuspendLayout();
            // 
            // Year
            // 
            Year.FormattingEnabled = true;
            Year.Location = new Point(12, 10);
            Year.Name = "Year";
            Year.Size = new Size(66, 23);
            Year.TabIndex = 0;
            Year.SelectedIndexChanged += Year_SelectedIndexChanged;
            // 
            // Month
            // 
            Month.FormattingEnabled = true;
            Month.Location = new Point(102, 10);
            Month.Name = "Month";
            Month.Size = new Size(66, 23);
            Month.TabIndex = 1;
            Month.SelectedIndexChanged += Month_SelectedIndexChanged;
            // 
            // Day
            // 
            Day.FormattingEnabled = true;
            Day.Location = new Point(192, 10);
            Day.Name = "Day";
            Day.Size = new Size(66, 23);
            Day.TabIndex = 2;
            Day.SelectedIndexChanged += Day_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(84, 13);
            label1.Name = "label1";
            label1.Size = new Size(12, 15);
            label1.TabIndex = 3;
            label1.Text = "/";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(174, 13);
            label2.Name = "label2";
            label2.Size = new Size(12, 15);
            label2.TabIndex = 4;
            label2.Text = "/";
            // 
            // DatePicker
            // 
            DatePicker.Location = new Point(8, 7);
            DatePicker.MaxDate = new DateTime(2030, 12, 31, 0, 0, 0, 0);
            DatePicker.MinDate = new DateTime(2020, 1, 1, 0, 0, 0, 0);
            DatePicker.Name = "DatePicker";
            DatePicker.Size = new Size(200, 23);
            DatePicker.TabIndex = 5;
            DatePicker.Visible = false;
            // 
            // UserDateCombo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(Day);
            Controls.Add(Month);
            Controls.Add(Year);
            Controls.Add(DatePicker);
            Name = "UserDateCombo";
            Size = new Size(270, 40);
            Load += UserDateCombo_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox Year;
        private ComboBox Month;
        private ComboBox Day;
        private Label label1;
        private Label label2;
        private DateTimePicker DatePicker;
    }
}
