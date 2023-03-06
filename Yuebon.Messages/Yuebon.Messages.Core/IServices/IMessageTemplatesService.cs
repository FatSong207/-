using System;
using System.Collections.Generic;
using Yuebon.Commons.IServices;
using Yuebon.Messages.Dtos;
using Yuebon.Messages.Models;

namespace Yuebon.Messages.IServices
{
    /// <summary>
    /// 定義服務接口
    /// </summary>
    public interface IMessageTemplatesService:IService<MessageTemplates,MessageTemplatesOutputDto, string>
    {

        /// <summary>
        /// 根據用戶查詢微信小程序訂閱消息模板列表，關聯用戶訂閱表
        /// </summary>
        /// <param name="userId">用戶編號</param>
        /// <returns></returns>
        List<MemberMessageTemplatesOuputDto> ListByUseInWxApplet(string userId);

        /// <summary>
        /// 根據消息類型查詢消息模板
        /// </summary>
        /// <param name="messageType">消息類型</param>
        /// <returns></returns>
        MessageTemplates GetByMessageType(string messageType);
    }
}
