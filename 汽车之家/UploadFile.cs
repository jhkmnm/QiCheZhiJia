using System;
using System.Text;
using System.Windows.Forms;
using HAP = HtmlAgilityPack;
using CsharpHttpHelper;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
using Newtonsoft.Json;

namespace Aide
{
    public partial class UploadFile : Form
    {
        YiChe yc;
        string ImageUpload;
        string SelectExsitPic;
        HAP.HtmlDocument doc;
        ImgControl1 PreviousSelected;
        ImgControl1 CurrentSelected;
        string str_viewstate = "";
        string str_viewstategenerator = "";
        string str_eventvalidation = "";
        string url = "http://das.app.easypass.cn/FileManage/FileUpload/";
        string uploadcheck = "http://das.app.easypass.cn/FileManage/UploadAccessCheck.aspx?args=";
        string ajaxupload = "http://das.app.easypass.cn/FileManage/FileUpload/AjaxFileUpLoad.aspx?args=";
        string currentargs = "";
        string savedata = "http://das.app.easypass.cn/FileManage/FileUpload/SaveDataToDataBase.ashx";
        string imagecanvas = "http://das.app.easypass.cn/FileManage/FileUpload/ImageCanvas.aspx?args=";

        public UploadFile(YiChe yc, string ImageUpload)
        {
            InitializeComponent();
            this.yc = yc;
            this.ImageUpload = ImageUpload;

            doc = yc.InforManagerNews(ImageUpload);

            var currentargsjs = doc.DocumentNode.SelectNodes("//script")[6].OuterHtml.Trim().Split('\r');
            foreach(string str in currentargsjs)
            {
                if(str.Contains("CurrentArgs"))
                {
                    currentargs = str.Replace("\n        var CurrentArgs = '", "").Replace("'", "");
                    break;
                }
            }

            var piclink = doc.DocumentNode.SelectSingleNode("//a[contains(@href, 'SelectExsitPic')]");
            if (piclink != null)
            {
                SelectExsitPic = url + piclink.GetAttributeValue("href", "");
            }
        }

        private void InitImg(int type)
        {
            if (type == 0)
            {
                doc = yc.InforManagerNews(SelectExsitPic);
            }

            var viewstate = doc.GetElementbyId("__VIEWSTATE");
            if (viewstate != null)
                str_viewstate = HttpHelper.URLEncode(viewstate.GetAttributeValue("value", ""));

            var viewstategenerator = doc.GetElementbyId("__VIEWSTATEGENERATOR");
            if(viewstategenerator != null)
                str_viewstategenerator = viewstategenerator.GetAttributeValue("value", "");

            var eventvalidation = doc.GetElementbyId("__EVENTVALIDATION");
            if (eventvalidation != null)
                str_eventvalidation = HttpHelper.URLEncode(eventvalidation.GetAttributeValue("value", ""));

            var imgs = doc.GetElementbyId("alert_imgs_mng");
            if(imgs != null)
            {
                panel1.Controls.Clear();
                var imgList = imgs.SelectNodes(".//li");
                int xstep = 143;
                int ystep = 116;
                int xstart = 3;
                int ystart = 3;
                for (int i = 0; i < imgList.Count; i++)
                {
                    var thumb = imgList[i].SelectSingleNode(".//img").GetAttributeValue("src", "");
                    var artwork = imgList[i].SelectSingleNode(".//div/a[contains(@href, 'http')]").GetAttributeValue("href", "");
                    var id = imgList[i].SelectSingleNode(".//div/a[contains(@href, 'java')]").GetAttributeValue("href", "").Replace("javascript:deleteWindow(", "").Replace(");", "");
                    var name = imgList[i].SelectSingleNode(".//input[@name='hiddenclid']").GetAttributeValue("value", "");
                    int x = xstart;
                    int y = ystart;

                    if (i > 0)
                    {
                        x = xstart + (xstep * (i % 4));
                        y = ystart + (ystep * (i / 4));
                    }

                    var img = new ImgControl1();
                    img.Location = new System.Drawing.Point(x, y);
                    img.Name = string.Format("img{0}", i);
                    img.Tag = name;
                    img.Size = new System.Drawing.Size(122, 98);
                    img.pbxImg.ImageLocation = thumb;
                    img.lblFocusImg.Tag = artwork;
                    img.lblDel.Tag = id;
                    img.Click += Img_Click;
                    img.pbxImg.Click += Img_Click;
                    img.lblDel.Click += LblDel_Click;
                    panel1.Controls.Add(img);
                }

                if (type == 0)
                {
                    int count = 0;
                    if (!doc.DocumentNode.OuterHtml.Contains("此分类下没有图片"))
                    {
                        var pager = doc.GetElementbyId("pager1");
                        if (pager != null)
                        {
                            var pagerLinks = pager.SelectNodes(".//a");
                            var recordCount = pagerLinks[pagerLinks.Count - 1].GetAttributeValue("href", "").Replace("javascript:__doPostBack('pager1','", "").Replace("'", "").Replace(")", "");
                            count = Convert.ToInt32(recordCount) * 8;
                        }
                    }
                    ucPager.RecordCount = count;
                    ucPager.PageIndex = 1;
                    ucPager.InitPageInfo();
                    ucPager.PageChanged -= UcPager_PageChanged;
                    ucPager.PageChanged += UcPager_PageChanged;
                }
            }
        }

        private void LblDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除这张图片吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var llb = (LinkLabel)sender;
                var postdata = InitPostData(Convert.ToInt32(llb.Tag.ToString()));
                doc = yc.Post_CheYiTong(SelectExsitPic, postdata);
                InitImg(0);
            }            
        }

        private void UcPager_PageChanged(object sender, EventArgs e)
        {
            var postdata = InitPostData(1);
            doc = yc.Post_CheYiTong(SelectExsitPic, postdata);
            InitImg(1);
        }

        private void Img_Click(object sender, EventArgs e)
        {
            var img = sender as ImgControl1;
            if (img == null)
                img = (sender as PictureBox).Parent as ImgControl1;
            
            if (PreviousSelected != null)
            {
                PreviousSelected.pbxImg.BorderStyle = BorderStyle.None;
            }
            img.pbxImg.BorderStyle = BorderStyle.FixedSingle;
            CurrentSelected = img;
            PreviousSelected = img;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">1翻页>1删除</param>
        /// <returns></returns>
        private string InitPostData(int type)
        {
            StringBuilder sb = new StringBuilder(5000);
            sb.AppendFormat("__VIEWSTATE={0}&", str_viewstate);
            sb.AppendFormat("__VIEWSTATEGENERATOR={0}&", str_viewstategenerator);
            if(type == 1)
            {
                sb.Append("__EVENTTARGET=pager1&");
                sb.AppendFormat("__EVENTARGUMENT={0}&", ucPager.PageIndex);
                sb.Append("hideDelete=&");
            }
            else if(type > 1)
            {
                sb.Append("__EVENTTARGET=&__EVENTARGUMENT=&");
                sb.AppendFormat("btnDel=&hideDelete={0}&", type);
            }
            sb.AppendFormat("__EVENTVALIDATION={0}&", str_eventvalidation);
            foreach (Control con in panel1.Controls)
            {
                var img = con as ImgControl1;
                if (img != null)
                {
                    sb.AppendFormat("hiddenurl={0}&hiddenclid={1}&", HttpHelper.URLEncode(img.lblFocusImg.Tag.ToString()), img.Tag.ToString());
                }
            }
            sb.Append("hideFileUrl=&hideFileClID=");
            return sb.ToString();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedTab == tabPage2)
            {
                if(panel1.Controls.Count == 0)
                {
                    InitImg(0);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (CurrentSelected == null)
            {
                MessageBox.Show("请选择图片");
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            Regex reg = new Regex("(.jpg|.jpeg|.gif|.png|.bmp)$");
            int size = 2 * 1024 * 1024;
            using (OpenFileDialog file = new OpenFileDialog())
            {
                file.Filter = "JPG|*.jpg|JPGE|*.jpeg|GIF|*.gif|PNG|*.png|BMP|*.bmp";
                file.CheckFileExists = true;
                if (file.ShowDialog() == DialogResult.OK)
                {
                    var match = reg.Match(file.SafeFileName);
                    if (!match.Success)
                    {
                        MessageBox.Show("请选择图片文件");
                    }
                    var f = new FileInfo(file.FileName);
                    if (f.Length > size)
                    {
                        MessageBox.Show("文件大于必须小于2M");
                    }
                    lblImgPath.Text = file.FileName;
                    pbxImg.ImageLocation = file.FileName;
                    pbxImg.Tag = file.SafeFileName;
                }
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            //1. 上传检查， 2. 上传， 3. 裁剪， 4. 上传
            doc = yc.Post_CheYiTong(uploadcheck + currentargs, "1"); //1
            if(doc.DocumentNode.OuterHtml == "True")
            {
                PostImage();    //2
                var result = JsonConvert.DeserializeObject<UploadResult>(doc.DocumentNode.OuterHtml);
                var post = string.Format("&bfile={0}&vid={1}&gfile={2}&length={3}&uclass={4}&aid={5}", result.thnmbFileName, result.vid, result.imageurl, result.length, result.uclass, result.aid);
                doc = yc.Post_CheYiTong(savedata, post);
                doc = yc.InforManagerNews(imagecanvas + currentargs + "&imageurl=" + result.imageurl);
                var viewstate = doc.GetElementbyId("__VIEWSTATE");
                post = "__VIEWSTATE=" + HttpHelper.URLEncode(viewstate.GetAttributeValue("value", ""));
                var viewstaterator = doc.GetElementbyId("__VIEWSTATEGENERATOR");
                post += "__VIEWSTATEGENERATOR=" + viewstaterator.GetAttributeValue("value", "");
                var eventval = doc.GetElementbyId("__EVENTVALIDATION");
                post += "__EVENTVALIDATION=" + HttpHelper.URLEncode(eventval.GetAttributeValue("value", ""));
                post += "txt_width=360&txt_height=480&txt_top=140&txt_left=30&txt_DropWidth=300&txt_DropHeight=200&txt_Zoom=0.375&btnCanvas=%E7%A1%AE%E5%AE%9A";
                doc = yc.Post_CheYiTong(HttpHelper.URLEncode(imagecanvas + currentargs + "&imageurl=" + result.imageurl), post);
            }
        }

        private void PostImage()
        {
            #region 变量
            byte[] UploadBuffers = null;
            string BoundStr = "----WebKitFormBoundaryMxO2oJmYsONSlZ0g";//根据抓包生成
            StringBuilder UploadBuf = new StringBuilder();
            #endregion

            #region 头部数据
            UploadBuf.AppendFormat("{0}{1}", BoundStr, Environment.NewLine);
            UploadBuf.AppendFormat(@"Content-Disposition: form-data; name=""fuPhoto"";filename=""{0}""{1}", pbxImg.Tag.ToString(), Environment.NewLine);
            UploadBuf.AppendFormat("Content-Type: image/jpeg{0}", Environment.NewLine);
            byte[] HeadBytes = Encoding.ASCII.GetBytes(UploadBuf.ToString());
            #endregion

            #region 图片数据
            byte[] PicBytes = ImageToBytes(pbxImg.Image);
            #endregion

            #region 尾部数据
            UploadBuf.Clear();
            UploadBuf.Append("\r\n" + BoundStr + "--\r\n");
            byte[] TailBytes = Encoding.ASCII.GetBytes(UploadBuf.ToString());
            #endregion

            #region 数组拼接
            UploadBuffers = ComposeArrays(HeadBytes, PicBytes);
            UploadBuffers = ComposeArrays(UploadBuffers, TailBytes);
            #endregion

            #region 上传
            doc = yc.PostImg_CheYiTong(ajaxupload + currentargs, BoundStr, UploadBuffers);
            #endregion
        }

        #region 数组组合
        public static byte[] ComposeArrays(byte[] Array1, byte[] Array2)
        {
            byte[] Temp = new byte[Array1.Length + Array2.Length];
            Array1.CopyTo(Temp, 0);
            Array2.CopyTo(Temp, Array1.Length);
            return Temp;
        }
        #endregion

        public byte[] ImageToBytes(Image image)
        {
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                if (format.Equals(ImageFormat.Jpeg))
                {
                    image.Save(ms, ImageFormat.Jpeg);
                }
                else if (format.Equals(ImageFormat.Png))
                {
                    image.Save(ms, ImageFormat.Png);
                }
                else if (format.Equals(ImageFormat.Bmp))
                {
                    image.Save(ms, ImageFormat.Bmp);
                }
                else if (format.Equals(ImageFormat.Gif))
                {
                    image.Save(ms, ImageFormat.Gif);
                }
                byte[] buffer = new byte[ms.Length];
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }
    }

    public class UploadResult
    {
        public string error { get; set; }
        public string msg { get; set; }
        public string Redirect { get; set; }
        public string imageurl { get; set; }
        public string FileName { get; set; }
        public string thnmbFileName { get; set; }
        public string vid { get; set; }
        public string aid { get; set; }
        public string length { get; set; }
        public string uclass { get; set; }
    }
}
