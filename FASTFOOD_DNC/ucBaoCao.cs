using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FASTFOOD_DNC
{
    public partial class ucBaoCao : UserControl
    {
        string connection = "Data Source=Huy\\SQLEXPRESS;Initial Catalog=FASTFOOD;Integrated Security=True";

        public ucBaoCao()
        {
            // Hàm này sẽ tự động gọi InitializeComponent()
            // có trong file Designer.cs
            InitializeComponent();
        }

        private void ucBaoCao_Load(object sender, EventArgs e)
        {
            // Thiết lập giá trị mặc định cho 30 ngày gần nhất
            dtpTuNgay.Value = DateTime.Now.AddDays(-30);
            dtpDenNgay.Value = DateTime.Now;
        }

        private void btnTaoBaoCao_Click(object sender, EventArgs e)
        {
            // Lấy ngày bắt đầu (từ 00:00:00)
            DateTime tuNgay = dtpTuNgay.Value.Date;

            // Lấy ngày kết thúc (tới 23:59:59)
            DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1);

            if (tuNgay > denNgay)
            {
                MessageBox.Show("Ngày bắt đầu không thể lớn hơn ngày kết thúc.", "Lỗi");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();

                    // 1. Chạy Query 1 (Lấy Tổng Quan)
                    LoadSoLieuTongQuan(conn, tuNgay, denNgay);

                    // 2. Chạy Query 2 (Lấy Danh Sách Đơn Hàng)
                    LoadDanhSachDonHang(conn, tuNgay, denNgay);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo báo cáo: " + ex.Message, "Lỗi");
            }
        }

        // Hàm thực thi Query 1 (Hiển thị lên Label)
        private void LoadSoLieuTongQuan(SqlConnection conn, DateTime tuNgay, DateTime denNgay)
        {
            string query = @"
                SELECT 
                    ISNULL(SUM(TONGTIEN), 0) AS TongDoanhThu,
                    ISNULL(COUNT(MADON), 0) AS TongSoDonHang
                FROM DONHANG
                WHERE 
                    TRANGTHAI = N'Đã Giao' 
                    AND (NGAYDAT BETWEEN @TuNgay AND @DenNgay)";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
                cmd.Parameters.AddWithValue("@DenNgay", denNgay);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        decimal tongDoanhThu = (decimal)reader["TongDoanhThu"];
                        int tongSoDonHang = (int)reader["TongSoDonHang"];

                        // Hiển thị lên Label
                        lblTongDoanhThu.Text = $"{tongDoanhThu:N0} đ";
                        lblTongSoDon.Text = tongSoDonHang.ToString();
                    }
                } // reader tự động đóng
            }
        }

        // Hàm thực thi Query 2 (Hiển thị lên DataGridView)
        private void LoadDanhSachDonHang(SqlConnection conn, DateTime tuNgay, DateTime denNgay)
        {
            string query = @"
                SELECT 
                    D.MADON AS N'Mã Đơn', 
                    K.TENKH AS N'Tên Khách Hàng',
                    D.NGAYDAT AS N'Ngày Đặt',
                    D.TONGTIEN AS N'Tổng Tiền'
                FROM 
                    DONHANG D
                JOIN 
                    KHACHHANG K ON D.MAKH = K.MAKH
                WHERE 
                    D.TRANGTHAI = N'Đã Giao'
                    AND (D.NGAYDAT BETWEEN @TuNgay AND @DenNgay)
                ORDER BY 
                    D.NGAYDAT DESC";

            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@TuNgay", tuNgay);
            adapter.SelectCommand.Parameters.AddWithValue("@DenNgay", denNgay);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            // Hiển thị lên DataGridView
            dgvDonHangDaGiao.DataSource = dt;
        }
    }
}