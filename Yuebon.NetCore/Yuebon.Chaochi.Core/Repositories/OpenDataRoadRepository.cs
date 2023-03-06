using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuebon.Chaochi.Core.IRepositories;
using Yuebon.Chaochi.Core.Models;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;

namespace Yuebon.Chaochi.Core.Repositories
{
    public class OpenDataRoadRepository : BaseRepository<OpenDataRoad, string>, IOpenDataRoadRepository
    {
        public OpenDataRoadRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }
        public void InsertRange(List<OpenDataRoad> openDataRoads)
        {
            DbContext.GetDbSet<OpenDataRoad>().AddRange(openDataRoads);
            DbContext.SaveChanges();
        }
        public IEnumerable<dynamic> GetAllByField(string fieldName)
        {
            string sql = $"select distinct {fieldName} from OpenDataRoad"; 
            return DapperConn.Query(sql);
        }

        public IEnumerable<dynamic> GetStreetByQuery(string county, string site_id, string query)
        {
            string sql = $"select distinct road from OpenDataRoad where city='{county}' and site_id ='{site_id}' and road like '%{query}%'";
            return DapperConn.Query(sql);
        }
    }
}
