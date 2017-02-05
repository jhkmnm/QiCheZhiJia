using CsharpHttpHelper;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using HAP = HtmlAgilityPack;
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
        string model_TS = "http://ics.autohome.com.cn/Price/NewsTemplateSection1/edit?newsId=";
        string news_draft = "http://ics.autohome.com.cn/Price/News/GetDraft?random=0.47937742094599356&take=200";
        #endregion

        Html html = new Html();
        private static string StrJS = "";
        string token = "";
        string redisKey = "";
        string exponment = "";
        string modulus = "";
        public string cookie = "";
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
        private HAP.HtmlDocument GetHtml(string url)
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
            HAP.HtmlDocument htmlDoc = new HAP.HtmlDocument();
            htmlDoc.LoadHtml(htmlr.Html);
            return htmlDoc;
        }
        #endregion

        #region 登录
        public System.Drawing.Image LoadValidateCode()
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

            HAP.HtmlDocument htmlDoc = new HAP.HtmlDocument();
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

            HAP.HtmlDocument htmlDoc = GetHtml(employeelist);
            
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

            HAP.HtmlDocument htmlDoc = new HAP.HtmlDocument();
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
        public HAP.HtmlDocument LoadOrder()
        {
            return GetHtml(dmsOrder);
        }

        public List<Rows> GetNicks()
        {
            HAP.HtmlDocument htmlDoc = GetHtml("http://ics.autohome.com.cn/dms/Order/GetDealerSales");

            var result = JsonConvert.DeserializeObject<NicksResult>(htmlDoc.DocumentNode.OuterHtml);

            return result.rows;
        }

        private List<PublicOrder> GetNewOrder(string pid, string cid, string sid, string fid, string oid)
        {
            HAP.HtmlDocument htmlDoc = GetHtml(string.Format(publicOrder, pid, cid, fid, sid, oid));

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

                    //更新发送数量
                    nicks.ForEach(f => f.Send = sendlogs.Where(w => w.NickID == f.Id).Sum(s => s.OrderCount));
                    //按发送数量排序，较少优先
                    nicks.Sort((a, b) => a.Send.Value.CompareTo(b.Send.Value));

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
                            else
                                send = 0;
                        }

                        var sendorders = orders.Take(send).ToList();
                        orders.RemoveRange(0, send);

                        sendorders.ForEach(a =>
                        {
                            if (SendOrder(nicks[i], a))
                            {
                                dal.UpdateOrderSend(a.Id, nicks[i].Id);
                                dal.UpdateSendCount(nicks[i].Id);
                                dal.AddSendLog(nicks[i].Id, 1);
                                result.Message = string.Format("系统事件{0}将客户分配给销售顾问{1}{2}", DateTime.Now.ToString(), nicks[i].Nick, Environment.NewLine);
                                result.Result = true;
                                SendResult(result);
                            }
                        });
                    }
                }
                else
                {
                    result.Message = "程序运行中，暂时未发现新线索";
                    result.Result = true;
                    SendResult(result);
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

            HAP.HtmlDocument htmlDoc = GetHtml(string.Format(onsalelist, Tool.userInfo_qc.CompanyID));
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
        public int PostNews()
        {
            var doc = GetHtml(news_draft);
            var draft = JsonConvert.DeserializeObject<NewDraft>(doc.DocumentNode.OuterHtml);
            int count = 0;
            foreach(var data in draft.Data)
            {
                var newsinfo = GetNewsInfo(model_TS + data.NewsId.ToString());
                var json = JsonConvert.SerializeObject(newsinfo);
                var postdatas = "promotion=" + HttpHelper.URLEncode(HttpHelper.URLEncode(json)) + "&token=" + token;
                var item = new HttpItem
                {
                    URL = "http://ics.autohome.com.cn/Price/NewsTemplate/SaveNewsTemplateData",
                    Method = "post",
                    Cookie = cookie,
                    Postdata = postdatas,
                    ContentType = "application/x-www-form-urlencoded; charset=UTF-8",
                    Referer = "http://ics.autohome.com.cn/dms/Order/Index",
                    UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)"
                };
                item.Header.Add("X-Requested-With", "XMLHttpRequest");
                item.Allowautoredirect = false;
                var http = new HttpHelper();
                var htmlr = http.GetHtml(item);
                try
                {
                    var result = JsonConvert.DeserializeObject<NewsResult>(htmlr.Html);
                    if (result.NewsId > 0)
                        count++;
                }
                catch(Exception)
                {

                }
            }
            return count;
        }        
        
        private QiCheNewsPostData GetNewsInfo(string newurl)
        {
            var doc = GetHtml(newurl);
            token = doc.GetElementbyId("token").GetAttributeValue("value", "");
            var postdata = new QiCheNewsPostData();
            postdata.TemplateId = doc.GetElementbyId("templateId").GetAttributeValue("value", "");
            int ivalue = 0;
            int.TryParse(doc.GetElementbyId("txtIsMoreThanWarningLine").GetAttributeValue("value", ""), out ivalue);
            postdata.IsMoreThanWarningLine = ivalue;
            postdata.Integrity = 100;
            postdata.IsPublish = 1;
            var series = doc.DocumentNode.SelectSingleNode("//input[@type='radio' and @name='ckSeriesId' and @checked='checked']");
            postdata.SeriesId = series.GetAttributeValue("value", "");
            postdata.SeriesName = Tool.UTF8ToGBK(series.GetAttributeValue("sname", ""));
            postdata.CompanyId = doc.GetElementbyId("companyId").GetAttributeValue("value", "");
            postdata.DealerIds = postdata.CompanyId;
            var IsPriceOff = doc.DocumentNode.SelectSingleNode("//input[@name='IsPriceOff' and @checked='checked']");
            postdata.IsPriceOff = IsPriceOff.GetAttributeValue("value", "");
            postdata.DiscountRate = doc.GetElementbyId("selDiscountRate").GetAttributeValue("value", "0");
            var hasEquipCar = doc.DocumentNode.SelectSingleNode("//input[@name='equipCar' and @checked='checked']");
            postdata.HasEquipCar = hasEquipCar == null ? 1 : 0;
            var hasGiftPackage = doc.DocumentNode.SelectSingleNode("//input[@name='package' and @checked='checked']");
            postdata.HasGiftPackage = hasGiftPackage == null ? 1 : 0;
            ivalue = int.TryParse(doc.GetElementbyId("priceoff").GetAttributeValue("value", ""), out ivalue) ? ivalue : 0;
            postdata.PriceOff = ivalue;
            postdata.LicensePrice = doc.GetElementbyId("LicenseTax").GetAttributeValue("value", "");
            postdata.OtherPrice = doc.GetElementbyId("OtherPrice").GetAttributeValue("value", "");            
            ivalue = int.TryParse(doc.GetElementbyId("txtFactorySubsidyPrice").GetAttributeValue("value", ""), out ivalue) ? ivalue : 0;
            postdata.FactorySubsidyPrice = ivalue * 10000;
            postdata.ExchangeSubsidyPrice = doc.GetElementbyId("txtExchangeSubsidyPrice").GetAttributeValue("value", "");            
            ivalue = int.TryParse(doc.GetElementbyId("txtGovSubsidyPrice").GetAttributeValue("value", ""), out ivalue) ? ivalue : 0;
            postdata.GovSubsidyPrice = + ivalue * 10000;
            ivalue = int.TryParse(doc.GetElementbyId("txtAllowancePrice").GetAttributeValue("value", ""), out ivalue) ? ivalue : 0;
            postdata.AllowancePrice = + ivalue * 10000;
            ivalue = int.TryParse(doc.GetElementbyId("txtInsuranceDiscount").GetAttributeValue("value", ""), out ivalue) ? ivalue : 0;
            postdata.InsuranceDiscount = + ivalue * 10000;
            postdata.PurchaseTax = doc.GetElementbyId("selPurchaseTax").GetAttributeValue("value", "0");
            postdata.CompulsoryInsurance = doc.GetElementbyId("selCompulsoryInsurance").GetAttributeValue("value", "0");
            postdata.CommercialInsurance = doc.GetElementbyId("selCommercialInsurance").GetAttributeValue("value", "0");
            var dealerinsurance = doc.DocumentNode.SelectSingleNode("//input[@id='chkHasDealerInsurance' and @checked='checked']");
            postdata.HasDealerInsurance = dealerinsurance == null ? 0 : 1;
            postdata.StartTime= doc.GetElementbyId("txtstarttime").GetAttributeValue("value", "");
            postdata.EndTime = doc.GetElementbyId("txtendtime").GetAttributeValue("value", "");
            postdata.HasAddress = 1;
            postdata.HasPhone = 1;
            var bullet = doc.DocumentNode.SelectSingleNode("//input[@id='bullet' and @checked='checked']");
            postdata.IsBullet = bullet == null ? 0 : 1;
            var memberfocus = doc.DocumentNode.SelectSingleNode("//input[@id='chk_memberFocus' and @checked='checked']");
            postdata.IsFocus = memberfocus == null ? 0 : 1;            
            postdata.Comment = doc.GetElementbyId("comment").GetAttributeValue("value", "");
            var scripts = doc.DocumentNode.SelectSingleNode("//html/body/script[23]/text()").InnerText.Trim();
            int forcount = 0;
            bool isnews = false;
            foreach (string script in scripts.Split('\n'))
            {
                if (forcount == 5) break;
                if (script.Contains("news_content"))
                {
                    forcount++;
                    postdata.Content = Tool.UTF8ToGBK(script.Trim().Replace("var news_content = \"", "").Replace("\"", "").Replace(";", ""));
                }
                else if (script.Contains("news_conclusion"))
                {
                    forcount++;
                    postdata.Conclusion = Tool.UTF8ToGBK(script.Trim().Replace("var news_conclusion = \"", "").Replace("\"", "").Replace(";", ""));
                }
                else if (script.Contains("GetNewsId") || isnews)
                {
                    if(isnews)
                    {
                        forcount++;
                        postdata.NewsId = script.Trim().Replace("return '", "").Replace("';", "");
                    }
                    isnews = true;
                }
                else if (script.Contains("news_IsAutoTemplate"))
                {
                    forcount++;
                    ivalue = int.TryParse(script.Trim().Replace("var news_IsAutoTemplate = ", "").Replace(";", ""), out ivalue) ? ivalue : 0;
                    postdata.IsAutoTemplate = ivalue;
                }
                else if (script.Contains("news_IsAutoTitle"))
                {
                    forcount++;
                    ivalue = int.TryParse(script.Trim().Replace("var news_IsAutoTitle = ", "").Replace(";", ""), out ivalue) ? ivalue : 0;
                    postdata.IsAutoTitle = ivalue;
                }
            }
            var isrecommend = doc.DocumentNode.SelectSingleNode("//input[@id='chk_isRecommend' and @checked='checked']");
            postdata.IsReCommend = isrecommend == null ? 0 : 1;

            var dealerids = doc.DocumentNode.SelectNodes("//input[@name='associatedStore' and @checked='checked']");
            var vpostdata = "eqids=&dealerids=" + string.Join(",", dealerids.Select(s => s.GetAttributeValue("value", "")).ToList()) + "&seriesId=" + postdata.SeriesId + "&newstemplateid=" + postdata.TemplateId;

            var item = new HttpItem
            {
                URL = "http://ics.autohome.com.cn/Price/NewsTemplate/GetSpecsList",
                Method = "post",
                Cookie = cookie,
                Postdata = vpostdata,
                ContentType = "application/x-www-form-urlencoded; charset=UTF-8",
                Referer = "http://ics.autohome.com.cn/dms/Order/Index",
                UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)"
            };
            item.Header.Add("X-Requested-With", "XMLHttpRequest");
            item.Allowautoredirect = false;
            var http = new HttpHelper();
            var htmlr = http.GetHtml(item);
            postdata.SpecInfos = JsonConvert.DeserializeObject<List<SpecInfos>>(htmlr.Html);
            postdata.SpecInfos.ForEach(i =>
            {
                i.SpecName = Tool.UTF8ToGBK(i.SpecName);
                i.SpecIsImport = Tool.UTF8ToGBK(i.SpecIsImport);                
                if (i.SelectedPrmtConditionList.Count > 0)
                {
                    i.PromotionCondition = "<p>";
                    for (int j = 0; j < i.SelectedPrmtConditionList.Count; j++)
                    {
                        i.SelectedPrmtConditionList[j].Name = Tool.UTF8ToGBK(i.SelectedPrmtConditionList[j].Name);
                        if (j != 0)
                        {
                            i.PromotionCondition += "<span>|</span>";
                        }
                        i.PromotionCondition += "<span class='nfomationissuepic-article-txt'>" + i.SelectedPrmtConditionList[j].Name + "</span>";
                    }
                }
            });
            vpostdata = "action=0&seriesid=" + postdata.SeriesId + "&packageId=" + postdata.HasGiftPackage + "&priceOff=" + postdata.PriceOff + "&isPriceOff=" + postdata.IsPriceOff + "&isRecommend=" + postdata.IsReCommend + "&price=" + postdata.SpecInfos.Min(s => s.CarPrice) + "&checkRepeate=" + postdata.IsReCommend + "&DealerIds%5B%5D=" + postdata.DealerIds;

            item.URL = "http://ics.autohome.com.cn/Price/NewsTemplate/GetTemplateTitle";
            item.Postdata = vpostdata;
            htmlr = http.GetHtml(item);
            List<TemplateTitle> templatetitles = JsonConvert.DeserializeObject<List<TemplateTitle>>(htmlr.Html);
            postdata.Titles = new List<Title>();
            foreach (var i in templatetitles)
            {
                postdata.Titles.Add(new Title { TitleText = Tool.UTF8ToGBK(i.Title), TitleValue = i.Id, DefaultSubTitle = Tool.UTF8ToGBK(i.DefaultSubTitle) });
            }
            postdata.TitleId = postdata.Titles[0].TitleValue.ToString();
            postdata.Title = postdata.Titles[0].TitleText;
            postdata.SubTitle = postdata.Titles[0].DefaultSubTitle;
            postdata.Packages = new List<Package>();
            scripts = doc.DocumentNode.SelectSingleNode("//*[@id='div_section2']/script/text()").InnerText.Trim();
            forcount = 0;
            foreach (string script in scripts.Split('\n'))
            {
                if (forcount == 5) break;

                if (script.Contains("BigPicJson"))
                {
                    forcount++;
                    postdata.BigNewsTemplateImage = JsonConvert.DeserializeObject<BigNewsTemplateImage>(script.Trim().Replace("var  BigPicJson=", "").Replace(";", ""));
                    postdata.BigNewsTemplateImage.ImageUrl = postdata.BigNewsTemplateImage.ImageUrl.Insert(postdata.BigNewsTemplateImage.ImageUrl.LastIndexOf("/") + 1, "s_");
                    postdata.BigNewsTemplateImage.SmallImageUrl = postdata.BigNewsTemplateImage.ImageUrl;
                }
                else if (script.Contains("SmallPicJson"))
                {
                    forcount++;
                    postdata.NewsTemplateImages = JsonConvert.DeserializeObject<List<BigNewsTemplateImage>>(script.Trim().Replace("var SmallPicJson=", "").Replace(";", ""));
                    postdata.NewsTemplateImages.ForEach(i => {
                        i.ImageUrl = i.ImageUrl.Insert(i.ImageUrl.LastIndexOf("/") + 1, "s_");
                        i.SmallImageUrl = i.ImageUrl;
                    });
                }
                else if(script.Contains("FocusPicJson"))
                {
                    forcount++;
                    postdata.FocusImage = JsonConvert.DeserializeObject<BigNewsTemplateImage>(script.Trim().Replace("var FocusPicJson=", "").Replace(";", ""));
                    postdata.FocusImage.ImageUrl = postdata.FocusImage.ImageUrl.Insert(postdata.FocusImage.ImageUrl.LastIndexOf("/") + 1, "s_");
                    postdata.FocusImage.SmallImageUrl = postdata.FocusImage.ImageUrl;
                }
                else if (script.Contains("MaintainEngineJson"))
                {
                    forcount++;
                    postdata.MaintainEngines = new List<MaintainEngine>();
                    postdata.MaintainEngines.Add(JsonConvert.DeserializeObject<MaintainEngine>(script.Trim().Replace("var MaintainEngineJson=", "").Replace(";", "")));
                    postdata.MaintainEngines.ForEach(i => {
                        i.DealerId = i.companyId;
                        i.WarrantKm = i.warrantyKm.ToString();
                        i.InsuranceCompany = Tool.UTF8ToGBK(i.InsuranceCompany);
                        i.FinanceCompany = Tool.UTF8ToGBK(i.FinanceCompany);
                    });
                }
                else if(script.Contains("SeriesColor"))
                {
                    forcount++;
                    postdata.SeriesColor = JsonConvert.DeserializeObject<List<SeriesColor>>(script.Trim().Replace("var SeriesColor = ", "").Replace(";", ""));
                    postdata.SeriesColor.ForEach(i => i.name = Tool.UTF8ToGBK(i.name));
                }
            }
            postdata.BigImageTitle = "";
            postdata.PackAgeIds = "";
            postdata.EquipCarIds = "";
            //item = new HttpItem
            //{
            //    URL = "http://ics.autohome.com.cn/Price/EquipmentCar/GetEquipCar_Packgae_PackageDetails",
            //    Method = "post",
            //    Cookie = cookie,
            //    Postdata = vpostdata,
            //    ContentType = "application/x-www-form-urlencoded; charset=UTF-8",
            //    Referer = "http://ics.autohome.com.cn/dms/Order/Index",
            //    UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)"
            //};
            //item.Header.Add("X-Requested-With", "XMLHttpRequest");
            //item.Allowautoredirect = false;
            //http = new HttpHelper();
            //htmlr = http.GetHtml(item);
            //postdata.EquipCars = JsonConvert.DeserializeObject<List<EquipCar>>(htmlr.Html);
            postdata.EquipCars = new List<EquipCar>();
            postdata.Gifts = new List<Gift>();
            postdata.DealerList = new List<string>() { postdata.DealerIds };
            postdata.EquptCarSpecIds = new List<EquptCarSpecId>();

            var prmtconditions = doc.DocumentNode.SelectNodes("//input[@name='prmtCondition']");
            postdata.NewsPromotionConditionsList = new List<SelectedPrmtCondition>();
            postdata.SelectedPrmtConditionList = new List<SelectedPrmtCondition>();
            foreach (HAP.HtmlNode node in prmtconditions)
            {
                postdata.NewsPromotionConditionsList.Add(new SelectedPrmtCondition { Name = Tool.UTF8ToGBK(node.GetAttributeValue("conditionname", "")), Id = Convert.ToInt32(node.GetAttributeValue("value", "0")) });
                if(node.GetAttributeValue("checked", "") == "checked")
                {
                    postdata.SelectedPrmtConditionList.Add(new SelectedPrmtCondition { Name = Tool.UTF8ToGBK(node.GetAttributeValue("conditionname", "")), Id = Convert.ToInt32(node.GetAttributeValue("value", "0")) });
                }
            }
            return postdata;
        }
        #endregion
    }

    public class QiCheNewsPostData
    {
        public string TemplateId { get; set; }
        public int IsMoreThanWarningLine { get; set; }
        public int Integrity { get; set; }
        public int IsPublish { get; set; }
        public string NewsId { get; set; }
        public string SeriesId { get; set; }
        public string SeriesName { get; set; }
        public string CompanyId { get; set; }
        public string IsPriceOff { get; set; }
        public string DiscountRate { get; set; }
        public int HasEquipCar { get; set; }
        public int HasGiftPackage { get; set; }
        public int PriceOff { get; set; }
        public string LicensePrice { get; set; }
        public string OtherPrice { get; set; }
        public int FactorySubsidyPrice { get; set; }
        public string ExchangeSubsidyPrice { get; set; }
        public int GovSubsidyPrice { get; set; }
        public int AllowancePrice { get; set; }
        public int InsuranceDiscount { get; set; }
        public string PurchaseTax { get; set; }
        public string CompulsoryInsurance { get; set; }
        public string CommercialInsurance { get; set; }
        public int HasDealerInsurance { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int HasAddress { get; set; }
        public int HasPhone { get; set; }
        public int IsBullet { get; set; }
        public int IsFocus { get; set; }
        public string Title { get; set; }
        public string TitleId { get; set; }
        public string SubTitle { get; set; }
        public string Content { get; set; }
        public string Comment { get; set; }
        public string Conclusion { get; set; }
        public int IsReCommend { get; set; }
        public List<SpecInfos> SpecInfos { get; set; }
        public List<Title> Titles { get; set; }
        public List<Package> Packages { get; set; }
        public BigNewsTemplateImage BigNewsTemplateImage { get; set; }
        public string BigImageTitle { get; set; }
        public List<BigNewsTemplateImage> NewsTemplateImages { get; set; }
        public BigNewsTemplateImage FocusImage { get; set; }
        public List<MaintainEngine> MaintainEngines { get; set; }
        public List<SeriesColor> SeriesColor { get; set; }
        public string DealerIds { get; set; }
        public string PackAgeIds { get; set; }
        public string EquipCarIds { get; set; }
        public List<EquipCar> EquipCars { get; set; }
        public List<Gift> Gifts { get; set; }
        public List<string> DealerList { get; set; }
        public List<EquptCarSpecId> EquptCarSpecIds { get; set; }
        public int IsAutoTemplate { get; set; }
        public int IsAutoTitle { get; set; }
        public List<SelectedPrmtCondition> SelectedPrmtConditionList { get; set; }
        public List<SelectedPrmtCondition> NewsPromotionConditionsList { get; set; }
    }

    public class SpecInfos
    {
        public int Id { get; set; }
        public int SpecId { get; set; }
        public string SpecName { get; set; }
        public int OriginalPrice { get; set; }
        public decimal ForbidLine { get; set; }
        public decimal WarningLine { get; set; }
        public int WarningLinePrice { get; set; }
        public int ForbidLinePrice { get; set; }
        public string ForbidLinePriceTxt { get { return Math.Round(ForbidLinePrice / 10000.00, 2).ToString("0.00"); } }
        public int Price { get; set; }
        public string IsPriceOff { get; set; }
        public int PriceOff { get; set; }
        public string PriceOffTxt { get { return Math.Round(PriceOff / 10000.00, 2).ToString("0.00"); } }
        public int IsEquipCar { get; set; }
        public int EquipCarId { get; set; }
        public int HasAllowance { get; set; }
        public int HasCoupon { get; set; }
        public string InventoryColor { get; set; }
        public string GiftPackageCode { get { return ""; } }
        public int SeriesId { get; set; }
        public int Status { get; set; }
        public int IsDel { get; set; }
        public int FactorySubsidyPrice { get; set; }
        public string FactorySubsidyPriceTxt { get { return Math.Round(FactorySubsidyPrice / 10000.00, 2).ToString("0.00"); } }
        public int ExchangeSubsidyPrice { get; set; }
        public int GovSubsidyPrice { get; set; }
        public string GovSubsidyPriceTxt { get { return Math.Round(GovSubsidyPrice / 10000.00, 2).ToString("0.00"); } }
        public int AllowancePrice { get; set; }
        public string AllowancePriceTxt { get { return Math.Round(AllowancePrice / 10000.00, 2).ToString("0.00"); } }
        public string PurchaseTax { get; set; }
        public string CompulsoryInsurance { get; set; }
        public string CommercialInsurance { get; set; }
        public int InsuranceDiscount { get; set; }
        public string InsuranceDiscountValue { get { return Math.Round(InsuranceDiscount / 10000.00, 2).ToString("0.00"); } }
        public int VehicleTaxPrice { get; set; }
        public int LicenseTaxPrice { get; set; }
        public int OtherPrice { get; set; }
        public int CompulsoryInsurancePrice { get; set; }
        public int CommercialInsurancePrice { get; set; }
        public int PurchaseTaxPrice { get; set; }
        public decimal SpecDisplacement { get; set; }
        public string SpecStructureseat { get; set; }
        public string SpecIsImport { get; set; }
        public int YearId { get; set; }
        private string Colors { get; set; }
        public string PromotionCondition { get; set; }
        public List<SelectedPrmtCondition> SelectedPrmtConditionList { get; set; }
        public decimal ForbidLinePriceInTenThousand { get; set; }
        public decimal AbsForbidLinePriceInTenThousand { get; set; }
        public decimal WarningLinePriceInTenThousand { get; set; }
        public decimal AbsWarningLinePriceInTenThousand { get; set; }
        public string PurchaseTaxTxt {
            get {
                if (PurchaseTax == "50")
                    return Tool.UTF8ToGBK("购置税:赠送50%");
                if (PurchaseTax == "100")
                    return Tool.UTF8ToGBK("购置税:赠送100%");
                return Tool.UTF8ToGBK("不赠送购置税");
            }
        }
        public string CompulsoryInsuranceTxt {
            get {
                if (CompulsoryInsurance == "0")
                    return Tool.UTF8ToGBK("不赠送交强险");
                return Tool.UTF8ToGBK("交强险:赠送" + CompulsoryInsurance + "年");
            }
        }
        public string CommercialInsuranceTxt {
            get
            {
                if (CommercialInsurance == "0")
                    return Tool.UTF8ToGBK("不赠送商业险");
                return Tool.UTF8ToGBK("商业险:赠送" + CommercialInsurance + "年");
            }
        }        
        public string InsuranceDiscountTxt {
            get
            {
                if (InsuranceDiscount == 0)
                    return Tool.UTF8ToGBK("无保险优惠");
                return Tool.UTF8ToGBK("保险优惠:" + InsuranceDiscountValue + "万元");
            }
        }
        public int CarPrice {
            get {
                if (IsPriceOff == "1")
                {
                    //self.Price(self.OriginalPrice() - self.PriceOff());
                    return OriginalPrice - PriceOff;
                }
                else
                {
                    //self.Price(self.OriginalPrice() + self.PriceOff());
                    return OriginalPrice + PriceOff;
                }
            }
        }
        public int DealPrice {
            get {
                var dealPrice = CarPrice + PurchaseTaxPrice + CompulsoryInsurancePrice + CommercialInsurancePrice + VehicleTaxPrice - InsuranceDiscount + LicenseTaxPrice + OtherPrice;
                return dealPrice;
            }
        }

        private bool value = true;

        public bool EditPriceOff { get { return value; } }
        public bool EditInsurance { get { return value; } }
        public bool EditOther { get { return value; } }
        public bool EditColor { get { return value; } }
        public bool EditPromotionCondition { get { return value; } }
    }
    public class SelectedPrmtCondition
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class Title
    {
        public string TitleText { get; set; }
        public int TitleValue { get; set; }
        public string DefaultSubTitle { get; set; }
    }

    public class TemplateTitle
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleTemplate { get; set; }
        public string ShowPriceType { get; set; }
        public int SubShowPriceType { get; set; }
        public string SubTitleTemplate { get; set; }
        public int IsPriceOff { get; set; }
        public int IsHaveTestDrive { get; set; }
        public int IsHaveGift { get; set; }
        public int IsHaveCycle { get; set; }
        public int IsHaveYear { get; set; }
        public int IsRecomand { get; set; }
        public string DefaultSubTitle { get; set; }
    }

    public class Package
    {

    }

    public class BigNewsTemplateImage
    {
        public int Id { get; set; }
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }
        public string SmallImageUrl { get; set; }
        public int ImageType { get; set; }
        public int OrderId { get; set; }
        public int IsDel { get; set; }
    }

    public class MaintainEngine
    {
        public int Id { get; set; }
        public int DealerId { get; set; }
        public int companyId { get; set; }
        public int SeriesId { get; set; }
        public string WarrantyYear { get; set; }
        public string WarrantKm { get; set; }
        public int warrantyKm { get; set; }
        public string DealerWarrantyKm { get; set; }
        public string EngineOil { get; set; }
        public string EngineOil3 { get; set; }
        public string InsuranceCompany { get; set; }
        public int InsurancePrice { get; set; }
        public string FinanceCompany { get; set; }
        public int LoanPayment { get; set; }
        public int LoanMonth { get; set; }
        public int LoanMonthPrice { get; set; }
        public string CreateTime { get; set; }
        public string LastTime { get; set; }
        public int IsDel { get; set; }
        public int IsShowInfo { get; set; }
    }

    public class SeriesColor
    {
        public int id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public int picnum { get; set; }
        public int clubpicnum { get; set; }
    }

    public class EquipCar
    {

    }

    public class Gift
    {

    }

    public class EquptCarSpecId
    {

    }

    public class NewsResult
    {
        public int ReturnCode { get; set; }
        public string ErrorMessage { get; set; }
        public int NewsId { get; set; }
        public int TemplateId { get; set; }
        public int BulletinCode { get; set; }
        public int FocusImageCode { get; set; }
    }

    public class NewDraft
    {
        public int Result { get; set; }
        public bool Reload { get; set; }
        public string Message { get; set; }
        public List<NewListDTP> Data { get; set; }
        public int RecordCount { get; set; }
    }

    public class NewListDTP
    {
        public int NewsId { get; set; }
        public int RecommendedState { get; set; }
        public int IsBullet { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int CompanyId { get; set; }
        public string Title { get; set; }
        public int ArticleId { get; set; }
        public int NewsTemplateID { get; set; }
        public int IsPublish { get; set; }
        public int IsIITemplate { get; set; }
        public int HasEquipCar { get; set; }
        public int HasUpdate { get; set; }
        public string UpdateMsg { get; set; }
        public int HasDelete { get; set; }
        public string DealerIds { get; set; }
        public List<string> DealerNames { get; set; }
        public int IsPosition { get; set; }
        public string PositionUrl { get; set; }
        public int IsShowTemplate { get; set; }
        public int IncludeTel { get; set; }
        public int IncludeAddress { get; set; }
        public int Integrity { get; set; }
        public int IsDel { get; set; }
        public int GroupNewsId { get; set; }
        public int MaxSelfDefinedCount { get; set; }
        public int MaxRelationDealerCount { get; set; }
        public int IsMoreThanWarningLine { get; set; }
        public int IsNeedRecommended { get; set; }        
    }
}
