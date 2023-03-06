using Yuebon.Commons.Services;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.Models;
using System.Data;
using Yuebon.Chaochi.Dtos;
using System.Collections.Generic;
using Yuebon.Commons.Mapping;
using System.Threading.Tasks;
using Yuebon.Commons.Pages;
using Yuebon.Chaochi.IServices;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerLNService : BaseService<CustomerLN, CustomerLNOutputDto, string>, ICustomerLNService
    {
        private readonly ICustomerLNRepository _userRepository;
        private readonly Yuebon.Security.IServices.IUserService _userService;
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
        public CustomerLNService(ICustomerLNRepository repository, Yuebon.Security.IServices.IUserService userService, ILandLordBelongingService landLordBelongingService, Security.IRepositories.IUserLogOnRepository userLogOnRepository, Security.IServices.ILogService logService, Security.IServices.IRoleService roleService, Security.IServices.IOrganizeService organizeService) : base(repository)
        {
            _userRepository = repository;
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
        public async Task<bool> InsertAccess(CustomerLN customerLN,string userId,string userDeptId)
        {
            return await _userRepository.UpdateTableFieldAsync("CreatorUserId",$"{customerLN.CreatorUserId},{userId}",$"Id='{customerLN.Id}'") &&
            await _userRepository.UpdateTableFieldAsync("CreatorUserDeptId", $"{customerLN.CreatorUserDeptId},{userDeptId}", $"Id='{customerLN.Id}'");
        }

        /// <summary>
        /// 根據出租人地址查詢姓名
        /// </summary>
        /// <param name="addr">建物地址</param>
        /// <returns></returns>
        public async Task<CustomerLN> GetNameByAddr(string addr)
        {
            return await _userRepository.GetWhereAsync($"LNAdd = N'{addr}'");
        }

        /// <summary>
        /// 根據房東自然人身份證字號查詢
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<CustomerLN> GetByCustomerLNID(string LNID, IDbConnection conn = null, IDbTransaction tran = null)
        {
            return await _userRepository.GetByCustomerLNID(LNID,conn,tran);
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public async Task<PageResult<CustomerLNOutputDto>> FindWithPagerSearchAsync(SearchCustomerLNModel search)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(true);
            var filter = search.Filter;

            //if(filter.LNAdd_1_1=="/Yes") 
            //    where += " and LNAdd_1_1='/Yes'";
            //if(filter.LNAdd_1_2 == "/Yes") 
            //    where += " and LNAdd_1_2='/Yes'";
            //if(filter.LNAdd_2_1 == "/Yes") 
            //    where += " and LNAdd_2_1='/Yes'";
            //if(filter.LNAdd_2_2 == "/Yes") 
            //    where += " and LNAdd_2_2='/Yes'";
            //if(filter.LNAdd_2_3 == "/Yes") 
            //    where += " and LNAdd_2_3='/Yes'";
            //if(filter.LNAdd_2_4 == "/Yes") 
            //    where += " and LNAdd_2_4='/Yes'";
            //if(filter.LNAdd_3_1 == "/Yes") 
            //    where += " and LNAdd_3_1='/Yes'";
            //if(filter.LNAdd_3_2 == "/Yes") 
            //    where += " and LNAdd_3_2='/Yes'";
            if (!string.IsNullOrEmpty(filter.LNAdd))
                where += $" and LNAdd like '%{filter.LNAdd}%' ";
            if (!string.IsNullOrEmpty(filter.LNName))
                where += string.Format(" and LNName like '%{0}%' ", filter.LNName);
            if (!string.IsNullOrEmpty(filter.LNID))
                where += string.Format(" and LNID like '%{0}%'", filter.LNID);
            if (!string.IsNullOrEmpty(filter.LNBirthday))
                where += string.Format(" and LNBirthday like '%{0}%'", filter.LNBirthday);
            if (!string.IsNullOrEmpty(filter.LNCell))
                where += string.Format(" and LNCell like '%{0}%'", filter.LNCell);
            if (!string.IsNullOrEmpty(filter.LNTel_1))
                where += string.Format(" and LNTel_1 like '%{0}%'", filter.LNTel_1);
            if (!string.IsNullOrEmpty(filter.LNTel_2))
                where += string.Format(" and LNTel_2 like '%{0}%'", filter.LNTel_2);
            if (!string.IsNullOrEmpty(filter.CreatorUserId)) {
                //var user = await _userService.GetWhereAsync($" RealName  = '{filter.CreatorUserId}'");
                //if (user != null)
                    where += $" and  CreatorUserId like '%{filter.CreatorUserId}%'";
                //else
                //    where += " and 1=0 ";
            }
            //if(!string.IsNullOrEmpty(filter.LNAdd_1))
            //    where += string.Format(" and LNAdd_1 like '%{0}%'", filter.LNAdd_1);
            //if(!string.IsNullOrEmpty(filter.LNAdd_2))
            //    where += string.Format(" and LNAdd_2 like '%{0}%'", filter.LNAdd_2);
            //if(!string.IsNullOrEmpty(filter.LNAdd_3))
            //    where += string.Format(" and LNAdd_3 like '%{0}%'", filter.LNAdd_3);
            //if(!string.IsNullOrEmpty(filter.LNAdd_4))
            //    where += string.Format(" and LNAdd_4 like '%{0}%'", filter.LNAdd_4);
            //if(!string.IsNullOrEmpty(filter.LNAdd_5))
            //    where += string.Format(" and LNAdd_5 like '%{0}%'", filter.LNAdd_5);
            //if(!string.IsNullOrEmpty(filter.LNAdd_6))
            //    where += string.Format(" and LNAdd_6 like '%{0}%'", filter.LNAdd_6);
            //if(!string.IsNullOrEmpty(filter.LNAdd_7))
            //    where += string.Format(" and LNAdd_7 like '%{0}%'", filter.LNAdd_7);
            //if(!string.IsNullOrEmpty(filter.LNAdd_8))
            //    where += string.Format(" and LNAdd_8 like '%{0}%'", filter.LNAdd_8);
            //if(!string.IsNullOrEmpty(filter.LNAdd_9))
            //    where += string.Format(" and LNAdd_8 like '%{0}%'", filter.LNAdd_9);


            if (!string.IsNullOrEmpty(search.CreatorTime1))
                where += " and CreatorTime >='" + search.CreatorTime1 + " 00:00:00'";
            if (!string.IsNullOrEmpty(search.CreatorTime2)) {
                where += " and CreatorTime <='" + search.CreatorTime2 + " 23:59:59'";
            }
            PagerInfo pagerInfo = new PagerInfo {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<CustomerLN> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            List<CustomerLNOutputDto> resultList = list.MapTo<CustomerLNOutputDto>();
            List<CustomerLNOutputDto> listResult = new List<CustomerLNOutputDto>();
            foreach (CustomerLNOutputDto item in resultList) {
                if (item.CreatorUserId.Contains(",")) {
                    var CreatorUserNames = "";
                    foreach (var item2 in item.CreatorUserId.Split(",")) {
                        CreatorUserNames +=_userService.Get(item2).RealName+",";
                    }
                    item.CreatorUserName = CreatorUserNames;
                } else {
                    item.CreatorUserName = _userService.Get(item.CreatorUserId).RealName;
                }
                listResult.Add(item);
            }
            PageResult<CustomerLNOutputDto> pageResult = new PageResult<CustomerLNOutputDto> {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = listResult,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }

        public override Task<long> InsertAsync(CustomerLN customerLN, IDbConnection conn = null, IDbTransaction trans = null)
        {
            if (customerLN.LNAddSame == "/Yes") {
                customerLN.LNAddC = customerLN.LNAdd;
                customerLN.LNAddC_1 = customerLN.LNAdd_1;
                customerLN.LNAddC_1_1 = customerLN.LNAdd_1_1;
                customerLN.LNAddC_1_2 = customerLN.LNAdd_1_2;
                customerLN.LNAddC_2 = customerLN.LNAdd_2;
                customerLN.LNAddC_2_1 = customerLN.LNAdd_2_1;
                customerLN.LNAddC_2_2 = customerLN.LNAdd_2_2;
                customerLN.LNAddC_2_3 = customerLN.LNAdd_2_3;
                customerLN.LNAddC_2_4 = customerLN.LNAdd_2_4;
                customerLN.LNAddC_3 = customerLN.LNAdd_3;
                customerLN.LNAddC_3_1 = customerLN.LNAdd_3_1;
                customerLN.LNAddC_3_2 = customerLN.LNAdd_3_2;
                customerLN.LNAddC_4 = customerLN.LNAdd_4;
                customerLN.LNAddC_5 = customerLN.LNAdd_5;
                customerLN.LNAddC_6 = customerLN.LNAdd_6;
                customerLN.LNAddC_7 = customerLN.LNAdd_7;
                customerLN.LNAddC_8 = customerLN.LNAdd_8;
                customerLN.LNAddC_9 = customerLN.LNAdd_9;
            }
            return repository.InsertAsync(customerLN);
        }

        public void Insert(CustomerLN customerLN, string currentUserId)
        {
            if (customerLN.LNAddSame == "/Yes") {
                customerLN.LNAddC = customerLN.LNAdd;
                customerLN.LNAddC_1 = customerLN.LNAdd_1;
                customerLN.LNAddC_1_1 = customerLN.LNAdd_1_1;
                customerLN.LNAddC_1_2 = customerLN.LNAdd_1_2;
                customerLN.LNAddC_2 = customerLN.LNAdd_2;
                customerLN.LNAddC_2_1 = customerLN.LNAdd_2_1;
                customerLN.LNAddC_2_2 = customerLN.LNAdd_2_2;
                customerLN.LNAddC_2_3 = customerLN.LNAdd_2_3;
                customerLN.LNAddC_2_4 = customerLN.LNAdd_2_4;
                customerLN.LNAddC_3 = customerLN.LNAdd_3;
                customerLN.LNAddC_3_1 = customerLN.LNAdd_3_1;
                customerLN.LNAddC_3_2 = customerLN.LNAdd_3_2;
                customerLN.LNAddC_4 = customerLN.LNAdd_4;
                customerLN.LNAddC_5 = customerLN.LNAdd_5;
                customerLN.LNAddC_6 = customerLN.LNAdd_6;
                customerLN.LNAddC_7 = customerLN.LNAdd_7;
                customerLN.LNAddC_8 = customerLN.LNAdd_8;
                customerLN.LNAddC_9 = customerLN.LNAdd_9;
            }
            repository.Insert(customerLN);
            _landLordBelongingService.Insert(new LandLordBelonging {
                LId = customerLN.LNID,
                SalesId = currentUserId,
            });
        }
    }
}