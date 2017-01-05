using CsharpHttpHelper;
using CsharpHttpHelper.Enum;
using System.Drawing;
using System.IO;
using HtmlAgilityPack;

namespace Aide
{
    public class Html
    {        
        string cookie = "";
        HtmlDocument doc = new HtmlDocument();

        public Image GetImage(string url)
        {
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = url,
                Method = "get",
                ResultType = ResultType.Byte,
                Cookie = this.cookie
            };
            var result = http.GetHtml(item);
            cookie += HttpHelper.GetSmallCookie(result.Cookie);
            MemoryStream ms = new MemoryStream(result.ResultByte);
            return Bitmap.FromStream(ms, true);            
        }

        public HtmlDocument Get(string url)
        {
            var item = new HttpItem()
            {
                URL = url,
                Method = "get",
                ContentType = "text/html"
            };
            if(!string.IsNullOrWhiteSpace(cookie))
            {
                item.Cookie = cookie;
            }
            HttpHelper http = new HttpHelper();
            HttpResult result = http.GetHtml(item);
            cookie += HttpHelper.GetSmallCookie(result.Cookie);
            doc.LoadHtml(result.Html);
            return doc;
        }

        public HtmlDocument Post(HttpItem item)
        {
            item.Cookie = cookie;
            HttpHelper http = new HttpHelper();
            HttpResult result = http.GetHtml(item);
            cookie += HttpHelper.GetSmallCookie(result.Cookie);
            cookie = HttpHelper.GetSmallCookie(cookie);
            doc.LoadHtml(result.Html);
            return doc;
        }
    }
}
