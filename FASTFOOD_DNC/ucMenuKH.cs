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
    public partial class ucMenuKH : UserControl
    {
        string connectionString = "Data Source=Huy\\SQLEXPRESS;Initial Catalog=FASTFOOD;Integrated Security=True";
        public ucMenuKH()
        {
            InitializeComponent();
        }
        
        
        private void ucMenuKH_Load(object sender, EventArgs e)
        {
            LoadMonAn();
        }
        public void LoadMonAn()
        {
           

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Lấy các thông tin cần thiết từ bảng MONAN
                    string query = "SELECT MAMON, TENMON, DONGIA,MOTA FROM MONAN";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    DataSet dataSet = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dataSet);
                    dgvMenuKH.DataSource = dataSet.Tables[0];

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách món ăn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvMenuKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra này là đúng
            {
                DataGridViewRow row = dgvMenuKH.Rows[e.RowIndex];

                // Tên cột (MAMON, TENMON, DONGIA, MOTA) 
                // phải khớp với tên trong câu query VÀ DataPropertyName

                txtMaMon.Text = row.Cells["MAMON"].Value.ToString();
                txtTenMon.Text = row.Cells["TENMON"].Value.ToString();

                // SỬA LỖI: Thêm kiểm tra DBNull.Value
                // Nếu MOTA là NULL, gán chuỗi rỗng ""
                txtMoTa.Text = row.Cells["MOTA"].Value == DBNull.Value
                               ? ""
                               : row.Cells["MOTA"].Value.ToString();

                // SỬA LỖI: Thêm kiểm tra DBNull.Value
                // Nếu DONGIA là NULL, gán giá trị 0
                numDonGia.Value = row.Cells["DONGIA"].Value == DBNull.Value
                                  ? 0
                                  : Convert.ToDecimal(row.Cells["DONGIA"].Value);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            if (!UserSession.IsLoggedIn())
            {
                MessageBox.Show("Vui lòng đăng nhập để Them", "Thông báo");
                return;
            }

            //Kiểm tra xem người dùng đã chọn món ăn chưa
            if (string.IsNullOrWhiteSpace(txtMaMon.Text))
            {
                MessageBox.Show("Vui lòng chọn một món ăn từ danh sách.",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Lấy thông tin từ Form và UserSession
            int maKhachHang = UserSession.MaKhachHang; // Lấy MAKH từ session đã lưu
            int maMon = Convert.ToInt32(txtMaMon.Text);
            int soLuong = (int)numSL.Value; // Lấy số lượng từ NumericUpDown
            string tenMon = txtTenMon.Text; // Dùng để hiển thị thông báo

            // 4. Kiểm tra số lượng
            if (soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0.",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 5. Logic SQL (UPSERT - Cập nhật nếu có, Thêm mới nếu không)
            // Đây là logic đúng cho giỏ hàng
            string query = @"
        IF EXISTS (SELECT 1 FROM GIOHANG WHERE MAKH = @makh AND MAMON = @mamon)
        BEGIN
            -- Nếu món đã có -> Cập nhật (cộng dồn) số lượng
            UPDATE GIOHANG
            SET SOLUONG = SOLUONG + @soluong
            WHERE MAKH = @makh AND MAMON = @mamon
        END
        ELSE
        BEGIN
            -- Nếu món chưa có -> Thêm mới
            INSERT INTO GIOHANG (MAKH, MAMON, SOLUONG)
            VALUES (@makh, @mamon, @soluong)
        END";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Thêm các tham số
                        cmd.Parameters.AddWithValue("@makh", maKhachHang);
                        cmd.Parameters.AddWithValue("@mamon", maMon);
                        cmd.Parameters.AddWithValue("@soluong", soLuong);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Thêm {soLuong} {tenMon} vào giỏ hàng thành công!",
                                            "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm món ăn: " + ex.Message,
                                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
