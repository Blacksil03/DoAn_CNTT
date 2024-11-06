using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyPhongHoc
{
    public partial class ThietBi : Form
    {
        private MyDataTable dataTable = new MyDataTable();
        private String maThietBi = "";

        public ThietBi()
        {
            InitializeComponent();
            dataTable.OpenConnection();
        }

        void LayDuLieu()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM ThietBi");
            dataTable.Fill(cmd);

            BindingSource binding = new BindingSource();
            binding.DataSource = dataTable;
            dataGridViewThietBi.DataSource = binding;

            //lấy dữ liệu lên control
            txtMaThietBi.DataBindings.Clear();
            txtTenTB.DataBindings.Clear();
            txtTinhTrang.DataBindings.Clear();
            txtTrangThai.DataBindings.Clear();
            dtpNgayMuon.DataBindings.Clear();
            dtpNgayTra.DataBindings.Clear();

            txtMaThietBi.DataBindings.Add("Text", binding, "MaThietBi", false, DataSourceUpdateMode.Never);
            txtTenTB.DataBindings.Add("Text", binding, "TenThietBi", false, DataSourceUpdateMode.Never);
            txtTinhTrang.DataBindings.Add("Text", binding, "TinhTrang", false, DataSourceUpdateMode.Never);
            txtTrangThai.DataBindings.Add("Text", binding, "TrangThai", false, DataSourceUpdateMode.Never);
            dtpNgayMuon.DataBindings.Add("Value", binding, "NgayMuon", false, DataSourceUpdateMode.Never);
            dtpNgayTra.DataBindings.Add("Value", binding, "NgayTra", false, DataSourceUpdateMode.Never);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            maThietBi = "";
            LayDuLieu();

            //Hiện các control
            txtMaThietBi.Enabled = true;
            txtTenTB.Enabled = true;
            txtTinhTrang.Enabled = true;
            txtTrangThai.Enabled = true;
            dtpNgayMuon.Enabled = true;
            dtpNgayTra.Enabled = true;

            txtMaThietBi.Text = "";
            txtTenTB.Text = "";
            txtTinhTrang.Text = "";
            txtTrangThai.Text = "";
            dtpNgayMuon.Value = DateTime.Now;
            dtpNgayTra.Value = DateTime.Now;

            dataGridViewThietBi.ClearSelection();
            dataGridViewThietBi.Enabled = false;
            txtMaThietBi.Focus();
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnXoa.Enabled = true;
            btnChinhSua.Enabled = true;
            btnHuyBo.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult kq;
            kq = MessageBox.Show("Bạn có muốn xóa Thiết Bị " + txtMaThietBi.Text + " không?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                try
                {
                    string sql = @"DELETE FROM ThietBi WHERE MaThietBi = @MaThietBi";
                    SqlCommand cmd = new SqlCommand(sql);
                    cmd.Parameters.Add("@MaThietBi", SqlDbType.NVarChar, 30).Value = txtMaThietBi.Text;
                    dataTable.Update(cmd);

                    // Tải lại form
                    ThietBi_Load(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaThietBi.Text.Trim() == "")
                MessageBox.Show("Mã Thiết Bị không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtTenTB.Text.Trim() == "")
                MessageBox.Show("Tên Thiết Bị không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtTinhTrang.Text.Trim() == "")
                MessageBox.Show("Tình Trạng không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtTrangThai.Text.Trim() == "")
                MessageBox.Show("Trạng Thái không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (dtpNgayMuon.Text.Trim() == "")
                MessageBox.Show("Ngày Mượn không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (dtpNgayTra.Text.Trim() == "")
                MessageBox.Show("Ngày Trả không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    // Thêm mới
                    if (maThietBi == "")
                    {
                        string sql = @"INSERT INTO ThietBi VALUES(@MaThietBi, @TenThietBi, @TinhTrang, @TrangThai, @NgayMuon, @NgayTra)";
                        SqlCommand cmd = new SqlCommand(sql);
                        cmd.Parameters.Add("@MaThietBi", SqlDbType.NVarChar, 30).Value = txtMaThietBi.Text;
                        cmd.Parameters.Add("@TenThietBi", SqlDbType.NVarChar, 50).Value = txtTenTB.Text;
                        cmd.Parameters.Add("@TinhTrang", SqlDbType.NVarChar, 30).Value = txtTinhTrang.Text;
                        cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 30).Value = txtTrangThai.Text;
                        cmd.Parameters.Add("@NgayMuon", SqlDbType.Date).Value = dtpNgayMuon.Value;
                        cmd.Parameters.Add("@NgayTra", SqlDbType.Date).Value = dtpNgayTra.Value;
                        dataTable.Update(cmd);
                    }
                    else // Sửa
                    {
                        string sql = @"UPDATE ThietBi
                                       SET    MaThietBi = @MaThietBiMoi,
                                              TenThietBi = @TenThietBi,
                                              TinhTrang = @TinhTrang,
                                              TrangThai = @TrangThai,
                                              NgayMuon = @NgayMuon,
                                              NgayTra = @NgayTra
                                       WHERE  MaThietBi = @MaThietBiCu";
                        SqlCommand cmd = new SqlCommand(sql);
                        cmd.Parameters.Add("@MaThietBiMoi", SqlDbType.NVarChar, 30).Value = txtMaThietBi.Text;
                        cmd.Parameters.Add("@MaThietBiCu", SqlDbType.NVarChar, 30).Value = maThietBi;
                        cmd.Parameters.Add("@TenThietBi", SqlDbType.NVarChar, 50).Value = txtTenTB.Text;
                        cmd.Parameters.Add("@TinhTrang", SqlDbType.NVarChar, 30).Value = txtTinhTrang.Text;
                        cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 30).Value = txtTrangThai.Text;
                        cmd.Parameters.Add("@NgayMuon", SqlDbType.Date).Value = dtpNgayMuon.Value;
                        cmd.Parameters.Add("@NgayTra", SqlDbType.Date).Value = dtpNgayTra.Value;
                        dataTable.Update(cmd);
                    }

                    ThietBi_Load(sender, e);
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

        private void ThietBi_Load(object sender, EventArgs e)
        {
            LayDuLieu();
            dataGridViewThietBi.Enabled = true;
            btnLuu.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnChinhSua.Enabled = true;
            btnThoat.Enabled = true;
            btnHuyBo.Enabled = false;

            txtMaThietBi.Enabled = false;
            txtTenTB.Enabled = false;
            txtTinhTrang.Enabled = false;
            txtTrangThai.Enabled = false;
            dtpNgayMuon.Enabled = false;
            dtpNgayTra.Enabled = false;
        }

        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            maThietBi = txtMaThietBi.Text;

            //Hiện các control
            btnLuu.Enabled = true;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnChinhSua.Enabled = false;
            btnHuyBo.Enabled = true;

            dataGridViewThietBi.Enabled = false;
            txtMaThietBi.Enabled = true;
            txtTenTB.Enabled = true;
            txtTinhTrang.Enabled = true;
            txtTrangThai.Enabled = true;
            dtpNgayMuon.Enabled = true;
            dtpNgayTra.Enabled = true;
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            ThietBi_Load(sender, e);
        }

        private void TimDuLieu(string txtFind)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM ThietBi WHERE MaThietBi = @MaTB OR TenThietBi = @TenTB");
            cmd.Parameters.Add("@MaTB", SqlDbType.NVarChar, 30).Value = txtFind;
            cmd.Parameters.Add("@TenTB", SqlDbType.NVarChar, 50).Value = txtFind;
            dataTable.Fill(cmd);

            BindingSource binding = new BindingSource();
            binding.DataSource = dataTable;
            dataGridViewThietBi.DataSource = binding;

            //lấy dữ liệu lên control
            txtMaThietBi.DataBindings.Clear();
            txtTenTB.DataBindings.Clear();
            txtTinhTrang.DataBindings.Clear();
            txtTrangThai.DataBindings.Clear();
            dtpNgayMuon.DataBindings.Clear();
            dtpNgayTra.DataBindings.Clear();

            txtMaThietBi.DataBindings.Add("Text", binding, "MaThietBi", false, DataSourceUpdateMode.Never);
            txtTenTB.DataBindings.Add("Text", binding, "TenThietBi", false, DataSourceUpdateMode.Never);
            txtTinhTrang.DataBindings.Add("Text", binding, "TinhTrang", false, DataSourceUpdateMode.Never);
            txtTrangThai.DataBindings.Add("Text", binding, "TrangThai", false, DataSourceUpdateMode.Never);
            dtpNgayMuon.DataBindings.Add("Value", binding, "NgayMuon", false, DataSourceUpdateMode.Never);
            dtpNgayTra.DataBindings.Add("Value", binding, "NgayTra", false, DataSourceUpdateMode.Never);
        }

        private void TimKiem_Load(string txtFind)
        {
            TimDuLieu(txtFind);
            dataGridViewThietBi.Enabled = true;
            btnLuu.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnChinhSua.Enabled = false;
            btnThoat.Enabled = true;
            btnHuyBo.Enabled = false;

            txtMaThietBi.Enabled = false;
            txtTenTB.Enabled = false;
            txtTinhTrang.Enabled = false;
            txtTrangThai.Enabled = false;
            dtpNgayMuon.Enabled = false;
            dtpNgayTra.Enabled = false;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtTimKiem.Text.Trim() == "")
                MessageBox.Show("Vui lòng nhập vào ô tìm kiếm", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                TimKiem_Load(txtTimKiem.Text);
                if (dataGridViewThietBi.RowCount > 0)
                {
                    btnThem.Enabled = true;
                    btnXoa.Enabled = true;
                    btnChinhSua.Enabled = true;
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ThietBi_Load(sender, e);
        }
    }
}

