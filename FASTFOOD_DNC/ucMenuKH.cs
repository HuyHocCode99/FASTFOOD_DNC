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

        }
        public void LoadMonAn()
        {
            // Xóa các món ăn cũ đi trước khi tải lại để tránh trùng lặp
            panel1.Controls.Clear();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Lấy các thông tin cần thiết từ bảng MONAN
                    string query = "SELECT MAMON, TENMON, DONGIA, HINHANH FROM MONAN";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // 1. Tạo một UserControl con (ucMonAnItem) cho mỗi món ăn
                            ucMonAnItem itemControl = new ucMonAnItem();

                            // 2. Gán dữ liệu từ CSDL vào các thuộc tính của itemControl
                            itemControl.MaMon = Convert.ToInt32(reader["MAMON"]);
                            itemControl.TenMon = reader["TENMON"].ToString();
                            itemControl.DonGia = Convert.ToDecimal(reader["DONGIA"]);
                            // Giả sử bạn có thuộc tính HinhAnh trong ucMonAnItem
                            // itemControl.HinhAnh = reader["HINHANH"].ToString();

                            // 3. Đăng ký sự kiện: Khi nút "Thêm" trên itemControl được nhấn,
                            // thì phương thức 'ItemControl_ThemButtonClick' sẽ được gọi
                            itemControl.ThemButtonClick += ItemControl_ThemButtonClick;

                            // 4. Thêm itemControl đã có dữ liệu vào FlowLayoutPanel
                            panel1.Controls.Add(itemControl);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách món ăn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
