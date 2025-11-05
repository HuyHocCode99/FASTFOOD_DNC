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
            // SỬA LỖI DỮ LIỆU: Thống nhất một kiểu chữ "Đang Giao"
            cboTrangThaiLoc.Items.Add("Đã đặt");
            cboTrangThaiLoc.Items.Add("Chờ Xử Lí");
            cboTrangThaiLoc.Items.Add("Đang Giao"); // Bỏ kiểu chữ "Ð" cũ
            cboTrangThaiLoc.Items.Add("Đã Giao");
            cboTrangThaiLoc.Items.Add("Đã Hủy"); // Thêm trạng thái Hủy
            LoadDonHang();
        }

        private void LoadDonHang()
        {
            // SỬA LỖI LỚN NHẤT:
            // Kiểm tra vai trò Admin, KHÔNG kiểm tra IsLoggedIn()
            if (UserSession.VaiTro != "Admin")
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này.", "Lỗi Phân Quyền");
                return;
            }

            // Câu truy vấn SQL (Load tất cả đơn hàng)
            string query = @"SELECT D.MADON,D.NGAYDAT,D.TONGTIEN,D.TRANGTHAI,K.TENKH 
                             FROM DONHANG D JOIN KHACHHANG K ON D.MAKH = K.MAKH
                             ORDER BY D.NGAYDAT DESC"; // Sắp xếp đơn mới lên đầu
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
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDonHang.Rows[e.RowIndex];

                // CẢI THIỆN: Định dạng lại dữ liệu khi hiển thị
                txtMaDonHang.Text = row.Cells["MADON"].Value.ToString();
                txtTenKhachHang.Text = row.Cells["TENKH"].Value.ToString();
                txtTrangThaiHienTai.Text = row.Cells["TRANGTHAI"].Value.ToString();

                // Định dạng Ngày (ví dụ: 04/11/2025)
                object ngayDatValue = row.Cells["NGAYDAT"].Value;
                if (ngayDatValue != DBNull.Value)
                {
                    txtNgayDat.Text = Convert.ToDateTime(ngayDatValue).ToString("dd/MM/yyyy");
                }

                // Định dạng Tiền Tệ (ví dụ: 150,000)
                object tongTienValue = row.Cells["TONGTIEN"].Value;
                if (tongTienValue != DBNull.Value)
                {
                    txtTongTien.Text = Convert.ToDecimal(tongTienValue).ToString("N0");
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cboTrangThaiLoc.Text) || string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                MessageBox.Show("Mời Bạn Nhập Đầy Đủ Thông Tin Để Tìm Kiếm", "Thông Báo");
                return;
            }

            string trangThai = cboTrangThaiLoc.Text.Trim();
            string timKiem = txtTimKiem.Text.Trim();

            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
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

                    SqlDataAdapter adapter = new SqlDataAdapter(queryTim, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@trangThai", trangThai);
                    adapter.SelectCommand.Parameters.AddWithValue("@timKiem", timKiem);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvDonHang.DataSource = dt;

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

            // CẢI THIỆN: Kiểm tra logic trạng thái
            if (txtTrangThaiHienTai.Text != "Chờ Xử Lí" && txtTrangThaiHienTai.Text != "Đã đặt")
            {
                MessageBox.Show("Chỉ có thể xác nhận đơn hàng đang 'Chờ Xử Lí' hoặc 'Đã đặt'.", "Lỗi Logic");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    // SỬA LỖI DỮ LIỆU: Thống nhất 1 kiểu chữ "Đang Giao"
                    string updateQuery = "UPDATE DONHANG SET TRANGTHAI = N'Đang Giao' WHERE MADON = @madon";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@madon", Convert.ToInt32(txtMaDonHang.Text));
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xác nhận đơn hàng thành công!", "Thông báo");
                            LoadDonHang(); // Tải lại danh sách
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
                MessageBox.Show("Vui lòng chọn đơn hàng để hoàn thành.", "Thông báo");
                return;
            }

            // CẢI THIỆN: Kiểm tra logic trạng thái
            if (txtTrangThaiHienTai.Text != "Đang Giao")
            {
                MessageBox.Show("Chỉ có thể hoàn thành đơn hàng đang ở trạng thái 'Đang Giao'.", "Lỗi Logic");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    string updateQuery = "UPDATE DONHANG SET TRANGTHAI = N'Đã Giao' WHERE MADON = @madon";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@madon", Convert.ToInt32(txtMaDonHang.Text));
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã hoàn thành đơn hàng!", "Thông báo");
                            LoadDonHang(); // Tải lại danh sách
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hoàn thành đơn hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuyDon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaDonHang.Text))
            {
                MessageBox.Show("Vui lòng chọn đơn hàng để hủy.", "Thông báo");
                return;
            }

            // SỬA LỖI LOGIC NGHIỆP VỤ:
            // Không nên XÓA đơn hàng. Chỉ nên cập nhật trạng thái là 'Đã Hủy'.
            // Xóa đơn hàng sẽ làm sai lệch báo cáo doanh thu.

            if (txtTrangThaiHienTai.Text == "Đã Giao")
            {
                MessageBox.Show("Không thể hủy đơn hàng đã giao thành công.", "Lỗi Logic");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    // string XoaQuery = "DELETE FROM DONHANG WHERE MADON = @MADON"; // Không nên dùng
                    string HuyQuery = "UPDATE DONHANG SET TRANGTHAI = N'Đã Hủy' WHERE MADON = @madon";

                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(HuyQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@madon", Convert.ToInt32(txtMaDonHang.Text));
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Hủy đơn hàng thành công!", "Thông báo");
                            LoadDonHang(); // Tải lại danh sách
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hủy đơn hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}