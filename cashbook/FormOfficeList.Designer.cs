namespace cashbook
{
    partial class FormOfficeList
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
            OfficeList = new DataGridView();
            label1 = new Label();
            OfficeName = new TextBox();
            Search = new Button();
            Create = new Button();
            ((System.ComponentModel.ISupportInitialize)OfficeList).BeginInit();
            SuspendLayout();
            // 
            // OfficeList
            // 
            OfficeList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            OfficeList.Location = new Point(12, 41);
            OfficeList.Name = "OfficeList";
            OfficeList.RowTemplate.Height = 25;
            OfficeList.Size = new Size(473, 282);
            OfficeList.TabIndex = 0;
            OfficeList.CellDoubleClick += OfficeList_CellDoubleClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(67, 15);
            label1.TabIndex = 1;
            label1.Text = "事業所名称";
            // 
            // OfficeName
            // 
            OfficeName.ImeMode = ImeMode.Hiragana;
            OfficeName.Location = new Point(85, 12);
            OfficeName.Name = "OfficeName";
            OfficeName.Size = new Size(195, 23);
            OfficeName.TabIndex = 2;
            OfficeName.KeyUp += OfficeName_KeyUp;
            OfficeName.Leave += OfficeName_Leave;
            // 
            // Search
            // 
            Search.Location = new Point(286, 12);
            Search.Name = "Search";
            Search.Size = new Size(75, 23);
            Search.TabIndex = 3;
            Search.Text = "検索";
            Search.UseVisualStyleBackColor = true;
            Search.Click += Search_Click;
            // 
            // Create
            // 
            Create.Location = new Point(399, 11);
            Create.Name = "Create";
            Create.Size = new Size(75, 23);
            Create.TabIndex = 4;
            Create.Text = "新規登録";
            Create.UseVisualStyleBackColor = true;
            Create.Click += Create_Click;
            // 
            // FormOfficeList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(Create);
            Controls.Add(Search);
            Controls.Add(OfficeName);
            Controls.Add(label1);
            Controls.Add(OfficeList);
            Name = "FormOfficeList";
            Text = "FormOfficeList";
            Activated += FormOfficeList_Activated;
            ((System.ComponentModel.ISupportInitialize)OfficeList).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView OfficeList;
        private Label label1;
        private TextBox OfficeName;
        private Button Search;
        private Button Create;
    }
}