using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Yuebon.Commons.Core.Dtos;
using Yuebon.Commons.Extend;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;
using Yuebon.Commons.Services;
using Yuebon.Security.Dtos;
using Yuebon.Security.IRepositories;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;

namespace Yuebon.Security.Services
{
    /// <summary>
    /// 菜單
    /// </summary>
    public class MenuService: BaseService<Menu, MenuOutputDto, string>, IMenuService
    {
        private readonly IMenuRepository _MenuRepository;
        private readonly IUserRepository userRepository;
        private readonly ISystemTypeRepository systemTypeRepository;
        private readonly IRoleAuthorizeRepository roleAuthorizeRepository;
        private readonly ILogService _logService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="_userRepository"></param>
        /// <param name="_roleAuthorizeRepository"></param>
        /// <param name="_systemTypeRepository"></param>
        /// <param name="logService"></param>
        public MenuService(IMenuRepository repository,IUserRepository _userRepository, IRoleAuthorizeRepository _roleAuthorizeRepository, ISystemTypeRepository _systemTypeRepository, ILogService logService) : base(repository)
        {
            _MenuRepository = repository;
            userRepository = _userRepository;
            roleAuthorizeRepository = _roleAuthorizeRepository;
            systemTypeRepository = _systemTypeRepository;
            _logService = logService;
        }

        /// <summary>
        /// 根據用戶獲取功能菜單
        /// </summary>
        /// <param name="userId">用戶ID</param>
        /// <returns></returns>
        public List<Menu> GetMenuByUser(string userId)
        {
            List<Menu> result = new List<Menu>();
            List<Menu> allMenuls = new List<Menu>();
            List<Menu> subMenuls = new List<Menu>();
            string where = string.Format("Layers=1");
            IEnumerable<Menu> allMenus = _MenuRepository.GetAllByIsNotDeleteAndEnabledMark();
            allMenuls = allMenus.ToList();
            if (userId == string.Empty) //超級管理員
            {
                return allMenuls;
            }
            var user = userRepository.Get(userId);
            if (user == null)
                return result;
            var userRoles = user.RoleId;
            where = string.Format("ItemType = 1 and ObjectType = 1 and ObjectId='{0}'",userRoles);
            var Menus = roleAuthorizeRepository.GetListWhere(where);
            foreach (RoleAuthorize item in Menus)
            {
                Menu MenuEntity = allMenuls.Find(t => t.Id == item.ItemId);
                if (MenuEntity != null)
                {
                    result.Add(MenuEntity);
                }
            }
            return result.OrderBy(t => t.SortCode).ToList();
        }


        /// <summary>
        /// 獲取功能菜單適用于Vue 樹形列表
        /// </summary>
        /// <param name="systemTypeId">子系統Id</param>
        /// <returns></returns>
        public async Task<List<MenuTreeTableOutputDto>> GetAllMenuTreeTable(string systemTypeId)
        {
            string where = "1=1";
            List<MenuTreeTableOutputDto> reslist = new List<MenuTreeTableOutputDto>();
            if (!string.IsNullOrEmpty(systemTypeId))
            {
                IEnumerable<Menu> elist = await _MenuRepository.GetListWhereAsync("SystemTypeId='" + systemTypeId + "'");
                List<Menu> list = elist.OrderBy(t => t.SortCode).ToList();
                List<Menu> oneMenuList = list.FindAll(t => t.ParentId == "");
                foreach (Menu item in oneMenuList)
                {
                    MenuTreeTableOutputDto menuTreeTableOutputDto = new MenuTreeTableOutputDto();
                    menuTreeTableOutputDto = item.MapTo<MenuTreeTableOutputDto>();
                    menuTreeTableOutputDto.Children = GetSubMenus(list, item.Id).ToList<MenuTreeTableOutputDto>();
                    reslist.Add(menuTreeTableOutputDto);
                }

            }
            else
            {
                IEnumerable<SystemType> listSystemType = await systemTypeRepository.GetListWhereAsync(where);

                foreach (SystemType systemType in listSystemType)
                {
                    MenuTreeTableOutputDto menuTreeTableOutputDto = new MenuTreeTableOutputDto();
                    menuTreeTableOutputDto.Id = systemType.Id;
                    menuTreeTableOutputDto.FullName = systemType.FullName;
                    menuTreeTableOutputDto.EnCode = systemType.EnCode;
                    menuTreeTableOutputDto.UrlAddress = systemType.Url;
                    menuTreeTableOutputDto.EnabledMark = systemType.EnabledMark;

                    menuTreeTableOutputDto.SystemTag = true;

                    IEnumerable<Menu> elist = await _MenuRepository.GetListWhereAsync("SystemTypeId='" + systemType.Id + "'");
                    if (elist.Count() > 0)
                    {
                        List<Menu> list = elist.OrderBy(t => t.SortCode).ToList();
                        menuTreeTableOutputDto.Children = GetSubMenus(list, "").ToList<MenuTreeTableOutputDto>();
                    }
                    reslist.Add(menuTreeTableOutputDto);
                }
            }
            return reslist;
        }


        /// <summary>
        /// 獲取子菜單，遞歸調用
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parentId">父級Id</param>
        /// <returns></returns>
        private List<MenuTreeTableOutputDto> GetSubMenus(List<Menu> data, string parentId)
        {
            List<MenuTreeTableOutputDto> list = new List<MenuTreeTableOutputDto>();
            MenuTreeTableOutputDto menuTreeTableOutputDto = new MenuTreeTableOutputDto();
            var ChilList = data.FindAll(t => t.ParentId == parentId);
            foreach (Menu entity in ChilList)
            {
                menuTreeTableOutputDto = entity.MapTo<MenuTreeTableOutputDto>();
                menuTreeTableOutputDto.Children = GetSubMenus(data, entity.Id).OrderBy(t => t.SortCode).MapTo<MenuTreeTableOutputDto>();
                list.Add(menuTreeTableOutputDto);
            }
            return list;
        }

        /// <summary>
        /// 根據角色ID字符串（逗號分開)和系統類型ID，獲取對應的操作功能列表
        /// </summary>
        /// <param name="roleIds">角色ID</param>
        /// <param name="typeID">系統類型ID</param>
        /// <param name="isMenu">是否是菜單</param>
        /// <returns></returns>
        public List<Menu> GetFunctions(string roleIds, string typeID,bool isMenu=false)
        {
            return _MenuRepository.GetFunctions(roleIds, typeID, isMenu).ToList();
        }


        /// <summary>
        /// 根據系統類型ID，獲取對應的操作功能列表
        /// </summary>
        /// <param name="typeID">系統類型ID</param>
        /// <returns></returns>
        public List<Menu> GetFunctions(string typeID)
        {
            return _MenuRepository.GetFunctions(typeID).ToList();
        }


        /// <summary>
        /// 根據父級功能編碼查詢所有子集功能，主要用于頁面操作按鈕權限
        /// </summary>
        /// <param name="enCode">菜單功能編碼</param>
        /// <returns></returns>
        public async Task<IEnumerable<MenuOutputDto>> GetListByParentEnCode(string enCode)
        {
            string where = string.Format("EnCode='{0}'", enCode);
            Menu function = await repository.GetWhereAsync(where);
            where = string.Format("ParentId='{0}'", function.ParentId);
            IEnumerable<Menu> list = await repository.GetAllByIsNotEnabledMarkAsync(where);
            return list.MapTo<MenuOutputDto>().ToList();
        }
        /// <summary>
        /// 按條件批量刪除
        /// </summary>
        /// <param name="idsInfo">主鍵Id集合</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public CommonResult DeleteBatchWhere(DeletesInputDto idsInfo, IDbTransaction trans = null)
        {
            CommonResult result = new CommonResult();
            string where = string.Empty;
            for (int i = 0; i < idsInfo.Ids.Length; i++)
            {
                if (idsInfo.Ids[0] != null)
                {
                    where = string.Format("ParentId='{0}'", idsInfo.Ids[0]);
                    IEnumerable<Menu> list = _MenuRepository.GetListWhere(where);
                    if (list.Count()>0)
                    {
                        result.ErrMsg = "功能存在子集數據，不能刪除";
                        return result;
                    }
                }
            }
            where = "id in ('" + idsInfo.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            bool bl = repository.DeleteBatchWhere(where);
            if (bl)
            {
                result.ErrCode ="0";
            }
            return result;
        }

        /// <summary>
        /// 按條件批量刪除
        /// </summary>
        /// <param name="idsInfo">主鍵Id集合</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public async Task<CommonResult> DeleteBatchWhereAsync(DeletesInputDto idsInfo,IDbTransaction trans = null)
        {
            CommonResult result = new CommonResult();
            string where =string.Empty;
            for (int i =0;i< idsInfo.Ids.Length;i++)
            {
                if (idsInfo.Ids[0].ToString().Length>0)
                {
                    where = string.Format("ParentId='{0}'", idsInfo.Ids[0]);
                    IEnumerable<Menu> list = _MenuRepository.GetListWhere(where);
                    if (list.Count()>0)
                    {
                        result.ErrMsg = "功能存在子集數據，不能刪除";
                        return result;
                    }
                }
            }
            where = "id in ('" + idsInfo.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            bool bl = await repository.DeleteBatchWhereAsync(where);
            if (bl)
            {
                result.ErrCode = "0";
            }
            return result;
        }
    }
}