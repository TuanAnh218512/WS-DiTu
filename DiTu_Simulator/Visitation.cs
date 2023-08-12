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
    public partial class Visitation : Form
    {
        public Visitation()
        {
            InitializeComponent();
        }
        localhost.DiTu ws = new localhost.DiTu();
        private void loadTable()
        {
            DataTable dtb = ws.View("Visitation", "visitation_id");
            dataGV_Vis.DataSource = dtb;
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
                dataGV_Vis.ClearSelection();
            }
            else if (type == "edit")
            {
                txt_id.Enabled = false;
                btn_them.Enabled = false;
                btn_sua.Enabled = true;
                btn_xoa.Enabled = true;
            }

        }
        private void Visitation_Load(object sender, EventArgs e)
        {
            loadTable();
        }
                          
        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (validation("pk"))
            {
                if (ws.Delete((DataTable)dataGV_Vis.DataSource, inputVal()))
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
                if (ws.Edit((DataTable)dataGV_Vis.DataSource, inputVal()))
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
                if (ws.Create((DataTable)dataGV_Vis.DataSource, inputVal()))
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
        private void dataGV_Vis_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TextBox[] input = inputRef();
            int r = dataGV_Vis.CurrentCell.RowIndex;
            for (int i = 0; i < input.Length; i++)
                input[i].Text = dataGV_Vis.Rows[r].Cells[i].Value.ToString();
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
            return new TextBox[] { txt_id, txt_Pri_id, txt_name, txt_relation, textBox2 };
        }
        private DateTimePicker[] inputRe()
        {
            return new DateTimePicker[] { dat_vis };
        }
        private object[] inputVal()
        {
            return new object[] { txt_id.Text, txt_Pri_id.Text, txt_name.Text, txt_relation.Text, textBox2.Text, dat_vis.Text };
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            string keyword = textBox1.Text;
            DataTable dtb = ws.Search("Visitation", "visitation_id", keyword);

            // Hiển thị kết quả trên DataGridView
            dataGV_Vis.DataSource = dtb;
        }
    }
}
