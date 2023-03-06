using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Services;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Data;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 服務接口實現
    /// </summary>
    public class SendMailInfoService: BaseService<SendMailInfo,SendMailInfoOutputDto, string>, ISendMailInfoService
    {
		private readonly ISendMailInfoRepository _repository;
        private readonly IComplaintNoticeMailService _complaintNoticeMailService;

        public SendMailInfoService(ISendMailInfoRepository repository,IComplaintNoticeMailService complaintNoticeMailService) : base(repository)
        {
			_repository=repository;
            _complaintNoticeMailService = complaintNoticeMailService;
        }

        public Task<long> ComplaintSendMailInfoInsertAsync(SendMailInfo entity, IDbConnection conn = null, IDbTransaction trans = null)
        {
            
            return _repository.InsertAsync(entity,conn,trans);
        }
    }
}