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
        Decimal sum = 0; // Biến này lưu tổng tiền, sẽ được cập nhật bởi TinhTongTien

        public ucGIoHang()
        {
            InitializeComponent();
        }

        private void ucGIoHang_Load(object sender, EventArgs e)
        {
            LoadGioHang();
        }

        // 1. HÀM LOAD GIỎ HÀNG (Code của bạn đã đúng)
        public void LoadGioHang()
        {
            if (!UserSession.IsLoggedIn())
            {
                MessageBox.Show("Vui lòng đăng nhập để xem giỏ hàng.", "Thông báo");
                return;
            }

            int maKhachHang = UserSession.MaKhachHang;
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
                    adapter.SelectCommand.Parameters.AddWithValue("@makh", maKhachHang);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvGioHang.DataSource = dt;

                    if (dgvGioHang.Columns["MAGIOHANG"] != null)
                    {
                        dgvGioHang.Columns["MAGIOHANG"].Visible = false;
                    }

                    // Tính tổng tiền sau khi tải
                    TinhTongTien(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải giỏ hàng: " + ex.Message, "Lỗi");
            }
        }

        // 2. HÀM TÍNH TỔNG TIỀN (Code của bạn đã đúng)
        private void TinhTongTien(DataTable dt)
        {
            decimal tongTien = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (row["Thành Tiền"] != DBNull.Value)
                {
                    tongTien += Convert.ToDecimal(row["Thành Tiền"]);
                }
            }

            // Cập nhật biến toàn cục 'sum'
            this.sum = tongTien;

            // Hiển thị lên Label
            lblTongTien.Text = $"Tổng: {tongTien:N0} đ";
        }

        // 3. HÀM XÓA MÓN (Code của bạn đã đúng)
        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (!UserSession.IsLoggedIn())
            {
                MessageBox.Show("Vui lòng đăng nhập để Xóa.", "Thông báo");
                return;
            }

            if (dgvGioHang.SelectedRows.Count > 0)
            {
                // Xác nhận lại
                if (MessageBox.Show("Bạn có chắc muốn xóa món này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

                int magiohang = Convert.ToInt32(dgvGioHang.SelectedRows[0].Cells["MAGIOHANG"].Value);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM GIOHANG WHERE MAGIOHANG = @magiohang";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@magiohang", magiohang);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa món thành công!", "Thông báo");
                            LoadGioHang(); // Tải lại giỏ hàng
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn món để xóa.", "Thông báo");
            }
        }

        // 4. HÀM ĐẶT HÀNG (ĐÃ SỬA LỖI LOGIC NGHIÊM TRỌNG)
        private void btnDatHang_Click(object sender, EventArgs e)
        {
            if (!UserSession.IsLoggedIn())
            {
                MessageBox.Show("Vui lòng đăng nhập để Đặt hàng.", "Thông báo");
                return;
            }

            // 1. KIỂM TRA GIỎ HÀNG TRƯỚC
            if (this.sum == 0 || dgvGioHang.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng của bạn đang rỗng!", "Thông báo");
                return;
            }

            // Xác nhận đặt hàng
            if (MessageBox.Show($"Tổng đơn hàng của bạn là {this.sum:N0} đ. Bạn có chắc muốn đặt hàng?", "Xác nhận Đặt Hàng", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            int maKhachHang = UserSession.MaKhachHang;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // 2. BẮT ĐẦU TRANSACTION
                // Đảm bảo cả hai lệnh (INSERT và DELETE) cùng thành công hoặc cùng thất bại
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 3. BƯỚC 1: TẠO ĐƠN HÀNG (INSERT)
                    string orderQuery = @"INSERT INTO DONHANG(MAKH, TONGTIEN, TRANGTHAI, NGAYDAT) 
                                          VALUES(@makh, @tongtien, @tt, @ngaydat)";

                    using (SqlCommand cmdOrder = new SqlCommand(orderQuery, conn, transaction))
                    {
                        cmdOrder.Parameters.AddWithValue("@makh", maKhachHang);
                        cmdOrder.Parameters.AddWithValue("@tongtien", this.sum); // Dùng biến 'sum' đã tính
                        cmdOrder.Parameters.AddWithValue("@tt", "Chờ Xử Lí"); // Mặc định là 'Chờ Xử Lí'
                        cmdOrder.Parameters.AddWithValue("@ngaydat", DateTime.Now);
                        cmdOrder.ExecuteNonQuery();
                    }

                    // 4. BƯỚC 2: XÓA GIỎ HÀNG (DELETE)
                    string clearCartQuery = "DELETE FROM GIOHANG WHERE MAKH = @makh";
                    using (SqlCommand cmdClear = new SqlCommand(clearCartQuery, conn, transaction))
                    {
                        cmdClear.Parameters.AddWithValue("@makh", maKhachHang);
                        cmdClear.ExecuteNonQuery();
                    }

                    // 5. HOÀN TẤT GIAO DỊCH
                    transaction.Commit();

                    MessageBox.Show("Đặt hàng thành công! Cảm ơn bạn.", "Thông báo");
                    LoadGioHang(); // Tải lại giỏ hàng (giờ đã rỗng)
                }
                catch (Exception ex)
                {
                    // 6. NẾU CÓ LỖI, HỦY BỎ TẤT CẢ
                    transaction.Rollback();
                    MessageBox.Show("Lỗi khi đặt hàng: " + ex.Message, "Lỗi");
                }
            }
        }
    }
}