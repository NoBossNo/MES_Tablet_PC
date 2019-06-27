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
    public partial class login : Form
    {
        private object[] obj, obj_LineName,obj_pic_url;
        private String[] str_p_list, str_LineName,str_pic_url;
        Thread thread_LineName;
        public login()
        {
            InitializeComponent();
            textBox3.Text = main.M_var.IMAC = methods.GetMacAddress();
            thread_LineName = new Thread(GET_LineName);
            thread_LineName.IsBackground = true;
            thread_LineName.Start();
        }

        //缩小按钮
        private void button3_Click(object sender, EventArgs e)
        {
            methods.btnMinim_Click(this);
            
        }
        //关闭按钮
        private void button4_Click(object sender, EventArgs e)
        {
           methods.menuStrip1Remove(this.Text);
           this.Dispose();
        }

        //上线登录按钮 
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty || textBox2.Text == String.Empty || textBox3.Text == String.Empty || textBox4.Text == String.Empty)
            {
                MessageBox.Show("输入内容不允许为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                this.textBox1.Focus();
                set_loginString();
            }
            else if (str_p_list == null || str_LineName==null)
            {
                MessageBox.Show("获取信息错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                this.textBox1.Focus();
                set_loginString();
            }
            else if (str_pic_url==null)
            {
                MessageBox.Show("获取SOP失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                this.textBox1.Focus();
                set_loginString();
            }
            else if(methods.IF_Directory(methods.sys_str_dir))
            {
                String strWork = methods.Serjavaweb.WorkRegister(main.M_var.IMAC, main.M_var.YonghuID1, "1");
                if (strWork.Substring(0, 1) == "0")
                {
                    methods.Form_main();
                    methods.mai.PaneAddForm(new PICTURE_BOX(), "SOP作业");
                    methods.menuStrip1Remove(this.Text);
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("上线失败:" + strWork);
                    set_loginString();
                }
            }
        }
        
        //查询HR登记信息
        private void GET_HR_EMPLOYEE()
        {
            String strsql = "select a.hr_fcode,a.hr_fname,a.ctt_id from CTT_HR_EMPLOYEE a where a.ctt_id='" + textBox1.Text.ToString() + "'";
            try
            {
                 obj = methods.Serjavaweb.getColumnStrings(strsql);
                 str_p_list = obj[0].ToString().Split('[');
            }
            catch (Exception ex)
            {
                set_loginString();
                str_p_list = null;
                //MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        //获取线体，岗位资源，线体名称,工作中心
        private void GET_LineName()
        {
            String strsql = "select a.fline,a.frescode, a.line_code, b.line_name,b.workshop_no from Mes_Terminal a left join MES_LINE_INFO b on a.fline=b.line_no where a.fmac='" + main.M_var.IMAC + "'";
            try
            {
                obj_LineName = methods.Serjavaweb.getColumnStringAll(strsql);
                str_LineName = obj_LineName[0].ToString().Split('[');
                if (str_LineName!=null)
                {
                    main.M_var.ILineCod = str_LineName[1];
                    main.M_var.IResCode = str_LineName[2];
                    main.M_var.ILine = str_LineName[3];
                    main.M_var.ILineName = str_LineName[4];
                    main.M_var.Iworkshop_no = str_LineName[5];
                    select_pic_url();
                }
            }
            catch (Exception ex)
            {
                str_LineName = null;
                set_loginString();
                //str_LineName = null;
            }
        }

        //根据线体去查询正在生产的排产单的产品编码的SOP文件路径
        private void select_pic_url()
        {
            String strsql = "select b.pic_url,b.id from  mes_sop_file b" +
                            " left join mes_dep_task_info a" +
                            " on a.board_item=b.board_item" +
                            " left join mes_sop_file_line c" +
                            " on c.file_id=b.id" +
                            " where c.line_no='" + main.M_var.ILineCod +
                            "' and c.line_code='" + main.M_var.ILine +
                            "' and c.workshop_no='" + main.M_var.Iworkshop_no +
                            "' and a.task_no=(select tt.tf003 from mes_moctf tt  where tt.tf001='" + main.M_var.Iworkshop_no + "' and tt.tf002='" + main.M_var.ILine + "' and TF004='" + main.M_var.IResCode +
                            "' and tt.create_date=(select max(create_date) from mes_moctf where  tf001='" + main.M_var.Iworkshop_no + "' and  tf002='" + main.M_var.ILine + "' and TF004='" + main.M_var.IResCode + "'))";
            try
            {
                obj_pic_url = methods.Serjavaweb.getColumnStringAll(strsql);
                str_pic_url = obj_pic_url[0].ToString().Split('[');
                main.M_var.IURL = str_pic_url[1].ToString();
            }
            catch (Exception ex)
            {
                main.M_var.IURL = null;
                str_pic_url = null;
                //MessageBox.Show(ex.Message);
            }
        }

        //变量归档
        private void set_loginString()
        {
            main.M_var.YonghuID1 = null;
            main.M_var.Gonghao1 = null;
            main.M_var.YonghuName1 = null;
            textBox1.Text ="";
            textBox2.Text ="";
            textBox4.Text = "";

        }

        /*Textbox跳转*/
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (String.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("请刷卡", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.textBox1.Focus();
                }
                else
                {
                    GET_HR_EMPLOYEE();
                    if (obj == null || str_p_list == null)
                    {
                        MessageBox.Show("未找到信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.textBox1.Focus();
                    }
                    else
                    {
                        main.M_var.YonghuID1 = textBox1.Text;//设置变量用户ID
                        textBox4.Text = main.M_var.Gonghao1 = str_p_list[0];//设置变量用户工号
                        textBox2.Text = main.M_var.YonghuName1 = str_p_list[1];//设置变量用户姓名
                        SendKeys.Send("{tab}");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            methods.menuStrip1Remove(this.Text);
            this.Dispose();
        }

        //MessageBox.Show(main.M_var.ILineCod + "----" + main.M_var.IResCode + "----" + main.M_var.ILine + "----" + main.M_var.ILineName + "----" + main.M_var.Iworkshop_no, "提示", MessageBoxButtons.OK, MessageBoxIcon.None);

    }
}
