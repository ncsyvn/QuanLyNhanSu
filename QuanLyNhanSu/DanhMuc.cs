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
        private int MaNhanVien;

        private string MaNoiHoc;

        #region Xử Lý Danh Mục Nhân Viên

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
        }

        // Đóng kết nối sau khi tắt form
        private void DanhMuc_FormClosing(object sender, FormClosingEventArgs e)
        {
            con.Close();
        }

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
           for (i = 0; i < dataGridViewNhanVien.RowCount; i++) dataGridViewNhanVien.Rows[i].Cells[0].Value = i+1;
        }

        #endregion

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


        #region Xử lý bảng NoiHoc

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
                textBoxSuaMaNoiHoc2.ReadOnly = true;

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
    }
    #endregion
}

