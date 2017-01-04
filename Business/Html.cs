using CsharpHttpHelper;
using CsharpHttpHelper.Enum;
using System.Net;
using System.Text;
using System.Drawing;
using System.IO;

namespace Business
{
    public class Html
    {        
        string cookie = "";

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

        public string Get(string url)
        {
            var item = new HttpItem()
            {
                URL = url,
                Method = "get",
                ContentType = "text/html",
                ResultCookieType = ResultCookieType.CookieCollection
            };
            if(!string.IsNullOrWhiteSpace(cookie))
            {
                item.Cookie = cookie;
            }
            HttpHelper http = new HttpHelper();
            HttpResult result = http.GetHtml(item);
            cookie += HttpHelper.GetSmallCookie(result.Cookie);
            return result.Html;
        }

        public string Post(HttpItem item)
        {
            item.Cookie = cookie;
            HttpHelper http = new HttpHelper();
            HttpResult result = http.GetHtml(item);
            cookie += HttpHelper.GetSmallCookie(result.Cookie);
            cookie = HttpHelper.GetSmallCookie(cookie);
            return result.Html;
        }
    }
}
