using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyPhongHoc
{
    public partial class CapPhongHoc : Form
    {
        private MyDataTable dataTable = new MyDataTable();
        private String maPh = "", maGV = "", tietHoc = "", maTB = "";
        private DateTime ngay = DateTime.Now;

        public CapPhongHoc()
        {
            InitializeComponent();
            dataTable.OpenConnection();
        }

        public void LayDuLieu_1(ComboBox comboBox)
        {
            MyDataTable table = new MyDataTable();
            table.OpenConnection();
            string sql = "SELECT * FROM PhongHoc";
            SqlCommand command = new SqlCommand(sql);
            table.Fill(command);
            comboBox.DataSource = table;
            comboBox.DisplayMember = "TenPhong";
            comboBox.ValueMember = "MaPhong";
        }

        public void LayDuLieu_2(ComboBox comboBox)
        {
            MyDataTable table = new MyDataTable();
            table.OpenConnection();
            string sql = "SELECT * FROM GiangVien";
            SqlCommand command = new SqlCommand(sql);
            table.Fill(command);
            comboBox.DataSource = table;
            comboBox.DisplayMember = "TenGV";
            comboBox.ValueMember = "MaGiangVien";
        }

        public void LayDuLieu_3(ComboBox comboBox)
        {
            MyDataTable table = new MyDataTable();
            table.OpenConnection();
            string sql = "SELECT * FROM ThietBi";
            SqlCommand command = new SqlCommand(sql);
            table.Fill(command);
            comboBox.DataSource = table;
            comboBox.DisplayMember = "TenThietBi";
            comboBox.ValueMember = "MaThietBi";
        }

        public void LayDuLieu_4(ComboBox comboBox)
        {
            MyDataTable table = new MyDataTable();
            table.OpenConnection();
            string sql = "SELECT * FROM TKB_Phong";
            SqlCommand command = new SqlCommand(sql);
            table.Fill(command);
            comboBox.DataSource = table;
            comboBox.DisplayMember = "TietHoc";
            comboBox.ValueMember = "TietHoc";
        }

        void LayDuLieu()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM CapPhongHoc");
            dataTable.Fill(cmd);

            BindingSource binding = new BindingSource();
            binding.DataSource = dataTable;
            dataGridViewCPH.DataSource = binding;

            // Xóa kết nối dữ liệu nếu tôn tại
            cboMaPhong.DataBindings.Clear();
            cboMaGV.DataBindings.Clear();
            cboTietHoc.DataBindings.Clear();
            cboMaTB.DataBindings.Clear();
            txtGhiChu.DataBindings.Clear();
            dtpNgay.DataBindings.Clear();

            // Lấy dữ liệu lên control
            cboMaPhong.DataBindings.Add("SelectedValue", binding, "Maphong", false, DataSourceUpdateMode.Never);
            cboMaGV.DataBindings.Add("SelectedValue", binding, "MaGiangVien", false, DataSourceUpdateMode.Never);
            cboTietHoc.DataBindings.Add("SelectedValue", binding, "TietHoc", false, DataSourceUpdateMode.Never);
            cboMaTB.DataBindings.Add("SelectedValue", binding, "MaThietBi", false, DataSourceUpdateMode.Never);
            txtGhiChu.DataBindings.Add("Text", binding, "GhiChu", false, DataSourceUpdateMode.Never);
            dtpNgay.DataBindings.Add("Value", binding, "Ngay", false, DataSourceUpdateMode.Never);
        }

        private void CapPhongHoc_Load(object sender, EventArgs e)
        {
            LayDuLieu_1(cboMaPhong);
            LayDuLieu_2(cboMaGV);
            LayDuLieu_3(cboMaTB);
            LayDuLieu_4(cboTietHoc);
            LayDuLieu();

            btnLuu.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnChinhSua.Enabled = true;
            btnHuyBo.Enabled = false;
            btnThoat.Enabled = true;

            dataGridViewCPH.Enabled = true;
            cboMaPhong.Enabled = false;
            cboMaGV.Enabled = false;
            cboTietHoc.Enabled = false;
            cboMaTB.Enabled = false;
            dtpNgay.Enabled = false;
            txtGhiChu.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            maPh = maGV = tietHoc = maTB = "";
            ngay = DateTime.Now;
            LayDuLieu();

            // Hiện các control
            cboMaPhong.Enabled = true;
            cboMaGV.Enabled = true;
            cboTietHoc.Enabled = true;
            cboMaTB.Enabled = true;
            dtpNgay.Enabled = true;
            txtGhiChu.Enabled = true;

            // Xóa nội dung của các control
            cboMaPhong.SelectedIndex = 0;
            cboMaGV.SelectedIndex = 0;
            cboTietHoc.SelectedIndex = 0;
            cboMaTB.SelectedIndex = 0;
            txtGhiChu.Text = "";
            dtpNgay.Value = DateTime.Now;

            dataGridViewCPH.ClearSelection();
            dataGridViewCPH.Enabled = false;
            cboMaPhong.Focus();
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnChinhSua.Enabled = false;
            btnHuyBo.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult kq;
            kq = MessageBox.Show("Bạn có muốn xóa Phòng" + cboMaPhong.SelectedText + " không?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                try
                {
                    string sql = @"DELETE FROM CapPhongHoc WHERE MaPhong = @MaPhong";
                    SqlCommand cmd = new SqlCommand(sql);
                    cmd.Parameters.Add("@MaPhong", SqlDbType.NVarChar, 30).Value = cboMaPhong.SelectedText;
                    dataTable.Update(cmd);

                    // Tải lại form
                    CapPhongHoc_Load(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            maPh = cboMaPhong.SelectedText;
            maGV = cboMaGV.SelectedText;
            maTB = cboMaTB.SelectedText;
            tietHoc = cboTietHoc.SelectedText;
            ngay = dtpNgay.Value;

            // Ẩn và hiện một số control
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnChinhSua.Enabled = false;
            btnHuyBo.Enabled = true;

            // Hiện các control
            dataGridViewCPH.Enabled = false;
            cboMaPhong.Enabled = true;
            cboMaGV.Enabled = true;
            cboTietHoc.Enabled = true;
            cboMaTB.Enabled = true;
            dtpNgay.Enabled = true;
            txtGhiChu.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboMaPhong.Text.Trim() == "")
                MessageBox.Show("Phòng không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (cboMaGV.Text.Trim() == "")
                MessageBox.Show("Giảng viên không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (cboMaTB.Text.Trim() == "")
                MessageBox.Show("Thiết bị không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (cboTietHoc.Text.Trim() == "")
                MessageBox.Show("Tiết học không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    // Thêm mới
                    if (maPh == "")
                    {
                        string sql = @"INSERT INTO CapPhongHoc VALUES(@MaPhong, @MaGiangVien, @TietHoc, @Ngay, @GhiChu, @MaThietBi)";
                        SqlCommand cmd = new SqlCommand(sql);
                        cmd.Parameters.Add("@MaPhong", SqlDbType.NVarChar, 30).Value = cboMaPhong.SelectedValue;
                        cmd.Parameters.Add("@MaGiangVien", SqlDbType.NVarChar, 30).Value = cboMaGV.SelectedValue;
                        cmd.Parameters.Add("@TietHoc", SqlDbType.NVarChar, 10).Value = cboTietHoc.SelectedValue;
                        cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = dtpNgay.Value;
                        cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 100).Value = txtGhiChu.Text;
                        cmd.Parameters.Add("@MaThietBi", SqlDbType.NVarChar, 30).Value = cboMaTB.SelectedValue;
                        dataTable.Update(cmd);
                    }
                    else // Sửa
                    {
                        string sql = @"UPDATE CapPhongHoc
                                       SET    MaPhong = @MaPhongMoi,
                                              MaGiangVien = @MaGiangVienMoi,
                                              MaThietBi = @MaThietBiMoi,
                                              TietHoc = @TietHocMoi,
                                              Ngay = @NgayMoi,
                                              GhiChu = @GhiChu
                                       WHERE  MaPhong = @MaPhongCu
                                       AND    MaGiangVien = @MaGiangVienCu
                                       AND    MaThietBi = @MaThietBiCu
                                       AND    TietHoc = @TietHocCu
                                       AND    Ngay = @NgayCu";
                        SqlCommand cmd = new SqlCommand(sql);
                        cmd.Parameters.Add("@MaPhongMoi", SqlDbType.NVarChar, 30).Value = cboMaPhong.SelectedValue;
                        cmd.Parameters.Add("@MaPhongCu", SqlDbType.NVarChar, 30).Value = maPh;
                        cmd.Parameters.Add("@MaGiangVienMoi", SqlDbType.NVarChar, 30).Value = cboMaGV.SelectedValue;
                        cmd.Parameters.Add("@MaGiangVienCu", SqlDbType.NVarChar, 30).Value = maGV;
                        cmd.Parameters.Add("@MaThietBiMoi", SqlDbType.NVarChar, 30).Value = cboMaTB.SelectedValue;
                        cmd.Parameters.Add("@MaThietBiCu", SqlDbType.NVarChar, 30).Value = maTB;
                        cmd.Parameters.Add("@TietHocMoi", SqlDbType.NVarChar, 10).Value = cboTietHoc.SelectedValue;
                        cmd.Parameters.Add("@TietHocCu", SqlDbType.NVarChar, 10).Value = tietHoc;
                        cmd.Parameters.Add("@NgayMoi", SqlDbType.Date).Value = dtpNgay.Value;
                        cmd.Parameters.Add("@NgayCu", SqlDbType.Date).Value = ngay;
                        cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 100).Value = txtGhiChu.Text;
                        dataTable.Update(cmd);
                    }

                    CapPhongHoc_Load(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            CapPhongHoc_Load(sender, e);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
