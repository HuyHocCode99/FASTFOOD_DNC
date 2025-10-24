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
    public partial class frmDangNhap : Form
    {
        string connection = "Data Source=Huy\\SQLEXPRESS;Initial Catalog=FASTFOOD;Integrated Security=True";
        SqlConnection conn = null;
        public frmDangNhap()
        {
            InitializeComponent();
        }
        public frmDangNhap(string v)
        {
            InitializeComponent();
        }
        private void DangNhap_Load(object sender, EventArgs e)
        {
            txtTK.Enabled = true;
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            Boolean KiemTra = false;
            if(txtMK == null || txtTK == null)
            {
                MessageBox.Show("Mời Bạn Nhập Đầy Đủ Thông Tin!", "Thông Báo!",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    string query = "SELECT * FROM TAIKHOAN";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    while(true)
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string TenDN = reader["TENDN"].ToString().Trim();
                            string MatKhau = reader["MK"].ToString().Trim();
                            if (txtTK.Text.Trim() == TenDN && txtMK.Text.Trim() == MatKhau)
                            {
                                KiemTra = true;

                                if(reader["VAITRO"].ToString().Trim() == "Admin")
                                {
                                    if (KiemTra)
                                    {
                                        MessageBox.Show("Đăng Nhập Thành Công!", "Thông Báo!",
                                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        frmAdmin frmAD = new frmAdmin();
                                        frmAD.Show();
                                        this.Hide();
                                    }
                                    break;
                                }
                                else if (reader["VAITRO"].ToString().Trim() == "Khach Hang")
                                {
                                    if (KiemTra)
                                    {
                                        MessageBox.Show("Đăng Nhập Thành Công!", "Thông Báo!",
                                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        frmKhachHang frmKH = new frmKhachHang(txtTK.Text.Trim());
                                        frmKH.Show();
                                        this.Hide();
                                    }
                                    break;
                                }
                            }
                        }
                        reader.Close();
                        break;
                    }
                }
                
                
            }
            catch (Exception ex)
            {
                if(!KiemTra)
                {
                    MessageBox.Show("Tài Khoản Hoặc Mật Khẩu Không Đúng!", "Thông Báo!",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMK.Clear();
                    txtTK.Clear();
                    return;
                }
            }

        }

        private void linklblDK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDangKy frmDK = new frmDangKy(txtTK.Text.Trim());
            frmDK.Show();
            this.Hide();
        }
    }
}
