namespace FASTFOOD_DNC
{
    partial class KhachHang
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
            this.pnlNavigationKH = new System.Windows.Forms.Panel();
            this.pnlHeaderKH = new System.Windows.Forms.Panel();
            this.picLogoKH = new System.Windows.Forms.PictureBox();
            this.lblPageTitleKH = new System.Windows.Forms.Label();
            this.pictureBox1KH = new System.Windows.Forms.PictureBox();
            this.lblCurrentUserInfo = new System.Windows.Forms.Label();
            this.lnkSignOutKH = new System.Windows.Forms.LinkLabel();
            this.pnlNavigationKH.SuspendLayout();
            this.pnlHeaderKH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogoKH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1KH)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlNavigationKH
            // 
            this.pnlNavigationKH.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(40)))), ((int)(((byte)(30)))));
            this.pnlNavigationKH.Controls.Add(this.picLogoKH);
            this.pnlNavigationKH.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlNavigationKH.Location = new System.Drawing.Point(0, 0);
            this.pnlNavigationKH.Name = "pnlNavigationKH";
            this.pnlNavigationKH.Size = new System.Drawing.Size(230, 748);
            this.pnlNavigationKH.TabIndex = 0;
            // 
            // pnlHeaderKH
            // 
            this.pnlHeaderKH.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlHeaderKH.Controls.Add(this.lnkSignOutKH);
            this.pnlHeaderKH.Controls.Add(this.lblCurrentUserInfo);
            this.pnlHeaderKH.Controls.Add(this.pictureBox1KH);
            this.pnlHeaderKH.Controls.Add(this.lblPageTitleKH);
            this.pnlHeaderKH.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeaderKH.Location = new System.Drawing.Point(230, 0);
            this.pnlHeaderKH.Name = "pnlHeaderKH";
            this.pnlHeaderKH.Size = new System.Drawing.Size(997, 50);
            this.pnlHeaderKH.TabIndex = 2;
            // 
            // picLogoKH
            // 
            this.picLogoKH.Dock = System.Windows.Forms.DockStyle.Top;
            this.picLogoKH.Location = new System.Drawing.Point(0, 0);
            this.picLogoKH.Name = "picLogoKH";
            this.picLogoKH.Size = new System.Drawing.Size(230, 100);
            this.picLogoKH.TabIndex = 0;
            this.picLogoKH.TabStop = false;
            // 
            // lblPageTitleKH
            // 
            this.lblPageTitleKH.AutoSize = true;
            this.lblPageTitleKH.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPageTitleKH.Location = new System.Drawing.Point(0, 0);
            this.lblPageTitleKH.Name = "lblPageTitleKH";
            this.lblPageTitleKH.Size = new System.Drawing.Size(51, 19);
            this.lblPageTitleKH.TabIndex = 0;
            this.lblPageTitleKH.Text = "label1";
            this.lblPageTitleKH.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1KH
            // 
            this.pictureBox1KH.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1KH.Image = global::FASTFOOD_DNC.Properties.Resources.teacher_240_1128987;
            this.pictureBox1KH.Location = new System.Drawing.Point(938, 0);
            this.pictureBox1KH.Name = "pictureBox1KH";
            this.pictureBox1KH.Size = new System.Drawing.Size(59, 50);
            this.pictureBox1KH.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1KH.TabIndex = 1;
            this.pictureBox1KH.TabStop = false;
            // 
            // lblCurrentUserInfo
            // 
            this.lblCurrentUserInfo.AutoSize = true;
            this.lblCurrentUserInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblCurrentUserInfo.Location = new System.Drawing.Point(887, 0);
            this.lblCurrentUserInfo.Name = "lblCurrentUserInfo";
            this.lblCurrentUserInfo.Size = new System.Drawing.Size(51, 19);
            this.lblCurrentUserInfo.TabIndex = 2;
            this.lblCurrentUserInfo.Text = "label1";
            this.lblCurrentUserInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lnkSignOutKH
            // 
            this.lnkSignOutKH.AutoSize = true;
            this.lnkSignOutKH.Dock = System.Windows.Forms.DockStyle.Right;
            this.lnkSignOutKH.Location = new System.Drawing.Point(811, 0);
            this.lnkSignOutKH.Name = "lnkSignOutKH";
            this.lnkSignOutKH.Size = new System.Drawing.Size(76, 19);
            this.lnkSignOutKH.TabIndex = 3;
            this.lnkSignOutKH.TabStop = true;
            this.lnkSignOutKH.Text = "Đăng xuất";
            this.lnkSignOutKH.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // KhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 748);
            this.Controls.Add(this.pnlHeaderKH);
            this.Controls.Add(this.pnlNavigationKH);
            this.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "KhachHang";
            this.Text = "KhachHang";
            this.pnlNavigationKH.ResumeLayout(false);
            this.pnlHeaderKH.ResumeLayout(false);
            this.pnlHeaderKH.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogoKH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1KH)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlNavigationKH;
        private System.Windows.Forms.Panel pnlHeaderKH;
        private System.Windows.Forms.PictureBox picLogoKH;
        private System.Windows.Forms.Label lblPageTitleKH;
        private System.Windows.Forms.PictureBox pictureBox1KH;
        private System.Windows.Forms.Label lblCurrentUserInfo;
        private System.Windows.Forms.LinkLabel lnkSignOutKH;
    }
}