using CsharpHttpHelper;
using CsharpHttpHelper.Enum;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Aide
{
    public class YiChe
    {
        /// <summary>
        /// 登录页面
        /// </summary>
        string loginurl = "http://dealer.easypass.cn/LoginPage/DefaultLogin.aspx";

        /// <summary>
        /// 登录提交
        /// </summary>
        string postlogin = "http://dealer.easypass.cn/Interface/CheckUserNameInterface.aspx?UserName={0}&CheckCode={1}&Pwd={2}&autoLogin=true&r=0.20315851840735166&appid=00000000-0000-0000-0000-000000000000&urls=";

        string validateCode = "http://dealer.easypass.cn/LoginCheckCode.ashx";
        string homeindex = "http://dealer.easypass.cn/HomeIndex.aspx?autoLogin=true";
        string useradmin = "http://dealer.easypass.cn/UserManager/UserAdmin.aspx";
        Html html = new Html();
        
        public Image LoadValidateCode()
        {
            HttpItem item = new HttpItem
            {
                URL = validateCode
            };
            var doc = html.Get(item);

            return html.GetImage(doc.RedirectUrl);            
        }

        public void GotoLoginPage()
        {
            html.Get(loginurl);
        }

        public ViewResult Login(string userName, string passWord, string code)
        {
            var result = UserLogin(userName, passWord, code);
            //if (result.Result)
            //{
            //    result = LoadPersonalInfo(userName, passWord);
            //}

            return result;
        }

        private ViewResult UserLogin(string userName, string passWord, string code)
        {
            var username = HttpHelper.URLEncode(userName, Encoding.UTF8);
            var password = HttpHelper.URLEncode(passWord, Encoding.UTF8);
            var url = string.Format(postlogin, username, code, password);

            var htmlDoc = html.Get(url);            
            ViewResult result = new ViewResult();
            result.Result = false;
            result.Exit = false;
            var strhtml = htmlDoc.DocumentNode.OuterHtml;
            if (strhtml.IndexOf("验证码") != -1)
            {
                result.Message = "验证码输入有误";
            }
            else if (strhtml.IndexOf("账号或密码不正确") != -1)
            {
                result.Message = "账号或密码不正确，请重新登录！";
            }
            else
            {
                result.Result = true;
                result.Exit = true;
                html.Get(homeindex);
            }

            return result;
        }

        /// <summary>
        /// 读取本地的账号和密码
        /// </summary>
        public string[] LoadPw()
        {
            string[] str = new string[2];
            if (ConfigurationManager.AppSettings["chkSavePass_YC"] == "True")
            {
                str[0] = ConfigurationManager.AppSettings["UserName_YC"];
                str[1] = ConfigurationManager.AppSettings["PassWord_YC"];
            }
            return str;
        }

        /// <summary>
        /// 本地保存登录的账号和密码
        /// </summary>
        public void SavePw()
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["UserName_YC"].Value = Tool.userInfo.UserName;
            configuration.AppSettings.Settings["PassWord_YC"].Value = Tool.userInfo.PassWord;
            configuration.AppSettings.Settings["chkSavePass_YC"].Value = "True";
            configuration.Save();
        }
    }
}
