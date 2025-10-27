using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FASTFOOD_DNC
{
    public partial class frmKhachHang : Form
    {
        string connection = "Data Source=Huy\\SQLEXPRESS;Initial Catalog=FASTFOOD;Integrated Security=True";
        SqlConnection conn;
        private string currentusername;
        public frmKhachHang()
        {
            InitializeComponent();
        }
        public frmKhachHang(string v)
        {
            InitializeComponent();
            this.currentusername = v;
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            lblPageTitleKH.Text = "Menu";
            pnMenuKH.Controls.Clear();
            FASTFOOD_DNC.ucMenuKH menuControl = new FASTFOOD_DNC.ucMenuKH();
            menuControl.Dock = DockStyle.Fill;
            pnMenuKH.Controls.Add(menuControl);
            try
    {
        using (SqlConnection conn = new SqlConnection(connection))
        {
            conn.Open();
            // --- Bước 1: Lấy MAKH từ TAIKHOAN ---
            SqlCommand cmdTK = new SqlCommand("SELECT MAKH FROM TAIKHOAN WHERE TENDN = @TENDN", conn);
            cmdTK.Parameters.AddWithValue("@TENDN", this.currentusername);

            // FIX: Kiểm tra kết quả trước khi ép kiểu để tránh lỗi
            object maKHResult = cmdTK.ExecuteScalar();

            if (maKHResult != null && maKHResult != DBNull.Value)
            {
                int maKH = Convert.ToInt32(maKHResult);

                // --- Bước 2: Lấy TENKH từ KHACHHANG ---
                SqlCommand cmdKH = new SqlCommand("SELECT TENKH FROM KHACHHANG WHERE MAKH = @MAKH", conn);
                cmdKH.Parameters.AddWithValue("@MAKH", maKH);

                object tenKHResult = cmdKH.ExecuteScalar();

                if (tenKHResult != null)
                {
                    // Hiển thị tên khách hàng lên label
                    lblCurrentUserInfoKH.Text = "Xin chào, " + tenKHResult.ToString();
                }
            }
            else
            {
                // Xử lý trường hợp không tìm thấy tài khoản
                lblCurrentUserInfoKH.Text = "Khách vãng lai";
                MessageBox.Show("Không tìm thấy thông tin khách hàng cho tài khoản này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
        }

        private void btnMenuKH_Click(object sender, EventArgs e)
        {
            lblPageTitleKH.Text = "Menu";
            pnMenuKH.Controls.Clear();
            FASTFOOD_DNC.ucMenuKH menuControl = new FASTFOOD_DNC.ucMenuKH();
            menuControl.Dock = DockStyle.Fill;
            pnMenuKH.Controls.Add(menuControl);
        }

        private void btnDonHangKH_Click(object sender, EventArgs e)
        {
            lblPageTitleKH.Text = "Đơn Hàng";
            pnMenuKH.Controls.Clear();
            FASTFOOD_DNC.ucDonHangKH menuControl = new FASTFOOD_DNC.ucDonHangKH();
            menuControl.Dock = DockStyle.Fill;
            pnMenuKH.Controls.Add(menuControl);
        }

        private void btnGioHang_Click(object sender, EventArgs e)
        {
            lblPageTitleKH.Text = "Giỏ Hàng";
            pnMenuKH.Controls.Clear();
            FASTFOOD_DNC.ucGIoHang menuControl = new FASTFOOD_DNC.ucGIoHang();
            menuControl.Dock = DockStyle.Fill;
            pnMenuKH.Controls.Add(menuControl);
        }
    }
}
