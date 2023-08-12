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
    public partial class SearchHistory : Form
    {
        
        public SearchHistory()
        {
            InitializeComponent();
        }
        localhost.DiTu ws = new localhost.DiTu();
        private void loadTable()
        {
            DataTable dtb = ws.View("SearchHistory", "search_id");
            dataGV_Sea.DataSource = dtb;
            setButton("clear");
        }
        private void setButton(string type)
        {

            if (type == "clear")
            {
                txt_Sea_id.Enabled = true;
                btn_them.Enabled = true;
                btn_sua.Enabled = false;
                btn_xoa.Enabled = false;
                foreach (TextBox txt in inputRef())
                    txt.Clear();
                dataGV_Sea.ClearSelection();
            }
            else if (type == "edit")
            {
                txt_Sea_id.Enabled = false;
                btn_them.Enabled = false;
                btn_sua.Enabled = true;
                btn_xoa.Enabled = true;
            }

        }
        private void SearchHistory_Load(object sender, EventArgs e)
        {
            loadTable();
        }
        private void btn_search_Click(object sender, EventArgs e)
        {
            string keyword = textBox1.Text;
            DataTable dtb = ws.Search("SearchHistory", "search_id", keyword);

            // Hiển thị kết quả trên DataGridView
            dataGV_Sea.DataSource = dtb;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (validation("pk"))
            {
                if (ws.Delete((DataTable)dataGV_Sea.DataSource, inputVal()))
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
                if (ws.Edit((DataTable)dataGV_Sea.DataSource, inputVal()))
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

        private void btn_Dong_Click(object sender, EventArgs e)
        {

        }
        private void btn_them_Click(object sender, EventArgs e)
        {
            if (validation())
            {
                if (ws.Create((DataTable)dataGV_Sea.DataSource, inputVal()))
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
        private void dataGV_Sea_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TextBox[] input = inputRef();
            int r = dataGV_Sea.CurrentCell.RowIndex;
            for (int i = 0; i < input.Length; i++)
                input[i].Text = dataGV_Sea.Rows[r].Cells[i].Value.ToString();
            setButton("edit");
        }
        private bool validation(string type = "all")
        {
            object[] err = { "search_id", "search_date", "staff_id", "cell_number", "result" };
            TextBox[] input = inputRef();
            DateTimePicker[] inpu = inputRe();
            int len = (type == "pk") ? 1 : input.Length + inpu.Length;

            for (int i = 0; i < len; i++)
            {
                if (i < input.Length)
                {
                    if (input[i].Text == "")
                    {
                        MessageBox.Show("Vui lòng nhập " + err[i]);
                        return false;
                    }
                }
                else
                {
                    int index = i - input.Length;
                    if (inpu[index].Value == null)
                    {
                        MessageBox.Show("Vui lòng chọn " + err[i]);
                        return false;
                    }
                }
            }
            return true;
        }
        private TextBox[] inputRef()
        {
            return new TextBox[] { txt_Sea_id, txt_Sta_id, txt_cell, txt_res };
        }
        private DateTimePicker[] inputRe()
        {
            return new DateTimePicker[] { dat_sea };
        }
        private object[] inputVal()
        {
            return new object[] { txt_Sea_id.Text, txt_Sta_id.Text, txt_cell.Text, txt_res.Text };
        }

    }
}
