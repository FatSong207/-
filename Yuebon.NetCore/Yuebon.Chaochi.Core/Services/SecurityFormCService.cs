using System;
using System.Data.Common;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Repositories;
using Yuebon.Commons.Services;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Models;
using System.Data;
using Yuebon.Chaochi.Dtos;
using System.Collections.Generic;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Mapping;
using System.Threading.Tasks;
using Yuebon.Commons.Extend;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Enums;
using Yuebon.Chaochi.Core.Models;
using Yuebon.Chaochi.Core.Dtos;
using Yuebon.Commons.Json;
using System.IO;
using System.Linq;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class SecurityFormCService : BaseService<SecurityFormC, SecurityFormCOutputDto, string>, ISecurityFormCService
    {
        private readonly ISecurityFormCRepository _repository;
        //private readonly ICustomerLNService _customerLNService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public SecurityFormCService(ISecurityFormCRepository repository) : base(repository)
        {
            _repository = repository;
            //_customerLNService = customerLNService;
        }


        /// <summary>
        /// 根據Id取得此ID所有圖檔
        /// </summary>
        /// <param name="id">主鍵</param>
        /// <returns></returns>
        public async Task<FileInfoOutputDto> GetImgOutDtoAsync(string id)
        {
            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Security.Models.SysSetting>();

            var dbfiles = _repository.GetListWhere($"BuildingId='{id}'");

            var path = $"{sysSetting.ChaochiFilepath}";
            string uploadPath = @$"{path}BuildingImage\{id}\SecurityFormC\";
            if (System.IO.Directory.Exists(uploadPath)) {
                //DirectoryInfo infoD = new DirectoryInfo(uploadPath);
                //List<FileInfo> files = infoD.GetFiles().OrderBy(p => p.CreationTime).ToList<FileInfo>();
                FileInfoOutputDto FileInfoOutputDto = new FileInfoOutputDto {
                    Id = id,
                };
                List<MyFileInfo> myfiles = dbfiles.Select(x => new MyFileInfo {Id=x.Id, FileName = x.fileName,seq=x.seq,title=x.title }).OrderBy(p=>Convert.ToInt32(p.seq)).ToList();
                FileInfoOutputDto.BuildingImages = myfiles;

                return FileInfoOutputDto;
            } else {
                return new FileInfoOutputDto();
            }
        }
        //public async Task<PageResult<HistoryFormLNOutputDto>> FindWithPagerSearchAsync(SearchHistoryFormLNModel search)
        //{
        //    bool order = search.Order == "asc" ? false : true;
        //    string where = GetDataPrivilege(false);
        //    if (!string.IsNullOrEmpty(search.Keywords)) {
        //        var curLNID = _customerLNService.Get(search.Keywords).LNID;
        //        where += $"and IDNo='{curLNID}'";
        //    }
        //    if (search.Filter is not null) {
        //        if (!string.IsNullOrEmpty(search.Filter.Note)) {
        //            search.Filter.Note = search.Filter.Note.Replace(",", "' and '");
        //            where += $" and UploadTime between '{search.Filter.Note} 23:59:59 '";
        //        }
        //    }
        //    PagerInfo pagerInfo = new PagerInfo {
        //        CurrenetPageIndex = search.CurrenetPageIndex,
        //        PageSize = search.PageSize
        //    };
        //    List<HistoryFormLN> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
        //    List<HistoryFormLNOutputDto> resultList = list.MapTo<HistoryFormLNOutputDto>();
        //    List<HistoryFormLNOutputDto> listResult = new List<HistoryFormLNOutputDto>();
        //    foreach (HistoryFormLNOutputDto item in resultList) {
        //        listResult.Add(item);
        //    }
        //    PageResult<HistoryFormLNOutputDto> pageResult = new PageResult<HistoryFormLNOutputDto> {
        //        CurrentPage = pagerInfo.CurrenetPageIndex,
        //        Items = listResult,
        //        ItemsPerPage = pagerInfo.PageSize,
        //        TotalItems = pagerInfo.RecordCount
        //    };
        //    return pageResult;
        //}
    }
}