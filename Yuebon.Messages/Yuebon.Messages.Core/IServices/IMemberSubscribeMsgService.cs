using System;
using Yuebon.Commons.IServices;
using Yuebon.Messages.Dtos;
using Yuebon.Messages.Models;

namespace Yuebon.Messages.IServices
{
    /// <summary>
    /// 定義服務接口
    /// </summary>
    public interface IMemberSubscribeMsgService:IService<MemberSubscribeMsg,MemberSubscribeMsgOutputDto, string>
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


        /// <summary>
        /// 根據消息模板Id（主鍵）查詢用戶訂閱消息
        /// </summary>
        /// <param name="messageTemplateId">消息模板主鍵</param>
        /// <param name="userId">用戶</param>
        /// <param name="subscribeType">消息類型</param>
        /// <returns></returns>
        MemberSubscribeMsg GetByMessageTemplateIdAndUser(string messageTemplateId, string userId, string subscribeType);
        /// <summary>
        /// 更新用戶訂閱消息
        /// </summary>
        /// <param name="messageTemplateId">消息模板主鍵</param>
        /// <param name="userId">用戶</param>
        /// <param name="subscribeType">消息類型</param>
        /// <param name="subscribeStatus">訂閱狀態</param>
        /// <returns></returns>
        bool UpdateByMessageTemplateIdAndUser(string messageTemplateId, string userId, string subscribeType, string subscribeStatus);

        long Insert(MemberSubscribeMsg info);
        /// <summary>
        /// 更新訂閱狀態
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool UpdateSubscribeStatus(MemberSubscribeMsg info);
    }
}
