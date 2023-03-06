using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuebon.Chaochi.Core.Models;
using Yuebon.Commons.IServices;

namespace Yuebon.Chaochi.Core.IServices
{
    public interface IOpenDataRoadService: IService<OpenDataRoad, OpenDataRoad, string>
    {
        void InsertRange(List<OpenDataRoad> openDataRoads);
        IEnumerable<dynamic> GetAllByField(string fieldName);
        IEnumerable<dynamic> GetStreetByQuery(string county, string site_id, string query);
    }
}
