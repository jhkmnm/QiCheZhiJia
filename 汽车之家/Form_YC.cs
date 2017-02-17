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
        string ImageUpload = "";
        string SelectExsitPic = "";


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

            dtpPromotionB.Value = DateTime.Now.AddMonths(1).AddDays(1);
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
                    var statesubsidies = node.GetAttributeValue("statesubsidies", "");
                    var localsubsidies = node.GetAttributeValue("localsubsidies", "");

                    var colorcount = tds[7].SelectSingleNode(".//div/p/span");
                    cars.Cars.Add(new Car
                    {
                        CarID = Convert.ToInt32(inputCarInfo.GetAttributeValue("carid", "")),
                        Discount = Convert.ToDecimal(inputrate.GetAttributeValue("value", "0")),
                        IsAllowance = inputallowance != null ? inputallowance.GetAttributeValue("checked", "") : "",
                        IsCheck = true,
                        TypeName = tds[0].GetAttributeValue("title", ""),
                        YearType = typeinput.GetAttributeValue("yeartype", ""),
                        IsNewEnergy = Convert.ToInt32(node.GetAttributeValue("isnewenergy", "0")),
                        Subsidies = tds[4].InnerText,
                        StoreState = 1,
                        CarReferPrice = Convert.ToDecimal(inputCarInfo.GetAttributeValue("carreferprice", "0")),
                        FavorablePrice = Convert.ToDecimal(inputprice.GetAttributeValue("value", "0")),
                        ColorName = colorcount.InnerText,
                        PushedCount = tds[8].InnerText.Trim(), 
                        StateSubsidies = statesubsidies != "--" ? string.Format("{0:0.00}", Convert.ToDecimal(statesubsidies)) : "0",
                        LocalSubsidies = localsubsidies != "--" ? string.Format("{0:0.00}", Convert.ToDecimal(localsubsidies)) : "0",
                    });
                }
            }
            var note = doc.GetElementbyId("LimitCarListNote");
            cars.Note = note.InnerText.Trim();

            var publistCarList = doc.DocumentNode.SelectNodes("//div[@id='LimitPublishCarList']/table/tbody/tr");
            foreach (HtmlNode node in publistCarList)
            {
                var tr = node.GetAttributeValue("mark", "");
                if (!string.IsNullOrWhiteSpace(tr))
                {
                    var tds = node.SelectNodes(".//td");
                    var typeinput = tds[0].SelectSingleNode(".//input[@type='checkbox']");
                    var inputCarInfo = node.SelectSingleNode(".//input[@carinfo='carInfo']");
                    
                    cars.PublishCarList.Add(new Car { 
                        CarID = Convert.ToInt32(typeinput.GetAttributeValue("value", "")),                        
                        CarReferPrice = Convert.ToDecimal(inputCarInfo.GetAttributeValue("carreferprice", "0")),
                        StoreState = 1,
                        IsAllowance = "false"
                    });                    
                }
            }
            #endregion

            #region 图片
            var imgLogo = doc.GetElementbyId("imgLogo");
            imgLogo_hdf.ImgUrl = imgLogo.GetAttributeValue("src", "");
            imgLogo_hdf.CSID = carid;
            imgLogo_hdf.yiche = yc;
            var imgPosition1 = doc.GetElementbyId("imgPosition1");
            imgPosition1_hdf.ImgUrl = imgPosition1.GetAttributeValue("src", "");
            imgPosition1_hdf.CSID = carid;
            imgPosition1_hdf.yiche = yc;
            var imgPosition2 = doc.GetElementbyId("imgPosition2");
            imgPosition2_hdf.ImgUrl = imgPosition2.GetAttributeValue("src", "");
            imgPosition2_hdf.CSID = carid;
            imgPosition2_hdf.yiche = yc;
            var imgPosition3 = doc.GetElementbyId("imgPosition3");
            imgPosition3_hdf.ImgUrl = imgPosition3.GetAttributeValue("src", "");
            imgPosition3_hdf.CSID = carid;
            imgPosition3_hdf.yiche = yc;
            var imgPosition4 = doc.GetElementbyId("imgPosition4");
            imgPosition4_hdf.ImgUrl = imgPosition4.GetAttributeValue("src", "");
            imgPosition4_hdf.CSID = carid;
            imgPosition4_hdf.yiche = yc;
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
            var piclink = doc.DocumentNode.SelectSingleNode("//div[@class='hl_tabs mtm10']/ul/li/a");
            if(piclink != null)
            {
                SelectExsitPic = piclink.GetAttributeValue("href", "");
            }

            var uploadfile = doc.GetElementbyId("imgUploadChangeifrUpLoadFile");
            if(uploadfile != null)
            {
                ImageUpload = uploadfile.GetAttributeValue("src", "");
            }

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
            sb.AppendFormat("hdfCSShowName={0}&", HttpHelper.URLEncode(csshowname, Encoding.UTF8));//HttpHelper.URLEncode(csshowname)
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
            sb.AppendFormat("title_article={0}&", HttpHelper.URLEncode(title_article.Text, Encoding.UTF8));
            sb.AppendFormat("rdoStoreState={0}&", rdoStoreState);
            sb.AppendFormat("txtLead={0}&txtPrice={1}&ddlBusinessTax={2}&", HttpHelper.URLEncode(txtLead.Text, Encoding.UTF8), txtPrice.Text, ddlBusinessTax.SelectedValue);
            sb.AppendFormat("ddlTrafficTax={0}&PurchaseTax={1}&", ddlTrafficTax.SelectedValue, 1);//txtPurchaseTax.Text
            sb.AppendFormat("txtOtherInfo={0}&", HttpHelper.URLEncode(txtOtherInfo.Text, Encoding.UTF8));
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
            sb.AppendFormat("__VIEWSTATE={0}&", HttpHelper.URLEncode(str_viewstate));            

            var viewstategenerator = doc.GetElementbyId("__VIEWSTATEGENERATOR");
            if (viewstategenerator != null)
                str_viewstategenerator = viewstategenerator.GetAttributeValue("value", "");
            sb.AppendFormat("__VIEWSTATEGENERATOR={0}&__ASYNCPOST=true&btnPublish=%E5%8F%91%E5%B8%83", str_viewstategenerator);

            return sb.ToString();
        }

        private string PushDataDetail()
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
            sb.AppendFormat("hdfCSShowName={0}&", HttpHelper.URLEncode(csshowname, Encoding.UTF8));//HttpHelper.URLEncode(csshowname)
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

            //var chklyeartype = doc.DocumentNode.SelectNodes("//input[@name='chklYearType' and @checked='checked']");
            //if (chklyeartype != null)
            //{
            //    foreach (HtmlNode node in chklyeartype)
            //    {
            //        sb.AppendFormat("chklYearType={0}&", node.GetAttributeValue("value", ""));
            //    }
            //}
            carControl1.YearTypeList.ForEach(f => sb.AppendFormat("chklYearType={0}&", f));

            int i = 0;
            carControl1.CarDataSource.Cars.FindAll(w => w.IsCheck).ForEach(f => {
                sb.AppendFormat("{0}={1}&", string.Format("rptFavourableList%24ctl{0:00}%24checkbox", i), cars.Cars[i].CarID);
                sb.AppendFormat("{0}={1}&", string.Format("rptFavourableList%24ctl{0:00}%24rate", i), cars.Cars[i].Discount);
                sb.AppendFormat("{0}={1}&", string.Format("rptFavourableList%24ctl{0:00}%24Text1", i), cars.Cars[i].FavorablePrice);
                i++;
            });
            //for (int i = 0; i < carControl1.CarDataSource.Cars.FindAll(w => w.IsCheck).Count; i++)
            //{
            //    sb.AppendFormat("{0}={1}&", string.Format("rptFavourableList%24ctl{0:00}%24checkbox", i), cars.Cars[i].CarID);
            //    sb.AppendFormat("{0}={1}&", string.Format("rptFavourableList%24ctl{0:00}%24rate", i), cars.Cars[i].Discount);
            //    sb.AppendFormat("{0}={1}&", string.Format("rptFavourableList%24ctl{0:00}%24Text1", i), cars.Cars[i].FavorablePrice);
            //}

            for (i = 0; i < cars.PublishCarList.Count; i++)
            {
                sb.AppendFormat("{0}={1}&", string.Format("rtpNotPushCarList%24ctl{0:00}%24checkbox", i), cars.PublishCarList[i].CarID);
            }
            sb.Append("NewEnergyTitleTemplate=2&");
            sb.AppendFormat("title_article={0}&", HttpHelper.URLEncode(title_article.Text, Encoding.UTF8));
            sb.AppendFormat("rdoStoreState={0}&", rdoStoreState);
            sb.AppendFormat("txtLead={0}&txtPrice={1}&ddlBusinessTax={2}&", HttpHelper.URLEncode(txtLead.Text, Encoding.UTF8), txtPrice.Text, ddlBusinessTax.SelectedValue);
            sb.AppendFormat("ddlTrafficTax={0}&PurchaseTax={1}&", ddlTrafficTax.SelectedValue, 1);//txtPurchaseTax.Text
            sb.AppendFormat("txtOtherInfo={0}&", HttpHelper.URLEncode(txtOtherInfo.Text, Encoding.UTF8));
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
            sb.AppendFormat("__VIEWSTATE={0}&", HttpHelper.URLEncode(str_viewstate));            

            var viewstategenerator = doc.GetElementbyId("__VIEWSTATEGENERATOR");
            if (viewstategenerator != null)
                str_viewstategenerator = viewstategenerator.GetAttributeValue("value", "");
            sb.AppendFormat("__VIEWSTATEGENERATOR={0}&__ASYNCPOST=true&btnPublish=%E5%8F%91%E5%B8%83", str_viewstategenerator);

            //radlst=2093&radlst=9572&radlst=2095&

            return sb.ToString();
        }

        private void rbtSource_CheckedChanged(object sender, EventArgs e)
        {
            var rbt = (RadioButton)sender;
            InitForm(rbt.Tag.ToString());
        }

        private void btnSend_Ex_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtMoney.Text))
            {
                MessageBox.Show("请输入优惠金额");
                return;
            }            

            var maxMoney = Convert.ToDecimal(txtMoney.Text);

            if(maxMoney == 0)
            {
                MessageBox.Show("优惠金额为0，请选择礼包");
                return;
            }

            var giftPrice = txtPrice.Text;
            GenerateTitleAndLead(maxMoney, maxMoney, giftPrice, csshowname);
            cars.Cars.ForEach(f => f.FavorablePrice = maxMoney);

            var postdata = PushData();

            var result = yc.Post_CheYiTong(url, postdata);
            if(result.DocumentNode.OuterHtml.Contains("NewsSuccess.aspx"))
            {
                MessageBox.Show("发布成功");
            }
            else
            {
                MessageBox.Show("发布失败");
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
                    if (title.Length > 18)
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
                    if (title.Length > 18)
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

        public List<GiftInfo> GiftInofList { get; set; }

        public string GiftInofJson {
            get
            {
                StringBuilder sb = new StringBuilder(500);
                if (GiftInofList.Count > 0)
                {
                    sb.Append("[");
                    GiftInofList.ForEach(f =>
                    {
                        sb.Append("{");
                        sb.AppendFormat("\"IsCheck\":{0},\"Price\":{1},\"QCYPIsCheck\":\"{2}\",\"QCYPValue\":\"{3}\",\"YKIsCheck\":{4},\"YKValue\":{5},\"SYXIsCheck\":{6},\"SYXValue\":{7},\"JQXIsCheck\":{8},\"JQXValue\":{9},\"GZSIsCheck\":{10},\"GZSValue\":\"{11}\",\"BAOYANGIsCheck\":{12},\"BAOYANGValue\":\"{13}\",\"OherInfoIsCheck\":\"{14}\",\"OherInfoValue\":\"{15}\"", f.IsCheck.ToString().ToLower(), f.Price, f.QCYPIsCheck.ToString().ToLower(), f.IsCheck.ToString().ToLower(), f.QCYPValue, f.YKIsCheck, f.YKValue, f.SYXIsCheck.ToString().ToLower(), f.SYXValue, f.JQXIsCheck.ToString().ToLower(), f.JQXValue, f.GZSIsCheck.ToString().ToLower(), f.GZSValue, f.BAOYANGIsCheck.ToString().ToLower(), f.BAOYANGValue, f.OherInfoIsCheck.ToString().ToLower(), f.OherInfoValue);
                        sb.Append("},");
                    });
                    sb.Append("]");
                }
                else
                {
                    sb.Append("{\"IsCheck\":false,\"Price\":0,\"QCYPIsCheck\":false,\"QCYPValue\":\"\",\"YKIsCheck\":false,\"YKValue\":0,\"SYXIsCheck\":false,\"SYXValue\":1,\"JQXIsCheck\":false,\"JQXValue\":1,\"GZSIsCheck\":false,\"GZSValue\":1,\"BAOYANGIsCheck\":false,\"BAOYANGValue\":\"\",\"OherInfoIsCheck\":false,\"OherInfoValue\":\"\"}");
                }
                return sb.ToString();
            }
        }
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
