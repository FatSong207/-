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

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 服務接口實現
    /// </summary>
    public class ComplaintNoticeMailService: BaseService<ComplaintNoticeMail,ComplaintNoticeMailOutputDto, string>, IComplaintNoticeMailService
    {
		private readonly IComplaintNoticeMailRepository _repository;
        public ComplaintNoticeMailService(IComplaintNoticeMailRepository repository) : base(repository)
        {
			_repository=repository;
        }
    }
}