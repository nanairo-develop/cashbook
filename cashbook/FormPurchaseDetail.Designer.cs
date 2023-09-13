namespace Cashbook
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
            DatePickerLabel = new Label();
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
            Memo = new TextBox();
            OfficeSelect = new Button();
            RowAdd = new Button();
            NewInsert = new Button();
            ((System.ComponentModel.ISupportInitialize)DetailList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SumData).BeginInit();
            SuspendLayout();
            // 
            // DatePickerLabel
            // 
            DatePickerLabel.AutoSize = true;
            DatePickerLabel.Location = new Point(12, 9);
            DatePickerLabel.Name = "DatePickerLabel";
            DatePickerLabel.Size = new Size(31, 15);
            DatePickerLabel.TabIndex = 0;
            DatePickerLabel.Text = "日付";
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
            ComboManagerLabel.Location = new Point(460, 9);
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
            ComboManager.Size = new Size(100, 23);
            ComboManager.TabIndex = 2;
            ComboManager.KeyDown += ComboManager_KeyDown;
            ComboManager.Leave += ComboManager_Leave;
            // 
            // ComboOffice
            // 
            ComboOffice.FormattingEnabled = true;
            ComboOffice.Location = new Point(85, 42);
            ComboOffice.Name = "ComboOffice";
            ComboOffice.Size = new Size(285, 23);
            ComboOffice.TabIndex = 3;
            ComboOffice.KeyDown += ComboOffice_KeyDown;
            ComboOffice.Leave += ComboOffice_Leave;
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
            DetailList.Size = new Size(674, 227);
            DetailList.TabIndex = 5;
            DetailList.CellEnter += DetailList_CellEnter;
            DetailList.CellLeave += DetailList_CellLeave;
            // 
            // SumData
            // 
            SumData.AllowUserToDeleteRows = false;
            SumData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            SumData.Columns.AddRange(new DataGridViewColumn[] { SumReceivable, 支払合計 });
            SumData.Location = new Point(358, 304);
            SumData.Name = "SumData";
            SumData.RowTemplate.Height = 25;
            SumData.Size = new Size(247, 54);
            SumData.TabIndex = 7;
            SumData.TabStop = false;
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
            SlipNumber.Leave += SlipNumber_Leave;
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
            Change.Location = new Point(611, 333);
            Change.Name = "Change";
            Change.Size = new Size(75, 23);
            Change.TabIndex = 8;
            Change.Text = "更新";
            Change.UseVisualStyleBackColor = true;
            Change.Click += Change_Click;
            // 
            // MessageArea
            // 
            MessageArea.BackColor = Color.NavajoWhite;
            MessageArea.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            MessageArea.Location = new Point(12, 364);
            MessageArea.Multiline = true;
            MessageArea.Name = "MessageArea";
            MessageArea.ScrollBars = ScrollBars.Vertical;
            MessageArea.Size = new Size(593, 74);
            MessageArea.TabIndex = 9;
            // 
            // DatePicker
            // 
            DatePicker.DateTimeFrom = new DateTime(0L);
            DatePicker.IntDay = 2;
            DatePicker.IntMonth = 5;
            DatePicker.IntYear = 2023;
            DatePicker.Location = new Point(85, 6);
            DatePicker.Name = "DatePicker";
            DatePicker.Size = new Size(205, 23);
            DatePicker.TabIndex = 1;
            DatePicker.Value = new DateTime(2023, 5, 2, 0, 0, 0, 0);
            DatePicker.Leave += DatePicker_Leave;
            // 
            // Memo
            // 
            Memo.Location = new Point(12, 304);
            Memo.Multiline = true;
            Memo.Name = "Memo";
            Memo.ScrollBars = ScrollBars.Vertical;
            Memo.Size = new Size(340, 54);
            Memo.TabIndex = 6;
            // 
            // OfficeSelect
            // 
            OfficeSelect.Location = new Point(376, 42);
            OfficeSelect.Name = "OfficeSelect";
            OfficeSelect.Size = new Size(75, 23);
            OfficeSelect.TabIndex = 14;
            OfficeSelect.Text = "選択";
            OfficeSelect.UseVisualStyleBackColor = true;
            OfficeSelect.Click += OfficeSelect_Click;
            // 
            // RowAdd
            // 
            RowAdd.Location = new Point(627, 42);
            RowAdd.Name = "RowAdd";
            RowAdd.Size = new Size(59, 23);
            RowAdd.TabIndex = 15;
            RowAdd.Text = "行追加";
            RowAdd.UseVisualStyleBackColor = true;
            RowAdd.Click += RowAdd_Click;
            // 
            // NewInsert
            // 
            NewInsert.Location = new Point(611, 362);
            NewInsert.Name = "NewInsert";
            NewInsert.Size = new Size(75, 23);
            NewInsert.TabIndex = 16;
            NewInsert.Text = "新規入力";
            NewInsert.UseVisualStyleBackColor = true;
            NewInsert.Click += NewInsert_Click;
            // 
            // FormPurchaseDetail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 450);
            Controls.Add(NewInsert);
            Controls.Add(RowAdd);
            Controls.Add(OfficeSelect);
            Controls.Add(Memo);
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
            Controls.Add(DatePickerLabel);
            Name = "FormPurchaseDetail";
            StartPosition = FormStartPosition.Manual;
            Text = "FormPurchaseDetail";
            Activated += FormPurchaseDetail_Activated;
            Load += FormPurchaseDetail_Load;
            ((System.ComponentModel.ISupportInitialize)DetailList).EndInit();
            ((System.ComponentModel.ISupportInitialize)SumData).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label DatePickerLabel;
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
        private TextBox MessageArea;
        private UserDateCombo DatePicker;
        private TextBox Memo;
        private Button OfficeSelect;
        private Button RowAdd;
        private Button NewInsert;
    }
}