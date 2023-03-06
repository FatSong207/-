using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Commons.Helpers
{
    /// <summary>
    /// Cookie操作類
    /// </summary>
    public static class CookiesHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static HttpContext HttpHelper => HttpContextHelper.HttpContext;
        /// <summary>
        /// Cookie名稱
        /// </summary>
        public static string CookieName { get; set; } = "yuebon_";

        /// <summary>
        /// 設置Cookie鍵
        /// </summary>
        /// <param name="cookieName">鍵</param>
        /// <returns></returns>
        private static string CookieKey(string cookieName)
        {
            return CookieName+ cookieName;
        }
        /// <summary>
        /// 刪除Cookie
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cookieName">Coookie名稱</param>
        public static void DeleteCookie(HttpContext context, string cookieName)
        {
            string key = CookieKey(cookieName);
            context.Response.Cookies.Delete(key);
        }
        /// <summary>
        /// 寫/保存Cookie
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cookieName">Coookie名稱</param>
        /// <param name="value">Coookie值</param>
        /// <param name="months">有效月數</param>
        public static void WriteCookie(HttpContext context, string cookieName, string value, int months)
        {
            WriteCookie(context, cookieName, value, months, 0);
        }

        /// <summary>
        /// 寫/保存Cookie
        /// </summary>
        /// <param name="cookieName">Coookie名稱</param>
        /// <param name="value">Coookie值</param>
        /// <param name="months">有效月數</param>
        public static void WriteCookie(string cookieName, string value, int months)
        {
            WriteCookie(HttpHelper, cookieName, value, months, 0);
        }
        /// <summary>
        /// 寫/保存Cookie
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cookieName">Coookie名稱</param>
        /// <param name="value">Coookie值</param>
        /// <param name="months">有效月數</param>
        /// <param name="days">有效天數</param>
        public static void WriteCookie(HttpContext context, string cookieName, string value, int months, int days)
        {
            string key = CookieKey(cookieName);
            if (!context.Request.Cookies.ContainsKey(key))
            {
                DateTime expires = DateTime.Today.AddDays(30 * months + days);
                DateTimeOffset dateAndOffset = new DateTimeOffset(expires,
                                            TimeZoneInfo.Local.GetUtcOffset(expires));

                context.Response.Cookies.Append(key, value,
                    new CookieOptions
                    {
                        Expires = expires
                    });
            }
        }
        /// <summary>
        /// 獲取Cookie值
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cookieName">Coookie名稱</param>
        /// <returns></returns>
        public static string ReadCookie(HttpContext context, string cookieName)
        {
            string key = CookieKey(cookieName);

            try
            {
                return System.Net.WebUtility.UrlDecode(context.Request.Cookies[key]);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 獲取Cookie值
        /// </summary>
        /// <param name="cookieName">Coookie名稱</param>
        /// <returns></returns>
        public static string ReadCookie(string cookieName)
        {
            string key = CookieKey(cookieName);

            try
            {
                return System.Net.WebUtility.UrlDecode(HttpHelper.Request.Cookies[key]);
            }
            catch
            {
                return "";
            }
        }
    }
}
