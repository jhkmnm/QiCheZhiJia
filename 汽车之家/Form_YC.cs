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


        List<TextValue> discountRate = new List<TextValue>();
        List<TextValue> PurchaseTax = new List<TextValue>();
        List<TextValue> CompulsoryInsurance = new List<TextValue>();
        List<TextValue> CommercialInsurance = new List<TextValue>();
        string[] strCompulsory = { "不赠送交强险", "赠送1年交强险", "赠送2年交强险", "赠送3年交强险", "赠送4年交强险", "赠送5年交强险", "赠送6年交强险", "赠送7年交强险", "赠送8年交强险", "赠送9年交强险" };

        string[] strCommercial = { "不赠送商业险", "赠送1年商业险", "赠送2年商业险", "赠送3年商业险", "赠送4年商业险", "赠送5年商业险", "赠送6年商业险", "赠送7年商业险", "赠送8年商业险", "赠送9年商业险"};
        public Form_YC(YiChe yc)
        {
            InitializeComponent();
            this.yc = yc;
            promotionType.AddRange(new[] { new TextValue { Text = "优惠金额", Value = "0" }, new TextValue { Text = "优惠折扣率", Value = "1" } });


            discountRate.AddRange(new[] { new TextValue { Text = "优惠金额", Value = "0" }, new TextValue { Text = "优惠折扣率", Value = "1" } });

            PurchaseTax.AddRange(new[] { new TextValue { Text = "不赠送购置税", Value = "0" }, new TextValue { Text = "赠送50%购置税", Value = "50" }, new TextValue { Text = "赠送100%购置税", Value = "100" } });

            for (int i = 0; i < strCompulsory.Length;i++)
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
                var label = stateList.SelectSingleNode(".//label[@for='"+ stateInputs[i].Id +"']");
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
            if(lead != null)
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
                int x = xstart + (xstep * i);
                int y = ystart;

                if (i > 3)
                {
                    x = xstart + (xstep * (i - 4));
                    y = ystart + ystep;
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
            foreach(HtmlNode node in cartrs)
            {
                var carinfotr = node.GetAttributeValue("carinfotr", "");
                if(!string.IsNullOrWhiteSpace(carinfotr))
                {
                    var tds = node.SelectNodes(".//td");
                    var typeinput = tds[0].SelectSingleNode(".//input[@type='checkbox']");
                    var colorcount = tds[7].SelectSingleNode(".//div/p/span");                    
                    cars.Cars.Add(new Car { 
                        TypeName = tds[0].GetAttributeValue("title", ""),
                        YearType = typeinput.GetAttributeValue("yeartype", ""),
                        ID = Convert.ToInt32(typeinput.GetAttributeValue("value", "")),
                        Subsidies = tds[4].InnerText,
                        StoreState = 1,
                        CarReferPrice = Convert.ToDecimal(tds[1].InnerText),
                        Money = 0,
                        SelectColor = colorcount.InnerText,
                        PushedCount = tds[8].InnerText.Trim()
                    });
                }
            }
            var note = doc.GetElementbyId("LimitCarListNote");
            cars.Note = note.InnerText;

            var publistCarList = doc.DocumentNode.SelectNodes("//div[@id='LimitPublishCarList']/table/tr");
            foreach(HtmlNode node in publistCarList)
            {
                var tr = node.GetAttributeValue("mark", "");
                if (!string.IsNullOrWhiteSpace(tr))
                {
                    var tds = node.SelectNodes(".//td");
                    var typeinput = tds[0].SelectSingleNode(".//input[@type='checkbox']");
                    cars.PublishCarList.Add(new Car { ID = Convert.ToInt32(typeinput.GetAttributeValue("value", "")) });
                }
            }
            #endregion
        }

        void StoreState_CheckedChanged(object sender, EventArgs e)
        {
            var rbt = (RadioButton)sender;
            rdoStoreState = rbt.Tag.ToString();
        }

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
            sb.AppendFormat("HADRDCCID={0}&", HttpHelper.URLEncode(hadrdccid.GetAttributeValue("value", "")));

            var hacbcidsid = doc.GetElementbyId("HACBCIDSID");
            sb.AppendFormat("HACBCIDSID={0}&", HttpHelper.URLEncode(hacbcidsid.GetAttributeValue("value", "")));

            var hcbcidsid = doc.GetElementbyId("HCBCIDSID");
            sb.AppendFormat("HCBCIDSID={0}&", HttpHelper.URLEncode(hcbcidsid.GetAttributeValue("value", "")));

            var viewstate = doc.GetElementbyId("__VIEWSTATE");
            sb.AppendFormat("__VIEWSTATE={0}&", HttpHelper.URLEncode(viewstate.GetAttributeValue("value", "")));

            var viewstategenerator = doc.GetElementbyId("__VIEWSTATEGENERATOR");
            sb.AppendFormat("__VIEWSTATEGENERATOR={0}&", viewstategenerator.GetAttributeValue("value", ""));
                        
            sb.AppendFormat("hdfSelectCarBrandID={0}&", cbid);
            sb.AppendFormat("hdfSelectSerialID={0}&", carid);
            sb.AppendFormat("hdfSelectSerialType={0}&", energytype);
            sb.AppendFormat("hdfNewsTitleTemplate={0}&", energytype == "0" ? "1" : "2");
            sb.AppendFormat("hdfCanSetTitleArticle={0}&", energytype == "0" ? "0" : "1");
            sb.AppendFormat("hdfCSShowName={0}&", HttpHelper.URLEncode(csshowname));
            sb.AppendFormat("hdfLastCheckedID={0}&", carid);
            
            var hdfnewstype = doc.GetElementbyId("hdfNewsType");
            sb.AppendFormat("hdfNewsType={0}&", hdfnewstype.GetAttributeValue("value", ""));

            var rptcarbrandhdfcbid = doc.GetElementbyId("rptCarBrand_ctl00_hdfCBID");
            sb.AppendFormat("{0}={1}&",HttpHelper.URLEncode("rptCarBrand$ctl00$hdfCBID"), rptcarbrandhdfcbid.GetAttributeValue("value", ""));
            
            sb.AppendFormat("CarSerialGroup={0}&hdfCarIDs=&hdfCarNewsList=&", carid);

            var hdfofflinecount = doc.GetElementbyId("hdfOffLineCount");
            sb.AppendFormat("hdfOffLineCount={0}&", hdfofflinecount.GetAttributeValue("value", ""));

            var hdfmindata = doc.GetElementbyId("hdfMinData");
            sb.AppendFormat("hdfMinData={0}&", hdfmindata.GetAttributeValue("value", ""));
            
            sb.AppendFormat("txtDateTimeBegin={0}&", dtpPromotionA.Value.ToString("yyyy-MM-dd"));
            sb.AppendFormat("txtDateTimeEnd={0}&", dtpPromotionB.Value.ToString("yyyy-MM-dd"));
            sb.AppendFormat("title_article=&rdoStoreState={0}&txtLead=&txtPrice=&", rdoStoreState);
            sb.Append("ddlBusinessTax=1&ddlTrafficTax=1&PurchaseTax=1&");
            sb.Append("txtOtherInfo=%E5%A6%82%E7%A4%BC%E5%8C%85%E8%A7%84%E5%88%99%E6%88%96%E7%A4%BC%E5%8C%85%E5%86%85%E5%AE%B9&");
            sb.Append("imgLogo_hdf=&hdfSerial=&hdfImgSelectID=&imgPosition1_hdf=&imgPosition2_hdf=&imgPosition3_hdf=&imgPosition4_hdf=&");
            sb.Append("chkIsShowMaintenance=on&chkIsShowSaleAddr=on&chkIsShowMap=on&chkIsShow400Number=on&hdfCurrentState=0&hdnType=money&");
            sb.Append("hdnCarMerchandiseID=&hdnPromotionType=&hdfCarInfoJson=&hdfGiftInfo=&imgUploadChangehidethumburl=&imgUploadChangehideUrl=&");
            sb.Append("txtStartPrice=&txtEndPrice=&txtKeyword=&__ASYNCPOST=true&btnReferInfo=%E5%88%B7%E6%96%B0");
            #endregion
            doc = yc.Post_CheYiTong(url, sb.ToString());
            InitDetail();
        }

        private void PushData() 
        {
            StringBuilder sb = new StringBuilder(5000);
            sb.Append("scriptManager=UpdatePanel4%7CbtnPublish&");

            var hadrdccid = doc.GetElementbyId("HADRDCCID");
            sb.AppendFormat("HADRDCCID={0}&", HttpHelper.URLEncode(hadrdccid.GetAttributeValue("value", "")));

            var hacbcidsid = doc.GetElementbyId("HACBCIDSID");
            sb.AppendFormat("HACBCIDSID={0}&", HttpHelper.URLEncode(hacbcidsid.GetAttributeValue("value", "")));

            var hcbcidsid = doc.GetElementbyId("HCBCIDSID");
            sb.AppendFormat("HCBCIDSID={0}&", HttpHelper.URLEncode(hcbcidsid.GetAttributeValue("value", "")));

            sb.AppendFormat("hdfSelectCarBrandID={0}&", cbid);
            sb.AppendFormat("hdfSelectSerialID={0}&", carid);
            sb.AppendFormat("hdfSelectSerialType={0}&", energytype);
            sb.AppendFormat("hdfNewsTitleTemplate={0}&", energytype == "0" ? "1" : "2");
            sb.AppendFormat("hdfCanSetTitleArticle={0}&", energytype == "0" ? "0" : "1");
            sb.AppendFormat("hdfCSShowName={0}&", HttpHelper.URLEncode(csshowname));
            sb.AppendFormat("hdfLastCheckedID={0}&", carid);

            var hdfnewstype = doc.GetElementbyId("hdfNewsType");
            sb.AppendFormat("hdfNewsType={0}&", hdfnewstype.GetAttributeValue("value", ""));

            var rptcarbrandhdfcbid = doc.GetElementbyId("rptCarBrand_ctl00_hdfCBID");
            sb.AppendFormat("{0}={1}&", HttpHelper.URLEncode("rptCarBrand$ctl00$hdfCBID"), rptcarbrandhdfcbid.GetAttributeValue("value", ""));

            sb.AppendFormat("CarSerialGroup={0}&hdfCarIDs=&", carid);

            var hdfcarnewslist = doc.GetElementbyId("hdfCarNewsList");
            sb.AppendFormat("hdfCarNewsList={0}&", HttpHelper.URLEncode(hdfcarnewslist.GetAttributeValue("value", "")));

            var hdfofflinecount = doc.GetElementbyId("hdfOffLineCount");
            sb.AppendFormat("hdfOffLineCount={0}&", hdfofflinecount.GetAttributeValue("value", ""));

            var hdfmindata = doc.GetElementbyId("hdfMinData");
            sb.AppendFormat("hdfMinData={0}&", hdfmindata.GetAttributeValue("value", ""));

            sb.AppendFormat("txtDateTimeBegin={0}&", dtpPromotionA.Value.ToString("yyyy-MM-dd"));
            sb.AppendFormat("txtDateTimeEnd={0}&", dtpPromotionB.Value.ToString("yyyy-MM-dd"));

            var chklyeartype = doc.DocumentNode.SelectNodes("//input[@name='chklYearType' and @checked='checked']");
            if(chklyeartype != null)
            {
                foreach(HtmlNode node in chklyeartype)
                {
                    sb.AppendFormat("chklYearType={0}&", node.GetAttributeValue("value", ""));
                }
            }

            for(int i=0;i < cars.Cars.Count;i++)
            {
                sb.AppendFormat("{0}={1}&", HttpHelper.URLEncode(string.Format("rptFavourableList_ctl{0:00}_checkbox", i)), cars.Cars[i].ID);
                sb.AppendFormat("{0}={1}&", HttpHelper.URLEncode(string.Format("rptFavourableList_ctl{0:00}_Text1", i)), cars.Cars[i].Money);
            }
            //rptFavourableList_ctl00_rate      rptFavourableList%24ctl00%24rate=0&

            for (int i = 0; i < cars.PublishCarList.Count; i++)
            {
                sb.AppendFormat("{0}={1}&", HttpHelper.URLEncode(string.Format("rtpNotPushCarList_ctl{0:00}_checkbox", i)), cars.PublishCarList[i].ID);
            }
            sb.Append("NewEnergyTitleTemplate=2&");
            sb.AppendFormat("title_article={0}&", title_article.Text);
            sb.AppendFormat("rdoStoreState={0}&", rdoStoreState);
            sb.AppendFormat("txtLead={0}&txtPrice={1}&ddlBusinessTax={2}&", txtLead.Text, txtPrice.Text, ddlBusinessTax.SelectedValue);
            sb.AppendFormat("ddlTrafficTax={0}&PurchaseTax={1}&", ddlTrafficTax.SelectedValue, txtPurchaseTax.Text);
            sb.AppendFormat("txtOtherInfo={0}&", txtOtherInfo.Text);
            sb.AppendFormat("imgLogo_hdf={0}&hdfSerial={1}&hdfImgSelectID=&", imgLogo_hdf.ImgUrl, carid, imgLogo_hdf.ImgSelectID);
            sb.AppendFormat("imgPosition1_hdf={0}&", imgPosition1_hdf.ImgUrl);
            sb.AppendFormat("imgPosition2_hdf={0}&", imgPosition2_hdf.ImgUrl);
            sb.AppendFormat("imgPosition3_hdf={0}&", imgPosition3_hdf.ImgUrl);
            sb.AppendFormat("imgPosition4_hdf={0}&", imgPosition4_hdf.ImgUrl);
            sb.AppendFormat("chkIsShowMaintenance={0}&", chkIsShowMaintenance.Checked ? "on" : "off");
            sb.AppendFormat("chkIsShowSaleAddr={0}&", chkIsShowSaleAddr.Checked ? "on" : "off");
            sb.AppendFormat("chkIsShowMap={0}&", chkIsShowMap.Checked ? "on" : "off");
            sb.AppendFormat("chkIsShow400Number={0}&", chkIsShow400Number.Checked ? "on" : "off");

            var hdfcurrentstate = doc.GetElementbyId("hdfCurrentState");
            sb.AppendFormat("hdfCurrentState={0}&", hdfcurrentstate.GetAttributeValue("value", ""));
            sb.AppendFormat("hdnType={0}&hdnCarMerchandiseID=&", ddlPromotionType.SelectedValue);
            sb.AppendFormat("hdnPromotionType={0}&", ddlPromotionType.SelectedIndex);
            sb.AppendFormat("hdfCarInfoJson={0}&", cars.CarInfoJson);

            //hdfGiftInfo=%7B%22IsCheck%22%3Afalse%2C%22Price%22%3A0%2C%22QCYPIsCheck%22%3Afalse%2C%22QCYPValue%22%3A%22%22%2C%22YKIsCheck%22%3Afalse%2C%22YKValue%22%3A0%2C%22SYXIsCheck%22%3Afalse%2C%22SYXValue%22%3A1%2C%22JQXIsCheck%22%3Afalse%2C%22JQXValue%22%3A1%2C%22GZSIsCheck%22%3Afalse%2C%22GZSValue%22%3A1%2C%22BAOYANGIsCheck%22%3Afalse%2C%22BAOYANGValue%22%3A%22%22%2C%22OherInfoIsCheck%22%3Afalse%2C%22OherInfoValue%22%3A%22%22%7D&

            //imgUploadChangehidethumburl=&imgUploadChangehideUrl=&txtStartPrice=&txtEndPrice=&txtKeyword=&__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE=%2FwEPDwUKMTcyOTg5MDk4OA8WBB4JQ2FyU2VyaWFsMt8jAAEAAAD%2F%2F%2F%2F%2FAQAAAAAAAAAMAgAAAE5TeXN0ZW0uRGF0YSwgVmVyc2lvbj00LjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPWI3N2E1YzU2MTkzNGUwODkFAQAAABVTeXN0ZW0uRGF0YS5EYXRhVGFibGUDAAAAGURhdGFUYWJsZS5SZW1vdGluZ1ZlcnNpb24JWG1sU2NoZW1hC1htbERpZmZHcmFtAwEBDlN5c3RlbS5WZXJzaW9uAgAAAAkDAAAABgQAAADVCDw%2FeG1sIHZlcnNpb249IjEuMCIgZW5jb2Rpbmc9InV0Zi0xNiI%2FPg0KPHhzOnNjaGVtYSB4bWxucz0iIiB4bWxuczp4cz0iaHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEiIHhtbG5zOm1zZGF0YT0idXJuOnNjaGVtYXMtbWljcm9zb2Z0LWNvbTp4bWwtbXNkYXRhIj4NCiAgPHhzOmVsZW1lbnQgbmFtZT0iVGFibGUiPg0KICAgIDx4czpjb21wbGV4VHlwZT4NCiAgICAgIDx4czpzZXF1ZW5jZT4NCiAgICAgICAgPHhzOmVsZW1lbnQgbmFtZT0iQ1NJRCIgdHlwZT0ieHM6aW50IiBtc2RhdGE6dGFyZ2V0TmFtZXNwYWNlPSIiIG1pbk9jY3Vycz0iMCIgLz4N&__VIEWSTATEGENERATOR=5DAC1E26&__ASYNCPOST=true&btnPublish=%E5%8F%91%E5%B8%83
        }

        private void rbtSource_CheckedChanged(object sender, EventArgs e)
        {
            var rbt = (RadioButton)sender;
            InitForm(rbt.Tag.ToString());
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            btnDetail.Visible = false;
            pDetail.Location = pSimple.Location;
            pSimple.Visible = false;
            btnSend.Location = new Point(btnSend.Location.X, btnSend.Location.Y + pDetail.Size.Height + 27);
        }
    }

    public class PromotionCars
    {
        public PromotionCars()
        {
            YearType = new List<string>();
            Cars = new List<Car>();
            PublishCarList = new List<Car>();
        }

        /// <summary>
        /// 年款
        /// </summary>
        public List<string> YearType { get; set; }
        /// <summary>
        /// 车列表
        /// </summary>
        public List<Car> Cars { get; set; }

        public string CarInfoJson { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Note { get; set; }

        public List<Car> PublishCarList { get; set; }
    }

    public class Car
    {
        public int ID { get; set; }
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
        /// </summary>
        public decimal CarReferPrice { get; set; }
        /// <summary>
        /// 优惠金额(万)
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 新能源车补贴(万)
        /// </summary>
        public string Subsidies { get; set; }
        /// <summary>
        /// 优惠价(万)
        /// </summary>
        public decimal PromotionPrice { get { return CarReferPrice - Money; } }
        /// <summary>
        /// 库存状态
        /// </summary>
        public int StoreState { get; set; }
        /// <summary>
        /// 车身颜色
        /// </summary>
        public string SelectColor { get; set; }

        public string PushedCount { get; set; }
    }
}
