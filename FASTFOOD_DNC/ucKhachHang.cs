using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FASTFOOD_DNC
{
    public partial class ucKhachHang : UserControl
    {
        string connection = "Data Source=Huy\\SQLEXPRESS;Initial Catalog=FASTFOOD;Integrated Security=True";
        // Bỏ biến SqlConnection conn = null; toàn cục không cần thiết

        public ucKhachHang()
        {
            InitializeComponent();
        }

        private void ucKhachHang_Load(object sender, EventArgs e)
        {
            LoadKhachHangData(); // Tải dữ liệu khách hàng khi UserControl load
        }

        // Hàm để tải dữ liệu khách hàng vào dgvKhachHang
        private void LoadKhachHangData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    // SỬA LỖI: Thêm MAKH vào câu truy vấn để có thể sử dụng khi chọn hàng
                    string truyvan = "SELECT MAKH, TENKH, DIACHIKH, SODT FROM KHACHHANG";
                    SqlDataAdapter adapter = new SqlDataAdapter(truyvan, conn); // Khởi tạo adapter trực tiếp với truy vấn và kết nối
                    DataTable dtKhachHang = new DataTable();
                    adapter.Fill(dtKhachHang);
                    dgvKhachHang.DataSource = dtKhachHang;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu khách hàng: " + ex.Message, "Thông Báo!",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];

                // Lấy MAKH từ hàng được chọn
                string maKhachHang = row.Cells["MAKH"].Value.ToString();

                // Đổ dữ liệu khách hàng lên các TextBox (nếu có txtMaKH thì đổ vào đó)
                // txtMaKH.Text = maKhachHang; // Nếu bạn có một TextBox để hiển thị MAKH
                txtTenKH.Text = row.Cells["TENKH"].Value.ToString();
                txtDiaChi.Text = row.Cells["DIACHIKH"].Value.ToString();
                txtSDT.Text = row.Cells["SODT"].Value.ToString();

                // SỬA LỖI: Tải lịch sử đơn hàng của khách hàng được chọn
                LoadLichSuDonHang(maKhachHang);
            }
        }

        // Hàm mới để tải lịch sử đơn hàng của một khách hàng cụ thể
        private void LoadLichSuDonHang(string maKhachHang)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();

                    // SỬA LỖI: Dùng đúng tên cột (MADON, NGAYDAT) từ CSDL
                    string truyvan = @"
                SELECT 
                    DH.MADON, 
                    DH.NGAYDAT, 
                    DH.TONGTIEN, 
                    DH.TRANGTHAI
                    -- Bạn không cần join KHACHHANG ở đây nữa
                    -- vì bạn chỉ cần hiển thị đơn hàng,
                    -- thông tin khách hàng đã có ở dgvKhachHang
                FROM DONHANG DH
                WHERE DH.MAKH = @maKhachHang
                ORDER BY DH.NGAYDAT DESC"; // Sắp xếp đơn mới lên đầu

                    SqlCommand cmdHD = new SqlCommand(truyvan, conn);
                    cmdHD.Parameters.AddWithValue("@maKhachHang", maKhachHang);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmdHD);
                    DataTable dtHD = new DataTable();
                    adapter.Fill(dtHD);

                    // Đảm bảo tên DataGridView là dgvLichSuDon (như code gốc của bạn)
                    dgvLichSuDon.DataSource = dtHD;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử đơn hàng: " + ex.Message, "Thông Báo!",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKH_Click(object sender, EventArgs e)
        {
            //Kiểm tra xem người dùng đã nhập tên để tìm chưa
            if (string.IsNullOrWhiteSpace(txtTimKiemKH.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng cần tìm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string truyvan = "SELECT MAKH, TENKH, DIACHIKH, SODT FROM KHACHHANG WHERE TENKH LIKE @TenKH";

                // Khởi tạo command
                SqlCommand cmdtimKH = new SqlCommand(truyvan, conn);

                
                // Thêm dấu % để tìm kiếm tương đối. Nếu muốn tìm chính xác, bỏ "%" đi.
                cmdtimKH.Parameters.AddWithValue("@TenKH", "%" + txtTimKiemKH.Text + "%");

                try
                {
                    conn.Open(); // Mở kết nối
                    SqlDataReader reader = cmdtimKH.ExecuteReader();
                    // 4. Kiểm tra xem có dữ liệu trả về không
                    if (reader.Read()) // Chỉ cần đọc dòng đầu tiên tìm thấy
                    {
                       
                        
                        
                        txtTenKH.Text = reader["TENKH"].ToString(); // Có thể giữ lại tên đã tìm
                        txtDiaChi.Text = reader["DIACHIKH"].ToString();
                        txtSDT.Text = reader["SODT"].ToString();

                        MessageBox.Show("Đã tìm thấy khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Nếu không có dòng nào, thông báo không tìm thấy
                        MessageBox.Show("Không tìm thấy khách hàng nào có tên phù hợp.", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    reader.Close(); // Đóng reader khi dùng xong
                }
                catch (Exception ex)
                {
                    // Báo lỗi nếu có sự cố khi kết nối hoặc thực thi
                    MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } // Kết nối sẽ tự động đóng ở đây
        }
    }
}