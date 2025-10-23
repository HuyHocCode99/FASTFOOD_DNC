namespace FASTFOOD_DNC
{
    partial class ucGIoHang
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvLichSuDonHang = new System.Windows.Forms.DataGridView();
            this.MaDonHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayDat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TongTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTieuDeChiTiet = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSuDonHang)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvLichSuDonHang);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblTieuDeChiTiet);
            this.splitContainer1.Size = new System.Drawing.Size(1079, 704);
            this.splitContainer1.SplitterDistance = 529;
            this.splitContainer1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(529, 60);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lịch sử đơn hàng";
            // 
            // dgvLichSuDonHang
            // 
            this.dgvLichSuDonHang.AllowUserToAddRows = false;
            this.dgvLichSuDonHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLichSuDonHang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaDonHang,
            this.NgayDat,
            this.TongTien,
            this.TrangThai});
            this.dgvLichSuDonHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLichSuDonHang.Location = new System.Drawing.Point(0, 60);
            this.dgvLichSuDonHang.Name = "dgvLichSuDonHang";
            this.dgvLichSuDonHang.ReadOnly = true;
            this.dgvLichSuDonHang.RowHeadersWidth = 51;
            this.dgvLichSuDonHang.RowTemplate.Height = 24;
            this.dgvLichSuDonHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLichSuDonHang.Size = new System.Drawing.Size(529, 644);
            this.dgvLichSuDonHang.TabIndex = 1;
            // 
            // MaDonHang
            // 
            this.MaDonHang.HeaderText = "Mã đơn hàng";
            this.MaDonHang.MinimumWidth = 6;
            this.MaDonHang.Name = "MaDonHang";
            this.MaDonHang.ReadOnly = true;
            this.MaDonHang.Width = 125;
            // 
            // NgayDat
            // 
            this.NgayDat.HeaderText = "Ngày đặt";
            this.NgayDat.MinimumWidth = 6;
            this.NgayDat.Name = "NgayDat";
            this.NgayDat.ReadOnly = true;
            this.NgayDat.Width = 125;
            // 
            // TongTien
            // 
            this.TongTien.HeaderText = "Tổng tiền";
            this.TongTien.MinimumWidth = 6;
            this.TongTien.Name = "TongTien";
            this.TongTien.ReadOnly = true;
            this.TongTien.Width = 125;
            // 
            // TrangThai
            // 
            this.TrangThai.HeaderText = "Trạng thái";
            this.TrangThai.MinimumWidth = 6;
            this.TrangThai.Name = "TrangThai";
            this.TrangThai.ReadOnly = true;
            this.TrangThai.Width = 125;
            // 
            // lblTieuDeChiTiet
            // 
            this.lblTieuDeChiTiet.AutoSize = true;
            this.lblTieuDeChiTiet.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTieuDeChiTiet.Location = new System.Drawing.Point(0, 0);
            this.lblTieuDeChiTiet.Name = "lblTieuDeChiTiet";
            this.lblTieuDeChiTiet.Size = new System.Drawing.Size(138, 20);
            this.lblTieuDeChiTiet.TabIndex = 0;
            this.lblTieuDeChiTiet.Text = "Chi tiết Đơn hàng";
            // 
            // ucGIoHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ucGIoHang";
            this.Size = new System.Drawing.Size(1079, 704);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSuDonHang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvLichSuDonHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaDonHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayDat;
        private System.Windows.Forms.DataGridViewTextBoxColumn TongTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrangThai;
        private System.Windows.Forms.Label lblTieuDeChiTiet;
    }
}
