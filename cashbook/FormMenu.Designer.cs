namespace Cashbook
{
    partial class FormMenu
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
            PurchaseDetail = new Button();
            PurchaseList = new Button();
            OfficeList = new Button();
            SuspendLayout();
            // 
            // PurchaseDetail
            // 
            PurchaseDetail.Location = new Point(12, 12);
            PurchaseDetail.Name = "PurchaseDetail";
            PurchaseDetail.Size = new Size(141, 67);
            PurchaseDetail.TabIndex = 0;
            PurchaseDetail.Text = "伝票入力";
            PurchaseDetail.UseVisualStyleBackColor = true;
            PurchaseDetail.Click += PurchaseDetail_Click;
            // 
            // PurchaseList
            // 
            PurchaseList.Location = new Point(159, 12);
            PurchaseList.Name = "PurchaseList";
            PurchaseList.Size = new Size(141, 67);
            PurchaseList.TabIndex = 1;
            PurchaseList.Text = "伝票一覧";
            PurchaseList.UseVisualStyleBackColor = true;
            PurchaseList.Click += PurchaseList_Click;
            // 
            // OfficeList
            // 
            OfficeList.Location = new Point(306, 12);
            OfficeList.Name = "OfficeList";
            OfficeList.Size = new Size(141, 67);
            OfficeList.TabIndex = 2;
            OfficeList.Text = "仕入先一覧";
            OfficeList.UseVisualStyleBackColor = true;
            OfficeList.Click += OfficeList_Click;
            // 
            // FormMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(OfficeList);
            Controls.Add(PurchaseList);
            Controls.Add(PurchaseDetail);
            Name = "FormMenu";
            Text = "FormMenu";
            ResumeLayout(false);
        }

        #endregion

        private Button PurchaseDetail;
        private Button PurchaseList;
        private Button OfficeList;
    }
}