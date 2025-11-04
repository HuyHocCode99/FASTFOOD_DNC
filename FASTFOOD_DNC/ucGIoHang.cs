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

namespace FASTFOOD_DNC
{
    public partial class ucGIoHang : UserControl
    {
        string connectionString = "Data Source=Huy\\SQLEXPRESS;Initial Catalog=FASTFOOD;Integrated Security=True";
        Decimal sum = 0;
        public ucGIoHang()
        {
            InitializeComponent();
        }

        private void ucGIoHang_Load(object sender, EventArgs e)
        {
            LoadGioHang();
        }

        // Viết Hàm Load Giỏ Hàng
        public void LoadGioHang()
        {
            if (!UserSession.IsLoggedIn())
            {
                MessageBox.Show("Vui lòng đăng nhập để xem giỏ hàng.", "Thông báo");
                return;
            }

            // Lấy MAKH từ session
            int maKhachHang = UserSession.MaKhachHang;

            // Câu truy vấn SQL
            string query = @"
        SELECT 
            G.MAGIOHANG,
            M.TENMON AS N'Tên Món', 
            G.SOLUONG AS N'Số Lượng', 
            M.DONGIA AS N'Đơn Giá',
            G.NGAYTHEM AS N'Ngày Thêm',
            (G.SOLUONG * M.DONGIA) AS N'Thành Tiền'
        FROM 
            GIOHANG G
        JOIN 
            MONAN M ON G.MAMON = M.MAMON
        WHERE 
            G.MAKH = @makh";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

                    // Thêm tham số @makh
                    adapter.SelectCommand.Parameters.AddWithValue("@makh", maKhachHang);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán dữ liệu cho DataGridView
                    dgvGioHang.DataSource = dt;

                    // (Tùy chọn) Ẩn cột MAGIOHANG cho đẹp
                    if (dgvGioHang.Columns["MAGIOHANG"] != null)
                    {
                        dgvGioHang.Columns["MAGIOHANG"].Visible = false;
                    }

                    //SAU KHI TẢI XONG, TÍNH TỔNG TIỀN
                    TinhTongTien(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải giỏ hàng: " + ex.Message, "Lỗi");
            }
        }

        // 2. HÀM PHỤ ĐỂ TÍNH TỔNG TIỀN
        private void TinhTongTien(DataTable dt)
        {
            decimal tongTien = 0;

            // Duyệt qua từng dòng trong DataTable
            foreach (DataRow row in dt.Rows)
            {
                // Lấy giá trị từ cột "Thành Tiền"
                // Phải kiểm tra DBNull.Value phòng trường hợp giá trị bị null
                if (row["Thành Tiền"] != DBNull.Value)
                {
                    tongTien += Convert.ToDecimal(row["Thành Tiền"]);
                    sum = tongTien;
                }
            }

            // Hiển thị lên Label (ví dụ: "Tổng: 150,000 đ")
            lblTongTien.Text = $"Tổng: {tongTien:N0} đ";
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (!UserSession.IsLoggedIn())
            {
                MessageBox.Show("Vui lòng đăng nhập để Xoa.", "Thông báo");
                return;
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Lấy MAGIOHANG từ dòng được chọn
                if (dgvGioHang.SelectedRows.Count > 0)
                {
                    int magiohang = Convert.ToInt32(dgvGioHang.SelectedRows[0].Cells["MAGIOHANG"].Value);
                    // Câu lệnh xóa
                    string deleteQuery = "DELETE FROM GIOHANG WHERE MAGIOHANG = @magiohang";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@magiohang", magiohang);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa món thành công!", "Thông báo");
                            LoadGioHang(); // Tải lại giỏ hàng sau khi xóa
                        }
                        else
                        {
                            MessageBox.Show("Xóa món thất bại!", "Lỗi");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn món để xóa.", "Thông báo");
                }
            }
        }

        private void btnDatHang_Click(object sender, EventArgs e)
        {
            if (!UserSession.IsLoggedIn())
            {
                MessageBox.Show("Vui lòng đăng nhập để Dat hàng.", "Thông báo");
                return;
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                int maKhachHang = UserSession.MaKhachHang;
                // Câu lệnh xóa tất cả món trong giỏ hàng của khách hàng
                string clearCartQuery = "DELETE FROM GIOHANG WHERE MAKH = @makh";
                using (SqlCommand cmdClear = new SqlCommand(clearCartQuery, conn))
                {
                    cmdClear.Parameters.AddWithValue("@makh", maKhachHang);
                    
                    int rowsAffected = cmdClear.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đặt hàng thành công!", "Thông báo");
                        LoadGioHang(); // Tải lại giỏ hàng sau khi đặt hàng
                    }
                    else
                    {
                        MessageBox.Show("Giỏ hàng trống hoặc đặt hàng thất bại!", "Lỗi");
                    }
                }
                
                try
                {
                    string orderQuery = @"INSERT INTO DONHANG(MAKH,TONGTIEN,TRANGTHAI,NGAYDAT) VALUES(@makh,@tongtien,@tt,@ngaydat)";
                    using (SqlCommand cmdOrder = new SqlCommand(orderQuery, conn))
                    {
                        cmdOrder.Parameters.AddWithValue("@makh", maKhachHang);
                        cmdOrder.Parameters.AddWithValue("@tongtien", sum);
                        cmdOrder.Parameters.AddWithValue("@tt", "Đã đặt");
                        cmdOrder.Parameters.AddWithValue("@ngaydat", DateTime.Now);
                        // Kiểm tra xem đặt thành công chưa
                        if (dgvGioHang.RowCount > 0)
                        {
                            int result = cmdOrder.ExecuteNonQuery();
                            if (result > 0)
                            {
                                MessageBox.Show("Đơn hàng đã được tạo thành công!", "Thông báo");
                                return;
                            }
                        }
                        
                    }
                        
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi đặt hàng: " + ex.Message, "Lỗi");
                }
            }
        }
    }
}
