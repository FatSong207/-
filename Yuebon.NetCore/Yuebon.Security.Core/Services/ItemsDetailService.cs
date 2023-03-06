using System;
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
    /// <summary>
    /// 
    /// </summary>
    public class ItemsDetailService: BaseService<ItemsDetail, ItemsDetailOutputDto, string>, IItemsDetailService
    {
        private readonly IItemsDetailRepository _repository;
        private readonly ILogService _logService;
        private readonly IItemsService itemsService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logService"></param>
        public ItemsDetailService(IItemsDetailRepository repository, ILogService logService, IItemsService _itemsService) : base(repository)
        {
            _repository = repository;
            _logService = logService;
            itemsService = _itemsService;
        }

        /// <summary>
        /// 根據數據字典分類編碼獲取該分類可用內容
        /// </summary>
        /// <param name="itemCode">分類編碼</param>
        /// <returns></returns>
        public async Task<List<ItemsDetailOutputDto>> GetItemDetailsByItemCode(string itemCode)
        {
            string where = string.Empty;
            if (!string.IsNullOrEmpty(itemCode))
            {
                where = string.Format("EnCode='{0}'", itemCode);
            }
            Items items = await itemsService.GetByEnCodAsynce(itemCode);
            List<ItemsDetailOutputDto> resultList = new List<ItemsDetailOutputDto>();
            if (items != null)
            {
                where = string.Format("ItemId='{0}'", items.Id);
                IEnumerable<ItemsDetail> list = _repository.GetAllByIsNotDeleteAndEnabledMark(where);
                resultList = list.OrderBy(t => t.SortCode).MapTo<ItemsDetailOutputDto>();
            }
            return resultList;
        }


        /// <summary>
        /// 獲取適用于Vue 樹形列表
        /// </summary>
        /// <param name="itemId">類別Id</param>
        /// <returns></returns>
        public async Task<List<ItemsDetailOutputDto>> GetAllItemsDetailTreeTable(string itemId)
        {
            string where = "1=1";
            List<ItemsDetailOutputDto> reslist = new List<ItemsDetailOutputDto>();
            where += " and ItemId='" + itemId + "'";
            IEnumerable<ItemsDetail> elist = await _repository.GetListWhereAsync(where);
            List<ItemsDetail> list = elist.OrderBy(t => t.SortCode).ToList();
            List<ItemsDetail> oneMenuList = list.FindAll(t => t.ParentId == "");
            foreach (ItemsDetail item in oneMenuList)
            {
                ItemsDetailOutputDto menuTreeTableOutputDto = new ItemsDetailOutputDto();
                menuTreeTableOutputDto = item.MapTo<ItemsDetailOutputDto>();
                menuTreeTableOutputDto.Children = GetSubMenus(list, item.Id).ToList<ItemsDetailOutputDto>();
                reslist.Add(menuTreeTableOutputDto);
            }
            return reslist;
        }


        /// <summary>
        /// 獲取子菜單，遞歸調用
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parentId">父級Id</param>
        /// <returns></returns>
        private List<ItemsDetailOutputDto> GetSubMenus(List<ItemsDetail> data, string parentId)
        {
            List<ItemsDetailOutputDto> list = new List<ItemsDetailOutputDto>();
            ItemsDetailOutputDto menuTreeTableOutputDto = new ItemsDetailOutputDto();
            var ChilList = data.FindAll(t => t.ParentId == parentId);
            foreach (ItemsDetail entity in ChilList)
            {
                menuTreeTableOutputDto = entity.MapTo<ItemsDetailOutputDto>();
                menuTreeTableOutputDto.Children = GetSubMenus(data, entity.Id).OrderBy(t => t.SortCode).MapTo<ItemsDetailOutputDto>();
                list.Add(menuTreeTableOutputDto);
            }
            return list;
        }
    }
}