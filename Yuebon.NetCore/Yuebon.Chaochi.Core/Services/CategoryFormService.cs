using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Yuebon.Chaochi.Core.Dtos;
using Yuebon.Chaochi.Core.IRepositories;
using Yuebon.Chaochi.Core.IServices;
using Yuebon.Chaochi.Core.Models;
using Yuebon.Commons.Core.Dtos;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;
using Yuebon.Commons.Services;
using Yuebon.Security.IServices;

namespace Yuebon.Chaochi.Core.Services
{
    /// <summary>
    /// 表單分類
    /// </summary>
    public class CategoryFormService : BaseService<CategoryForm, CategoryFormOutputDto, string>, ICategoryFormService
    {
        private readonly ICategoryFormRepository _repository;
        private readonly ILogService _logService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logService"></param>
        public CategoryFormService(ICategoryFormRepository repository, ILogService logService) : base(repository)
        {
            _repository = repository;
            _logService = logService;
        }


        /// <summary>
        /// 獲取表單分類適用于Vue 樹形列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<CategoryFormOutputDto>> GetAllCategoryTreeTable()
        {
            List<CategoryFormOutputDto> reslist = new List<CategoryFormOutputDto>();
            IEnumerable<CategoryForm> elist = await _repository.GetAllByIsNotDeleteAndEnabledMarkAsync();
            List<CategoryForm> list = elist.OrderBy(t => t.SortOrder).ToList();
            List<CategoryForm> oneMenuList = list.FindAll(t => t.ParentId == "");
            foreach (CategoryForm item in oneMenuList) {
                CategoryFormOutputDto menuTreeTableOutputDto = new CategoryFormOutputDto();
                try { 
                menuTreeTableOutputDto = item.MapTo<CategoryFormOutputDto>();
                menuTreeTableOutputDto.Children = GetSubCategory(list, item.Id).ToList<CategoryFormOutputDto>();
                } catch(Exception e) {
                    Console.Write(e.Message);
                }
                reslist.Add(menuTreeTableOutputDto);
            }

            return reslist;
        }


        /// <summary>
        /// 獲取子集，遞迴調用
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parentId">父層Id</param>
        /// <returns></returns>
        private List<CategoryFormOutputDto> GetSubCategory(List<CategoryForm> data, string parentId)
        {
            List<CategoryFormOutputDto> list = new List<CategoryFormOutputDto>();
            CategoryFormOutputDto CategoryFormOutputDto = new CategoryFormOutputDto();
            var ChilList = data.FindAll(t => t.ParentId == parentId);
            foreach (CategoryForm entity in ChilList) {
                CategoryFormOutputDto = entity.MapTo<CategoryFormOutputDto>();
                CategoryFormOutputDto.Children = GetSubCategory(data, entity.Id).OrderBy(t => t.SortOrder).MapTo<CategoryFormOutputDto>();
                list.Add(CategoryFormOutputDto);
            }
            return list;
        }

        /// <summary>
        /// 獲取根節點分類
        /// </summary>
        /// <param name="id">組織Id</param>
        /// <returns></returns>
        public CategoryForm GetRootCategory(string id)
        {
            return _repository.GetRootCategory(id);
        }


        /// <summary>
        /// 按條件批次刪除
        /// </summary>
        /// <param name="idsInfo">主鍵Id集合</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public CommonResult DeleteBatchWhere(DeletesInputDto idsInfo, IDbTransaction trans = null)
        {
            CommonResult result = new CommonResult();
            string where = string.Empty;
            for (int i = 0; i < idsInfo.Ids.Length; i++) {
                if (idsInfo.Ids[0] != null) {
                    where = string.Format("ParentId='{0}'", idsInfo.Ids[0]);
                    IEnumerable<CategoryForm> list = _repository.GetListWhere(where);
                    if (list.Count() > 0) {
                        result.ErrMsg = "功能存在子集數據，不能刪除";
                        return result;
                    }
                }
            }
            where = "id in ('" + idsInfo.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            bool bl = repository.DeleteBatchWhere(where);
            if (bl) {
                result.ErrCode = "0";
            }
            return result;
        }

        /// <summary>
        /// 按條件批次刪除
        /// </summary>
        /// <param name="idsInfo">主鍵Id集合</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public async Task<CommonResult> DeleteBatchWhereAsync(DeletesInputDto idsInfo, IDbTransaction trans = null)
        {
            CommonResult result = new CommonResult();
            string where = string.Empty;
            for (int i = 0; i < idsInfo.Ids.Length; i++) {
                if (idsInfo.Ids[0].ToString().Length > 0) {
                    where = string.Format("ParentId='{0}'", idsInfo.Ids[0]);
                    IEnumerable<CategoryForm> list = _repository.GetListWhere(where);
                    if (list.Count() > 0) {
                        result.ErrMsg = "該表單分類已存在於子集資料，不能刪除";
                        return result;
                    }
                }
            }
            where = "id in ('" + idsInfo.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            bool bl = await repository.DeleteBatchWhereAsync(where);
            if (bl) {
                result.ErrCode = "0";
            }
            return result;
        }
    }
}
