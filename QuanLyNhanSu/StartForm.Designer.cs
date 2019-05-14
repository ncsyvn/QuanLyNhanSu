namespace QuanLyNhanSu
{
    partial class StartForm
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
            this.buttonDanhMuc = new System.Windows.Forms.Button();
            this.buttonNghiepVu = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonDanhMuc
            // 
            this.buttonDanhMuc.Location = new System.Drawing.Point(187, 109);
            this.buttonDanhMuc.Name = "buttonDanhMuc";
            this.buttonDanhMuc.Size = new System.Drawing.Size(75, 77);
            this.buttonDanhMuc.TabIndex = 0;
            this.buttonDanhMuc.UseVisualStyleBackColor = true;
            this.buttonDanhMuc.Click += new System.EventHandler(this.buttonDanhMuc_Click);
            // 
            // buttonNghiepVu
            // 
            this.buttonNghiepVu.Location = new System.Drawing.Point(305, 109);
            this.buttonNghiepVu.Name = "buttonNghiepVu";
            this.buttonNghiepVu.Size = new System.Drawing.Size(75, 77);
            this.buttonNghiepVu.TabIndex = 1;
            this.buttonNghiepVu.UseVisualStyleBackColor = true;
            this.buttonNghiepVu.Click += new System.EventHandler(this.buttonNghiepVu_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(168, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "QUẢN LÝ NHÂN SỰ";
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 314);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonNghiepVu);
            this.Controls.Add(this.buttonDanhMuc);
            this.Name = "StartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StartForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonDanhMuc;
        private System.Windows.Forms.Button buttonNghiepVu;
        private System.Windows.Forms.Label label1;
    }
}