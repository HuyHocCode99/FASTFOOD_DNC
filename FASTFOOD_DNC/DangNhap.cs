using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FASTFOOD_DNC
{
    public partial class frmDangNhap : Form
    {
        string connection = "Data Source=Huy\\SQLEXPRESS;Initial Catalog=FASTFOOD;Integrated Security=True";
        // Bỏ biến conn và matk toàn cục đi, không cần thiết

        public frmDangNhap()
        {
            InitializeComponent();
        }

        public frmDangNhap(string v)
        {
            InitializeComponent();
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            txtTK.Enabled = true;
        }

        // Bỏ hàm GetMaKhachHang và GetTenKhachHang, chúng ta sẽ làm tốt hơn
        // bằng 1 câu lệnh SQL duy nhất.

        private void btnDN_Click(object sender, EventArgs e)
        {
            // Bước 1: Kiểm tra input (sửa lỗi_ 5)
            if (string.IsNullOrWhiteSpace(txtTK.Text) || string.IsNullOrWhiteSpace(txtMK.Text))
            {
                MessageBox.Show("Mời Bạn Nhập Đầy Đủ Thông Tin!", "Thông Báo!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Bước 2: Sử dụng 'using' để kết nối tự động đóng
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();

                    // Bước 3: Dùng truy vấn tham số (chống SQL Injection) và JOIN
                    // Lấy tất cả thông tin cần thiết trong 1 lần (sửa lỗi 1, 2, 4)
                    string query = @"
                        SELECT T.MATK, T.VAITRO, K.MAKH, K.TENKH 
                        FROM TAIKHOAN T
                        LEFT JOIN KHACHHANG K ON T.MATK = K.MATK
                        WHERE T.TENDN = @tendn AND T.MK = @mk";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tendn", txtTK.Text.Trim());
                    cmd.Parameters.AddWithValue("@mk", txtMK.Text.Trim());

                    SqlDataReader reader = cmd.ExecuteReader();

                    // Bước 4: Kiểm tra kết quả
                    if (reader.Read()) // Nếu reader.Read() == true -> Đăng nhập thành công
                    {
                        // Lấy thông tin từ reader (sửa lỗi 3)
                        int matk = Convert.ToInt32(reader["MATK"]);
                        string vaiTro = reader["VAITRO"].ToString().Trim();

                        MessageBox.Show("Đăng Nhập Thành Công!", "Thông Báo!",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (vaiTro == "Admin")
                        {
                            // Đăng nhập với tư cách Admin
                            // Admin có thể không có MAKH, nên ta gán 0
                            UserSession.Login(matk, 0, "Admin", "Admin");

                            frmAdmin frmAD = new frmAdmin();
                            frmAD.Show();
                            this.Hide();
                        }
                        else if (vaiTro == "Khach Hang") // Sửa "Khach Hang" cho khớp CSDL
                        {
                            // Đăng nhập với tư cách Khách hàng
                            // Phải kiểm tra DBNull vì MAKH có thể NULL
                            int makh = reader["MAKH"] != DBNull.Value ? Convert.ToInt32(reader["MAKH"]) : 0;
                            string tenkh = reader["TENKH"] != DBNull.Value ? reader["TENKH"].ToString() : "Khách";

                            // Lưu vào Session
                            UserSession.Login(matk, makh, tenkh, vaiTro);

                            frmKhachHang frmKH = new frmKhachHang(txtTK.Text.Trim());
                            frmKH.Show();
                            this.Hide();
                        }
                    }
                    else // Nếu reader.Read() == false -> Sai tài khoản hoặc mật khẩu
                    {
                        MessageBox.Show("Tài Khoản Hoặc Mật Khẩu Không Đúng!", "Thông Báo!",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtMK.Clear();
                        txtTK.Clear();
                        txtTK.Focus();
                    }
                    reader.Close(); // Đóng reader
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối hoặc truy vấn: " + ex.Message, "Lỗi!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linklblDK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDangKy frmDK = new frmDangKy(txtTK.Text.Trim());
            frmDK.Show();
            this.Hide();
        }
    }
}