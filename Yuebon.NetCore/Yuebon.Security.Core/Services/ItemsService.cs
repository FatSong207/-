﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Services;
using Yuebon.Security.Dtos;
using Yuebon.Security.IRepositories;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;

namespace Yuebon.Security.Services
{
    public class ItemsService: BaseService<Items, ItemsOutputDto, string>, IItemsService
    {
        private readonly IItemsRepository _repository;
        private readonly ILogService _logService;
        public ItemsService(IItemsRepository repository, ILogService logService) : base(repository)
        {
            _repository = repository;
            _logService = logService;
        }


        /// <summary>
        /// 獲取功能菜單適用于Vue 樹形列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<ItemsOutputDto>> GetAllItemsTreeTable()
        {
            List<ItemsOutputDto> reslist = new List<ItemsOutputDto>();
            IEnumerable<Items> elist =await _repository.GetListWhereAsync("1=1");
            List<Items> list = elist.OrderBy(t => t.SortCode).ToList();
            List<Items> oneMenuList = list.FindAll(t => t.ParentId == "");
            foreach (Items item in oneMenuList)
            {
                ItemsOutputDto menuTreeTableOutputDto = new ItemsOutputDto();
                menuTreeTableOutputDto = item.MapTo<ItemsOutputDto>();
                menuTreeTableOutputDto.Children = GetSubMenus(list, item.Id).ToList<ItemsOutputDto>();
                reslist.Add(menuTreeTableOutputDto);
            }
            return reslist;
        }

        /// <summary>
        /// 根據編碼查詢字典分類
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public async Task<Items> GetByEnCodAsynce(string enCode)
        {
            return await _repository.GetByEnCodAsynce(enCode);
        }
        /// <summary>
        /// 更新時判斷分類編碼是否存在（排除自己）
        /// </summary>
        /// <param name="enCode">分類編碼</param
        /// <param name="id">主鍵Id</param>
        /// <returns></returns>
        public async Task<Items> GetByEnCodAsynce(string enCode, string id)
        {
            return await _repository.GetByEnCodAsynce(enCode,id);
        }

        /// <summary>
        /// 獲取子菜單，遞歸調用
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parentId">父級Id</param>
        /// <returns></returns>
        private List<ItemsOutputDto> GetSubMenus(List<Items> data, string parentId)
        {
            List<ItemsOutputDto> list = new List<ItemsOutputDto>();
            ItemsOutputDto menuTreeTableOutputDto = new ItemsOutputDto();
            var ChilList = data.FindAll(t => t.ParentId == parentId);
            foreach (Items entity in ChilList)
            {
                menuTreeTableOutputDto = entity.MapTo<ItemsOutputDto>();
                menuTreeTableOutputDto.Children = GetSubMenus(data, entity.Id).OrderBy(t => t.SortCode).MapTo<ItemsOutputDto>();
                list.Add(menuTreeTableOutputDto);
            }
            return list;
        }
    }
}