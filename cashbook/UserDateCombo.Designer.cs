namespace Cashbook
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
            label1 = new Label();
            label2 = new Label();
            DatePicker = new DateTimePicker();
            Year = new NumericUpDown();
            Month = new NumericUpDown();
            Day = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)Year).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Month).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Day).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(65, 3);
            label1.Name = "label1";
            label1.Size = new Size(12, 15);
            label1.TabIndex = 3;
            label1.Text = "/";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(134, 3);
            label2.Name = "label2";
            label2.Size = new Size(12, 15);
            label2.TabIndex = 4;
            label2.Text = "/";
            // 
            // DatePicker
            // 
            DatePicker.Location = new Point(203, 0);
            DatePicker.MaxDate = new DateTime(2030, 12, 31, 0, 0, 0, 0);
            DatePicker.MinDate = new DateTime(2020, 1, 1, 0, 0, 0, 0);
            DatePicker.Name = "DatePicker";
            DatePicker.Size = new Size(200, 23);
            DatePicker.TabIndex = 5;
            DatePicker.Visible = false;
            // 
            // Year
            // 
            Year.Location = new Point(0, 0);
            Year.Maximum = new decimal(new int[] { 2030, 0, 0, 0 });
            Year.Minimum = new decimal(new int[] { 2020, 0, 0, 0 });
            Year.Name = "Year";
            Year.Size = new Size(59, 23);
            Year.TabIndex = 6;
            Year.Value = new decimal(new int[] { 2020, 0, 0, 0 });
            Year.ValueChanged += Year_ValueChanged;
            // 
            // Month
            // 
            Month.Location = new Point(83, 0);
            Month.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            Month.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            Month.Name = "Month";
            Month.Size = new Size(45, 23);
            Month.TabIndex = 7;
            Month.Value = new decimal(new int[] { 12, 0, 0, 0 });
            Month.ValueChanged += Month_ValueChanged;
            // 
            // Day
            // 
            Day.Location = new Point(152, 0);
            Day.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            Day.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            Day.Name = "Day";
            Day.Size = new Size(45, 23);
            Day.TabIndex = 8;
            Day.Value = new decimal(new int[] { 12, 0, 0, 0 });
            Day.ValueChanged += Day_ValueChanged;
            // 
            // UserDateCombo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(Day);
            Controls.Add(Month);
            Controls.Add(Year);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(DatePicker);
            Name = "UserDateCombo";
            Size = new Size(406, 23);
            Load += UserDateCombo_Load;
            ((System.ComponentModel.ISupportInitialize)Year).EndInit();
            ((System.ComponentModel.ISupportInitialize)Month).EndInit();
            ((System.ComponentModel.ISupportInitialize)Day).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private DateTimePicker DatePicker;
        private NumericUpDown Year;
        private NumericUpDown Month;
        private NumericUpDown Day;
    }
}
