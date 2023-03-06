using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Yuebon.Commons.Core.Dtos;
using Yuebon.Commons.Extend;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;
using Yuebon.Commons.Services;
using Yuebon.Security.Dtos;
using Yuebon.Security.IRepositories;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;

namespace Yuebon.Security.Services
{
    /// <summary>
    /// 組織機構
    /// </summary>
    public class OrganizeDService: BaseService<OrganizeD, OrganizeOutputDto, string>, IOrganizeDService
    {
        private readonly IOrganizeDRepository _repository;
        private readonly ILogService _logService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logService"></param>
        public OrganizeDService(IOrganizeDRepository repository, ILogService logService) : base(repository)
        {
            _repository = repository;
            _logService = logService;
        }


        /// <summary>
        /// 獲取組織機構適用于Vue 樹形列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrganizeOutputDto>> GetAllOrganizeTreeTable()
        {
            List<OrganizeOutputDto> reslist = new List<OrganizeOutputDto>();
            IEnumerable<OrganizeD> elist = await _repository.GetAllAsync();
            List<OrganizeD> list = elist.OrderBy(t => t.SortCode).ToList();
            List<OrganizeD> oneMenuList = list.FindAll(t => t.ParentId == "");
            foreach (OrganizeD item in oneMenuList)
            {
                OrganizeOutputDto menuTreeTableOutputDto = new OrganizeOutputDto();
                menuTreeTableOutputDto = item.MapTo<OrganizeOutputDto>();
                menuTreeTableOutputDto.Children = GetSubOrganizes(list, item.Id).ToList<OrganizeOutputDto>();
                reslist.Add(menuTreeTableOutputDto);
            }

            return reslist;
        }


        /// <summary>
        /// 獲取子集，遞歸調用
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parentId">父級Id</param>
        /// <returns></returns>
        private List<OrganizeOutputDto> GetSubOrganizes(List<OrganizeD> data, string parentId)
        {
            List<OrganizeOutputDto> list = new List<OrganizeOutputDto>();
            OrganizeOutputDto OrganizeOutputDto = new OrganizeOutputDto();
            var ChilList = data.FindAll(t => t.ParentId == parentId);
            foreach (OrganizeD entity in ChilList)
            {
                OrganizeOutputDto = entity.MapTo<OrganizeOutputDto>();
                OrganizeOutputDto.Children = GetSubOrganizes(data, entity.Id).OrderBy(t => t.SortCode).MapTo<OrganizeOutputDto>();
                list.Add(OrganizeOutputDto);
            }
            return list;
        }

        /// <summary>
        /// 檢查組織是否存在
        /// </summary>
        /// <param name="Id">組織ID</param>
        /// <returns></returns>
        public bool CheckOrganizeExist(string Id, IDbConnection conn = null, IDbTransaction trans = null) {
            OrganizeD org = _repository.GetWhere($"Id = {Id}", conn, trans);
            return org != null;
        }

        /// <summary>
        /// 取不存在的組織ID
        /// </summary>
        /// <param name="ids">匯入的組織ID清單</param>
        /// <returns></returns>
        public DeletesInputDto GetObsoleteOrgList(List<string> ids, IDbConnection conn = null, IDbTransaction trans = null) {
            DeletesInputDto deletesInputDto = new DeletesInputDto();
            IEnumerable<OrganizeD> orgs = _repository.GetAll(conn, trans);
            List<string> currentIds = orgs.Select(o => o.Id).ToList();
            string[] obsoleteIds = currentIds.Except(ids).ToArray();
            if (obsoleteIds != null && obsoleteIds.Length > 0) {
                deletesInputDto.Ids = obsoleteIds;
            }

            return deletesInputDto;
        }

        /// <summary>
        /// 取要被停用的組織
        /// </summary>
        /// <param name="orgImportList">匯入的組織清單</param>
        /// <returns></returns>
        public List<OrganizeD> GetObsoleteOrgList(List<OrganizeD> orgImportList, IDbConnection conn = null, IDbTransaction trans = null)
        {
            IEnumerable<OrganizeD> orgDBList = _repository.GetAll(conn, trans);
            List<OrganizeD> obsoleteOrgs = orgDBList.Except(orgImportList).ToList();

            return obsoleteOrgs;
        }

        /// <summary>
        /// 取要被停用的部門ID清單
        /// </summary>
        /// <param name="orgImportIdList">匯入的部門ID清單</param>
        /// <returns></returns>
        public List<string> GetObsoleteOrgIdList(List<string> orgImportIdList, IDbConnection conn = null, IDbTransaction trans = null)
        {
            List<string> orgIdDBList = _repository.GetAll(conn, trans).ToList(x => x.Id);
            List<string> obsoleteOrgs = orgIdDBList.Except(orgImportIdList).ToList();

            return obsoleteOrgs;
        }

        /// <summary>
        /// 獲取根節點組織
        /// </summary>
        /// <param name="id">組織Id</param>
        /// <returns></returns>
        public OrganizeD GetRootOrganize(string id)
        {
           return _repository.GetRootOrganize(id);
        }


        /// <summary>
        /// 按條件批量刪除
        /// </summary>
        /// <param name="idsInfo">主鍵Id集合</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public CommonResult DeleteBatchWhere(DeletesInputDto idsInfo, IDbTransaction trans = null)
        {
            CommonResult result = new CommonResult();
            string where = string.Empty;
            for (int i = 0; i < idsInfo.Ids.Length; i++)
            {
                if (idsInfo.Ids[0] != null)
                {
                    where = string.Format("ParentId='{0}'", idsInfo.Ids[0]);
                    IEnumerable<OrganizeD> list = _repository.GetListWhere(where);
                    if (list.Count() > 0)
                    {
                        result.ErrMsg = "功能存在子集數據，不能刪除";
                        return result;
                    }
                }
            }
            where = "id in ('" + idsInfo.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            bool bl = repository.DeleteBatchWhere(where);
            if (bl)
            {
                result.ErrCode = "0";
            }
            return result;
        }

        /// <summary>
        /// 按條件批量刪除
        /// </summary>
        /// <param name="idsInfo">主鍵Id集合</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public async Task<CommonResult> DeleteBatchWhereAsync(DeletesInputDto idsInfo, IDbTransaction trans = null)
        {
            CommonResult result = new CommonResult();
            string where = string.Empty;
            for (int i = 0; i < idsInfo.Ids.Length; i++)
            {
                if (idsInfo.Ids[0].ToString().Length > 0)
                {
                    where = string.Format("ParentId='{0}'", idsInfo.Ids[0]);
                    IEnumerable<OrganizeD> list = _repository.GetListWhere(where);
                    if (list.Count()>0)
                    {
                        result.ErrMsg = "該機構存在子集數據，不能刪除";
                        return result;
                    }
                }
            }
            where = "id in ('" + idsInfo.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            bool bl = await repository.DeleteBatchWhereAsync(where);
            if (bl)
            {
                result.ErrCode = "0";
            }
            return result;
        }
    }
}