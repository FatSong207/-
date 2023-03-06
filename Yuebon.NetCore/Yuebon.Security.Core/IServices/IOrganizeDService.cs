using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Yuebon.Commons.Core.Dtos;
using Yuebon.Commons.IServices;
using Yuebon.Commons.Models;
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.Security.IServices
{
    /// <summary>
    /// 組織機構
    /// </summary>
    public interface IOrganizeDService:IService<OrganizeD, OrganizeOutputDto, string>
    {

        /// <summary>
        /// 獲取組織機構適用于Vue 樹形列表
        /// </summary>
        /// <returns></returns>
        Task<List<OrganizeOutputDto>> GetAllOrganizeTreeTable();

        /// <summary>
        /// 獲取根節點組織
        /// </summary>
        /// <param name="id">組織Id</param>
        /// <returns></returns>
        OrganizeD GetRootOrganize(string id);

        /// <summary>
        /// 檢查組織是否存在
        /// </summary>
        /// <param name="Id">組織ID</param>
        /// <returns></returns>
        bool CheckOrganizeExist(string Id, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 取不存在的組織ID
        /// </summary>
        /// <param name="ids">匯入的組織ID清單</param>
        /// <returns></returns>
        DeletesInputDto GetObsoleteOrgList(List<string> ids, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 取不存在的組織
        /// </summary>
        /// <param name="orgImportList">匯入的組織清單</param>
        /// <returns></returns>
        List<OrganizeD> GetObsoleteOrgList(List<OrganizeD> orgImportList, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 取要被停用的部門ID清單
        /// </summary>
        /// <param name="orgImportIdList">匯入的部門ID清單</param>
        /// <returns></returns>
        public List<string> GetObsoleteOrgIdList(List<string> orgImportIdList, IDbConnection conn = null, IDbTransaction trans = null);

        /// <summary>
        /// 按條件批量刪除
        /// </summary>
        /// <param name="ids">主鍵Id集合</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        CommonResult DeleteBatchWhere(DeletesInputDto ids, IDbTransaction trans = null);
        /// <summary>
        /// 異步按條件批量刪除
        /// </summary>
        /// <param name="ids">主鍵Id集合</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        Task<CommonResult> DeleteBatchWhereAsync(DeletesInputDto ids, IDbTransaction trans = null);
    }
}
