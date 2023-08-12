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
    public partial class Punishment : Form
    {
        localhost.DiTu ws = new localhost.DiTu();
        public Punishment()
        {
            InitializeComponent();
        }
        private void loadTable()
        {
            DataTable dtb = ws.View("Punishment", "punishment_id");
            dataGridView1.DataSource = dtb;
            setButton("clear");
        }
        private void setButton(string type)
        {

            if (type == "clear")
            {
                txt_id.Enabled = true;
                btn_them.Enabled = true;
                btn_sua.Enabled = false;
                btn_xoa.Enabled = false;
                foreach (TextBox txt in inputRef())
                    txt.Clear();
                dataGridView1.ClearSelection();
            }
            else if (type == "edit")
            {
                txt_id.Enabled = false;
                btn_them.Enabled = false;
                btn_sua.Enabled = true;
                btn_xoa.Enabled = true;
            }

        }
        private void btn_search_Click(object sender, EventArgs e)
        {
            string keyword = textBox1.Text;
            DataTable dtb = ws.Search("Punishment", "punishment_id", keyword);

            // Hiển thị kết quả trên DataGridView
            dataGridView1.DataSource = dtb;
        }
        private void Punishment_Load(object sender, EventArgs e)
        {
            loadTable();
        }
        
        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (validation("pk"))
            {
                if (ws.Delete((DataTable)dataGridView1.DataSource, inputVal()))
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
                if (ws.Edit((DataTable)dataGridView1.DataSource, inputVal()))
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
                if (ws.Create((DataTable)dataGridView1.DataSource, inputVal()))
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
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TextBox[] input = inputRef();
            int r = dataGridView1.CurrentCell.RowIndex;
            for (int i = 0; i < input.Length; i++)
                input[i].Text = dataGridView1.Rows[r].Cells[i].Value.ToString();
            setButton("edit");
        }
        private bool validation(string type = "all")
        {
            object[] err = { "punishment_id", "prisoner_id", "punishment_type", "punishment_date", "description"};
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
            return new TextBox[] { txt_id, txt_pri_id, txt_type, textBox2};
        }
        private DateTimePicker[] inputRe()
        {
            return new DateTimePicker[] { dat_pun};
        }
        private object[] inputVal()
        {
            return new object[] { txt_id.Text, txt_pri_id.Text, txt_type.Text, textBox2.Text, dat_pun.Text};
        }

        
    }
}
