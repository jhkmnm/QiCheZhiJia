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
        }
    }
}
