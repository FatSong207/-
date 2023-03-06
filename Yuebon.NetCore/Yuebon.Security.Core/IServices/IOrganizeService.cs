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
    public interface IOrganizeService:IService<Organize, OrganizeOutputDto, string>
    {

        /// <summary>
        /// 用組織全名獲取組織機構資訊
        /// </summary>
        /// <param name="name">組織全名</param>
        Task<Organize> GetByName(string name);

        /// <summary>
        /// 獲取組織機構適用于Vue 樹形列表
        /// </summary>
        /// <returns></returns>
        Task<List<OrganizeOutputDto>> GetAllOrganizeTreeTable();

        /// <summary>
        /// 獲取人員所屬完整緮織ID清單
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetParentOrganizeIdList(string departmentId);

        /// <summary>
        /// 獲取人員所屬完整下層緮織ID清單
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetChildOrganizeIdList(string departmentId);

        /// <summary>
        /// 獲取當前使用者可觸碰ㄉ組織機構適用于Vue 樹形列表
        /// </summary>
        /// <returns></returns>
        Task<List<OrganizeOutputDto>> GetPermissionOrganizeTreeTable(List<string> permissionDeptsList);

        /// <summary>
        /// 獲取當前使用者可觸碰ㄉ組織機構適用于Vue 樹形列表
        /// </summary>
        /// <returns></returns>
        Task<List<OrganizeOutputDto>> GetPermissionOrganizeTreeTable2(List<string> permissionDeptsList);

        /// <summary>
        /// 獲取根節點組織
        /// </summary>
        /// <param name="id">組織Id</param>
        /// <returns></returns>
        Organize GetRootOrganize(string id);


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
