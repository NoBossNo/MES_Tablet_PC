using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MES_Tablet_PC
{
    public partial class Error_Applyfor : Form
    {
        String strsql;
        String[] str_sys_code = null;
        object[] str_p_sys_code;
        public Error_Applyfor()
        {
            InitializeComponent();
            ThreadStart ts = new ThreadStart(Error);
            Thread thread = new Thread(ts);
            thread.Name = "Error";
            thread.Start();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            methods.menuStrip1Remove(this.Text);
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            methods.btnMinim_Click(this);
        }

        private void Error() 
        {
            textBox4.Text = main.M_var.ILineName;
            textBox2.Text = main.M_var.YonghuName1;
            try
            {
                strsql = "select de003 from SYS_CODE where de001='缺陷原因'";
                str_p_sys_code = methods.Serjavaweb.getColumnStringAll(strsql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载数据错误，请联系管理员" + ex.Message);
            }
            if(str_p_sys_code!=null)
            {
                Setcombobox(this.comboBox1);
            }

        }

        private void Setcombobox(ComboBox comb) 
        {
            if (this.comboBox1.InvokeRequired)
            {
                myDelegate mydle = new myDelegate(this.Setcombobox);
                this.Invoke(mydle, new object[] { comb });
            }
            else 
            {
                for (int i = 0; i < str_p_sys_code.Length; i++)
                {
                    String str = str_p_sys_code[i].ToString();
                    str_sys_code = str.Split('[');
                    for (int j = 1; j <= str_sys_code.Length - 1; j++)
                    {
                        comb.Items.Add(str_sys_code[j]);
                    }
                }
            }

        }

        private delegate void myDelegate(ComboBox com);

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || textBox4.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("请检查内容不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else 
            {
                //调用故障申报函数
            }
        }
    }
}
