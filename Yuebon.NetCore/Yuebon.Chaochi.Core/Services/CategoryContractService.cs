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
    /// 契約分類
    /// </summary>
    public class CategoryContractService : BaseService<CategoryContract, CategoryContractOutputDto, string>, ICategoryContractService
    {
        private readonly ICategoryContractRepository _repository;
        private readonly ILogService _logService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logService"></param>
        public CategoryContractService(ICategoryContractRepository repository, ILogService logService) : base(repository)
        {
            _repository = repository;
            _logService = logService;
        }


        /// <summary>
        /// 獲取契約分類適用于Vue 樹形列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<CategoryContractOutputDto>> GetAllCategoryTreeTable()
        {
            List<CategoryContractOutputDto> reslist = new List<CategoryContractOutputDto>();
            IEnumerable<CategoryContract> elist = await _repository.GetAllByIsNotDeleteAndEnabledMarkAsync();
            List<CategoryContract> list = elist.OrderBy(t => t.Name).ToList();
            List<CategoryContract> oneMenuList = list.FindAll(t => t.ParentId == "");
            foreach (CategoryContract item in oneMenuList) {
                CategoryContractOutputDto menuTreeTableOutputDto = new CategoryContractOutputDto();
                try {
                    menuTreeTableOutputDto = item.MapTo<CategoryContractOutputDto>();
                    menuTreeTableOutputDto.Children = GetSubCategory(list, item.Id).ToList<CategoryContractOutputDto>();
                } catch (Exception e) {
                    Console.Write(e.Message);
                }
                reslist.Add(menuTreeTableOutputDto);
            }

            return reslist;
        }

        /// <summary>
        /// 獲取子節點分類
        /// </summary>
        /// <param name="parentId">父節點分類Id</param>
        /// <returns></returns>
        public async Task<List<CategoryContractOutputDto>> GetByParent(string parentId)
        {
            List<CategoryContractOutputDto> list = new List<CategoryContractOutputDto>();
            List<CategoryContract> resultList = await _repository.GetCategoryByParent(parentId);
            foreach (CategoryContract entity in resultList) {
                CategoryContractOutputDto outputDto = entity.MapTo<CategoryContractOutputDto>();
                list.Add(outputDto);
            }

            return list;
        }


        /// <summary>
        /// 獲取子集，遞迴調用
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parentId">父層Id</param>
        /// <returns></returns>
        private List<CategoryContractOutputDto> GetSubCategory(List<CategoryContract> data, string parentId)
        {
            List<CategoryContractOutputDto> list = new List<CategoryContractOutputDto>();
            CategoryContractOutputDto CategoryContractOutputDto = new CategoryContractOutputDto();
            var ChilList = data.FindAll(t => t.ParentId == parentId);
            foreach (CategoryContract entity in ChilList) {
                CategoryContractOutputDto = entity.MapTo<CategoryContractOutputDto>();
                CategoryContractOutputDto.Children = GetSubCategory(data, entity.Id).OrderBy(t => t.Name).MapTo<CategoryContractOutputDto>();
                list.Add(CategoryContractOutputDto);
            }
            return list;
        }

        /// <summary>
        /// 獲取根節點分類
        /// </summary>
        /// <param name="id">組織Id</param>
        /// <returns></returns>
        public CategoryContract GetRootCategory(string id)
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
                    IEnumerable<CategoryContract> list = _repository.GetListWhere(where);
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
                    IEnumerable<CategoryContract> list = _repository.GetListWhere(where);
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
