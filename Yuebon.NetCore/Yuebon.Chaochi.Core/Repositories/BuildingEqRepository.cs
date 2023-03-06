
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class BuildingEqRepository : BaseRepository<BuildingEq, string>, IBuildingEqRepository
    {
        /// <summary>
        /// 
        /// </summary>
        public BuildingEqRepository()
        {
        }

        public BuildingEqRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }
        public void InsertRange(List<BuildingEq> buildingEqs)
        {
            DbContext.GetDbSet<BuildingEq>().AddRange(buildingEqs);
            DbContext.SaveChanges();
        }
    }
}