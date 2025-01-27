﻿using Dapper;
using System.Collections.Generic;
using System.Linq;
using Yuebon.Commons.DbContextCore;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.Security.Dtos;
using Yuebon.Security.IRepositories;
using Yuebon.Security.Models;

namespace Yuebon.Security.Repositories
{
    /// <summary>
    /// 應用倉儲實現
    /// </summary>
    public class APPRepository : BaseRepository<APP,string>, IAPPRepository
    {
        public APPRepository()
        {
        }
        public APPRepository(IDbContextCore context) : base(context)
        {
        }
        /// <summary>
        /// 獲取app對象
        /// </summary>
        /// <param name="appid">應用ID</param>
        /// <param name="secret">應用密鑰AppSecret</param>
        /// <returns></returns>
        public APP GetAPP(string appid, string secret)
        {
            string sql = @"SELECT * FROM Sys_APP t WHERE t.AppId = @AppId and AppSecret=@AppSecret and EnabledMark=1";
            return DapperConnRead.QueryFirstOrDefault<APP>(sql, new { AppId = appid, AppSecret = secret });
        }

        /// <summary>
        /// 獲取app對象
        /// </summary>
        /// <param name="appid">應用ID</param>
        /// <returns></returns>
        public APP GetAPP(string appid)
        {
            string sql = @"SELECT * FROM Sys_APP t WHERE t.AppId = @AppId and EnabledMark=1";
            return DapperConnRead.QueryFirstOrDefault<APP>(sql, new { AppId = appid });

        }
        public IList<AppOutputDto> SelectApp()
        {
            const string query = @"select a.*,u.id as Id,u.NickName,u.Account,u.HeadIcon from Sys_APP a,Sys_User u where a.CreatorUserId=u.Id ";
            return DapperConnRead.Query<AppOutputDto, User, AppOutputDto>(query, (app, user) => { app.UserInfo = user; return app; }, null, splitOn: "Id").ToList<AppOutputDto>();
        }
    }
}