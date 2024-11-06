using System;
using System.Windows.Forms;

namespace QuanLyPhongHoc
{
    public partial class Flash : Form
    {
        public Flash()
        {
            InitializeComponent();
        }

        private void Flash_Load(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            this.Close();
        }
    }
}
