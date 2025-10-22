using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FASTFOOD_DNC
{
    public partial class frmDangKy : Form
    {
        string connection = "Data Source = Huy\\SQLEXPRESS;Initial Catalog=FASTFOOD;Integrated Security=True";
        public frmDangKy()
        {
            InitializeComponent();
        }

        private void frmDangKy_Load(object sender, EventArgs e)
        {
            
            SqlConnection conn = new SqlConnection(connection);
            txtHovaTen.Enabled = true;
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                if (txtHovaTen.Text == "" || txtDiachi.Text == "" || txtMatkhau.Text == "" || txtTentaikhoan.Text == "")
                {
                    MessageBox.Show("Mời Bạn Nhập Đầy Đủ Thông Tin!","Thông Báo!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    conn .Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        MessageBox.Show("Kết Nối Cơ Sở Dữ Liệu Thành Công!","Kết Nối Dữ Liệu");
                    }
                    string insertTaiKhoan = @"INSERT INTO TAIKHOAN(TENDN,MK,VAITRO) VALUES(@TenDN,@MatKhau,N'Khach Hang');
                                              SELECT CAST(SCOPE_IDENTITY() AS INT);";// Chọn ID vừa mới chèn   vào bản khách hàng
                    using(SqlCommand cmdTaiKhoan = new SqlCommand(insertTaiKhoan, conn))
                    {
                        cmdTaiKhoan.Parameters.AddWithValue("@TenDN", txtTentaikhoan.Text);
                        cmdTaiKhoan.Parameters.AddWithValue("@MatKhau", txtMatkhau.Text);
                        int newTaiKhoanID = (int)cmdTaiKhoan.ExecuteScalar(); // Lấy ID vừa mới chèn

                        string insertKhachHang = @"INSERT INTO KHACHHANG(HOTEN,DIACHI,SDT,TAIKHOAN_ID) 
                                                   VALUES(@HoVaTen,@DiaChi,@SDT,@TaiKhoanID)";
                        using (SqlCommand cmdKhachHang = new SqlCommand(insertKhachHang, conn))
                        {
                            cmdKhachHang.Parameters.AddWithValue("@HoVaTen", txtHovaTen.Text);
                            cmdKhachHang.Parameters.AddWithValue("@DiaChi", txtDiachi.Text);
                            cmdKhachHang.Parameters.AddWithValue("@SDT", txtSoDT.Text);
                            cmdKhachHang.Parameters.AddWithValue("@TaiKhoanID", newTaiKhoanID);
                            cmdKhachHang.ExecuteNonQuery();
                        }
                        MessageBox.Show("Đăng Ký Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // chuyển sang form đăng nhập
                        Form DangNhap = new frmDangNhap();
                        this.Hide();
                    }
                }
                catch (Exception ex) { 
                    
                }
                
            }

                
        }
    }
}
