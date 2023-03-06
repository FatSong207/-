using System;
using System.Data.Common;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Repositories;
using Yuebon.Commons.Services;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Models;
using System.Data;
using Yuebon.Chaochi.Dtos;
using System.Collections.Generic;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Mapping;
using System.Threading.Tasks;
using Yuebon.Commons.Extend;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Enums;
using Yuebon.Chaochi.Core.Dtos;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerLCService : BaseService<CustomerLC, CustomerLCOutputDto, string>, ICustomerLCService
    {
        private readonly ICustomerLCRepository _repository;
        private readonly Security.IServices.IUserService _userService;
        private readonly Security.IRepositories.IUserLogOnRepository _userSigninRepository;
        private readonly Security.IServices.ILogService _logService;
        private readonly Security.IServices.IRoleService _roleService;
        private Security.IServices.IOrganizeService _organizeService;
        private readonly ILandLordBelongingService _landLordBelongingService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="userLogOnRepository"></param>
        /// <param name="logService"></param>
        /// <param name="roleService"></param>
        /// <param name="organizeService"></param>
        public CustomerLCService(ICustomerLCRepository repository, Yuebon.Security.IServices.IUserService userService, ILandLordBelongingService landLordBelongingService, Security.IRepositories.IUserLogOnRepository userLogOnRepository, Security.IServices.ILogService logService, Security.IServices.IRoleService roleService, Security.IServices.IOrganizeService organizeService) : base(repository)
        {
            _repository = repository;
            _userService = userService;
            _userSigninRepository = userLogOnRepository;
            _logService = logService;
            _roleService = roleService;
            _organizeService = organizeService;
            _landLordBelongingService = landLordBelongingService;
        }

        /// <summary>
        /// 新增存取權
        /// </summary>
        /// <param name="customerLN"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> InsertAccess(CustomerLC customerLC, string userId, string userDeptId)
        {
            return await _repository.UpdateTableFieldAsync("CreatorUserId", $"{customerLC.CreatorUserId},{userId}", $"Id='{customerLC.Id}'") &&
            await _repository.UpdateTableFieldAsync("CreatorUserDeptId", $"{customerLC.CreatorUserDeptId},{userDeptId}", $"Id='{customerLC.Id}'");
        }

        /// <summary>
        /// 根據出租人地址查詢姓名
        /// </summary>
        /// <param name="addr">建物地址</param>
        /// <returns></returns>
        public async Task<CustomerLC> GetNameByAddr(string addr)
        {
            return await _repository.GetWhereAsync($"LCAdd = N'{addr}'");
        }

        /// <summary>
        /// 根據房東法人統一編號查詢
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<CustomerLC> GetByLCID(string LCID, IDbConnection conn, IDbTransaction tran)
        {
            return await _repository.GetByLCID(LCID, conn, tran);
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public async Task<PageResult<CustomerLCOutputDto>> FindWithPagerSearchAsync(SearchCustomerLCModel search)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(true);
            var filter = search.Filter;

            if (filter is not null) {
                if (!string.IsNullOrEmpty(filter.LCAdd))
                    where += $" and LCAdd like '%{filter.LCAdd}%'";
                if (!string.IsNullOrEmpty(filter.LCName))
                    where += $" and LCName like '%{filter.LCName}%'";
                if (!string.IsNullOrEmpty(filter.LCRep))
                    where += $" and LCRep like '%{filter.LCRep}%'";
                if (!string.IsNullOrEmpty(filter.LCID))
                    where += $" and LCID like '%{filter.LCID}%'";
                if (!string.IsNullOrEmpty(filter.LCTel_1))
                    where += $" and LCTel_1 like '%{filter.LCTel_1}%'";
                if (!string.IsNullOrEmpty(filter.LCTel_2))
                    where += $" and LCTel_2 like '%{filter.LCTel_2}%'";
                if (!string.IsNullOrEmpty(filter.CreatorUserId)) {
                    where += $" and  CreatorUserId like '%{filter.CreatorUserId}%'";
                }
            }

            PagerInfo pagerInfo = new PagerInfo {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<CustomerLC> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            List<CustomerLCOutputDto> resultList = list.MapTo<CustomerLCOutputDto>();
            List<CustomerLCOutputDto> listResult = new List<CustomerLCOutputDto>();
            foreach (CustomerLCOutputDto item in resultList) {
                if (item.CreatorUserId.Contains(",")) {
                    var CreatorUserNames = "";
                    foreach (var item2 in item.CreatorUserId.Split(",")) {
                        CreatorUserNames += _userService.Get(item2).RealName + ",";
                    }
                    item.CreatorUserName = CreatorUserNames;
                } else {
                    item.CreatorUserName = _userService.Get(item.CreatorUserId).RealName;
                }
                listResult.Add(item);
            }
            PageResult<CustomerLCOutputDto> pageResult = new PageResult<CustomerLCOutputDto> {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = listResult,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }

        public void Insert(CustomerLC customerLC, string currentUserId)
        {
            _repository.Insert(customerLC);
            _landLordBelongingService.Insert(new LandLordBelonging {
                LId = customerLC.LCID,
                SalesId = currentUserId,
            });
        }
    }
}