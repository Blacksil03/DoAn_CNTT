using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyPhongHoc
{
    public partial class ThoiKhoaBieu : Form
    {
        private MyDataTable dataTable = new MyDataTable();
        private String maTKB = "";

        public ThoiKhoaBieu()
        {
            InitializeComponent();
            dataTable.OpenConnection();
        }

        void LayDuLieu()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM TKB_Phong");
            dataTable.Fill(cmd);

            BindingSource binding = new BindingSource();
            binding.DataSource = dataTable;
            dataGridViewTKB.DataSource = binding;

            txtThoiGian.DataBindings.Clear();
            txtTiet.DataBindings.Clear();

            txtTiet.DataBindings.Add("Text", binding, "TietHoc", false, DataSourceUpdateMode.Never);
            txtThoiGian.DataBindings.Add("Text", binding, "ThoiGian", false, DataSourceUpdateMode.Never);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            maTKB = "";
            LayDuLieu();

            // Hiện các control
            txtThoiGian.Enabled = true;
            txtTiet.Enabled = true;

            txtThoiGian.Text = "";
            txtTiet.Text = "";

            dataGridViewTKB.ClearSelection();
            dataGridViewTKB.Enabled = false;
            txtTiet.Focus();
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnChinhSua.Enabled = false;
            btnHuyBo.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            DialogResult kq;
            kq = MessageBox.Show("Bạn có muốn xóa Tiết " + txtTiet.Text + " không?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                string sql = @"DELETE FROM TKB_Phong WHERE TietHoc = @TietHoc";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.Add("@TietHoc", SqlDbType.Int).Value = txtTiet.Text;
                dataTable.Update(cmd);

                // Tải lại form
                ThoiKhoaBieu_Load(sender, e);
            }
        }

        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            maTKB = txtTiet.Text;

            //Hiện các control
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnChinhSua.Enabled = false;
            btnHuyBo.Enabled = true;

            dataGridViewTKB.Enabled = false;
            txtThoiGian.Enabled = true;
            txtTiet.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtTiet.Text.Trim() == "")
                MessageBox.Show("Tiết Học không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtThoiGian.Text.Trim() == "")
                MessageBox.Show("Thời Gian không được rỗng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    // Thêm mới
                    if (maTKB == "")
                    {
                        string sql = @"INSERT INTO TKB_Phong VALUES(@TietHoc,@ThoiGian)";
                        SqlCommand cmd = new SqlCommand(sql);
                        cmd.Parameters.Add("@TietHoc", SqlDbType.NVarChar, 10).Value = txtTiet.Text;
                        cmd.Parameters.Add("@ThoiGian", SqlDbType.NVarChar, 30).Value = txtThoiGian.Text;
                        dataTable.Update(cmd);
                    }
                    else // Sửa
                    {
                        string sql = @"UPDATE TKB_Phong
                                       SET    TietHoc = @TietHocMoi,
                                              ThoiGian = @ThoiGian
                                       WHERE  TietHoc = @TietHoccu";
                        SqlCommand cmd = new SqlCommand(sql);
                        cmd.Parameters.Add("@TietHocMoi", SqlDbType.NVarChar, 10).Value = txtTiet.Text;
                        cmd.Parameters.Add("@TietHoccu", SqlDbType.NVarChar, 10).Value = maTKB;
                        cmd.Parameters.Add("@ThoiGian", SqlDbType.NVarChar, 30).Value = txtThoiGian.Text;
                        dataTable.Update(cmd);
                    }

                    ThoiKhoaBieu_Load(sender, e);
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

        private void ThoiKhoaBieu_Load(object sender, EventArgs e)
        {
            LayDuLieu();
            btnLuu.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnChinhSua.Enabled = true;
            btnHuyBo.Enabled = false;

            dataGridViewTKB.Enabled = true;
            txtThoiGian.Enabled = false;
            txtTiet.Enabled = false;
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            ThoiKhoaBieu_Load(sender, e);
        }

        private void TimDuLieu(string txtFind)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM TKB_Phong WHERE TietHoc = @TietHoc");
            cmd.Parameters.Add("@TietHoc", SqlDbType.NVarChar, 10).Value = txtFind;
            dataTable.Fill(cmd);

            BindingSource binding = new BindingSource();
            binding.DataSource = dataTable;
            dataGridViewTKB.DataSource = binding;

            txtThoiGian.DataBindings.Clear();
            txtTiet.DataBindings.Clear();

            txtTiet.DataBindings.Add("Text", binding, "TietHoc", false, DataSourceUpdateMode.Never);
            txtThoiGian.DataBindings.Add("Text", binding, "ThoiGian", false, DataSourceUpdateMode.Never);
        }

        private void TimKiem_Load(string txtFind)
        {
            TimDuLieu(txtFind);
            btnLuu.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnChinhSua.Enabled = false;
            btnHuyBo.Enabled = false;

            dataGridViewTKB.Enabled = true;
            txtThoiGian.Enabled = false;
            txtTiet.Enabled = false;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtTimKiem.Text.Trim() == "")
                MessageBox.Show("Vui lòng nhập vào ô tìm kiếm", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                TimKiem_Load(txtTimKiem.Text);
                if (dataGridViewTKB.RowCount > 0)
                {
                    btnThem.Enabled = true;
                    btnXoa.Enabled = true;
                    btnChinhSua.Enabled = true;
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ThoiKhoaBieu_Load(sender, e);
        }
    }
}
