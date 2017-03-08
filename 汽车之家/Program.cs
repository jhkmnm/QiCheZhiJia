using System;
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
            Tool.service.Url = System.Configuration.ConfigurationManager.AppSettings["dataSrvUrl"];
            var form = new FormLogin(Site.Qiche);
            if(form.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new FormMain(form.qiche, form.yiche));
            }
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
