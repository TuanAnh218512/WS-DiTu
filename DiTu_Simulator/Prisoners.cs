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
    public partial class Prisoners : Form
    {
        localhost.DiTu ws = new localhost.DiTu();
        public Prisoners()
        {
            InitializeComponent();
        }
        private void Prisoners_Load(object sender, EventArgs e)
        {
            loadTable();
        }
        private void loadTable()
        {
            DataTable dtb = ws.View("Prisoners", "prisoner_id");
            dataGV_Pri.DataSource = dtb;
            setButton("clear");
        }
        private void setButton(string type)
        {

            if (type == "clear")
            {
                txt_Pri_id.Enabled = true;
                btn_them.Enabled = true;
                btn_sua.Enabled = false;
                btn_xoa.Enabled = false;
                foreach (TextBox txt in inputRef())
                    txt.Clear();
                dataGV_Pri.ClearSelection();
            }
            else if (type == "edit")
            {
                txt_Pri_id.Enabled = false;
                btn_them.Enabled = false;
                btn_sua.Enabled = true;
                btn_xoa.Enabled = true;
            }
            
        }
        private void btn_search_Click(object sender, EventArgs e)
        {

            string keyword = textBox1.Text;
            DataTable dtb = ws.Search("Prisoners", "prisoner_id", keyword);

            // Hiển thị kết quả trên DataGridView
            dataGV_Pri.DataSource = dtb;
        }
        private bool validation(string type = "all")
        {
            object[] err = { "prisoner_id", "first_name", "last_name", "gender", "date_of_birth", "entry_date", "release_date", "crime", "sentence_duration", "cell_number", "dangerous_level", "status" };
            TextBox[] input = inputRef();
            DateTimePicker[] inpu = inputRe();
            int len = (type == "pk") ? 1 : input.Length + inpu.Length ; 
            
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
            return new TextBox[] { txt_Pri_id, txt_fir, txt_las, txt_gen, txt_gui, txt_num, txt_lev, txt_status };
        }
        private DateTimePicker[] inputRe()
        {
            return new DateTimePicker[] { dat_bir, dat_in, dat_rel };
        }
        private object[] inputVal()
        {
            return new object[] { txt_Pri_id.Text, txt_fir.Text, txt_las.Text, txt_gen.Text, txt_gui.Text, txt_sen.Text, txt_num.Text, txt_lev.Text, txt_status.Text, dat_bir.Text, dat_in.Text, dat_rel.Text };
        }

        private void dataGV_Pri_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TextBox[] input = inputRef();
            int r = dataGV_Pri.CurrentCell.RowIndex;
            for (int i = 0; i < input.Length; i++)
                input[i].Text = dataGV_Pri.Rows[r].Cells[i].Value.ToString();
            setButton("edit");
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (validation("pk"))
            {
                if (ws.Delete((DataTable)dataGV_Pri.DataSource, inputVal()))
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
                if (ws.Edit((DataTable)dataGV_Pri.DataSource, inputVal()))
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
                if (ws.Create((DataTable)dataGV_Pri.DataSource, inputVal()))
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

        
    }
}
