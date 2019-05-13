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
        public ReportForm()
        {
            InitializeComponent();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'n05_Ql_NhanSu_T5DataSet_NhanVien_NoiHoc.GetNhanVien_NoiHoc' table. You can move, or remove it, as needed.
            this.getNhanVien_NoiHocTableAdapter.Fill(this.n05_Ql_NhanSu_T5DataSet_NhanVien_NoiHoc.GetNhanVien_NoiHoc, 16150151);

            this.reportViewer2.RefreshReport();
        }
    }
}
