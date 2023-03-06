using System;
using Yuebon.Commons.Services;
using Yuebon.Security.IServices;
using Yuebon.Messages.IRepositories;
using Yuebon.Messages.IServices;
using Yuebon.Messages.Dtos;
using Yuebon.Messages.Models;
using System.Collections.Generic;

namespace Yuebon.Messages.Services
{
    /// <summary>
    /// 服務接口實現
    /// </summary>
    public class MessageTemplatesService: BaseService<MessageTemplates,MessageTemplatesOutputDto, string>, IMessageTemplatesService
    {
		private readonly IMessageTemplatesRepository _repository;
        private readonly ILogService _logService;
        public MessageTemplatesService(IMessageTemplatesRepository repository,ILogService logService) : base(repository)
        {
			_repository=repository;
			_logService=logService;
        }

        /// <summary>
        /// 根據消息類型查詢消息模板
        /// </summary>
        /// <param name="messageType">消息類型</param>
        /// <returns></returns>
        public MessageTemplates GetByMessageType(string messageType)
        {
            return _repository.GetWhere("messageType='" + messageType + "'");
        }

        /// <summary>
        /// 根據用戶查詢微信小程序訂閱消息模板列表，關聯用戶訂閱表
        /// </summary>
        /// <param name="userId">用戶編號</param>
        /// <returns></returns>
        public List<MemberMessageTemplatesOuputDto> ListByUseInWxApplet(string userId)
        {
            return _repository.ListByUseInWxApplet(userId);
        }
    }
}