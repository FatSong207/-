using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Json;
using Yuebon.Commons.Models;
using Yuebon.Commons.Net;
using Yuebon.Commons.Options;

namespace Yuebon.Commons.Helpers
{
    /// <summary>
    /// 簽名驗證自定義類
    /// </summary>
    public class SignHelper
    {
        /// <summary>
        /// 全局過濾器驗證簽名
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static CommonResult CheckSign(HttpContext httpContext)
        {
            CommonResult result = new CommonResult();
            //從http請求的頭里面獲取參數
            var request = httpContext.Request;
            var appId = ""; //客戶端應用唯一標識
            string nonce = "";//隨機字符串
            var signature = ""; //參數簽名，去除空參數,按字母倒序排序進行Md5簽名 為了提高傳參過程中，防止參數被惡意修改，在請求接口的時候加上sign可以有效防止參數被篡改
            long timeStamp; //時間戳， 校驗5分鐘內有效
            try
            {
                appId = request.Headers["appId"].SingleOrDefault();
                nonce = request.Headers["nonce"].SingleOrDefault();
                timeStamp = Convert.ToInt64(request.Headers["timeStamp"].SingleOrDefault());
                signature = request.Headers["signature"].SingleOrDefault();
            }
            catch (Exception ex)
            {
                result.ErrCode = "40004";
                result.ErrMsg = "簽名參數異常:" + ex.Message;
                return result;
            }

            //appId是否為可用的
            AllowCacheApp allowCacheApp = VerifyAppId(appId);
            if (allowCacheApp == null)
            {
                result.ErrCode = "40004";
                result.ErrMsg = "AppId不被允許訪問:" + appId;
                return result;
            }

            //判斷timespan是否有效，請求是否超時
            DateTime tonow= timeStamp.UnixTimeToDateTime();
            var expires_minute = tonow.Minute - DateTime.Now.Minute;
            RemoteIpParser remoteIpParser = new RemoteIpParser();
            string strIp = remoteIpParser.GetClientIp(httpContext).MapToIPv4().ToString();
            Console.WriteLine($"IpFrom:{strIp}");
            Console.WriteLine($"tonow:{tonow}");
            Console.WriteLine($"expires_minute : { tonow}-{DateTime.Now} = {expires_minute}");
            if (expires_minute > 5 || expires_minute < -5)
            {
                Console.WriteLine($"tonow:{tonow}");
                Console.WriteLine($"expires_minute : { tonow}-{DateTime.Now} = {expires_minute}");
                result.ErrCode = "40004";
                result.ErrMsg = "接口請求超時";
                return result;
            }

            //根據請求類型拼接參數
            NameValueCollection form = HttpUtility.ParseQueryString(request.QueryString.ToString());
            var data = string.Empty;
            if (form.Count > 0)
            {
                data = GetQueryString(form);
            }
            else
            {
                //request.EnableBuffering();
                request.Body.Seek(0, SeekOrigin.Begin);
                Stream stream = request.Body;
                StreamReader streamReader = new StreamReader(stream);
                data = streamReader.ReadToEndAsync().Result;
                request.Body.Seek(0, SeekOrigin.Begin);
            }
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            object reqtimeStampCache = yuebonCacheHelper.Get("request_" + timeStamp + nonce);
            if (reqtimeStampCache != null)
            {
                result.ErrCode = "40004";
                result.ErrMsg = "無效簽名";
                return result;
            }
            TimeSpan expiresSliding = DateTime.Now.AddMinutes(120) - DateTime.Now;
            yuebonCacheHelper.Add("request_" + timeStamp + nonce, timeStamp + nonce, expiresSliding);
            bool blValidate = Validate(timeStamp.ToString(), nonce, allowCacheApp.AppSecret, data, signature);
            if (!blValidate)
            {
                result.ErrCode = "40004";
                result.ErrMsg = "無效簽名";
                return result;
            }
            else
            {
                result.ErrCode = "0";
                result.Success = true;
                return result;
            }
        }
        /// <summary>
        /// get請求查詢參數， url上直接接參數時,通過此方法獲取
        /// </summary>
        /// <param name="form">請求參數</param>
        /// <returns></returns>
        public static string GetQueryString(NameValueCollection form)
        {
            //第一步：取出所有get參數
            Dictionary<string, string> parames = new Dictionary<string, string>();
            for (int f = 0; f < form.Count; f++)
            {
                var key = form.Keys[f];
                if (key != null)
                    parames.Add(key, form[key]);
            }
            // 第二步：把字典按Key的字母順序排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parames);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();

            // 第三步：把所有參數名和參數值串在一起
            StringBuilder query = new StringBuilder("");  //簽名字符串
            if (parames == null || parames.Count == 0) return query.ToString();
            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrEmpty(key)) query.Append(key).Append(value);
            }
            return query.ToString();
        }
        /// <summary>
        /// 簽名驗證
        /// </summary>
        /// <param name="timeStamp">時間戳</param>
        /// <param name="nonce">隨機字符串</param>
        /// <param name="appSecret">客戶端應用密鑰</param>
        /// <param name="data">接口參數內容</param>
        /// <param name="signature">當前請求內容的數字簽名</param>
        /// <returns></returns>
        public static bool Validate(string timeStamp,string nonce,string appSecret, string data,string signature)
        {
            var signStr = timeStamp + nonce  +  data + appSecret;
            string signMd5 = MD5Util.GetMD5_32(signStr);
            return signMd5 == signature;
        }


        /// <summary>
        /// 驗證appId是否被允許
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        private static AllowCacheApp VerifyAppId(string appId)
        {
            AllowCacheApp allowCacheApp = new AllowCacheApp();
            if (string.IsNullOrEmpty(appId)) return allowCacheApp;
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            List<AllowCacheApp> list = yuebonCacheHelper.Get("AllowAppId").ToJson().ToList<AllowCacheApp>();
            if (list!=null&& list.Count>0)
            {
                allowCacheApp = list.Where(s => s.AppId == appId).FirstOrDefault();
            }
            return allowCacheApp;
        }
    }
}
