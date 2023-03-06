using System;
using System.Collections.Generic;
using Yuebon.Commons.IRepositories;
using Yuebon.Messages.Dtos;
using Yuebon.Messages.Models;

namespace Yuebon.Messages.IRepositories
{
    /// <summary>
    /// 定義倉儲接口
    /// </summary>
    public interface IMessageTemplatesRepository:IRepository<MessageTemplates, string>
    {

        /// <summary>
        /// 根據用戶查詢微信小程序訂閱消息模板列表，關聯用戶訂閱表
        /// </summary>
        /// <param name="userId">用戶編號</param>
        /// <returns></returns>
        List<MemberMessageTemplatesOuputDto> ListByUseInWxApplet(string userId);
    }
}