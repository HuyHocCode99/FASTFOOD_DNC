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
    public partial class ucDonHangKH : UserControl
    {
        string connectionString = "Data Source=Huy\\SQLEXPRESS;Initial Catalog=FASTFOOD;Integrated Security=True";
        public ucDonHangKH(string madon)
        {
            InitializeComponent();
        }
        public ucDonHangKH()
        {
            InitializeComponent();
        }


        private void ucDonHangKH_Load(object sender, EventArgs e)
        {
            LoadDonHangKH();
        }
        private void LoadDonHangKH()
        {
            if (!UserSession.IsLoggedIn())
            {
                MessageBox.Show("Vui lòng đăng nhập để xem", "Thông báo");
                return;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Lấy MAKH từ session
                    int maKhachHang = UserSession.MaKhachHang;
                    // Câu truy vấn SQL
                    string query = @"SELECT MADON,NGAYDAT,TONGTIEN,TRANGTHAI FROM DONHANG WHERE MAKH = @makh";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@makh", maKhachHang);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvMoAnDaDat.DataSource = dt;
                    }
                }
            }
            catch (Exception ex) { 
                MessageBox.Show("Lỗi khi tải đơn hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvMoAnDaDat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMoAnDaDat.Rows[e.RowIndex];
                string madon = row.Cells["MADON"].Value.ToString();
                txtMaDon.Text = madon;
                txtNgayDat.Text = row.Cells["NGAYDAT"].Value.ToString();
                txtTongTien.Text = row.Cells["TONGTIEN"].Value.ToString();
                txtTrangThai.Text = row.Cells["TRANGTHAI"].Value.ToString();
            }
        }
    }
}
