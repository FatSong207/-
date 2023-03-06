using System;
using System.Linq;
using System.Threading.Tasks;
using Senparc.WebSocket;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.WxOpen.Containers;
using Senparc.Weixin;

namespace Yuebon.WeChat.CommonService.MessageHandlers.WebSocket
{
    /// <summary>
    /// .NET Core 自定義 WebSocket 處理類
    /// </summary>
    public class CustomNetCoreWebSocketMessageHandler : WebSocketMessageHandler
    {
        public override Task OnConnecting(WebSocketHelper webSocketHandler)
        {
            //TODO:處理連接時的邏輯
            return base.OnConnecting(webSocketHandler);
        }

        public override Task OnDisConnected(WebSocketHelper webSocketHandler)
        {
            //TODO:處理斷開連接時的邏輯
            return base.OnDisConnected(webSocketHandler);
        }


        public override async Task OnMessageReceiced(WebSocketHelper webSocketHandler, ReceivedMessage receivedMessage, string originalData)
        {
            if (receivedMessage == null || string.IsNullOrEmpty(receivedMessage.Message))
            {
                return;
            }

            var message = receivedMessage.Message;

            await webSocketHandler.SendMessage("originalData：" + originalData, webSocketHandler.WebSocket.Clients.Caller);
            await webSocketHandler.SendMessage("您發送了文字：" + message, webSocketHandler.WebSocket.Clients.Caller);
            await webSocketHandler.SendMessage("正在處理中（反轉文字）...", webSocketHandler.WebSocket.Clients.Caller);

            await Task.Delay(1000);

            //處理文字
            var result = string.Concat(message.Reverse());
            await webSocketHandler.SendMessage(result, webSocketHandler.WebSocket.Clients.Caller);

            var appId = Config.SenparcWeixinSetting.WxOpenAppId;//與微信小程序帳號后臺的AppId設置保持一致，區分大小寫。

            try
            {

                var sessionBag = SessionContainer.GetSession(receivedMessage.SessionId);

                //臨時演示使用固定openId
                var openId = sessionBag != null ? sessionBag.OpenId : "onh7q0DGM1dctSDbdByIHvX4imxA";// "用戶未正確登陸";

                //await webSocketHandler.SendMessage("OpenId：" + openId, webSocketHandler.WebSocket.Clients.Caller);
                //await webSocketHandler.SendMessage("FormId：" + formId);

                //群發
                await webSocketHandler.SendMessage($"[群發消息] [來自 OpenId：***{openId.Substring(openId.Length - 10, 10)}，昵稱：{sessionBag.DecodedUserInfo?.nickName}]：{message}", webSocketHandler.WebSocket.Clients.All);

            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\r\n\r\n" + originalData + "\r\n\r\nAPPID:" + appId;

                await webSocketHandler.SendMessage(msg, webSocketHandler.WebSocket.Clients.Caller); //VS2017以下如果編譯不通過，可以注釋掉這一行

                WeixinTrace.SendCustomLog("WebSocket OnMessageReceiced()過程出錯", msg);
            }
        }
    }
}
