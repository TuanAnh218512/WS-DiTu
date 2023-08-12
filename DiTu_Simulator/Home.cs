using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiTu_Simulator
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_pri_Click(object sender, EventArgs e)
        {
            new Prisoners().Show();
        }

        private void btn_cel_Click(object sender, EventArgs e)
        {
            new Cells().Show();
        }

        private void btn_SeHi_Click(object sender, EventArgs e)
        {
            new SearchHistory().Show();
        }

        private void btn_vis_Click(object sender, EventArgs e)
        {
            new Visitation().Show();
        }

        private void btn_pun_Click(object sender, EventArgs e)
        {
            new Punishment().Show();
        }

        private void btn_sta_Click(object sender, EventArgs e)
        {
            new Staff().Show();
        }
    }
}
