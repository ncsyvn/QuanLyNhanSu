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
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void buttonDanhMuc_Click(object sender, EventArgs e)
        {
            DanhMuc danhMuc = new DanhMuc();
            danhMuc.Show();
        }

        private void buttonNghiepVu_Click(object sender, EventArgs e)
        {
            NghiepVu nghiepVu = new NghiepVu();
            nghiepVu.Show();
        }
    }
}
