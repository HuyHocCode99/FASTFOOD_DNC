using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FASTFOOD_DNC
{
    public static class UserSession
    {
        public static int MaTaiKhoan { get; private set; }
        public static int MaKhachHang { get; private set; }
        public static string TenKhachHang { get; private set; }
        public static string VaiTro { get; private set; }


        public static void Login(int matk, int makh,string tenkh,string vaitro)
        {
            MaTaiKhoan = matk;
            MaKhachHang = makh;
            TenKhachHang = tenkh;
            VaiTro = vaitro;
        }

        public static void Logout()
        {
            MaTaiKhoan = 0;
            MaKhachHang = 0;
            TenKhachHang = string.Empty;
            VaiTro = string.Empty;
        }

        public static bool IsLoggedIn()
        {
            return MaTaiKhoan != 0;
        }



    }
}
