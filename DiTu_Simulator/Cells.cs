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
    public partial class Cells : Form
    {
        localhost.DiTu ws = new localhost.DiTu();
        public Cells()
        {
            InitializeComponent();
        }

        private void Cells_Load(object sender, EventArgs e)
        {
            loadTable();
        }
        private void loadTable()
        {
            DataTable dtb = ws.View("Cells", "cell_number");
            dataGV_Cells.DataSource = dtb;
            setButton("clear");
        }
        private void setButton(string type)
        {

            if (type == "clear")
            {
                txt_cell.Enabled = true;
                btn_them.Enabled = true;
                btn_sua.Enabled = false;
                btn_xoa.Enabled = false;
                foreach (TextBox txt in inputRef())
                    txt.Clear();
                dataGV_Cells.ClearSelection();
            }
            else if (type == "edit")
            {
                txt_cell.Enabled = false;
                btn_them.Enabled = false;
                btn_sua.Enabled = true;
                btn_xoa.Enabled = true;
            }
            
        }

        private bool validation(string type = "all")
        {
            object[] err = { "cell_number", "capacity", "current_occupancy" };
            TextBox[] input = inputRef();
            int len = (type == "pk") ? 1 : input.Length;
            for (int i = 0; i < len; i++)
            {
                if (input[i].Text == "")
                {
                    MessageBox.Show("Vui lòng nhập " + err[i]);
                    return false;
                }
            }
            return true;
        }
        private TextBox[] inputRef()
        {
            return new TextBox[] { txt_cell, txt_cap, txt_cur };
        }

        private object[] inputVal()
        {
            return new object[] { txt_cell.Text, txt_cap.Text, txt_cur.Text };
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (validation("pk"))
            {
                if (ws.Delete((DataTable)dataGV_Cells.DataSource, inputVal()))
                {
                    loadTable();
                    MessageBox.Show("Xoá thành công!");
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra!");
                }
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (validation())
            {
                if (ws.Edit((DataTable)dataGV_Cells.DataSource, inputVal()))
                {
                    loadTable();
                    MessageBox.Show("Sửa thành công!");
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra!");
                }
            }
        }

        
        private void btn_them_Click(object sender, EventArgs e)
        {
            if (validation())
            {
                if (ws.Create((DataTable)dataGV_Cells.DataSource, inputVal()))
                {
                    loadTable();
                    MessageBox.Show("Thêm thành công!");
                }
                else
                {
                    MessageBox.Show("Lỗi! Vui lòng kiểm tra lại Username hoặc User Code");
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            setButton("clear");
        }

        private void dataGV_Cells_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TextBox[] input = inputRef();
            int r = dataGV_Cells.CurrentCell.RowIndex;
            for (int i = 0; i < input.Length; i++)
                input[i].Text = dataGV_Cells.Rows[r].Cells[i].Value.ToString();
            setButton("edit");
        }

        private void btn_search_Click(object sender, EventArgs e)
        {

            string keyword = textBox1.Text;
            DataTable dtb = ws.Search("Cells", "cell_number", keyword);

            // Hiển thị kết quả trên DataGridView
            dataGV_Cells.DataSource = dtb;
        }
    }
}
