using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Senparc.NeuChar.Context;
using Senparc.Weixin.Exceptions;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.MessageHandlers;
using Yuebon.WeChat.CommonService.Download;
using Yuebon.WeChat.CommonService.Utilities;
using Senparc.NeuChar.Entities;
using Microsoft.AspNetCore.Http;


namespace Yuebon.WeChat.CommonService.CustomMessageHandler
{
    /// <summary>
    /// 自定義MessageHandler
    /// </summary>
    public partial class CustomMessageHandler
    {
        public override Task<IResponseMessageBase> OnEvent_ClickRequestAsync(RequestMessageEvent_Click requestMessage)
        {
            return Task.Factory.StartNew<IResponseMessageBase>(() =>
            {
                var syncResponseMessage = OnEvent_ClickRequest(requestMessage);//這里為了保持Demo的連貫性，結果先從同步方法獲取，實際使用過程中可以全部直接定義異步方法
                //常識獲取Click事件的同步方法
                if (syncResponseMessage is ResponseMessageText)
                {
                    var textResponseMessage = syncResponseMessage as ResponseMessageText;
                    textResponseMessage.Content += "\r\n\r\n  -- 來自【異步MessageHandler】的回復";
                }

                return syncResponseMessage;
            });
        }
    }
}