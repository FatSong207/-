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
using System.Data;
using Yuebon.Commons.IDbContext;
using System;
using Org.BouncyCastle.Bcpg;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 待辦事項服務接口實現
    /// </summary>
    public class ToDoListService: BaseService<ToDoList,ToDoListOutputDto, string>, IToDoListService
    {
		private readonly IToDoListRepository _repository;
        private readonly IDbContextCore ybContext;

        public ToDoListService(IToDoListRepository repository, IDbContextCore _ybContext) : base(repository)
        {
			_repository=repository;
            ybContext = _ybContext;
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public override async Task<PageResult<ToDoListOutputDto>> FindWithPagerAsync(SearchInputDto<ToDoList> search, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = string.Format(" Account = '{0}' and Status = '待審核'", search.Keywords);
            var filter = search.Filter;

            if (filter != null) {
                // 待辦事項名稱
                if (!string.IsNullOrWhiteSpace(filter.Name))
                    where += string.Format(" and Name like '%{0}%'", filter.Name);

                // 功能類型
                if (!string.IsNullOrWhiteSpace(filter.Type))
                    where += string.Format(" and Type like '%{0}%'", filter.Type);

                // 狀態
                if (!string.IsNullOrWhiteSpace(filter.Status))
                    where += string.Format(" and Status like '%{0}%'", filter.Status);
            }

            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<ToDoList> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);

            PageResult<ToDoListOutputDto> pageResult = new PageResult<ToDoListOutputDto>
            {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = list.MapTo<ToDoListOutputDto>(),
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };

            return pageResult;
        }

        /// <summary>
        /// 更新待辧事項狀態
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="account">審核者Id</param>
        /// <param name="typeId">功能類型ID</param>
        /// <param name="status">當前狀態</param>
        /// <returns></returns> 
        public async Task<bool> UpdateToDoListStatus(string userId, string account, string typeId, string status, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool result = false;
            ToDoList info = await _repository.GetWhereAsync($"Account = '{account}' AND TypeId = '{typeId}'", conn, trans);

            if (info != null) {
                info.Status = status;
                info.LastModifyTime = DateTime.Now;
                info.LastModifyUserId = userId;

                result = await repository.UpdateAsync(info, info.Id, conn, trans);
            }

            return result;

            //int count = _repository.UpdateBySql($"UPDATE Chaochi_ToDoList SET Status = '{status}',LastModifyTime = GETDATE(), LastModifyUserId = '{userId}' WHERE Account = '{account}' AND TypeId = '{typeId}'");

            //return count > 0;
        }

        /// <summary>
        /// 根據功能模組ID查詢待辦事項
        /// </summary>
        /// <param name="typeId">功能模組ID</param>
        /// <returns></returns>
        public async Task<ToDoListOutputDto> GetByTypeID(string typeId)
        {
            ToDoListOutputDto outputDto = null;
            ToDoList info = await _repository.GetWhereAsync(string.Format(" TypeId like '%{0}%'", typeId));
            if (info != null) {
                outputDto = info.MapTo<ToDoListOutputDto>();
            }

            return outputDto;
        }

        /// <summary>
        /// 根據功能模組ID和功能模組資料查詢待辦事項
        /// </summary>
        /// <param name="typeId">功能模組ID</param>
        /// <param name="typeData">功能模組資料</param>
        /// <returns></returns>
        public async Task<ToDoListOutputDto> GetByTypeData(string typeId, string typeData)
        {
            ToDoListOutputDto outputDto = null;
            ToDoList info = await _repository.GetWhereAsync(string.Format(" TypeId = '{0}' AND TypeData = '{1}'", typeId, typeData));
            if (info != null) {
                outputDto = info.MapTo<ToDoListOutputDto>();
            }

            return outputDto;
        }

        public static string getCID(string typeId)
        {
            string cid = string.Empty;
            typeId = typeId.TrimEnd(new char[] { '-' });
            string[] result = typeId.Split('-');
            if (result.Length >= 0) {
                cid = result[0] + '-' + result[1];
            }

            return cid;
        }
    }
}