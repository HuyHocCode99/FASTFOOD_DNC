using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FASTFOOD_DNC
{
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();
        }
        public frmAdmin(string v)
        {
            InitializeComponent();
        }

        

        private void btnMenu_Click(object sender, EventArgs e)
        {
            lblPageTitle.Text = "Menu";

            // Xóa tất cả các control đang có trong panel trước khi thêm cái mới
            pnMenu.Controls.Clear();

            // 1. Tạo một đối tượng (instance) của User Control 'ucMenu'
            FASTFOOD_DNC.ucMenu menuControl = new FASTFOOD_DNC.ucMenu();

            // 2. Đặt thuộc tính Dock để User Control tự lấp đầy Panel
            menuControl.Dock = DockStyle.Fill;

            // 3. Thêm User Control vào bộ sưu tập Controls của Panel
            pnMenu.Controls.Add(menuControl);
        }

        private void btnDonHang_Click(object sender, EventArgs e)
        {
            lblPageTitle.Text = "Đơn Hàng";
            pnMenu.Controls.Clear();
            FASTFOOD_DNC.ucDonHang menuControl = new FASTFOOD_DNC.ucDonHang();
            menuControl.Dock = DockStyle.Fill;
            pnMenu.Controls.Add(menuControl);
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            lblPageTitle.Text = "Thông Tin Khách Hàng";
            pnMenu.Controls.Clear();
            FASTFOOD_DNC.ucKhachHang menuControl = new FASTFOOD_DNC.ucKhachHang();
            menuControl.Dock = DockStyle.Fill;
            pnMenu.Controls.Add(menuControl);
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            lblPageTitle.Text = "FAST FOOD";
            lblCurrentUserInfo.Text = "Admin!";
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            frmDangNhap frmDN = new frmDangNhap();
            frmDN.Show();
            this.Hide();
        }

        private void lnkSignOut_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDangNhap frmDN = new frmDangNhap();
            frmDN.Show();
            this.Hide();
        }
    }
}
