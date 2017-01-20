using CsharpHttpHelper;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using CsharpHttpHelper.Enum;
using System.IO;
using System.Threading;

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
        string publicOrder = "http://ics.autohome.com.cn/Dms/Order/GetPublicOrders?ki=9DF3FD033BAD49F2AD12724D56DB11A9&appid=dms&provinceid={0}&cityid={1}&factoryID={2}&seriesid={3}&logicType={4}&pageindex=1&pagesize=200&kt=2644691E-91BE-4F2F-97B3-57FD0316D52C";

        string onsalelist = "http://ics.autohome.com.cn/Price/CarPrice/GetOnSaleList?dealerId={0}";

        #region 资讯模板
        string model_TS = "http://ics.autohome.com.cn/Price/NewsTemplateSection1";
        #endregion

        Html html = new Html();
        private static string StrJS = "";
        string token = "";
        string redisKey = "";
        string exponment = "";
        string modulus = "";
        string cookie = "";
        DAL dal = new DAL();

        public string pid{get;set;}
        public string cid{get;set;}
        public string sid{get;set;}
        public string fid { get; set; }
        public string oid{get;set;}
        public List<Nicks> nicks { get; set; }

        public QiCheZhiJia(string js)
        {
            StrJS = js;
        }

        #region Html
        private HtmlDocument GetHtml(string url)
        {
            var item = new HttpItem()
            {
                URL = url,
                Method = "get",
                ContentType = "text/html",
                Cookie = cookie
            };

            HttpHelper http = new HttpHelper();
            HttpResult htmlr = http.GetHtml(item);
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlr.Html);
            return htmlDoc;
        }
        #endregion

        #region 登录
        public Image LoadValidateCode()
        {
            HttpItem item = new HttpItem()
            {
                URL = validateCode,
                Method = "get",
                ResultType = ResultType.Byte
            };

            HttpHelper http = new HttpHelper();
            var result = http.GetHtml(item);
            cookie += HttpHelper.GetSmallCookie(result.Cookie);
            MemoryStream ms = new MemoryStream(result.ResultByte);
            return Bitmap.FromStream(ms, true);
        }

        public void GotoLoginPage()
        {
            var item = new HttpItem()
            {
                URL = loginurl,
                Method = "get",
                ContentType = "text/html"
            };
            HttpHelper http = new HttpHelper();
            HttpResult result = http.GetHtml(item);
            cookie = HttpHelper.GetSmallCookie(result.Cookie);

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(result.Html);

            token = htmlDoc.DocumentNode.SelectNodes("//input[@name='__RequestVerificationToken']")[0].GetAttributeValue("value", "");
            redisKey = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='rkey']").GetAttributeValue("value", "");
            exponment = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='hidPublicKeyExponent']").GetAttributeValue("value", "");
            modulus = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"hidPublicKeyModulus\"]").GetAttributeValue("value", "");

            item.URL = entervalidateCode;            
            result = http.GetHtml(item);
            cookie += HttpHelper.GetSmallCookie(result.Cookie);            
        }

        /// <summary>
        /// 本地保存登录的账号和密码
        /// </summary>
        public void SavePw()
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["UserName_QC"].Value = Tool.userInfo_qc.UserName;
            configuration.AppSettings.Settings["PassWord_QC"].Value = Tool.userInfo_qc.PassWord;
            configuration.AppSettings.Settings["chkSavePass_QC"].Value = "True";
            configuration.Save();
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

        /// <summary>
        /// 加载联系人信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        private ViewResult LoadPersonalInfo(string userName, string passWord)
        {
            ViewResult vresult = new ViewResult();
            
            HtmlDocument htmlDoc = GetHtml(employeelist);
            
            var result = JsonConvert.DeserializeObject<LinkResult>(htmlDoc.DocumentNode.OuterHtml);
            var infoformat = "姓名:{0};性别:{1};职位:{2};手机:{3};座机:{4}" + Environment.NewLine;
            StringBuilder sb = new StringBuilder(result.Data.SaleList.Count * 25);

            result.Data.SaleList.ForEach(a => sb.AppendFormat(infoformat, a.Name, a.Sex == "1" ? "男" : "女", a.RoleName, a.Phone, a.TelPhone));

            Service.User user = new Service.User
            {
                Company = result.Data.SaleList[0].CompanyString.TrimEnd(','),
                CompanyID = result.Data.SaleList[0].CompanyID.ToString(),
                SiteName = "汽车之家",
                PassWord = passWord,
                UserName = userName,
                Status = 1,
                LinkInfo = sb.ToString()
            };

            var loginResult = Tool.service.UserLogin(user);

            if (loginResult.Result)
            {
                Tool.userInfo_qc = loginResult.Data;
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
                UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)",
                Cookie = cookie
            };
            item.Header.Add("X-Requested-With", "XMLHttpRequest");
            item.Allowautoredirect = false;

            ViewResult result = new ViewResult();
            result.Result = false;
            result.Exit = false;
            
            HttpHelper http = new HttpHelper();
            HttpResult htmlr = http.GetHtml(item);
            cookie += HttpHelper.GetSmallCookie(htmlr.Cookie);
            cookie = HttpHelper.GetSmallCookie(cookie);

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlr.Html);

            var strhtml = htmlDoc.DocumentNode.OuterHtml;

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

        #endregion

        #region 抢单
        /// <summary>
        /// 加载公共订单页面，省，市，车型，订单类型选项
        /// </summary>
        public HtmlDocument LoadOrder()
        {
            return GetHtml(dmsOrder);
        }

        public List<Rows> GetNicks()
        {
            HtmlDocument htmlDoc = GetHtml("http://ics.autohome.com.cn/dms/Order/GetDealerSales");

            var result = JsonConvert.DeserializeObject<NicksResult>(htmlDoc.DocumentNode.OuterHtml);

            return result.rows;
        }

        private List<PublicOrder> GetNewOrder(string pid, string cid, string sid, string fid, string oid)
        {
            HtmlDocument htmlDoc = GetHtml(string.Format(publicOrder, pid, cid, fid, sid, oid));

            var result = JsonConvert.DeserializeObject<ReturnResult>(htmlDoc.DocumentNode.OuterHtml);

            return result.Result.List;
        }

        /// <summary>
        /// 执行分配订单
        /// </summary>
        public void SendOrder()
        {
            ViewResult result = new ViewResult();
            while (true)
            {
                var orders = GetNewOrder(pid, cid, fid, sid, oid);
                result.Result = false;
                if (orders.Count > 0)
                {
                    orders.ForEach(a => dal.AddOrders(new Orders { CustomerName = a.CustomerName, Id = a.Id }));

                    var sendlogs = dal.GetTodaySendLog();

                    var total = orders.Count + sendlogs.Sum(s => s.OrderCount);
                    var count = nicks.Count;

                    for (int i = 0; i < nicks.Count; i++)
                    {
                        var sendcount = GetAvg(total, count);
                        if (sendcount <= 0)
                            break;

                        int send = sendcount;                        
                        total -= sendcount;
                        count--;

                        if (sendlogs.Count > 0)
                        {
                            var sendlog = sendlogs.FirstOrDefault(f => f.NickID == nicks[i].Id);
                            if (sendlog != null && sendlog.OrderCount < sendcount)
                            {
                                send = sendcount - sendlog.OrderCount;
                            }
                        }

                        var sendorders = orders.Take(send).ToList();
                        orders.RemoveRange(0, send);

                        sendorders.ForEach(a =>
                        {
                            if (SendOrder(nicks[i], a))
                            {
                                dal.UpdateOrderSend(a.Id, nicks[i].Id);
                                dal.UpdateSendCount(nicks[i].Id);
                                result.Message += string.Format("系统事件{0}将客户分配给销售顾问{1}{2}", DateTime.Now.ToString(), nicks[i].Nick, Environment.NewLine);
                                result.Result = true;
                                SendResult(result);
                            }
                        });
                    }
                }
                
                //间隔2分钟
                Thread.Sleep(1000 * 60 * 2);
            }
        }

        private bool SendOrder(Nicks nick, PublicOrder order)
        {
            string str = HttpHelper.URLEncode(nick.Nick, Encoding.UTF8).ToUpper();
            string str2 = string.Format("phone={0}&orderID={1}&dealerID=0&saleId={2}&saleName={3}", new object[] { order.CustomerPhone, order.Id, nick.Id, str });
            HttpItem item = new HttpItem
            {
                URL = "http://ics.autohome.com.cn/dms/Order/AssignOrder",
                Method = "post",
                Cookie = cookie,
                Postdata = str2,
                ContentType = "application/x-www-form-urlencoded; charset=UTF-8",
                Referer = "http://ics.autohome.com.cn/dms/Order/Index",
                UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)"
            };
            item.Header.Add("X-Requested-With", "XMLHttpRequest");
            item.Allowautoredirect = false;

            HttpHelper http = new HttpHelper();
            HttpResult htmlr = http.GetHtml(item);

            if (htmlr.Html.IndexOf(":0}") == -1)
            {
                return false;
            }
            return true;
        }

        private int GetAvg(int total, int count)
        {
            return (int)Math.Ceiling(total / (count * 1.0));
        }

        public delegate void delsendorder(ViewResult vr);
        public event delsendorder SendOrderEvent;

        public void SendResult(ViewResult vr)
        {
            if (SendOrderEvent != null)
            {
                SendOrderEvent(vr);
            }
        }

        #endregion

        #region 保存价格
        private List<SaleData> GetOnSaleList()
        {
            var result = new List<SaleData>();
            
            HtmlDocument htmlDoc = GetHtml(string.Format(onsalelist, Tool.userInfo_qc.CompanyID));
            var saledata = JsonConvert.DeserializeObject<OnSaleData>(htmlDoc.DocumentNode.OuterHtml);
            result.AddRange(saledata.Data);
            if(saledata.RecordCount > 2)
            {
                int index = 2;
                while(index <= saledata.RecordCount)
                {
                    htmlDoc = GetHtml(string.Format(onsalelist, Tool.userInfo_qc.CompanyID) + "&skip=" + index.ToString());
                    saledata = JsonConvert.DeserializeObject<OnSaleData>(htmlDoc.DocumentNode.OuterHtml);
                    if (saledata.Data == null)
                        break;
                    index += 2;
                    result.AddRange(saledata.Data);
                }
            }

            return result;
        }

        /// <summary>
        /// 保存价格
        /// </summary>
        /// <returns></returns>
        public ViewResult SavePrice()
        {
            ViewResult result = new ViewResult();
            result.Result = false;

            var saledata = GetOnSaleList();

            if (saledata.Count == 0)
            {
                result.Message = "未找到数据";
                return result;
            }

            int companyid = Convert.ToInt32(Tool.userInfo_qc.CompanyID);           

            int[] specIds = saledata.Select(a => a.SpecId).ToArray();
            int[] prices = saledata.Select(a => a.Price).ToArray();
            int[] minPrices = saledata.Select(a => a.MinPrice).ToArray();

            var posturl = "http://ics.autohome.com.cn/Price/CarPrice/SavePrice?r=0.4723048365226039";
            StringBuilder sb = new StringBuilder(specIds.Length * 30);
            sb.Append("dealerIds%5B%5D=" + companyid);
            foreach (int i in specIds)
            {
                sb.Append("&specIds%5B%5D=" + i);
            }
            foreach (int i in prices)
            {
                sb.AppendFormat("&prices%5B%5D=" + i);
            }
            foreach (int i in minPrices)
            {
                sb.AppendFormat("&minPrices%5B%5D=" + i);
            }

            var item = new HttpItem
            {
                URL = posturl,
                ContentType = "application/x-www-form-urlencoded; charset=UTF-8",
                Method = "post",
                Postdata = sb.ToString(),
                Referer = "http://ics.autohome.com.cn/Price/CarPrice/OnSale",
                UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)",
                Cookie = cookie
            };
            item.Header.Add("X-Requested-With", "XMLHttpRequest");
            item.Allowautoredirect = false;

            HttpHelper http = new HttpHelper();
            HttpResult htmlr = http.GetHtml(item);
            result.Result = saledata.Count > 0;
            result.Message = "一共成功保存"+ saledata.Count.ToString() +"条报价";
            return result;
        }

        #endregion

        #region 资讯
        public HtmlDocument GoToNews()
        {
            return GetHtml(model_TS);
        }
        #endregion
    }
}
