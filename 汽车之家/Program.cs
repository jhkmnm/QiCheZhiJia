using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace 汽车之家
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
            Application.Run(new FormLogin());

            //Tool.service.Url = System.Configuration.ConfigurationManager.AppSettings["dataSrvUrl"];
        }
    }

    public class Tool
    {
        public static Service.Service service = new Service.Service();

        public static Service.User userInfo = new Service.User();
    }
}
