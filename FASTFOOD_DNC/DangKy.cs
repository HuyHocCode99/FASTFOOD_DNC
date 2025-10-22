using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FASTFOOD_DNC
{
    public partial class frmDangKy : Form
    {
        string connection = "Data Source=Huy\\SQLEXPRESS;Initial Catalog=FASTFOOD;Integrated Security=True";

        public frmDangKy()
        {
            InitializeComponent();
        }

        private void frmDangKy_Load(object sender, EventArgs e)
        {
            txtHovaTen.Enabled = true;
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            // ===== LỖI 1: KIỂM TRA SỐ ĐIỆN THOẠI =====
            if (string.IsNullOrWhiteSpace(txtHovaTen.Text) ||
                string.IsNullOrWhiteSpace(txtDiachi.Text) ||
                string.IsNullOrWhiteSpace(txtSoDT.Text) ||  // BẠN THIẾU KIỂM TRA NÀY
                string.IsNullOrWhiteSpace(txtMatkhauXN.Text) ||
                string.IsNullOrWhiteSpace(txtTentaikhoan.Text))
            {
                MessageBox.Show("Mời Bạn Nhập Đầy Đủ Thông Tin!", "Thông Báo!",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlConnection conn = null;
            SqlTransaction transaction = null; // ===== THÊM TRANSACTION =====

            try
            {
                conn = new SqlConnection(connection);
                conn.Open();

                // ===== BẎ MESSAGEBOX NÀY ĐI - KHÔNG CẦN THIẾT =====
                // MessageBox.Show("Kết Nối Cơ Sở Dữ Liệu Thành Công!", "Kết Nối Dữ Liệu");

                // ===== BẮT ĐẦU TRANSACTION =====
                transaction = conn.BeginTransaction();

                // ===== LỖI 2: TÊN CỘT TRONG DATABASE =====
                // Database của bạn có cột MATK chứ KHÔNG PHẢI TAIKHOAN_ID
                // Database của bạn có cột TENKH chứ KHÔNG PHẢI HOTEN
                // Database của bạn có cột DIACHIKH chứ KHÔNG PHẢI DIACHI
                // Database của bạn có cột SODT chứ KHÔNG PHẢI SDT

                string insertTaiKhoan = @"INSERT INTO TAIKHOAN(TENDN, MK, VAITRO) 
                                         VALUES(@TenDN, @MatKhau, N'Khach Hang');
                                         SELECT CAST(SCOPE_IDENTITY() AS INT);";

                SqlCommand cmdTaiKhoan = new SqlCommand(insertTaiKhoan, conn, transaction);
                cmdTaiKhoan.Parameters.AddWithValue("@TenDN", txtTentaikhoan.Text.Trim());
                cmdTaiKhoan.Parameters.AddWithValue("@MatKhau", txtMatKhau.Text);

                // Lấy MATK vừa được tạo
                int maTK = (int)cmdTaiKhoan.ExecuteScalar();

                // ===== SỬA LẠI TÊN CỘT CHO ĐÚNG VỚI DATABASE =====
                string insertKhachHang = @"INSERT INTO KHACHHANG(TENKH, DIACHIKH, SODT, MATK) 
                                          VALUES(@TenKH, @DiaChi, @SoDT, @MaTK)";

                SqlCommand cmdKhachHang = new SqlCommand(insertKhachHang, conn, transaction);
                cmdKhachHang.Parameters.AddWithValue("@TENKH", txtHovaTen.Text.Trim());
                cmdKhachHang.Parameters.AddWithValue("@DIACHIKH", txtDiachi.Text.Trim());
                cmdKhachHang.Parameters.AddWithValue("@SODT", txtSoDT.Text.Trim());
                cmdKhachHang.Parameters.AddWithValue("@MaTK", maTK);

                cmdKhachHang.ExecuteNonQuery();

                // ===== COMMIT TRANSACTION =====
                transaction.Commit();

                MessageBox.Show("Đăng ký tài khoản thành công!", "Thông báo",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ===== LỖI 3: CHUYỂN FORM SAI =====
                // Bạn tạo form nhưng không gọi Show()
                //frmDangNhap frmDN = new frmDangNhap(txtTentaikhoan.Text.Trim());
                //frmDN.Show();
                //this.Close();
            }
            catch (SqlException ex)
            {
                // ===== ROLLBACK KHI CÓ LỖI =====
                if (transaction != null)
                    transaction.Rollback();

                // ===== LỖI 4: XỬ LÝ LỖI KHÔNG RÕ RÀNG =====
                // Bạn chỉ hiện "Lỗi!" mà không biết lỗi gì
                if (ex.Number == 2627 || ex.Number == 2601) // Lỗi UNIQUE constraint
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại! Vui lòng chọn tên khác.",
                                  "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDiachi.Clear();
                    txtHovaTen.Clear();
                    txtMatkhauXN.Clear();
                    txtSoDT.Clear();
                    txtTentaikhoan.Clear();
                    txtMatkhauXN.Clear();
                }
                else if (ex.Number == 547) // Lỗi Foreign Key
                {
                    MessageBox.Show("Lỗi ràng buộc dữ liệu!",
                                  "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message + "\n\nMã lỗi: " + ex.Number,
                                  "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();

                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }
}

// ===== QUAN TRỌNG: SỬA FORM ĐĂNG NHẬP =====
// Thêm constructor này vào frmDangNhap.cs để nhận tên đăng nhập

/*
public partial class frmDangNhap : Form
{
    public frmDangNhap()
    {
        InitializeComponent();
    }
    
    // Constructor nhận tên đăng nhập từ form đăng ký
    public frmDangNhap(string tenDangNhap) : this()
    {
        // Giả sử bạn có textbox tên là txtTenDangNhap
        txtTenDangNhap.Text = tenDangNhap;
        txtMatKhau.Focus(); // Focus vào ô mật khẩu
    }
}
*/