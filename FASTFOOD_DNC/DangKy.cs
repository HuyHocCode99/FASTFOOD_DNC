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
        string connection = "Data Source=Huy\\SQLEXPRESS;Initial Catalog=FASTFOOD;Integrated Security=True";
        SqlConnection conn = null;
        public frmDangKy()
        {
            InitializeComponent();
        }
        public frmDangKy(string v)
        {
            InitializeComponent();
        }
        private void frmDangKy_Load(object sender, EventArgs e)
        {
            txtHovaTen.Enabled = true;
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            Boolean KiemTra = false;
            
            if (string.IsNullOrWhiteSpace(txtHovaTen.Text) ||
                string.IsNullOrWhiteSpace(txtDiachi.Text) ||
                string.IsNullOrWhiteSpace(txtSoDT.Text) || 
                string.IsNullOrWhiteSpace(txtMatkhauXN.Text) ||
                string.IsNullOrWhiteSpace(txtTentaikhoan.Text))
            {
                MessageBox.Show("Mời Bạn Nhập Đầy Đủ Thông Tin!", "Thông Báo!",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtMatKhau.Text != txtMatkhauXN.Text)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp! Vui lòng nhập lại.",
                              "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMatKhau.Clear();
                txtMatkhauXN.Clear();
                return;
            }


            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    string insertTaiKhoan = @"INSERT INTO TAIKHOAN(TENDN, MK, VAITRO) 
                                         VALUES(@TenDN, @MatKhau, N'Khach Hang');
                                         SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    SqlCommand cmdTaiKhoan = new SqlCommand(insertTaiKhoan, conn);
                    cmdTaiKhoan.Parameters.AddWithValue("@TenDN", txtTentaikhoan.Text.Trim());
                    cmdTaiKhoan.Parameters.AddWithValue("@MatKhau", txtMatKhau.Text);


                    int maTK = (int)cmdTaiKhoan.ExecuteScalar();


                    string insertKhachHang = @"INSERT INTO KHACHHANG(TENKH, DIACHIKH, SODT, MATK) 
                                          VALUES(@TenKH, @DiaChiKH, @SoDT, @MaTK)";

                    SqlCommand cmdKhachHang = new SqlCommand(insertKhachHang, conn);
                    cmdKhachHang.Parameters.AddWithValue("@TenKH", txtHovaTen.Text.Trim());
                    cmdKhachHang.Parameters.AddWithValue("@DiaChiKH", txtDiachi.Text.Trim());
                    cmdKhachHang.Parameters.AddWithValue("@SoDT", txtSoDT.Text.Trim());
                    cmdKhachHang.Parameters.AddWithValue("@MaTK", maTK);

                    cmdKhachHang.ExecuteNonQuery();

                    if (txtMatKhau.Text == txtMatkhauXN.Text)
                    {
                       MessageBox.Show("Đăng ký tài khoản thành công!", "Thông báo",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                    }

                    
                    KiemTra = true;
                }
                    
                
                if(KiemTra == true)
                {
                    frmDangNhap frmDN = new frmDangNhap(txtTentaikhoan.Text.Trim());
                    frmDN.Show();
                    this.Hide();
                }

                
            }
            catch (SqlException ex)
            {

                if (ex.Number == 2627 || ex.Number == 2601) // lỗi unique

                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại! Vui lòng chọn tên khác.",
                                  "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDiachi.Clear();
                    txtHovaTen.Clear();
                    txtMatKhau.Clear();
                    txtSoDT.Clear();
                    txtTentaikhoan.Clear();
                    txtMatkhauXN.Clear();
                }
                else if (ex.Number == 547) // Lỗi Foreign Key
                {
                    MessageBox.Show("Lỗi ràng buộc dữ liệu!",
                                  "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message + "\n\nMã lỗi: " + ex.Number,
                                  "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                

                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        private void linklblDangNhap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDangNhap frmDN = new frmDangNhap(txtTentaikhoan.Text.Trim());
            frmDN.Show();
            this.Hide();
        }
    }
}

