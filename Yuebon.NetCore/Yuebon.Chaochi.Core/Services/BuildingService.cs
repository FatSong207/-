using System;
using System.Data.Common;
using System.Linq;
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
using System.IO;
using Dapper;
using Yuebon.Commons.Json;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class BuildingService : BaseService<Building, BuildingOutputDto, string>, IBuildingService
    {
        private readonly IBuildingRepository _userRepository;
        private readonly Security.IServices.IUserService _userService;
        private readonly IBuildingBelongingRepository _buildingBelongingRepository;
        private readonly IBuildingAdvertisementService _buildingAdvertisementService;
        private readonly Security.IRepositories.IUserLogOnRepository _userSigninRepository;
        private readonly Security.IServices.ILogService _logService;
        private readonly Security.IServices.IRoleService _roleService;
        private Security.IServices.IOrganizeService _organizeService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="userLogOnRepository"></param>
        /// <param name="logService"></param>
        /// <param name="roleService"></param>
        /// <param name="organizeService"></param>
        /// <param name="buildingAdvertisementService"></param>
        public BuildingService(IBuildingRepository repository, Yuebon.Security.IServices.IUserService userService, IBuildingBelongingRepository buildingBelongingRepository, IBuildingAdvertisementService buildingAdvertisementService, Security.IRepositories.IUserLogOnRepository userLogOnRepository, Security.IServices.ILogService logService, Security.IServices.IRoleService roleService, Security.IServices.IOrganizeService organizeService) : base(repository)
        {
            _userRepository = repository;
            _userService = userService;
            _buildingBelongingRepository = buildingBelongingRepository;
            _buildingAdvertisementService = buildingAdvertisementService;
            _userSigninRepository = userLogOnRepository;
            _logService = logService;
            _roleService = roleService;
            _organizeService = organizeService;
        }

        /// <summary>
        /// 獲取Chaochi_BuildingPropoties所有資料(用於下拉選單)
        /// </summary>
        /// <returns></returns>
        public Task<List<BuildingPropoties>> GetAllBuildingPropotiesAsync()
        {
            return _userRepository.GetAllBuildingPropotiesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buildingEquipment"></param>
        /// <param name="buildingEq"></param>
        public void DoubleUpdate(BuildingEquipment buildingEquipment, BuildingEq buildingEq)
        {

            _userRepository.DoubleUpdate(buildingEquipment, buildingEq);
        }


        /// <summary>
        /// 用戶登陸驗證。
        /// </summary>
        /// <param name="userName">用戶名</param>
        /// <param name="password">密碼（第一次md5加密后）</param>
        /// <returns>驗證成功返回用戶實體，驗證失敗返回null|提示消息</returns>
        public async Task<Tuple<User, string>> Validate(string userName, string password)
        {
            var userEntity = await _userRepository.GetUserByLogin(userName);

            if (userEntity == null) {
                return new Tuple<User, string>(null, "系統不存在該用戶，請重新確認。");
            }

            if (!userEntity.EnabledMark) {
                return new Tuple<User, string>(null, "該用戶已被禁用，請聯系管理員。");
            }

            var userSinginEntity = _userSigninRepository.GetByUserId(userEntity.Id);

            string inputPassword = MD5Util.GetMD5_32(DEncrypt.Encrypt(MD5Util.GetMD5_32(password).ToLower(), userSinginEntity.UserSecretkey).ToLower()).ToLower();

            if (inputPassword != userSinginEntity.UserPassword) {
                return new Tuple<User, string>(null, "密碼錯誤，請重新輸入。");
            } else {
                Security.Models.UserLogOn userLogOn = _userSigninRepository.GetWhere("UserId='" + userEntity.Id + "'");
                if (userLogOn.AllowEndTime < DateTime.Now) {
                    return new Tuple<User, string>(null, "您的帳號已過期，請聯系系統管理員！");
                }
                if (userLogOn.LockEndDate > DateTime.Now) {
                    string dateStr = userLogOn.LockEndDate.ToEasyStringDQ();
                    return new Tuple<User, string>(null, "當前被鎖定，請" + dateStr + "登錄");
                }
                if (userLogOn.FirstVisitTime == null) {
                    userLogOn.FirstVisitTime = userLogOn.PreviousVisitTime = DateTime.Now;
                } else {
                    userLogOn.PreviousVisitTime = DateTime.Now;
                }
                userLogOn.LogOnCount++;
                userLogOn.LastVisitTime = DateTime.Now;
                userLogOn.UserOnLine = true;
                await _userSigninRepository.UpdateAsync(userLogOn, userLogOn.Id);
                return new Tuple<User, string>(userEntity, "");
            }
        }

        /// <summary>
        /// 用戶登陸驗證。
        /// </summary>
        /// <param name="userName">用戶名</param>
        /// <param name="password">密碼（第一次md5加密后）</param>
        /// <param name="userType">用戶類型</param>
        /// <returns>驗證成功返回用戶實體，驗證失敗返回null|提示消息</returns>
        public async Task<Tuple<User, string>> Validate(string userName, string password, UserType userType)
        {
            var userEntity = await _userRepository.GetUserByLogin(userName);

            if (userEntity == null) {
                return new Tuple<User, string>(null, "系統不存在該用戶，請重新確認。");
            }

            if (!userEntity.EnabledMark) {
                return new Tuple<User, string>(null, "該用戶已被禁用，請聯系管理員。");
            }

            var userSinginEntity = _userSigninRepository.GetByUserId(userEntity.Id);

            string inputPassword = MD5Util.GetMD5_32(DEncrypt.Encrypt(MD5Util.GetMD5_32(password).ToLower(), userSinginEntity.UserSecretkey).ToLower()).ToLower();

            if (inputPassword != userSinginEntity.UserPassword) {
                return new Tuple<User, string>(null, "密碼錯誤，請重新輸入。");
            } else {
                Security.Models.UserLogOn userLogOn = _userSigninRepository.GetWhere("UserId='" + userEntity.Id + "'");
                if (userLogOn.AllowEndTime < DateTime.Now) {
                    return new Tuple<User, string>(null, "您的帳號已過期，請聯系系統管理員！");
                }
                if (userLogOn.LockEndDate > DateTime.Now) {
                    string dateStr = userLogOn.LockEndDate.ToEasyStringDQ();
                    return new Tuple<User, string>(null, "當前被鎖定，請" + dateStr + "登錄");
                }
                if (userLogOn.FirstVisitTime == null) {
                    userLogOn.FirstVisitTime = userLogOn.PreviousVisitTime = DateTime.Now;
                } else {
                    userLogOn.PreviousVisitTime = DateTime.Now;
                }
                userLogOn.LogOnCount++;
                userLogOn.LastVisitTime = DateTime.Now;
                userLogOn.UserOnLine = true;
                await _userSigninRepository.UpdateAsync(userLogOn, userLogOn.Id);
                return new Tuple<User, string>(userEntity, "");
            }
        }

        /// <summary>
        /// 根據用戶帳號查詢用戶信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<User> GetByUserName(string userName)
        {
            return await _userRepository.GetByUserName(userName);
        }
        /// <summary>
        /// 根據用戶手機號碼查詢用戶信息
        /// </summary>
        /// <param name="mobilephone">手機號碼</param>
        /// <returns></returns>
        public async Task<User> GetUserByMobilePhone(string mobilephone)
        {
            return await _userRepository.GetUserByMobilePhone(mobilephone);
        }
        /// <summary>
        /// 根據Email、Account、手機號查詢用戶信息
        /// </summary>
        /// <param name="account">登錄帳號</param>
        /// <returns></returns>
        public async Task<User> GetUserByLogin(string account)
        {
            return await _userRepository.GetUserByLogin(account);
        }
        /// <summary>
        /// 註冊用戶
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        public bool Insert(Building entity, Security.Models.UserLogOn userLogOnEntity, IDbTransaction trans = null)
        {
            return _userRepository.Insert(entity, userLogOnEntity, trans);
        }

        /// <summary>
        /// 註冊用戶
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        public async Task<bool> InsertAsync(Building entity, List<BuildingBelonging> buildingBelongingEntitys, IDbTransaction trans = null)
        {
            return await _userRepository.InsertAsync(entity, buildingBelongingEntitys, trans);
        }
        /// <summary>
        /// 註冊用戶,第三方平臺
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        public bool Insert(Building entity, Security.Models.UserLogOn userLogOnEntity, Security.Models.UserOpenIds userOpenIds, IDbTransaction trans = null)
        {
            return _userRepository.Insert(entity, userLogOnEntity, userOpenIds, trans);
        }

        /// <summary>
        /// 根據第三方OpenId查詢用戶信息
        /// </summary>
        /// <param name="openIdType">第三方類型</param>
        /// <param name="openId">OpenId值</param>
        /// <returns></returns>
        public User GetUserByOpenId(string openIdType, string openId)
        {
            return _userRepository.GetUserByOpenId(openIdType, openId);
        }
        /// <summary>
        /// 根據userId查詢用戶信息
        /// </summary>
        /// <param name="openIdType">第三方類型</param>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        public Security.Models.UserOpenIds GetUserOpenIdByuserId(string openIdType, string userId)
        {
            return _userRepository.GetUserOpenIdByuserId(openIdType, userId);
        }
        /// <summary>
        /// 更新用戶信息,第三方平臺
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="userOpenIds"></param>
        /// <param name="trans"></param>
        public bool UpdateUserByOpenId(User entity, Security.Models.UserLogOn userLogOnEntity, Security.Models.UserOpenIds userOpenIds, IDbTransaction trans = null)
        {
            return _userRepository.UpdateUserByOpenId(entity, userLogOnEntity, userOpenIds, trans);
        }

        /// <summary>
        /// 根據微信UnionId查詢用戶信息
        /// </summary>
        /// <param name="unionId">UnionId值</param>
        /// <returns></returns>
        public User GetUserByUnionId(string unionId)
        {
            return _userRepository.GetUserByUnionId(unionId);
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public async Task<PageResult<BuildingListOutputDto>> FindWithPagerSearchAsync(SearchBuildingModel search)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(true);
            //search.BAdd =
            //    Utils.memergeAddFix(
            //       search.BAdd_1,
            //       search.BAdd_1_1,
            //       search.BAdd_1_2,
            //       search.BAdd_2,
            //       search.BAdd_2_1,
            //       search.BAdd_2_2,
            //       search.BAdd_2_3,
            //       search.BAdd_2_4,
            //       search.BAdd_3,
            //       search.BAdd_3_1,
            //       search.BAdd_3_2,
            //       search.BAdd_4,
            //       search.BAdd_5,
            //       search.BAdd_6,
            //       search.BAdd_7,
            //       search.BAdd_8,
            //       search.BAdd_9
            //    );
            var filter = search.Filter;
            if (!string.IsNullOrEmpty(filter.BelongLID)) {
                where += $" and BelongLID = '{filter.BelongLID}' ";
            }

            if (!string.IsNullOrEmpty(filter.CreatorUserId)) {
                if (filter.CreatorUserId == "等待指派") {
                    where += " and CreatorUserId = '' ";
                } else {
                    var user = await _userService.GetWhereAsync($" id  like '%{filter.CreatorUserId}%'");
                    if (user != null)
                        where += $" and  CreatorUserId = '{user.Id}'";
                    else
                        where += " and 1=0 ";
                }
            }

            if (!string.IsNullOrEmpty(search.BAdd)) {
                where += $" and BAdd like N'%{search.BAdd}%'";
            }

            if (!string.IsNullOrEmpty(filter.BState)) {
                where += $" and BState = '{filter.BState}' ";
            }

            PagerInfo pagerInfo = new PagerInfo {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<Building> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            List<BuildingListOutputDto> resultList = list.MapTo<BuildingListOutputDto>();
            List<BuildingListOutputDto> listResult = new List<BuildingListOutputDto>();
            foreach (BuildingListOutputDto item in resultList) {
                //if (!string.IsNullOrEmpty(item.OrganizeId))
                //{
                //    item.OrganizeName = _organizeService.Get(item.OrganizeId).FullName;
                //}
                //if (!string.IsNullOrEmpty(item.RoleId))
                //{
                //    item.RoleName = _roleService.GetRoleNameStr(item.RoleId);
                //}
                //if (!string.IsNullOrEmpty(item.DepartmentId))
                //{
                //    item.DepartmentName = _organizeService.Get(item.DepartmentId).FullName;
                //}
                //if (!string.IsNullOrEmpty(item.DutyId))
                //{
                //    item.DutyName = _roleService.Get(item.DutyId).FullName;
                //}

                //where = GetDataPrivilege(false);
                //where += string.Format(" and BuildingId = '{0}'", item.Id);

                //List<BuildingBelonging> BuildingBelongings = await _buildingBelongingRepository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
                //item.BuildingBelongings = BuildingBelongings;
                item.CreatorUserName = (!string.IsNullOrEmpty(item.CreatorUserId)) ? _userService.Get(item.CreatorUserId).RealName : "等待指派";



                listResult.Add(item);
            }
            PageResult<BuildingListOutputDto> pageResult = new PageResult<BuildingListOutputDto> {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = listResult,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public async Task<PageResult<MyFileInfo>> FindBuildImagesWithPagerSearchAsync(SearchBuildingModel search)
        {
            bool order = search.Order == "asc" ? false : true;

            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Security.Models.SysSetting>();

            var path = $"{sysSetting.ChaochiFilepath}";
            string uploadPath = @$"{path}BuildingImage\{search.Keywords}";

            var infoD = new DirectoryInfo(sysSetting.ChaochiFilepath + @$"BuildingImage\{search.Keywords}");
            if (!Directory.Exists(infoD.ToString())) {
                Directory.CreateDirectory(infoD.ToString());
            }
            var files = infoD.GetFiles().OrderByDescending(p => p.CreationTime).ToList();
            var FileInfoOutputDto = new FileInfoOutputDto {
                Id = search.Keywords,
            };
            var total = files.Count();
            var myfiles = files.Skip((search.CurrenetPageIndex - 1) * search.PageSize).Take(search.PageSize).Select(x => new MyFileInfo { FileName = x.Name }).ToList();
            FileInfoOutputDto.BuildingImages = myfiles;



            PagerInfo pagerInfo = new PagerInfo {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize,
                RecordCount = total,
            };

            //List<BuildingListOutputDto> listResult = new List<BuildingListOutputDto>();
            //foreach (BuildingListOutputDto item in resultList) {
            //    listResult.Add(item);
            //}
            PageResult<MyFileInfo> pageResult = new PageResult<MyFileInfo> {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = FileInfoOutputDto.BuildingImages,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };

            return pageResult;
        }


        /// <summary>
        /// 異步查詢單個實體。
        /// </summary>
        /// <param name="id">主鍵</param>
        /// <returns></returns>
        public override async Task<BuildingOutputDto> GetOutDtoAsync(string id)
        {
            Building info = await repository.GetAsync(id);
            BuildingOutputDto BuildingOutputDto = info.MapTo<BuildingOutputDto>();

            //出租住宅基本資料
            //var building = await iService.GetAsync(id);
            var buildingRentBasic = info.MapTo<BuildingRentBasicOutputDto>();
            var buildingSituation = info.MapTo<BuildingSituationOutputDto>();
            BuildingOutputDto.BuildingRentBasic = buildingRentBasic;
            BuildingOutputDto.BuildingSituation = buildingSituation;

            string where = GetDataPrivilege(false);
            where += string.Format(" and BuildingId = '{0}'", id);
            List<BuildingBelonging> BuildingBelongings = _buildingBelongingRepository.GetListWhere(where).ToList();
            BuildingOutputDto.BuildingBelongings = BuildingBelongings;

            //where = GetDataPrivilege(false);
            //where += string.Format(" and BAdd = '{0}'", info.BAdd);
            //List<Remittance> Remittances = _IRemittanceRepository.GetListWhere(where).ToList();

            //if (Remittances != null && Remittances.Count >= 2) {
            //    throw new Exception("匯款資料不唯一!");
            //} else {
            //    BuildingOutputDto.Remittances = Remittances;
            //}

            //https://www.aspsnippets.com/Articles/ASPNet-Core-MVC-Display-List-of-Files-from-Folder-Directory.aspx
            //string[] filePaths = Directory.GetFiles(Path.Combine("D:/zzz/", "image/"));
            //BuildingOutputDto.BuildingImages = filePaths;

            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Security.Models.SysSetting>();
            //https://stackoverflow.com/questions/13865718/directory-getfiles-get-todays-files-only
            //DirectoryInfo infoD = new DirectoryInfo(Path.Combine("D:/zzz/", "image/"));
            DirectoryInfo infoD = new DirectoryInfo(sysSetting.ChaochiFilepath);

            //https://stackoverflow.com/questions/13865718/directory-getfiles-get-todays-files-only
            //DirectoryInfo infoD = new DirectoryInfo(Path.Combine("D:/zzz/", "image/"));
            List<FileInfo> files = infoD.GetFiles().OrderBy(p => p.CreationTime).ToList<FileInfo>();
            //https://stackoverflow.com/questions/10750146/linq-select-to-new-object
            List<MyFileInfo> myfiles = files.Select(x => new MyFileInfo { FileName = x.Name }).ToList();
            BuildingOutputDto.BuildingImages = myfiles;

            return BuildingOutputDto;
        }

        /// <summary>
        /// 異步查詢單個實體。
        /// </summary>
        /// <param name="id">主鍵</param>
        /// <returns></returns>
        public async Task<FileInfoOutputDto> GetImgOutDtoAsync(string id)
        {


            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Security.Models.SysSetting>();

            var path = $"{sysSetting.ChaochiFilepath}";
            string uploadPath = @$"{path}BuildingImage\{id}";
            if (System.IO.Directory.Exists(uploadPath)) {
                DirectoryInfo infoD = new DirectoryInfo(sysSetting.ChaochiFilepath + @$"BuildingImage\{id}");

                List<FileInfo> files = infoD.GetFiles().OrderBy(p => p.CreationTime).ToList<FileInfo>();


                FileInfoOutputDto FileInfoOutputDto = new FileInfoOutputDto {
                    Id = id,
                };
                List<MyFileInfo> myfiles = files.Select(x => new MyFileInfo { FileName = x.Name }).ToList();
                FileInfoOutputDto.BuildingImages = myfiles;

                return FileInfoOutputDto;
            } else {
                return new FileInfoOutputDto();
            }
        }

        /// <summary>
        /// 根據地址查詢一個建物ID
        /// </summary>
        /// <param name="BAdd"></param>
        /// <returns></returns>
        public string GetIdByBAdd(string BAdd)
        {
            var inst = repository.GetWhere($"BAdd= N'{BAdd}'");
            if (inst is not null) {
                return inst.Id;
            } else {
                return "";
            }
        }

        /// <summary>
        /// 根據地址查詢一個建物
        /// </summary>
        /// <param name="BAdd"></param>
        /// <returns></returns>
        public async Task<Building> GetBuildingByBAddAsync(string BAdd)
        {
            var building = await repository.GetWhereAsync($"BAdd=N'{BAdd}'");
            return building;
        }

        /// <summary>
        /// 更新建物狀態
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="Id">主鍵值</param>
        /// <param name="state">建物狀態</param>
        /// <returns></returns> 
        public async Task<bool> UpdateBState(string userId, string Id, string state, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool result = false;

            Building buildingInfo = repository.Get(Id);
            if (buildingInfo is not null) {
                buildingInfo.BState = state;
                buildingInfo.LastModifyUserId = userId;
                buildingInfo.LastModifyTime = DateTime.Now;

                result = await repository.UpdateAsync(buildingInfo, Id, conn, trans);

                if ("待招租".Equals(state)) {
                    result = await _buildingAdvertisementService.UpdateBRStartDate(userId, Id, DateTime.Today, conn, trans);
                }
            }

            return result;
        }

        /// <summary>
        /// 更新建物狀態(排程用)
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="Id">主鍵值</param>
        /// <param name="state">建物狀態</param>
        /// <returns></returns> 
        public bool UpdateBStateTask(string userId, string Id, string state, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool result = false;

            Building buildingInfo = repository.Get(Id);
            if (buildingInfo is not null) {
                buildingInfo.BState = state;
                buildingInfo.LastModifyUserId = userId;
                buildingInfo.LastModifyTime = DateTime.Now;

                result = repository.Update(buildingInfo, Id, trans);

                if ("待招租".Equals(state)) {
                    result = _buildingAdvertisementService.UpdateBRStartDate(userId, Id, DateTime.Today, conn, trans).Result;
                }
            }

            return result;
        }
    }
}