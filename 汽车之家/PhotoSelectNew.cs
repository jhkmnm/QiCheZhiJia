using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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

        public string SelectedImgPath
        {
            get { return CurrentSelected.ImageLocation; }
        }

        public PhotoSelectNew(YiChe yc, string csid)
        {
            InitializeComponent();
            this.yc = yc;
            this.csid = csid;            
        }

        private void InitForm()
        {
            doc = yc.InforManagerNews(photoUrl + csid);

            #region
            var carYear = doc.GetElementbyId("ddCarYear");
            List<TextValue> yearList = new List<TextValue>();
            //<select name="ddCarYear" onchange="javascript:setTimeout(&#39;__doPostBack(\&#39;ddCarYear\&#39;,\&#39;\&#39;)&#39;, 0)" id="ddCarYear">
            //<option selected="selected" value="0">请选择年款</option>
            //<option value="2017">2017款</option>
            //<option value="2016">2016款</option>
            //<option value="2014">2014款</option>

            #endregion

            //http://www.cnblogs.com/huyong/p/4201381.html  分页

            #region 图片
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
                img.Name = "pic" + i;
                img.Size = new System.Drawing.Size(120, 80);
                img.ImageLocation = value;
                img.Click += img_Click;

                this.tabPage1.Controls.Add(img);
            }
            #endregion
        }

        void img_Click(object sender, EventArgs e)
        {
            var img = (PictureBox)sender;
            if(PreviousSelected != null)
            {
                PreviousSelected.BorderStyle = BorderStyle.None;
            }

            CurrentSelected = img;
            PreviousSelected = img;
        }
    }
}
