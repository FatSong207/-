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
using System.Linq;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 合約審核記錄服務接口實現
    /// </summary>
    public class ContractFlowLogService: BaseService<ContractFlowLog,ContractFlowLogOutputDto, string>, IContractFlowLogService
    {
		private readonly IContractFlowLogRepository _repository;
        public ContractFlowLogService(IContractFlowLogRepository repository) : base(repository)
        {
			_repository=repository;
        }

        /// <summary>
        /// 根據合約編號查詢審核記錄
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>
        public async Task<List<ContractFlowLogOutputDto>> GetByCID(string cid)
        {
            List<ContractFlowLogOutputDto> resultList = new List<ContractFlowLogOutputDto>();

            IEnumerable<ContractFlowLog> elist = await _repository.GetListWhereAsync(string.Format(" CID like '%{0}%'", cid));
            List<ContractFlowLog> flowLogList = elist.OrderBy(t => t.CreatorTime).ToList();
            foreach (ContractFlowLog flowLog in flowLogList) {
                ContractFlowLogOutputDto outputDto = flowLog.MapTo<ContractFlowLogOutputDto>();
                resultList.Add(outputDto);
            }

            return resultList;
        }

        /// <summary>
        /// 根據合約編號和合約狀態查詢審核記錄
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <param name="cstatus">合約狀態</param>
        /// <returns></returns>
        public async Task<List<ContractFlowLogOutputDto>> GetByCStatus(string cid, string cstatus)
        {
            List<ContractFlowLogOutputDto> resultList = new List<ContractFlowLogOutputDto>();

            IEnumerable<ContractFlowLog> elist = await _repository.GetListWhereAsync(string.Format(" CID = '{0}' AND CStatus = '{1}'", cid, cstatus));
            List<ContractFlowLog> flowLogList = elist.OrderBy(t => t.CreatorTime).ToList();
            foreach (ContractFlowLog flowLog in flowLogList) {
                ContractFlowLogOutputDto outputDto = flowLog.MapTo<ContractFlowLogOutputDto>();
                resultList.Add(outputDto);
            }

            return resultList;
        }
    }
}