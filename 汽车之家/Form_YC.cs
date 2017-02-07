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
                rbt.Tag = value;
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
                rbt.AutoSize = true;
                rbt.Size = new System.Drawing.Size(101, 16);
                rbt.TabStop = true;
                rbt.UseVisualStyleBackColor = true;
                this.pStoreState.Controls.Add(rbt);
            }
            #endregion            
        }

        private void InitDetail()
        {
            var article = doc.GetElementbyId("title_article");
            title_article.Text = article.GetAttributeValue("backvalue", "");
            var number = doc.GetElementbyId("title_number");
            title_number.Text = number.InnerText;
            var lead = doc.GetElementbyId("txtLead");
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
            PromotionCars cars = new PromotionCars();
            cars.Cars = new List<Car>();
            var yeartype = doc.DocumentNode.SelectNodes("//input[@name='chklYearType']");
            yeartype.ToList().ForEach(f => cars.YearType.Add(f.GetAttributeValue("value", "")));

            var cartrs = doc.DocumentNode.SelectNodes("//tbody[@id='listInfo']/tr");
            foreach(HtmlNode node in cartrs)
            {
                var carinfotr = node.GetAttributeValue("carinfotr", "");
                if(!string.IsNullOrWhiteSpace(carinfotr))
                {
                    var tds = node.SelectNodes(".//td");
                    var car = new Car();
                    car.TypeName = tds[0].GetAttributeValue("title", "");
                    var typeinput = tds[0].SelectSingleNode(".//input[@type='checkbox']");
                    car.YearType = typeinput.GetAttributeValue("yeartype", "");
                    car.ID = Convert.ToInt32(typeinput.GetAttributeValue("value", ""));
                }
            }
            var note = doc.GetElementbyId("LimitCarListNote");
            cars.Note = note.InnerText;
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
            var value = rbt.Tag.ToString();
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

            var hdfselectcarbrandid = doc.GetElementbyId("hdfSelectCarBrandID");
            sb.AppendFormat("hdfSelectCarBrandID={0}&", hdfselectcarbrandid.GetAttributeValue("value", ""));

            var hdfselectserialid = doc.GetElementbyId("hdfSelectSerialID");
            sb.AppendFormat("hdfSelectSerialID={0}&", hdfselectserialid.GetAttributeValue("value", ""));

            var hdfselectserialtype = doc.GetElementbyId("hdfSelectSerialType");
            sb.AppendFormat("hdfSelectSerialType={0}&", hdfselectserialtype.GetAttributeValue("value", ""));

            var hdfnewstitletemplate = doc.GetElementbyId("hdfNewsTitleTemplate");
            sb.AppendFormat("hdfNewsTitleTemplate={0}&", hdfnewstitletemplate.GetAttributeValue("value", ""));

            var hdfcansettitlearticle = doc.GetElementbyId("hdfCanSetTitleArticle");
            sb.AppendFormat("hdfCanSetTitleArticle={0}&", hdfcansettitlearticle.GetAttributeValue("value", ""));

            var hdfcsshowname = doc.GetElementbyId("hdfCSShowName");
            sb.AppendFormat("hdfCSShowName={0}&", HttpHelper.URLEncode(hdfcsshowname.GetAttributeValue("value", "")));

            var hdflastcheckedid = doc.GetElementbyId("hdfLastCheckedID");
            sb.AppendFormat("hdfLastCheckedID={0}&", hdflastcheckedid.GetAttributeValue("value", ""));
            
            var hdfnewstype = doc.GetElementbyId("hdfNewsType");
            sb.AppendFormat("hdfNewsType={0}&", hdfnewstype.GetAttributeValue("value", ""));

            var rptcarbrandhdfcbid = doc.GetElementbyId("rptCarBrand_ctl00_hdfCBID");
            sb.AppendFormat("{0}={1}&",HttpHelper.URLEncode("rptCarBrand$ctl00$hdfCBID"), rptcarbrandhdfcbid.GetAttributeValue("value", ""));
            
            sb.AppendFormat("CarSerialGroup={0}&hdfCarIDs=&hdfCarNewsList=&", value);

            var hdfofflinecount = doc.GetElementbyId("hdfOffLineCount");
            sb.AppendFormat("hdfOffLineCount={0}&", hdfofflinecount.GetAttributeValue("value", ""));

            var hdfmindata = doc.GetElementbyId("hdfMinData");
            sb.AppendFormat("hdfMinData={0}&", hdfmindata.GetAttributeValue("value", ""));
            
            sb.AppendFormat("txtDateTimeBegin={0}&", dtpPromotionA.Value.ToString("yyyy-MM-dd"));
            sb.AppendFormat("txtDateTimeEnd={0}&", dtpPromotionB.Value.ToString("yyyy-MM-dd"));
            sb.AppendFormat("title_article=&rdoStoreState={0}txtLead=&txtPrice=&", rdoStoreState);
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
        /// <summary>
        /// 年款
        /// </summary>
        public List<string> YearType { get; set; }
        /// <summary>
        /// 车列表
        /// </summary>
        public List<Car> Cars { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Note { get; set; }
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
        public decimal PromotionPrice { get; set; }
        /// <summary>
        /// 库存状态
        /// </summary>
        public int StoreState { get; set; }
        /// <summary>
        /// 车身颜色
        /// </summary>
        public string SelectColor { get; set; }
    }
}
