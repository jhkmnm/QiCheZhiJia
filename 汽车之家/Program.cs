using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Aide
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            InitLog4Net();
            Tool.service.Url = System.Configuration.ConfigurationManager.AppSettings["dataSrvUrl"];
            try
            {
                var test = Tool.service.HelloWorld();
            }
            catch(Exception ex)
            {
                MessageBox.Show("软件启动异常，详询QQ:278815541");
                return;
            }
            var version = System.Diagnostics.FileVersionInfo.GetVersionInfo(Application.ExecutablePath).FileVersion.ToString();
            var newversion = Tool.service.GetDicByName("最新版本").Value;
            if (version != newversion)
            {
                MessageBox.Show("尊敬的用户，软件已经更新至" + newversion + "了，为了更好的使用软件，请下载最新的版本，详询QQ:278815541");
                return;
            }
            var form = new FormLogin(Site.Qiche);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            if(form.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new FormMain(form.qiche, form.yiche));
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if(e.ExceptionObject is System.Reflection.TargetInvocationException)
            {
                MessageBox.Show("普通新闻只能在win10系统下才能正常使用!");
            }
        }

        private static void InitLog4Net()
        {
            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
        }
    }

    public class Tool
    {

        public static Site site;
        public static Service.Service service = new Service.Service();

        public static Service.User userInfo_qc;

        public static Service.User userInfo_yc;

        public static string UTF8ToGBK(string str)
        {
            var buffer = Encoding.UTF8.GetBytes(str);
            return Encoding.GetEncoding("GBK").GetString(buffer);
        }

        public static AideTimer aideTimer = new AideTimer();
    }
}
