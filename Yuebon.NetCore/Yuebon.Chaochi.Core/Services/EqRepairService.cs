using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Models;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Json;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Services;

namespace Youbon.Chaochi.Services
{
    /// <summary>
    /// 服務接口實現
    /// </summary>
    public class EqRepairService : BaseService<EqRepair, EqRepairOutputDto, string>, IEqRepairService
    {
        private readonly IEqRepairRepository _repository;
        private readonly IEqRepairDetailRepository _eqRepairDetailRepository;

        public EqRepairService(IEqRepairRepository repository,IEqRepairDetailRepository eqRepairDetailRepository) : base(repository)
        {
            _repository = repository;
            _eqRepairDetailRepository = eqRepairDetailRepository;
        }

        /// <summary>
        /// 修繕明細頁
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<EqRepairOutputDto> GetOutDtoAsync(string id)
        {
            var info = await _repository.GetAsync(id);
            var result = info.MapTo<EqRepairOutputDto>();
            var list =await _eqRepairDetailRepository.GetListWhereAsync($" EqRepairId = '{result.Id}' ");
            result.eqRepairDetails = list.ToList();
            return result;
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public async Task<PageResult<EqRepairOutputDto>> FindWithPagerAsync(SearchInputDto<EqRepair> search)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(false);
            var filter = search.Filter;
            if (filter is not null) {
                if (!string.IsNullOrEmpty(filter.State)) {
                    where += $" and State = '{filter.State}' ";
                }
                if (!string.IsNullOrEmpty(filter.Guid)) {
                    where += $" and Guid like '%{filter.Guid}%' ";
                }
                if (!string.IsNullOrEmpty(filter.ApplicationDate)) {
                    where += $" and ApplicationDate like '{filter.ApplicationDate}%' ";
                }
                if (!string.IsNullOrEmpty(filter.EndCaseDate)) {
                    where += $" and EndCaseDate like '{filter.EndCaseDate}%' ";
                }
                if (!string.IsNullOrEmpty(filter.BAdd)) {
                    where += $" and BAdd like '%{filter.BAdd}%' ";
                }
            }

            PagerInfo pagerInfo = new PagerInfo {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<EqRepair> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            List<EqRepairOutputDto> resultList = list.MapTo<EqRepairOutputDto>();
            List<EqRepairOutputDto> listResult = new List<EqRepairOutputDto>();
            foreach (EqRepairOutputDto item in resultList) {
                listResult.Add(item);
            }
            PageResult<EqRepairOutputDto> pageResult = new PageResult<EqRepairOutputDto> {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = listResult,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }

        /// <summary>
        /// 異步查詢單個實體。
        /// </summary>
        /// <param name="id">主鍵</param>
        /// <returns></returns>
        public async Task<FileInfoOutputDto> GetImgOutDtoAsync(string id)
        {
            Yuebon.Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Yuebon.Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();

            var dbfiles = _eqRepairDetailRepository.GetListWhere($"Id='{id}'");

            var path = $"{sysSetting.ChaochiFilepath}";
            string uploadPath = @$"{path}EqRepair\{dbfiles.FirstOrDefault().EqRepairId}\{id}";
            if (System.IO.Directory.Exists(uploadPath)) {
                //DirectoryInfo infoD = new DirectoryInfo(uploadPath);
                //List<FileInfo> files = infoD.GetFiles().OrderBy(p => p.CreationTime).ToList<FileInfo>();
                FileInfoOutputDto FileInfoOutputDto = new FileInfoOutputDto {
                    Id = id,
                };
                List<EqFileInfo> myfiles = dbfiles.Select(x => new EqFileInfo { BeforeRepairClose = x.BeforeRepairClose, BeforeRepairFar=x.BeforeRepairFar, RepairingClose=x.RepairingClose, RepairingFar=x.RepairingFar, AfterRepairClose=x.AfterRepairClose, AfterRepairFar=x.AfterRepairFar}).ToList();
                FileInfoOutputDto.EqRepairImages = myfiles;

                return FileInfoOutputDto;
            } else {
                return new FileInfoOutputDto();
            }
        }
    }
}