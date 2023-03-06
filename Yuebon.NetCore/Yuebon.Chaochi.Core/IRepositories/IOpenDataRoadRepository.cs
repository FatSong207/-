using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Core.Models;

namespace Yuebon.Chaochi.Core.IRepositories
{
    public interface IOpenDataRoadRepository : IRepository<OpenDataRoad, string>
    {
        void InsertRange(List<OpenDataRoad> openDataRoads);
        IEnumerable<dynamic> GetAllByField(string fieldName);
        IEnumerable<dynamic> GetStreetByQuery(string county,string site_id,string query);
    }
}
