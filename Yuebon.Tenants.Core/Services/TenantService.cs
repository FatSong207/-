using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Extend;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Services;
using Yuebon.Tenants.Dtos;
using Yuebon.Tenants.IRepositories;
using Yuebon.Tenants.IServices;
using Yuebon.Tenants.Models;

namespace Yuebon.Tenants.Services
{
    /// <summary>
    /// 租戶服務接口實現
    /// </summary>
    public class TenantService: BaseService<Tenant,TenantOutputDto, string>, ITenantService
    {
		private readonly ITenantRepository _repository;
        private readonly ITenantLogonRepository _repositoryLogon;
        public TenantService(ITenantRepository repository, ITenantLogonRepository repositoryLogon) : base(repository)
        {
			_repository=repository;
            _repositoryLogon = repositoryLogon;
        }

        /// <summary>
        /// 根據租戶帳號查詢租戶信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<Tenant> GetByUserName(string userName)
        {
            return await _repository.GetByUserName(userName);
        }


        /// <summary>
        /// 註冊租戶
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tenantLogOnEntity"></param>
        public async Task<bool> InsertAsync(Tenant entity, TenantLogon tenantLogOnEntity)
        {
            return await _repository.InsertAsync(entity, tenantLogOnEntity);
        }


        /// <summary>
        /// 租戶登陸驗證。
        /// </summary>
        /// <param name="userName">用戶名</param>
        /// <param name="password">密碼（第一次md5加密后）</param>
        /// <returns>驗證成功返回用戶實體，驗證失敗返回null|提示消息</returns>
        public async Task<Tuple<Tenant, string>> Validate(string userName, string password)
        {
            var userEntity = await _repository.GetByUserName(userName);

            if (userEntity == null)
            {
                return new Tuple<Tenant, string>(null, "系統不存在該用戶，請重新確認。");
            }

            if (!userEntity.EnabledMark)
            {
                return new Tuple<Tenant, string>(null, "該用戶已被禁用，請聯系管理員。");
            }

            var userSinginEntity = _repositoryLogon.GetByTenantId(userEntity.Id);

            string inputPassword = MD5Util.GetMD5_32(DEncrypt.Encrypt(MD5Util.GetMD5_32(password).ToLower(), userSinginEntity.TenantSecretkey).ToLower()).ToLower();

            if (inputPassword != userSinginEntity.TenantPassword)
            {
                return new Tuple<Tenant, string>(null, "密碼錯誤，請重新輸入。");
            }
            else
            {
                TenantLogon userLogOn = _repositoryLogon.GetWhere("UserId='" + userEntity.Id + "'");
                if (userLogOn.AllowEndTime < DateTime.Now)
                {
                    return new Tuple<Tenant, string>(null, "您的帳號已過期，請聯系系統管理員！");
                }
                if (userLogOn.LockEndDate > DateTime.Now)
                {
                    string dateStr = userLogOn.LockEndDate.ToEasyStringDQ();
                    return new Tuple<Tenant, string>(null, "當前被鎖定，請" + dateStr + "登錄");
                }
                if (userLogOn.FirstVisitTime == null)
                {
                    userLogOn.FirstVisitTime = userLogOn.PreviousVisitTime = DateTime.Now;
                }
                else
                {
                    userLogOn.PreviousVisitTime = DateTime.Now;
                }
                userLogOn.LogOnCount++;
                userLogOn.LastVisitTime = DateTime.Now;
                userLogOn.TenantOnLine = true;
                await _repositoryLogon.UpdateAsync(userLogOn, userLogOn.Id);
                return new Tuple<Tenant, string>(userEntity, "");
            }
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public override async Task<PageResult<TenantOutputDto>> FindWithPagerAsync(SearchInputDto<Tenant> search, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(false);
            if (!string.IsNullOrEmpty(search.Keywords))
            {
                where += " and (TenantName like '%" + search.Keywords + "%' or CompanyName like '%" + search.Keywords + "%')";
            };
            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<Tenant> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            PageResult<TenantOutputDto> pageResult = new PageResult<TenantOutputDto>
            {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = list.MapTo<TenantOutputDto>(),
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }
    }
}