using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace MES_Tablet_PC
{
    public partial class registration : Form
    {
        Get_comboBox1 Get_combox1 = new Get_comboBox1();
        Get_comboBox2 Get_combox2 = new Get_comboBox2();
        Thread thread;
        public registration()
        {
            InitializeComponent();
            this.comboBox1.DropDownHeight = 1;
            this.comboBox1.DropDownWidth = 1;
            this.comboBox2.DropDownHeight = 1;
            this.comboBox2.DropDownWidth = 1;
            textBox1.Text = methods.GetMacAddress();
            thread = new Thread(Get_combox1.SetdataGridView1);
            thread.IsBackground = true;
            thread.Start();
        }

        //缩小按钮
        private void button3_Click_1(object sender, EventArgs e)
        {
            methods.btnMinim_Click(this);
        }
        //关闭按钮
        private void button4_Click(object sender, EventArgs e)
        {
            methods.menuStrip1Remove(this.Text);
            this.Dispose();
        }

        //点击下拉框获取线体
        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox2.Text = "";
            if (thread.ThreadState == ThreadState.Stopped)
            {
                int x, y;
                try
                {
                    x = comboBox1.Top + comboBox1.Height;//获取控件的上边距加高度
                    y = comboBox1.Left;//获取控件的左边距
                    Get_combox1.TopLevel = false;//设置为不是顶级控件
                    panel1.Controls.Add(Get_combox1);//向容器中添加窗体
                    Get_combox1.Top = x;//设置上边距
                    Get_combox1.Left = y;//设置左边距
                    Get_combox1.BringToFront();
                    Get_combox1.Show();//设置显示窗体
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                //子线程没有完成的情况下休眠等待1.3秒，给时间去完成
                System.Threading.Thread.Sleep(1300);
                int x, y;
                try
                {
                    x = comboBox1.Top + comboBox1.Height;//获取控件的上边距加高度
                    y = comboBox1.Left;//获取控件的左边距
                    Get_combox1.TopLevel = false;//设置为不是顶级控件
                    panel1.Controls.Add(Get_combox1);//向容器中添加窗体
                    Get_combox1.Top = x;//设置上边距
                    Get_combox1.Left = y;//设置左边距
                    Get_combox1.BringToFront();
                    Get_combox1.Show();//设置显示窗体
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void comboBox1_Enter(object sender, EventArgs e)
        {
            this.comboBox2.Focus();
        }

        //点击下拉框获取岗位资源代码
        private void comboBox2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("请先选择线体", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else
            {
                Get_combox2.SetdataGridView2();
                int x, y;
                try
                {
                    x = comboBox2.Top + comboBox2.Height;//获取控件的上边距加高度
                    y = comboBox2.Left;//获取控件的左边距
                    Get_combox2.TopLevel = false;//设置为不是顶级控件
                    panel1.Controls.Add(Get_combox2);//向容器中添加窗体
                    Get_combox2.Top = x;//设置上边距
                    Get_combox2.Left = y;//设置左边距
                    Get_combox2.BringToFront();
                    Get_combox2.Show();//设置显示窗体
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        private void comboBox2_Enter(object sender, EventArgs e)
        {
            this.textBox3.Focus();
        }

        //登记按钮
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == string.Empty || comboBox2.Text == string.Empty || textBox1.Text == string.Empty || textBox2.Text == string.Empty || textBox3.Text == string.Empty || textBox4.Text == string.Empty)
            {
                MessageBox.Show("输入内容不允许为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else
            {
                string oErrMessage = methods.Serjavaweb.TerminalRegister(textBox1.Text, textBox3.Text, textBox4.Text, comboBox2.Text, textBox2.Text);
                if (oErrMessage.Substring(0, 1) == "0")
                {
                    MessageBox.Show("认证OK！", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else
                {
                    MessageBox.Show("认证失败!" + oErrMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
            }
            TextNULL();
        }

        //控件归档
        private void TextNULL()
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

        }

        //更新按钮
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == string.Empty || comboBox2.Text == string.Empty || textBox1.Text == string.Empty || textBox2.Text == string.Empty || textBox3.Text == string.Empty || textBox4.Text == string.Empty)
            {
                MessageBox.Show("输入内容不允许为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else
            {
                string oErrMessage = methods.Serjavaweb.UpdateTerminalRes(textBox1.Text, textBox3.Text, textBox4.Text, comboBox2.Text, textBox2.Text);
                if (oErrMessage.Substring(0, 1) == "0")
                {
                    MessageBox.Show("更新OK！", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else
                {
                    MessageBox.Show("更新失败!" + oErrMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
            }
            TextNULL();
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("C:\\Program Files\\Common Files\\microsoft shared\\ink\\TabTip.exe");
            //uint WM_SYSCOMMAND = 274;
            //int SC_CLOSE = 61536;
            //IntPtr ptr = FindWindow("IPTip_Main_Window", null);
            //PostMessage(ptr, WM_SYSCOMMAND, SC_CLOSE, 0);
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("C:\\Program Files\\Common Files\\microsoft shared\\ink\\TabTip.exe");
        }

        [DllImport("user32")]
        static extern IntPtr FindWindow(String sClassName, String sAppName);
        [DllImport("user32")]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
    }
}
