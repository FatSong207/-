

using NPOI.OpenXmlFormats.Dml;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Yuebon.Commons.Core.Dtos;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;
using Yuebon.Commons.Services;
using Yuebon.Security.Dtos;
using Yuebon.Security.IRepositories;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;
using Zxw.Framework.NetCore.Extensions;

namespace Yuebon.Security.Services
{
    /// <summary>
    /// 組織機構
    /// </summary>
    public class OrganizeService: BaseService<Organize, OrganizeOutputDto, string>, IOrganizeService
    {
        private readonly IOrganizeRepository _repository;
        private readonly ILogService _logService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logService"></param>
        public OrganizeService(IOrganizeRepository repository, ILogService logService) : base(repository)
        {
            _repository = repository;
            _logService = logService;
        }

        /// <summary>
        /// 用組織全名獲取組織機構資訊
        /// </summary>
        /// <param name="name">組織全名</param>
        public async Task<Organize> GetByName(string name) {
            Organize info = await _repository.GetWhereAsync($"FullName = '{name}'");
            
            return info;
        }

        /// <summary>
        /// 獲取組織機構適用于Vue 樹形列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrganizeOutputDto>> GetAllOrganizeTreeTable()
        {
            List<OrganizeOutputDto> reslist = new List<OrganizeOutputDto>();
            IEnumerable<Organize> elist = await _repository.GetAllAsync();
            List<Organize> list = elist.OrderBy(t => t.SortCode).ToList();
            List<Organize> oneMenuList = list.FindAll(t => t.ParentId == "");
            foreach (Organize item in oneMenuList)
            {
                OrganizeOutputDto menuTreeTableOutputDto = new OrganizeOutputDto();
                menuTreeTableOutputDto = item.MapTo<OrganizeOutputDto>();
                menuTreeTableOutputDto.Children = GetSubOrganizes(list, item.Id).ToList<OrganizeOutputDto>();
                reslist.Add(menuTreeTableOutputDto);
            }

            return reslist;
        }

        /// <summary>
        /// 獲取人員所屬完整上層組織ID清單
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetParentOrganizeIdList(string departmentId)
        {
            List<string> reslist = new List<string>();
            IEnumerable<Organize> elist = await _repository.GetAllAsync();
            List<Organize> list = elist.OrderBy(t => t.SortCode).ToList();
            Organize department = await _repository.GetAsync(departmentId);
            if(department != null) {
                reslist.Add(department.Id);
            if (department.ParentId != "Root") {
                    List<string> parentDeptIdList = GetParentOrganizes(list, department.ParentId).Select(x => x.Id).ToList<string>();
                    if (parentDeptIdList.Count > 0) {
                        reslist.AddRange(parentDeptIdList);
                    }
                }
            }

            return reslist;
        }

        /// <summary>
        /// 獲取人員所屬完整下層緮織ID清單
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetChildOrganizeIdList(string departmentId)
        {
            List<string> reslist = new List<string>();
            IEnumerable<Organize> elist = await _repository.GetAllAsync();
            List<Organize> list = elist.OrderBy(t => t.SortCode).ToList();
            Organize department = await _repository.GetAsync(departmentId);
            if (department != null) {
                //reslist.Add(department.Id);
                //if (department.ParentId != "Root") {
                var deptLayers = department.Layers;
                var childDeptList = GetSubOrganizes(list, department.Id);
                if (childDeptList.Count > 0) {
                    var childDeptIdList = childDeptList.Select(x => x.Id).ToList<string>();
                    reslist.AddRange(childDeptIdList);
                    foreach (var childDept in childDeptList) {
                        if (childDept.Layers <= 7) {
                            childDeptIdList = await GetChildOrganizeIdList(childDept.Id);
                            reslist.AddRange(childDeptIdList);
                        }
                    }
                    //List<string> parentDeptIdList = GetSubOrganizes(list, department.Id).Select(x => x.Id).ToList<string>();

                }
                //}
            }

            return reslist;
        }

        /// <summary>
        /// 獲取人員所屬完整下層緮織清單
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrganizeOutputDto>> GetChildOrganizeList(string departmentId)
        {
            List<OrganizeOutputDto> reslist = new List<OrganizeOutputDto>();
            IEnumerable<Organize> elist = await _repository.GetAllAsync();
            List<Organize> list = elist.OrderBy(t => t.SortCode).ToList();
            Organize department = await _repository.GetAsync(departmentId);
            if (department != null) {
                var childDeptList = GetSubOrganizes(list, department.Id);
                if (childDeptList.Count > 0) {
                    reslist.AddRange(childDeptList);
                    foreach (var childDept in childDeptList) {
                        if (childDept.Layers <= 5) {
                            childDeptList = await GetChildOrganizeList(childDept.Id);
                            reslist.AddRange(childDeptList);
                        }
                    }
                }
            }

            return reslist;
        }

        /// <summary>
        /// 獲取當前使用者可觸碰的組織機構適用于Vue 樹形列表(組織倒數第二層)
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrganizeOutputDto>> GetPermissionOrganizeTreeTable(List<string> permissionDeptsList)
        {
            List<OrganizeOutputDto> reslist = new List<OrganizeOutputDto>();
            IEnumerable<Organize> elist = await _repository.GetListWhereAsync($" id in ('{permissionDeptsList.Join("','")}') ");
            List<Organize> list = elist.OrderBy(t => t.SortCode).ToList();
            List<Organize> oneMenuList = list.FindAll(x => x.Layers == list.Select(p => p.Layers).Min());
            foreach (Organize item in oneMenuList) {
                OrganizeOutputDto menuTreeTableOutputDto = new OrganizeOutputDto();
                menuTreeTableOutputDto = item.MapTo<OrganizeOutputDto>();
                menuTreeTableOutputDto.Children = GetSubOrganizes(list, item.Id).ToList<OrganizeOutputDto>();
                reslist.Add(menuTreeTableOutputDto);
            }

            return reslist;
        }

        /// <summary>
        /// 獲取當前使用者可觸碰的組織機構適用于Vue 樹形列表(組織最後一層)
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrganizeOutputDto>> GetPermissionOrganizeTreeTable2(List<string> permissionDeptsList)
        {
            List<OrganizeOutputDto> reslist = new List<OrganizeOutputDto>();
            IEnumerable<Organize> elist = await _repository.GetListWhereAsync($" id in ('{permissionDeptsList.Join("','")}') ");
            List<Organize> list = elist.OrderBy(t => t.SortCode).ToList();            
            //var layers = (list.Select(p => p.Layers).Min() <= 4) ? 5 : list.Select(p => p.Layers).Max();
            List<Organize> oneMenuList = list.FindAll(x => x.Layers == list.Select(p => p.Layers).Max());
            var item = oneMenuList.FirstOrDefault();            
            var childList = await GetChildOrganizeList(item.Id);
            OrganizeOutputDto parentOutputDto = new OrganizeOutputDto();
            parentOutputDto = item.MapTo<OrganizeOutputDto>();            
            if (childList.Count > 0) {
                parentOutputDto.Children = childList;
            }
            reslist.Add(parentOutputDto);

            return reslist;
        }


        /// <summary>
        /// 獲取子集，遞歸調用
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parentId">父級Id</param>
        /// <returns></returns>
        private List<OrganizeOutputDto> GetSubOrganizes(List<Organize> data, string parentId)
        {
            List<OrganizeOutputDto> list = new List<OrganizeOutputDto>();
            OrganizeOutputDto OrganizeOutputDto = new OrganizeOutputDto();
            var ChilList = data.FindAll(t => t.ParentId == parentId);
            foreach (Organize entity in ChilList)
            {
                OrganizeOutputDto = entity.MapTo<OrganizeOutputDto>();
                OrganizeOutputDto.Children = GetSubOrganizes(data, entity.Id).OrderBy(t => t.SortCode).MapTo<OrganizeOutputDto>();
                list.Add(OrganizeOutputDto);
            }
            return list;
        }

        /// <summary>
        /// 獲取父集，遞歸調用
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parentId">父級Id</param>
        /// <returns></returns>
        private List<OrganizeOutputDto> GetParentOrganizes(List<Organize> data, string parentId)
        {
            List<OrganizeOutputDto> list = new List<OrganizeOutputDto>();
            OrganizeOutputDto OrganizeOutputDto = new OrganizeOutputDto();
            var parentList = data.FindAll(t => t.Id == parentId);
            foreach (Organize entity in parentList) {
                OrganizeOutputDto = entity.MapTo<OrganizeOutputDto>();
                OrganizeOutputDto.Children = GetParentOrganizes(data, entity.ParentId).OrderBy(t => t.SortCode).MapTo<OrganizeOutputDto>();
                list.Add(OrganizeOutputDto);
            }
            return list;
        }

        /// <summary>
        /// 獲取根節點組織
        /// </summary>
        /// <param name="id">組織Id</param>
        /// <returns></returns>
        public Organize GetRootOrganize(string id)
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
                    IEnumerable<Organize> list = _repository.GetListWhere(where);
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
                    IEnumerable<Organize> list = _repository.GetListWhere(where);
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