using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class NghiepVu : Form
    {
        public NghiepVu()
        {
            InitializeComponent();
        }

        #region Properties

        private int MaNhanVien;
        private string MaNoiHoc;
        private string MaKHL;
        private string MaNgonNgu;
        private string MaCongTy;
        private string MaKNVL;

        #endregion

        //------------------------------------------------------------------------------------------------

        #region Load DataGridView

        #region Load DatagridView NhanVien cách 2 - OK

        SqlConnection con;
        // Load danh sách ngay khi mở form
        private void NghiepVu_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'n05_Ql_NhanSu_T5DataSet10.NgonNgu' table. You can move, or remove it, as needed.
            this.ngonNguTableAdapter.Fill(this.n05_Ql_NhanSu_T5DataSet10.NgonNgu);
            // TODO: This line of code loads data into the 'n05_Ql_NhanSu_T5DataSet9.KhaNangViTinh' table. You can move, or remove it, as needed.
            this.khaNangViTinhTableAdapter.Fill(this.n05_Ql_NhanSu_T5DataSet9.KhaNangViTinh);
            // TODO: This line of code loads data into the 'n05_Ql_NhanSu_T5DataSet8.NoiHoc' table. You can move, or remove it, as needed.
            this.noiHocTableAdapter.Fill(this.n05_Ql_NhanSu_T5DataSet8.NoiHoc);
            // TODO: This line of code loads data into the 'n05_Ql_NhanSu_T5DataSet7.KhoaHuanLuyen' table. You can move, or remove it, as needed.
            this.khoaHuanLuyenTableAdapter.Fill(this.n05_Ql_NhanSu_T5DataSet7.KhoaHuanLuyen);
            // TODO: This line of code loads data into the 'n05_Ql_NhanSu_T5DataSet6.CongTy' table. You can move, or remove it, as needed.
            this.congTyTableAdapter.Fill(this.n05_Ql_NhanSu_T5DataSet6.CongTy);
            // TODO: This line of code loads data into the 'n05_Ql_NhanSu_T5DataSet5.NhanVien' table. You can move, or remove it, as needed.
            this.nhanVienTableAdapter.Fill(this.n05_Ql_NhanSu_T5DataSet5.NhanVien);
            
            string conString = ConfigurationManager.ConnectionStrings["QuanLyNhanSu"].ConnectionString.ToString();
            con = new SqlConnection(conString);
            con.Open();

            HienThiNhanVien_NoiHoc();

        }

        // Đóng kết nối sau khi tắt form
        private void NghiepVu_FormClosing(object sender, FormClosingEventArgs e)
        {
            con.Close();
        }

        #endregion

        #endregion

        //------------------------------------------------------------------------------------------------

        #region Xử Lý Nhập Nhân Viên_Nơi Học
        // Phương thức hiển thị danh sách nhân viên_Nơi học lên dataGridView
        public void HienThiNhanVien_NoiHoc()
        {
            int i;
            string sqlSelect = "select * from NhanVien_NoiHoc";
            SqlCommand cmd = new SqlCommand(sqlSelect, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewNhanVien_NoiHoc.DataSource = dt;
            dataGridViewNhanVien_NoiHoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (i = 0; i < dataGridViewNhanVien_NoiHoc.RowCount; i++) dataGridViewNhanVien_NoiHoc.Rows[i].Cells[0].Value = i + 1;
        }

        // Phương thức thêm Nhân Viên_Nơi Học
        public void ThemNhanVien_NoiHoc()
        {
            string sqlInsert = "Insert into NhanVien_NoiHoc values(@MaNhanvien, @MaNoiHoc," +
                               "@ThoiGianHoc_BatDau, @ThoiGianHoc_KetThuc, @LoaiTotNghiep)";
            SqlCommand cmd = new SqlCommand(sqlInsert, con);
            cmd.Parameters.AddWithValue("MaNhanVien", Convert.ToInt32(comboBoxThemMaNhanVien1.Text));
            cmd.Parameters.AddWithValue("MaNoiHoc", comboBoxThemMaNoiHoc1.Text);
            cmd.Parameters.AddWithValue("ThoiGianHoc_BatDau", dateTimePickerThemThoiGianBatDau1.Value.Date.Year.ToString() + '/' +
                                                     dateTimePickerThemThoiGianBatDau1.Value.Date.Month.ToString() + '/' +
                                                     dateTimePickerThemThoiGianBatDau1.Value.Date.Day.ToString());
            cmd.Parameters.AddWithValue("ThoiGianHoc_KetThuc", dateTimePickerThemThoiGianKetThuc1.Value.Date.Year.ToString() + '/' +
                                                     dateTimePickerThemThoiGianKetThuc1.Value.Date.Month.ToString() + '/' +
                                                     dateTimePickerThemThoiGianKetThuc1.Value.Date.Day.ToString());
            cmd.Parameters.AddWithValue("LoaiTotNghiep", textBoxThemLoaiTotNghiep1.Text);
            cmd.ExecuteNonQuery();
        }

        // Phương thức sửa nhân viên_Nơi học
        public void SuaNhanVien_NoiHoc()
        {
            string sqlUpdate = "Update NhanVien_NoiHoc Set ThoiGianHoc_BatDau=@ThoiGianHoc_BatDau, " +
                "ThoiGianHoc_KetThuc=@ThoiGianHoc_KetThuc, LoaiTotNghiep=@LoaiTotNghiep " +
                "where MaNhanVien=@MaNhanVien and MaNoiHoc=@MaNoiHoc";
            SqlCommand cmd = new SqlCommand(sqlUpdate, con);
            cmd.Parameters.AddWithValue("MaNhanVien", Convert.ToInt32(textBoxSuaMaNhanVien1.Text));
            cmd.Parameters.AddWithValue("MaNoiHoc", textBoxSuaMaNoiHoc1.Text);
            cmd.Parameters.AddWithValue("ThoiGianHoc_BatDau", dateTimePickerSuaThoiGianBatDau1.Value.Date.Year.ToString() + '/' +
                                                     dateTimePickerSuaThoiGianBatDau1.Value.Date.Month.ToString() + '/' +
                                                     dateTimePickerSuaThoiGianBatDau1.Value.Date.Day.ToString());
            cmd.Parameters.AddWithValue("ThoiGianHoc_KetThuc", dateTimePickerSuaThoiGianKetThuc1.Value.Date.Year.ToString() + '/' +
                                         dateTimePickerSuaThoiGianKetThuc1.Value.Date.Month.ToString() + '/' +
                                         dateTimePickerSuaThoiGianKetThuc1.Value.Date.Day.ToString());
            cmd.Parameters.AddWithValue("LoaiTotNghiep", textBoxSuaLoaiTotNghiep1.Text);

            cmd.ExecuteNonQuery();
        }

        // Phương thức Xóa Nhân Viên_Nơi Học
        public void XoaNhanVien_NoiHoc()
        {
            string sqlDelete = "Delete From NhanVien_NoiHoc Where MaNhanVien=@MaNhanVien and MaNoiHoc=@MaNoiHoc";
            SqlCommand cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaNhanVien", MaNhanVien);
            cmd.Parameters.AddWithValue("MaNoiHoc", MaNoiHoc);
            cmd.ExecuteNonQuery();
        }

        // Phương thức Tìm nhân viên_Nơi học (Mới chỉ tìm theo mã hoặc tên)
        public void TimNhanVien_NoiHoc()
        {
            string sqlFindId = "Select * from NhanVien_NoiHoc Where (CharIndex( Convert(Varchar(100), @MaNhanVien) , Convert(Varchar(100), MaNhanVien), 0 ) >=1) and" +
                "                                            ((CharIndex(@MaNoiHoc, MaNoiHoc, 0))>=1 or (@MaNoiHoc=''))";

            SqlCommand cmd = new SqlCommand(sqlFindId, con);

            if (textBoxTimMaNhanVien1.Text == "") cmd.Parameters.AddWithValue("MaNhanVien", 0);
            else cmd.Parameters.AddWithValue("MaNhanVien", Convert.ToInt32(textBoxTimMaNhanVien1.Text));
            cmd.Parameters.AddWithValue("MaNoiHoc", textBoxTimMaNoiHoc1.Text);
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewNhanVien_NoiHoc.DataSource = dt;
            dataGridViewNhanVien_NoiHoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (int i = 0; i < dataGridViewNhanVien_NoiHoc.RowCount; i++) dataGridViewNhanVien_NoiHoc.Rows[i].Cells[0].Value = i + 1;
        }

        // Xử lý sự kiện click nút sửa, cho phép sửa dữ liệu   - Sự kiện tự tạo thêm bằng tay
        void buttonSuaNhanVien_NoiHoc_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            // Mở khóa cho phép sửa dữ liệu sau khi click vào nút sửa, không cho phép sửa mã nhân viên và mã nơi học.
            textBoxSuaMaNhanVien1.ReadOnly = true;
            textBoxSuaMaNoiHoc1.ReadOnly = true;
            dateTimePickerSuaThoiGianBatDau1.Enabled = true;
            dateTimePickerSuaThoiGianKetThuc1.Enabled = true;
            textBoxSuaLoaiTotNghiep1.ReadOnly = false;
        }

        // Xử lý sự kiện click nút xóa, cho phép xóa dữ liệu    - Sự kiện tự tạo thêm bằng tay
        void buttonXoaNhanVien_NoiHoc_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn xóa quá trình này", "Xóa Quá Quá Trình", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                XoaNhanVien_NoiHoc();
                HienThiNhanVien_NoiHoc
();
            }
            else
            {

            }
        }


        // Xử lý sự kiện click nút thêm, cho phép thêm dữ liệu
        private void buttonThemNhanVien_NoiHoc1_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn thêm quá trình?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                ThemNhanVien_NoiHoc();
                HienThiNhanVien_NoiHoc();
            }
            else
            {
                Dispose();
            }
        }

        // Xử lý sự kiện click vào mỗi dòng, dữ liệu tự động update vào form sửa
        private void dataGridViewNhanVien_NoiHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dataGridViewNhanVien_NoiHoc.RowCount-1)
            {
                // Set giá trị cho các thành phần 
                DateTime dateTimeBd = new DateTime();
                dateTimeBd = Convert.ToDateTime(dataGridViewNhanVien_NoiHoc.Rows[e.RowIndex].Cells[3].Value.ToString());
                DateTime dateTimeKt = new DateTime();
                dateTimeKt = Convert.ToDateTime(dataGridViewNhanVien_NoiHoc.Rows[e.RowIndex].Cells[4].Value.ToString());

                textBoxSuaMaNhanVien1.Text = dataGridViewNhanVien_NoiHoc.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxSuaMaNoiHoc1.Text = dataGridViewNhanVien_NoiHoc.Rows[e.RowIndex].Cells[2].Value.ToString();
                dateTimePickerSuaThoiGianBatDau1.Value = dateTimeBd;
                dateTimePickerSuaThoiGianKetThuc1.Value = dateTimeKt;
                textBoxSuaLoaiTotNghiep1.Text = dataGridViewNhanVien_NoiHoc.Rows[e.RowIndex].Cells[5].Value.ToString();


                MaNhanVien = Convert.ToInt32(dataGridViewNhanVien_NoiHoc.Rows[e.RowIndex].Cells[1].Value);


                // Khóa việc sửa giá trị trước khi click vào nút sửa.
                textBoxSuaMaNhanVien1.ReadOnly = true;
                textBoxSuaMaNoiHoc1.ReadOnly = true;
                dateTimePickerSuaThoiGianBatDau1.Enabled = false;
                dateTimePickerSuaThoiGianKetThuc1.Enabled = false;
                textBoxSuaLoaiTotNghiep1.ReadOnly = true;

                buttonSuaNhanVien_NoiHoc.Click += buttonSuaNhanVien_NoiHoc_Click;

                buttonXoaNhanVien_NoiHoc.Click += buttonXoaNhanVien_NoiHoc_Click;
            }
        }

        // Xử lý sự kiện click vào nút Lưu lại (buttonSuaKHL5)
        private void buttonSuaNhanVien_NoiHoc1_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Chắc chắn sửa thông tin?", "Sửa Thông Tin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                SuaNhanVien_NoiHoc();

                // Khóa lại chức năng sửa
                textBoxSuaMaNhanVien1.ReadOnly = true;
                textBoxSuaMaNoiHoc1.ReadOnly = true;
                dateTimePickerSuaThoiGianBatDau1.Enabled = false;
                dateTimePickerSuaThoiGianKetThuc1.Enabled = false;
                textBoxSuaLoaiTotNghiep1.ReadOnly = true;

                //Hiển thị lại danh sách
                HienThiNhanVien_NoiHoc();
            }
            else
            {

            }
        }

        // Xử lý sự kiện click nút tìm kiếm
        private void buttonTimNhanVien_NoiHoc1_Click(object sender, EventArgs e)
        {
            TimNhanVien_NoiHoc();
        }

        // Click nút quay lại, hiển thị toàn bộ dữ liệu khóa huấn luyện
        private void buttonQuayLaiNhanVien_NoiHoc1_Click(object sender, EventArgs e)
        {
            HienThiNhanVien_NoiHoc();
        }
        #endregion

    }
}
