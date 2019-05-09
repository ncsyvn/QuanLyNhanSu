using System;
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

            HienThiNoiHoc();

            HienThiNgonNgu();

            HienThiKhoaHuanLuyen();

            HienThiKhaNangViTinh();

            HienThiCongTy();
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
            for (i = 0; i < dataGridViewNhanVien.RowCount; i++) dataGridViewNhanVien.Rows[i].Cells[0].Value = i + 1;
        }

        // Phương thức thêm Nhân Viên
        public void ThemNhanVien()
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
            cmd.Parameters.AddWithValue("SoCMTND", textBoxThemSoCMTND1.Text);
            cmd.Parameters.AddWithValue("NoiCapCMTND", textBoxThemNoiCapCMTND1.Text);
            cmd.Parameters.AddWithValue("SoDienThoai", textBoxThemSoDienThoai1.Text);
            cmd.Parameters.AddWithValue("ChucVu", textBoxThemChucVu1.Text);
            cmd.Parameters.AddWithValue("MaPhongBan", comboBoxThemMaPhongBan1.Text);
            cmd.ExecuteNonQuery();
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
            // "(HoVaTen = @HoVaTen or HoVaTen='0'))";// and" +
            //"(NgaySinh=@NgaySinh or NgaySinh=GetDate()) and" +
            //"(GioiTinh=@GioiTinh or GioiTinh=NULL) and" +
            //"(NoiSinh=@NoiSinh or NoiSinh=NULL) and" +
            //"(SoCMTND=@SoCMTND or SoCMTND=NULL) and" +
            //"(NoiCapCMTND=@NoiCapCMTND or NoiCapCMTND=NULL) and" +
            //"(SoDienThoai=@SoDienThoai or SoDienThoai=NULL) and" +
            // "(ChucVu=@ChucVu or ChucVu=NULL))";
            // "(MaPhongBan=@MaPhongBan or MaPhongBan=NULL))";

            SqlCommand cmd = new SqlCommand(sqlFindId, con);

            if (textBoxTimMaNhanVien1.Text == "") cmd.Parameters.AddWithValue("MaNhanVien", 0);
            else cmd.Parameters.AddWithValue("MaNhanVien", Convert.ToInt32(textBoxTimMaNhanVien1.Text));
            cmd.Parameters.AddWithValue("HoVaTen", textBoxTimHoVaTen1.Text);


            /*cmd.Parameters.AddWithValue("NgaySinh", dateTimePickerTimNgaySinh1.Value.Date.Year.ToString() + '/' +
                                                     dateTimePickerTimNgaySinh1.Value.Date.Month.ToString() + '/' +
                                                     dateTimePickerTimNgaySinh1.Value.Date.Day.ToString());
            //cmd.Parameters.AddWithValue("GioiTinh", Convert.ToInt32(comboBoxTimGioiTinh1.Text));
            cmd.Parameters.AddWithValue("NoiSinh", textBoxTimNoiSinh1.Text);
            cmd.Parameters.AddWithValue("SoCMTND", textBoxTimSoCMTND1.Text);
            cmd.Parameters.AddWithValue("NoiCapCMTND", textBoxTimNoiCapCMTND1.Text);
            cmd.Parameters.AddWithValue("SoDienThoai", textBoxTimSoDienThoai1.Text);
            cmd.Parameters.AddWithValue("ChucVu", textBoxTimChucVu1.Text);
            //cmd.Parameters.AddWithValue("MaPhongBan", comboBoxTimMaPhongBan1.Text);*/
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

            buttonXoaNhanVien.Click += buttonXoaNhanVien_Click;


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

        // Xử lý sự kiện click vào mỗi dòng, dữ liệu tự động update vào form sửa
        private void dataGridViewNoiHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Set giá trị cho các thành phần 

            textBoxSuaMaNoiHoc2.Text = dataGridViewNoiHoc.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBoxSuaTenNoiHoc2.Text = dataGridViewNoiHoc.Rows[e.RowIndex].Cells[2].Value.ToString();

            MaNoiHoc = dataGridViewNoiHoc.Rows[e.RowIndex].Cells[1].Value.ToString();

            buttonSuaNoiHoc.Click += buttonSuaNoiHoc_Click;

            // Khóa việc sửa giá trị trước khi click vào nút sửa.
            textBoxSuaMaNoiHoc2.ReadOnly = true;
            textBoxSuaMaNoiHoc2.ReadOnly = true;

            buttonXoaNoiHoc.Click += buttonXoaNoiHoc_Click;

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

        // Xử lý sự kiện click vào mỗi dòng, dữ liệu tự động update vào form sửa
        private void dataGridViewNgonNgu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Set giá trị cho các thành phần 

            textBoxSuaMaNgonNgu3.Text = dataGridViewNgonNgu.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBoxSuaTenNgonNgu3.Text = dataGridViewNgonNgu.Rows[e.RowIndex].Cells[2].Value.ToString();

            MaNgonNgu = dataGridViewNgonNgu.Rows[e.RowIndex].Cells[1].Value.ToString();

            buttonSuaNgonNgu.Click += buttonSuaNgonNgu_Click;

            // Khóa việc sửa giá trị trước khi click vào nút sửa.
            textBoxSuaMaNgonNgu3.ReadOnly = true;
            textBoxSuaTenNgonNgu3.ReadOnly = true;

            buttonXoaNgonNgu.Click += buttonXoaNgonNgu_Click;
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

        // Xử lý sự kiện click vào mỗi dòng, dữ liệu tự động update vào form sửa
        private void dataGridViewKNVT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Set giá trị cho các thành phần 

            textBoxSuaMaKNVT4.Text = dataGridViewKNVT.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBoxSuaTenKNVT4.Text = dataGridViewKNVT.Rows[e.RowIndex].Cells[2].Value.ToString();

            MaKhaNangViTinh = dataGridViewKNVT.Rows[e.RowIndex].Cells[1].Value.ToString();

            buttonSuaKNVT.Click += buttonSuaKhaNangViTinh_Click;

            // Khóa việc sửa giá trị trước khi click vào nút sửa.
            textBoxSuaMaKNVT4.ReadOnly = true;
            textBoxSuaTenKNVT4.ReadOnly = true;

            buttonXoaKNVT.Click += buttonXoaKhaNangViTinh_Click;
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

        // Xử lý sự kiện click vào mỗi dòng, dữ liệu tự động update vào form sửa
        private void dataGridViewKHL_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Set giá trị cho các thành phần 

            textBoxSuaMaKHL5.Text = dataGridViewKHL.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBoxSuaTenKHL5.Text = dataGridViewKHL.Rows[e.RowIndex].Cells[2].Value.ToString();

            MaKhoaHuanLuyen = dataGridViewKHL.Rows[e.RowIndex].Cells[1].Value.ToString();

            buttonSuaKHL.Click += buttonSuaKhoaHuanLuyen_Click;

            // Khóa việc sửa giá trị trước khi click vào nút sửa.
            textBoxSuaMaKHL5.ReadOnly = true;
            textBoxSuaTenKHL5.ReadOnly = true;

            buttonXoaKHL.Click += buttonXoaKhoaHuanLuyen_Click;
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

        // Xử lý sự kiện click vào mỗi dòng, dữ liệu tự động update vào form sửa
        private void dataGridViewCongTy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Set giá trị cho các thành phần 

            textBoxSuaMaCongTy6.Text = dataGridViewCongTy.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBoxSuaTenCongTy6.Text = dataGridViewCongTy.Rows[e.RowIndex].Cells[2].Value.ToString();

            MaCongTy = dataGridViewCongTy.Rows[e.RowIndex].Cells[1].Value.ToString();

            buttonSuaCongTy.Click += buttonSuaCongTy_Click;

            // Khóa việc sửa giá trị trước khi click vào nút sửa.
            textBoxSuaMaCongTy6.ReadOnly = true;
            textBoxSuaTenCongTy6.ReadOnly = true;

            buttonXoaCongTy.Click += buttonXoaCongTy_Click;
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
    }
}


    

