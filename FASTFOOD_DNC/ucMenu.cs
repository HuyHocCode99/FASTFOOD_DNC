using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FASTFOOD_DNC
{
    public partial class ucMenu : UserControl
    {
        string connection = "Data Source=Huy\\SQLEXPRESS;Initial Catalog=FASTFOOD;Integrated Security=True";

        // Bỏ các biến toàn cục SqlConnection và DataSet không cần thiết
        // DataSet dsMonAn = new DataSet("MonAn"); 

        public ucMenu()
        {
            InitializeComponent();
        }

        // Constructor này có thể không cần thiết nếu không dùng đến
        // public ucMenu(string v)
        // {
        //     InitializeComponent();
        // }

        private void ucMenu_Load(object sender, EventArgs e)
        {
            // Gọi hàm tải dữ liệu khi UserControl được load
            LoadMenuData();
        }

        // TÁCH HÀM: Tạo một hàm riêng để tải dữ liệu, dễ quản lý và tái sử dụng
        public void LoadMenuData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    // Lấy tất cả các cột cần thiết từ bảng MONAN
                    string query = "SELECT MAMON, TENMON, DONGIA, MOTA FROM MONAN WHERE TENMON LIKE @TENMON";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    // Giả sử có một TextBox tên txtSearch để tìm kiếm, nếu không thì để trống
                    // Nếu dùng txtTenMon cho việc tìm kiếm thì giữ nguyên
                    cmd.Parameters.AddWithValue("@TENMON", "%" + txtTenMon.Text.Trim() + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dtMonAn = new DataTable();
                    adapter.Fill(dtMonAn); // Tự động đổ dữ liệu vào DataTable

                    // Gán nguồn dữ liệu cho DataGridView
                    dgvMenu.DataSource = dtMonAn;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Thông Báo!",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Dùng string.IsNullOrWhiteSpace để kiểm tra chuỗi tốt hơn
            if (string.IsNullOrWhiteSpace(txtTenMon.Text) ||
                string.IsNullOrWhiteSpace(txtMoTa.Text) ||
                numDonGia.Value <= 0) // Kiểm tra giá trị hợp lệ
            {
                MessageBox.Show("Vui lòng nhập đầy đủ và chính xác thông tin!", "Thông Báo!",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    // Câu lệnh INSERT đơn giản, chưa cần HINHANH ở đây
                    string query = "INSERT INTO MONAN (TENMON, DONGIA, MOTA) VALUES (@tenmon, @dongia, @mota)";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Thêm tham số để chống SQL Injection
                    cmd.Parameters.AddWithValue("@tenmon", txtTenMon.Text.Trim());
                    // SỬA LỖI: Lấy giá trị từ thuộc tính .Value, không phải .Text
                    cmd.Parameters.AddWithValue("@dongia", numDonGia.Value);
                    cmd.Parameters.AddWithValue("@mota", txtMoTa.Text.Trim());

                    // Thực thi câu lệnh INSERT
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Thêm món ăn thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // SỬA LỖI: Tải lại dữ liệu để DataGridView cập nhật thông tin mới nhất từ CSDL
                    LoadMenuData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm món ăn: " + ex.Message, "Thông Báo!",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dgvMenu.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn món ăn để lưu!", "Thông Báo!",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtTenMon.Text) ||
                string.IsNullOrWhiteSpace(txtMoTa.Text) ||
                numDonGia.Value <= 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ và chính xác thông tin!", "Thông Báo!",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    // FIX: Correctly retrieve the selected DataGridViewRow instead of assigning it to DataGridView
                    DataGridViewRow row = dgvMenu.SelectedRows[0];

                    // Assuming MAMON is the primary key and exists in the DataGridView
                    string maMon = row.Cells["MAMON"].Value.ToString();

                    string query = "UPDATE MONAN SET TENMON = @tenmon, DONGIA = @dongia, MOTA = @mota WHERE MAMON = @mamon";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@tenmon", txtTenMon.Text.Trim());
                    cmd.Parameters.AddWithValue("@dongia", numDonGia.Value);
                    cmd.Parameters.AddWithValue("@mota", txtMoTa.Text.Trim());
                    cmd.Parameters.AddWithValue("@mamon", maMon);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật món ăn thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Reload the data to reflect changes
                    LoadMenuData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu món ăn: " + ex.Message, "Thông Báo!",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(dgvMenu.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn món ăn để xóa!", "Thông Báo!",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
                using(SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    DataGridViewRow row = dgvMenu.SelectedRows[0];
                    string maMon = row.Cells["MAMON"].Value.ToString();
                    string query = "DELETE FROM MONAN WHERE MAMON = @mamon";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@mamon", maMon);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xóa món ăn thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Reload the data to reflect changes
                    LoadMenuData();
                }
                
        }
    }
}