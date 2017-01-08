using System;
using System.Collections.Generic;
using System.Linq;
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
            Application.Run(new FormLogin());

            //FormLogin f = new FormLogin();
            //if (f.ShowDialog() == DialogResult.OK)
            //{
            //    FormMain fm = new FormMain();
            //    fm.ShowDialog();
            //}

            //Tool.service.Url = System.Configuration.ConfigurationManager.AppSettings["dataSrvUrl"];
        }
    }

    public class Tool
    {
        public static Service.Service service = new Service.Service();

        public static Service.User userInfo_qc;

        public static Service.User userInfo_yc;
    }
}
