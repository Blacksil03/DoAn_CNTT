using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace QuanLyPhongHoc
{
    public partial class TaiKhoan : Form
    {
        private MyDataTable dataTable = new MyDataTable();
        private String tenDN = "";

        public TaiKhoan()
        {
            InitializeComponent();
            dataTable.OpenConnection();
        }

        void LayDuLieu()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM TaiKhoan");
            dataTable.Fill(cmd);

            BindingSource binding = new BindingSource();
            binding.DataSource = dataTable;
            dataGridViewTK.DataSource = binding;

            // Xóa kết nối dữ liệu với các control
            txtTenDangNhap.DataBindings.Clear();
            txtMatKhau.DataBindings.Clear();
            txtHoVaTen.DataBindings.Clear();
            cboQuyenHan.DataBindings.Clear();
            txtGhiChu.DataBindings.Clear();

            // Tạo kết nối dữ liệu với các control
            txtTenDangNhap.DataBindings.Add("Text", binding, "TenDangNhap", false, DataSourceUpdateMode.Never);
            txtMatKhau.DataBindings.Add("Text", binding, "MatKhau", false, DataSourceUpdateMode.Never);
            txtHoVaTen.DataBindings.Add("Text", binding, "HoVaTen", false, DataSourceUpdateMode.Never);
            cboQuyenHan.DataBindings.Add("SelectedItem", binding, "QuyenHan", false, DataSourceUpdateMode.Never);
            txtGhiChu.DataBindings.Add("Text", binding, "GhiChu", false, DataSourceUpdateMode.Never);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            tenDN = "";
            LayDuLieu();

            //Hiện các control
            txtHoVaTen.Enabled = true;
            txtGhiChu.Enabled = true;
            txtTenDangNhap.Enabled = true;
            txtMatKhau.Enabled = true;
            cboQuyenHan.Enabled = true;

            txtTenDangNhap.Text = "";
            txtMatKhau.Text = "";
            txtHoVaTen.Text = "";
            cboQuyenHan.Text = "";
            txtGhiChu.Text = "";

            dataGridViewTK.ClearSelection();
            dataGridViewTK.Enabled = false;
            txtTenDangNhap.Focus();
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnChinhSua.Enabled = false;
            btnHuyBo.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult kq;
            kq = MessageBox.Show("Bạn có muốn xóa tài khoản " + txtTenDangNhap.Text + " không?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                try
                {
                    string sql = @"DELETE FROM TaiKhoan WHERE TenDangNhap = @TenDN";
                    SqlCommand cmd = new SqlCommand(sql);
                    cmd.Parameters.Add("@TenDN", SqlDbType.NVarChar, 30).Value = txtTenDangNhap.Text;
                    dataTable.Update(cmd);

                    // Tải lại form
                    TaiKhoan_Load(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            TaiKhoan_Load(sender, e);
        }

        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            tenDN = txtTenDangNhap.Text;

            // Ẩn và hiện một số control
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnChinhSua.Enabled = false;
            btnHuyBo.Enabled = true;

            // Hiện các control
            txtHoVaTen.Enabled = true;
            txtGhiChu.Enabled = true;
            txtMatKhau.Enabled = true;
            txtTenDangNhap.Enabled = true;
            cboQuyenHan.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtTenDangNhap.Text.Trim() == "")
                MessageBox.Show("Tên đăng nhập không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtMatKhau.Text.Trim() == "")
                MessageBox.Show("Mật khẩu không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtHoVaTen.Text.Trim() == "")
                MessageBox.Show("Họ và tên không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (cboQuyenHan.Text.Trim() == "")
                MessageBox.Show("Quyền hạn không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    // Thêm mới
                    if (tenDN == "")
                    {
                        string sql = @"INSERT INTO TaiKhoan VALUES(@TenDangNhap, @MatKhau, @HoVaTen, @QuyenHan, @GhiChu)";
                        SqlCommand cmd = new SqlCommand(sql);
                        cmd.Parameters.Add("@TenDangNhap", SqlDbType.NVarChar, 30).Value = txtTenDangNhap.Text;
                        cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 50).Value = txtMatKhau.Text;
                        cmd.Parameters.Add("@HoVaTen", SqlDbType.NVarChar, 50).Value = txtHoVaTen.Text;
                        cmd.Parameters.Add("@QuyenHan", SqlDbType.NVarChar, 10).Value = cboQuyenHan.Text;
                        cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 100).Value = txtGhiChu.Text;
                        dataTable.Update(cmd);
                    }
                    else // Sửa
                    {
                        string sql = @"UPDATE TaiKhoan
                                       SET    TenDangNhap = @TenDangNhapMoi,
                                              MatKhau = @MatKhau,
                                              HoVaTen = @HoVaTen,
                                              QuyenHan = @QuyenHan,
                                              GhiChu = @GhiChu
                                       WHERE  TenDangNhap = @TenDangNhapCu";
                        SqlCommand cmd = new SqlCommand(sql);
                        cmd.Parameters.Add("@TenDangNhapMoi", SqlDbType.NVarChar, 30).Value = txtTenDangNhap.Text;
                        cmd.Parameters.Add("@TenDangNhapCu", SqlDbType.NVarChar, 30).Value = tenDN;
                        cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 50).Value = txtMatKhau.Text;
                        cmd.Parameters.Add("@HoVaTen", SqlDbType.NVarChar, 50).Value = txtHoVaTen.Text;
                        cmd.Parameters.Add("@QuyenHan", SqlDbType.NVarChar, 10).Value = cboQuyenHan.Text;
                        cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 100).Value = txtGhiChu.Text;
                        dataTable.Update(cmd);
                    }

                    TaiKhoan_Load(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TaiKhoan_Load(object sender, EventArgs e)
        {
            LayDuLieu();
            btnLuu.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnChinhSua.Enabled = true;
            btnHuyBo.Enabled = false;

            dataGridViewTK.Enabled = true;
            txtHoVaTen.Enabled = false;
            txtGhiChu.Enabled = false;
            txtMatKhau.Enabled = false;
            txtTenDangNhap.Enabled = false;
            cboQuyenHan.Enabled = false;
        }

        private void dataGridViewTK_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewTK.Columns[e.ColumnIndex].Name == "MatKhau")
            {
                e.Value = "●●●●●●●●●●●●";
            }
        }
    }
}
