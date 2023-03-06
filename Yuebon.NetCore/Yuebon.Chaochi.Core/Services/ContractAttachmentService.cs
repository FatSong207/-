using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Services;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Linq;
using System;
using System.Data;
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 合約附件服務接口實現
    /// </summary>
    public class ContractAttachmentService: BaseService<ContractAttachment,ContractAttachmentOutputDto, string>, IContractAttachmentService
    {
		private readonly IContractAttachmentRepository _repository;
        private readonly Security.IServices.IUserService _userService;
        public ContractAttachmentService(IContractAttachmentRepository repository, Security.IServices.IUserService userService) : base(repository)
        {
			_repository = repository;
            _userService = userService;
        }

        /// <summary>
        /// 根據合約編號查詢合約必要附件
        /// 
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        public async Task<List<ContractAttachmentOutputDto>> GetMajorByCID(string cid)
        {
            List<ContractAttachmentOutputDto> resultList = new List<ContractAttachmentOutputDto>();

            IEnumerable<ContractAttachment> elist = await _repository.GetListWhereAsync(string.Format("CID = '{0}' AND ArchiveTo != '' AND ArchiveTo IS NOT NULL", cid));
            //List<ContractAttachment> attachmentList = elist.OrderBy(t => t.Version).ToList();
            List<ContractAttachment> attachmentList = elist.ToList();
            foreach (ContractAttachment attachment in attachmentList) {
                ContractAttachmentOutputDto attachmentOutputDto = new ContractAttachmentOutputDto();
                attachmentOutputDto = attachment.MapTo<ContractAttachmentOutputDto>();
                resultList.Add(attachmentOutputDto);
            }

            return resultList;
        }

        /// <summary>
        /// 根據合約編號查詢合約其他附件
        /// 
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>   
        public async Task<List<ContractAttachmentOutputDto>> GetMinorByCID(string cid)
        {
            List<ContractAttachmentOutputDto> resultList = new List<ContractAttachmentOutputDto>();

            IEnumerable<ContractAttachment> elist = await _repository.GetListWhereAsync(string.Format("CID = '{0}' AND (ArchiveTo = '' OR ArchiveTo IS NULL)", cid));
            List<ContractAttachment> attachmentList = elist.ToList();
            foreach (ContractAttachment attachment in attachmentList) {
                ContractAttachmentOutputDto attachmentOutputDto = new ContractAttachmentOutputDto();
                attachmentOutputDto = attachment.MapTo<ContractAttachmentOutputDto>();
                resultList.Add(attachmentOutputDto);
            }

            return resultList;
        }

        /// <summary>
        /// 根據合約必要附件主鍵值查詢合約必要附件狀態
        /// 
        /// </summary>
        /// <param name="id">合約必要附件主鍵值</param>
        /// <returns></returns>   
        public async Task<string> GetStatusById(string id)
        {
            string status = string.Empty;

            ContractAttachment attachment = await _repository.GetWhereAsync(string.Format("Id = '{0}'", id));
            if (attachment != null) {
                status = attachment.Status;
            }

            return status;
        }

        /// <summary>
        /// 更新主約附件狀態
        /// 
        /// </summary>
        /// <param name="account">使用者帳號</param>
        /// <param name="cid">合約編號</param>
        /// <param name="status">附件狀態</param>
        /// <returns></returns> 
        public async Task<bool> UpdateContractAttachmentStatus(string account, string cid, string cname, string status, IDbConnection conn = null, IDbTransaction trans = null)
        {
            DateTime time = DateTime.Now;
            Security.Models.User user = await _userService.GetUserByLogin(account);
            string userName = (user is not null) ? user.RealName : string.Empty;
            string userId = (user is not null) ? user.Id : string.Empty;

            bool result = false;
            ContractAttachment info = await _repository.GetWhereAsync($"cid = '{cid}' AND FormName = '{cname}'", conn, trans);


            if (info != null) {
                info.Status = status;
                if ("未綁定".Equals(status)) {
                    info.UploadTime = null;
                    info.UploadUserId = null;
                } else {
                    info.UploadTime = time;
                    info.UploadUserId = userName;
                }
                info.LastModifyTime = time;
                info.LastModifyUserId = userId;

                result = await repository.UpdateAsync(info, info.Id, conn, trans);
            }

            return result;
        }
    }
}