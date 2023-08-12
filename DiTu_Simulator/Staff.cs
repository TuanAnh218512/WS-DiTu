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
    public partial class Staff : Form
    {
        public Staff()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txt_pos_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void btn_Dong_Click(object sender, EventArgs e)
        {

        }
        localhost.DiTu ws = new localhost.DiTu();
        private void loadTable()
        {
            DataTable dtb = ws.View("Staff", "staff_id");
            dataGV_sta.DataSource = dtb;
            setButton("clear");
        }
        private void setButton(string type)
        {

            if (type == "clear")
            {
                txt_Sta_id.Enabled = true;
                btn_them.Enabled = true;
                btn_sua.Enabled = false;
                btn_xoa.Enabled = false;
                foreach (TextBox txt in inputRef())
                    txt.Clear();
                dataGV_sta.ClearSelection();
            }
            else if (type == "edit")
            {
                txt_Sta_id.Enabled = false;
                btn_them.Enabled = false;
                btn_sua.Enabled = true;
                btn_xoa.Enabled = true;
            }

        }
        private void Staff_Load(object sender, EventArgs e)
        {
            loadTable();
        }
        private void btn_search_Click(object sender, EventArgs e)
        {
            string keyword = textBox1.Text;
            DataTable dtb = ws.Search("Staff", "staff_id", keyword);

            // Hiển thị kết quả trên DataGridView
            dataGV_sta.DataSource = dtb;
        }
        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (validation("pk"))
            {
                if (ws.Delete((DataTable)dataGV_sta.DataSource, inputVal()))
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
                if (ws.Edit((DataTable)dataGV_sta.DataSource, inputVal()))
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
                if (ws.Create((DataTable)dataGV_sta.DataSource, inputVal()))
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
        private void dataGV_sta_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TextBox[] input = inputRef();
            int r = dataGV_sta.CurrentCell.RowIndex;
            for (int i = 0; i < input.Length; i++)
                input[i].Text = dataGV_sta.Rows[r].Cells[i].Value.ToString();
            setButton("edit");
        }
        private bool validation(string type = "all")
        {
            object[] err = { "staff_id", "first_name", "last_name", "position", "department", "hire_date", "salary", "contact_number" };
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
            return new TextBox[] { txt_Sta_id, txt_pos, txt_fir, txt_las, txt_dep, txt_bacs, txt_contast };
        }
        private DateTimePicker[] inputRe()
        {
            return new DateTimePicker[] { dat_bir };
        }
        private object[] inputVal()
        {
            return new object[] { txt_Sta_id.Text, txt_pos.Text, txt_fir.Text, txt_las.Text, txt_bacs.Text, txt_contast.Text, dat_bir.Text };
        }

        
    }
}
