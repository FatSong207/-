using System;
using Yuebon.Commons.IRepositories;
using Yuebon.Messages.Dtos;
using Yuebon.Messages.Models;

namespace Yuebon.Messages.IRepositories
{
    /// <summary>
    /// 定義倉儲接口
    /// </summary>
    public interface IMemberSubscribeMsgRepository:IRepository<MemberSubscribeMsg, string>
    {

        /// <summary>
        /// 根據消息類型查詢消息模板
        /// </summary>
        /// <param name="messageType">消息類型</param>
        /// <param name="userId">用戶Id</param>
        /// <returns></returns>
        MemberMessageTemplatesOuputDto GetByMessageTypeWithUser(string messageType, string userId);

        /// <summary>
        /// 按用戶、訂閱類型和消息模板主鍵查詢
        /// </summary>
        /// <param name="subscribeType">消息類型</param>
        /// <param name="userId">用戶</param>
        /// <param name="messageTemplateId">模板Id主鍵</param>
        /// <returns></returns>
        MemberMessageTemplatesOuputDto GetByWithUser(string subscribeType, string userId, string messageTemplateId);
    }
}