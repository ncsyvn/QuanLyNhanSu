﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace QuanLyNhanSu
{
    public partial class DanhMuc : Form
    {
        public DanhMuc()
        {
            InitializeComponent();
        }

        #region Properties
        private int MaNhanVien;

        private string MaNoiHoc;

        private string MaNgonNgu;

        private string MaKhaNangViTinh;

        private string MaKhoaHuanLuyen;

        private string MaCongTy;

        private string MaPhongBan;

        public static DataGridView dataGridView = new DataGridView();
        #endregion

        //------------------------------------------------------------------------------------------------

        #region Load DataGridView

        #region Load DatagripView NhanVien Cách 1 - OK
        /*
        private void DanhMuc_Load(object sender, EventArgs e)
        {
            dataGridViewNhanVien.DataSource = GetAllNhanVien().Tables[0];
            dataGridViewNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        string query = "Select * from NhanVien";
        DataSet GetAllNhanVien()
        {
            DataSet data = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(data);
                connection.Close();
            }
            return data;
        }
        */
        #endregion


        #region Load DatagridView NhanVien cách 2 - OK

        SqlConnection con;
        // Load danh sách nhân viên ngay khi mở form
        private void DanhMuc_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'n05_Ql_NhanSu_T5DataSet3.NoiHoc' table. You can move, or remove it, as needed.
            this.noiHocTableAdapter.Fill(this.n05_Ql_NhanSu_T5DataSet3.NoiHoc);
            // TODO: This line of code loads data into the 'n05_Ql_NhanSu_T5DataSet2.PhongBan' table. You can move, or remove it, as needed.
            this.phongBanTableAdapter.Fill(this.n05_Ql_NhanSu_T5DataSet2.PhongBan);
            string conString = ConfigurationManager.ConnectionStrings["QuanLyNhanSu"].ConnectionString.ToString();
            con = new SqlConnection(conString);
            con.Open();

            HienThiNhanVien();
            textBoxThemMaNhanVien1.Text = (Convert.ToInt32(dataGridViewNhanVien.Rows[dataGridViewNhanVien.RowCount - 2].Cells[1].Value) + 1).ToString();
            textBoxThemMaNhanVien1.ReadOnly = true;

            HienThiNoiHoc();

            HienThiNgonNgu();

            HienThiKhoaHuanLuyen();

            HienThiKhaNangViTinh();

            HienThiCongTy();

            HienThiPhongBan();
        }

        // Đóng kết nối sau khi tắt form
        private void DanhMuc_FormClosing(object sender, FormClosingEventArgs e)
        {
            con.Close();
        }



        #endregion

        #endregion

        //--------------------------------------------------------------------------------------------------

        #region Xử Lý danh mục Nhân Viên
        // Phương thức hiển thị danh sách nhân viên lên dataGridView
        public void HienThiNhanVien()
        {
            int i;
            string sqlSelect = "select * from NhanVien";
            SqlCommand cmd = new SqlCommand(sqlSelect, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewNhanVien.DataSource = dt;
            dataGridViewNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.DataSource = dt;
            for (i = 0; i < dataGridViewNhanVien.RowCount; i++) dataGridViewNhanVien.Rows[i].Cells[0].Value = i + 1;
        }

        // Phương thức thêm Nhân Viên
        public void ThemNhanVien()
        {
            int count = 0;
            if (textBoxThemHoVaTen1.Text == "")
            {
                textBoxThemHoVaTen1.BackColor = Color.Pink;
                textBoxThemHoVaTen1.Text = "Thiếu thông tin";
                count++;
            }
            if (textBoxThemSoDienThoai1.Text == "")
            {
                textBoxThemSoDienThoai1.BackColor = Color.Pink;
                textBoxThemSoDienThoai1.Text = "Thiếu thông tin";
                count++;
            }
            if (textBoxThemSoCMTND1.Text == "")
            {
                textBoxThemSoCMTND1.BackColor = Color.Pink;
                textBoxThemSoCMTND1.Text = "0";
                count++;
            }
            if (textBoxThemNoiCapCMTND1.Text == "")
            {
                textBoxThemNoiCapCMTND1.BackColor = Color.Pink;
                textBoxThemNoiCapCMTND1.Text = "Thiếu thông tin";
                count++;
            }
            if (textBoxThemChucVu1.Text == "")
            {
                textBoxThemChucVu1.BackColor = Color.Pink;
                textBoxThemChucVu1.Text = "Thiếu thông tin";
                count++;
            }
            if (textBoxThemNoiSinh1.Text == "")
            {
                textBoxThemNoiSinh1.BackColor = Color.Pink;
                textBoxThemNoiSinh1.Text = "Thiếu thông tin";
                count++;
            }
            if (comboBoxThemGioiTinh1.Text == "")
            {
                comboBoxThemGioiTinh1.BackColor = Color.Pink;
                comboBoxThemGioiTinh1.Text = "Thiếu thông tin";
                count++;
            }


            if (CheckInt(textBoxThemSoCMTND1.Text) == 0)
            {
                DialogResult dlr= MessageBox.Show("Mời bạn xem lại thông tin số CMTND", "CMTND Sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (dlr==DialogResult.OK)
                {
                    textBoxThemSoCMTND1.Text = "0";
                }

            }

            if (count != 0)
            {
                MessageBox.Show("Mời bạn điền đủ thông tin", "Thiếu Thông Tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string sqlInsert = "Insert into NhanVien values(@MaNhanvien, @HoVaTen," +
                               " @NgaySinh, @GioiTinh, @NoiSinh, @SoCMTND, @NoiCapCMTND, " +
                               "@SoDienThoai, @ChucVu, @MaPhongBan)";
                SqlCommand cmd = new SqlCommand(sqlInsert, con);
                cmd.Parameters.AddWithValue("MaNhanVien", Convert.ToInt32(textBoxThemMaNhanVien1.Text));
                cmd.Parameters.AddWithValue("HoVaTen", textBoxThemHoVaTen1.Text);
                cmd.Parameters.AddWithValue("NgaySinh", dateTimePickerThemNgaySinh1.Value.Date.Year.ToString() + '/' +
                                                         dateTimePickerThemNgaySinh1.Value.Date.Month.ToString() + '/' +
                                                         dateTimePickerThemNgaySinh1.Value.Date.Day.ToString());
                cmd.Parameters.AddWithValue("GioiTinh", Convert.ToInt32(comboBoxThemGioiTinh1.Text));
                cmd.Parameters.AddWithValue("NoiSinh", textBoxThemNoiSinh1.Text);
                cmd.Parameters.AddWithValue("SoCMTND", Convert.ToInt32(textBoxThemSoCMTND1.Text));
                cmd.Parameters.AddWithValue("NoiCapCMTND", textBoxThemNoiCapCMTND1.Text);
                cmd.Parameters.AddWithValue("SoDienThoai", textBoxThemSoDienThoai1.Text);
                cmd.Parameters.AddWithValue("ChucVu", textBoxThemChucVu1.Text);
                cmd.Parameters.AddWithValue("MaPhongBan", comboBoxThemMaPhongBan1.Text);

                cmd.ExecuteNonQuery();
                

                textBoxThemMaNhanVien1.Text = "";
                textBoxThemHoVaTen1.Text = "";
                textBoxThemSoCMTND1.Text = "";
                textBoxThemNoiSinh1.Text = "";
                textBoxThemNoiCapCMTND1.Text = "";
                comboBoxThemGioiTinh1.Text = "";
                textBoxThemSoDienThoai1.Text = "";
                textBoxThemChucVu1.Text = "";
            }

        }

        // Phương thức sửa nhân viên
        public void SuaNhanVien()
        {
            string sqlUpdate = "Update NhanVien Set HoVaTen=@HoVaTen," +
                " NgaySinh=@NgaySinh, GioiTinh=@GioiTinh, NoiSinh=@NoiSinh, " +
                "SoCMTND=@SoCMTND, NoiCapCMTND=@NoiCapCMTND, SoDienThoai=@SoDienThoai," +
                " ChucVu=@ChucVu, MaPhongBan=@MaPhongBan " +
                "where MaNhanVien=@MaNhanVien ";
            SqlCommand cmd = new SqlCommand(sqlUpdate, con);
            cmd.Parameters.AddWithValue("MaNhanVien", Convert.ToInt32(textBoxSuaMaNhanVien1.Text));
            cmd.Parameters.AddWithValue("HoVaTen", textBoxSuaHoVaTen1.Text);
            cmd.Parameters.AddWithValue("NgaySinh", dateTimePickerSuaNgaySinh1.Value.Date.Year.ToString() + '/' +
                                                     dateTimePickerSuaNgaySinh1.Value.Date.Month.ToString() + '/' +
                                                     dateTimePickerSuaNgaySinh1.Value.Date.Day.ToString());
            cmd.Parameters.AddWithValue("GioiTinh", Convert.ToInt32(comboBoxSuaGioiTinh1.Text));
            cmd.Parameters.AddWithValue("NoiSinh", textBoxSuaNoiSinh1.Text);
            cmd.Parameters.AddWithValue("SoCMTND", textBoxSuaSoCMTND1.Text);
            cmd.Parameters.AddWithValue("NoiCapCMTND", textBoxSuaNoiCapCMTND1.Text);
            cmd.Parameters.AddWithValue("SoDienThoai", textBoxSuaSoDienThoai1.Text);
            cmd.Parameters.AddWithValue("ChucVu", textBoxSuaChucVu1.Text);
            cmd.Parameters.AddWithValue("MaPhongBan", comboBoxSuaMaPhongBan1.Text);
            cmd.ExecuteNonQuery();
        }

        // Phương thức Xóa Nhân Viên
        public void XoaNhanVien()
        {
            string sqlDelete = "Delete From NhanVien Where MaNhanVien=@MaNhanVien";
            SqlCommand cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaNhanVien", MaNhanVien);
            cmd.ExecuteNonQuery();
        }

        // Phương thức Tìm nhân viên (Mới chỉ tìm theo mã hoặc tên)
        public void TimNhanVien()
        {
            string sqlFindId = "Select * from NhanVien Where (CharIndex( Convert(Varchar(100), @MaNhanVien) , Convert(Varchar(100), MaNhanVien), 0 ) >=1) and" +
                "                                            ((CharIndex(@HoVaTen, HoVaTen, 0))>=1 or (@HoVaTen=''))";

            SqlCommand cmd = new SqlCommand(sqlFindId, con);

            if (textBoxTimMaNhanVien1.Text == "") cmd.Parameters.AddWithValue("MaNhanVien", 0);
            else cmd.Parameters.AddWithValue("MaNhanVien", Convert.ToInt32(textBoxTimMaNhanVien1.Text));
            cmd.Parameters.AddWithValue("HoVaTen", textBoxTimHoVaTen1.Text);
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewNhanVien.DataSource = dt;
            dataGridViewNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (int i = 0; i < dataGridViewNhanVien.RowCount; i++) dataGridViewNhanVien.Rows[i].Cells[0].Value = i + 1;
        }

        // Xử lý sự kiện click nút Thêm
        private void buttonThemNhanVien1_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn thêm nhân viên?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                ThemNhanVien();
                HienThiNhanVien();
                textBoxThemMaNhanVien1.Text = (Convert.ToInt32(dataGridViewNhanVien.Rows[dataGridViewNhanVien.RowCount - 2].Cells[1].Value) + 1).ToString();
                //Dispose();
            }
            else
            {
                Dispose();
            }
        }

        // Xử lý sự kiện click vào mỗi dòng, dữ liệu tự động update vào form sửa
        private void dataGridViewNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dataGridViewNhanVien.RowCount - 1)
            {
                // Set giá trị cho các thành phần 
                DateTime dateTime = new DateTime();
                dateTime = Convert.ToDateTime(dataGridViewNhanVien.Rows[e.RowIndex].Cells[3].Value.ToString());

                textBoxSuaMaNhanVien1.Text = dataGridViewNhanVien.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxSuaHoVaTen1.Text = dataGridViewNhanVien.Rows[e.RowIndex].Cells[2].Value.ToString();
                dateTimePickerSuaNgaySinh1.Value = dateTime;
                comboBoxSuaGioiTinh1.Text = dataGridViewNhanVien.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBoxSuaNoiSinh1.Text = dataGridViewNhanVien.Rows[e.RowIndex].Cells[5].Value.ToString();
                textBoxSuaSoCMTND1.Text = dataGridViewNhanVien.Rows[e.RowIndex].Cells[6].Value.ToString();
                textBoxSuaNoiCapCMTND1.Text = dataGridViewNhanVien.Rows[e.RowIndex].Cells[7].Value.ToString();
                textBoxSuaSoDienThoai1.Text = dataGridViewNhanVien.Rows[e.RowIndex].Cells[8].Value.ToString();
                textBoxSuaChucVu1.Text = dataGridViewNhanVien.Rows[e.RowIndex].Cells[9].Value.ToString();
                comboBoxSuaMaPhongBan1.Text = dataGridViewNhanVien.Rows[e.RowIndex].Cells[10].Value.ToString();

                MaNhanVien = Convert.ToInt32(dataGridViewNhanVien.Rows[e.RowIndex].Cells[1].Value);
                MaPhongBan = dataGridViewNhanVien.Rows[e.RowIndex].Cells[10].Value.ToString();

                buttonSuaNhanVien.Click += buttonSuaNhanVien_Click;

                // Khóa việc sửa giá trị trước khi click vào nút sửa.
                textBoxSuaMaNhanVien1.ReadOnly = true;
                textBoxSuaHoVaTen1.ReadOnly = true;
                dateTimePickerSuaNgaySinh1.Enabled = false;
                comboBoxSuaGioiTinh1.Enabled = false;
                textBoxSuaNoiSinh1.ReadOnly = true;
                textBoxSuaSoCMTND1.ReadOnly = true;
                textBoxSuaNoiCapCMTND1.ReadOnly = true;
                textBoxSuaSoDienThoai1.ReadOnly = true;
                textBoxSuaChucVu1.ReadOnly = true;
                comboBoxSuaMaPhongBan1.Enabled = false;

                buttonXoaNhanVien.Click -= buttonXoaNhanVien_Click;
                buttonXoaNhanVien.Click += buttonXoaNhanVien_Click;
            }

        }

        // Xử lý sự kiện click nút sửa, cho phép sửa dữ liệu     - Sự kiện tự tạo thêm bằng tay
        void buttonSuaNhanVien_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            // Mở khóa cho phép sửa dữ liệu sau khi click vào nút sửa, không cho phép sửa mã nhân viên.
            textBoxSuaMaNhanVien1.ReadOnly = true;
            textBoxSuaHoVaTen1.ReadOnly = false;
            comboBoxSuaGioiTinh1.Enabled = true;
            dateTimePickerSuaNgaySinh1.Enabled = true;
            textBoxSuaNoiSinh1.ReadOnly = false;
            textBoxSuaSoCMTND1.ReadOnly = false;
            textBoxSuaNoiCapCMTND1.ReadOnly = false;
            textBoxSuaSoDienThoai1.ReadOnly = false;
            textBoxSuaChucVu1.ReadOnly = false;
            comboBoxSuaMaPhongBan1.Enabled = true;
        }

        // Xử lý sự kiện click vào nút Lưu lại (buttonSuaNhanVien1)
        private void buttonSuaNhanVien1_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Chắc chắn sửa thông tin?", "Sửa Thông Tin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                SuaNhanVien();

                // Khóa lại chức năng sửa
                textBoxSuaMaNhanVien1.ReadOnly = true;
                textBoxSuaHoVaTen1.ReadOnly = true;
                dateTimePickerSuaNgaySinh1.Enabled = false;
                comboBoxSuaGioiTinh1.Enabled = false;
                textBoxSuaNoiSinh1.ReadOnly = true;
                textBoxSuaSoCMTND1.ReadOnly = true;
                textBoxSuaNoiCapCMTND1.ReadOnly = true;
                textBoxSuaSoDienThoai1.ReadOnly = true;
                textBoxSuaChucVu1.ReadOnly = true;
                comboBoxSuaMaPhongBan1.Enabled = false;

                //Hiển thị lại danh sách
                HienThiNhanVien();
            }
            else
            {

            }
        }

        // Xử lý sự kiện click nút xóa, cho phép xóa dữ liệu   - Sự kiện tự tạo thêm bằng tay
        void buttonXoaNhanVien_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            DataGridView dataGridView = sender as DataGridView;
            DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn xóa nhân viên", "Xóa Nhân Viên", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                XoaDuLieuNhanVien_Conflig(MaNhanVien);
                XoaNhanVien();
                HienThiNhanVien();
            }
            else
            {

            }
        }


        // Xử Lý sự kiện click nút tìm kiếm
        private void buttonTimNhanVien1_Click(object sender, EventArgs e)
        {
            TimNhanVien();
        }

        // Click nút quay lại, hiển thị đầy đủ danh sách nhân viên
        private void buttonQuayLaiNhanVien1_Click(object sender, EventArgs e)
        {
            HienThiNhanVien();
        }




        // Các sự kiện xử lý khi nhập thiếu thông tin trong lúc thêm
        private void textBoxThemHoVaTen1_DoubleClick(object sender, EventArgs e)
        {
            textBoxThemHoVaTen1.Text = "";
            textBoxThemHoVaTen1.BackColor = Color.White;
        }

        private void textBoxThemSoDienThoai1_DoubleClick(object sender, EventArgs e)
        {
            textBoxThemSoDienThoai1.Text = "";
            textBoxThemSoDienThoai1.BackColor = Color.White;
        }



        private void textBoxThemSoCMTND1_DoubleClick(object sender, EventArgs e)
        {
            textBoxThemSoCMTND1.Text = "";
            textBoxThemSoCMTND1.BackColor = Color.White;
        }

        private void textBoxThemChucVu1_DoubleClick(object sender, EventArgs e)
        {
            textBoxThemChucVu1.Text = "";
            textBoxThemChucVu1.BackColor = Color.White;
        }

        private void textBoxThemNoiSinh1_DoubleClick(object sender, EventArgs e)
        {
            textBoxThemNoiSinh1.Text = "";
            textBoxThemNoiSinh1.BackColor = Color.White;
        }

        private void textBoxThemNoiCapCMTND1_DoubleClick(object sender, EventArgs e)
        {
            textBoxThemNoiCapCMTND1.Text = "";
            textBoxThemNoiCapCMTND1.BackColor = Color.White;
        }

        #endregion

        //------------------------------------------------------------------------------------------------

        #region Xử lý Danh Mục NoiHoc

        // Phương thức Hiển thị Nơi Học
        public void HienThiNoiHoc()
        {
            int i;
            string sqlSelect = "select * from NoiHoc";
            SqlCommand cmd = new SqlCommand(sqlSelect, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewNoiHoc.DataSource = dt;
            dataGridViewNoiHoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (i = 0; i < dataGridViewNoiHoc.RowCount; i++) dataGridViewNoiHoc.Rows[i].Cells[0].Value = i + 1;
        }

        // Phương thức thêm Nơi Hoc
        public void ThemNoiHoc()
        {
            string sqlInsert = "Insert into NoiHoc values(@MaNoiHoc, @TenNoiHoc)";
            SqlCommand cmd = new SqlCommand(sqlInsert, con);
            cmd.Parameters.AddWithValue("MaNoiHoc", textBoxThemMaNoiHoc2.Text);
            cmd.Parameters.AddWithValue("TenNoiHoc", textBoxThemTenNoiHoc2.Text);
            cmd.ExecuteNonQuery();
            textBoxThemMaNoiHoc2.Text = "";
            textBoxThemTenNoiHoc2.Text = "";
        }

        // Phương thức sửa nơi học
        public void SuaNoiHoc()
        {
            string sqlUpdate = "Update NoiHoc Set TenNoiHoc=@TenNoiHoc where MaNoiHoc=@MaNoiHoc";
            SqlCommand cmd = new SqlCommand(sqlUpdate, con);
            cmd.Parameters.AddWithValue("MaNoiHoc", textBoxSuaMaNoiHoc2.Text);
            cmd.Parameters.AddWithValue("TenNoiHoc", textBoxSuaTenNoiHoc2.Text);

            cmd.ExecuteNonQuery();
        }

        // Phương thức Xóa Nơi Học
        public void XoaNoiHoc()
        {
            string sqlDelete = "Delete From NoiHoc Where MaNoiHoc=@MaNoiHoc";
            SqlCommand cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaNoiHoc", MaNoiHoc);
            cmd.ExecuteNonQuery();
        }

        // Phương thức tìm Nơi học
        public void TimNoiHoc()
        {
            string sqlFindId = "Select * from NoiHoc Where (CharIndex( @MaNoiHoc , MaNoiHoc, 0 ) >=1 or @MaNoiHoc='') and" +
                                                          "(CharIndex(@TenNoiHoc, TenNoiHoc, 0)>=1 or @TenNoiHoc='')";

            SqlCommand cmd = new SqlCommand(sqlFindId, con);

            cmd.Parameters.AddWithValue("MaNoiHoc", textBoxTimMaNoiHoc2.Text);
            cmd.Parameters.AddWithValue("TenNoiHoc", textBoxTimTenNoiHoc2.Text);

            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewNoiHoc.DataSource = dt;
            dataGridViewNoiHoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (int i = 0; i < dataGridViewNoiHoc.RowCount; i++) dataGridViewNoiHoc.Rows[i].Cells[0].Value = i + 1;
        }

        // Xử lý sự kiện click nút sửa, cho phép sửa dữ liệu   - Sự kiện tự tạo thêm bằng tay
        void buttonSuaNoiHoc_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            // Mở khóa cho phép sửa dữ liệu sau khi click vào nút sửa, không cho phép sửa mã nơi học.
            textBoxSuaMaNoiHoc2.ReadOnly = true;
            textBoxSuaTenNoiHoc2.ReadOnly = false;

        }
        // Xử lý sự kiện click nút xóa, cho phép xóa dữ liệu    - Sự kiện tự tạo thêm bằng tay
        void buttonXoaNoiHoc_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn xóa nơi học", "Xóa Nơi Học", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                XoaNhanVien_NoiHoc_Conflig(MaNoiHoc);
                XoaNoiHoc();
                HienThiNoiHoc();
            }
            else
            {

            }
        }

        // Xử lý sự kiện click nút thêm, cho phép thêm dữ liệu
        private void buttonThemNoiHoc2_Click(object sender, EventArgs e)
        {
            int i;
            int count = 0;
            for (i = 0; i < dataGridViewNoiHoc.RowCount - 1; i++)
                if (textBoxThemMaNoiHoc2.Text == dataGridViewNoiHoc.Rows[i].Cells[1].Value.ToString())
                {
                    count++;
                }
            if (count >= 1)
            {
                MessageBox.Show("Mã nơi học đã tồn tại", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn thêm nơi học?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.Yes)
                {
                    ThemNoiHoc();
                    HienThiNoiHoc();
                }
                else
                {
                    Dispose();
                }
            }

        }

        // Xử lý sự kiện click vào mỗi dòng, dữ liệu tự động update vào form sửa
        private void dataGridViewNoiHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dataGridViewNoiHoc.RowCount - 1)
            {
                // Set giá trị cho các thành phần 

                textBoxSuaMaNoiHoc2.Text = dataGridViewNoiHoc.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxSuaTenNoiHoc2.Text = dataGridViewNoiHoc.Rows[e.RowIndex].Cells[2].Value.ToString();

                MaNoiHoc = dataGridViewNoiHoc.Rows[e.RowIndex].Cells[1].Value.ToString();

                buttonSuaNoiHoc.Click += buttonSuaNoiHoc_Click;

                // Khóa việc sửa giá trị trước khi click vào nút sửa.
                textBoxSuaMaNoiHoc2.ReadOnly = true;
                textBoxSuaMaNoiHoc2.ReadOnly = true;

                buttonXoaNoiHoc.Click -= buttonXoaNoiHoc_Click;
                buttonXoaNoiHoc.Click += buttonXoaNoiHoc_Click;
            }
        }

        // Xử lý sự kiện click vào nút Lưu lại (buttonSuaNoiHoc2)
        private void buttonSuaNoiHoc2_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Chắc chắn sửa thông tin?", "Sửa Thông Tin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                SuaNoiHoc();

                // Khóa lại chức năng sửa
                textBoxSuaMaNoiHoc2.ReadOnly = true;
                textBoxSuaTenNoiHoc2.ReadOnly = true;

                //Hiển thị lại danh sách
                HienThiNoiHoc();
            }
            else
            {

            }
        }

        // Xử lý sự kiện click nút tìm kiếm
        private void buttonTimNoiHoc2_Click(object sender, EventArgs e)
        {
            TimNoiHoc();
        }

        // Click nút quay lại, hiển thị toàn bộ dữ liệu nơi học
        private void buttonQuayLaiNoiHoc2_Click(object sender, EventArgs e)
        {
            HienThiNoiHoc();
        }

        #endregion

        //-------------------------------------------------------------------------------------------------

        #region Xử lý Danh Mục Ngôn ngữ
        // Phương thức Hiển thị Ngôn Ngữ
        public void HienThiNgonNgu()
        {
            int i;
            string sqlSelect = "select * from NgonNgu";
            SqlCommand cmd = new SqlCommand(sqlSelect, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewNgonNgu.DataSource = dt;
            dataGridViewNgonNgu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (i = 0; i < dataGridViewNgonNgu.RowCount; i++) dataGridViewNgonNgu.Rows[i].Cells[0].Value = i + 1;
        }

        // Phương thức thêm NgonNgu
        public void ThemNgonNgu()
        {
            string sqlInsert = "Insert into NgonNgu values(@MaNgonNgu, @TenNgonNgu)";
            SqlCommand cmd = new SqlCommand(sqlInsert, con);
            cmd.Parameters.AddWithValue("MaNgonNgu", textBoxThemMaNgonNgu3.Text);
            cmd.Parameters.AddWithValue("TenNgonNgu", textBoxThemTenNgonNgu3.Text);
            cmd.ExecuteNonQuery();
            textBoxThemMaNgonNgu3.Text = "";
            textBoxThemTenNgonNgu3.Text = "";
        }

        // Phương thức sửa ngôn ngữ
        public void SuaNgonNgu()
        {
            string sqlUpdate = "Update NgonNgu Set TenNgonNgu=@TenNgonNgu where MaNgonNgu=@MaNgonNgu";
            SqlCommand cmd = new SqlCommand(sqlUpdate, con);
            cmd.Parameters.AddWithValue("MaNgonNgu", textBoxSuaMaNgonNgu3.Text);
            cmd.Parameters.AddWithValue("TenNgonNgu", textBoxSuaTenNgonNgu3.Text);

            cmd.ExecuteNonQuery();
        }

        // Phương thức Xóa Ngôn ngữ
        public void XoaNgonNgu()
        {
            string sqlDelete = "Delete From NgonNgu Where MaNgonNgu=@MaNgonNgu";
            SqlCommand cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaNgonNgu", MaNgonNgu);
            cmd.ExecuteNonQuery();
        }

        // Phương thức tìm Nơi học
        public void TimNgonNgu()
        {
            string sqlFindId = "Select * from NgonNgu Where (CharIndex( @MaNgonNgu , MaNgonNgu, 0 ) >=1 or @MaNgonNgu='') and" +
                                                          "(CharIndex(@TenNgonNgu, TenNgonNgu, 0)>=1 or @TenNgonNgu='')";

            SqlCommand cmd = new SqlCommand(sqlFindId, con);

            cmd.Parameters.AddWithValue("MaNgonNgu", textBoxTimMaNgonNgu3.Text);
            cmd.Parameters.AddWithValue("TenNgonNgu", textBoxTimTenNgonNgu3.Text);

            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewNgonNgu.DataSource = dt;
            dataGridViewNgonNgu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (int i = 0; i < dataGridViewNgonNgu.RowCount; i++) dataGridViewNgonNgu.Rows[i].Cells[0].Value = i + 1;
        }

        // Xử lý sự kiện click nút sửa, cho phép sửa dữ liệu   - Sự kiện tự tạo thêm bằng tay
        void buttonSuaNgonNgu_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            // Mở khóa cho phép sửa dữ liệu sau khi click vào nút sửa, không cho phép sửa mã nơi học.
            textBoxSuaMaNgonNgu3.ReadOnly = true;
            textBoxSuaTenNgonNgu3.ReadOnly = false;

        }

        // Xử lý sự kiện click nút xóa, cho phép xóa dữ liệu    - Sự kiện tự tạo thêm bằng tay
        void buttonXoaNgonNgu_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn xóa ngôn ngữ", "Xóa Ngôn Ngữ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                XoaNhanVien_NgonNgu_Conflig(MaNgonNgu);
                XoaNgonNgu();
                HienThiNgonNgu();
            }
            else
            {

            }
        }

        // Xử lý sự kiện click nút thêm, cho phép thêm dữ liệu
        private void buttonThemNgonNgu3_Click(object sender, EventArgs e)
        {
            int i;
            int count = 0;
            for (i = 0; i < dataGridViewNgonNgu.RowCount - 1; i++)
                if (textBoxThemMaNgonNgu3.Text == dataGridViewNgonNgu.Rows[i].Cells[1].Value.ToString())
                {
                    count++;
                }
            if (count == 1)
            {
                MessageBox.Show("Mã ngôn ngữ đã tồn tại", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn thêm ngôn ngữ?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.Yes)
                {
                    ThemNgonNgu();
                    HienThiNgonNgu();
                }
                else
                {
                    Dispose();
                }
            }
        }

        // Xử lý sự kiện click vào mỗi dòng, dữ liệu tự động update vào form sửa
        private void dataGridViewNgonNgu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dataGridViewNgonNgu.RowCount - 1)
            {
                // Set giá trị cho các thành phần 

                textBoxSuaMaNgonNgu3.Text = dataGridViewNgonNgu.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxSuaTenNgonNgu3.Text = dataGridViewNgonNgu.Rows[e.RowIndex].Cells[2].Value.ToString();

                MaNgonNgu = dataGridViewNgonNgu.Rows[e.RowIndex].Cells[1].Value.ToString();

                buttonSuaNgonNgu.Click += buttonSuaNgonNgu_Click;

                // Khóa việc sửa giá trị trước khi click vào nút sửa.
                textBoxSuaMaNgonNgu3.ReadOnly = true;
                textBoxSuaTenNgonNgu3.ReadOnly = true;

                buttonXoaNgonNgu.Click -= buttonXoaNgonNgu_Click;
                buttonXoaNgonNgu.Click += buttonXoaNgonNgu_Click;
            }

        }

        // Xử lý sự kiện click vào nút Lưu lại (buttonSuaNgonNgu3)
        private void buttonSuaNgonNgu3_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Chắc chắn sửa thông tin?", "Sửa Thông Tin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                SuaNgonNgu();

                // Khóa lại chức năng sửa
                textBoxSuaMaNgonNgu3.ReadOnly = true;
                textBoxSuaTenNgonNgu3.ReadOnly = true;

                //Hiển thị lại danh sách
                HienThiNgonNgu();
            }
            else
            {

            }
        }

        // Xử lý sự kiện click nút tìm kiếm
        private void buttonTimNgonNgu3_Click(object sender, EventArgs e)
        {
            TimNgonNgu();
        }

        // Click nút quay lại, hiển thị toàn bộ dữ liệu nơi học
        private void buttonQuayLaiNgonNgu3_Click(object sender, EventArgs e)
        {
            HienThiNgonNgu();
        }

        #endregion

        //-------------------------------------------------------------------------------------------------

        #region Xử Lý Danh Mục Khả Năng Vi Tính
        // Phương thức Hiển thị Khả năng vi tính
        public void HienThiKhaNangViTinh()
        {
            int i;
            string sqlSelect = "select * from KhaNangViTinh";
            SqlCommand cmd = new SqlCommand(sqlSelect, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewKNVT.DataSource = dt;
            dataGridViewKNVT.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (i = 0; i < dataGridViewKNVT.RowCount; i++) dataGridViewKNVT.Rows[i].Cells[0].Value = i + 1;
        }

        // Phương thức thêm Khả năng vi tính
        public void ThemKhaNangViTinh()
        {
            string sqlInsert = "Insert into KhaNangViTinh values(@MaKhaNangViTinh, @TenKhaNangViTinh)";
            SqlCommand cmd = new SqlCommand(sqlInsert, con);
            cmd.Parameters.AddWithValue("MaKhaNangViTinh", textBoxThemMaKNVT4.Text);
            cmd.Parameters.AddWithValue("TenKhaNangViTinh", textBoxThemTenKNVT4.Text);
            cmd.ExecuteNonQuery();
            textBoxThemMaKNVT4.Text = "";
            textBoxThemTenKNVT4.Text = "";
        }

        // Phương thức sửa khả năng vi tính
        public void SuaKhaNangViTinh()
        {
            string sqlUpdate = "Update KhaNangViTinh Set TenKhaNangViTinh=@TenKhaNangViTinh where MaKhaNangViTinh=@MaKhaNangViTinh";
            SqlCommand cmd = new SqlCommand(sqlUpdate, con);
            cmd.Parameters.AddWithValue("MaKhaNangViTinh", textBoxSuaMaKNVT4.Text);
            cmd.Parameters.AddWithValue("TenKhaNangViTinh", textBoxSuaTenKNVT4.Text);

            cmd.ExecuteNonQuery();
        }

        // Phương thức Xóa Khả năng vi tính
        public void XoaKhaNangViTinh()
        {
            string sqlDelete = "Delete From KhaNangViTinh Where MaKhaNangViTinh=@MaKhaNangViTinh";
            SqlCommand cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaKhaNangViTinh", MaKhaNangViTinh);
            cmd.ExecuteNonQuery();
        }

        // Phương thức tìm Khả năng vi tính
        public void TimKhaNangViTinh()
        {
            string sqlFindId = "Select * from KhaNangViTinh Where (CharIndex( @MaKhaNangViTinh , MaKhaNangViTinh, 0 ) >=1 or @MaKhaNangViTinh='') and" +
                                                          "(CharIndex(@TenKhaNangViTinh, TenKhaNangViTinh, 0)>=1 or @TenKhaNangViTinh='')";

            SqlCommand cmd = new SqlCommand(sqlFindId, con);

            cmd.Parameters.AddWithValue("MaKhaNangViTinh", textBoxTimMaKNVT4.Text);
            cmd.Parameters.AddWithValue("TenKhaNangViTinh", textBoxTimTenKNVT4.Text);

            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewKNVT.DataSource = dt;
            dataGridViewKNVT.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (int i = 0; i < dataGridViewKNVT.RowCount; i++) dataGridViewKNVT.Rows[i].Cells[0].Value = i + 1;
        }

        // Xử lý sự kiện click nút sửa, cho phép sửa dữ liệu   - Sự kiện tự tạo thêm bằng tay
        void buttonSuaKhaNangViTinh_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            // Mở khóa cho phép sửa dữ liệu sau khi click vào nút sửa, không cho phép sửa mã khả năng vi tính.
            textBoxSuaMaKNVT4.ReadOnly = true;
            textBoxSuaTenKNVT4.ReadOnly = false;

        }

        // Xử lý sự kiện click nút xóa, cho phép xóa dữ liệu    - Sự kiện tự tạo thêm bằng tay
        void buttonXoaKhaNangViTinh_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn xóa khả năng vi tính", "Xóa Khả Năng Vi Tính", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                XoaNhanVien_KhaNangViTinh_Conflig(MaKhaNangViTinh);
                XoaKhaNangViTinh();
                HienThiKhaNangViTinh();
            }
            else
            {

            }
        }

        // Xử lý sự kiện click nút thêm, cho phép thêm dữ liệu
        private void buttonThemKNVT4_Click(object sender, EventArgs e)
        {
            int i;
            int count = 0;
            for (i = 0; i < dataGridViewKNVT.RowCount - 1; i++)
                if (textBoxThemMaKNVT4.Text == dataGridViewKNVT.Rows[i].Cells[1].Value.ToString())
                {
                    count++;
                }
            if (count == 1)
            {
                MessageBox.Show("Mã khả năng vi tính đã tồn tại", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn thêm khả năng vi tính?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.Yes)
                {
                    ThemKhaNangViTinh();
                    HienThiKhaNangViTinh();
                }
                else
                {
                    Dispose();
                }
            }

        }

        // Xử lý sự kiện click vào mỗi dòng, dữ liệu tự động update vào form sửa
        private void dataGridViewKNVT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dataGridViewNgonNgu.RowCount - 1)
            {
                // Set giá trị cho các thành phần 

                textBoxSuaMaKNVT4.Text = dataGridViewKNVT.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxSuaTenKNVT4.Text = dataGridViewKNVT.Rows[e.RowIndex].Cells[2].Value.ToString();

                MaKhaNangViTinh = dataGridViewKNVT.Rows[e.RowIndex].Cells[1].Value.ToString();

                buttonSuaKNVT.Click += buttonSuaKhaNangViTinh_Click;

                // Khóa việc sửa giá trị trước khi click vào nút sửa.
                textBoxSuaMaKNVT4.ReadOnly = true;
                textBoxSuaTenKNVT4.ReadOnly = true;

                buttonXoaKNVT.Click -= buttonXoaKhaNangViTinh_Click;
                buttonXoaKNVT.Click += buttonXoaKhaNangViTinh_Click;
            }

        }

        // Xử lý sự kiện click vào nút Lưu lại (buttonSuaKNVT4)
        private void buttonSuaKNVT4_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Chắc chắn sửa thông tin?", "Sửa Thông Tin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                SuaKhaNangViTinh();

                // Khóa lại chức năng sửa
                textBoxSuaMaKNVT4.ReadOnly = true;
                textBoxSuaTenKNVT4.ReadOnly = true;

                //Hiển thị lại danh sách
                HienThiKhaNangViTinh();
            }
            else
            {

            }
        }

        // Xử lý sự kiện click nút tìm kiếm
        private void buttonTimKNVT_Click(object sender, EventArgs e)
        {
            TimKhaNangViTinh();
        }

        // Click nút quay lại, hiển thị toàn bộ dữ liệu nơi học
        private void buttonQuayLaiKNVT4_Click(object sender, EventArgs e)
        {
            HienThiKhaNangViTinh();
        }
        #endregion

        //-------------------------------------------------------------------------------------------------

        #region Xử Lý Danh Mục Khóa Huấn Luyện

        // Phương thức Hiển thị Khóa Huấn Luyện
        public void HienThiKhoaHuanLuyen()
        {
            int i;
            string sqlSelect = "select * from KhoaHuanLuyen";
            SqlCommand cmd = new SqlCommand(sqlSelect, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewKHL.DataSource = dt;
            dataGridViewKHL.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (i = 0; i < dataGridViewKHL.RowCount; i++) dataGridViewKHL.Rows[i].Cells[0].Value = i + 1;
        }

        // Phương thức thêm Khóa Huấn Luyện
        public void ThemKhoaHuanLuyen()
        {
            string sqlInsert = "Insert into KhoaHuanLuyen values(@MaKhoaHuanLuyen, @TenKhoaHuanLuyen)";
            SqlCommand cmd = new SqlCommand(sqlInsert, con);
            cmd.Parameters.AddWithValue("MaKhoaHuanLuyen", textBoxThemMaKHL5.Text);
            cmd.Parameters.AddWithValue("TenKhoaHuanLuyen", textBoxThemTenKHL5.Text);
            cmd.ExecuteNonQuery();
            textBoxThemMaKHL5.Text = "";
            textBoxThemTenKHL5.Text = "";
        }

        // Phương thức sửa khóa huấn luyện
        public void SuaKhoaHuanLuyen()
        {
            string sqlUpdate = "Update KhoaHuanLuyen Set TenKhoaHuanLuyen=@TenKhoaHuanLuyen where MaKhoaHuanLuyen=@MaKhoaHuanLuyen";
            SqlCommand cmd = new SqlCommand(sqlUpdate, con);
            cmd.Parameters.AddWithValue("MaKhoaHuanLuyen", textBoxSuaMaKHL5.Text);
            cmd.Parameters.AddWithValue("TenKhoaHuanLuyen", textBoxSuaTenKHL5.Text);

            cmd.ExecuteNonQuery();
        }

        // Phương thức Xóa Khóa huấn luyện
        public void XoaKhoaHuanLuyen()
        {
            string sqlDelete = "Delete From KhoaHuanLuyen Where MaKhoaHuanLuyen=@MaKhoaHuanLuyen";
            SqlCommand cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaKhoaHuanLuyen", MaKhoaHuanLuyen);
            cmd.ExecuteNonQuery();
        }

        // Phương thức tìm Khóa huấn luyện
        public void TimKhoaHuanLuyen()
        {
            string sqlFindId = "Select * from KhoaHuanLuyen Where (CharIndex( @MaKhoaHuanLuyen , MaKhoaHuanLuyen, 0 ) >=1 or @MaKhoaHuanLuyen='') and" +
                                                          "(CharIndex(@TenKhoaHuanLuyen, TenKhoaHuanLuyen, 0)>=1 or @TenKhoaHuanLuyen='')";

            SqlCommand cmd = new SqlCommand(sqlFindId, con);

            cmd.Parameters.AddWithValue("MaKhoaHuanLuyen", textBoxTimMaKNVT4.Text);
            cmd.Parameters.AddWithValue("TenKhoaHuanLuyen", textBoxTimTenKNVT4.Text);

            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewKHL.DataSource = dt;
            dataGridViewKHL.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (int i = 0; i < dataGridViewKHL.RowCount; i++) dataGridViewKHL.Rows[i].Cells[0].Value = i + 1;
        }

        // Xử lý sự kiện click nút sửa, cho phép sửa dữ liệu   - Sự kiện tự tạo thêm bằng tay
        void buttonSuaKhoaHuanLuyen_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            // Mở khóa cho phép sửa dữ liệu sau khi click vào nút sửa, không cho phép sửa mã khóa huấn luyện
            textBoxSuaMaKHL5.ReadOnly = true;
            textBoxSuaTenKHL5.ReadOnly = false;

        }

        // Xử lý sự kiện click nút xóa, cho phép xóa dữ liệu    - Sự kiện tự tạo thêm bằng tay
        void buttonXoaKhoaHuanLuyen_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn xóa khóa huấn luyện", "Xóa Khóa Huấn Luyện", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                XoaNhanVien_KhoaHuanLuyen_Conflig(MaKhoaHuanLuyen);
                XoaKhoaHuanLuyen();
                HienThiKhoaHuanLuyen();
            }
            else
            {

            }
        }

        // Xử lý sự kiện click nút thêm, cho phép thêm dữ liệu
        private void buttonKHL5_Click(object sender, EventArgs e)
        {
            int i;
            int count = 0;
            for (i = 0; i < dataGridViewKHL.RowCount - 1; i++)
                if (textBoxThemMaKHL5.Text == dataGridViewKHL.Rows[i].Cells[1].Value.ToString())
                {
                    count++;
                }
            if (count == 1)
            {
                MessageBox.Show("Mã khả khóa huấn luyện đã tồn tại", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn thêm khóa huấn luyện?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.Yes)
                {
                    ThemKhoaHuanLuyen();
                    HienThiKhoaHuanLuyen();
                }
                else
                {
                    Dispose();
                }
            }

        }

        // Xử lý sự kiện click vào mỗi dòng, dữ liệu tự động update vào form sửa
        private void dataGridViewKHL_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dataGridViewKHL.RowCount - 1)
            {
                // Set giá trị cho các thành phần 

                textBoxSuaMaKHL5.Text = dataGridViewKHL.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxSuaTenKHL5.Text = dataGridViewKHL.Rows[e.RowIndex].Cells[2].Value.ToString();

                MaKhoaHuanLuyen = dataGridViewKHL.Rows[e.RowIndex].Cells[1].Value.ToString();

                buttonSuaKHL.Click += buttonSuaKhoaHuanLuyen_Click;

                // Khóa việc sửa giá trị trước khi click vào nút sửa.
                textBoxSuaMaKHL5.ReadOnly = true;
                textBoxSuaTenKHL5.ReadOnly = true;

                buttonXoaKHL.Click -= buttonXoaKhoaHuanLuyen_Click;
                buttonXoaKHL.Click += buttonXoaKhoaHuanLuyen_Click;
            }

        }

        // Xử lý sự kiện click vào nút Lưu lại (buttonSuaKHL5)
        private void buttonSuaKHL5_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Chắc chắn sửa thông tin?", "Sửa Thông Tin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                SuaKhoaHuanLuyen();

                // Khóa lại chức năng sửa
                textBoxSuaMaKHL5.ReadOnly = true;
                textBoxSuaTenKHL5.ReadOnly = true;

                //Hiển thị lại danh sách
                HienThiKhoaHuanLuyen();
            }
            else
            {

            }
        }

        // Xử lý sự kiện click nút tìm kiếm
        private void buttonTimKHL5_Click(object sender, EventArgs e)
        {
            TimKhoaHuanLuyen();
        }

        // Click nút quay lại, hiển thị toàn bộ dữ liệu khóa huấn luyện
        private void buttonQuayLaiKHL5buttonQuayLaiKHL5_Click(object sender, EventArgs e)
        {
            HienThiKhoaHuanLuyen();
        }
        #endregion

        //-------------------------------------------------------------------------------------------------

        #region Xử Lý Danh Mục Công Ty
        // Phương thức Hiển thị Công Ty
        public void HienThiCongTy()
        {
            int i;
            string sqlSelect = "select * from CongTy";
            SqlCommand cmd = new SqlCommand(sqlSelect, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewCongTy.DataSource = dt;
            dataGridViewCongTy.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (i = 0; i < dataGridViewCongTy.RowCount; i++) dataGridViewCongTy.Rows[i].Cells[0].Value = i + 1;
        }

        // Phương thức thêm Công Ty
        public void ThemCongTy()
        {
            string sqlInsert = "Insert into CongTy values(@MaCongTy, @TenCongTy)";
            SqlCommand cmd = new SqlCommand(sqlInsert, con);
            cmd.Parameters.AddWithValue("MaCongTy", textBoxThemMaCongTy6.Text);
            cmd.Parameters.AddWithValue("TenCongTy", textBoxThemTenCongTy6.Text);
            cmd.ExecuteNonQuery();
            textBoxThemMaCongTy6.Text = "";
            textBoxThemTenCongTy6.Text = "";
        }

        // Phương thức sửa Công Ty
        public void SuaCongTy()
        {
            string sqlUpdate = "Update CongTy Set TenCongTy=@TenCongTy where MaCongTy=@MaCongTy";
            SqlCommand cmd = new SqlCommand(sqlUpdate, con);
            cmd.Parameters.AddWithValue("MaCongTy", textBoxSuaMaCongTy6.Text);
            cmd.Parameters.AddWithValue("TenCongTy", textBoxSuaTenCongTy6.Text);

            cmd.ExecuteNonQuery();
        }

        // Phương thức Xóa Công Ty
        public void XoaCongTy()
        {
            string sqlDelete = "Delete From CongTy Where MaCongTy=@MaCongTy";
            SqlCommand cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaCongTy", MaCongTy);
            cmd.ExecuteNonQuery();
        }

        // Phương thức tìm Công Ty
        public void TimCongTy()
        {
            string sqlFindId = "Select * from CongTy Where (CharIndex( @MaCongTy , MaCongTy, 0 ) >=1 or @MaCongTy='') and" +
                                                          "(CharIndex(@TenCongTy, TenCongTy, 0)>=1 or @TenCongTy='')";

            SqlCommand cmd = new SqlCommand(sqlFindId, con);

            cmd.Parameters.AddWithValue("MaCongTy", textBoxTimMaCongTy6.Text);
            cmd.Parameters.AddWithValue("TenCongTy", textBoxTimTenCongTy6.Text);

            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewCongTy.DataSource = dt;
            dataGridViewCongTy.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (int i = 0; i < dataGridViewCongTy.RowCount; i++) dataGridViewCongTy.Rows[i].Cells[0].Value = i + 1;
        }

        // Xử lý sự kiện click nút sửa, cho phép sửa dữ liệu   - Sự kiện tự tạo thêm bằng tay
        void buttonSuaCongTy_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            // Mở khóa cho phép sửa dữ liệu sau khi click vào nút sửa, không cho phép sửa mã khả năng vi tính.
            textBoxSuaMaCongTy6.ReadOnly = true;
            textBoxSuaTenCongTy6.ReadOnly = false;

        }

        // Xử lý sự kiện click nút xóa, cho phép xóa dữ liệu    - Sự kiện tự tạo thêm bằng tay
        void buttonXoaCongTy_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn xóa công ty", "Xóa Công Ty", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                XoaNhanVien_CongTy_Conflig(MaCongTy);
                XoaCongTy();
                HienThiCongTy();
            }
            else
            {

            }
        }

        // Xử lý sự kiện click nút thêm, cho phép thêm dữ liệu
        private void buttonThemCongTy6_Click(object sender, EventArgs e)
        {
            int i;
            int count = 0;
            for (i = 0; i < dataGridViewCongTy.RowCount - 1; i++)
                if (textBoxThemMaCongTy6.Text == dataGridViewCongTy.Rows[i].Cells[1].Value.ToString())
                {
                    count++;
                }
            if (count == 1)
            {
                MessageBox.Show("Mã công ty đã tồn tại", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn thêm công ty?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.Yes)
                {
                    ThemCongTy();
                    HienThiCongTy();
                }
                else
                {
                    Dispose();
                }
            }

        }

        // Xử lý sự kiện click vào mỗi dòng, dữ liệu tự động update vào form sửa
        private void dataGridViewCongTy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dataGridViewCongTy.RowCount - 1)
            {
                // Set giá trị cho các thành phần 

                textBoxSuaMaCongTy6.Text = dataGridViewCongTy.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxSuaTenCongTy6.Text = dataGridViewCongTy.Rows[e.RowIndex].Cells[2].Value.ToString();

                MaCongTy = dataGridViewCongTy.Rows[e.RowIndex].Cells[1].Value.ToString();

                buttonSuaCongTy.Click += buttonSuaCongTy_Click;

                // Khóa việc sửa giá trị trước khi click vào nút sửa.
                textBoxSuaMaCongTy6.ReadOnly = true;
                textBoxSuaTenCongTy6.ReadOnly = true;


                buttonXoaCongTy.Click -= buttonXoaCongTy_Click;
                buttonXoaCongTy.Click += buttonXoaCongTy_Click;
            }
        }

        // Xử lý sự kiện click vào nút Lưu lại (buttonSuaKHL5)
        private void buttonSuaCongTy6_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Chắc chắn sửa thông tin?", "Sửa Thông Tin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                SuaCongTy();

                // Khóa lại chức năng sửa
                textBoxSuaMaCongTy6.ReadOnly = true;
                textBoxSuaTenCongTy6.ReadOnly = true;

                //Hiển thị lại danh sách
                HienThiCongTy();
            }
            else
            {

            }
        }

        // Xử lý sự kiện click nút tìm kiếm
        private void buttonTimCongTy6_Click(object sender, EventArgs e)
        {
            TimCongTy();
        }

        // Click nút quay lại, hiển thị toàn bộ dữ liệu khóa huấn luyện
        private void buttonQuayLai6_Click(object sender, EventArgs e)
        {
            HienThiCongTy();
        }
        #endregion

        //-------------------------------------------------------------------------------------------------

        #region Xử Lý Danh Mục Phòng Ban
        // Phương thức Hiển thị Công Ty
        public void HienThiPhongBan()
        {
            int i;
            string sqlSelect = "select * from PhongBan";
            SqlCommand cmd = new SqlCommand(sqlSelect, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewPhongBan.DataSource = dt;
            dataGridViewPhongBan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (i = 0; i < dataGridViewPhongBan.RowCount; i++) dataGridViewPhongBan.Rows[i].Cells[0].Value = i + 1;
        }

        // Phương thức thêm PhongBan
        public void ThemPhongBan()
        {
            string sqlInsert = "Insert into PhongBan values(@MaPhongBan, @TenPhongBan)";
            SqlCommand cmd = new SqlCommand(sqlInsert, con);
            cmd.Parameters.AddWithValue("MaPhongBan", textBoxThemMaPhongBan7.Text);
            cmd.Parameters.AddWithValue("TenPhongBan", textBoxThemTenPhongBan7.Text);
            cmd.ExecuteNonQuery();
        }

        // Phương thức sửa PhongBan
        public void SuaPhongBan()
        {
            string sqlUpdate = "Update PhongBan Set TenPhongBan=@TenPhongBan where MaPhongBan=@MaPhongBan";
            SqlCommand cmd = new SqlCommand(sqlUpdate, con);
            cmd.Parameters.AddWithValue("MaPhongBan", textBoxSuaMaPhongBan7.Text);
            cmd.Parameters.AddWithValue("TenPhongBan", textBoxSuaTenPhongBan7.Text);

            cmd.ExecuteNonQuery();
        }

        // Phương thức Xóa PhongBan
        public void XoaPhongBan()
        {
            string sqlDelete = "Delete From PhongBan Where MaPhongBan=@MaPhongBan";
            SqlCommand cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaPhongBan", MaPhongBan);
            cmd.ExecuteNonQuery();
        }

        // Phương thức tìm PhongBan
        public void TimPhongBan()
        {
            string sqlFindId = "Select * from PhongBan Where (CharIndex( @MaPhongBan , MaPhongBan, 0 ) >=1 or @MaPhongBan='') and" +
                                                          "(CharIndex(@TenPhongBan, TenPhongBan, 0)>=1 or @TenPhongBan='')";

            SqlCommand cmd = new SqlCommand(sqlFindId, con);

            cmd.Parameters.AddWithValue("MaPhongBan", textBoxTimMaPhongBan7.Text);
            cmd.Parameters.AddWithValue("TenPhongBan", textBoxTimTenPhongBan7.Text);

            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridViewPhongBan.DataSource = dt;
            dataGridViewPhongBan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (int i = 0; i < dataGridViewPhongBan.RowCount; i++) dataGridViewPhongBan.Rows[i].Cells[0].Value = i + 1;
        }

        // Xử lý sự kiện click nút sửa, cho phép sửa dữ liệu   - Sự kiện tự tạo thêm bằng tay
        void buttonSuaPhongBan_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            // Mở khóa cho phép sửa dữ liệu sau khi click vào nút sửa, không cho phép sửa mã khả năng vi tính.
            textBoxSuaMaPhongBan7.ReadOnly = true;
            textBoxSuaTenPhongBan7.ReadOnly = false;

        }

        // Xử lý sự kiện click nút xóa, cho phép xóa dữ liệu    - Sự kiện tự tạo thêm bằng tay
        void buttonXoaPhongBan_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn xóa phòng ban", "Xóa Phòng Ban", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                SetMaPhongBan(MaPhongBan);
                XoaDuLieuNhanVien_Conflig_PhongBan(MaPhongBan);
                XoaPhongBan();
                HienThiNhanVien();
                HienThiPhongBan();
            }
            else
            {

            }
        }

        // Xử lý sự kiện click nút thêm, cho phép thêm dữ liệu
        private void buttonThemPhongBan7_Click(object sender, EventArgs e)
        {
            int i;
            int count = 0;
            for (i = 0; i < dataGridViewPhongBan.RowCount - 1; i++)
                if (textBoxThemMaPhongBan7.Text == dataGridViewPhongBan.Rows[i].Cells[1].Value.ToString())
                {
                    count++;
                }
            if (count == 1)
            {
                MessageBox.Show("Mã phòng ban đã tồn tại", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dlr = MessageBox.Show("Bạn chắc chắn muốn thêm phòng ban?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.Yes)
                {
                    ThemPhongBan();
                    HienThiPhongBan();
                }
                else
                {
                    Dispose();
                }
            }
        }

        // Xử lý sự kiện click vào mỗi dòng, dữ liệu tự động update vào form sửa
        private void dataGridViewPhongBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dataGridViewPhongBan.RowCount - 1)
            {
                // Set giá trị cho các thành phần 

                textBoxSuaMaPhongBan7.Text = dataGridViewPhongBan.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxSuaTenPhongBan7.Text = dataGridViewPhongBan.Rows[e.RowIndex].Cells[2].Value.ToString();

                MaPhongBan = dataGridViewPhongBan.Rows[e.RowIndex].Cells[1].Value.ToString();

                buttonSuaPhongBan.Click += buttonSuaPhongBan_Click;

                // Khóa việc sửa giá trị trước khi click vào nút sửa.
                textBoxSuaMaPhongBan7.ReadOnly = true;
                textBoxSuaTenPhongBan7.ReadOnly = true;

                buttonXoaPhongBan.Click -= buttonXoaPhongBan_Click;
                buttonXoaPhongBan.Click += buttonXoaPhongBan_Click;
            }
        }

        // Xử lý sự kiện click vào nút Lưu lại (buttonSuaPhongBan7)
        private void buttonSuaPhongBan7_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Chắc chắn sửa thông tin?", "Sửa Thông Tin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                SuaPhongBan();

                // Khóa lại chức năng sửa
                textBoxSuaMaPhongBan7.ReadOnly = true;
                textBoxSuaTenPhongBan7.ReadOnly = true;

                //Hiển thị lại danh sách
                HienThiPhongBan();
            }
            else
            {

            }
        }

        // Xử lý sự kiện click nút tìm kiếm
        private void buttonTimPhongBan7_Click(object sender, EventArgs e)
        {
            TimPhongBan();
        }

        // Click nút quay lại, hiển thị toàn bộ dữ liệu khóa huấn luyện
        private void buttonQuayLaiPhongBan7_Click(object sender, EventArgs e)
        {
            HienThiPhongBan();
        }

        #endregion




        // xử lý các trường hợp xóa danh mục trùng khóa ngoại

        #region Xử lý thay mã nhân viên thành OTHE Nhân Viên của phòng ban khi xóa phòng ban đó
        public void SetMaPhongBan(string MaPhongBan)
        {
            this.MaPhongBan = MaPhongBan;
        }
        public string GetMaPhongBan()
        {
            return MaPhongBan;
        }
        public void XoaDuLieuNhanVien_Conflig_PhongBan(string MaPhongBan)
        {
            string sqlDelete = "Update NhanVien set MaPhongBan='OTHE' Where MaPhongBan=@MaPhongBa" +
                "n";
            SqlCommand cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaPhongBan", MaPhongBan);
            cmd.ExecuteNonQuery();
            this.MaNhanVien = 0;
        }
        #endregion

        #region Xử lý xóa NhanVien_NoiHoc khi bên DanhMuc xóa nơi học
        public void SetMaNoiHoc(string MaNoiHoc)
        {
            this.MaNoiHoc = MaNoiHoc;
        }
        public string GetMaNoiHoc()
        {
            return MaNoiHoc;
        }
        public void XoaNhanVien_NoiHoc_Conflig(string MaNoiHoc)
        {
            string sqlDelete = "Delete From NhanVien_NoiHoc Where MaNoiHoc=@MaNoiHoc";
            SqlCommand cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaNoiHoc", MaNoiHoc);
            cmd.ExecuteNonQuery();
        }
        #endregion

        #region Xử lý xóa NhanVien_NgonNgu khi bên DanhMuc xóa NgonNgu
        public void SetMaNgonNgu(string MaNgonNgu)
        {
            this.MaNgonNgu = MaNgonNgu;
        }
        public string GetMaNgonNgu()
        {
            return MaNgonNgu;
        }
        public void XoaNhanVien_NgonNgu_Conflig(string MaNoiHoc)
        {
            string sqlDelete = "Delete From NhanVien_NgonNgu Where MaNgonNgu=@MaNgonNgu";
            SqlCommand cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaNgonNgu", MaNgonNgu);
            cmd.ExecuteNonQuery();
        }
        #endregion

        #region Xử lý xóa NhanVien_KhaNangViTinh khi bên DanhMuc xóa Khả năng vi tính
        public void SetMaKhaNangViTinh(string MaKhaNangViTinh)
        {
            this.MaKhaNangViTinh = MaKhaNangViTinh;
        }
        public string GetMaKhaNangViTinh()
        {
            return MaKhaNangViTinh;
        }
        public void XoaNhanVien_KhaNangViTinh_Conflig(string MaNoiHoc)
        {
            string sqlDelete = "Delete From NhanVien_KhaNangViTinh Where MaKhaNangViTinh=@MaKhaNangViTinh";
            SqlCommand cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaKhaNangViTinh", MaKhaNangViTinh);
            cmd.ExecuteNonQuery();
        }
        #endregion

        #region Xử lý xóa NhanVien_KhoaHuanLuyen khi bên DanhMuc xóa KhoaHuanLuyen
        public void SetMaKhoaHuanLuyen(string MaKhoaHuanLuyen)
        {
            this.MaKhoaHuanLuyen = MaKhoaHuanLuyen;
        }
        public string GetMaKhoaHuanLuyen()
        {
            return MaKhoaHuanLuyen;
        }
        public void XoaNhanVien_KhoaHuanLuyen_Conflig(string MaNoiHoc)
        {
            string sqlDelete = "Delete From NhanVien_KhoaHuanLuyen Where MaKhoaHuanLuyen=@MaKhoaHuanLuyen";
            SqlCommand cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaKhoaHuanLuyen", MaKhoaHuanLuyen);
            cmd.ExecuteNonQuery();
        }
        #endregion

        #region Xử lý xóa NhanVien_CongTy khi bên DanhMuc xóa công ty
        public void SetMaCongTy(string MaCongTy)
        {
            this.MaCongTy = MaCongTy;
        }
        public string GetMaCongTy()
        {
            return MaNoiHoc;
        }
        public void XoaNhanVien_CongTy_Conflig(string MaNoiHoc)
        {
            string sqlDelete = "Delete From NhanVien_CongTy Where MaCongTy=@MaCongTy";
            SqlCommand cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaCongTy", MaCongTy);
            cmd.ExecuteNonQuery();
        }
        #endregion

        #region Xử lý xóa toàn bộ dữ liệu của nhân viên khi DanhMuc xóa nhân viên đó.
        public void SetMaNhanVien(int MaNhanVien)
        {
            this.MaNhanVien = MaNhanVien;
        }
        public int GetMaNhanVien()
        {
            return MaNhanVien;
        }
        public void XoaDuLieuNhanVien_Conflig(int MaNhanVien)
        {
            string sqlDelete = "Delete From NhanVien_NoiHoc Where MaNhanVien=@MaNhanVien";
            SqlCommand cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaNhanVien", MaNhanVien);
            cmd.ExecuteNonQuery();

            sqlDelete = "Delete From NhanVien_CongTy Where MaNhanVien=@MaNhanVien";
            cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaNhanVien", MaNhanVien);
            cmd.ExecuteNonQuery();

            sqlDelete = "Delete From NhanVien_NgonNgu Where MaNhanVien=@MaNhanVien";
            cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaNhanVien", MaNhanVien);
            cmd.ExecuteNonQuery();

            sqlDelete = "Delete From NhanVien_KhoaHuanLuyen Where MaNhanVien=@MaNhanVien";
            cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaNhanVien", MaNhanVien);
            cmd.ExecuteNonQuery();

            sqlDelete = "Delete From NhanVien_KhaNangViTinh Where MaNhanVien=@MaNhanVien";
            cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaNhanVien", MaNhanVien);
            cmd.ExecuteNonQuery();

            sqlDelete = "Delete From BaoHiemYTe Where MaNhanVien=@MaNhanVien";
            cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaNhanVien", MaNhanVien);
            cmd.ExecuteNonQuery();

            sqlDelete = "Delete From BaoHiemXaHoi Where MaNhanVien=@MaNhanVien";
            cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaNhanVien", MaNhanVien);
            cmd.ExecuteNonQuery();

            sqlDelete = "Delete From NhanVien_Thue Where MaNhanVien=@MaNhanVien";
            cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaNhanVien", MaNhanVien);
            cmd.ExecuteNonQuery();

            sqlDelete = "Delete From NhanVien_NhatKy Where MaNhanVien=@MaNhanVien";
            cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaNhanVien", MaNhanVien);
            cmd.ExecuteNonQuery();

            sqlDelete = "Delete From NhanVien_ChamCong Where MaNhanVien=@MaNhanVien";
            cmd = new SqlCommand(sqlDelete, con);
            cmd.Parameters.AddWithValue("MaNhanVien", MaNhanVien);
            cmd.ExecuteNonQuery();
        }
        #endregion




        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (MaNhanVien != 0)
            {
                ReportForm reportForm = new ReportForm(MaNhanVien, MaPhongBan);
                reportForm.Show();
            }
            else
            {
                MessageBox.Show("Chưa chọn nhân viên");
            }

        }

        public int CheckInt(string s)
        {
            int i;
            for (i = 0; i < s.Length; i++)
                if (((int)s[i]) < 48 || ((int)s[i]) > 57) return 0;
            return 1;
        }
        
    }
}



