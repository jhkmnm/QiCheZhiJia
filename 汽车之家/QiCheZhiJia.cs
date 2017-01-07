using CsharpHttpHelper;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Aide
{
    public class QiCheZhiJia
    {
        string validateCode = "http://ics.autohome.com.cn/passport/Account/GetDealerValidateCode?time=1483443132656";
        /// <summary>
        /// 登录页面
        /// </summary>
        string loginurl = "http://ics.autohome.com.cn/passport/Account/Login";
        /// <summary>
        /// 登录提交
        /// </summary>
        string postlogin = "http://ics.autohome.com.cn/passport/";
        string entervalidateCode = "http://ics.autohome.com.cn/passport/Account/GetEnterpriseValidateCode";
        /// <summary>
        /// 账号列表
        /// </summary>
        string employeelist = "http://ics.autohome.com.cn/BSS/Employee/GetEmployee?dealerId=0&take=30";
        /// <summary>
        /// 公共线索
        /// </summary>
        string dmsOrder = "http://ics.autohome.com.cn/Dms/Order/Index";

        /// <summary>
        /// 获取公共订单
        /// </summary>
        string publicOrder = "http://ics.autohome.com.cn/Dms/Order/GetPublicOrders?timeStamp={0}&ik=9DF3FD033BAD49F2AD12824D56DB11A9&appid=dms&provinceid={1}&cityid={2}&factoryID={3}&seriesid={4}&logicType={5}&pageindex=1&pagesize=200&tk=2644691E-91BE-4F2F-97B3-57FD0356D52C";

        Html html = new Html();
        private static string StrJS = "";
        string token = "";
        string redisKey = "";
        string exponment = "";
        string modulus = "";

        public QiCheZhiJia(string js)
        {
            StrJS = js;
        }

        public Image LoadValidateCode()
        {
            return html.GetImage(validateCode);
        }

        public void GotoLoginPage()
        {
            var htmlDoc = html.Get(loginurl);

            token = htmlDoc.DocumentNode.SelectNodes("//input[@name='__RequestVerificationToken']")[0].GetAttributeValue("value", "");
            redisKey = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='rkey']").GetAttributeValue("value", "");
            exponment = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='hidPublicKeyExponent']").GetAttributeValue("value", "");
            modulus = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"hidPublicKeyModulus\"]").GetAttributeValue("value", "");

            html.Get(entervalidateCode);
        }

        private ViewResult LoadPersonalInfo(string userName, string passWord)
        {
            ViewResult vresult = new ViewResult();
            var htmlDoc = html.Get(employeelist);
            var result = JsonConvert.DeserializeObject<LinkResult>(htmlDoc.DocumentNode.OuterHtml);
            var infoformat = "姓名:{0};性别:{1};职位:{2};手机:{3};座机:{4}" + Environment.NewLine;
            StringBuilder sb = new StringBuilder(result.Data.SaleList.Count * 25);

            result.Data.SaleList.ForEach(item => sb.AppendFormat(infoformat, item.Name, item.Sex == "1" ? "男" : "女", item.RoleName, item.Phone, item.TelPhone));

            Service.User user = new Service.User
            {
                Company = result.Data.SaleList[0].CompanyString,
                PassWord = passWord,
                UserName = userName,
                Status = 1,
                LinkInfo = sb.ToString()
            };

            var loginResult = Tool.service.UserLogin(user);

            if (loginResult.Result)
            {
                Tool.userInfo = loginResult.Data;
            }
            else
            {
                vresult.Message = loginResult.Message;
                vresult.Exit = true;
            }
            vresult.Result = loginResult.Result;

            return vresult;
        }

        public ViewResult Login(string userName, string passWord, string code)
        {
            var result = UserLogin(userName, passWord, code);
            if(result.Result)
            {
                result = LoadPersonalInfo(userName, passWord);
            }

            return result;
        }

        private ViewResult UserLogin(string userName, string passWord, string code)
        {
            var username = HttpHelper.URLEncode(userName, Encoding.UTF8);
            var password = passWord;

            var jsmain = "mytest(\"{0}\",\"{1}\",\"{2}\")";
            var postdata = "__RequestVerificationToken={0}&UserNameDealer={1}&PasswordDealer={2}&RedisKey={3}&checkCodeDealer={4}";

            var passenc = HttpHelper.JavaScriptEval(StrJS, string.Format(jsmain, exponment, modulus, password));

            var item = new HttpItem
            {
                URL = postlogin,
                ContentType = "application/x-www-form-urlencoded; charset=UTF-8",
                Method = "post",
                Postdata = string.Format(postdata, token, username, passenc, redisKey, code),
                Referer = loginurl,
                UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)"
            };
            item.Header.Add("X-Requested-With", "XMLHttpRequest");
            item.Allowautoredirect = false;

            ViewResult result = new ViewResult();
            result.Result = false;
            result.Exit = false;

            var strhtml = html.Post(item).DocumentNode.OuterHtml;

            if (strhtml.IndexOf("8|1") != -1)
            {
                result.Message = "验证码输入有误，请重新输入！";                
            }
            else if (strhtml.IndexOf("3|1") != -1)
            {
                result.Message = "该用户帐户信息错误或访问受限";
            }
            else if (strhtml.IndexOf("2|1") != -1)
            {
                result.Message = "用户不存在或者密码错误";            
            }
            else if (strhtml.IndexOf("5|1") != -1)
            {
                result.Message = "验证码过期";
            }
            else if (strhtml == "0")
            {
                result.Result = true;
                result.Exit = true;
            }
            else
            {
                result.Message = "登录异常！建议重试！";
            }            

            return result;
        }

        /// <summary>
        /// 本地保存登录的账号和密码
        /// </summary>
        public void SavePw()
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["UserName_QC"].Value = Tool.userInfo.UserName;
            configuration.AppSettings.Settings["PassWord_QC"].Value = Tool.userInfo.PassWord;
            configuration.AppSettings.Settings["chkSavePass_QC"].Value = "True";
            configuration.Save();
        }

        private static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// 读取本地的账号和密码
        /// </summary>
        public string[] LoadPw()
        {
            string[] str = new string[2];
            if (ConfigurationManager.AppSettings["chkSavePass_QC"] == "True")
            {
                str[0] = ConfigurationManager.AppSettings["UserName_QC"];
                str[1] = ConfigurationManager.AppSettings["PassWord_QC"];
            }
            return str;
        }
    }
}
