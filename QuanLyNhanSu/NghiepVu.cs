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
        private string MaKNVT;

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
            HienThiNhanVien_KhaNangViTinh();
            HienThiNhanVien_NgonNgu();
            HienThiNhanVien_CongTy();
            HienThiNhanVien_KhoaHuanLuyen();

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
            DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn xóa quá trình này", "Xóa Quá Trình", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
            int i;
            int count = 0;
            for (i = 0; i < dataGridViewNhanVien_NoiHoc.RowCount - 1; i++)
                if (comboBoxThemMaNhanVien1.Text == dataGridViewNhanVien_NoiHoc.Rows[i].Cells[1].Value.ToString() &&
                    comboBoxThemMaNoiHoc1.Text == dataGridViewNhanVien_NoiHoc.Rows[i].Cells[2].Value.ToString())
                {
                    count++;
                }
            if (count == 1)
            {
                MessageBox.Show("Dữ liệu đã tồn tại", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn thêm nơi học cho nhân viên?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                MaNoiHoc = dataGridViewNhanVien_NoiHoc.Rows[e.RowIndex].Cells[2].Value.ToString();

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

        // Xử lý sự kiện click vào nút Lưu lại (buttonSuaNhanVien_NoiHoc1)
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

        // Click nút quay lại, hiển thị toàn bộ dữ liệu Nhanvien_Noihoc
        private void buttonQuayLaiNhanVien_NoiHoc1_Click(object sender, EventArgs e)
        {
            HienThiNhanVien_NoiHoc();
        }
        #endregion

        //------------------------------------------------------------------------------------------------

        #region Xử Lý Nhập Nhân Viên_Khả năng vi tính
        // Phương thức hiển thị danh sách nhân viên_Khả năng vi tính lên dataGridView
        public void HienThiNhanVien_KhaNangViTinh()
        {
            int i;
            string sqlSelect = "select * from NhanVien_KhaNangViTinh";
            SqlCommand cmd = new SqlCommand(sqlSelect, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewNhanVien_KNVT.DataSource = dt;
            dataGridViewNhanVien_KNVT.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (i = 0; i < dataGridViewNhanVien_KNVT.RowCount; i++) dataGridViewNhanVien_KNVT.Rows[i].Cells[0].Value = i + 1;
        }

        // Phương thức thêm Nhân Viên_Khả năng vi tính
        public void ThemNhanVien_KhaNangViTinh()
        {
            string sqlInsert = "Insert into NhanVien_KhaNangViTinh values(@MaNhanvien, @MaKhaNangViTinh, @PhanMemSuDung)";
            SqlCommand cmd = new SqlCommand(sqlInsert, con);
            cmd.Parameters.AddWithValue("MaNhanVien", Convert.ToInt32(comboBoxThemMaNhanVien2.Text));
            cmd.Parameters.AddWithValue("MaKhaNangViTinh", comboBoxThemMaKNVT2.Text);
            cmd.Parameters.AddWithValue("PhanMemSuDung", textBoxThemPhanMemSuDung2.Text);
            cmd.ExecuteNonQuery();
        }

        // Phương thức sửa nhân viên_Khả năng vi tính
        public void SuaNhanVien_KhaNangViTinh()
        {
            string sqlUpdate = "Update NhanVien_KhaNangViTinh Set PhanMemSuDung=@PhanMemSuDung " +
                "where MaNhanVien=@MaNhanVien and MaKhaNangViTinh=@MaKhaNangViTinh";
            SqlCommand cmd = new SqlCommand(sqlUpdate, con);
            cmd.Parameters.AddWithValue("MaNhanVien", Convert.ToInt32(textBoxSuaMaNhanVien2.Text));
            cmd.Parameters.AddWithValue("MaKhaNangViTinh", textBoxSuaMaKNVT2.Text);
            cmd.Parameters.AddWithValue("PhanMemSuDung", textBoxSuaPhanMemSuDung2.Text);

            cmd.ExecuteNonQuery();
        }

        // Phương thức Xóa Nhân Viên_Khả năng vi tính
        public void XoaNhanVien_KhaNangViTinh()
        {
            string sqlDelete = "Delete From NhanVien_KhaNangViTinh Where MaNhanVien=@MaNhanVien and MaKhaNangViTinh=@MaKhaNangViTinh";
            SqlCommand cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaNhanVien", MaNhanVien);
            cmd.Parameters.AddWithValue("MaKhaNangViTinh", MaKNVT);
            cmd.ExecuteNonQuery();
        }

        // Phương thức Tìm nhân viên_Khả năng vi tính (Mới chỉ tìm theo mã hoặc tên)
        public void TimNhanVien_KhaNangViTinh()
        {
            string sqlFindId = "Select * from NhanVien_KhaNangViTinh Where (CharIndex( Convert(Varchar(100), @MaNhanVien) , Convert(Varchar(100), MaNhanVien), 0 ) >=1) and" +
                "                                            ((CharIndex(@MaKhaNangViTinh, MaKhaNangViTinh, 0))>=1 or (@MaKhaNangViTinh=''))";

            SqlCommand cmd = new SqlCommand(sqlFindId, con);

            if (textBoxTimMaNhanVien2.Text == "") cmd.Parameters.AddWithValue("MaNhanVien", 0);
            else cmd.Parameters.AddWithValue("MaNhanVien", Convert.ToInt32(textBoxTimMaNhanVien2.Text));
            cmd.Parameters.AddWithValue("MaKhaNangViTinh", textBoxTimMaKNVT2.Text);
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewNhanVien_KNVT.DataSource = dt;
            dataGridViewNhanVien_KNVT.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (int i = 0; i < dataGridViewNhanVien_KNVT.RowCount; i++) dataGridViewNhanVien_KNVT.Rows[i].Cells[0].Value = i + 1;
        }

        // Xử lý sự kiện click nút sửa, cho phép sửa dữ liệu   - Sự kiện tự tạo thêm bằng tay
        void buttonSuaNhanVien_KNVT_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            // Mở khóa cho phép sửa dữ liệu sau khi click vào nút sửa, không cho phép sửa mã nhân viên và mã khả năng vi tính.
            textBoxSuaMaNhanVien2.ReadOnly = true;
            textBoxSuaMaKNVT2.ReadOnly = true;
            textBoxSuaPhanMemSuDung2.ReadOnly = false;
        }

        // Xử lý sự kiện click nút xóa, cho phép xóa dữ liệu    - Sự kiện tự tạo thêm bằng tay
        void buttonXoaNhanVien_KNVT_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn xóa kỹ năng này", "Xóa Kỹ Năng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                XoaNhanVien_KhaNangViTinh();
                HienThiNhanVien_KhaNangViTinh();
            }
            else
            {

            }
        }

        // Xử lý sự kiện click nút thêm, cho phép thêm dữ liệu
        private void buttonThemNhanVien_KNVT2_Click(object sender, EventArgs e)
        {
            int i;
            int count = 0;
            for (i = 0; i < dataGridViewNhanVien_KNVT.RowCount - 1; i++)
                if (comboBoxThemMaNhanVien2.Text == dataGridViewNhanVien_KNVT.Rows[i].Cells[1].Value.ToString() &&
                    comboBoxThemMaKNVT2.Text == dataGridViewNhanVien_KNVT.Rows[i].Cells[2].Value.ToString())
                {
                    count++;
                }
            if (count == 1)
            {
                MessageBox.Show("Dữ liệu đã tồn tại", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn thêm kỹ năng khả năng vi tính?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.Yes)
                {
                    ThemNhanVien_KhaNangViTinh();
                    HienThiNhanVien_KhaNangViTinh();
                }
                else
                {
                    Dispose();
                }
            }
                
        }

        // Xử lý sự kiện click vào mỗi dòng, dữ liệu tự động update vào form sửa
        private void dataGridViewNhanVien_KNVT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dataGridViewNhanVien_KNVT.RowCount - 1)
            {
                // Set giá trị cho các thành phần 

                textBoxSuaMaNhanVien2.Text = dataGridViewNhanVien_KNVT.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxSuaMaKNVT2.Text = dataGridViewNhanVien_KNVT.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBoxSuaPhanMemSuDung2.Text = dataGridViewNhanVien_KNVT.Rows[e.RowIndex].Cells[3].Value.ToString();


                MaNhanVien = Convert.ToInt32(dataGridViewNhanVien_NoiHoc.Rows[e.RowIndex].Cells[1].Value);
                MaKNVT = dataGridViewNhanVien_KNVT.Rows[e.RowIndex].Cells[2].Value.ToString();

                // Khóa việc sửa giá trị trước khi click vào nút sửa.
                textBoxSuaMaNhanVien2.ReadOnly = true;
                textBoxSuaMaKNVT2.ReadOnly = true;
                textBoxSuaPhanMemSuDung2.ReadOnly = true;

                buttonSuaNhanVien_KNVT.Click += buttonSuaNhanVien_KNVT_Click;

                buttonXoaNhanVien_KNVT.Click += buttonXoaNhanVien_KNVT_Click;
            }
        }

        // Xử lý sự kiện click vào nút Lưu lại (buttonSuaNhanVien_KNVT2)
        private void buttonSuaNhanVien_KNVT2_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Chắc chắn sửa thông tin?", "Sửa Thông Tin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                SuaNhanVien_KhaNangViTinh();

                // Khóa lại chức năng sửa
                textBoxSuaMaNhanVien2.ReadOnly = true;
                textBoxSuaMaKNVT2.ReadOnly = true;
                textBoxSuaPhanMemSuDung2.ReadOnly = true;

                //Hiển thị lại danh sách
                HienThiNhanVien_KhaNangViTinh();
            }
            else
            {

            }
        }

        // Xử lý sự kiện click nút tìm kiếm
        private void buttonTimNhanVien_KNVT2_Click(object sender, EventArgs e)
        {
            TimNhanVien_KhaNangViTinh();
        }

        // Click nút quay lại, hiển thị toàn bộ dữ liệu NhanVien_KNVT
        private void buttonQuayLaiNhannVien_KNVT2_Click(object sender, EventArgs e)
        {
            HienThiNhanVien_KhaNangViTinh();
        }
        #endregion

        //------------------------------------------------------------------------------------------------

        #region Xử Lý Nhập Nhân Viên_Ngôn Ngữ
        // Phương thức hiển thị danh sách nhân viên_Ngôn ngữ lên dataGridView
        public void HienThiNhanVien_NgonNgu()
        {
            int i;
            string sqlSelect = "select * from NhanVien_NgonNgu";
            SqlCommand cmd = new SqlCommand(sqlSelect, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewNhanVien_NgonNgu.DataSource = dt;
            dataGridViewNhanVien_NgonNgu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (i = 0; i < dataGridViewNhanVien_NgonNgu.RowCount; i++) dataGridViewNhanVien_NgonNgu.Rows[i].Cells[0].Value = i + 1;
        }

        // Phương thức thêm Nhân Viên_NgonNgu
        public void ThemNhanVien_NgonNgu()
        {
            string sqlInsert = "Insert into NhanVien_NgonNgu values(@MaNhanvien, @MaNgonNgu, @Nghe, @Noi, @Doc, @Viet)";
            SqlCommand cmd = new SqlCommand(sqlInsert, con);
            cmd.Parameters.AddWithValue("MaNhanVien", Convert.ToInt32(comboBoxThemMaNhanVien3.Text));
            cmd.Parameters.AddWithValue("MaNgonNgu", comboBoxThemMaNgonNgu3.Text);
            cmd.Parameters.AddWithValue("Nghe", textBoxThemNghe3.Text);
            cmd.Parameters.AddWithValue("Noi", textBoxThemNoi3.Text);
            cmd.Parameters.AddWithValue("Doc", textBoxThemDoc3.Text);
            cmd.Parameters.AddWithValue("Viet", textBoxThemViet3.Text);

            cmd.ExecuteNonQuery();
        }

        // Phương thức sửa nhân viên_Ngôn Ngữ
        public void SuaNhanVien_NgonNgu()
        {
            string sqlUpdate = "Update NhanVien_NgonNgu Set Nghe=@Nghe, Noi=@Noi, Doc=@Doc, Viet=@Viet " +
                "where MaNhanVien=@MaNhanVien and MaNgonNgu=@MaNgonNgu";
            SqlCommand cmd = new SqlCommand(sqlUpdate, con);
            cmd.Parameters.AddWithValue("MaNhanVien", Convert.ToInt32(textBoxSuaMaNhanVien3.Text));
            cmd.Parameters.AddWithValue("MaNgonNgu", textBoxSuaMaNgonNgu3.Text);
            cmd.Parameters.AddWithValue("Nghe", textBoxSuaNghe3.Text);
            cmd.Parameters.AddWithValue("Noi", textBoxSuaNoi3.Text);
            cmd.Parameters.AddWithValue("Doc", textBoxSuaDoc3.Text);
            cmd.Parameters.AddWithValue("Viet", textBoxSuaViet3.Text);

            cmd.ExecuteNonQuery();
        }

        // Phương thức Xóa Nhân Viên_Ngôn Ngữ
        public void XoaNhanVien_NgonNgu()
        {
            string sqlDelete = "Delete From NhanVien_NgonNgu Where MaNhanVien=@MaNhanVien and MaNgonNgu=@MaNgonNgu";
            SqlCommand cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaNhanVien", MaNhanVien);
            cmd.Parameters.AddWithValue("MaNgonNgu", MaNgonNgu);
            cmd.ExecuteNonQuery();
        }

        // Phương thức Tìm nhân viên_Ngôn Ngữ (Mới chỉ tìm theo 2 mã)
        public void TimNhanVien_NgonNgu()
        {
            string sqlFindId = "Select * from NhanVien_NgonNgu Where (CharIndex( Convert(Varchar(100), @MaNhanVien) , Convert(Varchar(100), MaNhanVien), 0 ) >=1) and" +
                "                                            ((CharIndex(@MaNgonNgu, MaNgonNgu, 0))>=1 or (@MaNgonNgu=''))";

            SqlCommand cmd = new SqlCommand(sqlFindId, con);

            if (textBoxTimMaNhanVien3.Text == "") cmd.Parameters.AddWithValue("MaNhanVien", 0);
            else cmd.Parameters.AddWithValue("MaNhanVien", Convert.ToInt32(textBoxTimMaNhanVien3.Text));
            cmd.Parameters.AddWithValue("MaNgonNgu", textBoxTimMaNgonNgu3.Text);
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewNhanVien_NgonNgu.DataSource = dt;
            dataGridViewNhanVien_NgonNgu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (int i = 0; i < dataGridViewNhanVien_NgonNgu.RowCount; i++) dataGridViewNhanVien_NgonNgu.Rows[i].Cells[0].Value = i + 1;
        }

        // Xử lý sự kiện click nút sửa, cho phép sửa dữ liệu   - Sự kiện tự tạo thêm bằng tay
        void buttonSuaNhanVien_NgonNgu_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            // Mở khóa cho phép sửa dữ liệu sau khi click vào nút sửa, không cho phép sửa mã nhân viên và mã khả năng vi tính.
            textBoxSuaMaNhanVien3.ReadOnly = true;
            textBoxSuaMaNgonNgu3.ReadOnly = true;
            textBoxSuaNghe3.ReadOnly = false;
            textBoxSuaNoi3.ReadOnly = false;
            textBoxSuaDoc3.ReadOnly = false;
            textBoxSuaViet3.ReadOnly = false;
        }

        // Xử lý sự kiện click nút xóa, cho phép xóa dữ liệu    - Sự kiện tự tạo thêm bằng tay
        void buttonXoaNhanVien_NgonNgu_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn xóa kỹ năng này", "Xóa Kỹ Năng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                XoaNhanVien_NgonNgu();
                HienThiNhanVien_NgonNgu();
            }
            else
            {

            }
        }

        // Xử lý sự kiện click nút thêm, cho phép thêm dữ liệu
        private void buttonThemNhanVien_NgonNgu3_Click(object sender, EventArgs e)
        {
            int i;
            int count = 0;
            for (i = 0; i < dataGridViewNhanVien_NgonNgu.RowCount - 1; i++)
                if (comboBoxThemMaNhanVien3.Text == dataGridViewNhanVien_NgonNgu.Rows[i].Cells[1].Value.ToString() &&
                    comboBoxThemMaNgonNgu3.Text == dataGridViewNhanVien_NgonNgu.Rows[i].Cells[2].Value.ToString())
                {
                    count++;
                }
            if (count == 1)
            {
                MessageBox.Show("Dữ liệu đã tồn tại", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn thêm trình độ ngoại ngữ?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.Yes)
                {
                    ThemNhanVien_NgonNgu();
                    HienThiNhanVien_NgonNgu();
                }
                else
                {
                    Dispose();
                }
            }
                
        }

        // Xử lý sự kiện click vào mỗi dòng, dữ liệu tự động update vào form sửa
        private void dataGridViewNhanVien_NgonNgu_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < dataGridViewNhanVien_NgonNgu.RowCount - 1)
            {
                // Set giá trị cho các thành phần 

                textBoxSuaMaNhanVien3.Text = dataGridViewNhanVien_NgonNgu.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxSuaMaNgonNgu3.Text = dataGridViewNhanVien_NgonNgu.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBoxSuaNghe3.Text = dataGridViewNhanVien_NgonNgu.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBoxSuaNoi3.Text = dataGridViewNhanVien_NgonNgu.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBoxSuaDoc3.Text = dataGridViewNhanVien_NgonNgu.Rows[e.RowIndex].Cells[5].Value.ToString();
                textBoxSuaViet3.Text = dataGridViewNhanVien_NgonNgu.Rows[e.RowIndex].Cells[6].Value.ToString();

                MaNhanVien = Convert.ToInt32(dataGridViewNhanVien_NgonNgu.Rows[e.RowIndex].Cells[1].Value);
                MaNgonNgu = dataGridViewNhanVien_KNVT.Rows[e.RowIndex].Cells[2].Value.ToString();

                // Khóa việc sửa giá trị trước khi click vào nút sửa.
                textBoxSuaMaNhanVien3.ReadOnly = true;
                textBoxSuaMaNgonNgu3.ReadOnly = true;
                textBoxSuaNghe3.ReadOnly = true;
                textBoxSuaNoi3.ReadOnly = true;
                textBoxSuaDoc3.ReadOnly = true;
                textBoxSuaViet3.ReadOnly = true;


                buttonSuaNhanVien_NgonNgu.Click += buttonSuaNhanVien_NgonNgu_Click;

                buttonXoaNhanVien_NgonNgu.Click += buttonXoaNhanVien_NgonNgu_Click;
            }
        }

        // Xử lý sự kiện click vào nút Lưu lại (buttonSuaNhanVien_NgonNgu3)
        private void buttonSuaNhanVien_NgonNgu3_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Chắc chắn sửa thông tin?", "Sửa Thông Tin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                SuaNhanVien_NgonNgu();

                // Khóa lại chức năng sửa
                textBoxSuaMaNhanVien3.ReadOnly = true;
                textBoxSuaMaNgonNgu3.ReadOnly = true;
                textBoxSuaNghe3.ReadOnly = true;
                textBoxSuaNoi3.ReadOnly = true;
                textBoxSuaDoc3.ReadOnly = true;
                textBoxSuaViet3.ReadOnly = true;

                //Hiển thị lại danh sách
                HienThiNhanVien_NgonNgu();
            }
            else
            {

            }
        }

        // Xử lý sự kiện click nút tìm kiếm
        private void buttonTimNhanVien_NgonNgu3_Click(object sender, EventArgs e)
        {
            TimNhanVien_NgonNgu();
        }

        // Xử lý sự kiện click vào nút Lưu lại (buttonSuaNhanVien_NgonNgu3)
        private void buttonQuayLaiNhanVien_NgonNgu3_Click(object sender, EventArgs e)
        {
            HienThiNhanVien_NgonNgu();
        }
        #endregion

        //------------------------------------------------------------------------------------------------


        #region Xử Lý Nhập Nhân Viên_Công Ty
        // Phương thức hiển thị danh sách nhân viên_CongTy lên dataGridView
        public void HienThiNhanVien_CongTy()
        {
            int i;
            string sqlSelect = "select * from NhanVien_CongTy";
            SqlCommand cmd = new SqlCommand(sqlSelect, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewNhanVien_CongTy.DataSource = dt;
            dataGridViewNhanVien_CongTy.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (i = 0; i < dataGridViewNhanVien_CongTy.RowCount; i++) dataGridViewNhanVien_CongTy.Rows[i].Cells[0].Value = i + 1;
        }

        // Phương thức thêm Nhân Viên_CongTy
        public void ThemNhanVien_CongTy()
        {
            string sqlInsert = "Insert into NhanVien_CongTy values(@MaNhanvien, @MaCongTy," +
                               "@ThoiGianLamViec_BatDau, @ThoiGianLamViec_KetThuc, @ChucVuDaLam, @LinhVuc, @NhiemVu, @Luong)";
            SqlCommand cmd = new SqlCommand(sqlInsert, con);
            cmd.Parameters.AddWithValue("MaNhanVien", Convert.ToInt32(comboBoxThemMaNhanVien4.Text));
            cmd.Parameters.AddWithValue("MaCongTy", comboBoxThemMaCongTy4.Text);
            cmd.Parameters.AddWithValue("ThoiGianLamViec_BatDau", dateTimePickerThemThoiGianBatDau4.Value.Date.Year.ToString() + '/' +
                                                     dateTimePickerThemThoiGianBatDau4.Value.Date.Month.ToString() + '/' +
                                                     dateTimePickerThemThoiGianBatDau4.Value.Date.Day.ToString());
            cmd.Parameters.AddWithValue("ThoiGianLamViec_KetThuc", dateTimePickerThemThoiGianKetThuc4.Value.Date.Year.ToString() + '/' +
                                                     dateTimePickerThemThoiGianKetThuc4.Value.Date.Month.ToString() + '/' +
                                                     dateTimePickerThemThoiGianKetThuc4.Value.Date.Day.ToString());
            cmd.Parameters.AddWithValue("ChucVuDaLam", textBoxThemChucVuDaLam4.Text);
            cmd.Parameters.AddWithValue("LinhVuc", textBoxThemLinhVuc4.Text);
            cmd.Parameters.AddWithValue("NhiemVu", textBoxThemNhiemVu4.Text);
            cmd.Parameters.AddWithValue("Luong", Convert.ToInt32(textBoxThemLuong4.Text));

            cmd.ExecuteNonQuery();
        }

        // Phương thức sửa nhân viên_CongTy
        public void SuaNhanVien_CongTy()
        {
            string sqlUpdate = "Update NhanVien_CongTy Set ThoiGianLamViec_BatDau=@ThoiGianLamViec_BatDau, " +
                "ThoiGianLamViec_KetThuc=@ThoiGianLamViec_KetThuc, ChucVuDaLam=@ChucVuDaLam, LinhVuc=@LinhVuc, NhiemVu=@NhiemVu, Luong=@Luong " +
                "where MaNhanVien=@MaNhanVien and MaCongTy=@MaCongTy";
            SqlCommand cmd = new SqlCommand(sqlUpdate, con);
            cmd.Parameters.AddWithValue("MaNhanVien", Convert.ToInt32(textBoxSuaMaNhanVien4.Text));
            cmd.Parameters.AddWithValue("MaCongTy", textBoxSuaMaCongTy4.Text);
            cmd.Parameters.AddWithValue("ThoiGianLamViec_BatDau", dateTimePickerSuaThoiGianBatDau4.Value.Date.Year.ToString() + '/' +
                                                     dateTimePickerSuaThoiGianBatDau4.Value.Date.Month.ToString() + '/' +
                                                     dateTimePickerSuaThoiGianBatDau4.Value.Date.Day.ToString());
            cmd.Parameters.AddWithValue("ThoiGianLamViec_KetThuc", dateTimePickerSuaThoiGianKetThuc4.Value.Date.Year.ToString() + '/' +
                                         dateTimePickerSuaThoiGianKetThuc4.Value.Date.Month.ToString() + '/' +
                                         dateTimePickerSuaThoiGianKetThuc4.Value.Date.Day.ToString());
            cmd.Parameters.AddWithValue("ChucVuDaLam", textBoxSuaChucVuDaLam4.Text);
            cmd.Parameters.AddWithValue("LinhVuc", textBoxSuaLinhVuc4.Text);
            cmd.Parameters.AddWithValue("NhiemVu", textBoxSuaNhiemVu4.Text);
            cmd.Parameters.AddWithValue("Luong", Convert.ToInt32(textBoxSuaLuong4.Text));


            cmd.ExecuteNonQuery();
        }

        // Phương thức Xóa Nhân Viên_CongTy
        public void XoaNhanVien_CongTy()
        {
            string sqlDelete = "Delete From NhanVien_CongTy Where MaNhanVien=@MaNhanVien and MaCongTy=@MaCongTy";
            SqlCommand cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaNhanVien", MaNhanVien);
            cmd.Parameters.AddWithValue("MaCongTy", MaCongTy);
            cmd.ExecuteNonQuery();
        }

        // Phương thức Tìm nhân viên_CongTy (Mới chỉ tìm theo mã)
        public void TimNhanVien_CongTy()
        {
            string sqlFindId = "Select * from NhanVien_CongTy Where (CharIndex( Convert(Varchar(100), @MaNhanVien) , Convert(Varchar(100), MaNhanVien), 0 ) >=1) and" +
                "                                            ((CharIndex(@MaCongTy, MaCongTy, 0))>=1 or (@MaCongTy=''))";

            SqlCommand cmd = new SqlCommand(sqlFindId, con);

            if (textBoxTimMaNhanVien4.Text == "") cmd.Parameters.AddWithValue("MaNhanVien", 0);
            else cmd.Parameters.AddWithValue("MaNhanVien", Convert.ToInt32(textBoxTimMaNhanVien4.Text));
            cmd.Parameters.AddWithValue("MaCongTy", textBoxTimMaCongTy4.Text);
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewNhanVien_CongTy.DataSource = dt;
            dataGridViewNhanVien_CongTy.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (int i = 0; i < dataGridViewNhanVien_CongTy.RowCount; i++) dataGridViewNhanVien_CongTy.Rows[i].Cells[0].Value = i + 1;
        }

        // Xử lý sự kiện click nút sửa, cho phép sửa dữ liệu   - Sự kiện tự tạo thêm bằng tay
        void buttonSuaNhanVien_CongTy_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            // Mở khóa cho phép sửa dữ liệu sau khi click vào nút sửa, không cho phép sửa mã nhân viên và mã nơi học.
            textBoxSuaMaNhanVien4.ReadOnly = true;
            textBoxSuaMaCongTy4.ReadOnly = true;
            dateTimePickerSuaThoiGianBatDau4.Enabled = true;
            dateTimePickerSuaThoiGianKetThuc4.Enabled = true;
            textBoxSuaChucVuDaLam4.ReadOnly = false;
            textBoxSuaLinhVuc4.ReadOnly = false;
            textBoxSuaNhiemVu4.ReadOnly = false;
            textBoxSuaLuong4.ReadOnly = false;

        }

        // Xử lý sự kiện click nút xóa, cho phép xóa dữ liệu    - Sự kiện tự tạo thêm bằng tay
        void buttonXoaNhanVien_CongTy_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn xóa công ty này", "Xóa Công Ty", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                XoaNhanVien_CongTy();
                HienThiNhanVien_CongTy();
            }
            else
            {

            }
        }

        // Xử lý sự kiện click nút thêm
        private void buttonThemNhanVien_CongTy4_Click(object sender, EventArgs e)
        {
            int i;
            int count = 0;
            for (i = 0; i < dataGridViewNhanVien_CongTy.RowCount - 1; i++)
                if (comboBoxThemMaNhanVien4.Text == dataGridViewNhanVien_CongTy.Rows[i].Cells[1].Value.ToString() &&
                    comboBoxThemMaCongTy4.Text == dataGridViewNhanVien_CongTy.Rows[i].Cells[2].Value.ToString())
                {
                    count++;
                }
            if (count == 1)
            {
                MessageBox.Show("Dữ liệu đã tồn tại", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn thêm quá trình làm việc?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.Yes)
                {
                    ThemNhanVien_CongTy();
                    HienThiNhanVien_CongTy();
                }
                else
                {
                    Dispose();
                }
            }
                
        }
        // Xử lý sự kiện click vào từng dòng trong dataGridView
        private void dataGridViewNhanVien_CongTy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dataGridViewNhanVien_CongTy.RowCount - 1)
            {
                // Set giá trị cho các thành phần 
                DateTime dateTimeBd = new DateTime();
                dateTimeBd = Convert.ToDateTime(dataGridViewNhanVien_CongTy.Rows[e.RowIndex].Cells[3].Value.ToString());
                DateTime dateTimeKt = new DateTime();
                dateTimeKt = Convert.ToDateTime(dataGridViewNhanVien_CongTy.Rows[e.RowIndex].Cells[4].Value.ToString());

                textBoxSuaMaNhanVien4.Text = dataGridViewNhanVien_CongTy.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxSuaMaCongTy4.Text = dataGridViewNhanVien_CongTy.Rows[e.RowIndex].Cells[2].Value.ToString();
                dateTimePickerSuaThoiGianBatDau4.Value = dateTimeBd;
                dateTimePickerSuaThoiGianKetThuc4.Value = dateTimeKt;
                textBoxSuaChucVuDaLam4.Text = dataGridViewNhanVien_CongTy.Rows[e.RowIndex].Cells[5].Value.ToString();
                textBoxSuaLinhVuc4.Text = dataGridViewNhanVien_CongTy.Rows[e.RowIndex].Cells[6].Value.ToString();
                textBoxSuaNhiemVu4.Text = dataGridViewNhanVien_CongTy.Rows[e.RowIndex].Cells[7].Value.ToString();
                textBoxSuaLuong4.Text = dataGridViewNhanVien_CongTy.Rows[e.RowIndex].Cells[8].Value.ToString();



                MaNhanVien = Convert.ToInt32(dataGridViewNhanVien_CongTy.Rows[e.RowIndex].Cells[1].Value);
                MaCongTy = dataGridViewNhanVien_CongTy.Rows[e.RowIndex].Cells[2].Value.ToString();

                // Khóa việc sửa giá trị trước khi click vào nút sửa.
                textBoxSuaMaNhanVien4.ReadOnly = true;
                textBoxSuaMaCongTy4.ReadOnly = true;
                dateTimePickerSuaThoiGianBatDau4.Enabled = false;
                dateTimePickerSuaThoiGianKetThuc4.Enabled = false;
                textBoxSuaChucVuDaLam4.ReadOnly = true;
                textBoxSuaLinhVuc4.ReadOnly = true;
                textBoxSuaNhiemVu4.ReadOnly = true;
                textBoxSuaLuong4.ReadOnly = true;

                buttonSuaNhanVien_CongTy.Click += buttonSuaNhanVien_CongTy_Click;

                buttonXoaNhanVien_CongTy.Click += buttonXoaNhanVien_CongTy_Click;
            }
            else
            {

            }

                
        }

        // Xử lý sự kiện click nút Lưu (buttonSuaNhanVien_CongTy4)
        private void buttonSuaNhanVien_CongTy4_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Chắc chắn sửa thông tin?", "Sửa Thông Tin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                SuaNhanVien_CongTy();

                // Khóa lại chức năng sửa
                textBoxSuaMaNhanVien4.ReadOnly = true;
                textBoxSuaMaCongTy4.ReadOnly = true;
                dateTimePickerSuaThoiGianBatDau4.Enabled = false;
                dateTimePickerSuaThoiGianKetThuc4.Enabled = false;
                textBoxSuaChucVuDaLam4.ReadOnly = true;
                textBoxSuaLinhVuc4.ReadOnly = true;
                textBoxSuaNhiemVu4.ReadOnly = true;
                textBoxSuaLuong4.ReadOnly = true;

                //Hiển thị lại danh sách
                HienThiNhanVien_CongTy();
            }
            else
            {

            }
        }

        // Xử lý sự kiện click nút Tìm
        private void buttonTimNhanVien_CongTy4_Click(object sender, EventArgs e)
        {
            TimNhanVien_CongTy();
        }

        // XỬ lý sự kiện click nút Quay lại, hiển thị toàn bộ danh sách NhanVien_CongTy
        private void buttonQuayLaiNhanVien_CongTy4_Click(object sender, EventArgs e)
        {
            HienThiNhanVien_CongTy();
        }
        #endregion

        //------------------------------------------------------------------------------------------------


        #region Xử Lý Nhập Nhân Viên_khóa huấn luyên
        // Phương thức hiển thị danh sách nhân viên_khóa huấn luyện lên dataGridView
        public void HienThiNhanVien_KhoaHuanLuyen()
        {
            int i;
            string sqlSelect = "select * from NhanVien_KhoaHuanLuyen";
            SqlCommand cmd = new SqlCommand(sqlSelect, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewNhanVien_KHL.DataSource = dt;
            dataGridViewNhanVien_KHL.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (i = 0; i < dataGridViewNhanVien_KHL.RowCount; i++) dataGridViewNhanVien_KHL.Rows[i].Cells[0].Value = i + 1;
        }

        // Phương thức thêm Nhân Viên_Khóa huấn luyện
        public void ThemNhanVien_KhoaHuanLuyen()
        {
            string sqlInsert = "Insert into NhanVien_KhoaHuanLuyen values(@MaNhanvien, @MaKhoaHuanLuyen, @DonViToChuc," +
                               "@ThoiGianHuanLuyen_BatDau, @ThoiGianHuanLuyen_KetThuc, @VanBang)";
            SqlCommand cmd = new SqlCommand(sqlInsert, con);
            cmd.Parameters.AddWithValue("MaNhanVien", Convert.ToInt32(comboBoxThemMaNhanVien5.Text));
            cmd.Parameters.AddWithValue("MaKhoaHuanLuyen", comboBoxThemMaKHL5.Text);
            cmd.Parameters.AddWithValue("ThoiGianHuanLuyen_BatDau", dateTimePickerThemThoiGianBatDau5.Value.Date.Year.ToString() + '/' +
                                                     dateTimePickerThemThoiGianBatDau5.Value.Date.Month.ToString() + '/' +
                                                     dateTimePickerThemThoiGianBatDau5.Value.Date.Day.ToString());
            cmd.Parameters.AddWithValue("ThoiGianHuanLuyen_KetThuc", dateTimePickerThemThoiGianKetThuc5.Value.Date.Year.ToString() + '/' +
                                                     dateTimePickerThemThoiGianKetThuc5.Value.Date.Month.ToString() + '/' +
                                                     dateTimePickerThemThoiGianKetThuc5.Value.Date.Day.ToString());
            cmd.Parameters.AddWithValue("DonViToChuc", textBoxThemDonViToChuc5.Text);
            cmd.Parameters.AddWithValue("VanBang", textBoxThemVanBang5.Text);         
            cmd.ExecuteNonQuery();
        }

        // Phương thức sửa nhân viên_KhoaHuanLuyen
        public void SuaNhanVien_KhoaHuanLuyen()
        {
            string sqlUpdate = "Update NhanVien_KhoaHuanLuyen Set ThoiGianHuanLuyen_BatDau=@ThoiGianHuanLuyen_BatDau, " +
                "ThoiGianHuanLuyen_KetThuc=@ThoiGianHuanLuyen_KetThuc, DonViToChuc=@DonViToChuc, VanBang=@VanBang " +
                "where MaNhanVien=@MaNhanVien and MaKhoaHuanLuyen=@MaKhoaHuanLuyen";
            SqlCommand cmd = new SqlCommand(sqlUpdate, con);
            cmd.Parameters.AddWithValue("MaNhanVien", Convert.ToInt32(textBoxSuaMaNhanVien5.Text));
            cmd.Parameters.AddWithValue("MaKhoaHuanLuyen", textBoxSuaMaKHL5.Text);
            cmd.Parameters.AddWithValue("ThoiGianHuanLuyen_BatDau", dateTimePickerSuaThoiGianBatDau5.Value.Date.Year.ToString() + '/' +
                                                     dateTimePickerSuaThoiGianBatDau5.Value.Date.Month.ToString() + '/' +
                                                     dateTimePickerSuaThoiGianBatDau5.Value.Date.Day.ToString());
            cmd.Parameters.AddWithValue("ThoiGianHuanLuyen_KetThuc", dateTimePickerSuaThoiGianKetThuc5.Value.Date.Year.ToString() + '/' +
                                         dateTimePickerSuaThoiGianKetThuc5.Value.Date.Month.ToString() + '/' +
                                         dateTimePickerSuaThoiGianKetThuc5.Value.Date.Day.ToString());
            cmd.Parameters.AddWithValue("DonViToChuc", textBoxSuaDonViToChuc5.Text);
            cmd.Parameters.AddWithValue("VanBang", textBoxSuaVanBang5.Text);

            cmd.ExecuteNonQuery();
        }

        // Phương thức Xóa Nhân Viên_CongTy
        public void XoaNhanVien_KhoaHuanLuyen()
        {
            string sqlDelete = "Delete From NhanVien_KhoaHuanLuyen Where MaNhanVien=@MaNhanVien and MaKhoaHuanLuyen=@MaKhoaHuanLuyen";
            SqlCommand cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaNhanVien", MaNhanVien);
            cmd.Parameters.AddWithValue("MaKhoaHuanLuyen", MaKHL);
            cmd.ExecuteNonQuery();
        }

        // Phương thức Tìm nhân viên_CongTy (Mới chỉ tìm theo mã)
        public void TimNhanVien_KhoaHuanLuyen()
        {
            string sqlFindId = "Select * from NhanVien_KhoaHuanLuyen Where (CharIndex( Convert(Varchar(100), @MaNhanVien) , Convert(Varchar(100), MaNhanVien), 0 ) >=1) and" +
                "                                            ((CharIndex(@MaKhoaHuanLuyen, MaKhoaHuanLuyen, 0))>=1 or (@MaKhoaHuanLuyen=''))";

            SqlCommand cmd = new SqlCommand(sqlFindId, con);

            if (textBoxTimMaNhanVien5.Text == "") cmd.Parameters.AddWithValue("MaNhanVien", 0);
            else cmd.Parameters.AddWithValue("MaNhanVien", Convert.ToInt32(textBoxTimMaNhanVien5.Text));
            cmd.Parameters.AddWithValue("MaKhoaHuanLuyen", textBoxTimMaKHL5.Text);
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewNhanVien_KHL.DataSource = dt;
            dataGridViewNhanVien_KHL.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (int i = 0; i < dataGridViewNhanVien_KHL.RowCount; i++) dataGridViewNhanVien_KHL.Rows[i].Cells[0].Value = i + 1;
        }

        // Xử lý sự kiện click nút sửa, cho phép sửa dữ liệu   - Sự kiện tự tạo thêm bằng tay
        void buttonSuaNhanVien_KhoaHuanLuyen_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            // Mở khóa cho phép sửa dữ liệu sau khi click vào nút sửa, không cho phép sửa mã nhân viên và mã nơi học.
            textBoxSuaMaNhanVien5.ReadOnly = true;
            textBoxSuaMaKHL5.ReadOnly = true;
            dateTimePickerSuaThoiGianBatDau5.Enabled = true;
            dateTimePickerSuaThoiGianKetThuc5.Enabled = true;
            textBoxSuaDonViToChuc5.ReadOnly = false;
            textBoxSuaVanBang5.ReadOnly = false;
        }

        // Xử lý sự kiện click nút xóa, cho phép xóa dữ liệu    - Sự kiện tự tạo thêm bằng tay
        void buttonXoaNhanVien_KhoaHuanLuyen_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn xóa khóa huấn luyện này", "Xóa Khóa Huấn Luyện", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                XoaNhanVien_KhoaHuanLuyen();
                HienThiNhanVien_KhoaHuanLuyen();
            }
            else
            {

            }
        }


        private void buttonThemNhanVien_KHL5_Click(object sender, EventArgs e)
        {
            int i;
            int count = 0;
            for (i = 0; i < dataGridViewNhanVien_KHL.RowCount - 1; i++)
                if (comboBoxThemMaNhanVien5.Text == dataGridViewNhanVien_KHL.Rows[i].Cells[1].Value.ToString() &&
                    comboBoxThemMaKHL5.Text == dataGridViewNhanVien_KHL.Rows[i].Cells[2].Value.ToString())
                {
                    count++;
                }
            if (count == 1)
            {
                MessageBox.Show("Dữ liệu đã tồn tại", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn thêm khóa huấn luyện?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.Yes)
                {
                    ThemNhanVien_KhoaHuanLuyen();
                    HienThiNhanVien_KhoaHuanLuyen();
                }
                else
                {
                    Dispose();
                }
            }
                
        }

        private void dataGridViewNhanVien_KHL_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dataGridViewNhanVien_KHL.RowCount - 1)
            {
                // Set giá trị cho các thành phần 
                DateTime dateTimeBd = new DateTime();
                dateTimeBd = Convert.ToDateTime(dataGridViewNhanVien_KHL.Rows[e.RowIndex].Cells[4].Value.ToString());
                DateTime dateTimeKt = new DateTime();
                dateTimeKt = Convert.ToDateTime(dataGridViewNhanVien_KHL.Rows[e.RowIndex].Cells[5].Value.ToString());

                textBoxSuaMaNhanVien5.Text = dataGridViewNhanVien_KHL.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxSuaMaKHL5.Text = dataGridViewNhanVien_KHL.Rows[e.RowIndex].Cells[2].Value.ToString();
                dateTimePickerSuaThoiGianBatDau5.Value = dateTimeBd;
                dateTimePickerSuaThoiGianKetThuc5.Value = dateTimeKt;
                textBoxSuaDonViToChuc5.Text = dataGridViewNhanVien_KHL.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBoxSuaVanBang5.Text = dataGridViewNhanVien_KHL.Rows[e.RowIndex].Cells[6].Value.ToString();

                MaNhanVien = Convert.ToInt32(dataGridViewNhanVien_KHL.Rows[e.RowIndex].Cells[1].Value);
                MaKHL = dataGridViewNhanVien_KHL.Rows[e.RowIndex].Cells[2].Value.ToString();

                // Khóa việc sửa giá trị trước khi click vào nút sửa.
                textBoxSuaMaNhanVien5.ReadOnly = true;
                textBoxSuaMaKHL5.ReadOnly = true;
                dateTimePickerSuaThoiGianBatDau5.Enabled = false;
                dateTimePickerSuaThoiGianKetThuc5.Enabled = false;
                textBoxSuaDonViToChuc5.ReadOnly = true;
                textBoxSuaVanBang5.ReadOnly = true;

                buttonSuaNhanVien_KHL.Click += buttonSuaNhanVien_KhoaHuanLuyen_Click;

                buttonXoaNhanVien_KHL.Click += buttonXoaNhanVien_KhoaHuanLuyen_Click;
            }
            else
            {

            }
                
        }

        private void buttonSuaNhanVien_KHL5_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Chắc chắn sửa thông tin?", "Sửa Thông Tin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                SuaNhanVien_KhoaHuanLuyen();

                // Khóa lại chức năng sửa
                textBoxSuaMaNhanVien5.ReadOnly = true;
                textBoxSuaMaKHL5.ReadOnly = true;
                dateTimePickerSuaThoiGianBatDau5.Enabled = false;
                dateTimePickerSuaThoiGianKetThuc5.Enabled = false;
                textBoxSuaDonViToChuc5.ReadOnly = true;
                textBoxSuaVanBang5.ReadOnly = true;

                //Hiển thị lại danh sách
                HienThiNhanVien_KhoaHuanLuyen();
            }
            else
            {

            }
        }

        private void buttonTimNhanVien_KHL5_Click(object sender, EventArgs e)
        {
            TimNhanVien_KhoaHuanLuyen();
        }

        private void buttonQuayLaiNhanVien_KHL5_Click(object sender, EventArgs e)
        {
            HienThiNhanVien_KhoaHuanLuyen();
        }
        #endregion

    }
}
