using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HtmlAgilityPack;
using CsharpHttpHelper;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Aide
{
    public partial class Form_YC : Form
    {
        YiChe yc;
        DAL dal = new DAL();
        CarNews carnews;
        List<TextValue> promotionType = new List<TextValue>();
        HtmlAgilityPack.HtmlDocument doc;
        string url = "";
        string rdoStoreState = "";
        string carid = "";
        string cbid = "";
        string energytype = "";
        string csshowname = "";
        PromotionCars cars;
        int TemplaceNewsType = 1;
        string NewsType = "";
        string CarType = "";
        List<TextValue> BusinessTax = new List<TextValue>();
        List<TextValue> TrafficTax = new List<TextValue>();
        string ImageUpload = "";
        List<Merchandise> merchandise = new List<Merchandise>();
        List<TextValue> Colors = new List<TextValue>();
        int newsid = 0;

        public Form_YC(YiChe yc, int newsid = 0)
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            
            this.yc = yc;
            promotionType.AddRange(new[] { new TextValue { Text = "优惠金额", Value = "0" }, new TextValue { Text = "优惠折扣率", Value = "1" } });

            if (newsid > 0)
            {
                this.newsid = newsid;
                var news = dal.GetNews(newsid);
                var content = OperateIniFile.ReadIniData("Content", news.ID.ToString());
                carnews = JsonConvert.DeserializeObject<CarNews>(content);
                groupBox2.Enabled = false;
                gpbCarList.Enabled = false;
                if (carnews.IsDetail)
                    tabControl2.SelectedTab = tabPage2;
            }
            
            doc = yc.InforManagerNews("http://das.app.easypass.cn/InforManage/News/NewsList.aspx");
            if(!doc.DocumentNode.InnerText.Contains("新能源车"))
            {
                radioButton5.Visible = radioButton4.Visible = false;
            }

            NewsType = rbtSource1.Name;
            if (carnews == null)
                InitForm(rbtSource1.Tag.ToString());
            else
            {
                var fieldinfo = this.GetType().GetField(carnews.NewsType, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase);
                if(fieldinfo != null)
                {
                    var o = fieldinfo.GetValue(this);
                    NewsType = ((RadioButton)o).Name;
                    ((RadioButton)o).Checked = true;
                    //InitForm(((RadioButton)o).Tag.ToString());
                }
            }
        }

        void Source_CheckedChanged(object sender, EventArgs e)
        {
            var rbt = ((RadioButton)sender);
            if (!rbt.Checked) return;
            NewsType = rbt.Name;
            if(rbt.Text == "普通新闻")
            {
                pCX.Visible = false;
                gbPTNews.Visible = true;
                gbPTNews.Location = pCX.Location;
                InitPTNews(rbt.Tag.ToString());
            }
            else
            {
                gbPTNews.Visible = false;
                pCX.Visible = true;
                if (rbt.Text.Contains("降价"))
                    TemplaceNewsType = 1;
                else if (rbt.Text.Contains("置换"))
                    TemplaceNewsType = 2;
                else if (rbt.Text.Contains("加价"))
                    TemplaceNewsType = 3;
                InitForm(rbt.Tag.ToString());
            }
        }

        private void InitPTNews(string url)
        {
            this.url = url;
            doc = yc.InforManagerNews(url);

            List<TextValue> newsClass = new List<TextValue>();
            newsClass.AddRange(new TextValue[]{
                new TextValue{ Text = "请选择新闻分类", Value = "0" },
                new TextValue{ Text = "企业新闻", Value = "1" },
                new TextValue{ Text = "优惠促销", Value = "2" },
                new TextValue{ Text = "车友活动", Value = "3" },
                new TextValue{ Text = "新车到店", Value = "4" },
                new TextValue{ Text = "维修保养", Value = "5" },
                new TextValue{ Text = "置换信息", Value = "6" },
            });
            ddlNewsClass.DataSource = newsClass;
            ddlNewsClass.DisplayMember = "Text";
            ddlNewsClass.ValueMember = "Value";
            ddlNewsClass.SelectedIndex = 0;


            var divBrand = doc.GetElementbyId("brandSelect");
            var brands = divBrand.SelectNodes(".//div/div/div/ul/li");
            #region 相关车型
            int xstep = 143;
            int ystep = 22;
            int xstart = 9;
            int ystart = 8;
            this.tabPage3.Controls.Clear();            
            for (int i = 0; i < brands.Count; i++)
            {
                var text = brands[i].GetAttributeValue("title", "");
                var value = brands[i].SelectSingleNode(".//input").GetAttributeValue("value", "");
                int x = xstart;
                int y = ystart;
                if (i > 0)
                {
                    x = xstart + (xstep * (i % 4));
                    y = ystart + (ystep * (i / 4));
                }
                var chk = new CheckBox();
                chk.Location = new System.Drawing.Point(x, y);
                chk.Name = "chk" + value;
                chk.Tag = value;
                chk.Text = text;
                chk.AutoSize = true;
                chk.Size = new System.Drawing.Size(72, 16);
                chk.TabStop = true;
                chk.UseVisualStyleBackColor = true;
                if (carnews != null && carnews.PTNews.Brands.Contains(value))
                    chk.Checked = true;
                this.tabPage3.Controls.Add(chk);
            }
            #endregion

            var divInvest = doc.GetElementbyId("investSelect");
            var invests = divInvest.SelectNodes(".//div/div/div/div/ul/li");
            if (carnews != null && carnews.PTNews.SelectVote.VoteType == "1")
                rbtVoteType1.Checked = true;
            else
                rbtVoteType2.Checked = true;
            #region 相关调查
            xstep = 103;
            ystep = 22;
            xstart = 11;
            ystart = 7;
            this.pVote.Controls.Clear();
            for (int i = 0; i < invests.Count; i++)
            {
                var text = invests[i].SelectSingleNode(".//label").InnerText;
                var value = invests[i].SelectSingleNode(".//input").GetAttributeValue("value", "");
                int x = xstart;
                int y = ystart;
                if (i > 0)
                {
                    y = ystart + (ystep * i);
                }
                var rbt = new RadioButton();
                rbt.Location = new System.Drawing.Point(x, y);
                rbt.Name = "rbt" + value;
                rbt.Tag = value;
                rbt.Text = text;
                rbt.AutoSize = true;
                rbt.Size = new System.Drawing.Size(95, 16);
                rbt.TabStop = true;
                rbt.UseVisualStyleBackColor = true;
                if (carnews != null && carnews.PTNews.SelectVote.VoteIndex == value)
                    rbt.Checked = true;
                this.pVote.Controls.Add(rbt);
            }
            #endregion

            var divBuyCar = doc.GetElementbyId("buycarSlect");
            var carlist = divBuyCar.SelectNodes(".//div/div/div/ul/li");
            #region 在线购车
            xstep = 143;
            ystep = 22;
            xstart = 9;
            ystart = 8;
            this.tabPage5.Controls.Clear();
            for (int i = 0; i < carlist.Count; i++)
            {
                var text = carlist[i].GetAttributeValue("title", "");
                var value = carlist[i].SelectSingleNode(".//input").GetAttributeValue("value", "");
                int x = xstart;
                int y = ystart;
                if (i > 0)
                {
                    x = xstart + (xstep * (i % 4));
                    y = ystart + (ystep * (i / 4));
                }
                var chk = new CheckBox();
                chk.Location = new System.Drawing.Point(x, y);
                chk.Name = "chk" + value;
                chk.Tag = value;
                chk.Text = text;
                chk.AutoSize = true;
                chk.Size = new System.Drawing.Size(72, 16);
                chk.TabStop = true;
                chk.UseVisualStyleBackColor = true;
                if (carnews != null && carnews.PTNews.BuyCar.Contains(value))
                    chk.Checked = true;
                this.tabPage5.Controls.Add(chk);
            }
            #endregion

            if(carnews != null)
            {
                txtPTTitle.Text = carnews.PTNews.Title;
                ddlNewsClass.SelectedValue = carnews.PTNews.Type;
                txtDesc.Text = carnews.PTNews.Content;
                chkAddress.Checked = carnews.PTNews.Address;
                chkMap.Checked = carnews.PTNews.Map;
                chkTel.Checked = carnews.PTNews.Tel;
            }
        }

        private void InitForm(string url)
        {            
            this.url = url;
            doc = yc.InforManagerNews(url);
            #region 车型列表
            var carList = doc.DocumentNode.SelectNodes("//span[@id='radlst']/span/label");
            int xstep = 113;
            int ystep = 22;
            int xstart = 16;
            int ystart = 22;
            RadioButton rbtChk = null;
            gpbCarList.Controls.Clear();
            for (int i = 0; i < carList.Count; i++)
            {
                var value = carList[i].GetAttributeValue("for", "");
                var title = carList[i].GetAttributeValue("title", "");
                var csshowname = carList[i].GetAttributeValue("csshowname", "");
                var input = doc.GetElementbyId(value);
                var cbid = input.GetAttributeValue("cbid", "");
                var energytype = input.GetAttributeValue("energytype", "");
                int x = xstart + (xstep * i);
                int y = ystart;

                if (i > 5)
                {
                    x = xstart + (xstep * (i - 6));
                    y = ystart + ystep;
                }

                var rbt = new RadioButton();
                rbt.Location = new System.Drawing.Point(x, y);
                rbt.Name = "rbt" + value;
                rbt.Tag = value + ":" + cbid + ":" + energytype + ":" + csshowname;
                rbt.Text = title;
                rbt.CheckedChanged += Car_CheckedChanged;
                rbt.AutoSize = true;
                rbt.Size = new System.Drawing.Size(101, 16);
                rbt.TabStop = true;
                rbt.UseVisualStyleBackColor = true;
                gpbCarList.Controls.Add(rbt);
                if (carnews != null && carnews.CarType == rbt.Name)
                    rbtChk = rbt;
            }
            #endregion
            var datetimeend = doc.GetElementbyId("txtDateTimeEnd");
            if (datetimeend != null)
                dtpPromotionB.Value = Convert.ToDateTime(datetimeend.GetAttributeValue("value", DateTime.Now.AddMonths(1).AddDays(1).ToString()));
            else
                dtpPromotionB.Value = DateTime.Now.AddMonths(1).AddDays(1);
            ddlPromotionType.DataSource = promotionType;
            ddlPromotionType.DisplayMember = "Text";
            ddlPromotionType.ValueMember = "Value";
            if (carnews != null && !string.IsNullOrEmpty(carnews.PromotionType))
            {
                ddlPromotionType.SelectedValue = carnews.PromotionType;
                txtMoney.Text = carnews.PromotionValue;
            }
            else
                ddlPromotionType.SelectedIndex = 0;

            #region 库存状态
            var stateList = doc.GetElementbyId("rdoStoreState");
            var stateInputs = stateList.SelectNodes(".//input");
            xstep = 113;
            xstart = 4;
            ystart = 7;
            RadioButton rbtStoreChk = null;
            pStoreState.Controls.Clear();
            for (int i = 0; i < stateInputs.Count; i++)
            {
                var value = stateInputs[i].GetAttributeValue("value", "");
                var vchecked = stateInputs[i].GetAttributeValue("checked", "");
                var label = stateList.SelectSingleNode(".//label[@for='" + stateInputs[i].Id + "']");
                var title = label.InnerText;
                int x = xstart + (xstep * i);
                int y = ystart;

                var rbt = new RadioButton();
                rbt.Location = new System.Drawing.Point(x, y);
                rbt.Name = stateInputs[i].Id;
                rbt.Tag = value;
                rbt.Text = title;

                rbt.CheckedChanged += StoreState_CheckedChanged;                
                rbt.Checked = vchecked == "checked";
                rbt.AutoSize = true;
                rbt.Size = new System.Drawing.Size(101, 16);
                rbt.TabStop = true;
                rbt.UseVisualStyleBackColor = true;
                pStoreState.Controls.Add(rbt);

                if (carnews != null && carnews.StoreState == value)
                    rbtStoreChk = rbt;
            }            
            #endregion

            var uploadfile = doc.GetElementbyId("imgUploadChangeifrUpLoadFile");
            if (uploadfile != null)
            {
                ImageUpload = uploadfile.GetAttributeValue("src", "");
            }

            #region 礼包 汽车用品
            var dgvMerchandise = doc.GetElementbyId("dgvMerchandise");
            var merchandiseTR = dgvMerchandise.SelectNodes(".//tr");

            for (int i = 1; i < merchandiseTR.Count; i++)
            {
                var tds = merchandiseTR[i].SelectNodes(".//td");
                var id = tds[1].InnerText;
                var name = tds[2].InnerText;
                var Class = tds[3].InnerText;
                var price = tds[4].InnerText;

                merchandise.Add(new Merchandise { id = id, name = name, Class = Class, Price = price });
            }
            #endregion

            var article = doc.GetElementbyId("title_article");
            var number = doc.GetElementbyId("title_number");
            if (carnews != null && !string.IsNullOrWhiteSpace(carnews.Title))
            {
                title_article.Text = carnews.Title;
                title_number.Text = carnews.title_number;
            }
            else
            {
                title_article.Text = article.GetAttributeValue("backvalue", "");
                title_number.Text = number.InnerText;
            }
            var lead = doc.GetElementbyId("txtLead");
            if (carnews != null && !string.IsNullOrWhiteSpace(carnews.Lead))
            {
                txtLead.Text = carnews.Lead;
            }
            else if (lead != null)
            {
                txtLead.Text = lead.GetAttributeValue("backvalue", "");
            }

            if (rbtStoreChk != null) rbtStoreChk.Checked = true;
            if (rbtChk != null) rbtChk.Checked = true;
        }

        private void InitDetail()
        {
            #region 颜色列表
            var colorList = doc.DocumentNode.SelectNodes("//div[@id='UpdatePanel7']/span/label");
            int xstep = 152;
            int ystep = 22;
            int xstart = 8;
            int ystart = 30;
            this.pColor.Controls.Clear();
            this.pColor.Controls.Add(chkAllColor);
            for (int i = 0; i < colorList.Count; i++)
            {
                var value = colorList[i].GetAttributeValue("for", "");
                var title = colorList[i].GetAttributeValue("title", "");
                Colors.Add(new TextValue { Text = title, Value = value });

                int x = xstart;
                int y = ystart;

                if (i > 0)
                {
                    x = xstart + (xstep * (i % 4));
                    y = ystart + (ystep * (i / 4));
                }

                var chk = new CheckBox();
                chk.Location = new System.Drawing.Point(x, y);
                chk.Name = "chk" + value;
                chk.Tag = value;
                chk.Text = title;
                chk.AutoSize = true;
                chk.Size = new System.Drawing.Size(72, 16);
                chk.TabStop = true;
                chk.UseVisualStyleBackColor = true;
                if (carnews != null && carnews.Colors.Contains(value))
                    chk.Checked = true;
                this.pColor.Controls.Add(chk);
            }
            if (carnews != null && carnews.Colors.Contains("All"))
            {
                chkAllColor_CheckedChanged(null, new EventArgs());
            }
            #endregion

            #region 车型
            cars = new PromotionCars();
            var yeartype = doc.DocumentNode.SelectNodes("//input[@name='chklYearType']");
            yeartype.ToList().ForEach(f => {
                var text = f.GetAttributeValue("value", "");
                var year = new YearType{ Text = text, IsChecked = true};
                if(carnews != null)
                {
                    var yearA = carnews.YearType.Find(w => w.Text == text);
                    if (yearA != null)
                        year.IsChecked = yearA.IsChecked;
                }
                cars.YearType.Add(year);
            });

            var cartrs = doc.DocumentNode.SelectNodes("//tbody[@id='listInfo']/tr");
            foreach (HtmlNode node in cartrs)
            {
                var carinfotr = node.GetAttributeValue("carinfotr", "");
                if (!string.IsNullOrWhiteSpace(carinfotr))
                {
                    var tds = node.SelectNodes(".//td");
                    var typeinput = tds[0].SelectSingleNode(".//input[@type='checkbox']");
                    var inputCarInfo = node.SelectSingleNode(".//input[@carinfo='carInfo']");
                    var inputrate = node.SelectSingleNode(".//input[@rate='rate']");
                    var inputallowance = node.SelectSingleNode(".//input[@allowance='allowance']");
                    var inputprice = node.SelectSingleNode(".//input[@price='price']");
                    var statesubsidies = node.GetAttributeValue("statesubsidies", "0");
                    var localsubsidies = node.GetAttributeValue("localsubsidies", "0");
                    var colorcount = node.SelectSingleNode(".//td[@class='pick_color']/div/p/span");
                    var carid = Convert.ToInt32(inputCarInfo.GetAttributeValue("carid", ""));
                    var pushedcount = node.SelectSingleNode(".//td[contains(@class, 't_c')]");
                    Car item = null;
                    if (carnews != null)
                        item = carnews.CarList.Find(w => w.CarID == carid);
                    cars.Cars.Add(new Car
                    {
                        IsUp = TemplaceNewsType == 3,
                        CarID = carid,
                        Discount = Convert.ToDecimal(inputrate.GetAttributeValue("value", "0")),
                        IsAllowance = inputallowance != null ? inputallowance.GetAttributeValue("checked", "") : "",
                        IsCheck = item == null? true : item.IsCheck,
                        TypeName = tds[0].GetAttributeValue("title", ""),
                        YearType = typeinput.GetAttributeValue("yeartype", ""),
                        IsNewEnergy = Convert.ToInt32(node.GetAttributeValue("isnewenergy", "0")),
                        Subsidies = tds[4].InnerText.Trim(),
                        StoreState = item == null ? "1" : item.StoreState,
                        CarReferPrice = Convert.ToDecimal(inputCarInfo.GetAttributeValue("carreferprice", "0")),
                        FavorablePrice = item == null ? Convert.ToDecimal(inputprice.GetAttributeValue("value", "0")) : item.FavorablePrice,
                        ColorName = item == null ? colorcount.InnerText : item.ColorName,
                        PushedCount = pushedcount == null ? "0" : pushedcount.InnerText.Trim(),
                        StateSubsidies = statesubsidies != "--" ? string.Format("{0:0.00}", Convert.ToDecimal(statesubsidies)) : "0",
                        LocalSubsidies = localsubsidies != "--" ? string.Format("{0:0.00}", Convert.ToDecimal(localsubsidies)) : "0",
                    });
                }
            }
            var note = doc.GetElementbyId("LimitCarListNote");
            cars.Note = note.InnerText.Trim().Split('\r')[0];

            var publistCarList = doc.DocumentNode.SelectNodes("//div[@id='LimitPublishCarList']/table/tbody/tr");
            foreach (HtmlNode node in publistCarList)
            {
                var tr = node.GetAttributeValue("mark", "");
                if (!string.IsNullOrWhiteSpace(tr))
                {
                    var tds = node.SelectNodes(".//td");
                    var typeinput = tds[0].SelectSingleNode(".//input[@type='checkbox']");
                    var inputCarInfo = node.SelectSingleNode(".//input[@carinfo='carInfo']");
                    var a = tds[1].SelectSingleNode(".//a");

                    cars.PublishCarList.Add(new Car
                    {
                        TypeName = tds[0].InnerText.Trim(),
                        CarID = Convert.ToInt32(typeinput.GetAttributeValue("value", "")),
                        CarReferPrice = Convert.ToDecimal(inputCarInfo.GetAttributeValue("carreferprice", "0")),
                        PushedCount = a.InnerText.Trim(),
                        StoreState = "1",
                        IsAllowance = "false"
                    });
                }
            }
            #endregion

            #region 图片
            if (carnews == null)
            {
                var imgLogo = doc.GetElementbyId("imgLogo");
                imgLogo_hdf.ImgUrl = imgLogo.GetAttributeValue("src", "");
                imgLogo_hdf.CSID = carid;
                var imgPosition1 = doc.GetElementbyId("imgPosition1");
                imgPosition1_hdf.ImgUrl = imgPosition1.GetAttributeValue("src", "");
                imgPosition1_hdf.CSID = carid;
                var imgPosition2 = doc.GetElementbyId("imgPosition2");
                imgPosition2_hdf.ImgUrl = imgPosition2.GetAttributeValue("src", "");
                imgPosition2_hdf.CSID = carid;
                var imgPosition3 = doc.GetElementbyId("imgPosition3");
                imgPosition3_hdf.ImgUrl = imgPosition3.GetAttributeValue("src", "");
                imgPosition3_hdf.CSID = carid;
                var imgPosition4 = doc.GetElementbyId("imgPosition4");
                imgPosition4_hdf.ImgUrl = imgPosition4.GetAttributeValue("src", "");
                imgPosition4_hdf.CSID = carid;
            }
            else
            {
                imgLogo_hdf.ImgUrl = carnews.ImageA;
                imgLogo_hdf.CSID = carnews.CarID;
                imgPosition1_hdf.ImgUrl = carnews.ImageB;
                imgPosition1_hdf.CSID = carnews.CarID;
                imgPosition2_hdf.ImgUrl = carnews.ImageC;
                imgPosition2_hdf.CSID = carnews.CarID;
                imgPosition3_hdf.ImgUrl = carnews.ImageD;
                imgPosition3_hdf.CSID = carnews.CarID;
                imgPosition4_hdf.ImgUrl = carnews.ImageE;
                imgPosition4_hdf.CSID = carnews.CarID;
            }

            imgLogo_hdf.yiche = yc;
            imgLogo_hdf.ImageUpload = ImageUpload;
            imgPosition4_hdf.yiche = yc;
            imgPosition4_hdf.ImageUpload = ImageUpload;
            #endregion            

            cars.IsUp = TemplaceNewsType == 3;
            carA.CarDataSource = cars;
            carA.Colors = Colors;
            carA.ShowType(false);

            carControl1.CarDataSource = cars;
            carControl1.Colors = Colors;
            carControl1.ShowType(true);

            if(carnews != null)
            {
                dtpPromotionA.Value = carnews.StartDate;
                dtpPromotionB.Value = carnews.EndDate;
                chkIsShow400Number.Checked = carnews.IsShow400Number;
                chkIsShowMaintenance.Checked = carnews.IsShowMaintenance;
                chkIsShowMap.Checked = carnews.IsShowMap;
                chkIsShowSaleAddr.Checked = carnews.IsShowSaleAddr;
            }
        }

        void StoreState_CheckedChanged(object sender, EventArgs e)
        {
            var rbt = (RadioButton)sender;
            rdoStoreState = rbt.Tag.ToString();
        }

        string str_hadrdccid = "";
        string str_hacbcidsid = "";
        string str_hcbcidsid = "";
        string str_viewstate = "";
        string str_viewstategenerator = "";
        string str_hdfnewstype = "";
        string str_rptcarbrandhdfcbid = "";
        string str_hdfcurrentstate = "";

        void Car_CheckedChanged(object sender, EventArgs e)
        {
            var rbt = (RadioButton)sender;
            if (!rbt.Checked) return;
            CarType = rbt.Name;
            var tags = rbt.Tag.ToString().Split(':');
            carid = tags[0];
            cbid = tags[1];
            energytype = tags[2];
            csshowname = tags[3];
            #region 构建post数据
            StringBuilder sb = new StringBuilder(5000);
            sb.Append("scriptManager=scriptManager%7CbtnReferInfo&__EVENTTARGET=&__EVENTARGUMENT=&");

            var hadrdccid = doc.GetElementbyId("HADRDCCID");
            if (hadrdccid != null)
                str_hadrdccid = hadrdccid.GetAttributeValue("value", "");
            sb.AppendFormat("HADRDCCID={0}&", HttpHelper.URLEncode(str_hadrdccid));

            var hacbcidsid = doc.GetElementbyId("HACBCIDSID");
            if (hacbcidsid != null)
                str_hacbcidsid = hacbcidsid.GetAttributeValue("value", "");
            sb.AppendFormat("HACBCIDSID={0}&", HttpHelper.URLEncode(str_hacbcidsid));

            var hcbcidsid = doc.GetElementbyId("HCBCIDSID");
            if (hcbcidsid != null)
                str_hcbcidsid = hcbcidsid.GetAttributeValue("value", "");
            sb.AppendFormat("HCBCIDSID={0}&", HttpHelper.URLEncode(str_hcbcidsid));

            var viewstate = doc.GetElementbyId("__VIEWSTATE");
            if (viewstate != null)
                str_viewstate = viewstate.GetAttributeValue("value", "");
            sb.AppendFormat("__VIEWSTATE={0}&", HttpHelper.URLEncode(str_viewstate));

            var viewstategenerator = doc.GetElementbyId("__VIEWSTATEGENERATOR");
            if (viewstategenerator != null)
                str_viewstategenerator = viewstategenerator.GetAttributeValue("value", "");
            sb.AppendFormat("__VIEWSTATEGENERATOR={0}&", str_viewstategenerator);

            sb.AppendFormat("hdfSelectCarBrandID={0}&", cbid);
            sb.AppendFormat("hdfSelectSerialID={0}&", carid);
            sb.AppendFormat("hdfSelectSerialType={0}&", energytype);
            sb.AppendFormat("hdfNewsTitleTemplate={0}&", energytype == "0" ? "1" : "2");
            sb.AppendFormat("hdfCanSetTitleArticle={0}&", energytype == "0" ? "0" : "1");
            sb.AppendFormat("hdfCSShowName={0}&", HttpHelper.URLEncode(csshowname));
            sb.AppendFormat("hdfLastCheckedID={0}&", carid);

            var hdfnewstype = doc.GetElementbyId("hdfNewsType");
            if (hdfnewstype != null)
                str_hdfnewstype = hdfnewstype.GetAttributeValue("value", "");
            sb.AppendFormat("hdfNewsType={0}&", str_hdfnewstype);

            var rptcarbrandhdfcbid = doc.GetElementbyId("rptCarBrand_ctl00_hdfCBID");
            if (rptcarbrandhdfcbid != null)
                str_rptcarbrandhdfcbid = rptcarbrandhdfcbid.GetAttributeValue("value", "");
            sb.AppendFormat("{0}={1}&", HttpHelper.URLEncode("rptCarBrand$ctl00$hdfCBID"), str_rptcarbrandhdfcbid);

            sb.AppendFormat("CarSerialGroup={0}&hdfCarIDs=&hdfCarNewsList=&", carid);

            var hdfofflinecount = doc.GetElementbyId("hdfOffLineCount");
            sb.AppendFormat("hdfOffLineCount={0}&", hdfofflinecount.GetAttributeValue("value", ""));

            var hdfmindata = doc.GetElementbyId("hdfMinData");
            sb.AppendFormat("hdfMinData={0}&", hdfmindata.GetAttributeValue("value", ""));

            var hdfcurrentstate = doc.GetElementbyId("hdfCurrentState");
            if (hdfcurrentstate != null)
                str_hdfcurrentstate = hdfcurrentstate.GetAttributeValue("value", "");

            sb.AppendFormat("txtDateTimeBegin={0}&", dtpPromotionA.Value.ToString("yyyy-MM-dd"));
            sb.AppendFormat("txtDateTimeEnd={0}&", dtpPromotionB.Value.ToString("yyyy-MM-dd"));
            sb.AppendFormat("title_article=&rdoStoreState={0}&txtLead=&txtPrice=&", rdoStoreState);
            sb.Append("ddlBusinessTax=1&ddlTrafficTax=1&PurchaseTax=1&");
            sb.Append("txtOtherInfo=%E5%A6%82%E7%A4%BC%E5%8C%85%E8%A7%84%E5%88%99%E6%88%96%E7%A4%BC%E5%8C%85%E5%86%85%E5%AE%B9&");
            sb.Append("imgLogo_hdf=&hdfSerial=&hdfImgSelectID=&imgPosition1_hdf=&imgPosition2_hdf=&imgPosition3_hdf=&imgPosition4_hdf=&");
            sb.Append("chkIsShowMaintenance=on&chkIsShowSaleAddr=on&chkIsShowMap=on&chkIsShow400Number=on&");
            sb.AppendFormat("hdfCurrentState={0}&hdnType=money&", str_hdfcurrentstate);
            sb.Append("hdnCarMerchandiseID=&hdnPromotionType=&hdfCarInfoJson=&hdfGiftInfo=&imgUploadChangehidethumburl=&imgUploadChangehideUrl=&");
            sb.Append("txtStartPrice=&txtEndPrice=&txtKeyword=&__ASYNCPOST=true&btnReferInfo=%E5%88%B7%E6%96%B0");
            #endregion
            doc = yc.Post_CheYiTong(url, sb.ToString());
            InitDetail();
        }        

        private string PushData(bool isdetail)
        {
            StringBuilder sb = new StringBuilder(5000);
            sb.Append("scriptManager=UpdatePanel4%7CbtnPublish&");

            var hadrdccid = doc.GetElementbyId("HADRDCCID");
            if (hadrdccid != null)
                str_hadrdccid = hadrdccid.GetAttributeValue("value", "");
            sb.AppendFormat("HADRDCCID={0}&", HttpHelper.URLEncode(str_hadrdccid));

            var hacbcidsid = doc.GetElementbyId("HACBCIDSID");
            if (hacbcidsid != null)
                str_hacbcidsid = hacbcidsid.GetAttributeValue("value", "");
            sb.AppendFormat("HACBCIDSID={0}&", HttpHelper.URLEncode(str_hacbcidsid));

            var hcbcidsid = doc.GetElementbyId("HCBCIDSID");
            if (hcbcidsid != null)
                str_hcbcidsid = hcbcidsid.GetAttributeValue("value", "");
            sb.AppendFormat("HCBCIDSID={0}&", HttpHelper.URLEncode(str_hcbcidsid));

            sb.AppendFormat("hdfSelectCarBrandID={0}&", cbid);
            sb.AppendFormat("hdfSelectSerialID={0}&", carid);
            sb.AppendFormat("hdfSelectSerialType={0}&", energytype);
            sb.AppendFormat("hdfNewsTitleTemplate={0}&", energytype == "0" ? "1" : "2");
            sb.AppendFormat("hdfCanSetTitleArticle={0}&", energytype == "0" ? "0" : "1");
            sb.AppendFormat("hdfCSShowName={0}&", HttpHelper.URLEncode(csshowname, Encoding.UTF8));
            sb.AppendFormat("hdfLastCheckedID={0}&", carid);

            var hdfnewstype = doc.GetElementbyId("hdfNewsType");
            if (hdfnewstype != null)
                str_hdfnewstype = hdfnewstype.GetAttributeValue("value", "");
            sb.AppendFormat("hdfNewsType={0}&", str_hdfnewstype);

            var rptcarbrandhdfcbid = doc.GetElementbyId("rptCarBrand_ctl00_hdfCBID");
            if (rptcarbrandhdfcbid != null)
                str_rptcarbrandhdfcbid = rptcarbrandhdfcbid.GetAttributeValue("value", "");
            sb.AppendFormat("{0}={1}&", HttpHelper.URLEncode("rptCarBrand$ctl00$hdfCBID"), str_rptcarbrandhdfcbid);

            sb.AppendFormat("CarSerialGroup={0}&hdfCarIDs=&", carid);

            var hdfcarnewslist = doc.GetElementbyId("hdfCarNewsList");
            var inputvalue = hdfcarnewslist.GetAttributeValue("value", "").Replace("&quot;", "\"");
            sb.AppendFormat("hdfCarNewsList={0}&", HttpHelper.URLEncode(inputvalue, Encoding.UTF8));

            var hdfofflinecount = doc.GetElementbyId("hdfOffLineCount");
            sb.AppendFormat("hdfOffLineCount={0}&", hdfofflinecount.GetAttributeValue("value", ""));

            var hdfmindata = doc.GetElementbyId("hdfMinData");
            sb.AppendFormat("hdfMinData={0}&", hdfmindata.GetAttributeValue("value", ""));

            sb.AppendFormat("txtDateTimeBegin={0}&", dtpPromotionA.Value.ToString("yyyy-MM-dd"));
            sb.AppendFormat("txtDateTimeEnd={0}&", dtpPromotionB.Value.ToString("yyyy-MM-dd"));

            var chklyeartype = doc.DocumentNode.SelectNodes("//input[@name='chklYearType' and @checked='checked']");
            if (chklyeartype != null)
            {
                foreach (HtmlNode node in chklyeartype)
                {
                    sb.AppendFormat("chklYearType={0}&", node.GetAttributeValue("value", ""));
                }
            }            
            var carsource = isdetail ? carControl1.CarDataSource : carA.CarDataSource;
            for (int i = 0; i < carsource.Cars.Count; i++)
            {
                var car = carsource.Cars[i];
                sb.AppendFormat("{0}={1}&", string.Format("rptFavourableList%24ctl{0:00}%24checkbox", i), car.CarID);
                sb.AppendFormat("{0}={1}&", string.Format("rptFavourableList%24ctl{0:00}%24rate", i), car.Discount);
                sb.AppendFormat("{0}={1}&", string.Format("rptFavourableList%24ctl{0:00}%24Text1", i), car.FavorablePrice);
            }
            for (int i = 0; i < carsource.PublishCarList.Count; i++)
            {
                var pub = carsource.PublishCarList[i];
                sb.AppendFormat("{0}={1}&", string.Format("rtpNotPushCarList%24ctl{0:00}%24checkbox", i), pub.CarID);
            }

            sb.Append("NewEnergyTitleTemplate=2&");
            sb.AppendFormat("title_article={0}&", HttpHelper.URLEncode(title_article.Text, Encoding.UTF8));
            sb.AppendFormat("rdoStoreState={0}&", rdoStoreState);
            cars.Radlst.ForEach(f => sb.AppendFormat("radlst={0}&", f));
            sb.AppendFormat("txtLead={0}&txtPrice={1}&ddlBusinessTax={2}&", HttpHelper.URLEncode(txtLead.Text, Encoding.UTF8), cars.GGiftInof.Price, string.IsNullOrWhiteSpace(cars.GGiftInof.SYXValue) ? "1" : cars.GGiftInof.SYXValue);
            sb.AppendFormat("ddlTrafficTax={0}&PurchaseTax={1}&", string.IsNullOrWhiteSpace(cars.GGiftInof.JQXValue) ? "1" : cars.GGiftInof.JQXValue, 1);//txtPurchaseTax.Text
            sb.AppendFormat("txtOtherInfo={0}&", HttpHelper.URLEncode(string.IsNullOrWhiteSpace(cars.GGiftInof.OtherInfoValue) ? "如礼包规则或礼包内容" : cars.GGiftInof.OtherInfoValue, Encoding.UTF8));
            sb.AppendFormat("imgLogo_hdf={0}&hdfSerial={1}&hdfImgSelectID=&", HttpHelper.URLEncode(imgLogo_hdf.ImgUrl), carid, imgLogo_hdf.ImgSelectID);
            sb.AppendFormat("imgPosition1_hdf={0}&", HttpHelper.URLEncode(imgPosition1_hdf.ImgUrl));
            sb.AppendFormat("imgPosition2_hdf={0}&", HttpHelper.URLEncode(imgPosition2_hdf.ImgUrl));
            sb.AppendFormat("imgPosition3_hdf={0}&", HttpHelper.URLEncode(imgPosition3_hdf.ImgUrl));
            sb.AppendFormat("imgPosition4_hdf={0}&", HttpHelper.URLEncode(imgPosition4_hdf.ImgUrl));
            sb.AppendFormat("chkIsShowMaintenance={0}&", chkIsShowMaintenance.Checked ? "on" : "off");
            sb.AppendFormat("chkIsShowSaleAddr={0}&", chkIsShowSaleAddr.Checked ? "on" : "off");
            sb.AppendFormat("chkIsShowMap={0}&", chkIsShowMap.Checked ? "on" : "off");
            sb.AppendFormat("chkIsShow400Number={0}&", chkIsShow400Number.Checked ? "on" : "off");

            var hdfcurrentstate = doc.GetElementbyId("hdfCurrentState");
            if (hdfcurrentstate != null)
                str_hdfcurrentstate = hdfcurrentstate.GetAttributeValue("value", "");
            sb.AppendFormat("hdfCurrentState={0}&", str_hdfcurrentstate);            
            sb.AppendFormat("hdnType={0}&hdnCarMerchandiseID={1}&", ddlPromotionType.SelectedValue == "0" ? "money" : "rate", cars.GGiftInof.Merchandises != null ? string.Join(",", cars.GGiftInof.Merchandises.Select(s => s.id)) : "");
            sb.AppendFormat("hdnPromotionType={0}&", ddlPromotionType.SelectedIndex);
            sb.AppendFormat("hdfCarInfoJson={0}&hdfGiftInfo={1}&", HttpHelper.URLEncode(carsource.CarInfoJson, Encoding.UTF8), HttpHelper.URLEncode(cars.GiftInofJson));
            sb.Append("imgUploadChangehidethumburl=&imgUploadChangehideUrl=&txtStartPrice=&");
            sb.Append("txtEndPrice=&txtKeyword=&__EVENTTARGET=&__EVENTARGUMENT=&");

            var viewstate = doc.GetElementbyId("__VIEWSTATE");
            if (viewstate != null)
                str_viewstate = viewstate.GetAttributeValue("value", "");
            sb.AppendFormat("__VIEWSTATE={0}&", HttpHelper.URLEncode(str_viewstate));

            var viewstategenerator = doc.GetElementbyId("__VIEWSTATEGENERATOR");
            if (viewstategenerator != null)
                str_viewstategenerator = viewstategenerator.GetAttributeValue("value", "");
            sb.AppendFormat("__VIEWSTATEGENERATOR={0}&__ASYNCPOST=true&btnPublish=%E5%8F%91%E5%B8%83", str_viewstategenerator);
            return sb.ToString();
        }

        private string PTNewsPost()
        {
            StringBuilder sb = new StringBuilder(5000);
            sb.Append("ScriptManager1=ctl17%7CbtnPublish&__EVENTTARGET=&__EVENTARGUMENT=&");
            var viewstate = doc.GetElementbyId("__VIEWSTATE");
            if (viewstate != null)
                str_viewstate = viewstate.GetAttributeValue("value", "");
            sb.AppendFormat("__VIEWSTATE={0}&", HttpHelper.URLEncode(str_viewstate));
            var viewstategenerator = doc.GetElementbyId("__VIEWSTATEGENERATOR");
            if (viewstategenerator != null)
                str_viewstategenerator = viewstategenerator.GetAttributeValue("value", "");
            sb.AppendFormat("__VIEWSTATEGENERATOR={0}&", str_viewstategenerator);
            sb.AppendFormat("title_article={0}&", HttpHelper.URLEncode(txtPTTitle.Text, Encoding.UTF8));
            sb.AppendFormat("ddlNewsClass={0}&", ddlNewsClass.SelectedValue);
            sb.AppendFormat("txtDesc={0}&", HttpHelper.URLEncode(txtDesc.Text, Encoding.UTF8));
            var txtDesc_classId = doc.GetElementbyId("txtDesc_classId");
            sb.AppendFormat("txtDesc_classId={0}&", txtDesc_classId.GetAttributeValue("value", ""));
            var txtDesc_folder = doc.GetElementbyId("txtDesc_folder");
            sb.AppendFormat("txtDesc_folder={0}&", txtDesc_folder.GetAttributeValue("value", ""));
            var txtDesc_HostName = doc.GetElementbyId("txtDesc_HostName");
            sb.AppendFormat("txtDesc_HostName={0}&", txtDesc_HostName.GetAttributeValue("value", ""));
            var txtDesc_CrossDomain = doc.GetElementbyId("txtDesc_CrossDomain");
            sb.AppendFormat("txtDesc_CrossDomain={0}&", txtDesc_CrossDomain.GetAttributeValue("value", ""));
            var selectedValue = "";            
            foreach(Control con in tabPage3.Controls)
            {
                var chk  = con as CheckBox;
                if(chk != null && chk.Checked)
                {
                    var str = chk.Tag.ToString() + ",";
                    selectedValue += str.Split(',')[0] +",";
                    sb.AppendFormat("chkItem={0}&", HttpHelper.URLEncode(str, Encoding.UTF8));
                }                    
            }
            sb.AppendFormat("hidSeriesList={0}&", HttpHelper.URLEncode(selectedValue));
            foreach(Control con in pVote.Controls)
            {
                var rbt  = con as RadioButton;
                if(rbt != null && rbt.Checked)
                {
                    var str = rbt.Tag.ToString();
                    selectedValue = str.Split(',')[0] +",";
                    sb.AppendFormat("radItem={0}&", HttpHelper.URLEncode(str, Encoding.UTF8));
                    break;
                }
            }
            sb.AppendFormat("txtVoteID={0}&", selectedValue);
            sb.AppendFormat("txtVoteType={0}&", rbtVoteType1.Checked ? "1" : "2");
            sb.AppendFormat("VoteType={0}&", rbtVoteType1.Checked ? "1" : "2");
            foreach(Control con in tabPage5.Controls)
            {
                var chk  = con as CheckBox;
                if(chk != null && chk.Checked)
                {
                    var str = chk.Tag.ToString() + ",";
                    selectedValue += str.Split(',')[0] +",";
                    sb.AppendFormat("chkItem2={0}&", HttpHelper.URLEncode(str, Encoding.UTF8));
                }
            }
            sb.AppendFormat("hidSeriesListbuycar={0}&", HttpHelper.URLEncode(selectedValue));
            if(chkAddress.Checked) sb.AppendFormat("chkIsShowSaleAddr=on&");
            if(chkMap.Checked) sb.AppendFormat("chkIsShowMap=on&");
            if(chkTel.Checked) sb.AppendFormat("chkIsShow400Number=on&");
            sb.Append("txtNewsID=&txtDraftID=&__ASYNCPOST=true&btnPublish=%E5%8F%91%E5%B8%83");
            return sb.ToString();
        }

        private void rbtSource_CheckedChanged(object sender, EventArgs e)
        {
            var rbt = (RadioButton)sender;
            if(rbt.Checked)
                InitForm(rbt.Tag.ToString());
        }

        private void btnSend_Ex_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMoney.Text))
            {
                MessageBox.Show("请输入优惠金额");
                return;
            }

            var maxMoney = Convert.ToDecimal(txtMoney.Text);

            if (maxMoney == 0 && !cars.GGiftInof.IsCheck)
            {
                MessageBox.Show("优惠金额为0，请选择礼包");
                return;
            }

            if (cars.Cars == null || cars.Cars.Count == 0 || !cars.Cars.Any(w => w.IsCheck))
            {
                MessageBox.Show("请至少选择一个车型");
                return;
            }           

            var giftPrice = cars.GGiftInof.Price;
            GenerateTitleAndLead(maxMoney, maxMoney, giftPrice, csshowname);           

            InitCarNews();
            carnews.CarList.ForEach(f => f.FavorablePrice = maxMoney);

            string selected = "";
            if (chkAllColor.Checked)
                selected = "颜色齐全";

            foreach (Control con in this.Controls)
            {
                var chk = con as CheckBox;
                if (chk != null && chk.Checked)
                {
                    cars.Radlst.Add(chk.Tag.ToString());
                    if (selected != "颜色齐全")
                        selected += chk.Text + ",";
                }
            }

            cars.Cars.ForEach(c => c.ColorName = selected.TrimEnd(','));

            SendNews(false);
        }

        private void SendNews(bool isDetail)
        {
            carnews.IsDetail = false;
            
            var postdata = PushData(isDetail);
            var content = JsonConvert.SerializeObject(carnews);
            newsid = dal.AddNews(newsid, carnews.Title, "删除草稿", url);
            var r = OperateIniFile.WriteIniData(content, postdata, newsid.ToString());
            if (r)
                DialogResult = DialogResult.OK;
        }

        private void SendPTNews()
        {
            var postdata = PTNewsPost();
            var content = JsonConvert.SerializeObject(carnews);
            newsid = dal.AddNews(newsid, carnews.PTNews.Title, "删除草稿", url);
            var r = OperateIniFile.WriteIniData(content, postdata, newsid.ToString());
            if (r)
                DialogResult = DialogResult.OK;
        }

        private void InitCarNews()
        {
            if (carnews == null) carnews = new CarNews();
            carnews.NewsType = NewsType;
            if (NewsType == "rbtSource0")
            {
                carnews.PTNews.Title = txtPTTitle.Text;
                carnews.PTNews.Type = ddlNewsClass.SelectedValue.ToString();
                carnews.PTNews.Content = txtDesc.Text;
                carnews.PTNews.Address = chkAddress.Checked;
                carnews.PTNews.Map = chkMap.Checked;
                carnews.PTNews.Tel = chkTel.Checked;
                if (carnews.PTNews.Brands == null)
                    carnews.PTNews.Brands = new List<string>();
                else
                    carnews.PTNews.Brands.Clear();
                foreach (Control con in tabPage3.Controls)
                {
                    var chk = con as CheckBox;
                    if (chk != null && chk.Checked)
                        carnews.PTNews.Brands.Add(chk.Tag.ToString());
                }
                if (carnews.PTNews.SelectVote == null)
                    carnews.PTNews.SelectVote = new Vote();
                carnews.PTNews.SelectVote.VoteType = rbtVoteType1.Checked ? "1" : "2";
                foreach (Control con in pVote.Controls)
                {
                    var rbt = con as RadioButton;
                    if (rbt != null && rbt.Checked)
                    {
                        carnews.PTNews.SelectVote.VoteIndex = rbt.Tag.ToString();
                        break;
                    }
                }
                if (carnews.PTNews.BuyCar == null)
                    carnews.PTNews.BuyCar = new List<string>();
                else
                    carnews.PTNews.BuyCar.Clear();
                foreach (Control con in tabPage5.Controls)
                {
                    var chk = con as CheckBox;
                    if (chk != null && chk.Checked)
                        carnews.PTNews.BuyCar.Add(chk.Tag.ToString());
                }
            }
            else
            {
                #region 促销
                carnews.PromotionType = ddlPromotionType.SelectedValue.ToString();
                carnews.PromotionValue = txtMoney.Text;
                carnews.StoreState = rdoStoreState;
                carnews.CarType = CarType;
                foreach (Control con in pColor.Controls)
                {
                    var chk = con as CheckBox;
                    if (chk != null && chk.Checked)
                    {
                        carnews.Colors.Add(chk.Tag.ToString());
                    }
                }
                carnews.Title = title_article.Text;
                carnews.title_number = title_number.Text;
                carnews.Lead = txtLead.Text;
                carnews.ImageA = imgLogo_hdf.ImgUrl;
                carnews.CarID = imgLogo_hdf.CSID;
                carnews.ImageB = imgPosition1_hdf.ImgUrl;
                carnews.CarID = imgPosition1_hdf.CSID;
                carnews.ImageC = imgPosition2_hdf.ImgUrl;
                carnews.CarID = imgPosition2_hdf.CSID;
                carnews.ImageD = imgPosition3_hdf.ImgUrl;
                carnews.CarID = imgPosition3_hdf.CSID;
                carnews.ImageE = imgPosition4_hdf.ImgUrl;
                carnews.CarID = imgPosition4_hdf.CSID;

                if (!carnews.IsDetail)
                {
                    carnews.CarList = carControl1.CarDataSource.Cars;
                    carnews.YearType = carControl1.CarDataSource.YearType;
                }
                else
                {
                    carnews.CarList = carA.CarDataSource.Cars;
                }
                carnews.IsShowMaintenance = chkIsShowMaintenance.Checked;
                carnews.IsShowSaleAddr = chkIsShowSaleAddr.Checked;
                carnews.IsShowMap = chkIsShowMap.Checked;
                carnews.IsShow400Number = chkIsShow400Number.Checked;
                carnews.StartDate = dtpPromotionA.Value;
                carnews.EndDate = dtpPromotionB.Value;
                #endregion
            }
        }

        /// <summary>
        /// 设置标题和导语
        /// </summary>
        /// <param name="maxMoney"></param>
        /// <param name="maxMoneyWithSubsidies"></param>
        /// <param name="giftPrice">礼包金额</param>
        /// <param name="name">车型</param>
        /// <returns></returns>
        public void GenerateTitleAndLead(decimal maxMoney, decimal maxMoneyWithSubsidies, string giftPrice, string name)
        {
            var title = "";
            var lead = "";
            var newstitleTemplate = energytype == "0" ? 1 : 2;
            var content = "";
            var dealerShortName = Tool.userInfo_yc.Company;

            if (newstitleTemplate == 2)
            {
                maxMoney = maxMoneyWithSubsidies;
            }

            if (maxMoney == 0)
            {
                if (TemplaceNewsType == 1)
                {
                    if (newstitleTemplate == 1)
                    {
                        content = "享受补贴送大礼包";
                    }
                    if (newstitleTemplate == 2)
                    {
                        if (giftPrice != "")
                        {
                            content = "购车送" + giftPrice + "元大礼包";
                        }
                        else
                        {
                            content = "购车送大礼包";
                        }
                    }
                    if (giftPrice != "")
                    {
                        title = dealerShortName + name + content;
                        lead = dealerShortName + name + content + "，感兴趣的朋友可以到店咨询购买，具体优惠信息如下：";
                    }
                    else
                    {
                        title = dealerShortName + name + content;
                        lead = dealerShortName + name + content + "，感兴趣的朋友可以到店咨询购买，具体优惠信息如下：";
                    }

                    // 添加经销商名称如果超出了18个字，就不添加经销名称
                    if (title.Length > 18)
                    {
                        title = name + content;
                    }
                }
                else if (TemplaceNewsType == 2)
                {
                    if (newstitleTemplate == 1)
                    {
                        content = "享受补贴送大礼包";
                    }
                    if (newstitleTemplate == 2)
                    {
                        if (giftPrice != "")
                        {
                            content = "送" + giftPrice + "元大礼包";
                        }
                        else
                        {
                            content = "送大礼包";
                        }
                    }

                    if (giftPrice == "")
                    {
                        title = "置换" + name + content;
                        //  导语
                        lead = dealerShortName + "置换" + name + content + "，" + "感兴趣的朋友可以到店咨询购买，具体优惠信息请见下表：";
                    }
                    else
                    {
                        title = "置换" + name + content;
                        //  导语
                        lead = dealerShortName + "置换" + name + content + "，" + "感兴趣的朋友可以到店咨询购买，具体优惠信息请见下表：";
                    }
                }
                else if (TemplaceNewsType == 3)
                {
                    title = dealerShortName + name + "火热销售中";
                    lead = dealerShortName + name + "火热销售中，感兴趣的朋友可以到店咨询购买，具体优惠信息如下：";
                }
            }
            else
            {
                if (newstitleTemplate == 1)
                {
                    content = "享受补贴还优惠" + maxMoney + "万元";
                }
                else
                {
                    content = "优惠高达" + maxMoneyWithSubsidies + "万元";
                }

                if (TemplaceNewsType == 1)
                {
                    title = dealerShortName + name + content;
                    if (title.Length > 18)
                    {
                        title = name + content;
                    }

                    lead = dealerShortName + name + content + "，感兴趣的朋友可以到店咨询购买，具体优惠信息如下：";
                }
                else if (TemplaceNewsType == 2)
                {
                    title = "置换" + name + content;

                    lead = dealerShortName + "置换" + name + content + "，感兴趣的朋友可以到店咨询购买，具体优惠信息请见下表：";
                }
                else if (TemplaceNewsType == 3)
                {
                    title = dealerShortName + name + "火热销售中";
                    if (title.Length > 18)
                    {
                        title = name + "火热销售中";
                    }

                    lead = dealerShortName + name + "火热销售中，感兴趣的朋友可以到店咨询购买，具体信息如下：";
                }
            }

            title_article.Text = title;
            title_number.Text = title.Length.ToString();
            txtLead.Text = lead;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new FormGift(cars.GGiftInof, merchandise);
            if(form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cars.GGiftInof = form.info;
                carnews.giftInfo = form.info;
            }
        }

        private void chkAllColor_CheckedChanged(object sender, EventArgs e)
        {
            foreach(Control con in pColor.Controls)
            {
                var chk = con as CheckBox;
                if(chk != null && chk.Name != chkAllColor.Name)
                {
                    chk.Checked = chkAllColor.Checked;
                    chk.Enabled = !chkAllColor.Checked;
                }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new FormGift(cars.GGiftInof, merchandise);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cars.GGiftInof = form.info;
                carnews.giftInfo = form.info;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var maxMoney = cars.Cars.Max(f => f.FavorablePrice);

            if (maxMoney == 0 && !cars.GGiftInof.IsCheck)
            {
                MessageBox.Show("优惠金额为0，请选择礼包");
                return;
            }

            if (cars.Cars == null || cars.Cars.Count == 0 || !cars.Cars.Any(w => w.IsCheck))
            {
                MessageBox.Show("请至少选择一个车型");
                return;
            }
            InitCarNews();

            GenerateTitleAndLead(maxMoney, maxMoney, cars.GGiftInof.Price, csshowname);

            SendNews(true);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var maxMoney = cars.Cars.Max(f => f.FavorablePrice);
            GenerateTitleAndLead(maxMoney, maxMoney, cars.GGiftInof.Price, csshowname);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtPTTitle.Text))
            {
                MessageBox.Show("请输入标题");
                return;
            }
            else if(txtPTTitle.Text.Length > 18)
            {
                MessageBox.Show("标题最大长度为18个字符");
                return;
            }
            else if(ddlNewsClass.SelectedIndex == 0)
            {
                MessageBox.Show("请选择新闻分类");
                return;
            }
            else if(string.IsNullOrWhiteSpace(txtDesc.Text))
            {
                MessageBox.Show("请输入新闻正文");
                return;
            }
            InitCarNews();
            SendPTNews();
        }
    }

    public class PromotionCars
    {
        public PromotionCars()
        {
            YearType = new List<YearType>();
            Cars = new List<Car>();
            PublishCarList = new List<Car>();
            GGiftInof = new GiftInfo();
            Radlst = new List<string>();
        }

        /// <summary>
        /// 年款
        /// </summary>
        public List<YearType> YearType { get; set; }
        /// <summary>
        /// 车列表
        /// </summary>
        public List<Car> Cars { get; set; }

        public List<string> Radlst { get; set; }

        public string CarInfoJson { get {
            StringBuilder sb = new StringBuilder(500);
            sb.Append("[");
            Cars.ForEach(f => {
                sb.Append("{");
                sb.AppendFormat("\"RelaID\":{0},\"CarID\":{1},\"Discount\":\"{2}\",\"IsCheck\":{3},\"CarReferPrice\":\"{4}\",\"FavorablePrice\":\"{5}\",\"StoreState\":\"{6}\",\"ColorName\":\"{7}\",\"CustomColorName\":\"{8}\",\"Mark\":\"{9}\",\"ExtendCarID\":\"{10}\",\"IsNewEnergy\":\"{11}\",\"StateSubsidies\":{12},\"LocalSubsidies\":{13}", f.RelaID, f.CarID, f.Discount, f.IsCheck.ToString().ToLower(), f.CarReferPrice, f.FavorablePrice, f.StoreState, f.ColorName, f.CustomColorName, f.Mark, f.ExtendCarID, f.IsNewEnergy, f.StateSubsidies, f.LocalSubsidies);
                sb.Append("},");
            });
            PublishCarList.ForEach(f => {
                sb.Append("{");
                sb.AppendFormat("\"RelaID\":{0},\"CarID\":{1},\"Discount\":{2},\"IsCheck\":{3},\"CarReferPrice\":\"{4}\",\"FavorablePrice\":{5},\"IsAllowance\":{6},\"StoreState\":{7},\"ColorName\":\"{8}\",\"CustomColorName\":\"{9}\",\"Mark\":{10},\"ExtendCarID\":{11}", f.RelaID, f.CarID, f.Discount, f.IsCheck.ToString().ToLower(), f.CarReferPrice, f.FavorablePrice, f.IsAllowance, f.StoreState, f.ColorName, f.CustomColorName, f.Mark, f.ExtendCarID);
                sb.Append("},");
            });
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString();
        } }

        /// <summary>
        /// 说明
        /// </summary>
        public string Note { get; set; }

        public List<Car> PublishCarList { get; set; }

        public GiftInfo GGiftInof { get; set; }

        public string GiftInofJson {
            get
            {
                StringBuilder sb = new StringBuilder(500);
                if (GGiftInof.IsCheck)
                {
                    sb.Append("{");
                    sb.AppendFormat("\"IsCheck\":{0},\"Price\":\"{1}\",\"QCYPIsCheck\":{2},\"QCYPValue\":\"{3}\",\"YKIsCheck\":{4},\"YKValue\":\"{5}\",\"SYXIsCheck\":{6},\"SYXValue\":{7},\"JQXIsCheck\":{8},\"JQXValue\":{9},\"GZSIsCheck\":{10},\"GZSValue\":{11},\"BAOYANGIsCheck\":{12},\"BAOYANGValue\":\"{13}\",\"OherInfoIsCheck\":{14},\"OherInfoValue\":\"{15}\"", GGiftInof.IsCheck.ToString().ToLower(), GGiftInof.Price, GGiftInof.QCYPIsCheck.ToString().ToLower(), string.Join(",", GGiftInof.Merchandises.Select(s => s.id)), GGiftInof.YKIsCheck.ToString().ToLower(), GGiftInof.YKValue, GGiftInof.SYXIsCheck.ToString().ToLower(), GGiftInof.SYXValue, GGiftInof.JQXIsCheck.ToString().ToLower(), GGiftInof.JQXValue, GGiftInof.GZSIsCheck.ToString().ToLower(), GGiftInof.GZSValue, GGiftInof.BaoYangIsCheck.ToString().ToLower(), GGiftInof.BaoYangValue, GGiftInof.OtherInfoIsCheck.ToString().ToLower(), GGiftInof.OtherInfoValue);
                    sb.Append("}");
                }
                else
                {
                    sb.Append("{\"IsCheck\":false,\"Price\":0,\"QCYPIsCheck\":false,\"QCYPValue\":\"\",\"YKIsCheck\":false,\"YKValue\":0,\"SYXIsCheck\":false,\"SYXValue\":1,\"JQXIsCheck\":false,\"JQXValue\":1,\"GZSIsCheck\":false,\"GZSValue\":1,\"BAOYANGIsCheck\":false,\"BAOYANGValue\":\"\",\"OherInfoIsCheck\":false,\"OherInfoValue\":\"\"}");
                }
                return sb.ToString();
            }
        }

        public bool IsUp { get; set; }
    }

    public class Car
    {
        public bool IsUp { get; set; }
        public int RelaID { get; set; }
        public int CarID { get; set; }
        /// <summary>
        /// 是否勾选
        /// obj.IsCheck = $(trObj).find(":checkbox").eq(0).attr("checked"); 
        /// </summary>
        public bool IsCheck { get; set; }
        /// <summary>
        /// 年款
        /// </summary>
        public string YearType { get; set; }
        /// <summary>
        /// 车款名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 指导价(万)
        /// obj.CarReferPrice = inputCarInfo.attr("carreferprice");
        /// </summary>
        public decimal CarReferPrice { get; set; }

        /// <summary>
        /// 优惠金额(万)
        /// obj.FavorablePrice = $(trObj).find("input[price]").val() == "" ? 0 : $(trObj).find("input[price]").val();
        /// </summary>
        public decimal FavorablePrice { get; set; }
                
        /// <summary>
        /// 折扣率
        /// obj.Discount = $(trObj).find("input[rate]").val() == "" ? 0 : $(trObj).find("input[rate]").val();
        /// </summary>
        public decimal Discount { get; set; }
                
        /// <summary>
        /// 是否包含惠民补贴
        /// obj.IsAllowance = $(trObj).find("input[allowance]").attr("checked");
        /// </summary>
        public string IsAllowance { get; set; }

        /// <summary>
        /// 新能源车补贴(万)
        /// </summary>
        public string Subsidies { get; set; }
        /// <summary>
        /// 优惠价(万)
        /// </summary>
        public decimal PromotionPrice {
            get
            {
                return IsUp ? CarReferPrice + FavorablePrice : CarReferPrice - FavorablePrice;
            }
        }

        /// <summary>
        /// 库存状态
        //  obj.StoreState = $(trObj).find("select[storestate]").val() == "" ? 1 : $(trObj).find("select[storestate]").val();
        //  obj.StoreState = obj.StoreState ? obj.StoreState : 1;
        /// </summary>
        public string StoreState { get; set; }
                
        /// <summary>
        /// 车型颜色
        /// obj.ColorName = $(trObj).find(".pick_color a").attr("selectcolor");
        /// </summary>
        public string ColorName { get; set; }
                
        /// <summary>
        /// 车型颜色
        /// obj.CustomColorName = $(trObj).find(".pick_color a").attr("costumcolor");
        /// </summary>
        public string CustomColorName { get; set; }

        public string PushedCount { get; set; }
                
        /// <summary>
        /// 标识
        /// obj.Mark = $(this).attr("mark");
        /// </summary>
        public int Mark { get; set; }
                
        /// <summary>
        /// 标识 增配车ID
        /// obj.ExtendCarID = $(this).attr("ExtendCarID");
        /// </summary>
        public int ExtendCarID { get; set; }
        
        /// <summary>
        /// obj.IsNewEnergy = $(this).attr("isNewEnergy")
        /// </summary>
        public int IsNewEnergy { get; set; }
        public string StateSubsidies { get; set; }        
        public string LocalSubsidies { get; set; }

        public string Action { get { return "选择颜色"; } }
    }

    public class GiftInfo
    {        
        /// <summary>
        /// 是否赠送礼包
        /// </summary>
        public bool IsCheck { get; set; }

        //礼包 总价值
        public string Price { get; set; }

        // 汽车用品
        public bool QCYPIsCheck { get; set; }
        public string QCYPValue { get; set; }
        public List<Merchandise> Merchandises { get; set; }

        // 油卡
        public bool YKIsCheck { get; set; }
        public string YKValue { get; set; }

        // 商业险
        public bool SYXIsCheck { get; set; }
        public string SYXValue { get; set; }

        // 交强险
        public bool JQXIsCheck { get; set; }
        public string JQXValue { get; set; }

        // 购置税
        public bool GZSIsCheck { get; set; }
        public string GZSValue { get; set; }

        // 保养
        public bool BaoYangIsCheck { get; set; }
        public string BaoYangType { get; set; }
        public string BaoYangValue { get; set; }
        public string BaoYangValue2 { get; set; }

        // 其他内容
        public bool OtherInfoIsCheck { get; set; }
        public string OtherInfoValue  { get; set; }
    }

    public class Merchandise
    {
        public bool IsCheck { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string Class { get; set; }
        public string Price { get; set; }
        public string Del { get { return "删除"; } }
    }

    public class CarNews
    {
        public CarNews()
        {
            Colors = new List<string>();
            giftInfo = new GiftInfo();
            CarList = new List<Car>();
            promotionCars = new List<Car>();
            PTNews = new PTNews();
        }

        /// <summary>
        /// 是否是明细
        /// </summary>
        public bool IsDetail { get; set; }
        public string CarID { get; set; }

        /// <summary>
        /// 新闻类型
        /// </summary>
        public string NewsType { get; set; }    //保存控件名称
        /// <summary>
        /// 促销车型
        /// </summary>
        public string CarType { get; set; }     //保存控件名称
        
        public DateTime MinDate { get; set; }
        /// <summary>
        /// 促销开始日期
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 促销结束日期
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 库存状态 
        /// </summary>
        public string StoreState { get; set; }
        /// <summary>
        /// 优惠类型
        /// </summary>
        public string PromotionType { get; set; }
        /// <summary>
        /// 优惠值
        /// </summary>
        public string PromotionValue { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public List<string> Colors { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        public string title_number { get; set; }
        /// <summary>
        /// 导语
        /// </summary>
        public string Lead { get; set; }
        /// <summary>
        /// 礼包
        /// </summary>
        public GiftInfo giftInfo { get; set; }
        public List<YearType> YearType { get; set; }
        /// <summary>
        /// 未发车
        /// </summary>
        public List<Car> CarList { get; set; }
        /// <summary>
        /// 已发车
        /// </summary>
        public List<Car> promotionCars { get; set; }
        /// <summary>
        /// 大图
        /// </summary>
        public string ImageA { get; set; }
        /// <summary>
        /// 小图1
        /// </summary>
        public string ImageB { get; set; }
        /// <summary>
        /// 小图2
        /// </summary>
        public string ImageC { get; set; }
        /// <summary>
        /// 小图3
        /// </summary>
        public string ImageD { get; set; }
        /// <summary>
        /// 小图4
        /// </summary>
        public string ImageE { get; set; }
        /// <summary>
        /// 添加保养信息
        /// </summary>
        public bool IsShowMaintenance { get; set; }
        /// <summary>
        /// 添加公司信息
        /// </summary>
        public bool IsShowSaleAddr { get; set; }
        /// <summary>
        /// 添加地图名片
        /// </summary>
        public bool IsShowMap { get; set; }
        /// <summary>
        /// 添加400电话
        /// </summary>
        public bool IsShow400Number { get; set; }        
        public string Status { get; set; }
        public int Statusvalue { get; set; }
        public int Mark { get; set; }
        public int Extendcarid { get; set; }

        public PTNews PTNews { get; set; }
    }

    public class PTNews
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public List<string> Brands { get; set; }
        public Vote SelectVote { get; set; }
        public List<string> BuyCar { get; set; }
        public bool Address { get; set; }
        public bool Map { get; set; }
        public bool Tel { get; set; }
    }

    public class Vote
    {
        public string VoteType {get;set;}
        public string VoteIndex{get;set;}
    }

    public class YearType
    {
        public string Text { get; set; }
        public bool IsChecked { get; set; }
    }
}
