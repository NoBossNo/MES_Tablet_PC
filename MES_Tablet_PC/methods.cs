using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.IO;
using Util;

namespace MES_Tablet_PC
{
    class methods
    {
        internal static ServiceReference1.RfEntryClient Serjavaweb = new ServiceReference1.RfEntryClient();
        internal static main mai = null;
        
        internal static Form FormName = null;
        //获取程序安装下的路径
        internal static String sys_str_dir = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "SOP_PICTURE";
        
        //改变main中文本触发事件
        internal static void Form_main()
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "main")
                {
                    mai = f as main;
                    break;
                }
            }
        }

        //去判断窗口是否有打开
        internal static bool Form_main_1(Form forom)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == forom.Name)
                {
                    f.Show();
                    return true;
                }
            }
            return false;
        }

        //移除main中的菜单栏项
        internal static void menuStrip1Remove(String ToolStripItemName)
        {
            Form_main();
            for (int i = 0; i < mai.menuStrip1.Items.Count; i++)
            {
                ToolStripItem Toolmen = mai.menuStrip1.Items[i];
                if (Toolmen.Name == ToolStripItemName)
                {
                    mai.menuStrip1.Items.Remove(Toolmen);
                }
            }
        }

        //点击菜单栏项时先将所有窗体都隐藏，再显示点击的菜单窗体
        internal static void menuStrip1Click(object sender, EventArgs e)
        {
            foreach (Form form_maneHide in Application.OpenForms)
            {
                if (form_maneHide.Name!="main")
                form_maneHide.Hide();
            }
            ToolStripMenuItem Toolstrclick = (ToolStripMenuItem)sender;
            foreach (Form form_mane in Application.OpenForms)
            {
                if (form_mane.Text == Toolstrclick.Text)
                {
                    FormName = form_mane;
                    FormName.Show();
                    break;
                }
            }
        }

        /*获取本机MAC地址*/
        internal static string GetMacAddress()
        {
            try
            {
                string strMac = string.Empty;
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        strMac = mo["MacAddress"].ToString();
                    }
                }
                moc = null;
                mc = null;
                return strMac;
            }
            catch
            {
                return "获取失败";
            }
        }


        //设置最大化最小化
        //public static void btnMaxim_Click(Form form)
        //{
        //    if (form.WindowState != FormWindowState.Maximized)
        //    {
        //        form.WindowState = FormWindowState.Maximized;
        //    }
        //}
        internal static void btnMinim_Click(Form form)
        {
            form.Hide();
        }

        //下载图片并显示出来
        internal static bool IF_Directory(string str_dir)
        {
            try
            {
                if (main.M_var.IURL != null)
                {
                    if (!Directory.Exists(str_dir))
                    {
                        //判断有没有路径
                        Directory.CreateDirectory(str_dir);
                        //获取ftpIP，用户，密码
                        String strsql = "select ftp_ip,ftp_user,ftp_pwd from MES_SOP_FTP a where rownum=1";
                        object[] obj_1 = methods.Serjavaweb.getColumnStringAll(strsql);
                        String[] str_p_LineName = obj_1[0].ToString().Split('[');
                        //登录FTP
                        FTPUtil ftp = new FTPUtil(str_p_LineName[1].ToString(), "", str_p_LineName[2].ToString(), str_p_LineName[3].ToString());
                        //开始下载
                        ftp.Download_dz(main.M_var.IURL, str_dir + "\\SOP下载.jpg");
                        return true;
                    }
                    else
                    {
                        //获取ftpIP，用户，密码
                        String strsql = "select ftp_ip,ftp_user,ftp_pwd from MES_SOP_FTP a where rownum=1";
                        object[] obj_1 = methods.Serjavaweb.getColumnStringAll(strsql);
                        String[] str_p_LineName = obj_1[0].ToString().Split('[');
                        //登录FTP
                        FTPUtil ftp = new FTPUtil(str_p_LineName[1].ToString(), "", str_p_LineName[2].ToString(), str_p_LineName[3].ToString());
                        //开始下载
                        ftp.Download_dz(main.M_var.IURL, str_dir + "\\SOP下载.jpg");
                        return true;
                    }
                }
                else
                {
                    MessageBox.Show("获取图片失败路径为空");
                    return false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("获取图片失败请检查维护SOP是否与此MAC绑定岗位一致" + ex.Message);
                return false;
            }
        }

    }

    public class Mes_variable
    {
        private string iMAC,/*MAC地址*/ iUserid,/*用户名*/ iPassword,/*密码*/ iLineCod,/*工序线体*/ iResCode,/*工序*/iworkshop_no,/*工作中心*/ iLine,/*线体*/ iLineName,/*线体名称*/YonghuID,/*员工ID*/ Gonghao,/*员工工号*/ YonghuName,/*员工姓名*/iURL/*URL*/;

        /*URL*/
        public string IURL
        {
            get { return iURL; }
            set { iURL = value; }
        }

        /*工作中心*/
        public string Iworkshop_no
        {
            get { return iworkshop_no; }
            set { iworkshop_no = value; }
        }

        //用户名称
        public string YonghuName1
        {
            get { return YonghuName; }
            set { YonghuName = value; }
        }
        //用户工号
        public string Gonghao1
        {
            get { return Gonghao; }
            set { Gonghao = value; }
        }
        //用户卡ID
        public string YonghuID1
        {
            get { return YonghuID; }
            set { YonghuID = value; }
        }
        //线体名称
        public string ILineName
        {
            get { return iLineName; }
            set { iLineName = value; }
        }
        //线体
        public string ILine
        {
            get { return iLine; }
            set { iLine = value; }
        }
        //线体工序
        public string IResCode
        {
            get { return iResCode; }
            set { iResCode = value; }
        }
        //线体
        public string ILineCod
        {
            get { return iLineCod; }
            set { iLineCod = value; }
        }
        //用户密码
        public string IPassword
        {
            get { return iPassword; }
            set { iPassword = value; }
        }
        //用户账号
        public string IUserid
        {
            get { return iUserid; }
            set { iUserid = value; }
        }
        //MAC地址
        public string IMAC
        {
            get { return iMAC; }
            set { iMAC = value; }
        }

    }

}
