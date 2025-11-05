using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
// KHÔNG CẦN using Microsoft.Reporting.WinForms; nữa

namespace FASTFOOD_DNC
{
    public partial class ucBaoCao : UserControl
    {
        string connection = "Data Source=Huy\\SQLEXPRESS;Initial Catalog=FASTFOOD;Integrated Security=True";

        public ucBaoCao()
        {
            InitializeComponent();
        }

        private void ucBaoCao_Load(object sender, EventArgs e)
        {
            
            dtpTuNgay.Value = DateTime.Now.AddDays(-30);
            dtpDenNgay.Value = DateTime.Now;

            
            TaoBaoCao();
        }

        private void btnTaoBaoCao_Click(object sender, EventArgs e)
        {
            
            TaoBaoCao();
        }

        
        private void TaoBaoCao()
        {
            
            DateTime tuNgay = dtpTuNgay.Value.Date;
            
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

                    
                    LoadSoLieuTongQuan(conn, tuNgay, denNgay);

                    
                    LoadChiTietDonHang(conn, tuNgay, denNgay);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo báo cáo: " + ex.Message, "Lỗi");
            }
        }

        
        private void LoadSoLieuTongQuan(SqlConnection conn, DateTime tuNgay, DateTime denNgay)
        {

            string query = @"
    SELECT 
        ISNULL(SUM(TONGTIEN), 0) AS TongDoanhThu,
        ISNULL(COUNT(MADON), 0) AS TongSoDonHang
    FROM DONHANG
    WHERE 
        (NGAYDAT BETWEEN @TuNgay AND @DenNgay)"; 

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

                        
                        lblTongDoanhThu.Text = $"{tongDoanhThu:N0} đ";
                        lblTongSoDon.Text = tongSoDonHang.ToString();
                    }
                }
            }
        }

        
        private void LoadChiTietDonHang(SqlConnection conn, DateTime tuNgay, DateTime denNgay)
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
                    
                    (D.NGAYDAT BETWEEN @TuNgay AND @DenNgay)
                ORDER BY 
                    D.NGAYDAT DESC";

            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@TuNgay", tuNgay);
            adapter.SelectCommand.Parameters.AddWithValue("@DenNgay", denNgay);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            
            dgvBaoCao.DataSource = dt;

            
            if (dgvBaoCao.Columns["Tổng Tiền"] != null)
            {
                dgvBaoCao.Columns["Tổng Tiền"].DefaultCellStyle.Format = "N0";
            }
            if (dgvBaoCao.Columns["Ngày Đặt"] != null)
            {
                dgvBaoCao.Columns["Ngày Đặt"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }
    }
}