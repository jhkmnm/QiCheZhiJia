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
        bool isLogin = true;

        public Image GetImage(string url)
        {            
            HttpItem item = new HttpItem()
            {
                URL = url,
                Method = "get",
                ResultType = ResultType.Byte
            };
            return GetImage(item);
        }

        public Image GetImage(HttpItem item)
        {
            HttpHelper http = new HttpHelper();
            var result = http.GetHtml(item);
            if(!string.IsNullOrWhiteSpace(result.Cookie) && isLogin)
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
            doc.LoadHtml(Get(item).Html);
            return doc;
        }

        public HttpResult Get(HttpItem item)
        {
            item.Cookie = cookie;
            HttpHelper http = new HttpHelper();
            HttpResult result = http.GetHtml(item);
            if(isLogin)
                cookie += HttpHelper.GetSmallCookie(result.Cookie);
            return result;
        }

        public HtmlDocument Post(HttpItem item)
        {
            item.Cookie = cookie;
            HttpHelper http = new HttpHelper();
            HttpResult result = http.GetHtml(item);
            if (isLogin)
            {
                cookie += HttpHelper.GetSmallCookie(result.Cookie);
                cookie = HttpHelper.GetSmallCookie(cookie);
            }
            doc.LoadHtml(result.Html);
            return doc;
        }
    }
}
