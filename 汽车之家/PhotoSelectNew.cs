using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Text;
using CsharpHttpHelper;

namespace Aide
{
    public partial class PhotoSelectNew : Form
    {
        YiChe yc;
        string csid = "";
        string photoUrl = "http://das.app.easypass.cn/FileManageForMut/CarPhoto/PhotoSelectNew.aspx?csid=";
        HtmlAgilityPack.HtmlDocument doc;
        PictureBox PreviousSelected;
        PictureBox CurrentSelected;
        TabPage currentPage;
        string str_viewstate;
        string str_viewstategenerator;
        string str_eventvalidation;

        public string SelectedImgPath
        {
            get { return CurrentSelected.ImageLocation; }
        }

        public PhotoSelectNew(YiChe yc, string csid)
        {
            InitializeComponent();
            this.yc = yc;
            this.csid = csid;
            currentPage = lbtn1;
            InitDll();
        }

        /// <summary>
        /// GB2312转换成UTF8
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string gbk_utf8(string text)
        {
            byte[] buffer = System.Text.Encoding.GetEncoding("GBK").GetBytes(text);
            return System.Text.Encoding.UTF8.GetString(buffer);            
        }

        private void InitPostData()
        {
            StringBuilder sb = new StringBuilder(5000);
            sb.Append("ScriptManager1=UpdatePanel1%7Clbtn2&__LASTFOCUS=&");
            sb.AppendFormat("__EVENTTARGET={0}&", currentPage.Name);    //分页还是更换标签
            //__EVENTARGUMENT = &  页码
            var viewstate = doc.GetElementbyId("__VIEWSTATE");
            if (viewstate != null)
                str_viewstate = viewstate.GetAttributeValue("value", "");
            sb.AppendFormat("__VIEWSTATE={0}&", HttpHelper.URLEncode(str_viewstate));

            var viewstategenerator = doc.GetElementbyId("__VIEWSTATEGENERATOR");
            if (viewstategenerator != null)
                str_viewstategenerator = viewstategenerator.GetAttributeValue("value", "");
            sb.AppendFormat("__VIEWSTATEGENERATOR={0}&", str_viewstategenerator);

            var eventvalidation = doc.GetElementbyId("__EVENTVALIDATION");
            if (eventvalidation != null)
                str_eventvalidation = eventvalidation.GetAttributeValue("value", "");
            sb.AppendFormat("__EVENTVALIDATION={0}&", str_viewstategenerator);

            //"ddCarYear=0&ddlCarStyle=0&ddlCarColors=0&";

            // "rptImgs%24ctl00%24hidImgUrl=http%3A%2F%2Fimg2.bitauto.com%2Fautoalbum%2Ffiles%2F20170110%2F626%2F10121662603640_5421061_3.jpg&rptImgs%24ctl01%24hidImgUrl=http%3A%2F%2Fimg4.bitauto.com%2Fautoalbum%2Ffiles%2F20170110%2F399%2F09260039931461_5421049_3.jpg&rptImgs%24ctl02%24hidImgUrl=http%3A%2F%2Fimg2.bitauto.com%2Fautoalbum%2Ffiles%2F20170110%2F770%2F09255977023163_5421048_3.jpg&rptImgs%24ctl03%24hidImgUrl=http%3A%2F%2Fimg4.bitauto.com%2Fautoalbum%2Ffiles%2F20170110%2F075%2F09255907524102_5421047_3.jpg&rptImgs%24ctl04%24hidImgUrl=http%3A%2F%2Fimg2.bitauto.com%2Fautoalbum%2Ffiles%2F20170110%2F421%2F09255842112216_5421046_3.jpg&rptImgs%24ctl05%24hidImgUrl=http%3A%2F%2Fimg4.bitauto.com%2Fautoalbum%2Ffiles%2F20170110%2F663%2F09255766310886_5421045_3.jpg&rptImgs%24ctl06%24hidImgUrl=http%3A%2F%2Fimg2.bitauto.com%2Fautoalbum%2Ffiles%2F20170110%2F881%2F09255688115969_5421044_3.jpg&rptImgs%24ctl07%24hidImgUrl=http%3A%2F%2Fimg4.bitauto.com%2Fautoalbum%2Ffiles%2F20170110%2F163%2F09255616304955_5421043_3.jpg&rptImgs%24ctl08%24hidImgUrl=http%3A%2F%2Fimg2.bitauto.com%2Fautoalbum%2Ffiles%2F20170110%2F504%2F09255550403069_5421042_3.jpg&rptImgs%24ctl09%24hidImgUrl=http%3A%2F%2Fimg4.bitauto.com%2Fautoalbum%2Ffiles%2F20170110%2F781%2F09255478192056_5421041_3.jpg&rptImgs%24ctl10%24hidImgUrl=http%3A%2F%2Fimg2.bitauto.com%2Fautoalbum%2Ffiles%2F20170110%2F920%2F09255392091283_5421040_3.jpg&rptImgs%24ctl11%24hidImgUrl=http%3A%2F%2Fimg4.bitauto.com%2Fautoalbum%2Ffiles%2F20170110%2F277%2F09255327797761_5421039_3.jpg&rptImgs%24ctl12%24hidImgUrl=http%3A%2F%2Fimg2.bitauto.com%2Fautoalbum%2Ffiles%2F20170110%2F468%2F09255246880891_5421038_3.jpg&rptImgs%24ctl13%24hidImgUrl=http%3A%2F%2Fimg4.bitauto.com%2Fautoalbum%2Ffiles%2F20140703%2F855%2F14505585595369_3425639_3.jpg&rptImgs%24ctl14%24hidImgUrl=http%3A%2F%2Fimg2.bitauto.com%2Fautoalbum%2Ffiles%2F20140703%2F853%2F14505585390634_3425638_3.jpg&__ASYNCPOST=true&"
        }

        private void InitDll()
        {
            doc = yc.InforManagerNews(photoUrl + csid);

            if (ddlCarYear.Items.Count == 0)
            {
                var ddCarYear = doc.GetElementbyId("ddCarYear");
                var option = ddCarYear.SelectNodes(".//option");
                List<TextValue> yearList = new List<TextValue>();

                yearList.Add(new TextValue { Text = "请选择年款", Value = "0" });
                for (int i = 1; i < option.Count; i++)
                {
                    var value = option[i].GetAttributeValue("value", "");
                    yearList.Add(new TextValue { Text = value + "款", Value = value });
                }
                ddlCarYear.DataSource = yearList;
                ddlCarYear.DisplayMember = "Text";
                ddlCarYear.ValueMember = "Value";
            }

            var CarStyle = doc.GetElementbyId("ddlCarStyle");
            var styleoption = CarStyle.SelectNodes(".//option");
            if (styleoption.Count > 1)
            {
                List<TextValue> styleList = new List<TextValue>();
                styleList.Add(new TextValue { Text = "请选择车款", Value = "0" });
                for (int i = 1; i < styleoption.Count; i++)
                {
                    var value = styleoption[i].GetAttributeValue("value", "");
                    styleList.Add(new TextValue { Text = value + "款", Value = value });
                }
                ddlCarStyle.DataSource = styleList;
                ddlCarStyle.DisplayMember = "Text";
                ddlCarStyle.ValueMember = "Value";
            }

            var CarColors = doc.GetElementbyId("ddlCarColors");
            var colorsoption = CarColors.SelectNodes(".//option");
            if (colorsoption.Count > 1)
            {
                List<TextValue> colorsList = new List<TextValue>();
                colorsList.Add(new TextValue { Text = "请选择颜色", Value = "0" });
                for (int i = 1; i < colorsoption.Count; i++)
                {
                    var value = colorsoption[i].GetAttributeValue("value", "");
                    colorsList.Add(new TextValue { Text = value + "款", Value = value });
                }
                ddlCarColors.DataSource = colorsList;
                ddlCarColors.DisplayMember = "Text";
                ddlCarColors.ValueMember = "Value";
            }
        }

        private void InitImg()
        {
            var imgList = doc.GetElementbyId("imgList").SelectNodes(".//li/input[@type='hidden']");
            int xstep = 126;
            int ystep = 88;
            int xstart = 6;
            int ystart = 10;
            this.lbtn1.Controls.Clear();
            for (int i = 0; i < imgList.Count; i++)
            {
                var value = imgList[i].GetAttributeValue("value", "");

                int x = xstart;
                int y = ystart;

                if (i > 0)
                {
                    x = xstart + (xstep * (i % 5));
                    y = ystart + (ystep * (i / 5));
                }

                var img = new PictureBox();
                img.Location = new System.Drawing.Point(x, y);
                img.SizeMode = PictureBoxSizeMode.StretchImage;//
                img.Name = string.Format("rptImgs%24ctl{0:00}%24hidImgUrl", i);
                img.Size = new System.Drawing.Size(120, 80);
                img.ImageLocation = value;
                img.Click += img_Click;

                currentPage.Controls.Add(img);
            }
        }

        private void InitPage()
        {
            var pagerLinks = doc.GetElementbyId("AspNetPager1").SelectNodes(".//a");
            var recordCount = pagerLinks[pagerLinks.Count - 1].GetAttributeValue("href", "").Replace("javascript:__doPostBack('AspNetPager1','", "").Replace("'", "");
            ucPager.RecordCount = Convert.ToInt32(recordCount);
            ucPager.InitPageInfo();
            ucPager.PageChanged += UcPager_PageChanged;
        }

        private void UcPager_PageChanged(object sender, EventArgs e)
        {
            //ucPager.PageIndex;
        }

        void img_Click(object sender, EventArgs e)
        {
            var img = (PictureBox)sender;
            if(PreviousSelected != null)
            {
                PreviousSelected.BorderStyle = BorderStyle.None;
            }
            img.BorderStyle = BorderStyle.FixedSingle;
            CurrentSelected = img;
            PreviousSelected = img;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }
}
