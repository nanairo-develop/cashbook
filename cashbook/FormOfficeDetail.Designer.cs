namespace cashbook
{
    partial class FormOfficeDetail
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
            label1 = new Label();
            OfficeName = new TextBox();
            Order = new TextBox();
            label2 = new Label();
            Change = new Button();
            Insert = new Button();
            label3 = new Label();
            label4 = new Label();
            Id = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(56, 77);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 0;
            label1.Text = "名称";
            // 
            // OfficeName
            // 
            OfficeName.Location = new Point(203, 74);
            OfficeName.Name = "OfficeName";
            OfficeName.Size = new Size(200, 23);
            OfficeName.TabIndex = 1;
            // 
            // Order
            // 
            Order.ImeMode = ImeMode.Off;
            Order.Location = new Point(203, 131);
            Order.Name = "Order";
            Order.Size = new Size(200, 23);
            Order.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(56, 134);
            label2.Name = "label2";
            label2.Size = new Size(41, 15);
            label2.TabIndex = 3;
            label2.Text = "並び順";
            // 
            // Change
            // 
            Change.Location = new Point(328, 180);
            Change.Name = "Change";
            Change.Size = new Size(75, 23);
            Change.TabIndex = 4;
            Change.Text = "変更";
            Change.UseVisualStyleBackColor = true;
            // 
            // Insert
            // 
            Insert.Location = new Point(203, 180);
            Insert.Name = "Insert";
            Insert.Size = new Size(75, 23);
            Insert.TabIndex = 5;
            Insert.Text = "登録";
            Insert.UseVisualStyleBackColor = true;
            Insert.Click += Insert_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(56, 21);
            label3.Name = "label3";
            label3.Size = new Size(18, 15);
            label3.TabIndex = 6;
            label3.Text = "ID";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(203, 21);
            label4.Name = "label4";
            label4.Size = new Size(0, 15);
            label4.TabIndex = 7;
            // 
            // Id
            // 
            Id.Location = new Point(203, 18);
            Id.Name = "Id";
            Id.ReadOnly = true;
            Id.Size = new Size(200, 23);
            Id.TabIndex = 8;
            // 
            // FormOfficeDetail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(Id);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(Insert);
            Controls.Add(Change);
            Controls.Add(label2);
            Controls.Add(Order);
            Controls.Add(OfficeName);
            Controls.Add(label1);
            Name = "FormOfficeDetail";
            Text = "FormOfficeDetail";
            Load += FormOfficeDetail_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox OfficeName;
        private TextBox Order;
        private Label label2;
        private Button Change;
        private Button Insert;
        private Label label3;
        private Label label4;
        private TextBox Id;
    }
}