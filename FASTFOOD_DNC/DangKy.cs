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
                }
                try
                {
                    conn .Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        MessageBox.Show("Kết Nối Cơ Sở Dữ Liệu Thành Công!","Kết Nối Dữ Liệu");
                    }
                    string insertTaiKhoan = "INSERT INTO TAIKHOAN VALUES ('')";
                }
                catch (Exception ex) { 
                    
                }
                
            }

                
        }
    }
}
