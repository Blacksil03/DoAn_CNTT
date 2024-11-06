using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyPhongHoc
{
    public partial class ThongTinPhong : Form
    {
        private MyDataTable dataTable = new MyDataTable();
        private String maTTPhong = "";

        public ThongTinPhong()
        {
            InitializeComponent();
            dataTable.OpenConnection();
        }

        void LayDuLieu()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM PhongHoc");
            dataTable.Fill(cmd);

            BindingSource binding = new BindingSource();
            binding.DataSource = dataTable;
            dataGridViewTTP.DataSource = binding;

            //lấy dữ liệu lên control
            txtMaPhong.DataBindings.Clear();
            txtTenPhong.DataBindings.Clear();
            txtTang.DataBindings.Clear();
            numSucChua.DataBindings.Clear();
            txtGhiChu.DataBindings.Clear();

            txtMaPhong.DataBindings.Add("Text", binding, "MaPhong", false, DataSourceUpdateMode.Never);
            txtTenPhong.DataBindings.Add("Text", binding, "TenPhong", false, DataSourceUpdateMode.Never);
            txtTang.DataBindings.Add("Text", binding, "Tang", false, DataSourceUpdateMode.Never);
            numSucChua.DataBindings.Add("Value", binding, "SucChua", false, DataSourceUpdateMode.Never);
            txtGhiChu.DataBindings.Add("Text", binding, "GhiChu", false, DataSourceUpdateMode.Never);
        }

        private void ThongTinPhong_Load(object sender, EventArgs e)
        {
            LayDuLieu();
            btnLuu.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnChinhSua.Enabled = true;
            btnHuyBo.Enabled = false;

            txtMaPhong.Enabled = false;
            txtTang.Enabled = false;
            numSucChua.Enabled = false;
            txtTenPhong.Enabled = false;
            txtGhiChu.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            maTTPhong = "";
            LayDuLieu();

            //Hiện các control
            txtMaPhong.Enabled = true;
            txtTang.Enabled = true;
            txtTenPhong.Enabled = true;
            numSucChua.Enabled = true;
            txtGhiChu.Enabled = true;

            txtMaPhong.Text = "";
            txtTang.Text = "";
            txtTenPhong.Text = "";
            numSucChua.Value = 0;
            txtGhiChu.Text = "";

            dataGridViewTTP.ClearSelection();
            dataGridViewTTP.Enabled = false;
            txtMaPhong.Focus();
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnChinhSua.Enabled = false;
            btnHuyBo.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult kq;
            kq = MessageBox.Show("Bạn có muốn xóa Mã Phòng " + txtMaPhong.Text + " không?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                try
                {
                    string sql = @"DELETE FROM PhongHoc WHERE MaPhong = @MaPhong";
                    SqlCommand cmd = new SqlCommand(sql);
                    cmd.Parameters.Add("@MaPhong", SqlDbType.NVarChar, 30).Value = txtMaPhong.Text;
                    dataTable.Update(cmd);

                    // Tải lại form
                    ThongTinPhong_Load(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            maTTPhong = txtMaPhong.Text;

            //Hiện các control
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnChinhSua.Enabled = false;
            btnHuyBo.Enabled = true;

            dataGridViewTTP.Enabled = false;
            txtMaPhong.Enabled = true;
            txtTang.Enabled = true;
            numSucChua.Enabled = true;
            txtTenPhong.Enabled = true;
            txtGhiChu.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaPhong.Text.Trim() == "")
                MessageBox.Show("Mã Phòng không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtTang.Text.Trim() == "")
                MessageBox.Show("Tầng không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtTenPhong.Text.Trim() == "")
                MessageBox.Show("Tên Phòng không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    // Thêm mới
                    if (maTTPhong == "")
                    {
                        string sql = @"INSERT INTO PhongHoc VALUES(@MaPhong, @TenPhong, @Tang, @SucChua, @GhiChu)";
                        SqlCommand cmd = new SqlCommand(sql);
                        cmd.Parameters.Add("@MaPhong", SqlDbType.NVarChar, 30).Value = txtMaPhong.Text;
                        cmd.Parameters.Add("@TenPhong", SqlDbType.NVarChar, 50).Value = txtTenPhong.Text;
                        cmd.Parameters.Add("@Tang", SqlDbType.NVarChar, 30).Value = txtTang.Text;
                        cmd.Parameters.Add("@SucChua", SqlDbType.NVarChar, 30).Value = numSucChua.Value;
                        cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 100).Value = txtGhiChu.Text;
                        dataTable.Update(cmd);
                    }
                    else // Sửa
                    {
                        string sql = @"UPDATE PhongHoc
                                       SET    MaPhong = @MaPhongMoi,
                                              TenPhong = @TenPhong,
                                              Tang = @Tang,
                                              SucChua = @SucChua,
                                              GhiChu = @GhiChu
                                       WHERE  MaPhong = @MaPhongCu";
                        SqlCommand cmd = new SqlCommand(sql);
                        cmd.Parameters.Add("@MaPhongMoi", SqlDbType.NVarChar, 30).Value = txtMaPhong.Text;
                        cmd.Parameters.Add("@MaPhongCu", SqlDbType.NVarChar, 30).Value = maTTPhong;
                        cmd.Parameters.Add("@TenPhong", SqlDbType.NVarChar, 50).Value = txtTenPhong.Text;
                        cmd.Parameters.Add("@Tang", SqlDbType.NVarChar, 30).Value = txtTang.Text;
                        cmd.Parameters.Add("@SucChua", SqlDbType.NVarChar, 30).Value = numSucChua.Value;
                        cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 100).Value = txtGhiChu.Text;
                        dataTable.Update(cmd);
                    }

                    ThongTinPhong_Load(sender, e);
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

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            ThongTinPhong_Load(sender, e);
        }
    }
}
