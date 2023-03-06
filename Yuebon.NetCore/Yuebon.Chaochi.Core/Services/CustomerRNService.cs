using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Yuebon.Chaochi.Core.Dtos;
using Yuebon.Chaochi.Core.IRepositories;
using Yuebon.Chaochi.Core.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using Yuebon.Commons.Core.Dtos;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Services;
using Yuebon.Security.IServices;

namespace Yuebon.Chaochi.Core.Services
{
    public class CustomerRNService : BaseService<CustomerRN, CustomerRNOutputDto, string>, ICustomerRNService
    {
        private readonly ICustomerRNRepository _repository;
        private readonly IUserService _userService;
        private readonly ILogService _logService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logService"></param>
        public CustomerRNService(ICustomerRNRepository repository,IUserService userService, ILogService logService) : base(repository)
        {
            _repository = repository;
            _userService = userService;
            _logService = logService;
        }

        /// <summary>
        /// 新增存取權
        /// </summary>
        /// <param name="customerLN"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> InsertAccess(CustomerRN customerRN, string userId, string userDeptId)
        {
            return await _repository.UpdateTableFieldAsync("CreatorUserId", $"{customerRN.CreatorUserId},{userId}", $"Id='{customerRN.Id}'") &&
            await _repository.UpdateTableFieldAsync("CreatorUserDeptId", $"{customerRN.CreatorUserDeptId},{userDeptId}", $"Id='{customerRN.Id}'");
        }

        /// <summary>
        /// 根據用戶帳號查詢用戶信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<CustomerRN> GetCustomerByRNID(string RNID)
        {
            //CustomerRNOutputDto outputDto = new CustomerRNOutputDto();
            CustomerRN result = await _repository.GetCustomerByRNID(RNID);
            //outputDto = result.MapTo<CustomerRNOutputDto>();

            return result;
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public async Task<PageResult<CustomerRNOutputDto>> FindWithPagerSearchAsync(SearchCustomerRNModel search)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(true);
            var filter = search.Filter;


            if (filter.RNQualify1C == "/Yes") {
                where += $" and RNQualify1C = '/Yes' ";
            }
            if (filter.RNQualify2C == "/Yes") {
                where += $" and RNQualify2C = '/Yes' ";
            }
            if (filter.RNQualify3C == "/Yes") {
                where += $" and RNQualify3C = '/Yes' ";
            }
            if (filter.RNQualify1C == "/Off" && filter.RNQualify2C == "/Off" && filter.RNQualify3C == "/Off") {
                where += $" and ( ( RNQualify1C = '/Off' and RNQualify2C = '/Off' and RNQualify3C = '/Off' ) or ( RNQualify1C is null and  RNQualify2C is null and RNQualify3C is null ) ) ";
            }
            if (!string.IsNullOrEmpty(filter.RNAdd)) 
                where += $" and RNAdd like '%{filter.RNAdd}%' ";
            if (!string.IsNullOrEmpty(filter.RNName))
                where += string.Format(" and RNName like '%{0}%' ", filter.RNName);
            if (!string.IsNullOrEmpty(filter.RNID))
                where += string.Format(" and RNID like '%{0}%'", filter.RNID);
            if (!string.IsNullOrEmpty(filter.RNBirthday))
                where += string.Format(" and RNBirthday like '%{0}%'", filter.RNBirthday);
            if (!string.IsNullOrEmpty(filter.RNCell))
                where += string.Format(" and RNCell like '%{0}%'", filter.RNCell);
            if (!string.IsNullOrEmpty(filter.RNTel_1))
                where += string.Format(" and RNTel_1 like '%{0}%'", filter.RNTel_1);
            if (!string.IsNullOrEmpty(filter.RNTel_2))
                where += string.Format(" and RNTel_2 like '%{0}%'", filter.RNTel_2);
            if (!string.IsNullOrEmpty(filter.RNMail)) {
                where += string.Format(" and RNMail like '%{0}%' ", filter.RNMail);
            }
            if (!string.IsNullOrEmpty(filter.CreatorUserId)) {
                    where += $" and  CreatorUserId like '{filter.CreatorUserId}'";
            }
            if (!string.IsNullOrEmpty(search.CreatorTime1))
                where += " and CreatorTime >='" + search.CreatorTime1 + " 00:00:00'";
            if (!string.IsNullOrEmpty(search.CreatorTime2)) {
                where += " and CreatorTime <='" + search.CreatorTime2 + " 23:59:59'";
            }
            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<CustomerRN> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            List<CustomerRNOutputDto> resultList = list.MapTo<CustomerRNOutputDto>();
            List<CustomerRNOutputDto> listResult = new List<CustomerRNOutputDto>();
            foreach (CustomerRNOutputDto item in resultList) {
                if (item.CreatorUserId.Contains(",")) {
                    var CreatorUserNames = "";
                    foreach (var item2 in item.CreatorUserId.Split(",")) {
                        var user = _userService.Get(item2);
                        CreatorUserNames += (user != null) ? user.RealName + "," : $"{item2}(帳號己不存在)" + ",";
                    }
                    item.CreatorUserName = CreatorUserNames;
                } else {
                    var user = _userService.Get(item.CreatorUserId);
                    item.CreatorUserName = (user != null) ? user.RealName : $"{item.CreatorUserId}(帳號己不存在)";
                }
                listResult.Add(item);
            }
            PageResult<CustomerRNOutputDto> pageResult = new PageResult<CustomerRNOutputDto>
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
            string where = string.Empty;
            for (int i = 0; i < idsInfo.Ids.Length; i++) {
                if (idsInfo.Ids[0].ToString().Length > 0) {
                    where = string.Format("ParentId='{0}'", idsInfo.Ids[0]);
                    IEnumerable<CustomerRN> list = _repository.GetListWhere(where);
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

        /// <summary>
        /// 覆寫GetOutDtoAsync(因為家庭成員記錄在RNFOoutputDto)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<CustomerRNOutputDto> GetOutDtoAsync(string id)
        {
            var info = await repository.GetAsync(id);
            var outinfo = info.MapTo<CustomerRNOutputDto>();
            outinfo.RNFOutputDto = info.MapTo<RNFOutputDto>();
            return outinfo;
        }
    }
}
