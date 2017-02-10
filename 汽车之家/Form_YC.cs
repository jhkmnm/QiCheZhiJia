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
                    var inputCarInfo = node.SelectSingleNode(".//input[@carinfo='carInfo']");
                    var inputrate = node.SelectSingleNode(".//input[@rate='rate']");
                    var inputallowance = node.SelectSingleNode(".//input[@allowance='allowance']");
                    var inputprice = node.SelectSingleNode(".//input[@price='price']");                    

                    var colorcount = tds[7].SelectSingleNode(".//div/p/span");
                    cars.Cars.Add(new Car {
                        CarID = Convert.ToInt32(inputCarInfo.GetAttributeValue("carid", "")),
                        Discount = Convert.ToDecimal(inputrate.GetAttributeValue("value", "0")),
                        IsAllowance = inputallowance != null ? inputallowance.GetAttributeValue("checked", "") : "", 
                        IsCheck = "checked",
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
            cars.Note = note.InnerText;

            var publistCarList = doc.DocumentNode.SelectNodes("//div[@id='LimitPublishCarList']/table/tr");
            foreach(HtmlNode node in publistCarList)
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
                sb.AppendFormat("{0}={1}&", HttpHelper.URLEncode(string.Format("rptFavourableList_ctl{0:00}_checkbox", i)), cars.Cars[i].CarID);
                sb.AppendFormat("{0}={1}&", HttpHelper.URLEncode(string.Format("rptFavourableList_ctl{0:00}_Text1", i)), cars.Cars[i].FavorablePrice);
            }
            //rptFavourableList_ctl00_rate      rptFavourableList%24ctl00%24rate=0&

            for (int i = 0; i < cars.PublishCarList.Count; i++)
            {
                sb.AppendFormat("{0}={1}&", HttpHelper.URLEncode(string.Format("rtpNotPushCarList_ctl{0:00}_checkbox", i)), cars.PublishCarList[i].CarID);
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
            sb.AppendFormat("hdfGiftInfo={0}&imgUploadChangehidethumburl=&imgUploadChangehideUrl=&txtStartPrice=&", cars.GiftInofJson);
            sb.Append("txtEndPrice=&txtKeyword=&__EVENTTARGET=&__EVENTARGUMENT=&");

            var viewstate = doc.GetElementbyId("__VIEWSTATE");
            sb.AppendFormat("__VIEWSTATE={0}&", HttpHelper.URLEncode(viewstate.GetAttributeValue("value", "")));

            var viewstategenerator = doc.GetElementbyId("__VIEWSTATEGENERATOR");
            sb.AppendFormat("__VIEWSTATEGENERATOR={0}&__ASYNCPOST=true&btnPublish=%E5%8F%91%E5%B8%83", viewstategenerator.GetAttributeValue("value", ""));
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

        public string CarInfoJson { get {
            StringBuilder sb = new StringBuilder(500);
            sb.Append("[");
            Cars.ForEach(f =>
                sb.AppendFormat("{\"RelaID\"={0},\"CarID\"={1},\"Discount\"={2},\"IsCheck\"={3},\"CarReferPrice\"={4},\"FavorablePrice\"={5},\"IsAllowance\"={6},\"StoreState\"={7},\"ColorName\"=\"{8}\",\"CustomColorName\"=\"{9}\",\"Mark\"={10},\"ExtendCarID\"={11}},", f.RelaID, f.CarID, f.Discount, f.IsCheck, f.CarReferPrice, f.FavorablePrice, f.IsAllowance, f.StoreState, f.ColorName, f.CustomColorName, f.Mark, f.ExtendCarID)
            );
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
        public string IsCheck { get; set; }

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
}
