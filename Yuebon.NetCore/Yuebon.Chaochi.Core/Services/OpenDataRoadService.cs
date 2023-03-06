using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuebon.Chaochi.Core.IRepositories;
using Yuebon.Chaochi.Core.IServices;
using Yuebon.Chaochi.Core.Models;
using Yuebon.Commons.IRepositories;
using Yuebon.Commons.Services;

namespace Yuebon.Chaochi.Core.Services
{
    public class OpenDataRoadService : BaseService<OpenDataRoad, OpenDataRoad, string>, IOpenDataRoadService
    {
        private IOpenDataRoadRepository _Repository;

        public OpenDataRoadService(IOpenDataRoadRepository iRepository) : base(iRepository)
        {
            _Repository = iRepository;
        }

        public IEnumerable<dynamic> GetAllByField(string fieldName)
        {
            return _Repository.GetAllByField(fieldName);
        }

        public IEnumerable<dynamic> GetStreetByQuery(string county, string site_id, string query)
        {
            return _Repository.GetStreetByQuery(county, site_id, query);
        }

        public void InsertRange(List<OpenDataRoad> openDataRoads)
        {
            //var count=_Repository.DeleteBySql("delete from OpenDataRoad");
            _Repository.InsertRange(openDataRoads);
        }
    }
}
