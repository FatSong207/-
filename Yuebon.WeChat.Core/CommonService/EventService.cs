using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;
using Senparc.NeuChar.Entities;
using Senparc.NeuChar.Helpers;
using Senparc.CO2NET.Utilities;
using Microsoft.AspNetCore.Http;
using Yuebon.WeChat.CommonService.TemplateMessage;
using Yuebon.WeChat.CommonService.Utilities;
using Senparc.Weixin;
using Senparc.Weixin.MP;

namespace Yuebon.WeChat.CommonService
{
    /// <summary>
    /// 全局微信事件有關的處理程序
    /// </summary>
    public class EventService
    {
        /// <summary>
        /// 微信MessageHandler事件處理，此代碼的簡化MessageHandler方法已由/CustomerMessageHandler/CustomerMessageHandler_Event.cs完成，
        /// 此方法不再更新
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public ResponseMessageBase GetResponseMessage(RequestMessageEventBase requestMessage)
        {
            ResponseMessageBase responseMessage = null;
            switch (requestMessage.Event)
            {
                case Event.ENTER:
                    {
                        var strongResponseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();
                        strongResponseMessage.Content = "您剛才發送了ENTER事件請求。";
                        responseMessage = strongResponseMessage;
                        break;
                    }
                case Event.LOCATION:
                    throw new Exception("暫不可用");
                //break;
                case Event.subscribe://訂閱
                    {
                        var strongResponseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();

                        //獲取Senparc.Weixin.MP.dll版本信息
#if NET45
                        var dllPath = HttpContext.Current.Server.MapPath("~/bin/Senparc.Weixin.MP.dll");
#else
                        //var dllPath = ServerUtility.ContentRootMapPath("~/bin/Release/netcoreapp2.2/Senparc.Weixin.MP.dll");//本地測試路徑
                        var dllPath = ServerUtility.ContentRootMapPath("~/Senparc.Weixin.MP.dll");//發布路徑
#endif

                        var fileVersionInfo = FileVersionInfo.GetVersionInfo(dllPath);

                        var version = fileVersionInfo.FileVersion;
                        strongResponseMessage.Content = string.Format(
                            "歡迎關注【Senparc.Weixin.MP 微信公眾平臺SDK】，當前運行版本：v{0}。\r\n您還可以發送【位置】【圖片】【語音】信息，查看不同格式的回復。\r\nSDK官方地址：https://sdk.weixin.senparc.com",
                            version);
                        responseMessage = strongResponseMessage;
                        break;
                    }
                case Event.unsubscribe://退訂
                    {
                        //實際上用戶無法收到非訂閱帳號的消息，所以這里可以隨便寫。
                        //unsubscribe事件的意義在于及時刪除網站應用中已經記錄的OpenID綁定，消除冗余數據。
                        var strongResponseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();
                        strongResponseMessage.Content = "有空再來";
                        responseMessage = strongResponseMessage;
                        break;
                    }
                case Event.CLICK://菜單點擊事件，根據自己需要修改
                    //這里的CLICK在此DEMO中不會被執行到，因為已經重寫了OnEvent_ClickRequest
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return responseMessage;
        }

        public void ConfigOnWeixinExceptionFunc(WeixinException ex)
        {
            Senparc.Weixin.WeixinTrace.SendCustomLog("進入 ConfigOnWeixinExceptionFunc() 方法", ex.Message);
            try
            {
                Task.Factory.StartNew(async () =>
                {
                    var appId = Config.SenparcWeixinSetting.WeixinAppId;

                    string openId = "";//收到通知的管理員OpenId
                    var host = "A1 / AccessTokenOrAppId：" + (ex.AccessTokenOrAppId ?? "null");
                    string service = null;
                    string message = ex.Message;
                    var status = ex.GetType().Name;
                    var remark = "\r\n這是一條通過OnWeixinExceptionFunc事件發送的異步模板消息";
                    string url = "https://github.com/JeffreySu/WeiXinMPSDK/blob/24aca11630bf833f6a4b6d36dce80c5b171281d3/src/Senparc.Weixin.MP.Sample/Senparc.Weixin.MP.Sample/Global.asax.cs#L246";//需要點擊打開的URL

                    var sendTemplateMessage = true;

                    if (ex is ErrorJsonResultException)
                    {
                        var jsonEx = (ErrorJsonResultException)ex;
                        service = jsonEx.Url;
                        message = jsonEx.Message;

                        //需要忽略的類型
                        var ignoreErrorCodes = new[]
                        {
                                ReturnCode.获取access_token时AppSecret错误或者access_token无效,
                                ReturnCode.template_id不正确,
                                ReturnCode.缺少access_token参数,
                                ReturnCode.api功能未授权,
                                ReturnCode.用户未授权该api,
                                ReturnCode.参数错误invalid_parameter,
                                ReturnCode.接口调用超过限制,
                                ReturnCode.需要接收者关注,//43004

                                //其他更多可能的情況
                            };
                        if (ignoreErrorCodes.Contains(jsonEx.JsonResult.errcode))
                        {
                            sendTemplateMessage = false;//防止無限遞歸，這種請款那個下不發送消息
                        }

                        //TODO:防止更多的接口自身錯誤導致的無限遞歸。
                    }
                    else
                    {
                        if (ex.Message.StartsWith("openid:"))
                        {
                            openId = ex.Message.Split(':')[1];//發送給指定OpenId
                        }
                        service = "WeixinException";
                        message = ex.Message;
                    }


                    if (sendTemplateMessage)    // DPBMARK MP
                    {
                        int sleepSeconds = 3;
                        Thread.Sleep(sleepSeconds * 1000);
                        var data = new WeixinTemplate_ExceptionAlert(string.Format("微信發生異常（延時{0}秒）", sleepSeconds), host, service, status, message, remark);

                        //修改OpenId、啟用以下代碼后即可收到模板消息
                        if (!string.IsNullOrEmpty(openId))
                        {
                            var result = await Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessageAsync(appId, openId, data.TemplateId,
                              url, data);
                        }
                    }                           // DPBMARK_END
                });
            }
            catch (Exception e)
            {
                Senparc.Weixin.WeixinTrace.SendCustomLog("OnWeixinExceptionFunc過程錯誤", e.Message);
            }
        }
    }
}
