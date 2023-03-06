
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
    public class BuildingEquipmentRepository : BaseRepository<BuildingEquipment, string>, IBuildingEquipmentRepository
    {
        /// <summary>
        /// 
        /// </summary>
        public BuildingEquipmentRepository()
        {
        }

        public BuildingEquipmentRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }
    }
}