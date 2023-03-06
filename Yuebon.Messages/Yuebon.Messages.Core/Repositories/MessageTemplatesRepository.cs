using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using Yuebon.Commons.Linq;
using Yuebon.Commons.Repositories;
using Yuebon.Messages.Dtos;
using Yuebon.Messages.IRepositories;
using Yuebon.Messages.Models;

namespace Yuebon.Messages.Repositories
{
    /// <summary>
    /// 倉儲接口的實現
    /// </summary>
    public class MessageTemplatesRepository : BaseRepository<MessageTemplates, string>, IMessageTemplatesRepository
    {
		public MessageTemplatesRepository()
        {
            this.tableName = "Sys_MessageTemplates";
            this.primaryKey = "Id";
        }

        /// <summary>
        /// 根據用戶查詢微信小程序訂閱消息模板列表，關聯用戶訂閱表
        /// </summary>
        /// <param name="userId">用戶編號</param>
        /// <returns></returns>
        public List<MemberMessageTemplatesOuputDto> ListByUseInWxApplet(string userId)
        {
            string sqlStr = @"select a.*,b.Id as MemberSubscribeMsgId,b.SubscribeStatus as SubscribeStatus  from Sys_MessageTemplates as a 
LEFT join Sys_MemberSubscribeMsg as b on a.Id = b.MessageTemplateId and a.UseInWxApplet =1 and b.SubscribeUserId='" + userId + "'  where  a.WxAppletSubscribeTemplateId is not null";

            return DapperConn.Query<MemberMessageTemplatesOuputDto>(sqlStr).AsToList();
        }
    }
}