namespace FASTFOOD_DNC
{
    partial class ucMenuKH
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvMenuKH = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtMaMon = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.numDonGia = new System.Windows.Forms.NumericUpDown();
            this.cbxLoaiMon = new System.Windows.Forms.ComboBox();
            this.txtTenMon = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMenuKH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDonGia)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(52)))), ((int)(((byte)(54)))));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1839, 51);
            this.label1.TabIndex = 0;
            this.label1.Text = "Menu";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 51);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvMenuKH);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContainer2.Panel2.Controls.Add(this.txtMaMon);
            this.splitContainer2.Panel2.Controls.Add(this.label6);
            this.splitContainer2.Panel2.Controls.Add(this.txtMoTa);
            this.splitContainer2.Panel2.Controls.Add(this.numDonGia);
            this.splitContainer2.Panel2.Controls.Add(this.cbxLoaiMon);
            this.splitContainer2.Panel2.Controls.Add(this.txtTenMon);
            this.splitContainer2.Panel2.Controls.Add(this.btnThem);
            this.splitContainer2.Panel2.Controls.Add(this.label5);
            this.splitContainer2.Panel2.Controls.Add(this.label4);
            this.splitContainer2.Panel2.Controls.Add(this.label3);
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Size = new System.Drawing.Size(1839, 620);
            this.splitContainer2.SplitterDistance = 1012;
            this.splitContainer2.TabIndex = 1;
            // 
            // dgvMenuKH
            // 
            this.dgvMenuKH.AllowUserToAddRows = false;
            this.dgvMenuKH.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.dgvMenuKH.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMenuKH.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMenuKH.BackgroundColor = System.Drawing.Color.White;
            this.dgvMenuKH.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMenuKH.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMenuKH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMenuKH.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column1,
            this.Column2,
            this.Column3});
            this.dgvMenuKH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMenuKH.Location = new System.Drawing.Point(0, 0);
            this.dgvMenuKH.Name = "dgvMenuKH";
            this.dgvMenuKH.ReadOnly = true;
            this.dgvMenuKH.RowHeadersVisible = false;
            this.dgvMenuKH.RowHeadersWidth = 51;
            this.dgvMenuKH.RowTemplate.Height = 24;
            this.dgvMenuKH.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMenuKH.Size = new System.Drawing.Size(1012, 620);
            this.dgvMenuKH.TabIndex = 0;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "MaMon";
            this.Column4.HeaderText = "Mã món";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "TenMon";
            this.Column1.HeaderText = "Tên món";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "LoaiMon";
            this.Column2.HeaderText = "Loại món";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "DonGia";
            this.Column3.HeaderText = "Đơn giá";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // txtMaMon
            // 
            this.txtMaMon.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.txtMaMon.Location = new System.Drawing.Point(105, 58);
            this.txtMaMon.Name = "txtMaMon";
            this.txtMaMon.ReadOnly = true;
            this.txtMaMon.Size = new System.Drawing.Size(255, 30);
            this.txtMaMon.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.label6.Location = new System.Drawing.Point(20, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 23);
            this.label6.TabIndex = 10;
            this.label6.Text = "Mã món:";
            // 
            // txtMoTa
            // 
            this.txtMoTa.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.txtMoTa.Location = new System.Drawing.Point(105, 367);
            this.txtMoTa.Multiline = true;
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.ReadOnly = true;
            this.txtMoTa.Size = new System.Drawing.Size(337, 220);
            this.txtMoTa.TabIndex = 9;
            // 
            // numDonGia
            // 
            this.numDonGia.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.numDonGia.Location = new System.Drawing.Point(105, 286);
            this.numDonGia.Name = "numDonGia";
            this.numDonGia.ReadOnly = true;
            this.numDonGia.Size = new System.Drawing.Size(255, 30);
            this.numDonGia.TabIndex = 8;
            // 
            // cbxLoaiMon
            // 
            this.cbxLoaiMon.Enabled = false;
            this.cbxLoaiMon.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbxLoaiMon.FormattingEnabled = true;
            this.cbxLoaiMon.Location = new System.Drawing.Point(105, 212);
            this.cbxLoaiMon.Name = "cbxLoaiMon";
            this.cbxLoaiMon.Size = new System.Drawing.Size(255, 31);
            this.cbxLoaiMon.TabIndex = 7;
            // 
            // txtTenMon
            // 
            this.txtTenMon.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.txtTenMon.Location = new System.Drawing.Point(105, 136);
            this.txtTenMon.Name = "txtTenMon";
            this.txtTenMon.ReadOnly = true;
            this.txtTenMon.Size = new System.Drawing.Size(255, 30);
            this.txtTenMon.TabIndex = 6;
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(116)))), ((int)(((byte)(217)))));
            this.btnThem.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(0, 529);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(823, 91);
            this.btnThem.TabIndex = 5;
            this.btnThem.Text = "Thêm vào giỏ";
            this.btnThem.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.label5.Location = new System.Drawing.Point(16, 367);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 23);
            this.label5.TabIndex = 3;
            this.label5.Text = "Mô tả:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.label4.Location = new System.Drawing.Point(16, 288);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 23);
            this.label4.TabIndex = 2;
            this.label4.Text = "Đơn giá:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.label3.Location = new System.Drawing.Point(16, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 23);
            this.label3.TabIndex = 1;
            this.label3.Text = "Loại món:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.label2.Location = new System.Drawing.Point(16, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tên món:";
            // 
            // ucMenuKH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucMenuKH";
            this.Size = new System.Drawing.Size(1839, 671);
            this.Load += new System.EventHandler(this.ucMenuKH_Load);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMenuKH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDonGia)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblThemVaoGioHang;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.Label lblTenMon;
        private System.Windows.Forms.PictureBox picMonAn;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgvMenuKH;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.NumericUpDown numDonGia;
        private System.Windows.Forms.ComboBox cbxLoaiMon;
        private System.Windows.Forms.TextBox txtTenMon;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.TextBox txtMaMon;
        private System.Windows.Forms.Label label6;
    }
}
