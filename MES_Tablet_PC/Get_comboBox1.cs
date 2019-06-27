using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MES_Tablet_PC
{
    public partial class Get_comboBox1 : Form
    {
        private registration zd = null;
        public Get_comboBox1()
        {
            InitializeComponent();
            //设置无边框
            this.FormBorderStyle = FormBorderStyle.None;
        }
        //模仿下拉
        public void SetdataGridView1()
        {
            String strsql = "select b.line_no,b.line_name,b.workshop_center_code from MES_LINE b ";
            try
            {
                object[] str_p_list = methods.Serjavaweb.getColumnStringAll(strsql);
                String[] strlist;
                List<String> listS = new List<String>();
                int k = 0;
                if (str_p_list.Length == 0)
                {
                    MessageBox.Show("获取工序失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    for (int i = 0; i < str_p_list.Length; i++)
                    {
                        String str = str_p_list[i].ToString();
                        strlist = str.Split('[');
                        for (int j = 1; j < strlist.Length; j++)
                        {
                            listS.Add(strlist[j]);
                        }
                    }
                }
                for (int i = 0; i < str_p_list.Length; i++)
                {
                    this.dataGridView1.Rows.Add();
                    for (int j = 0; j < 3; j++)
                    {
                        this.dataGridView1.Rows[i].Cells[j].Value = listS[k];
                        k++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form_zhongduandengji()
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "registration")
                {
                    zd = f as registration;
                    break;
                }
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int i = this.dataGridView1.CurrentRow.Index;
                Form_zhongduandengji();
                zd.comboBox1.Text = this.dataGridView1.Rows[i].Cells[0].Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("装载失败" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Hide();
        }
    }
}
