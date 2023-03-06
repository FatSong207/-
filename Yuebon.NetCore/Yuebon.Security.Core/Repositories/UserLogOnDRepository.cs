using Dapper;
using System;
using System.Data;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Options;
using Yuebon.Commons.Repositories;
using Yuebon.Security.IRepositories;
using Yuebon.Security.Models;

namespace Yuebon.Security.Repositories
{
    public class UserLogOnDRepository : BaseRepository<UserLogOnD, string>, IUserLogOnDRepository
    {
        public UserLogOnDRepository()
        {
        }

        public UserLogOnDRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }



        /// <summary>
        /// 根據會員ID獲取用戶登錄信息實體
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserLogOnD GetByUserId(string userId)
        {
            string sql = @"SELECT * FROM Sys_UserLogOnD t WHERE t.UserId = @UserId";
            return DapperConn.QueryFirst<UserLogOnD>(sql, new { UserId = userId });
        }
    }
}