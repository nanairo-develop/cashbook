namespace Cashbook
{
    partial class FormPurchaseList
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            Search = new Button();
            PurchaseList = new DataGridView();
            Create = new Button();
            DateFrom = new UserDateCombo();
            DateTo = new UserDateCombo();
            ComboOffice = new ComboBox();
            ComboOfficeLabel = new Label();
            ComboManager = new ComboBox();
            ComboManagerLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)PurchaseList).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(260, 41);
            label1.Name = "label1";
            label1.Size = new Size(19, 15);
            label1.TabIndex = 2;
            label1.Text = "To";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 41);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 3;
            label2.Text = "From";
            // 
            // Search
            // 
            Search.Location = new Point(713, 41);
            Search.Name = "Search";
            Search.Size = new Size(75, 23);
            Search.TabIndex = 4;
            Search.Text = "検索";
            Search.UseVisualStyleBackColor = true;
            Search.Click += Search_Click;
            // 
            // PurchaseList
            // 
            PurchaseList.AllowUserToAddRows = false;
            PurchaseList.AllowUserToDeleteRows = false;
            PurchaseList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            PurchaseList.Location = new Point(12, 70);
            PurchaseList.Name = "PurchaseList";
            PurchaseList.ReadOnly = true;
            PurchaseList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            PurchaseList.Size = new Size(776, 318);
            PurchaseList.TabIndex = 5;
            PurchaseList.CellDoubleClick += PurchaseList_CellDoubleClick;
            // 
            // Create
            // 
            Create.Location = new Point(713, 394);
            Create.Name = "Create";
            Create.Size = new Size(75, 23);
            Create.TabIndex = 6;
            Create.Text = "新規登録";
            Create.UseVisualStyleBackColor = true;
            Create.Click += Create_Click;
            // 
            // DateFrom
            // 
            DateFrom.DateTimeFrom = new DateTime(0L);
            DateFrom.IntDay = 8;
            DateFrom.IntMonth = 5;
            DateFrom.IntYear = 2023;
            DateFrom.Location = new Point(51, 35);
            DateFrom.Name = "DateFrom";
            DateFrom.Size = new Size(203, 23);
            DateFrom.TabIndex = 1;
            DateFrom.TextDay = "8";
            DateFrom.TextMonth = "5";
            DateFrom.TextYear = "2023";
            DateFrom.Value = new DateTime(2023, 5, 8, 0, 0, 0, 0);
            // 
            // DateTo
            // 
            DateTo.DateTimeFrom = new DateTime(0L);
            DateTo.IntDay = 1;
            DateTo.IntMonth = 1;
            DateTo.IntYear = 2020;
            DateTo.Location = new Point(285, 35);
            DateTo.Name = "DateTo";
            DateTo.Size = new Size(203, 23);
            DateTo.TabIndex = 2;
            DateTo.TextDay = "1";
            DateTo.TextMonth = "1";
            DateTo.TextYear = "2020";
            DateTo.Value = new DateTime(2020, 1, 1, 0, 0, 0, 0);
            // 
            // ComboOffice
            // 
            ComboOffice.FormattingEnabled = true;
            ComboOffice.Location = new Point(85, 6);
            ComboOffice.Name = "ComboOffice";
            ComboOffice.Size = new Size(285, 23);
            ComboOffice.TabIndex = 8;
            ComboOffice.KeyDown += ComboOffice_KeyDown;
            // 
            // ComboOfficeLabel
            // 
            ComboOfficeLabel.AutoSize = true;
            ComboOfficeLabel.Location = new Point(12, 9);
            ComboOfficeLabel.Name = "ComboOfficeLabel";
            ComboOfficeLabel.Size = new Size(67, 15);
            ComboOfficeLabel.TabIndex = 7;
            ComboOfficeLabel.Text = "相手先名称";
            // 
            // ComboManager
            // 
            ComboManager.FormattingEnabled = true;
            ComboManager.Location = new Point(425, 6);
            ComboManager.Name = "ComboManager";
            ComboManager.Size = new Size(121, 23);
            ComboManager.TabIndex = 9;
            // 
            // ComboManagerLabel
            // 
            ComboManagerLabel.AutoSize = true;
            ComboManagerLabel.Location = new Point(376, 9);
            ComboManagerLabel.Name = "ComboManagerLabel";
            ComboManagerLabel.Size = new Size(43, 15);
            ComboManagerLabel.TabIndex = 10;
            ComboManagerLabel.Text = "担当者";
            // 
            // FormPurchaseList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ComboManager);
            Controls.Add(ComboManagerLabel);
            Controls.Add(ComboOffice);
            Controls.Add(ComboOfficeLabel);
            Controls.Add(DateTo);
            Controls.Add(DateFrom);
            Controls.Add(Create);
            Controls.Add(PurchaseList);
            Controls.Add(Search);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FormPurchaseList";
            Text = "伝票一覧-FormPurchaseList";
            Load += FormPurchaseList_Load;
            ((System.ComponentModel.ISupportInitialize)PurchaseList).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button Search;
        private DataGridView PurchaseList;
        private Button Create;
        private UserDateCombo DateFrom;
        private UserDateCombo DateTo;
        private ComboBox ComboOffice;
        private Label ComboOfficeLabel;
        private ComboBox ComboManager;
        private Label ComboManagerLabel;
    }
}