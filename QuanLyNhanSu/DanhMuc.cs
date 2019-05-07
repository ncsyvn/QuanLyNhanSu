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

        #region Load DatagripView NhanVien Cách 1 - Đẩy dữ liệu thành công
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


        #region DatagridView NhanVien cách 2 - dễ hơn nhưng lỗi


        SqlConnection con;
        private void DanhMuc_Load(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["QuanLyNhanSu"].ConnectionString.ToString();
            con = new SqlConnection(conString);
            con.Open();
            HienThiNhanVien();
        }

        private void DanhMuc_FormClosing(object sender, FormClosingEventArgs e)
        {
            con.Close();
        }

        public void HienThiNhanVien()
       {
           string sqlSelect = "select * from NhanVien";
           SqlCommand cmd = new SqlCommand(sqlSelect, con);
           SqlDataReader dr = cmd.ExecuteReader();
           DataTable dt = new DataTable();
           dt.Load(dr);
           dataGridViewNhanVien.DataSource = dt;
           dataGridViewNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
       }

        #endregion

        
    }
}
