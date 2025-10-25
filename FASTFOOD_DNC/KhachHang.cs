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
    public partial class frmKhachHang : Form
    {
        string connection = "Data Source=Huy\\SQLEXPRESS;Initial Catalog=FASTFOOD;Integrated Security=True";
        SqlConnection conn;
        
        public frmKhachHang()
        {
            InitializeComponent();
        }
        public frmKhachHang(string v)
        {
            InitializeComponent();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {

        }
    }
}
