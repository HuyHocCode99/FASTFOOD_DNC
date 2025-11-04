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
    public partial class ucDonHang : UserControl
    {
        string connection = "Data Source=Huy\\SQLEXPRESS;Initial Catalog=FASTFOOD;Integrated Security=True";
        public ucDonHang()
        {
            InitializeComponent();
        }

        private void ucDonHang_Load(object sender, EventArgs e)
        {
            cboTrangThaiLoc.Items.Add("Đã đặt");
            cboTrangThaiLoc.Items.Add("Chờ Xử Lí");
            cboTrangThaiLoc.Items.Add("Ðang Giao");
            cboTrangThaiLoc.Items.Add("Ðã Giao");
            LoadDonHang();
        }
        private void LoadDonHang()
        {
            if (!UserSession.IsLoggedIn())
            {
                MessageBox.Show("Vui lòng đăng nhập để xem đơn hàng.", "Thông báo");
                return;
            }
            // Lấy MAKH từ session
            
            // Câu truy vấn SQL
            string query = @"SELECT D.MADON,D.NGAYDAT,D.TONGTIEN,D.TRANGTHAI,K.TENKH FROM DONHANG D JOIN KHACHHANG K ON D.MAKH = K.MAKH";
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvDonHang.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải đơn hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDonHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDonHang.Rows[e.RowIndex];
                string madon = row.Cells["MADON"].Value.ToString();
                txtMaDonHang.Text = madon;
                txtTenKhachHang.Text = row.Cells["TENKH"].Value.ToString();
                txtNgayDat.Text = row.Cells["NGAYDAT"].Value.ToString();
                txtTongTien.Text = row.Cells["TONGTIEN"].Value.ToString();
                txtTrangThaiHienTai.Text = row.Cells["TRANGTHAI"].Value.ToString();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra đầu vào (Code của bạn đã đúng)
            if (string.IsNullOrWhiteSpace(cboTrangThaiLoc.Text) || string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                MessageBox.Show("Mời Bạn Nhập Đầy Đủ Thông Tin Để Tìm Kiếm", "Thông Báo");
                return;
            }

            // Lấy giá trị từ các controls
            string trangThai = cboTrangThaiLoc.Text.Trim();
            string timKiem = txtTimKiem.Text.Trim();

            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();

                    // 2. Xây dựng câu truy vấn
                    // Chọn các cột hữu ích để hiển thị: Mã đơn, Tên KH, SĐT, Ngày đặt, Tổng tiền, Trạng thái
                    string queryTim = @"
                SELECT D.MADON, K.TENKH, K.SODT, D.NGAYDAT, D.TONGTIEN, D.TRANGTHAI 
                FROM DONHANG D 
                JOIN KHACHHANG K ON D.MAKH = K.MAKH 
                WHERE 
                    D.TRANGTHAI = @trangThai 
                    AND 
                    (K.SODT = @timKiem OR K.MAKH = TRY_CONVERT(INT, @timKiem))
                ORDER BY 
                    D.NGAYDAT DESC";
                    // Sắp xếp theo ngày mới nhất lên đầu

                    SqlDataAdapter adapter = new SqlDataAdapter(queryTim, conn);

                    // 3. Thêm tham số (Rất quan trọng để chống SQL Injection)
                    adapter.SelectCommand.Parameters.AddWithValue("@trangThai", trangThai);
                    adapter.SelectCommand.Parameters.AddWithValue("@timKiem", timKiem);

                    // 4. Lấy và hiển thị dữ liệu
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Giả sử DataGridView của bạn tên là dgvKetQuaTimKiem
                    // HÃY THAY TÊN NÀY BẰNG TÊN ĐÚNG
                    dgvDonHang.DataSource = dt;

                    // 5. Thông báo kết quả
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy đơn hàng nào khớp với điều kiện.", "Thông báo");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm đơn hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXacNhanDon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaDonHang.Text))
            {
                MessageBox.Show("Vui lòng chọn đơn hàng để xác nhận.", "Thông báo");
                return;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    string updateQuery = "UPDATE DONHANG SET TRANGTHAI = 'Đang Giao' WHERE MADON = @madon";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@madon", Convert.ToInt32(txtMaDonHang.Text));
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xác nhận đơn hàng thành công!", "Thông báo");
                            LoadDonHang();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy đơn hàng để xác nhận.", "Thông báo");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xác nhận đơn hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHoanThanhDon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaDonHang.Text))
            {
                MessageBox.Show("Vui lòng chọn đơn hàng để xác nhận.", "Thông báo");
                return;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    string updateQuery = "UPDATE DONHANG SET TRANGTHAI = 'Đã Giao' WHERE MADON = @madon";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@madon", Convert.ToInt32(txtMaDonHang.Text));
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xác nhận đơn hàng thành công!", "Thông báo");
                            LoadDonHang();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy đơn hàng để xác nhận.", "Thông báo");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xác nhận đơn hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuyDon_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    string XoaQuery = "DELETE FROM DONHANG WHERE MADON = @MADON";
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(XoaQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MADON", Convert.ToInt32(txtMaDonHang.Text));
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Hủy đơn hàng thành công!", "Thông báo");
                            LoadDonHang();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy đơn hàng để hủy.", "Thông báo");
                        }

                    }
                }
            }
            catch (Exception ex) { 
                MessageBox.Show("Lỗi khi hủy đơn hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
