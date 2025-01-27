﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Json;
using Yuebon.Commons.Services;
using Yuebon.Security.Dtos;
using Yuebon.Security.IRepositories;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;

namespace Yuebon.Security.Services
{
    /// <summary>
    /// 地區信息
    /// </summary>
    public class AreaService: BaseService<Area, AreaOutputDto, string>, IAreaService
    {
        private readonly IAreaRepository _repository;
        private readonly ILogService _logService;
        public AreaService(IAreaRepository repository, ILogService logService) : base(repository)
        {
            _repository = repository;
            _logService = logService;
        }


        #region 用于uniapp下拉選項
        /// <summary>
        /// 獲取所有可用的地區，用于uniapp下拉選項
        /// </summary>
        /// <returns></returns>
        public List<AreaPickerOutputDto> GetAllByEnable()
        {
            List<AreaPickerOutputDto> list = new List<AreaPickerOutputDto>();
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            list = JsonConvert.DeserializeObject<List<AreaPickerOutputDto>>(yuebonCacheHelper.Get("Area_Enable_Uniapp").ToJson());
            if (list == null || list.Count <= 0)
            {
                List<Area> listFunction = _repository.GetAllByIsNotDeleteAndEnabledMark("Layers in (0,1,2)").OrderBy(t => t.SortCode).ToList();
                list = UniappViewJson(listFunction, "");
                yuebonCacheHelper.Add("Area_Enable_Uniapp", list);
            }
            return list;
        }
        /// <summary>
        /// 獲取省、市、縣/區三級可用的地區，用于uniapp下拉選項
        /// </summary>
        /// <returns></returns>
        public List<AreaPickerOutputDto> GetProvinceToAreaByEnable()
        {
            List<AreaPickerOutputDto> list = new List<AreaPickerOutputDto>();
            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            list = JsonConvert.DeserializeObject<List<AreaPickerOutputDto>>(yuebonCacheHelper.Get("Area_ProvinceToArea_Enable_Uniapp").ToJson());
            if (list == null || list.Count <= 0)
            {
                List<Area> listFunctionTemp = _repository.GetAllByIsNotDeleteAndEnabledMark("Layers in (1,2,3)").OrderBy(t => t.Id).ToList();
                List<Area> listFunction = new List<Area>();
                foreach (Area item in listFunctionTemp)
                {
                    if (item.Layers == 1) { item.ParentId = ""; }
                    listFunction.Add(item);
                }

                list = UniappViewJson(listFunction, "");
                yuebonCacheHelper.Add("Area_ProvinceToArea_Enable_Uniapp", list);
            }
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<AreaPickerOutputDto> UniappViewJson(List<Area> data, string parentId)
        {
            List<AreaPickerOutputDto> list = new List<AreaPickerOutputDto>();
            var ChildNodeList = data.FindAll(t => t.ParentId == parentId).ToList();
            foreach (Area entity in ChildNodeList)
            {
                AreaPickerOutputDto treeViewModel = new AreaPickerOutputDto();
                treeViewModel.value = entity.Id;
                treeViewModel.label = entity.FullName;
                treeViewModel.children = ChildrenUniappViewList(data, entity.Id);
                list.Add(treeViewModel);
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<AreaPickerOutputDto> ChildrenUniappViewList(List<Area> data, string parentId)
        {
            List<AreaPickerOutputDto> listChildren = new List<AreaPickerOutputDto>();
            var ChildNodeList = data.FindAll(t => t.ParentId == parentId).ToList();
            foreach (Area entity in ChildNodeList)
            {
                AreaPickerOutputDto treeViewModel = new AreaPickerOutputDto();
                treeViewModel.value = entity.Id;
                treeViewModel.label = entity.FullName;
                treeViewModel.children = ChildrenUniappViewList(data, entity.Id);
                listChildren.Add(treeViewModel);
            }
            return listChildren;
        }
        #endregion
    }
}