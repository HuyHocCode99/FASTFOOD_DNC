using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
// THÊM THƯ VIỆN NÀY
using Microsoft.Reporting.WinForms;

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
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.RefreshReport();
        }

        private void btnTaoBaoCao_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1);

            if (tuNgay > denNgay)
            {
                MessageBox.Show("Ngày bắt đầu không thể lớn hơn ngày kết thúc.", "Lỗi");
                return;
            }

            // 1. LẤY DATATABLE (KHỚP 100% VỚI FILE XML MỚI)
            DataTable dt = new DataTable();
            try
            {
                // Câu truy vấn này tạo ra 5 cột và tất cả đều là STRING
                string query = @"
                    SELECT 
                        CONVERT(NVARCHAR, ROW_NUMBER() OVER (ORDER BY D.NGAYDAT DESC)) AS STT,
                        CONVERT(NVARCHAR, D.MADON) AS MaDon, 
                        K.TENKH AS TenKH, 
                        CONVERT(NVARCHAR, D.NGAYDAT, 103) AS NgayDat, -- 103 = dd/MM/yyyy
                        CONVERT(NVARCHAR, D.TONGTIEN) AS TongTien
                    FROM DONHANG D
                    JOIN KHACHHANG K ON D.MAKH = K.MAKH
                    WHERE 
                        D.TRANGTHAI = N'Đã Giao'
                        AND (D.NGAYDAT BETWEEN @TuNgay AND @DenNgay)";

                using (SqlConnection conn = new SqlConnection(connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@TuNgay", tuNgay);
                    adapter.SelectCommand.Parameters.AddWithValue("@DenNgay", denNgay);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi");
                return;
            }

            // 2. ĐỔ DATATABLE VÀO REPORTVIEWER
            reportViewer1.LocalReport.DataSources.Clear();

            // 1. Tên file .rdlc (Đảm bảo Build Action = Embedded Resource)
            reportViewer1.LocalReport.ReportPath = "rpBaoCao.rdlc";

            // 2. Tên DataSet (Khớp 100% với XML mới: "dsDonHang")
            ReportDataSource rds = new ReportDataSource("dsDonHang", dt);

            reportViewer1.LocalReport.DataSources.Add(rds);

            // 3. Tên Parameters (Khớp 100% với XML mới)
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("paramTuNgay", tuNgay.ToString("dd/MM/yyyy"));
            parameters[1] = new ReportParameter("paramDenNgay", dtpDenNgay.Value.Date.ToString("dd/MM/yyyy"));
            reportViewer1.LocalReport.SetParameters(parameters);

            // 4. LÀM MỚI BÁO CÁO
            try
            {
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("LỖI KHI HIỂN THỊ BÁO CÁO: " + ex.Message, "Lỗi ReportViewer");
            }

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy dữ liệu báo cáo.", "Thông báo");
            }
        }
    }
}