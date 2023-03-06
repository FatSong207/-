using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Services;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using Yuebon.Security.IServices;
using Yuebon.Commons.Core.Dtos;
using System.Data;
using Yuebon.Commons.Models;
using System.Linq;
using Yuebon.Commons.Extensions;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 服務接口實現
    /// </summary>
    public class ReceiptService: BaseService<Receipt,ReceiptOutputDto, string>, IReceiptService
    {
		private readonly IReceiptRepository _repository;
        private readonly ILogService _logService;

        public ReceiptService(IReceiptRepository repository, ILogService logService) : base(repository)
        {
			_repository = repository;
            _logService = logService;
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public async Task<PageResult<ReceiptOutputDto>> FindWithPagerSearchAsync(SearchReceiptModel search)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(true);
            var filter = search.Filter;

            if(filter != null) {
                if (!string.IsNullOrEmpty(filter.RPayee))
                    where += string.Format(" and RPayee like '%{0}%'", filter.RPayee);
                if (!string.IsNullOrEmpty(filter.RPayeeName))
                    where += string.Format(" and RPayeeName like '%{0}%'", filter.RPayeeName);
                if (!string.IsNullOrEmpty(filter.RPayeeID))
                    where += string.Format(" and RPayeeID like '%{0}%'", filter.RPayeeID);
                if (!string.IsNullOrEmpty(filter.RPayeeTaxID))
                    where += string.Format(" and RPayeeTaxID like '%{0}%'", filter.RPayeeTaxID);
                if (!string.IsNullOrEmpty(filter.RPayeeUnit))
                    where += string.Format(" and RPayeeUnit like '%{0}%'", filter.RPayeeUnit);

                if (!string.IsNullOrEmpty(filter.RPayer))
                    where += string.Format(" and RPayer like '%{0}%'", filter.RPayer);
                if (!string.IsNullOrEmpty(filter.RPayerName))
                    where += string.Format(" and RPayerName like '%{0}%'", filter.RPayerName);
                if (!string.IsNullOrEmpty(filter.RPayerID))
                    where += string.Format(" and RPayerID like '%{0}%'", filter.RPayerID);
                if (!string.IsNullOrEmpty(filter.RPayerTaxID))
                    where += string.Format(" and RPayerTaxID like '%{0}%'", filter.RPayerTaxID);
                if (!string.IsNullOrEmpty(filter.RPayerUnit))
                    where += string.Format(" and RPayerUnit like '%{0}%'", filter.RPayerUnit);

                if (!string.IsNullOrEmpty(filter.RAdd_1))
                    where += string.Format(" and RAdd_1 like '%{0}%'", filter.RAdd_1);
                if (!string.IsNullOrEmpty(filter.RAdd_2))
                    where += string.Format(" and RAdd_2 like '%{0}%'", filter.RAdd_2);
                if (!string.IsNullOrEmpty(filter.RAdd_3))
                    where += string.Format(" and RAdd_3 like '%{0}%'", filter.RAdd_3);
                if (!string.IsNullOrEmpty(filter.RAdd_4))
                    where += string.Format(" and RAdd_4 like '%{0}%'", filter.RAdd_4);
                if (!string.IsNullOrEmpty(filter.RAdd_5))
                    where += string.Format(" and RAdd_5 like '%{0}%'", filter.RAdd_5);
                if (!string.IsNullOrEmpty(filter.RAdd_6))
                    where += string.Format(" and RAdd_6 like '%{0}%'", filter.RAdd_6);
                if (!string.IsNullOrEmpty(filter.RAdd_7))
                    where += string.Format(" and RAdd_7 like '%{0}%'", filter.RAdd_7);
                if (!string.IsNullOrEmpty(filter.RAdd_8))
                    where += string.Format(" and RAdd_8 like '%{0}%'", filter.RAdd_8);
                if (!string.IsNullOrEmpty(filter.RAdd_9))
                    where += string.Format(" and RAdd_9 like '%{0}%'", filter.RAdd_9);

                if (!string.IsNullOrEmpty(filter.RCate))
                    where += string.Format(" and RCate like '%{0}%' ", filter.RCate);
                if (!string.IsNullOrEmpty(filter.RStatus))
                    where += string.Format(" and RStatus like '%{0}%' ", filter.RStatus);
                if (!string.IsNullOrEmpty(filter.ReceiptId))
                    where += string.Format(" and RID like '%{0}%' ", filter.ReceiptId);
                if (!string.IsNullOrEmpty(filter.ROverBooking))
                    where += string.Format(" and ROverBooking like '%{0}%' ", filter.ROverBooking);
            }

            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<Receipt> list = await _repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            List<ReceiptOutputDto> resultList = list.MapTo<ReceiptOutputDto>();
            List<ReceiptOutputDto> listResult = new List<ReceiptOutputDto>();
            foreach (ReceiptOutputDto item in resultList) {
                listResult.Add(item);
            }
            PageResult<ReceiptOutputDto> pageResult = new PageResult<ReceiptOutputDto>
            {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = listResult,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
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
            string where = "id in ('" + idsInfo.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            bool bl = await _repository.DeleteBatchWhereAsync(where);
            if (bl) {
                result.ErrCode = "0";
            }
            return result;
        }

        /// <summary>
        /// 異步設定領收據狀態
        /// </summary>
        /// <param name="status">狀態</param>
        /// <param name="receiptId">領收據編號</param>
        /// <param name="userId">使用者ID</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public async Task<bool> SetReceiptStatusAsync(string status, string receiptId, string userId = null, IDbTransaction trans = null)
        {           
            return await _repository.SetReceiptStatusAsync(status, receiptId, userId, trans); ;
        }

        /// <summary>
        /// 根據領收據編號查詢收據編資料
        /// </summary>
        /// <param name="receiptId"></param>
        /// <returns></returns> 
        public async Task<Receipt> GetReceiptById(string receiptId)
        {
            return await _repository.GetReceiptById(receiptId);
        }
    }
}