using Yuebon.Commons.Services;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Yuebon.Commons.Mapping;
using System.Linq;
using System.Data;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Dtos;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 服務接口實現
    /// </summary>
    public class MOBuildingService: BaseService<MOBuilding,MOBuildingOutputDto, string>, IMOBuildingService
    {
		private readonly IMOBuildingRepository _repository;
        public MOBuildingService(IMOBuildingRepository repository) : base(repository)
        {
			_repository=repository;
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public override async Task<PageResult<MOBuildingOutputDto>> FindWithPagerAsync(SearchInputDto<MOBuilding> search, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(true);
            var filter = search.Filter;

            if (filter != null) {
                // MOID
                if (!string.IsNullOrWhiteSpace(filter.MOID))
                    where += string.Format(" and MOID like '%{0}%'", filter.MOID);
            }

            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<MOBuilding> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            PageResult<MOBuildingOutputDto> pageResult = new PageResult<MOBuildingOutputDto>
            {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = list.MapTo<MOBuildingOutputDto>(),
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }

        /// <summary>
        /// 根據多物件單號查詢多物件資料
        /// </summary>
        /// <param name="moid">多物件單號</param>
        /// <returns></returns>
        public async Task<List<MOBuildingOutputDto>> GetByMOID(string moid)
        {
            string where = string.Format("MOID = '{0}'", moid);

            List<MOBuildingOutputDto> buildingODtoList = new List<MOBuildingOutputDto>();
            IEnumerable<MOBuilding> buildingList = await _repository.GetListWhereAsync(where);
            if (buildingList.Count() > 0) {
                foreach (MOBuilding building in buildingList) {
                    MOBuildingOutputDto mobOutputDto = building.MapTo<MOBuildingOutputDto>();
                    buildingODtoList.Add(mobOutputDto);
                }
            }

            return buildingODtoList;
        }

        /// <summary>
        /// 根據多物件單號查詢物件資料
        /// </summary>
        /// <param name="moid">多物件單號</param>
        /// <param name="badd">物件地址</param>
        /// <returns></returns>
        public async Task<MOBuildingOutputDto> GetByMOBuilding(string moid, string badd, IDbConnection conn = null, IDbTransaction trans = null)
        {
            string where = string.Format("MOID = '{0}' AND Badd = '{1}'", moid, badd);
            MOBuilding moBuilding = await _repository.GetWhereAsync(where, conn, trans);
            MOBuildingOutputDto moBuildingOutputDto = moBuilding.MapTo<MOBuildingOutputDto>();

            return moBuildingOutputDto;
        }

        /// <summary>
        /// 根據建物地址查詢分租物件資料
        /// </summary>
        /// <param name="BAdd">建物地址</param>
        /// <returns></returns>
        public async Task<MOBuildingOutputDto> GetByBAdd(string BAdd, IDbConnection conn = null, IDbTransaction trans = null)
        {
            string where = string.Format("BAdd = '{0}'", BAdd);

            MOBuilding moBuilding = await _repository.GetWhereAsync(where, conn, trans);
            MOBuildingOutputDto moBuildingOutputDto = moBuilding.MapTo<MOBuildingOutputDto>();

            return moBuildingOutputDto;
        }
    }
}