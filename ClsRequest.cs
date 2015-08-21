using System;
using System.IO;
using System.Web;


namespace CommonCls
{
    /// <summary>
    /// Request 操作类（获取Post Get请求、浏览器版本、操作系统版本 等信息）
    /// </summary>
    public class ClsRequest
    {
        /// <summary>
        /// 判断当前页面是否接收到了Post请求
        /// </summary>
        /// <returns>是否接收到了Post请求</returns>
        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }

        /// <summary>
        /// 判断当前页面是否接收到了Get请求
        /// </summary>
        /// <returns>是否接收到了Get请求</returns>
        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("GET");
        }

        /// <summary>
        /// 返回指定的服务器变量信息
        /// </summary>
        /// <param name="strName">服务器变量名</param>
        /// <returns>服务器变量信息</returns>
        public static string GetServerString(string strName)
        {
            if (HttpContext.Current.Request.ServerVariables[strName] == null)
                return "";

            return HttpContext.Current.Request.ServerVariables[strName].ToString();
        }

        /// <summary>
        /// 返回上一个页面的地址
        /// </summary>
        /// <returns>上一个页面的地址</returns>
        public static string GetUrlReferrer()
        {
            string retVal = null;

            try
            {
                if (HttpContext.Current.Request.UrlReferrer != null)
                {
                    retVal = HttpContext.Current.Request.UrlReferrer.ToString();
                }
            }
            catch
            {
                throw;
            }

            if (retVal == null)
                return "";

            return retVal;
        }

        /// <summary>
        /// 得到当前完整主机头
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentFullHost()
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            if (!request.Url.IsDefaultPort)
                return string.Format("{0}:{1}", request.Url.Host, request.Url.Port.ToString());

            return request.Url.Host;
        }

        /// <summary>
        /// 得到主机头
        /// </summary>
        /// <returns></returns>
        public static string GetHost()
        {
            return HttpContext.Current.Request.Url.Host;
        }


        /// <summary>
        /// 获取当前请求的原始 URL(URL 中域信息之后的部分,包括查询字符串(如果存在))
        /// </summary>
        /// <returns>原始 URL</returns>
        public static string GetRawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
        }

        /// <summary>
        /// 判断当前访问是否来自浏览器软件
        /// </summary>
        /// <returns>当前访问是否来自浏览器软件</returns>
        public static bool IsBrowserGet()
        {
            string[] BrowserName = { "ie", "opera", "netscape", "mozilla", "konqueror", "firefox" };
            string curBrowser = HttpContext.Current.Request.Browser.Type.ToLower();
            for (int i = 0; i < BrowserName.Length; i++)
            {
                if (curBrowser.IndexOf(BrowserName[i]) >= 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 判断是否来自搜索引擎链接
        /// </summary>
        /// <returns>是否来自搜索引擎链接</returns>
        public static bool IsSearchEnginesGet()
        {
            if (HttpContext.Current.Request.UrlReferrer == null)
                return false;

            string[] SearchEngine = { "google", "yahoo", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom", "yisou", "iask", "soso", "gougou", "zhongsou" };
            string tmpReferrer = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
            for (int i = 0; i < SearchEngine.Length; i++)
            {
                if (tmpReferrer.IndexOf(SearchEngine[i]) >= 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 获得当前完整Url地址(不含参数)
        /// </summary>
        /// <returns>当前完整Url地址(不含参数)</returns>
        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }
        /// <summary>
        /// 获得当前完整Url地址
        /// </summary>
        /// <returns>当前完整Url地址</returns>
        public static string GetUrl_PathAndQuery()
        {

            return HttpContext.Current.Request.Url.PathAndQuery;// HttpContext.Current.Request.Url.ToString();

        }

        /// <summary>
        /// 获得指定Url参数的值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName)
        {
            return GetQueryString(strName, true);
        }

        /// <summary>
        /// 获得指定Url参数的值
        /// </summary> 
        /// <param name="strName">Url参数</param>
        /// <param name="sqlSafeCheck">是否删除SQL不安全字符</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName, bool flagSQLInjection)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
                return "";

            string tempReturn = HttpContext.Current.Request.QueryString[strName];
            if (flagSQLInjection)
                return CommonCls.SqlUtil.StripSQLInjection(CommonCls.StringUtil_Html.HtmlEncode(tempReturn));

            return tempReturn;
        }

        /// <summary>
        /// 获得当前页面的名称
        /// </summary>
        /// <returns>当前页面的名称</returns>
        public static string GetPageName()
        {
            string[] urlArr = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
            return urlArr[urlArr.Length - 1].ToLower();
        }

        /// <summary>
        /// 当前页面的站点名+页面名
        /// </summary>
        /// <returns>当前页面的站点名+页面名</returns>
        public static string GetAbsolutePath()
        {
            return HttpContext.Current.Request.Url.AbsolutePath.ToLower();
        }

        /// <summary>
        /// 返回表单或Url参数的总个数
        /// </summary>
        /// <returns></returns>
        public static int GetParamCount()
        {
            return HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.QueryString.Count;
        }


        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <returns>表单参数的值</returns>
        public static string GetFormString(string strName)
        {
            return GetFormString(strName, true);
        }

        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="sqlSafeCheck">是否删除SQL不安全字符</param>
        /// <returns>表单参数的值</returns>
        public static string GetFormString(string strName, bool flagSQLInjection)
        {
            if (HttpContext.Current.Request.Form[strName] == null)
                return "";
            string tempReturn = HttpContext.Current.Request.Form[strName];
            if (flagSQLInjection)
                return CommonCls.SqlUtil.StripSQLInjection(CommonCls.StringUtil_Html.HtmlEncode(tempReturn));

            return tempReturn;
        }

        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">参数</param>
        /// <returns>Url或表单参数的值</returns>
        public static string GetString(string strName)
        {
            return GetString(strName, false);
        }

        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>Url或表单参数的值</returns>
        public static string GetString(string strName, bool sqlSafeCheck)
        {
            if ("".Equals(GetQueryString(strName)))
                return GetFormString(strName, sqlSafeCheck);
            else
                return GetQueryString(strName, sqlSafeCheck);
        }

        /// <summary>
        /// 获得指定Url参数的int类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的int类型值</returns>
        public static int GetQueryInt(string strName)
        {
            return TypeConverter.StrToInt(HttpContext.Current.Request.QueryString[strName], 0);
        }


        /// <summary>
        /// 获得指定Url参数的int类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的int类型值</returns>
        public static int GetQueryInt(string strName, int defValue)
        {
            return TypeConverter.StrToInt(HttpContext.Current.Request.QueryString[strName], defValue);
        }


        /// <summary>
        /// 获得指定表单参数的int类型值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>表单参数的int类型值</returns>
        public static int GetFormInt(string strName, int defValue)
        {
            return TypeConverter.StrToInt(HttpContext.Current.Request.Form[strName], defValue);
        }

        /// <summary>
        /// 获得指定Url或表单参数的int类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的int类型值</returns>
        public static int GetInt(string strName, int defValue)
        {
            if (GetQueryInt(strName, defValue) == defValue)
                return GetFormInt(strName, defValue);
            else
                return GetQueryInt(strName, defValue);
        }

        /// <summary>
        /// 获得指定Url参数的float类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的int类型值</returns>
        public static float GetQueryFloat(string strName, float defValue)
        {
            return TypeConverter.StrToFloat(HttpContext.Current.Request.QueryString[strName], defValue);
        }


        /// <summary>
        /// 获得指定表单参数的float类型值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>表单参数的float类型值</returns>
        public static float GetFormFloat(string strName, float defValue)
        {
            return TypeConverter.StrToFloat(HttpContext.Current.Request.Form[strName], defValue);
        }

        /// <summary>
        /// 获得指定Url或表单参数的float类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的int类型值</returns>
        public static float GetFloat(string strName, float defValue)
        {
            if (GetQueryFloat(strName, defValue) == defValue)
                return GetFormFloat(strName, defValue);
            else
                return GetQueryFloat(strName, defValue);
        }

        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.UserHostAddress;

            if (string.IsNullOrEmpty(result) || !Validator.IsIP(result))
                return "127.0.0.1";

            return result;
        }


        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string UrlEncode(string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string UrlDecode(string str)
        {
            return HttpUtility.UrlDecode(str);
        }

        /// <summary>
        /// 获取站点根目录URL
        /// </summary>
        /// <returns></returns>
        public static string GetRootUrl(string forumPath)
        {
            int port = HttpContext.Current.Request.Url.Port;
            return string.Format("{0}://{1}{2}{3}",
                                 HttpContext.Current.Request.Url.Scheme,
                                 HttpContext.Current.Request.Url.Host.ToString(),
                                 (port == 80 || port == 0) ? "" : ":" + port,
                                 forumPath);
        }


        #region Private Methods

        /// <summary>
        /// 获得浏览器名称（包括版本号）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetBrowserName(HttpRequest request)
        {
            HttpBrowserCapabilities browser = request.Browser;

            if (browser == null)
                return "Unknown";

            string text;
            if (browser.Browser == "IE")
            {
                if (browser.Beta)
                    text = string.Concat("Internet Explorer ", browser.Version, "(beta)");
                else
                    text = "Internet Explorer " + browser.Version;
            }
            else
            {
                string userAgent = request.UserAgent;

                if (userAgent != null && userAgent.IndexOf("Chrome") != -1)
                    text = "Chrome";
                else if (userAgent != null && userAgent.IndexOf("Safari") != -1)
                    text = "Safari";
                else if (browser.Beta)
                    text = string.Concat(browser.Browser, " ", browser.Version, "(beta)");
                else
                    text = string.Concat(browser.Browser, " ", browser.Version);
            }

            return text;

        }

        /// <summary>
        /// 获得操作系统信息
        /// </summary>
        /// <returns></returns>
        public static string GetClientOS()
        {
            string os = string.Empty;
            string agent = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
            if (agent == null)
                return "Other";

            if (agent.IndexOf("Win") > -1)
                os = "Windows";
            else if (agent.IndexOf("Mac") > -1)
                os = "Mac";
            else if (agent.IndexOf("Linux") > -1)
                os = "Linux";
            else if (agent.IndexOf("FreeBSD") > -1)
                os = "FreeBSD";
            else if (agent.IndexOf("SunOS") > -1)
                os = "SunOS";
            else if (agent.IndexOf("OS/2") > -1)
                os = "OS/2";
            else if (agent.IndexOf("AIX") > -1)
                os = "AIX";
            else if (System.Text.RegularExpressions.Regex.IsMatch(agent, @"(Bot|Crawl|Spider)"))
                os = "Spiders";
            else
                os = "Other";
            return os;
        }
        /// <summary>
        /// 获取MAC地址
        /// </summary>
        /// <param name="clientip"></param>
        /// <returns></returns>
        public static string GetMacAddress(string clientip)
        {
            string mac = "";
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "nbtstat";
            process.StartInfo.Arguments = "-a " + clientip;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            int length = output.IndexOf("MAC Address =");
            if (length > 0)
            {
                mac = output.Substring(length + 14, 17);
            }
            return mac;
        }
        #endregion

        /// <summary>
        /// 判断当前客户端请求是否为IE
        /// </summary>
        /// <returns></returns>
        public static bool IsIE()
        {
            return HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].IndexOf("MSIE") >= 0;
        }





        public enum SpiderType
        {
            Other,

            Baidu,

            Google,

            Yahoo,

            Sogou,

            Youdao,

            Sohu,

            Bing,

            Qihoo,

            Soso,

            Alexa,

            Oracle,

            ASPSeek,

            Lexxe
        }

        /// <summary>
        /// 获得操作系统名称（及版本号）
        /// </summary>
        /// <param name="request"></param>
        /// <param name="isSpider"></param>
        /// <returns></returns>
        public static string GetPlatformName(HttpRequest request, out SpiderType spiderType)
        {
            spiderType = SpiderType.Other;
            string userAgent = request.UserAgent;

            if (string.IsNullOrEmpty(userAgent))
                return "Unknown";

            else if (userAgent.IndexOf("Windows NT 6.1") != -1)
                return "Windows 7";

            else if (userAgent.IndexOf("Windows NT 6") != -1)
                return "Windows Vista";

            else if (userAgent.IndexOf("Windows NT 5.1") != -1)
                return "Windows XP";

            else if (userAgent.IndexOf("Windows NT 5.2") != -1)
                return "Windows Server 2003";

            else if (userAgent.IndexOf("Windows NT 5") != -1)
                return "Windows 2000";

            else if (userAgent.IndexOf("iPhone") != -1)
                return "iPhone";

            else if (userAgent.IndexOf("(iPad;") != -1)
                return "iPad";

            else if (userAgent.IndexOf("Android") != -1)
                return "Android";

            else if (userAgent.IndexOf("9x") != -1)
                return "Windows ME";

            else if (userAgent.IndexOf("98") != -1)
                return "Windows 98";

            else if (userAgent.IndexOf("95") != -1)
                return "Windows 95";

            else if (userAgent.IndexOf("NT 4") != -1)
                return "Windows NT 4";

            spiderType = GetSpiderName(userAgent);
            if (spiderType != SpiderType.Other)
            {
                return spiderType.ToString();
            }

            if (request.Browser != null && !string.IsNullOrEmpty(request.Browser.Platform))
                return request.Browser.Platform.Replace("WinCE", "Windows CE");
            else
                return "Unknown";

        }

        /// <summary>
        /// 根据UserAgent获取蜘蛛名称，如果不是可识别的蜘蛛将返回null
        /// </summary>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public static SpiderType GetSpiderName(string userAgent)
        {
            //Baidu
            if (userAgent.IndexOf("baiduspider", StringComparison.OrdinalIgnoreCase) != -1 && userAgent.IndexOf("www.baidu.com/search/spider.htm") != -1)
                return SpiderType.Baidu;

            //Yahoo
            else if (userAgent.IndexOf("yahooseeker/", StringComparison.OrdinalIgnoreCase) != -1)
                return SpiderType.Yahoo;

            else if (userAgent.IndexOf("yahoo!", StringComparison.OrdinalIgnoreCase) != -1)
            {
                if (userAgent.IndexOf("help.yahoo.com/help/us/ysearch/slurp", StringComparison.OrdinalIgnoreCase) != -1)
                    return SpiderType.Yahoo;
                else if (userAgent.IndexOf("misc.yahoo.com.cn/help.html", StringComparison.OrdinalIgnoreCase) != -1)
                    return SpiderType.Yahoo;
                else if (userAgent.IndexOf("misc.yahoo.com.cn/help.html", StringComparison.OrdinalIgnoreCase) != -1)
                    return SpiderType.Yahoo;
            }

            //Google
            else if (userAgent.IndexOf("googlebot", StringComparison.OrdinalIgnoreCase) != -1)
                return SpiderType.Google;

            else if (string.Compare(userAgent, "mediapartners-google", true) == 0)
                return SpiderType.Google;

            else if (string.Compare(userAgent, "google image crawler", true) == 0)
                return SpiderType.Google;
            //Sougou
            else if (userAgent.IndexOf("sogou", StringComparison.OrdinalIgnoreCase) != -1 && userAgent.IndexOf("spider", StringComparison.OrdinalIgnoreCase) != -1)
                return SpiderType.Sogou;

            //Sohu
            else if (string.Compare(userAgent, "sohu-search", true) == 0)
                return SpiderType.Sohu;

            //Youdao
            else if (userAgent.IndexOf("youdaobot/", StringComparison.OrdinalIgnoreCase) != -1 && userAgent.IndexOf("www.youdao.com/help/webmaster/spider/", StringComparison.OrdinalIgnoreCase) != -1)
                return SpiderType.Youdao;

            else if (userAgent.IndexOf("yodaobot/", StringComparison.OrdinalIgnoreCase) != -1 && userAgent.IndexOf("www.yodao.com/help/webmaster/spider/", StringComparison.OrdinalIgnoreCase) != -1)
                return SpiderType.Youdao;

            //Windows Live Search
            else if (userAgent.IndexOf("msnbot", StringComparison.OrdinalIgnoreCase) != -1 && userAgent.IndexOf("search.msn.com/msnbot.htm", StringComparison.OrdinalIgnoreCase) != -1)
                return SpiderType.Bing;

            //Qihoo
            else if (userAgent.IndexOf("qihoobot", StringComparison.OrdinalIgnoreCase) != -1)
                return SpiderType.Qihoo;

            //Soso
            else if (userAgent.IndexOf("sosospider", StringComparison.OrdinalIgnoreCase) != -1)
                return SpiderType.Soso;

            //Alexa
            else if (userAgent.IndexOf("ia_archiver", StringComparison.OrdinalIgnoreCase) != -1)
                return SpiderType.Alexa;

            else if (userAgent.IndexOf("iaarchiver", StringComparison.OrdinalIgnoreCase) != -1)
                return SpiderType.Alexa;

            //ASPSeek
            else if (userAgent.IndexOf("aspseek/", StringComparison.OrdinalIgnoreCase) != -1)
                return SpiderType.ASPSeek;

            //Oracle
            else if (string.Compare(userAgent, "oracle ultra search", true) == 0)
                return SpiderType.Oracle;

            //Lexxe
            else if (userAgent.IndexOf("lexxebot/", StringComparison.OrdinalIgnoreCase) != -1)
                return SpiderType.Lexxe;

            return SpiderType.Other;
        }



    }



}
