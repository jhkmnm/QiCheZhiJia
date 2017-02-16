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
        TabPage PreviousPage;
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
            currentPage = PreviousPage = lbtn1;
            doc = yc.InforManagerNews(photoUrl + csid);
            InitDll(0);
            InitImg();
            InitPage();
        }

        /// <summary>
        /// 初始化下拉列表
        /// </summary>
        /// <param name="selectedIndex">当前选择的是哪一个下拉框1, 2, 3</param>
        private void InitDll(int selectedIndex)
        {
            var viewstate = doc.GetElementbyId("__VIEWSTATE");
            if (viewstate != null)
                str_viewstate = viewstate.GetAttributeValue("value", "");

            var viewstategenerator = doc.GetElementbyId("__VIEWSTATEGENERATOR");
            if (viewstategenerator != null)
                str_viewstategenerator = viewstategenerator.GetAttributeValue("value", "");

            var eventvalidation = doc.GetElementbyId("__EVENTVALIDATION");
            if (eventvalidation != null)
                str_eventvalidation = HttpHelper.URLEncode(eventvalidation.GetAttributeValue("value", ""));

            if (ddCarYear.Items.Count == 0)
            {
                ddCarYear.SelectedIndexChanged -= ddl_SelectedIndexChanged;
                var CarYear = doc.GetElementbyId("ddCarYear");
                var option = CarYear.SelectNodes(".//option");
                List<TextValue> yearList = new List<TextValue>();

                yearList.Add(new TextValue { Text = "请选择年款", Value = "0" });
                for (int i = 1; i < option.Count; i++)
                {
                    var value = option[i].GetAttributeValue("value", "");
                    yearList.Add(new TextValue { Text = value + "款", Value = value });
                }
                this.ddCarYear.DataSource = yearList;
                this.ddCarYear.DisplayMember = "Text";
                this.ddCarYear.ValueMember = "Value";
                ddCarYear.SelectedIndexChanged += ddl_SelectedIndexChanged;
            }

            if (selectedIndex < 2)
            {
                ddlCarStyle.SelectedIndexChanged -= ddl_SelectedIndexChanged;
                var CarStyle = doc.GetElementbyId("ddlCarStyle");
                var styleoption = CarStyle.SelectNodes(".//option");
                List<TextValue> styleList = new List<TextValue>();
                styleList.Add(new TextValue { Text = "请选择车款", Value = "0" });
                if (styleoption.Count > 1)
                {                    
                    for (int i = 1; i < styleoption.Count; i++)
                    {
                        var value = styleoption[i].GetAttributeValue("value", "");
                        styleList.Add(new TextValue { Text = value + "款", Value = value });
                    }
                }
                ddlCarStyle.DataSource = styleList;
                ddlCarStyle.DisplayMember = "Text";
                ddlCarStyle.ValueMember = "Value";
                ddlCarStyle.SelectedIndexChanged += ddl_SelectedIndexChanged;
            }

            if (selectedIndex < 3)
            {
                ddlCarColors.SelectedIndexChanged -= ddl_SelectedIndexChanged;
                var CarColors = doc.GetElementbyId("ddlCarColors");
                var colorsoption = CarColors.SelectNodes(".//option");
                List<TextValue> colorsList = new List<TextValue>();
                colorsList.Add(new TextValue { Text = "请选择颜色", Value = "0" });
                if (colorsoption.Count > 1)
                {                    
                    for (int i = 1; i < colorsoption.Count; i++)
                    {
                        var value = colorsoption[i].GetAttributeValue("value", "");
                        colorsList.Add(new TextValue { Text = value + "款", Value = value });
                    }
                }
                ddlCarColors.DataSource = colorsList;
                ddlCarColors.DisplayMember = "Text";
                ddlCarColors.ValueMember = "Value";
                ddlCarColors.SelectedIndexChanged += ddl_SelectedIndexChanged;
            }
        }

        private void InitImg()
        {
            currentPage.Controls.Clear();
            if (doc.DocumentNode.OuterHtml.Contains("此分类下没有图片"))
            {
                var label = new Label();
                label.AutoSize = true;
                label.Location = new System.Drawing.Point(292, 121);
                label.Name = "label1";
                label.Size = new System.Drawing.Size(41, 12);
                label.TabIndex = 0;
                label.Text = "此分类下没有图片";
                currentPage.Controls.Add(label);
            }
            else
            {
                var imgList = doc.GetElementbyId("imgList").SelectNodes(".//li/input[@type='hidden']");
                int xstep = 126;
                int ystep = 88;
                int xstart = 6;
                int ystart = 10;                
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
                    img.Name = string.Format("{0}_img{1}", currentPage.Name, i);
                    img.Tag = string.Format("rptImgs%24ctl{0:00}%24hidImgUrl", i);
                    img.Size = new System.Drawing.Size(120, 80);
                    img.ImageLocation = value;
                    img.Click += img_Click;

                    currentPage.Controls.Add(img);
                }
            }
        }

        private void InitPage()
        {
            int count = 0;
            if (!doc.DocumentNode.OuterHtml.Contains("此分类下没有图片"))
            {
                var pager = doc.GetElementbyId("AspNetPager1");
                if(pager != null)
                {
                    var pagerLinks = pager.SelectNodes(".//a");
                    var recordCount = pagerLinks[pagerLinks.Count - 1].GetAttributeValue("href", "").Replace("javascript:__doPostBack('AspNetPager1','", "").Replace("'", "").Replace(")", "");
                    count = Convert.ToInt32(recordCount) * 15;
                }                
            }
            ucPager.RecordCount = count;
            ucPager.PageIndex = 1;
            ucPager.InitPageInfo();
            ucPager.PageChanged -= UcPager_PageChanged;
            ucPager.PageChanged += UcPager_PageChanged;
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

        /// <summary>
        /// 初始化Post数据
        /// </summary>
        /// <param name="ispager">是否是分页</param>
        private string InitPostData(string eventTarget, string eventArgument)
        {
            StringBuilder sb = new StringBuilder(5000);
            sb.AppendFormat("ScriptManager1=UpdatePanel3%7C{0}&", eventTarget);// ispager ? "AspNetPager1" : currentPage.Name);
            sb.AppendFormat("ddCarYear={0}&", ddCarYear.SelectedValue);
            sb.AppendFormat("ddlCarStyle={0}&", ddlCarStyle.SelectedValue);
            sb.AppendFormat("ddlCarColors={0}&", ddlCarColors.SelectedValue);
            TabPage page = eventTarget.Contains("lbtn") ? currentPage : PreviousPage;
            foreach (Control con in page.Controls)
            {
                var img = con as PictureBox;
                if (img != null)
                {
                    sb.AppendFormat("{0}={1}&", img.Tag.ToString(), HttpHelper.URLEncode(img.ImageLocation));
                }
            }
            if (!string.IsNullOrWhiteSpace(eventArgument))
            {
                sb.AppendFormat("AspNetPager1_input={0}&", ucPager.PreviousPage);
            }                
            sb.AppendFormat("__EVENTTARGET={0}&", eventTarget);  //分页还是更换标签            
            sb.AppendFormat("__EVENTARGUMENT={0}&", eventArgument);//  页码            
            sb.Append("__LASTFOCUS=&");
            sb.AppendFormat("__VIEWSTATE={0}&", HttpHelper.URLEncode(str_viewstate));
            sb.AppendFormat("__VIEWSTATEGENERATOR={0}&", str_viewstategenerator);
            sb.AppendFormat("__EVENTVALIDATION={0}&", str_eventvalidation);            
            sb.Append("__ASYNCPOST=true&");
            return sb.ToString();
        }

        private void UcPager_PageChanged(object sender, EventArgs e)
        {
            var postdata = InitPostData("AspNetPager1", ucPager.PageIndex.ToString());
            doc = yc.Post_CheYiTong(photoUrl + csid, postdata);
            InitImg();
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

        private void ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ddl = (ComboBox)sender;
            var index = 1;
            if (ddl.Name == ddlCarStyle.Name)
                index = 2;
            else if (ddl.Name == ddlCarColors.Name)
                index = 3;

            var postdata = InitPostData(ddl.Name, "");
            doc = yc.Post_CheYiTong(photoUrl + csid, postdata);
            InitDll(index);
            InitImg();
            InitPage();
        }

        private void tbcImg_SelectedIndexChanged(object sender, EventArgs e)
        {
            PreviousPage = currentPage;
            currentPage = tbcImg.SelectedTab;
            var postdata = InitPostData(currentPage.Name, "");
            doc = yc.Post_CheYiTong(photoUrl + csid, postdata);
            InitImg();
            InitPage();
        }
    }
}
