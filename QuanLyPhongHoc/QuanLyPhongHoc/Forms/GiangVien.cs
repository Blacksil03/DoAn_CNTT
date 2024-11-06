using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyPhongHoc
{
    public partial class GiangVien : Form
    {
        private MyDataTable dataTable = new MyDataTable();
        private String maGV = "";

        public GiangVien()
        {
            InitializeComponent();
            dataTable.OpenConnection();
        }

        private void LayDuLieu()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM GiangVien");
            dataTable.Fill(cmd);

            BindingSource binding = new BindingSource();
            binding.DataSource = dataTable;
            dataGridViewGiangVien.DataSource = binding;

            // Xóa kết nối dữ liệu nếu tôn tại
            txtTenGV.DataBindings.Clear();
            txtMaGV.DataBindings.Clear();
            txtTenKhoa.DataBindings.Clear();
            txtSDT.DataBindings.Clear();
            txtGhiChu.DataBindings.Clear();
            dptNgaySinh.DataBindings.Clear();

            // Lấy dữ liệu lên control
            txtMaGV.DataBindings.Add("Text", binding, "MaGiangVien", false, DataSourceUpdateMode.Never);
            txtTenGV.DataBindings.Add("Text", binding, "TenGV", false, DataSourceUpdateMode.Never);
            txtTenKhoa.DataBindings.Add("Text", binding, "Khoa", false, DataSourceUpdateMode.Never);
            txtSDT.DataBindings.Add("Text", binding, "SoDienThoai", false, DataSourceUpdateMode.Never);
            dptNgaySinh.DataBindings.Add("Value", binding, "NgaySinh", false, DataSourceUpdateMode.Never);
            txtGhiChu.DataBindings.Add("Text", binding, "GhiChu", false, DataSourceUpdateMode.Never);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            maGV = "";
            LayDuLieu();

            // Hiện các control
            txtTenGV.Enabled = true;
            txtMaGV.Enabled = true;
            txtTenKhoa.Enabled = true;
            txtSDT.Enabled = true;
            txtGhiChu.Enabled = true;
            dptNgaySinh.Enabled = true;

            // Xóa nội dung các control
            txtMaGV.Text = "";
            txtTenGV.Text = "";
            txtTenKhoa.Text = "";
            txtSDT.Text = "";
            txtGhiChu.Text = "";
            dptNgaySinh.Value = DateTime.Now;

            dataGridViewGiangVien.ClearSelection();
            dataGridViewGiangVien.Enabled = false;
            txtMaGV.Focus();
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnXoa.Enabled = true;
            btnChinhSua.Enabled = false;
            btnHuyBo.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult kq;
            kq = MessageBox.Show("Bạn có muốn xóa giáo viên " + txtTenGV.Text + " không?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                try
                {
                    string sql = @"DELETE FROM GiangVien WHERE MaGiangVien = @MaGV";
                    SqlCommand cmd = new SqlCommand(sql);
                    cmd.Parameters.Add("@MaGV", SqlDbType.NVarChar, 30).Value = txtMaGV.Text;
                    dataTable.Update(cmd);

                    // Tải lại form
                    GiangVien_Load(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            maGV = txtMaGV.Text;

            // Ẩn và hiện một số control
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnChinhSua.Enabled = false;
            btnHuyBo.Enabled = true;

            // Hiện các control
            dataGridViewGiangVien.Enabled = false;
            txtTenGV.Enabled = true;
            txtMaGV.Enabled = true;
            txtTenKhoa.Enabled = true;
            txtSDT.Enabled = true;
            txtGhiChu.Enabled = true;
            dptNgaySinh.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaGV.Text.Trim() == "")
                MessageBox.Show("Mã Giảng Viên không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtTenGV.Text.Trim() == "")
                MessageBox.Show("Tên Giảng Viên không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtTenKhoa.Text.Trim() == "")
                MessageBox.Show("Tên Khoa không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtSDT.Text.Trim() == "")
                MessageBox.Show("Số Điện Thoại không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtGhiChu.Text.Trim() == "")
                MessageBox.Show("Địa Chỉ không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (dptNgaySinh.Text.Trim() == "")
                MessageBox.Show("Ngày Sinh không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    // Thêm mới
                    if (maGV == "")
                    {
                        string sql = @"INSERT INTO GiangVien VALUES(@MaGV, @TenGV, @TenKhoa, @NgaySinh, @SoDienThoai, @GhiChu)";
                        SqlCommand cmd = new SqlCommand(sql);
                        cmd.Parameters.Add("@MaGV", SqlDbType.NVarChar, 30).Value = txtMaGV.Text;
                        cmd.Parameters.Add("@TenGV", SqlDbType.NVarChar, 50).Value = txtTenGV.Text;
                        cmd.Parameters.Add("@TenKhoa", SqlDbType.NVarChar, 100).Value = txtTenKhoa.Text;
                        cmd.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = dptNgaySinh.Value;
                        cmd.Parameters.Add("@SoDienThoai", SqlDbType.NChar, 50).Value = txtSDT.Text;
                        cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 100).Value = txtGhiChu.Text;
                        dataTable.Update(cmd);
                    }
                    else // Sửa
                    {
                        string sql = @"UPDATE GiangVien
                                       SET    MaGiangVien = @MaGVMoi,
                                              TenGV = @TenGV,
                                              Khoa = @TenKhoa,
                                              NgaySinh = @NgaySinh,
                                              SoDienThoai = @SoDienThoai,
                                              GhiChu = @GhiChu
                                       WHERE  MaGiangVien = @MaGVCu";
                        SqlCommand cmd = new SqlCommand(sql);
                        cmd.Parameters.Add("@MaGVMoi", SqlDbType.NVarChar, 30).Value = txtMaGV.Text;
                        cmd.Parameters.Add("@MaGVCu", SqlDbType.NVarChar, 30).Value = maGV;
                        cmd.Parameters.Add("@TenGV", SqlDbType.NVarChar, 50).Value = txtTenGV.Text;
                        cmd.Parameters.Add("@TenKhoa", SqlDbType.NVarChar, 100).Value = txtTenKhoa.Text;
                        cmd.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = dptNgaySinh.Value;
                        cmd.Parameters.Add("@SoDienThoai", SqlDbType.NChar, 50).Value = txtSDT.Text;
                        cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 100).Value = txtGhiChu.Text;
                        dataTable.Update(cmd);
                    }

                    GiangVien_Load(sender, e);
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

        private void GiangVien_Load(object sender, EventArgs e)
        {
            LayDuLieu();
            dataGridViewGiangVien.Enabled = true;
            btnLuu.Enabled = false;
            btnHuyBo.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnChinhSua.Enabled = true;
            btnThoat.Enabled = true;

            txtTenGV.Enabled = false;
            txtMaGV.Enabled = false;
            txtTenKhoa.Enabled = false;
            txtSDT.Enabled = false;
            txtGhiChu.Enabled = false;
            dptNgaySinh.Enabled = false;
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            GiangVien_Load(sender, e);
        }

        private void TimDuLieu(string txtFind)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM GiangVien WHERE MaGiangVien = @MaGV OR TenGV = @TenGV");
            cmd.Parameters.Add("@MaGV", SqlDbType.NVarChar, 30).Value = txtFind;
            cmd.Parameters.Add("@TenGV", SqlDbType.NVarChar, 50).Value = txtFind;
            dataTable.Fill(cmd);

            BindingSource binding = new BindingSource();
            binding.DataSource = dataTable;
            dataGridViewGiangVien.DataSource = binding;

            // Xóa kết nối dữ liệu nếu tôn tại
            txtTenGV.DataBindings.Clear();
            txtMaGV.DataBindings.Clear();
            txtTenKhoa.DataBindings.Clear();
            txtSDT.DataBindings.Clear();
            txtGhiChu.DataBindings.Clear();
            dptNgaySinh.DataBindings.Clear();

            // Lấy dữ liệu lên control
            txtMaGV.DataBindings.Add("Text", binding, "MaGiangVien", false, DataSourceUpdateMode.Never);
            txtTenGV.DataBindings.Add("Text", binding, "TenGV", false, DataSourceUpdateMode.Never);
            txtTenKhoa.DataBindings.Add("Text", binding, "Khoa", false, DataSourceUpdateMode.Never);
            txtSDT.DataBindings.Add("Text", binding, "SoDienThoai", false, DataSourceUpdateMode.Never);
            dptNgaySinh.DataBindings.Add("Value", binding, "NgaySinh", false, DataSourceUpdateMode.Never);
            txtGhiChu.DataBindings.Add("Text", binding, "GhiChu", false, DataSourceUpdateMode.Never);
        }

        private void TimKiem_Load(string txtFind)
        {
            TimDuLieu(txtFind);
            dataGridViewGiangVien.Enabled = true;
            btnLuu.Enabled = false;
            btnHuyBo.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnChinhSua.Enabled = false;
            btnThoat.Enabled = true;

            txtTenGV.Enabled = false;
            txtMaGV.Enabled = false;
            txtTenKhoa.Enabled = false;
            txtSDT.Enabled = false;
            txtGhiChu.Enabled = false;
            dptNgaySinh.Enabled = false;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtTimKiem.Text.Trim() == "")
                MessageBox.Show("Vui lòng nhập vào ô tìm kiếm", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                TimKiem_Load(txtTimKiem.Text);
                if (dataGridViewGiangVien.RowCount > 0)
                {
                    btnThem.Enabled = true;
                    btnXoa.Enabled = true;
                    btnChinhSua.Enabled = true;
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            GiangVien_Load(sender, e);
        }
    }
}