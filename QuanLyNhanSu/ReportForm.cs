using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class ReportForm : Form
    {
        private int MaNhanVien;
        private string MaPhongBan;
       /* public ReportForm()
        {
            InitializeComponent();
        }*/

        public ReportForm(int MaNhanvien, string MaPhongBan)
        {
            InitializeComponent();
            this.MaNhanVien = MaNhanvien;
            this.MaPhongBan = MaPhongBan;
        }
        
        private void ReportForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'n05_Ql_NhanSu_T5DataSet_PhongBan.GetPhongBan' table. You can move, or remove it, as needed.
            this.GetPhongBanTableAdapter.Fill(this.n05_Ql_NhanSu_T5DataSet_PhongBan.GetPhongBan, MaPhongBan);

            // TODO: This line of code loads data into the 'n05_Ql_NhanSu_T5DataSet_NhanVien_CongTy.GetNhanVien_CongTy' table. You can move, or remove it, as needed.
            this.getNhanVien_CongTyTableAdapter.Fill(this.n05_Ql_NhanSu_T5DataSet_NhanVien_CongTy.GetNhanVien_CongTy, MaNhanVien);

            // TODO: This line of code loads data into the 'n05_Ql_NhanSu_T5DataSet_NhanVien.GetNhanVien' table. You can move, or remove it, as needed.
            this.getNhanVienTableAdapter.Fill(this.n05_Ql_NhanSu_T5DataSet_NhanVien.GetNhanVien, MaNhanVien);

            // TODO: This line of code loads data into the 'n05_Ql_NhanSu_T5DataSet_NhanVien_KhoaHuanLuyen.GetNhanVien_KhoaHuanLuyen' table. You can move, or remove it, as needed.
            this.getNhanVien_KhoaHuanLuyenTableAdapter.Fill(this.n05_Ql_NhanSu_T5DataSet_NhanVien_KhoaHuanLuyen.GetNhanVien_KhoaHuanLuyen, MaNhanVien);

            // TODO: This line of code loads data into the 'n05_Ql_NhanSu_T5DataSet_NhanVien_KhaNangViTinh.GetNhanVien_KhaNangViTinh' table. You can move, or remove it, as needed.
            this.getNhanVien_KhaNangViTinhTableAdapter.Fill(this.n05_Ql_NhanSu_T5DataSet_NhanVien_KhaNangViTinh.GetNhanVien_KhaNangViTinh, MaNhanVien);

            // TODO: This line of code loads data into the 'n05_Ql_NhanSu_T5DataSetNhanVien_NgonNgu.GetNhanVien_NgonNgu' table. You can move, or remove it, as needed.
            this.getNhanVien_NgonNguTableAdapter.Fill(this.n05_Ql_NhanSu_T5DataSetNhanVien_NgonNgu.GetNhanVien_NgonNgu, MaNhanVien);

            // TODO: This line of code loads data into the 'n05_Ql_NhanSu_T5DataSet_NhanVien_NoiHoc.GetNhanVien_NoiHoc' table. You can move, or remove it, as needed.
            this.getNhanVien_NoiHocTableAdapter.Fill(this.n05_Ql_NhanSu_T5DataSet_NhanVien_NoiHoc.GetNhanVien_NoiHoc, MaNhanVien);

            this.reportViewer2.RefreshReport();
        }
    }
}
