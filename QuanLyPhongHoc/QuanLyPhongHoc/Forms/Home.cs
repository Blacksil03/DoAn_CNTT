using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyPhongHoc
{
    public partial class Home : Form
    {
        private DangNhap dangNhap;
        private string hoVaTen;
        private Form currentFormChinh;

        public Home()
        {
            InitializeComponent();
            dangNhap = null;
            hoVaTen = "";
        }

        private void OpenChinhFrom(Form ChinhFrom)
        {
            if (currentFormChinh != null)
            {
                currentFormChinh.Close();
            }

            currentFormChinh = ChinhFrom;
            ChinhFrom.TopLevel = false;
            ChinhFrom.FormBorderStyle = FormBorderStyle.None;
            ChinhFrom.Dock = DockStyle.Fill;
            panelChinh.Controls.Add(ChinhFrom);
            panelChinh.Tag = ChinhFrom;
            ChinhFrom.BringToFront();
            ChinhFrom.Show();
        }

        public void ChuaDangNhap()
        {
            btnDangNhap.Enabled = true;
            btnDangXuat.Enabled = false;
            btnTaiKhoan.Enabled = false;
            btnGiangVien.Enabled = false;
            btnThoiKhoaBieu.Enabled = false;
            btnSoDoAGU.Enabled = false;
            btnThongTinPhong.Enabled = false;
            btnThietBi.Enabled = false;

            btnCapPhongHoc.Enabled = false;

            txtTrangThai.Text = "Chưa đăng nhập";
        }

        public void Admin()
        {
            btnDangNhap.Enabled = false;
            btnDangXuat.Enabled = true;
            btnTaiKhoan.Enabled = true;
            btnGiangVien.Enabled = false;
            btnThoiKhoaBieu.Enabled = false;
            btnSoDoAGU.Enabled = false;
            btnThongTinPhong.Enabled = false;
            btnThietBi.Enabled = false;
            btnCapPhongHoc.Enabled = false;

            txtTrangThai.Text = "Quản trị viên: " + hoVaTen;
        }

        public void NhanVien()
        {
            btnDangNhap.Enabled = false;
            btnDangXuat.Enabled = true;
            btnTaiKhoan.Enabled = false;
            btnGiangVien.Enabled = true;
            btnThoiKhoaBieu.Enabled = true;
            btnSoDoAGU.Enabled = true;
            btnThongTinPhong.Enabled = true;
            btnThietBi.Enabled = true;

            btnCapPhongHoc.Enabled = true;
            txtTrangThai.Text = "Nhân viên: " + hoVaTen;
        }

        private void btnThoiKhoaBieu_Click(object sender, EventArgs e)
        {
            OpenChinhFrom(new ThoiKhoaBieu());
            txtHienThi.Text = btnThoiKhoaBieu.Text;
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            OpenChinhFrom(new TaiKhoan());
            txtHienThi.Text = btnTaiKhoan.Text;
        }

        private void btnThietBi_Click(object sender, EventArgs e)
        {
            OpenChinhFrom(new ThietBi());
            txtHienThi.Text = btnThietBi.Text;
        }

        private void btnThongTinPhong_Click(object sender, EventArgs e)
        {
            OpenChinhFrom(new ThongTinPhong());
            txtHienThi.Text = btnThongTinPhong.Text;
        }

        private void btnSoDoAGU_Click(object sender, EventArgs e)
        {
            OpenChinhFrom(new PhongAGU());
            txtHienThi.Text = btnSoDoAGU.Text;
        }

        private void btnGiangVien_Click_1(object sender, EventArgs e)
        {
            OpenChinhFrom(new GiangVien());
            txtHienThi.Text = btnGiangVien.Text;
        }

        private void DangNhap()
        {
        LamLai:
            if (dangNhap == null || dangNhap.IsDisposed)
                dangNhap = new DangNhap();
            if (dangNhap.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(dangNhap.txtDangNhap.Text))
                {
                    MessageBox.Show("Tên đăng nhập không được bỏ trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goto LamLai;
                }
                else if (string.IsNullOrWhiteSpace(dangNhap.txtMatKhau.Text))
                {
                    MessageBox.Show("Mật khẩu không được bỏ trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goto LamLai;
                }
                else
                {
                    MyDataTable dataTable = new MyDataTable();
                    dataTable.OpenConnection();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap");
                    cmd.Parameters.Add("@TenDangNhap", SqlDbType.NVarChar, 20).Value = dangNhap.txtDangNhap.Text;
                    dataTable.Fill(cmd);

                    if (dataTable.Rows.Count > 0)
                    {
                        hoVaTen = dataTable.Rows[0]["HoVaTen"].ToString();
                        string quyenHan = dataTable.Rows[0]["QuyenHan"].ToString();

                        this.Hide();

                        if (quyenHan == "admin")
                        {
                            Admin();
                        }
                        else if (quyenHan == "user")
                        {
                            NhanVien();
                        }

                        else
                        {
                            ChuaDangNhap();
                            MessageBox.Show("Mật khẩu không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ChuaDangNhap();
                            goto LamLai;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        goto LamLai;
                    }
                }
            }
        }

        private void Home_Load(object sender, EventArgs e)
        {
            Flash flash = new Flash();
            flash.ShowDialog();
            ChuaDangNhap();
            DangNhap();
        }

        private void btnCapPhongHoc_Click(object sender, EventArgs e)
        {
            OpenChinhFrom(new CapPhongHoc());
            txtHienThi.Text = btnCapPhongHoc.Text;
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            if (currentFormChinh != null)
                currentFormChinh.Close();
            txtHienThi.Text = String.Empty;
            ChuaDangNhap();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            this.Hide();
            DangNhap();
            this.Show();
        }
    }
}
