using Newtonsoft.Json;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Log;
using Yuebon.Commons.Options;

namespace Yuebon.SMS.Zutong
{
    /// <summary>
    /// 助通科技融合云通信
    /// </summary>
    public class ZutongSMS
    {

        public ZutongSMS()
        {
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            AppSetting sysSetting = yuebonCacheHelper.Get<AppSetting>("SysSetting");
            if (sysSetting != null)
            {
                this.Appkey = DEncrypt.Decrypt(sysSetting.Smsusername);
                this.Appsecret = DEncrypt.Decrypt(sysSetting.Smspassword);
                this.Domain = sysSetting.Smsapiurl;
                this.SignName = sysSetting.SmsSignName;
            }
        }

        /// <summary>
        /// 短信發送
        /// </summary>
        /// <param name="cellPhone">必填:待發送手機號。支持以逗號分隔的形式進行批量調用，批量上限為1000個手機號碼,批量調用相對于單條調用及時性稍有延遲,驗證碼類型的短信推薦使用單條調用的方式，發送國際/港澳臺消息時，接收號碼格式為00+國際區號+號碼，如“0085200000000”</param>
        /// <param name="templateCode">模板code</param>
        /// <param name="OutId">可選:outId為提供給業務方擴展字段,最終在短信回執消息中將此值帶回給調用者</param>
        /// <param name="message">可選:模板中的變量替換JSON串,如模板內容為"親愛的${name},您的驗證碼為${code}"時,此處的值為 "{\"name\":\"Tom\"， \"code\":\"123\"}"</param>
        /// <param name="returnMsg"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool Send(string cellPhone, string templateCode, string message, out string returnMsg, string OutId = "", string speed = "0")
        {
            if ((string.IsNullOrEmpty(cellPhone) || (cellPhone.Trim().Length == 0)))
            {
                returnMsg = "手機號碼和消息內容不能為空";
                return false;
            }
            if (string.IsNullOrEmpty(message))
            {
                message = "";
            }
            try
            {
                SendSmsTp sendSmsTp = new SendSmsTp();
                TimeSpan ts = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
                sendSmsTp.username = this.Appkey;
                sendSmsTp.password = Encode(this.Appsecret);
                sendSmsTp.tKey = (long)ts.TotalMilliseconds / 1000;
                sendSmsTp.signature = this.SignName;
                sendSmsTp.tpId = templateCode.ToLong();
                List<record> records = new List<record>();
                record item=   new record
                {
                    mobile = cellPhone,
                    tpContent = message
                };
                records.Add(item);
                sendSmsTp.records = records;
                var json = sendSmsTp.ToJson();
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response =  HttpRequestHelper.HttpPost(this.Domain, data);
                SMSResult sMSResult = JsonConvert.DeserializeObject<SMSResult>(response);
                returnMsg = sMSResult.Msg;
                if (sMSResult.Code ==200)
                {
                    return true;
                }
                else
                {
                    Log4NetHelper.Error("發送短信錯誤，" + response.ToJson().ToString());
                    return false;
                }


            }
            catch (Exception e)
            {
                returnMsg = "未知錯誤(Exception )" + e.Message;
                return false;
            }
        }

        private static string Encode(string txt)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(txt);
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Appkey 應用Id
        /// </summary>
        public string Appkey { get; set; }
        /// <summary>
        /// Appsecret應用密鑰
        /// </summary>
        public string Appsecret { get; set; }

        /// <summary>
        /// 簽名
        /// </summary>
        public string SignName { get; set; }

        /// <summary>
        /// 短信API產品域名 Url地址
        /// </summary>
        public string Domain { get; set; }
    }
}
