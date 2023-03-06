using System;
using Yuebon.Commons.Services;
using Yuebon.Security.IServices;
using Yuebon.Messages.IRepositories;
using Yuebon.Messages.IServices;
using Yuebon.Messages.Dtos;
using Yuebon.Messages.Models;

namespace Yuebon.Messages.Services
{
    /// <summary>
    /// 服務接口實現
    /// </summary>
    public class MemberSubscribeMsgService: BaseService<MemberSubscribeMsg,MemberSubscribeMsgOutputDto, string>, IMemberSubscribeMsgService
    {
		private readonly IMemberSubscribeMsgRepository _repository;
        private readonly ILogService _logService;
        public MemberSubscribeMsgService(IMemberSubscribeMsgRepository repository,ILogService logService) : base(repository)
        {
			_repository=repository;
			_logService=logService;
        }


        /// <summary>
        /// 根據消息類型查詢消息模板
        /// </summary>
        /// <param name="messageType">消息類型</param>
        /// <param name="userId">用戶Id</param>
        /// <returns></returns>
        public MemberMessageTemplatesOuputDto GetByMessageTypeWithUser(string messageType, string userId)
        {
            return _repository.GetByMessageTypeWithUser(messageType, userId);
        }


        /// <summary>
        /// 按用戶、訂閱類型和消息模板主鍵查詢
        /// </summary>
        /// <param name="subscribeType">消息類型</param>
        /// <param name="userId">用戶</param>
        /// <param name="messageTemplateId">模板Id主鍵</param>
        /// <returns></returns>
        public MemberMessageTemplatesOuputDto GetByWithUser(string subscribeType, string userId, string messageTemplateId)
        {

            return _repository.GetByWithUser(subscribeType, userId, messageTemplateId);
        }


        /// <summary>
        /// 根據消息模板Id（主鍵）查詢用戶訂閱消息
        /// </summary>
        /// <param name="messageTemplateId">消息模板主鍵</param>
        /// <param name="userId">用戶</param>
        /// <param name="subscribeType">消息類型</param>
        /// <returns></returns>
        public MemberSubscribeMsg GetByMessageTemplateIdAndUser(string messageTemplateId, string userId, string subscribeType)
        {
            string sqlWhere = "MessageTemplateId='" + messageTemplateId + "' and SubscribeUserId='" + userId + "' and SubscribeType='" + subscribeType + "'";
            return _repository.GetWhere(sqlWhere);
        }
        /// <summary>
        /// 更新用戶訂閱消息
        /// </summary>
        /// <param name="messageTemplateId">消息模板主鍵</param>
        /// <param name="userId">用戶</param>
        /// <param name="subscribeType">消息類型</param>
        /// <param name="subscribeStatus">訂閱狀態</param>
        /// <returns></returns>
        public bool UpdateByMessageTemplateIdAndUser(string messageTemplateId, string userId, string subscribeType, string subscribeStatus)
        {
            string sqlWhere = "MessageTemplateId='" + messageTemplateId + "' and SubscribeUserId='" + userId + "' and SubscribeType='" + subscribeType + "'";
            return _repository.UpdateTableField("SubscribeStatus", subscribeStatus, sqlWhere);
        }

        public long Insert(MemberSubscribeMsg info)
        {
            return _repository.Insert(info);
        }
        /// <summary>
        /// 更新訂閱狀態
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool UpdateSubscribeStatus(MemberSubscribeMsg info)
        {
            string sqlWhere = "MessageTemplateId='" + info.MessageTemplateId + "' and SubscribeUserId='" + info.SubscribeUserId + "' and SubscribeType='" + info.SubscribeType + "'";
            return _repository.UpdateTableField("SubscribeStatus", info.SubscribeStatus, sqlWhere);
        }
    }
}