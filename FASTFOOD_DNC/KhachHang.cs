using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FASTFOOD_DNC
{
    public partial class frmKhachHang : Form
    {
        string connectionString = "Data Source=Huy\\SQLEXPRESS;Initial Catalog=FASTFOOD;Integrated Security=True";
        private string currentUsername;

        public frmKhachHang()
        {
            InitializeComponent();
        }

        // Hàm khởi tạo này nhận tên đăng nhập từ form Login
        public frmKhachHang(string username)
        {
            InitializeComponent();
            this.currentUsername = username;
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            // Tải thông tin người dùng khi form được mở
            LoadUserInfo();

            // Hiển thị giao diện Menu mặc định
            btnMenuKH.PerformClick();
        }

        // Tải thông tin người dùng (Tên Khách Hàng) để hiển thị
        private void LoadUserInfo()
        {
            // Nếu không có ai đăng nhập (currentUsername là null hoặc rỗng) thì không làm gì cả
            if (string.IsNullOrEmpty(this.currentUsername))
            {
                lblCurrentUserInfoKH.Text = "Khách vãng lai";
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // SỬA LỖI LOGIC: Truy vấn theo CSDL mới
                    // Bước 1: Từ TENDN trong bảng TAIKHOAN, lấy ra MATK.
                    SqlCommand cmdGetId = new SqlCommand("SELECT MATK FROM TAIKHOAN WHERE TENDN = @TENDN", conn);
                    cmdGetId.Parameters.AddWithValue("@TENDN", this.currentUsername);

                    // SỬA LỖI AN TOÀN: Kiểm tra kết quả trước khi dùng để tránh lỗi
                    object matkResult = cmdGetId.ExecuteScalar();

                    if (matkResult != null && matkResult != DBNull.Value)
                    {
                        int matk = Convert.ToInt32(matkResult);

                        // Bước 2: Từ MATK, tìm TENKH trong bảng KHACHHANG.
                        SqlCommand cmdGetName = new SqlCommand("SELECT TENKH FROM KHACHHANG WHERE MATK = @MATK", conn);
                        cmdGetName.Parameters.AddWithValue("@MATK", matk);

                        object tenKHResult = cmdGetName.ExecuteScalar();

                        if (tenKHResult != null)
                        {
                            lblCurrentUserInfoKH.Text =tenKHResult.ToString();
                        }
                        else
                        {
                            // Trường hợp có tài khoản nhưng chưa có thông tin khách hàng
                            lblCurrentUserInfoKH.Text = "Tài khoản chưa cập nhật thông tin";
                        }
                    }
                    else
                    {
                        // Trường hợp đặc biệt (ví dụ: tài khoản admin không có trong bảng khách hàng)
                        lblCurrentUserInfoKH.Text =this.currentUsername;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin người dùng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblCurrentUserInfoKH.Text = "Lỗi hiển thị";
            }
        }

        // CẢI TIẾN: Tạo một hàm dùng chung để tải User Control
        private void LoadUserControl(UserControl userControl, string pageTitle)
        {
            lblPageTitleKH.Text = pageTitle;
            pnMenuKH.Controls.Clear();
            userControl.Dock = DockStyle.Fill;
            pnMenuKH.Controls.Add(userControl);
        }

        private void btnMenuKH_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ucMenuKH(), "Menu");
        }

        private void btnDonHangKH_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ucDonHangKH(), "Đơn Hàng");
        }

        private void btnGioHang_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ucGIoHang(), "Giỏ Hàng");
        }
    }
}