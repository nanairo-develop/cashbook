using System.ComponentModel.DataAnnotations;

namespace Cashbook
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
            OfficeNameLabel = new Label();
            OfficeName = new TextBox();
            Order = new TextBox();
            OrderLabel = new Label();
            Change = new Button();
            Insert = new Button();
            label3 = new Label();
            label4 = new Label();
            Id = new TextBox();
            MessageArea = new TextBox();
            SuspendLayout();
            // 
            // OfficeNameLabel
            // 
            OfficeNameLabel.AutoSize = true;
            OfficeNameLabel.Location = new Point(56, 77);
            OfficeNameLabel.Name = "OfficeNameLabel";
            OfficeNameLabel.Size = new Size(31, 15);
            OfficeNameLabel.TabIndex = 0;
            OfficeNameLabel.Text = "名称";
            // 
            // OfficeName
            // 
            OfficeName.Location = new Point(203, 74);
            OfficeName.Name = "OfficeName";
            OfficeName.Size = new Size(200, 23);
            OfficeName.TabIndex = 1;
            OfficeName.Leave += OfficeName_Leave;
            // 
            // Order
            // 
            Order.ImeMode = ImeMode.Off;
            Order.Location = new Point(203, 131);
            Order.Name = "Order";
            Order.Size = new Size(200, 23);
            Order.TabIndex = 2;
            Order.Leave += Order_Leave;
            // 
            // OrderLabel
            // 
            OrderLabel.AutoSize = true;
            OrderLabel.Location = new Point(56, 134);
            OrderLabel.Name = "OrderLabel";
            OrderLabel.Size = new Size(41, 15);
            OrderLabel.TabIndex = 3;
            OrderLabel.Text = "並び順";
            // 
            // Change
            // 
            Change.Location = new Point(328, 180);
            Change.Name = "Change";
            Change.Size = new Size(75, 23);
            Change.TabIndex = 4;
            Change.Text = "変更";
            Change.UseVisualStyleBackColor = true;
            Change.Click += Change_Click;
            // 
            // Insert
            // 
            Insert.Location = new Point(203, 180);
            Insert.Name = "Insert";
            Insert.Size = new Size(75, 23);
            Insert.TabIndex = 3;
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
            // MessageArea
            // 
            MessageArea.BackColor = Color.NavajoWhite;
            MessageArea.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            MessageArea.Location = new Point(12, 364);
            MessageArea.Multiline = true;
            MessageArea.Name = "MessageArea";
            MessageArea.ScrollBars = ScrollBars.Vertical;
            MessageArea.Size = new Size(674, 74);
            MessageArea.TabIndex = 12;
            // 
            // FormOfficeDetail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(MessageArea);
            Controls.Add(Id);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(Insert);
            Controls.Add(Change);
            Controls.Add(OrderLabel);
            Controls.Add(Order);
            Controls.Add(OfficeName);
            Controls.Add(OfficeNameLabel);
            Name = "FormOfficeDetail";
            Text = "FormOfficeDetail";
            Load += FormOfficeDetail_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label OfficeNameLabel;
        private TextBox OfficeName;
        private TextBox Order;
        private Label OrderLabel;
        private Button Change;
        private Button Insert;
        private Label label3;
        private Label label4;
        private TextBox Id;
        private TextBox MessageArea;
    }
}