﻿using Dapper;
using System.Text;
using Yuebon.Commons.Enums;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.Security.IRepositories;
using Yuebon.Security.Models;

namespace Yuebon.Security.Repositories
{
    public class OrganizeRepository : BaseRepository<Organize, string>, IOrganizeRepository
    {
        public OrganizeRepository()
        {
        }

        public OrganizeRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// 獲取根節點組織
        /// </summary>
        /// <param name="id">組織Id</param>
        /// <returns></returns>
        public Organize GetRootOrganize(string id)
        {
            var sb = new StringBuilder(";WITH ");
            if(dbConnectionOptions.DatabaseType == DatabaseType.MySql)
            {
                sb.Append(" Recursive ");
            }
            sb.Append(" T AS (");
            //sb.Append(" SELECT Id, ParentId, FullName, Layers FROM Chaochi_Organize");
            sb.Append(" SELECT Id, ParentId, FullName, Layers FROM Sys_Organize");
            sb.AppendFormat(" WHERE Id = '{0}'",id);
            sb.Append(" UNION ALL ");
            //sb.Append(" SELECT A.Id, A.ParentId, A.FullName, A.Layers FROM Chaochi_Organize AS A JOIN T AS B ON A.Id = B.ParentId ) SELECT* FROM T ORDER BY Layers");
            sb.Append(" SELECT A.Id, A.ParentId, A.FullName, A.Layers FROM Sys_Organize AS A JOIN T AS B ON A.Id = B.ParentId ) SELECT* FROM T ORDER BY Layers");
            return  DapperConn.QueryFirstOrDefault<Organize>(sb.ToString());
        }
    }
}