using System;
using System.Text;
using System.Windows.Forms;
using CsharpHttpHelper;
using System.IO;
using System.Configuration;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Aide;
using AideM;

namespace 汽车之家
{
    public partial class FormLogin : Form
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
        Html html = new Html();
        private static string StrJS = "";
        string token = "";
        string redisKey = "";
        string exponment = "";
        string modulus = "";

        public FormLogin()
        {
            InitializeComponent();
        }

        private void LoadValidateCode()
        {
            pbCode.Image = html.GetImage(validateCode);
        }

        private void GotoLoginPage()
        {
            var htmlDoc = html.Get(loginurl);

            token = htmlDoc.DocumentNode.SelectNodes("//input[@name='__RequestVerificationToken']")[0].GetAttributeValue("value", "");
            redisKey = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='rkey']").GetAttributeValue("value", "");
            exponment = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='hidPublicKeyExponent']").GetAttributeValue("value", "");
            modulus = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"hidPublicKeyModulus\"]").GetAttributeValue("value", "");

            html.Get(entervalidateCode);
        }

        private bool LoadPersonalInfo()
        {
            var htmlDoc = html.Get(employeelist);
            var result = JsonConvert.DeserializeObject<LinkResult>(htmlDoc.DocumentNode.OuterHtml);
            var linkinfo = "姓名:{0};职位:{1};手机:{2}"+ Environment.NewLine;

            Service.User user = new Service.User{
                Company = result.Data.SaleList[0].CompanyString,
                PassWord = txtPassword.Text,
                UserName = txtUserName.Text,
                Status = 1
            };

            var loginResult = Tool.service.UserLogin(user);

            if(loginResult.Result)
            {
                Tool.userInfo = loginResult.Data;
            }
            else
            {
                MessageBox.Show(loginResult.Message);
            }
            return loginResult.Result;
        }

        private bool Login()
        {
            UserInfo.UserName = "";
            UserInfo.Password = "";

            var username = HttpHelper.URLEncode("晋江嘉华雷克萨斯", Encoding.UTF8);
            var password = "qzzs8888.";
            var code = txtCode.Text;

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

            var strhtml = html.Post(item).DocumentNode.OuterHtml;

            if (strhtml.IndexOf("8|1") != -1)
            {
                MessageBox.Show("验证码输入有误，请重新输入！");
                return false;
            }
            if (strhtml.IndexOf("3|1") != -1)
            {
                MessageBox.Show("密码错误");
                return false;
            }
            if (strhtml.IndexOf("2|1") != -1)
            {
                MessageBox.Show("用户名不存在");
                return false;
            }
            if (strhtml == "0")
            {
                return true;
            }
            MessageBox.Show("登录异常！建议重试！");
            return false;
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            this.LoadPw();
            string path = AppDomain.CurrentDomain.BaseDirectory + "js.lyt";
            if (!File.Exists(path))
            {
                MessageBox.Show("js.lyt文件缺失，建议重新解压软件解决！");
                base.Close();
            }
            StrJS = File.ReadAllText(path);
            GotoLoginPage();
            LoadValidateCode();
        }

        /// <summary>
        /// 本地保存登录的账号和密码
        /// </summary>
        private void SavePw()
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (this.chkSavePass.Checked)
            {
                configuration.AppSettings.Settings["UserName"].Value = UserInfo.UserName;
                configuration.AppSettings.Settings["PassWord"].Value = UserInfo.Password;
            }
            configuration.AppSettings.Settings["chkSavePass"].Value = this.chkSavePass.Checked.ToString();
            configuration.Save();
        }
        
        /// <summary>
        /// 读取本地的账号和密码
        /// </summary>
        private void LoadPw()
        {
            if (ConfigurationManager.AppSettings["chkSavePass"] == "True")
            {
                this.chkSavePass.Checked = true;
                this.txtUserName.Text = ConfigurationManager.AppSettings["UserName"];
                this.txtPassword.Text = ConfigurationManager.AppSettings["PassWord"];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Login())
            {
                if(LoadPersonalInfo())
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.DialogResult = DialogResult.No;
                }

                this.Close();
            }
        }

        private void btnRefImg_Click(object sender, EventArgs e)
        {
            LoadValidateCode();
        }
    }
}
