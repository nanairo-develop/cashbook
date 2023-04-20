namespace cashbook
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
            DateFrom = new DateTimePicker();
            DateTo = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            Search = new Button();
            PurchaseList = new DataGridView();
            Create = new Button();
            ((System.ComponentModel.ISupportInitialize)PurchaseList).BeginInit();
            SuspendLayout();
            // 
            // DateFrom
            // 
            DateFrom.Location = new Point(357, 12);
            DateFrom.Name = "DateFrom";
            DateFrom.Size = new Size(200, 23);
            DateFrom.TabIndex = 0;
            DateFrom.Value = new DateTime(2022, 4, 1, 0, 0, 0, 0);
            // 
            // DateTo
            // 
            DateTo.Location = new Point(588, 12);
            DateTo.Name = "DateTo";
            DateTo.Size = new Size(200, 23);
            DateTo.TabIndex = 1;
            DateTo.Value = new DateTime(2022, 4, 30, 0, 0, 0, 0);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(563, 18);
            label1.Name = "label1";
            label1.Size = new Size(19, 15);
            label1.TabIndex = 2;
            label1.Text = "To";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(318, 18);
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
            PurchaseList.CellContentClick += SlipList_CellContentClick;
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
            // FormPurchaseList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(Create);
            Controls.Add(PurchaseList);
            Controls.Add(Search);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(DateTo);
            Controls.Add(DateFrom);
            Name = "FormPurchaseList";
            Text = "伝票一覧-FormPurchaseList";
            ((System.ComponentModel.ISupportInitialize)PurchaseList).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker DateFrom;
        private DateTimePicker DateTo;
        private Label label1;
        private Label label2;
        private Button Search;
        private DataGridView PurchaseList;
        private Button Create;
    }
}