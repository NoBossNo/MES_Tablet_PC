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
    public partial class Get_comboBox2 : Form
    {
        private registration zd = null;
        public Get_comboBox2()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }

        public void SetdataGridView2()
        {
            Form_zhongduandengji();
            while (this.dataGridView2.Rows.Count != 0)
            {
                this.dataGridView2.Rows.RemoveAt(0);
            }

            String strsql = "select a.line_no,a.proc_no,a.line_code,a.line_name from MES_LINE_INFO a where a.line_code='" + zd.comboBox1.Text + "'";
            try
            {
                object[] str_p_list_1 = methods.Serjavaweb.getColumnStringAll(strsql);
                String[] strlist_1;
                List<String> listS = new List<String>();
                int k = 0;
                if (str_p_list_1.Length == 0)
                {
                    MessageBox.Show("获取工序失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    for (int i = 0; i < str_p_list_1.Length; i++)
                    {
                        String str = str_p_list_1[i].ToString();
                        strlist_1 = str.Split('[');
                        for (int j = 1; j < strlist_1.Length; j++)
                        {
                            listS.Add(strlist_1[j]);
                        }
                    }
                }
                for (int i = 0; i < str_p_list_1.Length; i++)
                {
                    this.dataGridView2.Rows.Add();
                    for (int j = 0; j < 4; j++)
                    {
                        this.dataGridView2.Rows[i].Cells[j].Value = listS[k];
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

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int i = this.dataGridView2.CurrentRow.Index;
                Form_zhongduandengji();
                zd.comboBox2.Text = this.dataGridView2.Rows[i].Cells[0].Value.ToString();
                zd.textBox2.Text  =this.dataGridView2.Rows[i].Cells[1].Value.ToString(); 

            }
            catch (Exception ex)
            {
                MessageBox.Show("装载失败" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Hide();
        }
    }
}
