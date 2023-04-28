namespace cashbook
{
    partial class FormPurchaseDetail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            PayDatePickerLabel = new Label();
            ComboOfficeLabel = new Label();
            ComboManagerLabel = new Label();
            ComboManager = new ComboBox();
            ComboOffice = new ComboBox();
            DetailList = new DataGridView();
            SumData = new DataGridView();
            SumReceivable = new DataGridViewTextBoxColumn();
            支払合計 = new DataGridViewTextBoxColumn();
            Insert = new Button();
            SlipNumber = new TextBox();
            SlipNumberLabel = new Label();
            Change = new Button();
            MessageArea = new TextBox();
            DatePicker = new UserDateCombo();
            ((System.ComponentModel.ISupportInitialize)DetailList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SumData).BeginInit();
            SuspendLayout();
            // 
            // PayDatePickerLabel
            // 
            PayDatePickerLabel.AutoSize = true;
            PayDatePickerLabel.Location = new Point(12, 9);
            PayDatePickerLabel.Name = "PayDatePickerLabel";
            PayDatePickerLabel.Size = new Size(31, 15);
            PayDatePickerLabel.TabIndex = 0;
            PayDatePickerLabel.Text = "日付";
            // 
            // ComboOfficeLabel
            // 
            ComboOfficeLabel.AutoSize = true;
            ComboOfficeLabel.Location = new Point(12, 45);
            ComboOfficeLabel.Name = "ComboOfficeLabel";
            ComboOfficeLabel.Size = new Size(67, 15);
            ComboOfficeLabel.TabIndex = 1;
            ComboOfficeLabel.Text = "相手先名称";
            // 
            // ComboManagerLabel
            // 
            ComboManagerLabel.AutoSize = true;
            ComboManagerLabel.Location = new Point(472, 9);
            ComboManagerLabel.Name = "ComboManagerLabel";
            ComboManagerLabel.Size = new Size(43, 15);
            ComboManagerLabel.TabIndex = 2;
            ComboManagerLabel.Text = "担当者";
            // 
            // ComboManager
            // 
            ComboManager.FormattingEnabled = true;
            ComboManager.Location = new Point(521, 6);
            ComboManager.Name = "ComboManager";
            ComboManager.Size = new Size(121, 23);
            ComboManager.TabIndex = 2;
            // 
            // ComboOffice
            // 
            ComboOffice.FormattingEnabled = true;
            ComboOffice.Location = new Point(85, 42);
            ComboOffice.Name = "ComboOffice";
            ComboOffice.Size = new Size(285, 23);
            ComboOffice.TabIndex = 3;
            ComboOffice.KeyDown += ComboOffice_KeyDown;
            // 
            // DetailList
            // 
            DetailList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            DetailList.DefaultCellStyle = dataGridViewCellStyle2;
            DetailList.Location = new Point(12, 71);
            DetailList.Name = "DetailList";
            DetailList.RowTemplate.Height = 25;
            DetailList.Size = new Size(776, 227);
            DetailList.TabIndex = 5;
            DetailList.CellEnter += DetailList_CellEnter;
            DetailList.CellValidating += DetailList_CellValidating;
            // 
            // SumData
            // 
            SumData.AllowUserToDeleteRows = false;
            SumData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            SumData.Columns.AddRange(new DataGridViewColumn[] { SumReceivable, 支払合計 });
            SumData.Location = new Point(241, 304);
            SumData.Name = "SumData";
            SumData.RowTemplate.Height = 25;
            SumData.Size = new Size(283, 54);
            SumData.TabIndex = 7;
            // 
            // SumReceivable
            // 
            SumReceivable.HeaderText = "SumReceivable";
            SumReceivable.Name = "SumReceivable";
            SumReceivable.ReadOnly = true;
            // 
            // 支払合計
            // 
            支払合計.HeaderText = "SumPayable";
            支払合計.Name = "支払合計";
            支払合計.ReadOnly = true;
            // 
            // Insert
            // 
            Insert.Location = new Point(611, 304);
            Insert.Name = "Insert";
            Insert.Size = new Size(75, 23);
            Insert.TabIndex = 7;
            Insert.Text = "登録";
            Insert.UseVisualStyleBackColor = true;
            Insert.Click += Insert_Click;
            // 
            // SlipNumber
            // 
            SlipNumber.Location = new Point(521, 42);
            SlipNumber.Name = "SlipNumber";
            SlipNumber.Size = new Size(100, 23);
            SlipNumber.TabIndex = 4;
            // 
            // SlipNumberLabel
            // 
            SlipNumberLabel.AutoSize = true;
            SlipNumberLabel.Location = new Point(460, 45);
            SlipNumberLabel.Name = "SlipNumberLabel";
            SlipNumberLabel.Size = new Size(55, 15);
            SlipNumberLabel.TabIndex = 10;
            SlipNumberLabel.Text = "伝票番号";
            // 
            // Change
            // 
            Change.Location = new Point(530, 304);
            Change.Name = "Change";
            Change.Size = new Size(75, 23);
            Change.TabIndex = 6;
            Change.Text = "更新";
            Change.UseVisualStyleBackColor = true;
            // 
            // MessageArea
            // 
            MessageArea.BackColor = Color.NavajoWhite;
            MessageArea.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            MessageArea.Location = new Point(12, 364);
            MessageArea.Multiline = true;
            MessageArea.Name = "MessageArea";
            MessageArea.ScrollBars = ScrollBars.Vertical;
            MessageArea.Size = new Size(674, 74);
            MessageArea.TabIndex = 11;
            // 
            // DatePicker
            // 
            DatePicker.IntDay = 0;
            DatePicker.IntMonth = 0;
            DatePicker.IntYear = 0;
            DatePicker.Location = new Point(49, -4);
            DatePicker.Name = "DatePicker";
            DatePicker.Size = new Size(270, 40);
            DatePicker.TabIndex = 12;
            // 
            // FormPurchaseDetail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(DatePicker);
            Controls.Add(MessageArea);
            Controls.Add(Change);
            Controls.Add(SlipNumberLabel);
            Controls.Add(SlipNumber);
            Controls.Add(Insert);
            Controls.Add(SumData);
            Controls.Add(DetailList);
            Controls.Add(ComboOffice);
            Controls.Add(ComboManager);
            Controls.Add(ComboManagerLabel);
            Controls.Add(ComboOfficeLabel);
            Controls.Add(PayDatePickerLabel);
            Name = "FormPurchaseDetail";
            StartPosition = FormStartPosition.Manual;
            Text = "FormPurchaseDetail";
            Load += FormPurchaseDetail_Load;
            ((System.ComponentModel.ISupportInitialize)DetailList).EndInit();
            ((System.ComponentModel.ISupportInitialize)SumData).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label PayDatePickerLabel;
        private Label ComboOfficeLabel;
        private Label ComboManagerLabel;
        private ComboBox ComboManager;
        private ComboBox ComboOffice;
        private DataGridView DetailList;
        private DataGridView SumData;
        private DataGridViewTextBoxColumn SumReceivable;
        private DataGridViewTextBoxColumn 支払合計;
        private Button Insert;
        private TextBox SlipNumber;
        private Label SlipNumberLabel;
        private Button Change;
        private Label MessageLabel;
        private TextBox MessageArea;
        private UserDateCombo DatePicker;
    }
}