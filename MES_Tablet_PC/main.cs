using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MES_Tablet_PC
{
    public partial class main : Form
    {
        public static Mes_variable M_var = new Mes_variable();
        public main()
        {
            InitializeComponent();
            WindowStyle(this);
        }

        //设置窗体最大,无边框属性,
        private static void WindowStyle(Form form)
        {
            try
            {
                if (form.WindowState == FormWindowState.Maximized)
                {
                    form.WindowState = FormWindowState.Normal;
                }
                else
                {
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.WindowState = FormWindowState.Maximized;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        //panel1容器切换窗体方法
        internal void PaneAddForm(Form form, String ToolStripItem)
        {
            try
            {
                if (methods.Form_main_1(form))
                {
                }
                else 
                {
                    //添加窗体
                    form.TopLevel = false;
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.panel1.Controls.Add(form);
                    SetParent((int)form.Handle, (int)this.Handle);
                    form.Show();

                    //添加菜单栏
                    ToolStripMenuItem ToolstrAdd = new ToolStripMenuItem();
                    ToolstrAdd.Name = ToolStripItem;
                    ToolstrAdd.Text = ToolStripItem;
                    menuStrip1.Items.Add(ToolstrAdd);
                    ToolstrAdd.Click += methods.menuStrip1Click;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //点击窗体上的按钮加载窗体---向菜单添加菜单
        private void button_Click(object sender, EventArgs e)
        {
            Button but = (Button)sender;
            switch (but.Text)
            {
                case "终端注册(更新)":
                    PaneAddForm(new registration(), but.Text);
                    break;

                case "上线登录":
                    if (methods.Form_main_1(new PICTURE_BOX()))
                    {
                        break;
                    }
                    else 
                    {
                        PaneAddForm(new login(), but.Text);
                        break;
                    }
                case "故障申报":
                    if (methods.Form_main_1(new PICTURE_BOX()))
                    {
                        PaneAddForm(new Error_Applyfor(), but.Text);
                        break;
                    }
                    else
                    {
                        MessageBox.Show("还未上线作业，无法采集不良项", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                        break;
                    }
                     //PaneAddForm(new Error_Applyfor(), but.Text);
                        //break;
                default:
                    MessageBox.Show("输入信息有误", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    break;
            }
        }

        //设置子窗体显示前面引用using System.Runtime.InteropServices; 
        [DllImport("user32")]
        public static extern int SetParent(int hWndChild, int hWndNewParent);

    }
}
