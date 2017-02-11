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

namespace Aide
{
    public partial class Form_YC : Form
    {
        YiChe yc;
        List<TextValue> promotionType = new List<TextValue>();
        HtmlAgilityPack.HtmlDocument doc;
        string url = "";
        string rdoStoreState = "";
        string carid = "";
        string cbid = "";
        string energytype = "";
        string csshowname = "";
        PromotionCars cars;
        int NewsType = 1;
        List<TextValue> BusinessTax = new List<TextValue>();
        List<TextValue> TrafficTax = new List<TextValue>();        


        List<TextValue> discountRate = new List<TextValue>();
        List<TextValue> PurchaseTax = new List<TextValue>();
        List<TextValue> CompulsoryInsurance = new List<TextValue>();
        List<TextValue> CommercialInsurance = new List<TextValue>();
        string[] strCompulsory = { "不赠送交强险", "赠送1年交强险", "赠送2年交强险", "赠送3年交强险", "赠送4年交强险", "赠送5年交强险", "赠送6年交强险", "赠送7年交强险", "赠送8年交强险", "赠送9年交强险" };

        string[] strCommercial = { "不赠送商业险", "赠送1年商业险", "赠送2年商业险", "赠送3年商业险", "赠送4年商业险", "赠送5年商业险", "赠送6年商业险", "赠送7年商业险", "赠送8年商业险", "赠送9年商业险" };
        public Form_YC(YiChe yc)
        {
            InitializeComponent();
            this.yc = yc;
            promotionType.AddRange(new[] { new TextValue { Text = "优惠金额", Value = "0" }, new TextValue { Text = "优惠折扣率", Value = "1" } });
            ddlPromotionType.DataSource = promotionType;
            ddlPromotionType.DisplayMember = "Text";
            ddlPromotionType.ValueMember = "Value";
            ddlPromotionType.SelectedIndex = 0;

            for (int i = 1; i < 10;i++ )
            {
                BusinessTax.Add(new TextValue { Text = i+"年", Value = i.ToString() });
                TrafficTax.Add(new TextValue { Text = i + "年", Value = i.ToString() });
            }
            ddlBusinessTax.DataSource = BusinessTax;
            ddlBusinessTax.DisplayMember = "Text";
            ddlBusinessTax.ValueMember = "Value";
            ddlBusinessTax.SelectedIndex = 0;

            ddlTrafficTax.DataSource = TrafficTax;
            ddlTrafficTax.DisplayMember = "Text";
            ddlTrafficTax.ValueMember = "Value";
            ddlTrafficTax.SelectedIndex = 0;


            discountRate.AddRange(new[] { new TextValue { Text = "优惠金额", Value = "0" }, new TextValue { Text = "优惠折扣率", Value = "1" } });

            PurchaseTax.AddRange(new[] { new TextValue { Text = "不赠送购置税", Value = "0" }, new TextValue { Text = "赠送50%购置税", Value = "50" }, new TextValue { Text = "赠送100%购置税", Value = "100" } });

            for (int i = 0; i < strCompulsory.Length; i++)
            {
                CompulsoryInsurance.Add(new TextValue { Text = strCompulsory[i], Value = i.ToString() });
            }

            for (int i = 0; i < strCommercial.Length; i++)
            {
                CommercialInsurance.Add(new TextValue { Text = strCommercial[i], Value = i.ToString() });
            }

            InitForm(rbtSource1.Tag.ToString());
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
                this.gpbCarList.Controls.Add(rbt);
            }
            #endregion

            dtpPromotionB.Value = DateTime.Now.AddMonths(1);
            ddlPromotionType.DataSource = promotionType;
            ddlPromotionType.DisplayMember = "Text";
            ddlPromotionType.ValueMember = "Value";
            ddlPromotionType.SelectedIndex = 0;

            #region 库存状态
            var stateList = doc.GetElementbyId("rdoStoreState");
            var stateInputs = stateList.SelectNodes(".//input");
            xstep = 113;
            xstart = 4;
            ystart = 7;
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
                this.pStoreState.Controls.Add(rbt);
            }
            #endregion

            #region 礼包

            var giftbox = doc.GetElementbyId("giftBox");
            var tiptextarea = giftbox.SelectSingleNode(".//p[@class='tip_textarea']");


            #region 礼包内容
            var giftdl = giftbox.SelectNodes(".//div[@class='jsTrigger']/input");
            //<div class="jsTrigger">
            //    <input type="checkbox" id="gift1" name="" data-jsindex="0"><label for="gift1">汽车用品</label>
            //    <input type="checkbox" id="gift2" name="" data-jsindex="1"><label for="gift2">油卡</label>
            //    <input type="checkbox" id="gift3" name="" data-jsindex="2"><label for="gift3">商业险</label>
            //    <input type="checkbox" id="gift4" name="" data-jsindex="3"><label for="gift4">交强险</label>
            //    <input type="checkbox" id="gift5" name="" data-jsindex="4"><label for="gift5">购置税</label>
            //    <input type="checkbox" id="gift6" name="" data-jsindex="5"><label for="gift6">保养</label>
            //    <input type="checkbox" id="gift7" name="" data-jsindex="6"><label for="gift7">其它</label>
            //</div>
            #endregion

            #endregion
        }

        private void InitDetail()
        {
            var article = doc.GetElementbyId("title_article");
            title_article.Text = article.GetAttributeValue("backvalue", "");
            var number = doc.GetElementbyId("title_number");
            title_number.Text = number.InnerText;
            var lead = doc.GetElementbyId("txtLead");
            if (lead != null)
                txtLead.Text = lead.GetAttributeValue("backvalue", "");
            #region 颜色列表
            var colorList = doc.DocumentNode.SelectNodes("//div[@id='UpdatePanel7']/span/label");
            int xstep = 88;
            int ystep = 22;
            int xstart = 8;
            int ystart = 30;
            for (int i = 0; i < colorList.Count; i++)
            {
                var value = colorList[i].GetAttributeValue("for", "");
                var title = colorList[i].GetAttributeValue("title", "");

                int x = xstart;
                int y = ystart;

                if (i > 0)
                {
                    x = xstart + (xstep * (i % 3));
                    y = ystart + (ystep * (i / 3));
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
                this.pColor.Controls.Add(chk);
            }
            #endregion

            #region 车型
            cars = new PromotionCars();
            var yeartype = doc.DocumentNode.SelectNodes("//input[@name='chklYearType']");
            yeartype.ToList().ForEach(f => cars.YearType.Add(f.GetAttributeValue("value", "")));

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

                    var colorcount = tds[7].SelectSingleNode(".//div/p/span");
                    cars.Cars.Add(new Car
                    {
                        CarID = Convert.ToInt32(inputCarInfo.GetAttributeValue("carid", "")),
                        Discount = Convert.ToDecimal(inputrate.GetAttributeValue("value", "0")),
                        IsAllowance = inputallowance != null ? inputallowance.GetAttributeValue("checked", "") : "",
                        IsCheck = true,
                        TypeName = tds[0].GetAttributeValue("title", ""),
                        YearType = typeinput.GetAttributeValue("yeartype", ""),

                        Subsidies = tds[4].InnerText,
                        StoreState = 1,
                        CarReferPrice = Convert.ToDecimal(inputCarInfo.GetAttributeValue("carreferprice", "0")),
                        FavorablePrice = Convert.ToDecimal(inputprice.GetAttributeValue("value", "0")),
                        ColorName = colorcount.InnerText,
                        PushedCount = tds[8].InnerText.Trim()
                    });
                }
            }
            var note = doc.GetElementbyId("LimitCarListNote");
            cars.Note = note.InnerText.Trim();

            var publistCarList = doc.DocumentNode.SelectNodes("//div[@id='promocarstable']/table/tbody/tr");
            foreach (HtmlNode node in publistCarList)
            {
                var tr = node.GetAttributeValue("mark", "");
                if (!string.IsNullOrWhiteSpace(tr))
                {
                    var tds = node.SelectNodes(".//td");
                    var typeinput = tds[0].SelectSingleNode(".//input[@type='checkbox']");
                    cars.PublishCarList.Add(new Car { CarID = Convert.ToInt32(typeinput.GetAttributeValue("value", "")) });
                }
            }
            #endregion

            #region 图片
            var imgLogo = doc.GetElementbyId("imgLogo");
            imgLogo_hdf.ImgUrl = imgLogo.GetAttributeValue("src", "");
            var imgPosition1 = doc.GetElementbyId("imgPosition1");
            imgPosition1_hdf.ImgUrl = imgPosition1.GetAttributeValue("src", "");
            var imgPosition2 = doc.GetElementbyId("imgPosition2");
            imgPosition2_hdf.ImgUrl = imgPosition2.GetAttributeValue("src", "");
            var imgPosition3 = doc.GetElementbyId("imgPosition3");
            imgPosition3_hdf.ImgUrl = imgPosition3.GetAttributeValue("src", "");
            var imgPosition4 = doc.GetElementbyId("imgPosition4");
            imgPosition4_hdf.ImgUrl = imgPosition4.GetAttributeValue("src", "");
            #endregion

            carA.CarDataSource = cars;
            carA.ShowType(false);

            carControl1.CarDataSource = cars;
            carControl1.ShowType(true);
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

        private string PushData()
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
            sb.AppendFormat("hdfCSShowName={0}&", "%E9%9B%B7%E5%85%8B%E8%90%A8%E6%96%AFLS&");//HttpHelper.URLEncode(csshowname)
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
            //sb.AppendFormat("hdfCarNewsList={0}&", HttpHelper.URLEncode(inputvalue));
            sb.AppendFormat("hdfCarNewsList={0}&", "%5B%7B%22title%22%3A%22%E9%9B%B7%E5%85%8B%E8%90%A8%E6%96%AFLS%E4%BC%98%E6%83%A0%E9%AB%98%E8%BE%BE0.01%E4%B8%87%E5%85%83%22%2C%22startdate%22%3A%222017-02-11%22%2C%22enddate%22%3A%222017-02-11%22%2C%22status%22%3A%22%E4%BF%83%E9%94%80%E4%B8%AD%22%2C%22statusvalue%22%3A1%2C%22carid%22%3A116345%2C%22mark%22%3A0%2C%22extendcarid%22%3A0%20%7D%2C%7B%22title%22%3A%22%E9%9B%B7%E5%85%8B%E8%90%A8%E6%96%AFLS%E4%BC%98%E6%83%A0%E9%AB%98%E8%BE%BE0.01%E4%B8%87%E5%85%83%22%2C%22startdate%22%3A%222017-02-11%22%2C%22enddate%22%3A%222017-02-11%22%2C%22status%22%3A%22%E4%BF%83%E9%94%80%E4%B8%AD%22%2C%22statusvalue%22%3A1%2C%22carid%22%3A116346%2C%22mark%22%3A0%2C%22extendcarid%22%3A0%20%7D%2C%7B%22title%22%3A%22%E9%9B%B7%E5%85%8B%E8%90%A8%E6%96%AFLS%E4%BC%98%E6%83%A0%E9%AB%98%E8%BE%BE0.01%E4%B8%87%E5%85%83%22%2C%22startdate%22%3A%222017-02-11%22%2C%22enddate%22%3A%222017-02-11%22%2C%22status%22%3A%22%E4%BF%83%E9%94%80%E4%B8%AD%22%2C%22statusvalue%22%3A1%2C%22carid%22%3A116347%2C%22mark%22%3A0%2C%22extendcarid%22%3A0%20%7D%2C%7B%22title%22%3A%22%E9%9B%B7%E5%85%8B%E8%90%A8%E6%96%AFLS%E4%BC%98%E6%83%A0%E9%AB%98%E8%BE%BE0.01%E4%B8%87%E5%85%83%22%2C%22startdate%22%3A%222017-02-11%22%2C%22enddate%22%3A%222017-02-11%22%2C%22status%22%3A%22%E4%BF%83%E9%94%80%E4%B8%AD%22%2C%22statusvalue%22%3A1%2C%22carid%22%3A116348%2C%22mark%22%3A0%2C%22extendcarid%22%3A0%20%7D%2C%7B%22title%22%3A%22%E9%9B%B7%E5%85%8B%E8%90%A8%E6%96%AFLS%E4%BC%98%E6%83%A0%E9%AB%98%E8%BE%BE0.01%E4%B8%87%E5%85%83%22%2C%22startdate%22%3A%222017-02-12%22%2C%22enddate%22%3A%222017-02-12%22%2C%22status%22%3A%22%E6%9C%AA%E5%BC%80%E5%A7%8B%22%2C%22statusvalue%22%3A0%2C%22carid%22%3A116345%2C%22mark%22%3A0%2C%22extendcarid%22%3A0%20%7D%2C%7B%22title%22%3A%22%E9%9B%B7%E5%85%8B%E8%90%A8%E6%96%AFLS%E4%BC%98%E6%83%A0%E9%AB%98%E8%BE%BE0.01%E4%B8%87%E5%85%83%22%2C%22startdate%22%3A%222017-02-12%22%2C%22enddate%22%3A%222017-02-12%22%2C%22status%22%3A%22%E6%9C%AA%E5%BC%80%E5%A7%8B%22%2C%22statusvalue%22%3A0%2C%22carid%22%3A116346%2C%22mark%22%3A0%2C%22extendcarid%22%3A0%20%7D%2C%7B%22title%22%3A%22%E9%9B%B7%E5%85%8B%E8%90%A8%E6%96%AFLS%E4%BC%98%E6%83%A0%E9%AB%98%E8%BE%BE0.01%E4%B8%87%E5%85%83%22%2C%22startdate%22%3A%222017-02-12%22%2C%22enddate%22%3A%222017-02-12%22%2C%22status%22%3A%22%E6%9C%AA%E5%BC%80%E5%A7%8B%22%2C%22statusvalue%22%3A0%2C%22carid%22%3A116347%2C%22mark%22%3A0%2C%22extendcarid%22%3A0%20%7D%2C%7B%22title%22%3A%22%E9%9B%B7%E5%85%8B%E8%90%A8%E6%96%AFLS%E4%BC%98%E6%83%A0%E9%AB%98%E8%BE%BE0.01%E4%B8%87%E5%85%83%22%2C%22startdate%22%3A%222017-02-12%22%2C%22enddate%22%3A%222017-02-12%22%2C%22status%22%3A%22%E6%9C%AA%E5%BC%80%E5%A7%8B%22%2C%22statusvalue%22%3A0%2C%22carid%22%3A116348%2C%22mark%22%3A0%2C%22extendcarid%22%3A0%20%7D%5D");

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

            for (int i = 0; i < cars.Cars.Count; i++)
            {
                sb.AppendFormat("{0}={1}&", string.Format("rptFavourableList%24ctl{0:00}%24checkbox", i), cars.Cars[i].CarID);
                sb.AppendFormat("{0}={1}&", string.Format("rptFavourableList%24ctl{0:00}%24rate", i), cars.Cars[i].Discount);
                sb.AppendFormat("{0}={1}&", string.Format("rptFavourableList%24ctl{0:00}%24Text1", i), cars.Cars[i].FavorablePrice);
            }

            for (int i = 0; i < cars.PublishCarList.Count; i++)
            {
                sb.AppendFormat("{0}={1}&", string.Format("rtpNotPushCarList%24ctl{0:00}%24checkbox", i), cars.PublishCarList[i].CarID);
            }
            sb.Append("NewEnergyTitleTemplate=2&");
            sb.AppendFormat("title_article={0}&", HttpHelper.URLEncode(title_article.Text));
            sb.AppendFormat("rdoStoreState={0}&", rdoStoreState);
            sb.AppendFormat("txtLead={0}&txtPrice={1}&ddlBusinessTax={2}&", HttpHelper.URLEncode(txtLead.Text), txtPrice.Text, ddlBusinessTax.SelectedValue);
            sb.AppendFormat("ddlTrafficTax={0}&PurchaseTax={1}&", ddlTrafficTax.SelectedValue, 1);//txtPurchaseTax.Text
            sb.AppendFormat("txtOtherInfo={0}&", HttpHelper.URLEncode(txtOtherInfo.Text));
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

            sb.AppendFormat("hdnType={0}&hdnCarMerchandiseID=&", ddlPromotionType.SelectedValue == "0" ? "money" : "rate");
            sb.AppendFormat("hdnPromotionType={0}&", ddlPromotionType.SelectedIndex);
            sb.AppendFormat("hdfCarInfoJson={0}&hdfGiftInfo={1}&", HttpHelper.URLEncode(cars.CarInfoJson), HttpHelper.URLEncode(cars.GiftInofJson));
            sb.Append("imgUploadChangehidethumburl=&imgUploadChangehideUrl=&txtStartPrice=&");
            sb.Append("txtEndPrice=&txtKeyword=&__EVENTTARGET=&__EVENTARGUMENT=&");

            var viewstate = doc.GetElementbyId("__VIEWSTATE");
            if (viewstate != null)
                str_viewstate = viewstate.GetAttributeValue("value", "");
            sb.AppendFormat("__VIEWSTATE={0}&", "%2FwEPDwUKMTcyOTg5MDk4OA8WBB4JQ2FyU2VyaWFsMt8jAAEAAAD%2F%2F%2F%2F%2FAQAAAAAAAAAMAgAAAE5TeXN0ZW0uRGF0YSwgVmVyc2lvbj00LjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPWI3N2E1YzU2MTkzNGUwODkFAQAAABVTeXN0ZW0uRGF0YS5EYXRhVGFibGUDAAAAGURhdGFUYWJsZS5SZW1vdGluZ1ZlcnNpb24JWG1sU2NoZW1hC1htbERpZmZHcmFtAwEBDlN5c3RlbS5WZXJzaW9uAgAAAAkDAAAABgQAAADVCDw%2FeG1sIHZlcnNpb249IjEuMCIgZW5jb2Rpbmc9InV0Zi0xNiI%2FPg0KPHhzOnNjaGVtYSB4bWxucz0iIiB4bWxuczp4cz0iaHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEiIHhtbG5zOm1zZGF0YT0idXJuOnNjaGVtYXMtbWljcm9zb2Z0LWNvbTp4bWwtbXNkYXRhIj4NCiAgPHhzOmVsZW1lbnQgbmFtZT0iVGFibGUiPg0KICAgIDx4czpjb21wbGV4VHlwZT4NCiAgICAgIDx4czpzZXF1ZW5jZT4NCiAgICAgICAgPHhzOmVsZW1lbnQgbmFtZT0iQ1NJRCIgdHlwZT0ieHM6aW50IiBtc2RhdGE6dGFyZ2V0TmFtZXNwYWNlPSIiIG1pbk9jY3Vycz0iMCIgLz4NCiAgICAgICAgPHhzOmVsZW1lbnQgbmFtZT0iQ1NTaG93TmFtZSIgdHlwZT0ieHM6c3RyaW5nIiBtc2RhdGE6dGFyZ2V0TmFtZXNwYWNlPSIiIG1pbk9jY3Vycz0iMCIgLz4NCiAgICAgICAgPHhzOmVsZW1lbnQgbmFtZT0iQ0JJRCIgdHlwZT0ieHM6aW50IiBtc2RhdGE6dGFyZ2V0TmFtZXNwYWNlPSIiIG1pbk9jY3Vycz0iMCIgLz4NCiAgICAgICAgPHhzOmVsZW1lbnQgbmFtZT0iQ0JOYW1lIiB0eXBlPSJ4czpzdHJpbmciIG1zZGF0YTp0YXJnZXROYW1lc3BhY2U9IiIgbWluT2NjdXJzPSIwIiAvPg0KICAgICAgICA8eHM6ZWxlbWVudCBuYW1lPSJQcm9tQ291bnQiIHR5cGU9InhzOmludCIgbXNkYXRhOnRhcmdldE5hbWVzcGFjZT0iIiBtaW5PY2N1cnM9IjAiIC8%2BDQogICAgICAgIDx4czplbGVtZW50IG5hbWU9IkNTRW5lcmd5VHlwZSIgdHlwZT0ieHM6aW50IiBtc2RhdGE6dGFyZ2V0TmFtZXNwYWNlPSIiIG1pbk9jY3Vycz0iMCIgLz4NCiAgICAgIDwveHM6c2VxdWVuY2U%2BDQogICAgPC94czpjb21wbGV4VHlwZT4NCiAgPC94czplbGVtZW50Pg0KICA8eHM6ZWxlbWVudCBuYW1lPSJ0bXBEYXRhU2V0IiBtc2RhdGE6SXNEYXRhU2V0PSJ0cnVlIiBtc2RhdGE6TWFpbkRhdGFUYWJsZT0iVGFibGUiIG1zZGF0YTpVc2VDdXJyZW50TG9jYWxlPSJ0cnVlIj4NCiAgICA8eHM6Y29tcGxleFR5cGU%2BDQogICAgICA8eHM6Y2hvaWNlIG1pbk9jY3Vycz0iMCIgbWF4T2NjdXJzPSJ1bmJvdW5kZWQiIC8%2BDQogICAgPC94czpjb21wbGV4VHlwZT4NCiAgPC94czplbGVtZW50Pg0KPC94czpzY2hlbWE%2BBgUAAADdGDxkaWZmZ3I6ZGlmZmdyYW0geG1sbnM6bXNkYXRhPSJ1cm46c2NoZW1hcy1taWNyb3NvZnQtY29tOnhtbC1tc2RhdGEiIHhtbG5zOmRpZmZncj0idXJuOnNjaGVtYXMtbWljcm9zb2Z0LWNvbTp4bWwtZGlmZmdyYW0tdjEiPg0KICA8dG1wRGF0YVNldD4NCiAgICA8VGFibGUgZGlmZmdyOmlkPSJUYWJsZTEiIG1zZGF0YTpyb3dPcmRlcj0iMCI%2BDQogICAgICA8Q1NJRD4xOTk2PC9DU0lEPg0KICAgICAgPENTU2hvd05hbWU%2B6Zu35YWL6JCo5pavTFM8L0NTU2hvd05hbWU%2BDQogICAgICA8Q0JJRD4yMDAyNjwvQ0JJRD4NCiAgICAgIDxDQk5hbWU%2B6Zu35YWL6JCo5pavPC9DQk5hbWU%2BDQogICAgICA8UHJvbUNvdW50PjI8L1Byb21Db3VudD4NCiAgICAgIDxDU0VuZXJneVR5cGU%2BMTwvQ1NFbmVyZ3lUeXBlPg0KICAgIDwvVGFibGU%2BDQogICAgPFRhYmxlIGRpZmZncjppZD0iVGFibGUyIiBtc2RhdGE6cm93T3JkZXI9IjEiPg0KICAgICAgPENTSUQ%2BMjA3MjwvQ1NJRD4NCiAgICAgIDxDU1Nob3dOYW1lPumbt%2BWFi%2BiQqOaWr0dTPC9DU1Nob3dOYW1lPg0KICAgICAgPENCSUQ%2BMjAwMjY8L0NCSUQ%2BDQogICAgICA8Q0JOYW1lPumbt%2BWFi%2BiQqOaWrzwvQ0JOYW1lPg0KICAgICAgPFByb21Db3VudD4yPC9Qcm9tQ291bnQ%2BDQogICAgICA8Q1NFbmVyZ3lUeXBlPjE8L0NTRW5lcmd5VHlwZT4NCiAgICA8L1RhYmxlPg0KICAgIDxUYWJsZSBkaWZmZ3I6aWQ9IlRhYmxlMyIgbXNkYXRhOnJvd09yZGVyPSIyIj4NCiAgICAgIDxDU0lEPjIwNzM8L0NTSUQ%2BDQogICAgICA8Q1NTaG93TmFtZT7pm7flhYvokKjmlq9MWDwvQ1NTaG93TmFtZT4NCiAgICAgIDxDQklEPjIwMDI2PC9DQklEPg0KICAgICAgPENCTmFtZT7pm7flhYvokKjmlq88L0NCTmFtZT4NCiAgICAgIDxQcm9tQ291bnQ%2BMjwvUHJvbUNvdW50Pg0KICAgICAgPENTRW5lcmd5VHlwZT4yPC9DU0VuZXJneVR5cGU%2BDQogICAgPC9UYWJsZT4NCiAgICA8VGFibGUgZGlmZmdyOmlkPSJUYWJsZTQiIG1zZGF0YTpyb3dPcmRlcj0iMyI%2BDQogICAgICA8Q1NJRD4yMTA5PC9DU0lEPg0KICAgICAgPENTU2hvd05hbWU%2B6Zu35YWL6JCo5pavRVM8L0NTU2hvd05hbWU%2BDQogICAgICA8Q0JJRD4yMDAyNjwvQ0JJRD4NCiAgICAgIDxDQk5hbWU%2B6Zu35YWL6JCo5pavPC9DQk5hbWU%2BDQogICAgICA8UHJvbUNvdW50PjI8L1Byb21Db3VudD4NCiAgICAgIDxDU0VuZXJneVR5cGU%2BMTwvQ1NFbmVyZ3lUeXBlPg0KICAgIDwvVGFibGU%2BDQogICAgPFRhYmxlIGRpZmZncjppZD0iVGFibGU1IiBtc2RhdGE6cm93T3JkZXI9IjQiPg0KICAgICAgPENTSUQ%2BMjExMDwvQ1NJRD4NCiAgICAgIDxDU1Nob3dOYW1lPumbt%2BWFi%2BiQqOaWr0lTPC9DU1Nob3dOYW1lPg0KICAgICAgPENCSUQ%2BMjAwMjY8L0NCSUQ%2BDQogICAgICA8Q0JOYW1lPumbt%2BWFi%2BiQqOaWrzwvQ0JOYW1lPg0KICAgICAgPFByb21Db3VudD4yPC9Qcm9tQ291bnQ%2BDQogICAgICA8Q1NFbmVyZ3lUeXBlPjI8L0NTRW5lcmd5VHlwZT4NCiAgICA8L1RhYmxlPg0KICAgIDxUYWJsZSBkaWZmZ3I6aWQ9IlRhYmxlNiIgbXNkYXRhOnJvd09yZGVyPSI1Ij4NCiAgICAgIDxDU0lEPjIxMzA8L0NTSUQ%2BDQogICAgICA8Q1NTaG93TmFtZT7pm7flhYvokKjmlq9SWDwvQ1NTaG93TmFtZT4NCiAgICAgIDxDQklEPjIwMDI2PC9DQklEPg0KICAgICAgPENCTmFtZT7pm7flhYvokKjmlq88L0NCTmFtZT4NCiAgICAgIDxQcm9tQ291bnQ%2BMjwvUHJvbUNvdW50Pg0KICAgICAgPENTRW5lcmd5VHlwZT4xPC9DU0VuZXJneVR5cGU%2BDQogICAgPC9UYWJsZT4NCiAgICA8VGFibGUgZGlmZmdyOmlkPSJUYWJsZTciIG1zZGF0YTpyb3dPcmRlcj0iNiI%2BDQogICAgICA8Q1NJRD4yNTAxPC9DU0lEPg0KICAgICAgPENTU2hvd05hbWU%2B6Zu35YWL6JCo5pavR1g8L0NTU2hvd05hbWU%2BDQogICAgICA8Q0JJRD4yMDAyNjwvQ0JJRD4NCiAgICAgIDxDQk5hbWU%2B6Zu35YWL6JCo5pavPC9DQk5hbWU%2BDQogICAgICA8UHJvbUNvdW50PjI8L1Byb21Db3VudD4NCiAgICAgIDxDU0VuZXJneVR5cGU%2BMjwvQ1NFbmVyZ3lUeXBlPg0KICAgIDwvVGFibGU%2BDQogICAgPFRhYmxlIGRpZmZncjppZD0iVGFibGU4IiBtc2RhdGE6cm93T3JkZXI9IjciPg0KICAgICAgPENTSUQ%2BMjYwMjwvQ1NJRD4NCiAgICAgIDxDU1Nob3dOYW1lPumbt%2BWFi%2BiQqOaWr0xGLUE8L0NTU2hvd05hbWU%2BDQogICAgICA8Q0JJRD4yMDAyNjwvQ0JJRD4NCiAgICAgIDxDQk5hbWU%2B6Zu35YWL6JCo5pavPC9DQk5hbWU%2BDQogICAgICA8UHJvbUNvdW50PjI8L1Byb21Db3VudD4NCiAgICAgIDxDU0VuZXJneVR5cGU%2BMjwvQ1NFbmVyZ3lUeXBlPg0KICAgIDwvVGFibGU%2BDQogICAgPFRhYmxlIGRpZmZncjppZD0iVGFibGU5IiBtc2RhdGE6cm93T3JkZXI9IjgiPg0KICAgICAgPENTSUQ%2BMjk3NzwvQ1NJRD4NCiAgICAgIDxDU1Nob3dOYW1lPumbt%2BWFi%2BiQqOaWr0NUPC9DU1Nob3dOYW1lPg0KICAgICAgPENCSUQ%2BMjAwMjY8L0NCSUQ%2BDQogICAgICA8Q0JOYW1lPumbt%2BWFi%2BiQqOaWrzwvQ0JOYW1lPg0KICAgICAgPFByb21Db3VudD4yPC9Qcm9tQ291bnQ%2BDQogICAgICA8Q1NFbmVyZ3lUeXBlPjA8L0NTRW5lcmd5VHlwZT4NCiAgICA8L1RhYmxlPg0KICAgIDxUYWJsZSBkaWZmZ3I6aWQ9IlRhYmxlMTAiIG1zZGF0YTpyb3dPcmRlcj0iOSI%2BDQogICAgICA8Q1NJRD40MTY1PC9DU0lEPg0KICAgICAgPENTU2hvd05hbWU%2B6Zu35YWL6JCo5pavTlg8L0NTU2hvd05hbWU%2BDQogICAgICA8Q0JJRD4yMDAyNjwvQ0JJRD4NCiAgICAgIDxDQk5hbWU%2B6Zu35YWL6JCo5pavPC9DQk5hbWU%2BDQogICAgICA8UHJvbUNvdW50PjI8L1Byb21Db3VudD4NCiAgICAgIDxDU0VuZXJneVR5cGU%2BMTwvQ1NFbmVyZ3lUeXBlPg0KICAgIDwvVGFibGU%2BDQogICAgPFRhYmxlIGRpZmZncjppZD0iVGFibGUxMSIgbXNkYXRhOnJvd09yZGVyPSIxMCI%2BDQogICAgICA8Q1NJRD40NjU3PC9DU0lEPg0KICAgICAgPENTU2hvd05hbWU%2B6Zu35YWL6JCo5pavUkM8L0NTU2hvd05hbWU%2BDQogICAgICA8Q0JJRD4yMDAyNjwvQ0JJRD4NCiAgICAgIDxDQk5hbWU%2B6Zu35YWL6JCo5pavPC9DQk5hbWU%2BDQogICAgICA8UHJvbUNvdW50PjI8L1Byb21Db3VudD4NCiAgICAgIDxDU0VuZXJneVR5cGU%2BMjwvQ1NFbmVyZ3lUeXBlPg0KICAgIDwvVGFibGU%2BDQogIDwvdG1wRGF0YVNldD4NCjwvZGlmZmdyOmRpZmZncmFtPgQDAAAADlN5c3RlbS5WZXJzaW9uBAAAAAZfTWFqb3IGX01pbm9yBl9CdWlsZAlfUmV2aXNpb24AAAAACAgICAIAAAAAAAAA%2F%2F%2F%2F%2F%2F%2F%2F%2F%2F8LHhNWYWxpZGF0ZVJlcXVlc3RNb2RlAgEWAgIDD2QWDAILD2QWAgIBDxYCHgtfIUl0ZW1Db3VudAIBFgJmD2QWBAICDxUBDOmbt%2BWFi%2BiQqOaWr2QCAw8WAh8CAgsWFmYPZBYCZg8VCgExBTIwMDI2BDE5OTYEMTk5NgQxOTk2Dumbt%2BWFi%2BiQqOaWr0xTATIO6Zu35YWL6JCo5pavTFMO6Zu35YWL6JCo5pavTFMBMmQCAQ9kFgJmDxUKATEFMjAwMjYEMjA3MgQyMDcyBDIwNzIO6Zu35YWL6JCo5pavR1MBMg7pm7flhYvokKjmlq9HUw7pm7flhYvokKjmlq9HUwEyZAICD2QWAmYPFQoBMgUyMDAyNgQyMDczBDIwNzMEMjA3Mw7pm7flhYvokKjmlq9MWAEyDumbt%2BWFi%2BiQqOaWr0xYDumbt%2BWFi%2BiQqOaWr0xYATJkAgMPZBYCZg8VCgExBTIwMDI2BDIxMDkEMjEwOQQyMTA5Dumbt%2BWFi%2BiQqOaWr0VTATIO6Zu35YWL6JCo5pavRVMO6Zu35YWL6JCo5pavRVMBMmQCBA9kFgJmDxUKATIFMjAwMjYEMjExMAQyMTEwBDIxMTAO6Zu35YWL6JCo5pavSVMBMg7pm7flhYvokKjmlq9JUw7pm7flhYvokKjmlq9JUwEyZAIFD2QWAmYPFQoBMQUyMDAyNgQyMTMwBDIxMzAEMjEzMA7pm7flhYvokKjmlq9SWAEyDumbt%2BWFi%2BiQqOaWr1JYDumbt%2BWFi%2BiQqOaWr1JYATJkAgYPZBYCZg8VCgEyBTIwMDI2BDI1MDEEMjUwMQQyNTAxDumbt%2BWFi%2BiQqOaWr0dYATIO6Zu35YWL6JCo5pavR1gO6Zu35YWL6JCo5pavR1gBMmQCBw9kFgJmDxUKATIFMjAwMjYEMjYwMgQyNjAyBDI2MDIQ6Zu35YWL6JCo5pavTEYtQQEyEOmbt%2BWFi%2BiQqOaWr0xGLUEQ6Zu35YWL6JCo5pavTEYtQQEyZAIID2QWAmYPFQoBMAUyMDAyNgQyOTc3BDI5NzcEMjk3Nw7pm7flhYvokKjmlq9DVAEyDumbt%2BWFi%2BiQqOaWr0NUDumbt%2BWFi%2BiQqOaWr0NUATJkAgkPZBYCZg8VCgExBTIwMDI2BDQxNjUENDE2NQQ0MTY1Dumbt%2BWFi%2BiQqOaWr05YATIO6Zu35YWL6JCo5pavTlgO6Zu35YWL6JCo5pavTlgBMmQCCg9kFgJmDxUKATIFMjAwMjYENDY1NwQ0NjU3BDQ2NTcO6Zu35YWL6JCo5pavUkMBMg7pm7flhYvokKjmlq9SQw7pm7flhYvokKjmlq9SQwEyZAIND2QWAmYPZBYGAgUPFgIfAgICFgQCAQ9kFgJmDxUCBDIwMTYEMjAxNmQCAg9kFgJmDxUCBDIwMTQEMjAxNGQCBg9kFgJmD2QWBgIBDxYCHwICBBYIZg9kFgpmDxUIATABMAEwAi0tAi0tFjQ2MEwg5Yqg6ZW%2F54mIIDIwMTTmrL4GMTA1NDQ5AzE0OWQCAQ8WBB4IeWVhcnR5cGUFBDIwMTQeBXZhbHVlBQYxMDU0NDlkAgIPFQIWNDYwTCDliqDplb%2FniYggMjAxNOasvgMxNDlkAgUPFgIeC2V4dGVuZGNhcmlkBQEwZAIGDxUCGjxwICBjbGFzcz0nY2hlY2tfcCc%2BLS08L3A%2BATBkAgEPZBYKZg8VCAEwATABMAItLQItLRw0NjBMIOixquWNjuWKoOmVv%2BeJiCAyMDE05qy%2BBjEwNTQ1MAUxNjMuOGQCAQ8WBB8DBQQyMDE0HwQFBjEwNTQ1MGQCAg8VAhw0NjBMIOixquWNjuWKoOmVv%2BeJiCAyMDE05qy%2BBTE2My44ZAIFDxYCHwUFATBkAgYPFQIaPHAgIGNsYXNzPSdjaGVja19wJz4tLTwvcD4BMGQCAg9kFgpmDxUIATABMAEwAi0tAi0tIjQ2MEwg6LGq5Y2O5Yqg6ZW%2F5YWo6amx54mIIDIwMTTmrL4GMTA1NDUzBTE4MC4zZAIBDxYEHwMFBDIwMTQfBAUGMTA1NDUzZAICDxUCIjQ2MEwg6LGq5Y2O5Yqg6ZW%2F5YWo6amx54mIIDIwMTTmrL4FMTgwLjNkAgUPFgIfBQUBMGQCBg8VAho8cCAgY2xhc3M9J2NoZWNrX3AnPi0tPC9wPgEwZAIDD2QWCmYPFQgBMAEwATEBMAEwDTYwMGhMIDIwMTTmrL4GMTA1NDU0BTIzOC44ZAIBDxYEHwMFBDIwMTQfBAUGMTA1NDU0ZAICDxUCDTYwMGhMIDIwMTTmrL4FMjM4LjhkAgUPFgIfBQUBMGQCBg8VAkA8cCAgY2xhc3M9J2NoZWNrX3AnPuWbveWutjowPC9wPjxwICBjbGFzcz0nY2hlY2tfcCc%2B5Zyw5pa5OjA8L3A%2BATBkAgMPFgIeBFRleHQFATRkAgUPFgIfAgIEFghmD2QWBmYPFQMMNDYwTCAyMDE25qy%2BBjExNjM0NQMxNDlkAgEPFgQfAwUEMjAxNh8EBQYxMTYzNDVkAgIPFQIMNDYwTCAyMDE25qy%2BATJkAgEPZBYGZg8VAxY0NjBMIOixquWNjueJiCAyMDE25qy%2BBjExNjM0NgUxNjMuOGQCAQ8WBB8DBQQyMDE2HwQFBjExNjM0NmQCAg8VAhY0NjBMIOixquWNjueJiCAyMDE25qy%2BATJkAgIPZBYGZg8VAxw0NjBMIOWbm%2BmpseWwiui0teeJiCAyMDE25qy%2BBjExNjM0NwUxODAuM2QCAQ8WBB8DBQQyMDE2HwQFBjExNjM0N2QCAg8VAhw0NjBMIOWbm%2BmpseWwiui0teeJiCAyMDE25qy%2BATJkAgMPZBYGZg8VAw02MDBoTCAyMDE25qy%2BBjExNjM0OAUyMzguOGQCAQ8WBB8DBQQyMDE2HwQFBjExNjM0OGQCAg8VAg02MDBoTCAyMDE25qy%2BATJkAgcPZBYCAgIPZBYCZg9kFgICAQ8WAh8CAicWTmYPZBYCZg8VBQUxODI4MQUxODI4MQUxODI4MRLnmb3ph5HngbDph5HlsZ7oibIS55m96YeR54Gw6YeR5bGe6ImyZAIBD2QWAmYPFQUEMjA5MgQyMDkyBDIwOTIG55m96ImyBueZveiJsmQCAg9kFgJmDxUFBDIwODkEMjA4OQQyMDg5DOWuneefs%2Bm7keiJsgzlrp3nn7Ppu5HoibJkAgMPZBYCZg8VBQQyMDkzBDIwOTMEMjA5Mwzlrp3nn7Pok53oibIM5a6d55%2Bz6JOd6ImyZAIED2QWAmYPFQUFMTk2NzIFMTk2NzIFMTk2NzIS6LaF6Z%2Bz6YCf6ZKb6ZO26ImyEui2hemfs%2BmAn%2BmSm%2BmTtuiJsmQCBQ9kFgJmDxUFBDk1NzIEOTU3MgQ5NTcyD%2Bi2hemfs%2BmAn%2BmTtuiJsg%2FotoXpn7PpgJ%2Fpk7boibJkAgYPZBYCZg8VBQQ1NTQyBDU1NDIENTU0MgnmqYTmpoTkupEJ5qmE5qaE5LqRZAIHD2QWAmYPFQUEMjA5NQQyMDk1BDIwOTUS5rW35rSL5a6d55%2Bz6JOd6ImyEua1t%2Ba0i%2BWuneefs%2BiTneiJsmQCCA9kFgJmDxUFBDIwNzcEMjA3NwQyMDc3Bum7keiJsgbpu5HoibJkAgkPZBYCZg8VBQQ4MDE3BDgwMTcEODAxNw%2FngbDnmb3nj43nj6DoibIP54Gw55m954%2BN54%2Bg6ImyZAIKD2QWAmYPFQUEMjA4NAQyMDg0BDIwODQG54Gw6ImyBueBsOiJsmQCCw9kFgJmDxUFBDIwODYEMjA4NgQyMDg2BumHkeiJsgbph5HoibJkAgwPZBYCZg8VBQQyMDgwBDIwODAEMjA4MAzph5HlsZ7nsbPoibIM6YeR5bGe57Gz6ImyZAIND2QWAmYPFQUEMjA4NQQyMDg1BDIwODUJ6YWS57qi6ImyCemFkue6ouiJsmQCDg9kFgJmDxUFBDgwMTUEODAxNQQ4MDE1D%2BmFkue6ouS6keavjeiJsg%2FphZLnuqLkupHmr43oibJkAg8PZBYCZg8VBQQ4MDE2BDgwMTYEODAxNg%2Fkuq7opJDph5HlsZ7oibIP5Lqu6KSQ6YeR5bGe6ImyZAIQD2QWAmYPFQUEMjA5MAQyMDkwBDIwOTAJ5Lqu6KSQ6ImyCeS6ruikkOiJsmQCEQ9kFgJmDxUFBDIwODIEMjA4MgQyMDgyCeS6rumTtuiJsgnkuq7pk7boibJkAhIPZBYCZg8VBQQ1NTQxBDU1NDEENTU0MQzmt7HmqYTmpoToibIM5rex5qmE5qaE6ImyZAITD2QWAmYPFQUEMjA5NAQyMDk0BDIwOTQY5rex5qmE5qaE6Imy5aSp6Z2S55%2Bz6ImyGOa3seaphOamhOiJsuWkqemdkuefs%2BiJsmQCFA9kFgJmDxUFBDgwMTMEODAxMwQ4MDEzEua3seaphOamhOS6keavjeiJshLmt7HmqYTmpoTkupHmr43oibJkAhUPZBYCZg8VBQQyMDgzBDIwODMEMjA4Mwnmt7HngbDoibIJ5rex54Gw6ImyZAIWD2QWAmYPFQUEMjA3NQQyMDc1BDIwNzUP5rex54Gw5LqR5q%2BN6ImyD%2Ba3seeBsOS6keavjeiJsmQCFw9kFgJmDxUFBDIwODgEMjA4OAQyMDg4Cea3seiTneiJsgnmt7Hok53oibJkAhgPZBYCZg8VBQQ5NTcxBDk1NzEEOTU3MRXmt7Hmo5XkupHmr43ph5HlsZ7oibIV5rex5qOV5LqR5q%2BN6YeR5bGe6ImyZAIZD2QWAmYPFQUENjkzNwQ2OTM3BDY5MzcJ5rC06LKC6ImyCeawtOiyguiJsmQCGg9kFgJmDxUFBDgwMTQEODAxNAQ4MDE0EuawtOmTtueBsOS6keavjeiJshLmsLTpk7bngbDkupHmr43oibJkAhsPZBYCZg8VBQQyMDc0BDIwNzQEMjA3NA%2FkuJ3lhYnnj43nj6Dnmb0P5Lid5YWJ54%2BN54%2Bg55m9ZAIcD2QWAmYPFQUEMjA5MQQyMDkxBDIwOTEM5aSp6Z2S55%2Bz6ImyDOWkqemdkuefs%2BiJsmQCHQ9kFgJmDxUFBDk1NzAEOTU3MAQ5NTcwEuWkqemdkuefs%2BS6keavjeiJshLlpKnpnZLnn7PkupHmr43oibJkAh4PZBYCZg8VBQQ4MDEyBDgwMTIEODAxMg%2FlpKnpnZLkupHmr43oibIP5aSp6Z2S5LqR5q%2BN6ImyZAIfD2QWAmYPFQUENzg4NAQ3ODg0BDc4ODQM5pif5YWJ6buR6ImyDOaYn%2BWFiem7keiJsmQCIA9kFgJmDxUFBDIwODEEMjA4MQQyMDgxA%2BmTtgPpk7ZkAiEPZBYCZg8VBQQyMDc2BDIwNzYEMjA3Ngbpk7boibIG6ZO26ImyZAIiD2QWAmYPFQUEMjA4NwQyMDg3BDIwODcM546J55%2Bz57u%2F6ImyDOeOieefs%2Be7v%2BiJsmQCIw9kFgJmDxUFBDIwNzkEMjA3OQQyMDc5D%2Baeo%2Be6ouS6keavjeiJsg%2FmnqPnuqLkupHmr43oibJkAiQPZBYCZg8VBQQyMDczBDIwNzMEMjA3Mwnnj43nj6Dnmb0J54%2BN54%2Bg55m9ZAIlD2QWAmYPFQUEOTU3MwQ5NTczBDk1NzMM54%2BN54%2Bg55m96ImyDOePjeePoOeZveiJsmQCJg9kFgJmDxUFBDIwNzgEMjA3OAQyMDc4FeajleeBsOS6keavjemHkeWxnuiJshXmo5XngbDkupHmr43ph5HlsZ7oibJkAg4PZBYCAgMPZBYCZg9kFgpmD2QWAmYPZBYCAgEPDxYCHghJbWFnZVVybAVUaHR0cDovL2ltZzEuYml0YXV0b2ltZy5jb20vYXV0b2FsYnVtL2ZpbGVzLzIwMTMxMDMwLzY3MS8xODIyMzI2NzEyMDYzNF8yOTA3NjA5XzMuanBnZGQCAg8PFgIfBwVUaHR0cDovL2ltZzMuYml0YXV0b2ltZy5jb20vYXV0b2FsYnVtL2ZpbGVzLzIwMTcwMTEwLzY0MC8xMDEyMzE2NDA5OTIzMV81NDIxMDYyXzQuSlBHZGQCBA8PFgIfBwVUaHR0cDovL2ltZzQuYml0YXV0b2ltZy5jb20vYXV0b2FsYnVtL2ZpbGVzLzIwMTQwNzAzLzc4Ni8xNDUxNDY3ODY5Nzg0Nl8zNDI1NjQzXzQuSlBHZGQCBg8PFgIfBwVUaHR0cDovL2ltZzEuYml0YXV0b2ltZy5jb20vYXV0b2FsYnVtL2ZpbGVzLzIwMTQwNzAzLzc4OS8xNDUxNDY3ODk5NTk3MV8zNDI1NjQ0XzQuSlBHZGQCCA8PFgIfBwVUaHR0cDovL2ltZzEuYml0YXV0b2ltZy5jb20vYXV0b2FsYnVtL2ZpbGVzLzIwMTQwNzAzLzc3OC8xNDUxNDY3Nzg5NDczMF8zNDI1NjQwXzQuSlBHZGQCFQ9kFgICAg8WAh4Dc3JjBZACaHR0cDovL2Rhcy5hcHAuZWFzeXBhc3MuY24vRmlsZU1hbmFnZS9GaWxlVXBsb2FkL0ltYWdlVXBsb2FkLmFzcHg%2FYXJncz1oaWRlVXJsSUQ6fWh0aHVtdXJsSUQ6fWNvbmNsaWVudGlkOmltZ1VwbG9hZENoYW5nZX1pc0NhbnZhczpUcnVlfXVwY0lEOjd9ZGlzcGxheTpub25lfWlzc2hvd2FsbDpUcnVlfWljbndpZHRoOjMwMH1pY25oZWlnaHQ6MjAwfWN3aWR0aDozNjB9Y2hlaWdodDoyNDB9bXBsZToyfWFwcGtleTo3ODRlOWM3YS1jMWYxLTQ5YjUtOTYzOC0yM2Q4YmRjMjM3ZDBkAhoPZBYCZg9kFgICAQ88KwARAwAPFgoeC18hRGF0YUJvdW5kZx8CAiUeCUhBRFJEQ0NJRAXmAyxEYXNHcmlkVmlld0FsdGVybmF0aW5nUm93LCxEYXNHcmlkVmlld0FsdGVybmF0aW5nUm93LCxEYXNHcmlkVmlld0FsdGVybmF0aW5nUm93LCxEYXNHcmlkVmlld0FsdGVybmF0aW5nUm93LCxEYXNHcmlkVmlld0FsdGVybmF0aW5nUm93LCxEYXNHcmlkVmlld0FsdGVybmF0aW5nUm93LCxEYXNHcmlkVmlld0FsdGVybmF0aW5nUm93LCxEYXNHcmlkVmlld0FsdGVybmF0aW5nUm93LCxEYXNHcmlkVmlld0FsdGVybmF0aW5nUm93LCxEYXNHcmlkVmlld0FsdGVybmF0aW5nUm93LCxEYXNHcmlkVmlld0FsdGVybmF0aW5nUm93LCxEYXNHcmlkVmlld0FsdGVybmF0aW5nUm93LCxEYXNHcmlkVmlld0FsdGVybmF0aW5nUm93LCxEYXNHcmlkVmlld0FsdGVybmF0aW5nUm93LCxEYXNHcmlkVmlld0FsdGVybmF0aW5nUm93LCxEYXNHcmlkVmlld0FsdGVybmF0aW5nUm93LCxEYXNHcmlkVmlld0FsdGVybmF0aW5nUm93LCxEYXNHcmlkVmlld0FsdGVybmF0aW5nUm93LB4KSEFDQkNJRFNJRAUgZGd2TWVyY2hhbmRpc2VfY3RsMDFfQWxsQ2hlY2tCb3geCUhDQkNJRFNJRAXVCGRndk1lcmNoYW5kaXNlX2N0bDAyX0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDAzX0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDA0X0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDA1X0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDA2X0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDA3X0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDA4X0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDA5X0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDEwX0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDExX0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDEyX0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDEzX0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDE0X0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDE1X0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDE2X0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDE3X0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDE4X0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDE5X0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDIwX0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDIxX0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDIyX0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDIzX0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDI0X0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDI1X0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDI2X0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDI3X0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDI4X0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDI5X0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDMwX0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDMxX0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDMyX0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDMzX0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDM0X0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDM1X0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDM2X0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDM3X0NoZWNrQm94LGRndk1lcmNoYW5kaXNlX2N0bDM4X0NoZWNrQm94ZAEQFgAWABYADBQrAAAWAmYPZBZOZg9kFgJmD2QWAgIBDxAPZBYCHgdvbmNsaWNrBRNDaGVja0JveENsaWNrKHRoaXMpZGRkAgEPD2QWBB4Lb25tb3VzZW92ZXIFGUdyaWRSb3dPbk1vdXNlT3Zlcih0aGlzKTseCm9ubW91c2VvdXQFGkdyaWRSb3dPbk1vdXNlT3V0KHRoaXMsMCk7FgpmD2QWAmYPEA9kFgIfDQUVQ2hlY2tCb3hDbGljayh0aGlzLDApZGRkAgEPDxYCHwYFBTU3MDI2ZGQCAg8PFgIfBgUS5aS05oqx5p6V5LqU5Lu25aWXZGQCAw8PFgIfBgUG5aS05p6VZGQCBA8PFgIfBgUGNTgwLjAwZGQCAg8PZBYEHw4FGUdyaWRSb3dPbk1vdXNlT3Zlcih0aGlzKTsfDwUaR3JpZFJvd09uTW91c2VPdXQodGhpcywxKTsWCmYPZBYCZg8QD2QWAh8NBRVDaGVja0JveENsaWNrKHRoaXMsMSlkZGQCAQ8PFgIfBgUFNTcwMjdkZAICDw8WAh8GBRLovablhoXljaHpgJrnorPljIVkZAIDDw8WAh8GBQzmkYbppbDmjILku7ZkZAIEDw8WAh8GBQU5OC4wMGRkAgMPD2QWBB8OBRlHcmlkUm93T25Nb3VzZU92ZXIodGhpcyk7Hw8FGkdyaWRSb3dPbk1vdXNlT3V0KHRoaXMsMik7FgpmD2QWAmYPEA9kFgIfDQUVQ2hlY2tCb3hDbGljayh0aGlzLDIpZGRkAgEPDxYCHwYFBTU3NDY2ZGQCAg8PFgIfBgUOTEVYVVPng5%2FngbDnvLhkZAIDDw8WAh8GBQznva7nianmlLbnurNkZAIEDw8WAh8GBQYzNjAuMDBkZAIEDw9kFgQfDgUZR3JpZFJvd09uTW91c2VPdmVyKHRoaXMpOx8PBRpHcmlkUm93T25Nb3VzZU91dCh0aGlzLDMpOxYKZg9kFgJmDxAPZBYCHw0FFUNoZWNrQm94Q2xpY2sodGhpcywzKWRkZAIBDw8WAh8GBQU1NzQ3OGRkAgIPDxYCHwYFEkxFWFVTIENUMjAwaOmbqOeciWRkAgMPDxYCHwYFCeaZtOmbqOaMoWRkAgQPDxYCHwYFBzI1ODAuMDBkZAIFDw9kFgQfDgUZR3JpZFJvd09uTW91c2VPdmVyKHRoaXMpOx8PBRpHcmlkUm93T25Nb3VzZU91dCh0aGlzLDQpOxYKZg9kFgJmDxAPZBYCHw0FFUNoZWNrQm94Q2xpY2sodGhpcyw0KWRkZAIBDw8WAh8GBQU1NzQ4M2RkAgIPDxYCHwYFEui9ruavgumYsuebl%2BieuuavjWRkAgMPDxYCHwYFDOeUteWtkOmYsuebl2RkAgQPDxYCHwYFBzE1MDAuMDBkZAIGDw9kFgQfDgUZR3JpZFJvd09uTW91c2VPdmVyKHRoaXMpOx8PBRpHcmlkUm93T25Nb3VzZU91dCh0aGlzLDUpOxYKZg9kFgJmDxAPZBYCHw0FFUNoZWNrQm94Q2xpY2sodGhpcyw1KWRkZAIBDw8WAh8GBQU1NzQ4NmRkAgIPDxYCHwYFGUVTMjUw44CBRVMzNTDlkI7lpIfnrrHlnqtkZAIDDw8WAh8GBQzlkI7lpIfnrrHlnqtkZAIEDw8WAh8GBQcxMDAwLjAwZGQCBw8PZBYEHw4FGUdyaWRSb3dPbk1vdXNlT3Zlcih0aGlzKTsfDwUaR3JpZFJvd09uTW91c2VPdXQodGhpcyw2KTsWCmYPZBYCZg8QD2QWAh8NBRVDaGVja0JveENsaWNrKHRoaXMsNilkZGQCAQ8PFgIfBgUFNTc1MDBkZAICDw8WAh8GBRVDVDIwMGjpm77nga%2Foo4XppbDnvalkZAIDDw8WAh8GBQbnga%2FnvalkZAIEDw8WAh8GBQcxMTgwLjAwZGQCCA8PZBYEHw4FGUdyaWRSb3dPbk1vdXNlT3Zlcih0aGlzKTsfDwUaR3JpZFJvd09uTW91c2VPdXQodGhpcyw3KTsWCmYPZBYCZg8QD2QWAh8NBRVDaGVja0JveENsaWNrKHRoaXMsNylkZGQCAQ8PFgIfBgUFNTc1MDFkZAICDw8WAh8GBRJDVDIwMGjlkI7pl6jouI%2Fmnb9kZAIDDw8WAh8GBQbouI%2Fmnb9kZAIEDw8WAh8GBQcyNTgwLjAwZGQCCQ8PZBYEHw4FGUdyaWRSb3dPbk1vdXNlT3Zlcih0aGlzKTsfDwUaR3JpZFJvd09uTW91c2VPdXQodGhpcyw4KTsWCmYPZBYCZg8QD2QWAh8NBRVDaGVja0JveENsaWNrKHRoaXMsOClkZGQCAQ8PFgIfBgUFNTc1MDNkZAICDw8WAh8GBRLovabpl6jkv53miqTppbDmnaFkZAIDDw8WAh8GBRPovabouqvppbDmnaEv6LS057q4ZGQCBA8PFgIfBgUHMTE4MC4wMGRkAgoPD2QWBB8OBRlHcmlkUm93T25Nb3VzZU92ZXIodGhpcyk7Hw8FGkdyaWRSb3dPbk1vdXNlT3V0KHRoaXMsOSk7FgpmD2QWAmYPEA9kFgIfDQUVQ2hlY2tCb3hDbGljayh0aGlzLDkpZGRkAgEPDxYCHwYFBTU3NTA0ZGQCAg8PFgIfBgUSQ1QyMDBo5ZCO5aSH566x5Z6rZGQCAw8PFgIfBgUM5ZCO5aSH566x5Z6rZGQCBA8PFgIfBgUHMjAwMC4wMGRkAgsPD2QWBB8OBRlHcmlkUm93T25Nb3VzZU92ZXIodGhpcyk7Hw8FG0dyaWRSb3dPbk1vdXNlT3V0KHRoaXMsMTApOxYKZg9kFgJmDxAPZBYCHw0FFkNoZWNrQm94Q2xpY2sodGhpcywxMClkZGQCAQ8PFgIfBgUFNTc1MDVkZAICDw8WAh8GBRJDVDIwMGjpq5jnuqfooaPmnrZkZAIDDw8WAh8GBQnmmbTpm6jmjKFkZAIEDw8WAh8GBQcxMjgwLjAwZGQCDA8PZBYEHw4FGUdyaWRSb3dPbk1vdXNlT3Zlcih0aGlzKTsfDwUbR3JpZFJvd09uTW91c2VPdXQodGhpcywxMSk7FgpmD2QWAmYPEA9kFgIfDQUWQ2hlY2tCb3hDbGljayh0aGlzLDExKWRkZAIBDw8WAh8GBQU1NzUwNmRkAgIPDxYCHwYFFeWJjeS%2FnemZqeadoOaJsOa1geadv2RkAgMPDxYCHwYFDOijhemlsOWll%2BS7tmRkAgQPDxYCHwYFBzQ4MDAuMDBkZAINDw9kFgQfDgUZR3JpZFJvd09uTW91c2VPdmVyKHRoaXMpOx8PBRtHcmlkUm93T25Nb3VzZU91dCh0aGlzLDEyKTsWCmYPZBYCZg8QD2QWAh8NBRZDaGVja0JveENsaWNrKHRoaXMsMTIpZGRkAgEPDxYCHwYFBTU3NTA3ZGQCAg8PFgIfBgUVTEVYVVMg6L2m5L6n5oyh5rOl5p2%2FZGQCAw8PFgIfBgUJ5oyh5rOl5p2%2FZGQCBA8PFgIfBgUHNDUwMC4wMGRkAg4PD2QWBB8OBRlHcmlkUm93T25Nb3VzZU92ZXIodGhpcyk7Hw8FG0dyaWRSb3dPbk1vdXNlT3V0KHRoaXMsMTMpOxYKZg9kFgJmDxAPZBYCHw0FFkNoZWNrQm94Q2xpY2sodGhpcywxMylkZGQCAQ8PFgIfBgUFNTc1MDhkZAICDw8WAh8GBRXlkI7kv53pmanmnaDmibDmtYHmnb9kZAIDDw8WAh8GBQzoo4XppbDlpZfku7ZkZAIEDw8WAh8GBQc0NTAwLjAwZGQCDw8PZBYEHw4FGUdyaWRSb3dPbk1vdXNlT3Zlcih0aGlzKTsfDwUbR3JpZFJvd09uTW91c2VPdXQodGhpcywxNCk7FgpmD2QWAmYPEA9kFgIfDQUWQ2hlY2tCb3hDbGljayh0aGlzLDE0KWRkZAIBDw8WAh8GBQU1NzUxMGRkAgIPDxYCHwYFD0xFWFVTIOaMoeazpeadv2RkAgMPDxYCHwYFCeaMoeazpeadv2RkAgQPDxYCHwYFBjU4MC4wMGRkAhAPD2QWBB8OBRlHcmlkUm93T25Nb3VzZU92ZXIodGhpcyk7Hw8FG0dyaWRSb3dPbk1vdXNlT3V0KHRoaXMsMTUpOxYKZg9kFgJmDxAPZBYCHw0FFkNoZWNrQm94Q2xpY2sodGhpcywxNSlkZGQCAQ8PFgIfBgUFNTc1MTJkZAICDw8WAh8GBQxMRVhVUyDpm6jnnIlkZAIDDw8WAh8GBQnmmbTpm6jmjKFkZAIEDw8WAh8GBQcxMjAwLjAwZGQCEQ8PZBYEHw4FGUdyaWRSb3dPbk1vdXNlT3Zlcih0aGlzKTsfDwUbR3JpZFJvd09uTW91c2VPdXQodGhpcywxNik7FgpmD2QWAmYPEA9kFgIfDQUWQ2hlY2tCb3hDbGljayh0aGlzLDE2KWRkZAIBDw8WAh8GBQU1NzUxM2RkAgIPDxYCHwYFHui%2FjuWuvueFp%2BaYjui4j%2Badv%2B%2B8iOiTneWFie%2B8iWRkAgMPDxYCHwYFDOi%2FjuWuvui4j%2Badv2RkAgQPDxYCHwYFBzE4MDAuMDBkZAISDw9kFgQfDgUZR3JpZFJvd09uTW91c2VPdmVyKHRoaXMpOx8PBRtHcmlkUm93T25Nb3VzZU91dCh0aGlzLDE3KTsWCmYPZBYCZg8QD2QWAh8NBRZDaGVja0JveENsaWNrKHRoaXMsMTcpZGRkAgEPDxYCHwYFBTU3NTE0ZGQCAg8PFgIfBgUe6L%2BO5a6%2B54Wn5piO6LiP5p2%2F77yI55m95YWJ77yJZGQCAw8PFgIfBgUM6L%2BO5a6%2B6LiP5p2%2FZGQCBA8PFgIfBgUHMTgwMC4wMGRkAhMPD2QWBB8OBRlHcmlkUm93T25Nb3VzZU92ZXIodGhpcyk7Hw8FG0dyaWRSb3dPbk1vdXNlT3V0KHRoaXMsMTgpOxYKZg9kFgJmDxAPZBYCHw0FFkNoZWNrQm94Q2xpY2sodGhpcywxOClkZGQCAQ8PFgIfBgUFNTc1MTVkZAICDw8WAh8GBRFMRVhVUyBSWOaMoeazpeadv2RkAgMPDxYCHwYFCeaMoeazpeadv2RkAgQPDxYCHwYFBzE1MDAuMDBkZAIUDw9kFgQfDgUZR3JpZFJvd09uTW91c2VPdmVyKHRoaXMpOx8PBRtHcmlkUm93T25Nb3VzZU91dCh0aGlzLDE5KTsWCmYPZBYCZg8QD2QWAh8NBRZDaGVja0JveENsaWNrKHRoaXMsMTkpZGRkAgEPDxYCHwYFBTU3NTE4ZGQCAg8PFgIfBgUSTEVYVVMg5ZCO6Zeo6LiP5p2%2FZGQCAw8PFgIfBgUG6LiP5p2%2FZGQCBA8PFgIfBgUHNDUwMC4wMGRkAhUPD2QWBB8OBRlHcmlkUm93T25Nb3VzZU92ZXIodGhpcyk7Hw8FG0dyaWRSb3dPbk1vdXNlT3V0KHRoaXMsMjApOxYKZg9kFgJmDxAPZBYCHw0FFkNoZWNrQm94Q2xpY2sodGhpcywyMClkZGQCAQ8PFgIfBgUFNTkyNjJkZAICDw8WAh8GBQtMRVhVU%2BWwj%2BeGimRkAgMPDxYCHwYFDOaRhumlsOaMguS7tmRkAgQPDxYCHwYFBjI4MC4wMGRkAhYPD2QWBB8OBRlHcmlkUm93T25Nb3VzZU92ZXIodGhpcyk7Hw8FG0dyaWRSb3dPbk1vdXNlT3V0KHRoaXMsMjEpOxYKZg9kFgJmDxAPZBYCHw0FFkNoZWNrQm94Q2xpY2sodGhpcywyMSlkZGQCAQ8PFgIfBgUFODA2NjBkZAICDw8WAh8GBRjpm7flhYvokKjmlq%2Fovabovb3pppnoho9kZAIDDw8WAh8GBQzmkYbppbDmjILku7ZkZAIEDw8WAh8GBQU5OC4wMGRkAhcPD2QWBB8OBRlHcmlkUm93T25Nb3VzZU92ZXIodGhpcyk7Hw8FG0dyaWRSb3dPbk1vdXNlT3V0KHRoaXMsMjIpOxYKZg9kFgJmDxAPZBYCHw0FFkNoZWNrQm94Q2xpY2sodGhpcywyMilkZGQCAQ8PFgIfBgUFODA2NjJkZAICDw8WAh8GBRjpm7flhYvokKjmlq%2Fovabovb3pppnmsLRkZAIDDw8WAh8GBQzmkYbppbDmjILku7ZkZAIEDw8WAh8GBQYzOTguMDBkZAIYDw9kFgQfDgUZR3JpZFJvd09uTW91c2VPdmVyKHRoaXMpOx8PBRtHcmlkUm93T25Nb3VzZU91dCh0aGlzLDIzKTsWCmYPZBYCZg8QD2QWAh8NBRZDaGVja0JveENsaWNrKHRoaXMsMjMpZGRkAgEPDxYCHwYFBTgwNjY4ZGQCAg8PFgIfBgUS6Ziy5rC05Za35Lid6ISa5Z6rZGQCAw8PFgIfBgUM5Zyw6IO26ISa5Z6rZGQCBA8PFgIfBgUHMTI4MC4wMGRkAhkPD2QWBB8OBRlHcmlkUm93T25Nb3VzZU92ZXIodGhpcyk7Hw8FG0dyaWRSb3dPbk1vdXNlT3V0KHRoaXMsMjQpOxYKZg9kFgJmDxAPZBYCHw0FFkNoZWNrQm94Q2xpY2sodGhpcywyNClkZGQCAQ8PFgIfBgUFODA2NjlkZAICDw8WAh8GBRjpm7flhYvokKjmlq%2FlkI7lpIfnrrHlnqtkZAIDDw8WAh8GBQzlkI7lpIfnrrHlnqtkZAIEDw8WAh8GBQcxMjAwLjAwZGQCGg8PZBYEHw4FGUdyaWRSb3dPbk1vdXNlT3Zlcih0aGlzKTsfDwUbR3JpZFJvd09uTW91c2VPdXQodGhpcywyNSk7FgpmD2QWAmYPEA9kFgIfDQUWQ2hlY2tCb3hDbGljayh0aGlzLDI1KWRkZAIBDw8WAh8GBQU4MTI0MGRkAgIPDxYCHwYFEuWJjeWQjuaXtuWwmuaKpOadv2RkAgMPDxYCHwYFD%2BWPkeWKqOacuuaKpOadv2RkAgQPDxYCHwYFBzc4MDAuMDBkZAIbDw9kFgQfDgUZR3JpZFJvd09uTW91c2VPdmVyKHRoaXMpOx8PBRtHcmlkUm93T25Nb3VzZU91dCh0aGlzLDI2KTsWCmYPZBYCZg8QD2QWAh8NBRZDaGVja0JveENsaWNrKHRoaXMsMjYpZGRkAgEPDxYCHwYFBTgxMjQxZGQCAg8PFgIfBgUi5YWo5YyF5Zu056uL5L2T6ISa5Z6rIOS4k%2Bi9puS4k%2BeUqGRkAgMPDxYCHwYFDOWcsOiDtuiEmuWeq2RkAgQPDxYCHwYFBzI4NjAuMDBkZAIcDw9kFgQfDgUZR3JpZFJvd09uTW91c2VPdmVyKHRoaXMpOx8PBRtHcmlkUm93T25Nb3VzZU91dCh0aGlzLDI3KTsWCmYPZBYCZg8QD2QWAh8NBRZDaGVja0JveENsaWNrKHRoaXMsMjcpZGRkAgEPDxYCHwYFBTgxMjQyZGQCAg8PFgIfBgUb5rG96L2m5YaF6aWw55So5ZOB5LqU5Lu25aWXZGQCAw8PFgIfBgUG5oqk5aWXZGQCBA8PFgIfBgUGNjM4LjAwZGQCHQ8PZBYEHw4FGUdyaWRSb3dPbk1vdXNlT3Zlcih0aGlzKTsfDwUbR3JpZFJvd09uTW91c2VPdXQodGhpcywyOCk7FgpmD2QWAmYPEA9kFgIfDQUWQ2hlY2tCb3hDbGljayh0aGlzLDI4KWRkZAIBDw8WAh8GBQU4MTI0M2RkAgIPDxYCHwYFGOaxvei9puWQjuS%2FnemZqeadoOi4j%2Badv2RkAgMPDxYCHwYFBui4j%2Badv2RkAgQPDxYCHwYFBzI1MDAuMDBkZAIeDw9kFgQfDgUZR3JpZFJvd09uTW91c2VPdmVyKHRoaXMpOx8PBRtHcmlkUm93T25Nb3VzZU91dCh0aGlzLDI5KTsWCmYPZBYCZg8QD2QWAh8NBRZDaGVja0JveENsaWNrKHRoaXMsMjkpZGRkAgEPDxYCHwYFBTgxMjQ0ZGQCAg8PFgIfBgUd5rG96L2m5YaF6aWwLS3nu7TkuZ%2FnurPlnZDlnqtkZAIDDw8WAh8GBQzluqflpZflnZDlnqtkZAIEDw8WAh8GBQcyMTgwLjAwZGQCHw8PZBYEHw4FGUdyaWRSb3dPbk1vdXNlT3Zlcih0aGlzKTsfDwUbR3JpZFJvd09uTW91c2VPdXQodGhpcywzMCk7FgpmD2QWAmYPEA9kFgIfDQUWQ2hlY2tCb3hDbGljayh0aGlzLDMwKWRkZAIBDw8WAh8GBQU4MTI0NWRkAgIPDxYCHwYFG%2Baxvei9puWGhemlsOeUqOWTgeS6lOS7tuWll2RkAgMPDxYCHwYFBuaKpOWll2RkAgQPDxYCHwYFBjU4MC4wMGRkAiAPD2QWBB8OBRlHcmlkUm93T25Nb3VzZU92ZXIodGhpcyk7Hw8FG0dyaWRSb3dPbk1vdXNlT3V0KHRoaXMsMzEpOxYKZg9kFgJmDxAPZBYCHw0FFkNoZWNrQm94Q2xpY2sodGhpcywzMSlkZGQCAQ8PFgIfBgUFODEyNDZkZAICDw8WAh8GBRrmsb3ovabnlKjlk4HlhoXppbAtLeeis%2BWMhWRkAgMPDxYCHwYFEummmeawtOmmmeiWsOeCreWMhWRkAgQPDxYCHwYFBTk4LjAwZGQCIQ8PZBYEHw4FGUdyaWRSb3dPbk1vdXNlT3Zlcih0aGlzKTsfDwUbR3JpZFJvd09uTW91c2VPdXQodGhpcywzMik7FgpmD2QWAmYPEA9kFgIfDQUWQ2hlY2tCb3hDbGljayh0aGlzLDMyKWRkZAIBDw8WAh8GBQU4MTI0N2RkAgIPDxYCHwYFGuaxvei9puWGhemlsC0t5oyC6aWw6aaZ5rC0ZGQCAw8PFgIfBgUS6aaZ5rC06aaZ6Jaw54Kt5YyFZGQCBA8PFgIfBgUGMTI4LjAwZGQCIg8PZBYEHw4FGUdyaWRSb3dPbk1vdXNlT3Zlcih0aGlzKTsfDwUbR3JpZFJvd09uTW91c2VPdXQodGhpcywzMyk7FgpmD2QWAmYPEA9kFgIfDQUWQ2hlY2tCb3hDbGljayh0aGlzLDMzKWRkZAIBDw8WAh8GBQU4MTI0OWRkAgIPDxYCHwYFFOaxvei9puWGhemlsC0t5oyC6aWwZGQCAw8PFgIfBgUM5pGG6aWw5oyC5Lu2ZGQCBA8PFgIfBgUGMzk4LjAwZGQCIw8PZBYEHw4FGUdyaWRSb3dPbk1vdXNlT3Zlcih0aGlzKTsfDwUbR3JpZFJvd09uTW91c2VPdXQodGhpcywzNCk7FgpmD2QWAmYPEA9kFgIfDQUWQ2hlY2tCb3hDbGljayh0aGlzLDM0KWRkZAIBDw8WAh8GBQU4MTI1MWRkAgIPDxYCHwYFGOmbt%2BWFi%2BiQqOaWr%2Bi9pui9vemmmeiGj2RkAgMPDxYCHwYFEummmeawtOmmmeiWsOeCreWMhWRkAgQPDxYCHwYFBjE5OC4wMGRkAiQPD2QWBB8OBRlHcmlkUm93T25Nb3VzZU92ZXIodGhpcyk7Hw8FG0dyaWRSb3dPbk1vdXNlT3V0KHRoaXMsMzUpOxYKZg9kFgJmDxAPZBYCHw0FFkNoZWNrQm94Q2xpY2sodGhpcywzNSlkZGQCAQ8PFgIfBgUFODYyODBkZAICDw8WAh8GBQ9MRVhVUyDoh6rooYzovaZkZAIDDw8WAh8GBQnmmbTpm6jmjKFkZAIEDw8WAh8GBQc2ODAwLjAwZGQCJQ8PZBYEHw4FGUdyaWRSb3dPbk1vdXNlT3Zlcih0aGlzKTsfDwUbR3JpZFJvd09uTW91c2VPdXQodGhpcywzNik7FgpmD2QWAmYPEA9kFgIfDQUWQ2hlY2tCb3hDbGljayh0aGlzLDM2KWRkZAIBDw8WAh8GBQU4NjI4MWRkAgIPDxYCHwYFD0xFWFVTIExGQei9puaooWRkAgMPDxYCHwYFBuaKpOWll2RkAgQPDxYCHwYFBzI5ODAuMDBkZAImDw8WAh4HVmlzaWJsZWhkZAIbD2QWAmYPZBYCAgEPFgIfAgInFk5mD2QWAmYPFQUFMTgyODEFMTgyODEFMTgyODES55m96YeR54Gw6YeR5bGe6ImyEueZvemHkeeBsOmHkeWxnuiJsmQCAQ9kFgJmDxUFBDIwOTIEMjA5MgQyMDkyBueZveiJsgbnmb3oibJkAgIPZBYCZg8VBQQyMDg5BDIwODkEMjA4OQzlrp3nn7Ppu5HoibIM5a6d55%2Bz6buR6ImyZAIDD2QWAmYPFQUEMjA5MwQyMDkzBDIwOTMM5a6d55%2Bz6JOd6ImyDOWuneefs%2BiTneiJsmQCBA9kFgJmDxUFBTE5NjcyBTE5NjcyBTE5NjcyEui2hemfs%2BmAn%2BmSm%2BmTtuiJshLotoXpn7PpgJ%2Fpkpvpk7boibJkAgUPZBYCZg8VBQQ5NTcyBDk1NzIEOTU3Mg%2FotoXpn7PpgJ%2Fpk7boibIP6LaF6Z%2Bz6YCf6ZO26ImyZAIGD2QWAmYPFQUENTU0MgQ1NTQyBDU1NDIJ5qmE5qaE5LqRCeaphOamhOS6kWQCBw9kFgJmDxUFBDIwOTUEMjA5NQQyMDk1Eua1t%2Ba0i%2BWuneefs%2BiTneiJshLmtbfmtIvlrp3nn7Pok53oibJkAggPZBYCZg8VBQQyMDc3BDIwNzcEMjA3Nwbpu5HoibIG6buR6ImyZAIJD2QWAmYPFQUEODAxNwQ4MDE3BDgwMTcP54Gw55m954%2BN54%2Bg6ImyD%2BeBsOeZveePjeePoOiJsmQCCg9kFgJmDxUFBDIwODQEMjA4NAQyMDg0BueBsOiJsgbngbDoibJkAgsPZBYCZg8VBQQyMDg2BDIwODYEMjA4Ngbph5HoibIG6YeR6ImyZAIMD2QWAmYPFQUEMjA4MAQyMDgwBDIwODAM6YeR5bGe57Gz6ImyDOmHkeWxnuexs%2BiJsmQCDQ9kFgJmDxUFBDIwODUEMjA4NQQyMDg1CemFkue6ouiJsgnphZLnuqLoibJkAg4PZBYCZg8VBQQ4MDE1BDgwMTUEODAxNQ%2FphZLnuqLkupHmr43oibIP6YWS57qi5LqR5q%2BN6ImyZAIPD2QWAmYPFQUEODAxNgQ4MDE2BDgwMTYP5Lqu6KSQ6YeR5bGe6ImyD%2BS6ruikkOmHkeWxnuiJsmQCEA9kFgJmDxUFBDIwOTAEMjA5MAQyMDkwCeS6ruikkOiJsgnkuq7opJDoibJkAhEPZBYCZg8VBQQyMDgyBDIwODIEMjA4Mgnkuq7pk7boibIJ5Lqu6ZO26ImyZAISD2QWAmYPFQUENTU0MQQ1NTQxBDU1NDEM5rex5qmE5qaE6ImyDOa3seaphOamhOiJsmQCEw9kFgJmDxUFBDIwOTQEMjA5NAQyMDk0GOa3seaphOamhOiJsuWkqemdkuefs%2BiJshjmt7HmqYTmpoToibLlpKnpnZLnn7PoibJkAhQPZBYCZg8VBQQ4MDEzBDgwMTMEODAxMxLmt7HmqYTmpoTkupHmr43oibIS5rex5qmE5qaE5LqR5q%2BN6ImyZAIVD2QWAmYPFQUEMjA4MwQyMDgzBDIwODMJ5rex54Gw6ImyCea3seeBsOiJsmQCFg9kFgJmDxUFBDIwNzUEMjA3NQQyMDc1D%2Ba3seeBsOS6keavjeiJsg%2Fmt7HngbDkupHmr43oibJkAhcPZBYCZg8VBQQyMDg4BDIwODgEMjA4OAnmt7Hok53oibIJ5rex6JOd6ImyZAIYD2QWAmYPFQUEOTU3MQQ5NTcxBDk1NzEV5rex5qOV5LqR5q%2BN6YeR5bGe6ImyFea3seajleS6keavjemHkeWxnuiJsmQCGQ9kFgJmDxUFBDY5MzcENjkzNwQ2OTM3CeawtOiyguiJsgnmsLTosoLoibJkAhoPZBYCZg8VBQQ4MDE0BDgwMTQEODAxNBLmsLTpk7bngbDkupHmr43oibIS5rC06ZO254Gw5LqR5q%2BN6ImyZAIbD2QWAmYPFQUEMjA3NAQyMDc0BDIwNzQP5Lid5YWJ54%2BN54%2Bg55m9D%2BS4neWFieePjeePoOeZvWQCHA9kFgJmDxUFBDIwOTEEMjA5MQQyMDkxDOWkqemdkuefs%2BiJsgzlpKnpnZLnn7PoibJkAh0PZBYCZg8VBQQ5NTcwBDk1NzAEOTU3MBLlpKnpnZLnn7PkupHmr43oibIS5aSp6Z2S55%2Bz5LqR5q%2BN6ImyZAIeD2QWAmYPFQUEODAxMgQ4MDEyBDgwMTIP5aSp6Z2S5LqR5q%2BN6ImyD%2BWkqemdkuS6keavjeiJsmQCHw9kFgJmDxUFBDc4ODQENzg4NAQ3ODg0DOaYn%2BWFiem7keiJsgzmmJ%2FlhYnpu5HoibJkAiAPZBYCZg8VBQQyMDgxBDIwODEEMjA4MQPpk7YD6ZO2ZAIhD2QWAmYPFQUEMjA3NgQyMDc2BDIwNzYG6ZO26ImyBumTtuiJsmQCIg9kFgJmDxUFBDIwODcEMjA4NwQyMDg3DOeOieefs%2Be7v%2BiJsgznjonnn7Pnu7%2FoibJkAiMPZBYCZg8VBQQyMDc5BDIwNzkEMjA3OQ%2FmnqPnuqLkupHmr43oibIP5p6j57qi5LqR5q%2BN6ImyZAIkD2QWAmYPFQUEMjA3MwQyMDczBDIwNzMJ54%2BN54%2Bg55m9CeePjeePoOeZvWQCJQ9kFgJmDxUFBDk1NzMEOTU3MwQ5NTczDOePjeePoOeZveiJsgznj43nj6Dnmb3oibJkAiYPZBYCZg8VBQQyMDc4BDIwNzgEMjA3OBXmo5XngbDkupHmr43ph5HlsZ7oibIV5qOV54Gw5LqR5q%2BN6YeR5bGe6ImyZBgCBR5fX0NvbnRyb2xzUmVxdWlyZVBvc3RCYWNrS2V5X18WMgUgcnB0RmF2b3VyYWJsZUxpc3QkY3RsMDAkY2hlY2tib3gFIHJwdEZhdm91cmFibGVMaXN0JGN0bDAxJGNoZWNrYm94BSBycHRGYXZvdXJhYmxlTGlzdCRjdGwwMiRjaGVja2JveAUgcnB0RmF2b3VyYWJsZUxpc3QkY3RsMDMkY2hlY2tib3gFIHJ0cE5vdFB1c2hDYXJMaXN0JGN0bDAwJGNoZWNrYm94BSBydHBOb3RQdXNoQ2FyTGlzdCRjdGwwMSRjaGVja2JveAUgcnRwTm90UHVzaENhckxpc3QkY3RsMDIkY2hlY2tib3gFIHJ0cE5vdFB1c2hDYXJMaXN0JGN0bDAzJGNoZWNrYm94BRRjaGtJc1Nob3dNYWludGVuYW5jZQURY2hrSXNTaG93U2FsZUFkZHIFDGNoa0lzU2hvd01hcAUSY2hrSXNTaG93NDAwTnVtYmVyBSBkZ3ZNZXJjaGFuZGlzZSRjdGwwMSRBbGxDaGVja0JveAUdZGd2TWVyY2hhbmRpc2UkY3RsMDIkQ2hlY2tCb3gFHWRndk1lcmNoYW5kaXNlJGN0bDAzJENoZWNrQm94BR1kZ3ZNZXJjaGFuZGlzZSRjdGwwNCRDaGVja0JveAUdZGd2TWVyY2hhbmRpc2UkY3RsMDUkQ2hlY2tCb3gFHWRndk1lcmNoYW5kaXNlJGN0bDA2JENoZWNrQm94BR1kZ3ZNZXJjaGFuZGlzZSRjdGwwNyRDaGVja0JveAUdZGd2TWVyY2hhbmRpc2UkY3RsMDgkQ2hlY2tCb3gFHWRndk1lcmNoYW5kaXNlJGN0bDA5JENoZWNrQm94BR1kZ3ZNZXJjaGFuZGlzZSRjdGwxMCRDaGVja0JveAUdZGd2TWVyY2hhbmRpc2UkY3RsMTEkQ2hlY2tCb3gFHWRndk1lcmNoYW5kaXNlJGN0bDEyJENoZWNrQm94BR1kZ3ZNZXJjaGFuZGlzZSRjdGwxMyRDaGVja0JveAUdZGd2TWVyY2hhbmRpc2UkY3RsMTQkQ2hlY2tCb3gFHWRndk1lcmNoYW5kaXNlJGN0bDE1JENoZWNrQm94BR1kZ3ZNZXJjaGFuZGlzZSRjdGwxNiRDaGVja0JveAUdZGd2TWVyY2hhbmRpc2UkY3RsMTckQ2hlY2tCb3gFHWRndk1lcmNoYW5kaXNlJGN0bDE4JENoZWNrQm94BR1kZ3ZNZXJjaGFuZGlzZSRjdGwxOSRDaGVja0JveAUdZGd2TWVyY2hhbmRpc2UkY3RsMjAkQ2hlY2tCb3gFHWRndk1lcmNoYW5kaXNlJGN0bDIxJENoZWNrQm94BR1kZ3ZNZXJjaGFuZGlzZSRjdGwyMiRDaGVja0JveAUdZGd2TWVyY2hhbmRpc2UkY3RsMjMkQ2hlY2tCb3gFHWRndk1lcmNoYW5kaXNlJGN0bDI0JENoZWNrQm94BR1kZ3ZNZXJjaGFuZGlzZSRjdGwyNSRDaGVja0JveAUdZGd2TWVyY2hhbmRpc2UkY3RsMjYkQ2hlY2tCb3gFHWRndk1lcmNoYW5kaXNlJGN0bDI3JENoZWNrQm94BR1kZ3ZNZXJjaGFuZGlzZSRjdGwyOCRDaGVja0JveAUdZGd2TWVyY2hhbmRpc2UkY3RsMjkkQ2hlY2tCb3gFHWRndk1lcmNoYW5kaXNlJGN0bDMwJENoZWNrQm94BR1kZ3ZNZXJjaGFuZGlzZSRjdGwzMSRDaGVja0JveAUdZGd2TWVyY2hhbmRpc2UkY3RsMzIkQ2hlY2tCb3gFHWRndk1lcmNoYW5kaXNlJGN0bDMzJENoZWNrQm94BR1kZ3ZNZXJjaGFuZGlzZSRjdGwzNCRDaGVja0JveAUdZGd2TWVyY2hhbmRpc2UkY3RsMzUkQ2hlY2tCb3gFHWRndk1lcmNoYW5kaXNlJGN0bDM2JENoZWNrQm94BR1kZ3ZNZXJjaGFuZGlzZSRjdGwzNyRDaGVja0JveAUdZGd2TWVyY2hhbmRpc2UkY3RsMzgkQ2hlY2tCb3gFDmRndk1lcmNoYW5kaXNlDzwrAAwBCAIBZKxdV5a4fl0e38dlrq31jSUllZfV");//HttpHelper.URLEncode(str_viewstate)

            var viewstategenerator = doc.GetElementbyId("__VIEWSTATEGENERATOR");
            if (viewstategenerator != null)
                str_viewstategenerator = viewstategenerator.GetAttributeValue("value", "");
            sb.AppendFormat("__VIEWSTATEGENERATOR={0}&__ASYNCPOST=true&btnPublish=%E5%8F%91%E5%B8%83", str_viewstategenerator);

            return sb.ToString();
        }

        private void rbtSource_CheckedChanged(object sender, EventArgs e)
        {
            var rbt = (RadioButton)sender;
            InitForm(rbt.Tag.ToString());
        }

        private void btnSend_Ex_Click(object sender, EventArgs e)
        {
            var maxMoney = Convert.ToDecimal(txtMoney.Text);
            var giftPrice = txtPrice.Text;
            GenerateTitleAndLead(maxMoney, maxMoney, giftPrice, csshowname);
            cars.Cars.ForEach(f => f.FavorablePrice = maxMoney);

            var postdata = PushData();

            doc = yc.Post_CheYiTong(url, postdata);
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
                if (NewsType == 1)
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
                    if (title.Length > 36)
                    {
                        title = name + content;
                    }
                }
                else if (NewsType == 2)
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
                else if (NewsType == 3)
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

                if (NewsType == 1)
                {
                    title = dealerShortName + name + content;
                    if (title.Length > 36)
                    {
                        title = name + content;
                    }

                    lead = dealerShortName + name + content + "，感兴趣的朋友可以到店咨询购买，具体优惠信息如下：";
                }
                else if (NewsType == 2)
                {
                    title = "置换" + name + content;

                    lead = dealerShortName + "置换" + name + content + "，感兴趣的朋友可以到店咨询购买，具体优惠信息请见下表：";
                }
                else if (NewsType == 3)
                {
                    title = dealerShortName + name + "火热销售中";
                    if (title.Length > 36)
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
    }

    public class PromotionCars
    {
        public PromotionCars()
        {
            YearType = new List<string>();
            Cars = new List<Car>();
            PublishCarList = new List<Car>();
            GiftInofList = new List<GiftInfo>();
        }

        /// <summary>
        /// 年款
        /// </summary>
        public List<string> YearType { get; set; }
        /// <summary>
        /// 车列表
        /// </summary>
        public List<Car> Cars { get; set; }

        public string CarInfoJson { get {
            StringBuilder sb = new StringBuilder(500);
            sb.Append("[");
            Cars.ForEach(f => {
                sb.Append("{");
                sb.AppendFormat("\"RelaID\"={0},\"CarID\"={1},\"Discount\"={2},\"IsCheck\"={3},\"CarReferPrice\"={4},\"FavorablePrice\"={5},\"IsAllowance\"={6},\"StoreState\"={7},\"ColorName\"=\"{8}\",\"CustomColorName\"=\"{9}\",\"Mark\"={10},\"ExtendCarID\"={11},", f.RelaID, f.CarID, f.Discount, f.IsCheck, f.CarReferPrice, f.FavorablePrice, f.IsAllowance, f.StoreState, f.ColorName, f.CustomColorName, f.Mark, f.ExtendCarID);
                sb.Append("}");
            });
            sb.Append("]");
            return sb.ToString();
        } }

        /// <summary>
        /// 说明
        /// </summary>
        public string Note { get; set; }

        public List<Car> PublishCarList { get; set; }

        public List<GiftInfo> GiftInofList { get; set; }

        public string GiftInofJson { get { return JsonConvert.SerializeObject(GiftInofList); } }
    }

    public class Car
    {
        //var trs = $("#listInfo tr[carinfotr]");
        //var array = new Array();
        //$(trs).each(function () {
        //    var trObj = this;
        //    var obj = new promotionCarInfo();
        //    var inputCarInfo = $(trObj).find("input[carinfo]");
        //    // 主键
        //    obj.RelaID = 0;
        //    //是否勾选
        //    obj.IsCheck = $(trObj).find(":checkbox").eq(0).attr("checked"); 
        //    //车款ID
        //    obj.CarID = parseInt(inputCarInfo.attr("carid"));
        //    // 指导价
        //    obj.CarReferPrice = inputCarInfo.attr("carreferprice");
        //    // 优惠金额
        //    obj.FavorablePrice = $(trObj).find("input[price]").val() == "" ? 0 : $(trObj).find("input[price]").val();
        //    // 折扣率
        //    obj.Discount = $(trObj).find("input[rate]").val() == "" ? 0 : $(trObj).find("input[rate]").val();
        //    // 是否包含惠民补贴
        //    obj.IsAllowance = $(trObj).find("input[allowance]").attr("checked");
        //    // 库存状态
        //    obj.StoreState = $(trObj).find("select[storestate]").val() == "" ? 1 : $(trObj).find("select[storestate]").val();
        //    obj.StoreState = obj.StoreState ? obj.StoreState : 1;
        //    // 车型颜色
        //    obj.ColorName = $(trObj).find(".pick_color a").attr("selectcolor");
        //    // 车型颜色
        //    obj.CustomColorName = $(trObj).find(".pick_color a").attr("costumcolor");
        //    // 标识
        //    obj.Mark = $(this).attr("mark");
        //    // 标识 增配车ID
        //    obj.ExtendCarID = $(this).attr("ExtendCarID");
        //    obj.IsNewEnergy = $(this).attr("isNewEnergy")
        //    if (obj.IsNewEnergy == 1) {
        //        if ($(this).attr("StateSubsidies") != '--') {
        //            obj.StateSubsidies = parseFloat($(this).attr("StateSubsidies")).toFixed(2);
        //        } else {
        //            obj.StateSubsidies = 0;
        //        }
        //        if ($(this).attr("LocalSubsidies") != '--') {
        //            obj.LocalSubsidies = parseFloat($(this).attr("LocalSubsidies")).toFixed(2);
        //        }
        //        else {
        //            obj.LocalSubsidies = 0;
        //        }
        //    } else {
        //        obj.StateSubsidies = 0;
        //        obj.LocalSubsidies = 0;
        //    }
        //    array.push(obj);
        //});

        public int RelaID { get; set; }

        /// <summary>
        /// 车款ID
        /// obj.CarID = parseInt(inputCarInfo.attr("carid"));
        /// </summary>

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
        public decimal PromotionPrice { get { return CarReferPrice - FavorablePrice; } }

        /// <summary>
        /// 库存状态
        //  obj.StoreState = $(trObj).find("select[storestate]").val() == "" ? 1 : $(trObj).find("select[storestate]").val();
        //  obj.StoreState = obj.StoreState ? obj.StoreState : 1;
        /// </summary>
        public int StoreState { get; set; }
                
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
        public string Mark { get; set; }
                
        /// <summary>
        /// 标识 增配车ID
        /// obj.ExtendCarID = $(this).attr("ExtendCarID");
        /// </summary>
        public string ExtendCarID { get; set; }
        
        /// <summary>
        /// obj.IsNewEnergy = $(this).attr("isNewEnergy")
        /// </summary>
        public string IsNewEnergy { get; set; }        

        /// <summary>
        /// //    if (obj.IsNewEnergy == 1) {
        //        if ($(this).attr("StateSubsidies") != '--') {
        //            obj.StateSubsidies = parseFloat($(this).attr("StateSubsidies")).toFixed(2);
        //        } else {
        //            obj.StateSubsidies = 0;
        //        }
        //        if ($(this).attr("LocalSubsidies") != '--') {
        //            obj.LocalSubsidies = parseFloat($(this).attr("LocalSubsidies")).toFixed(2);
        //        }
        //        else {
        //            obj.LocalSubsidies = 0;
        //        }
        //    } else {
        //        obj.StateSubsidies = 0;
        //        obj.LocalSubsidies = 0;
        //    }
        /// </summary>
        public string StateSubsidies { get; set; }
        /// <summary>
        /// //    if (obj.IsNewEnergy == 1) {
        //        if ($(this).attr("StateSubsidies") != '--') {
        //            obj.StateSubsidies = parseFloat($(this).attr("StateSubsidies")).toFixed(2);
        //        } else {
        //            obj.StateSubsidies = 0;
        //        }
        //        if ($(this).attr("LocalSubsidies") != '--') {
        //            obj.LocalSubsidies = parseFloat($(this).attr("LocalSubsidies")).toFixed(2);
        //        }
        //        else {
        //            obj.LocalSubsidies = 0;
        //        }
        //    } else {
        //        obj.StateSubsidies = 0;
        //        obj.LocalSubsidies = 0;
        //    }
        /// </summary>
        public string LocalSubsidies { get; set; }
    }

    public class GiftInfo
    {        
        /// <summary>
        /// 是否赠送礼包
        /// </summary>
        public bool IsCheck { get; set; }

        //礼包 总价值
        public int Price { get; set; }

        // 汽车用品
        public bool QCYPIsCheck { get; set; }
        public string QCYPValue { get; set; }

        // 油卡
        public bool YKIsCheck { get; set; }
        public int YKValue { get; set; }

        // 商业险
        public bool SYXIsCheck { get; set; }
        public int SYXValue { get; set; }

        // 交强险
        public bool JQXIsCheck { get; set; }
        public int JQXValue { get; set; }

        // 购置税
        public bool GZSIsCheck { get; set; }
        public int GZSValue { get; set; }

        // 保养
        public bool BAOYANGIsCheck { get; set; }
        public string BAOYANGValue { get; set; }

        // 其他内容
        public bool OherInfoIsCheck { get; set; }
        public string OherInfoValue { get; set; }
    }

    //this.SetGiftInfoJson = function () {
    //    /// <summary>
    //    ///     设置礼包信息
    //    /// </summary>
    //    var info = JSON.parse($("#hdfGiftInfo").val());

    //    // 是否赠送礼包
    //    $("#btnGift").attr("checked", info.IsCheck);
    //    self.IsShowGiftInfo(info.IsCheck);

    //    if ($("#hdnPromotionType").val() == "1") {
    //        $("#btnGift").attr("disabled", true);
    //        this.IsSetPriceChange(true);
    //    } else {
    //        $("#btnGift").attr("disabled", false);
    //        this.IsSetPriceChange(false);
    //    }

    //    if (info.IsCheck) {
    //        $("#txtPrice").val(info.Price);
    //    }

    //    // 汽车用品
    //    $("#gift1").attr("checked", info.QCYPIsCheck);
    //    self.IsShowInfo($("#gift1"), info.QCYPIsCheck);
    //    if (info.QCYPIsCheck) {
    //        var qcyps = JSON.parse(info.QCYPValue);

    //        merchandise.DelMerchandis();
    //        for (var i = 0; i < qcyps.length; i++) {
    //            var id = qcyps[i].mid;
    //            var name = qcyps[i].mname;
    //            var money = qcyps[i].msalePrice;

    //            merchandise.AddMerchandise(id, name, money);
    //        }
    //    }

    //    // 油卡
    //    $("#gift2").attr("checked", info.YKIsCheck);
    //    self.IsShowInfo($("#gift2"), info.YKIsCheck);
    //    if (info.YKIsCheck) {
    //        $("#txtOilCar").val(info.YKValue);
    //    }


    //    // 商业险
    //    $("#gift3").attr("checked", info.SYXIsCheck);
    //    self.IsShowInfo($("#gift3"), info.SYXIsCheck);
    //    if (info.SYXIsCheck) {
    //        $("#ddlBusinessTax").val(info.SYXValue);
    //    }

    //    // 交强险
    //    $("#gift4").attr("checked", info.JQXIsCheck);
    //    self.IsShowInfo($("#gift4"), info.JQXIsCheck);
    //    if (info.JQXIsCheck) {
    //        $("#ddlBusinessTax").val(info.JQXValue);
    //    }

    //    // 购置税
    //    $("#gift5").attr("checked", info.GZSIsCheck);
    //    self.IsShowInfo($("#gift5"), info.GZSIsCheck);
    //    if (info.GZSIsCheck) {
    //        $("dd[gzs] :radio[value='" + info.GZSValue + "']").attr("checked", true);
    //    }

    //    // 保养
    //    $("#gift6").attr("checked", info.BAOYANGIsCheck);
    //    self.IsShowInfo($("#gift6"), info.BAOYANGIsCheck);
    //    if (info.BAOYANGIsCheck) {
    //        $("dd[baoyang] :radio").attr("checked", false);
    //        $("dd[baoyang] :text").attr("disabled", true);

    //        // 赠送多少元
    //        var baoyao;
    //        if (info.BAOYANGValue.indexOf("元") != -1) {
    //            $("dd[baoyang] :radio[value='1']").attr("checked", true);
    //            $("dd[baoyang] :radio[value='1']").parent().find("input").attr("disabled", false);
    //            baoyao = info.BAOYANGValue;
    //            baoyao = baoyao.replace("元", "");
    //            $("#txtMMoney").val(baoyao);
    //        }

    //        // 赠送多少元
    //        if (info.BAOYANGValue.indexOf("次") != -1) {
    //            $("dd[baoyang] :radio[value='2']").attr("checked", true);
    //            $("dd[baoyang] :radio[value='2']").parent().find("input").attr("disabled", false);
    //            baoyao = info.BAOYANGValue;
    //            baoyao = baoyao.replace("次", "");
    //            $("#txtMTimes").val(baoyao);
    //        }

    //        // 赠送N年：赠送N万公里：
    //        if (info.BAOYANGValue.indexOf("年") != -1 || info.BAOYANGValue.indexOf("公里") != -1) {
    //            $("dd[baoyang] :radio[value='3']").attr("checked", true);
    //            $("dd[baoyang] :radio[value='3']").parent().find("input").attr("disabled", false);

    //            if (info.BAOYANGValue.indexOf("/") != -1) {
    //                baoyao = info.BAOYANGValue;
    //                baoyao = baoyao.replace("年", "");
    //                baoyao = baoyao.replace("万公里", "");

    //                $("#txtMYear").val(baoyao.split("/")[0]);
    //                $("#txtMMile").val(baoyao.split("/")[1]);
    //            } else if (info.BAOYANGValue.indexOf("年") != -1) {
    //                baoyao = info.BAOYANGValue;
    //                baoyao = baoyao.replace("年", "");
    //                $("#txtMYear").val(baoyao);
    //            } else if (info.BAOYANGValue.indexOf("万公里") != -1) {
    //                baoyao = info.BAOYANGValue;
    //                baoyao = baoyao.replace("万公里", "");
    //                $("#txtMMile").val(baoyao);
    //            }
    //        }
    //    }


    //    // 购置税
    //    $("#gift7").attr("checked", info.OherInfoIsCheck);
    //    self.IsShowInfo($("#gift7"), info.OherInfoIsCheck);
    //    if (info.OherInfoIsCheck) {
    //        $("#txtOtherInfo").val(info.OherInfoValue);
    //        if (info.OherInfoValue != errorMsg) {
    //            $("#txtOtherInfo").removeClass("moren");
    //        }
    //    }
    //};

    public class CarNews
    {
        public string Title { get; set; }
        public string Startdate { get; set; }
        public string Enddate { get; set; }
        public string Status { get; set; }
        public int Statusvalue { get; set; }
        public int Carid { get; set; }
        public int Mark { get; set; }
        public int Extendcarid { get; set; }
    }    
}
