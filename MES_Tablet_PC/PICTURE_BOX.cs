using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Util;

namespace MES_Tablet_PC
{
    public partial class PICTURE_BOX : Form
    {

        public PICTURE_BOX()
        {
            InitializeComponent();
            label3.Text = main.M_var.YonghuName1;
            label5.Text = main.M_var.IMAC;
            label7.Text = main.M_var.ILine;
            label10.Text = main.M_var.ILineName;
            pictureBox1.ImageLocation = methods.sys_str_dir + "\\SOP下载.jpg";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //进行下线函数
                if (main.M_var.IMAC == "" || main.M_var.YonghuID1 == "")
                {
                    MessageBox.Show("MAC,用户ID不允许为空");
                }
                else 
                {
                    String strWork = methods.Serjavaweb.WorkRegister(main.M_var.IMAC, main.M_var.YonghuID1, "0");
                    if (strWork.Substring(0, 1) == "0")
                    {
                        methods.menuStrip1Remove(this.Text);
                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("下线失败：" + strWork);
                    }
                }
            }
            catch
            {
                MessageBox.Show("下线失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            methods.btnMinim_Click(this);
        }
    }
}
