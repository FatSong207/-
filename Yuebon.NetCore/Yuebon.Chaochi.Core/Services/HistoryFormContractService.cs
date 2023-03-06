using System.Threading.Tasks;
using Yuebon.Commons.Services;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Collections.Generic;
using System.Linq;
using Yuebon.Commons.Mapping;
using System;
using System.Data;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 合約歷史服務接口實現
    /// </summary>
    public class HistoryFormContractService: BaseService<HistoryFormContract,HistoryFormContractOutputDto, string>, IHistoryFormContractService
    {
		private IHistoryFormContractRepository _repository;
        private Security.IServices.IUserService _userService;
        public HistoryFormContractService(IHistoryFormContractRepository repository, Security.IServices.IUserService userService) : base(repository)
        {
			_repository=repository;
            _userService = userService;
        }

        /// <summary>
        /// 根據合約編號查詢合約歷史
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        public async Task<List<HistoryFormContractOutputDto>> GetByCID(string cid)
        {
            List<HistoryFormContractOutputDto> resultList = new List<HistoryFormContractOutputDto>();

            IEnumerable<HistoryFormContract> elist = await _repository.GetListWhereAsync(string.Format(" CID = '{0}'", cid));
            List<HistoryFormContract> historyList = elist.OrderByDescending(t => t.CreatorTime).ToList();
            foreach (HistoryFormContract history in historyList) {
                HistoryFormContractOutputDto outputDto = history.MapTo<HistoryFormContractOutputDto>();
                resultList.Add(outputDto);
            }

            return resultList;
        }

        /// <summary>
        /// 根據合約編號+合約歷史版號查詢合約歷史
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <param name="version">合約歷史版號</param>
        /// <returns></returns>
        public async Task<HistoryFormContractOutputDto> GetByVersion(string cid, string version)
        {            
            HistoryFormContract history = await _repository.GetWhereAsync(string.Format(" CID = '{0}' AND Version = '{1}'", cid, version));
            HistoryFormContractOutputDto outputDto = history.MapTo<HistoryFormContractOutputDto>();

            return outputDto;
        }

        public async Task<int> GetMaxVersionByCID(string cid)
        {
            string where = string.Format("CID = '{0}'", cid);
            decimal result = await _repository.GetMaxValueByFieldAsync("Version", where);

            return (int) result;
        }

        /// <summary>
        /// 更新上傳最新版掃描檔路徑
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="cid">合約編號</param>
        /// <param name="version">目前歷史版本</param>
        /// <param name="path">掃描簽約檔路徑</param>
        /// <returns></returns> 
        public async Task<bool> UpdateSignedContractPath(string userId, string cid, string version, string path, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool result = false;
            HistoryFormContract info = await _repository.GetWhereAsync($"CID = '{cid}' AND Version = '{version}'", conn, trans);

            if (info != null) {
                info.SignedPDFPath = path;
                info.LastModifyTime = DateTime.Now;
                info.LastModifyUserId = userId;

                result = await repository.UpdateAsync(info, info.Id, conn, trans);
            }

            return result;
        }
    }
}