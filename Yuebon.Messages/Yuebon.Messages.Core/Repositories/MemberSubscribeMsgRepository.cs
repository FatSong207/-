using Dapper;
using System;
using System.Data.Common;
using Yuebon.Commons.Repositories;
using Yuebon.Messages.Dtos;
using Yuebon.Messages.IRepositories;
using Yuebon.Messages.Models;

namespace Yuebon.Messages.Repositories
{
    /// <summary>
    /// 倉儲接口的實現
    /// </summary>
    public class MemberSubscribeMsgRepository : BaseRepository<MemberSubscribeMsg, string>, IMemberSubscribeMsgRepository
    {
		public MemberSubscribeMsgRepository()
        {
            this.tableName = "Sys_MemberSubscribeMsg";
            this.primaryKey = "Id";
        }


        /// <summary>
        /// 根據消息類型查詢消息模板
        /// </summary>
        /// <param name="messageType">消息類型</param>
        /// <param name="userId">用戶Id</param>
        /// <returns></returns>
        public MemberMessageTemplatesOuputDto GetByMessageTypeWithUser(string messageType, string userId)
        {
            string sqlStr = @"select a.*,b.Id as MemberSubscribeMsgId,b.SubscribeStatus as SubscribeStatus,b.SubscribeType as SubscribeType  from Sys_MessageTemplates as a 
LEFT join Sys_MemberSubscribeMsg as b on a.Id = b.MessageTemplateId where a.UseInWxApplet =1 and a.WxAppletSubscribeTemplateId is not null and a.messageType = '" + messageType + "' and b.SubscribeUserId='" + userId + "'";
            return DapperConn.QueryFirstOrDefault<MemberMessageTemplatesOuputDto>(sqlStr);
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
            string sqlStr = @"select * from [dbo].[Sys_MemberSubscribeMsg]   where SubscribeUserId = '" + userId + "' and SubscribeType='" + subscribeType + "' and MessageTemplateId='" + messageTemplateId + "'";
            return DapperConn.QueryFirstOrDefault<MemberMessageTemplatesOuputDto>(sqlStr);
        }
    }
}