using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AideAdmin
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
            Application.Run(new Form1());

            //Tool.service.Url = System.Configuration.ConfigurationManager.AppSettings["dataSrvUrl"];
        }
    }

    public class Tool
    {
        public static localhost.Service service = new localhost.Service();
    }
}
