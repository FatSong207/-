using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;
using Newtonsoft.Json;
using System;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Json;
using Yuebon.Commons.Log;
using Yuebon.Commons.Options;

namespace Yuebon.SMS.AliYun
{
    /// <summary>
    /// 阿里云短信接口
    /// </summary>
    public class AliYunSMS
    {
        //產品名稱:云通信短信API產品
        private const string product = "Dysmsapi";
        //短信API產品域名
        private const string domain = "dysmsapi.aliyuncs.com";

        public AliYunSMS()
        {
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            AppSetting sysSetting = yuebonCacheHelper.Get<AppSetting>("SysSetting");
            if (sysSetting != null)
            {
                this.Appkey = DEncrypt.Decrypt(sysSetting.Smsusername);
                this.Appsecret = DEncrypt.Decrypt(sysSetting.Smspassword);
                this.SignName = sysSetting.SmsSignName;
            }
        }


        /// <summary>
        /// 群發
        /// </summary>
        /// <param name="phoneNumbers">必填:待發送手機號。支持JSON格式的批量調用，批量上限為100個手機號碼</param>
        /// <param name="TemplateCode">必填:短信模板</param>
        /// <param name="message">必填:模板中的變量替換JSON串,如模板內容為"親愛的${name},您的驗證碼為${code}"時,此處的值為 "{\"name\":\"Tom\"， \"code\":\"123\"}"</param>
        /// <param name="returnMsg"></param>
        /// <param name="OutId">可選:outId為提供給業務方擴展字段,最終在短信回執消息中將此值帶回給調用者</param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool Send(string[] phoneNumbers, string TemplateCode, string message, out string returnMsg, string OutId = "", string speed = "1")
        {
            if ((((phoneNumbers == null) || (phoneNumbers.Length == 0)) || string.IsNullOrEmpty(message)) || (message.Trim().Length == 0))
            {
                returnMsg = "手機號碼和消息內容不能為空";
                return false;
            }
            string phoneNumbersTot = string.Join(",", phoneNumbers);

            return this.Send(phoneNumbersTot, TemplateCode, message, out returnMsg, OutId, "0");

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
        public  bool Send(string cellPhone, string templateCode, string message, out string returnMsg, string OutId = "", string speed = "0")
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
            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", Appkey, Appsecret);
            IAcsClient acsClient = new DefaultAcsClient(profile);
            CommonRequest request = new CommonRequest();
            if (string.IsNullOrEmpty(templateCode) || templateCode.Length <= 0)
            {
                templateCode = "SMS_139390116";//阿里云自帶的
            }
            try
            {
                request.Domain = domain;
                request.Method = MethodType.POST;
                request.Action = "SendSms";
                request.Version = "2017-05-25";
                request.Product = "Dysmsapi";
                //必填:待發送手機號。支持以逗號分隔的形式進行批量調用，批量上限為20個手機號碼,批量調用相對于單條調用及時性稍有延遲,驗證碼類型的短信推薦使用單條調用的方式
                request.AddQueryParameters("PhoneNumbers", cellPhone);
                //必填:短信簽名-可在短信控制臺中找到
                request.AddQueryParameters("SignName", SignName);
                //必填:短信模板-可在短信控制臺中找到
                request.AddQueryParameters("TemplateCode", templateCode);
                //可選:模板中的變量替換JSON串,如模板內容為"親愛的${name},您的驗證碼為${code}"時,此處的值為 "{\"name\":\"Tom\"， \"code\":\"123\"}"

                request.AddQueryParameters("TemplateParam", message);

                //可選:outId為提供給業務方擴展字段,最終在短信回執消息中將此值帶回給調用者
                if (string.IsNullOrEmpty(OutId) || OutId.Length == 0)
                {
                    OutId = DateTime.Now.Ticks.ToString();
                }
                request.AddQueryParameters("OutId", OutId);
                //請求失敗這里會拋ClientException異常
                CommonResponse commonResponse = new CommonResponse();
                commonResponse = acsClient.GetCommonResponse(request);
                SMSResult sMSResult = JsonConvert.DeserializeObject<SMSResult>(commonResponse.Data);
                returnMsg = sMSResult.Message;
                if (sMSResult.Code == "OK")
                {
                    return true;
                }
                else
                {
                    Log4NetHelper.Error("發送短信錯誤，"+ commonResponse.ToJson().ToString()); 
                    return false;
                }


            }
            catch (ServerException e)
            {
                returnMsg = "未知錯誤(ServerException 1)" + e.ErrorMessage;
                return false;
            }
            catch (ClientException e)
            {
                returnMsg = "未知錯誤(ClientException 2)" + e.ErrorMessage;
                return false;
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

    }
}
