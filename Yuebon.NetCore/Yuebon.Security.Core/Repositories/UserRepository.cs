
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;
using System.Threading.Tasks;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Json;
using Yuebon.Commons.Net.TencentIp;
using Yuebon.Commons.Options;
using Yuebon.Commons.Repositories;
using Yuebon.Security.Dtos;
using Yuebon.Security.IRepositories;
using Yuebon.Security.Models;

namespace Yuebon.Security.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class UserRepository : BaseRepository<User, string>, IUserRepository
    {
        /// <summary>
        /// 
        /// </summary>
        public UserRepository()
        {
        }

        public UserRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// 根據用戶帳號查詢用戶信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<User> GetByUserName(string userName)
        {
            string sql = @"SELECT * FROM Sys_User t WHERE t.Account = @UserName";
            return await DapperConn.QueryFirstOrDefaultAsync<User>(sql, new { @UserName = userName });
        }

        /// <summary>
        /// 根據用戶手機號碼查詢用戶信息
        /// </summary>
        /// <param name="mobilephone">手機號碼</param>
        /// <returns></returns>
        public async Task<User> GetUserByMobilePhone(string mobilephone)
        {
            string sql = @"SELECT * FROM Sys_User t WHERE t.MobilePhone = @MobilePhone";
            return await DapperConn.QueryFirstOrDefaultAsync<User>(sql, new { @MobilePhone = mobilephone });
        }

        /// <summary>
        /// 根據Email查詢用戶信息
        /// </summary>
        /// <param name="email">email</param>
        /// <returns></returns>
        public async Task<User> GetUserByEmail(string email)
        {
            string sql = @"SELECT * FROM Sys_User t WHERE t.Email = @Email";
            return await DapperConn.QueryFirstOrDefaultAsync<User>(sql, new { @Email = email });
        }
        /// <summary>
        /// 根據Email、Account、手機號查詢用戶信息
        /// </summary>
        /// <param name="account">登錄帳號</param>
        /// <returns></returns>
        public async Task<User> GetUserByLogin(string account)
        {
            string sql = @"SELECT * FROM Sys_User t WHERE (t.Account = @Account Or t.Email = @Account Or t.MobilePhone = @Account)";
            return await DapperConn.QueryFirstOrDefaultAsync<User>(sql, new { @Account = account });
        }
        /// <summary>
        /// 註冊用戶
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        public bool Insert(User entity, UserLogOn userLogOnEntity, IDbTransaction trans = null)
        {
            userLogOnEntity.Id = GuidUtils.CreateNo();
            userLogOnEntity.UserId = entity.Id;
            userLogOnEntity.UserSecretkey = MD5Util.GetMD5_16(GuidUtils.NewGuidFormatN()).ToLower();
            userLogOnEntity.UserPassword = MD5Util.GetMD5_32(DEncrypt.Encrypt(MD5Util.GetMD5_32(userLogOnEntity.UserPassword).ToLower(), userLogOnEntity.UserSecretkey).ToLower()).ToLower();
            DbContext.GetDbSet<User>().Add(entity);
            DbContext.GetDbSet<UserLogOn>().Add(userLogOnEntity);
            return DbContext.SaveChanges() > 0;

        }

        /// <summary>
        /// 註冊用戶
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        public async Task<bool> InsertAsync(User entity, UserLogOn userLogOnEntity, IDbTransaction trans = null)
        {
            userLogOnEntity.Id = GuidUtils.CreateNo();
            userLogOnEntity.UserId = entity.Id;
            userLogOnEntity.UserSecretkey = MD5Util.GetMD5_16(GuidUtils.NewGuidFormatN()).ToLower();
            userLogOnEntity.UserPassword = MD5Util.GetMD5_32(DEncrypt.Encrypt(MD5Util.GetMD5_32(userLogOnEntity.UserPassword).ToLower(), userLogOnEntity.UserSecretkey).ToLower()).ToLower();
            DbContext.GetDbSet<User>().Add(entity);
            DbContext.GetDbSet<UserLogOn>().Add(userLogOnEntity);
            return await DbContext.SaveChangesAsync() > 0;
        }
        /// <summary>
        /// 註冊用戶,第三方平臺
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="userOpenIds"></param>
        /// <param name="trans"></param>
        public bool Insert(User entity, UserLogOn userLogOnEntity, UserOpenIds userOpenIds, IDbTransaction trans = null)
        {

            DbContext.GetDbSet<User>().Add(entity);
            DbContext.GetDbSet<UserLogOn>().Add(userLogOnEntity); userLogOnEntity.Id = GuidUtils.CreateNo();
            userLogOnEntity.UserId = entity.Id;
            userLogOnEntity.UserSecretkey = MD5Util.GetMD5_16(GuidUtils.NewGuidFormatN()).ToLower();
            userLogOnEntity.UserPassword = MD5Util.GetMD5_32(DEncrypt.Encrypt(MD5Util.GetMD5_32(userLogOnEntity.UserPassword).ToLower(), userLogOnEntity.UserSecretkey).ToLower()).ToLower();
            DbContext.GetDbSet<User>().Add(entity);
            DbContext.GetDbSet<UserLogOn>().Add(userLogOnEntity);
            DbContext.GetDbSet<UserOpenIds>().Add(userOpenIds);
            return DbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 根據微信UnionId查詢用戶信息
        /// </summary>
        /// <param name="unionId">UnionId值</param>
        /// <returns></returns>
        public User GetUserByUnionId(string unionId)
        {
            string sql = string.Format("select * from Sys_User where UnionId = '{0}'", unionId);
            return DapperConn.QueryFirstOrDefault<User>(sql);
        }
        /// <summary>
        /// 根據第三方OpenId查詢用戶信息
        /// </summary>
        /// <param name="openIdType">第三方類型</param>
        /// <param name="openId">OpenId值</param>
        /// <returns></returns>
        public User GetUserByOpenId(string openIdType, string openId)
        {
            string sql = string.Format("select * from Sys_User as u join Sys_UserOpenIds as o on u.Id = o.UserId and  o.OpenIdType = '{0}' and o.OpenId = '{1}'", openIdType, openId);
            return DapperConn.QueryFirstOrDefault<User>(sql);
        }

        /// <summary>
        /// 根據userId查詢用戶信息
        /// </summary>
        /// <param name="openIdType">第三方類型</param>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        public UserOpenIds GetUserOpenIdByuserId(string openIdType, string userId)
        {
            string sql = string.Format("select * from Sys_UserOpenIds  where OpenIdType = '{0}' and UserId = '{1}'", openIdType, userId);
            return DapperConn.QueryFirstOrDefault<UserOpenIds>(sql);
        }

        /// <summary>
        /// 更新用戶信息,第三方平臺
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        public bool UpdateUserByOpenId(User entity, UserLogOn userLogOnEntity, UserOpenIds userOpenIds, IDbTransaction trans = null)
        {
            DbContext.GetDbSet<User>().Add(entity);
            DbContext.GetDbSet<UserOpenIds>().Add(userOpenIds);
            return DbContext.SaveChanges() > 0;
        }





        /// <summary>
        /// 分頁得到所有用戶用于關注
        /// </summary>
        /// <param name="currentpage"></param>
        /// <param name="pagesize"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public IEnumerable<UserAllListFocusOutPutDto> GetUserAllListFocusByPage(string currentpage,
            string pagesize, string userid)
        {
            string sqlRecord = "";
            string sql = "";

            int countNotIn = (int.Parse(currentpage) - 1) * int.Parse(pagesize);

            sqlRecord = @"select * from sys_user where nickname <> '游客' and  ismember=1 ";

            sql = @"SELECT TOP " + pagesize +
                @"
case when t2.Id is null then 'n' 
else 'y' end as IfFocus ,
IsNull(t3.totalFocus,0) as TotalFocus, 
t1.*
from 
(select ISNULL(tin2.VipGrade,0) as VipGrade,
ISNULL(tin2.IsAuthentication,0) as IsAuthentication,
ISNULL(tin2.AuthenticationType,0) as AuthenticationType,
tin1.* from sys_user tin1 
left join Sys_UserExtend tin2 on tin1.Id=tin2.UserId 
where nickname <> '游客' and  ismember=1) t1
left join 
(select * from Sys_UserFocus where creatorUserid='" + userid + @"') t2 
on t1.id=t2.focususerid 
left join 
(select  top 100 percent focusUserID,count(*) as totalFocus from Sys_UserFocus group by focusUserID order by totalfocus desc) t3
on t1.Id=t3.focusUserID 

where t1.Id not in 
(
select top " + countNotIn + @"
tt1.Id 
from 
(select ISNULL(tin2.VipGrade,0) as VipGrade,
ISNULL(tin2.IsAuthentication,0) as IsAuthentication,
ISNULL(tin2.AuthenticationType,0) as AuthenticationType,
tin1.* from sys_user tin1 
left join Sys_UserExtend tin2 on tin1.Id=tin2.UserId 
where nickname <> '游客' and  ismember=1) tt1
left join 
(select * from Sys_UserFocus where creatorUserid='" + userid + @"') tt2
on tt1.id=tt2.focususerid 
left join 
(select  top 100 percent focusUserID,count(*) as totalFocus from Sys_UserFocus group by focusUserID order by totalfocus desc) tt3
on tt1.Id=tt3.focusUserID 

order by tt3.totalFocus desc
)

order by t3.totalFocus desc";


            List<UserAllListFocusOutPutDto> list = new List<UserAllListFocusOutPutDto>();

            IEnumerable<UserAllListFocusOutPutDto> infoOutputDto = DapperConn.Query<UserAllListFocusOutPutDto>(sql);

            //得到總記錄數
            List<UserAllListFocusOutPutDto> recordCountList = DapperConn.Query<UserAllListFocusOutPutDto>(sqlRecord).AsList();

            list = infoOutputDto.AsList();
            for (int i = 0; i < list.Count; i++) {
                list[i].RecordCount = recordCountList.Count;
            }
            return list;
        }

        /// <summary>
        /// 獲取可訪問的組織部門
        /// </summary>
        /// <param name="deptList"></param>
        /// <returns></returns>
        public List<string> GetPermissionDepts(string deptList)
        {
            deptList = deptList.Replace(",","','");
            //            string sql = @$"WITH n (id, FullName) AS 
            //(SELECT id, FullName FROM Chaochi_Organize WHERE id in ('{deptList}') 
            //UNION ALL SELECT child.id, child.FullName FROM Chaochi_Organize AS child, n WHERE n.id = child.parentId) SELECT* FROM n";
            string sql = @$"WITH n (id, FullName) AS 
            (SELECT id, FullName FROM Sys_Organize WHERE id in ('{deptList}') 
            UNION ALL SELECT child.id, child.FullName FROM Sys_Organize AS child, n WHERE n.id = child.parentId) SELECT* FROM n";
            var result =DapperConnRead.Query<string>(sql);
            return result.AsList();
        }

        /// <summary>
        /// 找出登入者的所屬店長
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public string GetStoreManager(string account)
        {
            string storeManagerAlias ="storemanager";
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();            
            AppSetting sysSetting = yuebonCacheHelper.Get<AppSetting>("SysSetting");
            if (sysSetting != null) {
                storeManagerAlias = sysSetting.ChaochiStoreManagerAlias;
            }
            string sql = @$"select Account from sys_user where roleid like concat('%', (select id from sys_role where encode = '{storeManagerAlias}'), '%') and DepartmentId = (select DepartmentId from sys_user where account = '{account}')";
            var result = DapperConnRead.QueryFirstOrDefault<string>(sql);            
            return result;
        }

        /// <summary>
        /// 找出所屬店的店長
        /// </summary>
        /// <param name="departmentId">所屬店ID</param>
        /// <returns></returns>
        public string GetManagerByDepartmentId(string departmentId)
        {
            //string storeManagerAlias = string.Empty;
            //YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            //AppSetting sysSetting = yuebonCacheHelper.Get<AppSetting>("SysSetting");
            //if (sysSetting != null) {
            //    storeManagerAlias = sysSetting.ChaochiStoreManagerAlias;
            //}
            //select* from sys_user where DepartmentId = 'CHW1114' and roleid like '%20220427153817366475515326%'
            //string sql = @$"select  from sys_user where roleid = (select id from sys_role where encode = '{storeManagerAlias}') and DepartmentId = '{departmentId}'";
            string sql = @$"select ManagerId from Sys_Organize where id = '{departmentId}'";            
            var result = DapperConnRead.QueryFirstOrDefault<string>(sql);
            return result;
        }

        /// <summary>
        /// 找出所屬店的所有業務和店長
        /// </summary>
        /// <param name="account"></param>
        /// <param name="isStoreManager">是否為店長</param>
        /// <returns></returns>
        public async Task<List<User>> GetStoreSales(string account, bool isStoreManager = false)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            string storeManagerAlias = string.Empty;
            string storeSalesAlias = string.Empty;
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            AppSetting sysSetting = yuebonCacheHelper.Get<AppSetting>("SysSetting");
            if (sysSetting != null) {
                storeManagerAlias = sysSetting.ChaochiStoreManagerAlias;
                storeSalesAlias = sysSetting.ChaochiSalesAlias;
            }
            List<User> userList = new List<User>();
            var list = JsonSerializer.Deserialize<List<String>>(yuebonCacheHelper.Get($"User_PermissionDepts{account}").ToJson());
            //string sql = @$"select * from sys_user where roleid in (select id from sys_role where encode = '{storeSalesAlias}' or encode = '{storeManagerAlias}') and DepartmentId = (select distinct DepartmentId from sys_user where account = '{account}')";
            string sql = @$"select * from sys_user where roleid in (select id from sys_role where encode = '{storeSalesAlias}' or encode = '{storeManagerAlias}') and DepartmentId in ('{string.Join("','", list)}')";
            //string sql = @$"select Account from sys_user where roleid = (select id from sys_role where encode = '{storeManagerAlias}') and DepartmentId in ('{string.Join("','", list)}')";
            var userInfo = await DapperConnRead.QueryAsync<User>(sql);
            if (userInfo != null) {
                userList = userInfo.AsList<User>();
            }

            // 店長自己
            if (isStoreManager) {
                User dummy = new User();
                dummy.Account = "";
                dummy.RealName = "待指派";
                userList.Insert(0, dummy);
            }
            return userList;
        }

        /// <summary>
        /// 找出所屬店的所有業務和店長
        /// </summary>
        /// <param name="departmentId">所屬店ID</param>
        /// <param name="isStoreManager">是否為店長</param>
        /// <returns></returns>
        public async Task<List<User>> GetStoreSalesByDepartmentId(string departmentId, bool isStoreManager = false)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            string storeManagerAlias = string.Empty;
            string storeSalesAlias = string.Empty;
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            AppSetting sysSetting = yuebonCacheHelper.Get<AppSetting>("SysSetting");
            if (sysSetting != null) {
                storeManagerAlias = sysSetting.ChaochiStoreManagerAlias;
                storeSalesAlias = sysSetting.ChaochiSalesAlias;
            }
            //string sql = @$"select Account from sys_user where roleid like '%{storeManagerId}%' and DepartmentId = (select DepartmentId from sys_user where account = '{account}')";
            List<User> userList = new List<User>();
            string sql = @$"select * from sys_user where roleid in (select id from sys_role where encode = '{storeSalesAlias}' or encode = '{storeManagerAlias}') and DepartmentId = '{departmentId}'";
            var userInfo = await DapperConnRead.QueryAsync<User>(sql);
            if (userInfo != null) {
                userList = userInfo.AsList<User>();
            }

            // 店長自己
            if (isStoreManager) {
                User dummy = new User();
                dummy.Account = "";
                dummy.RealName = "待指派";
                userList.Insert(0, dummy);
                //result.Add("", "待指派");
                //result.Insert(1, account);
            }
            return userList;
        }
    }
}