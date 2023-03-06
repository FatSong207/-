using Senparc.NeuChar;
using Senparc.NeuChar.Context;
using Senparc.NeuChar.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Yuebon.WeChat.MessageHandlers.WxOpenMessageHandler
{
    public class CustomWxOpenMessageContext : MessageContext<IRequestMessageBase, IResponseMessageBase>
    {
        public CustomWxOpenMessageContext()
        {
            base.MessageContextRemoved += CustomMessageContext_MessageContextRemoved;
        }

        public override IRequestMessageBase GetRequestEntityMappingResult(RequestMsgType requestMsgType, XDocument doc = null)
        {
            throw new NotImplementedException();
        }

        public override IResponseMessageBase GetResponseEntityMappingResult(ResponseMsgType responseMsgType, XDocument doc = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 當上下文過期，被移除時觸發的時間
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CustomMessageContext_MessageContextRemoved(object sender, Senparc.NeuChar.Context.WeixinContextRemovedEventArgs<IRequestMessageBase, IResponseMessageBase> e)
        {
            /* 注意，這個事件不是實時觸發的（當然你也可以專門寫一個線程監控）
             * 為了提高效率，根據WeixinContext中的算法，這里的過期消息會在過期后下一條請求執行之前被清除
             */

            var messageContext = e.MessageContext as CustomWxOpenMessageContext;
            if (messageContext == null)
            {
                return;//如果是正常的調用，messageContext不會為null
            }

            //TODO:這里根據需要執行消息過期時候的邏輯，下面的代碼僅供參考

            //Log.InfoFormat("{0}的消息上下文已過期",e.OpenId);
            //api.SendMessage(e.OpenId, "由于長時間未搭理客服，您的客服狀態已退出！");
        }
    }
}
