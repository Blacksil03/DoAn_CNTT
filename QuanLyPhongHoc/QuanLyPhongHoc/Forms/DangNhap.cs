using System;
using System.Windows.Forms;

namespace QuanLyPhongHoc
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void txtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDangNhap_Click(sender, e);
            }
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            txtDangNhap.Text = String.Empty;
            txtMatKhau.Text = String.Empty;
            txtDangNhap.Focus();
        }
    }
}
