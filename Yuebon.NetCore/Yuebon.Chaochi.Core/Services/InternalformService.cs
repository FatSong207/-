using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Models;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Services;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class InternalformService : BaseService<Internalform, InternalformOutputDto, string>, IInternalformService
    {
        private readonly IInternalformRepository _internalRepos;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="userLogOnRepository"></param>
        /// <param name="logService"></param>
        /// <param name="roleService"></param>
        /// <param name="organizeService"></param>
        public InternalformService(IInternalformRepository internalRepos)
            : base(internalRepos)
        {
            _internalRepos = internalRepos;
        }

        public async Task<PageResult<InternalformOutputDto>> FindFormListWithPage(SearchInternalformModel search)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(false);
            var filter = search.Filter;

            if (!string.IsNullOrEmpty(search.Keywords)) {
                //if (search.Keywords == "Y") { //用於線上表單管理列表(internalformmt)
                //    where += string.Format(" and HasPDFTemplate = 'Y' ");}
                if (search.Keywords == "Info") { //用於基本資料表(internalform)
                    where += string.Format(" and ListType = 'Info' ");
                }else if (search.Keywords =="Normal") { //用於一般表單(internalformnormal)
                    where += string.Format(" and ListType = 'Normal'  and FormId not in ('B031201','B031301','A000501','A000601')");
                }else if (search.Keywords == "Security") { //用於租安表單
                    where += string.Format(" and ListType = 'Security' ");
                }
                
            }

            if (!string.IsNullOrEmpty(filter.FormId)) {
                where += string.Format(" and FormId like '%{0}%'", filter.FormId);
            }
            if (!string.IsNullOrEmpty(filter.FormName)) {
                where += string.Format(" and FormName like '%{0}%'", filter.FormName);
            }
            if (!string.IsNullOrEmpty(filter.Dept)) {
                where += string.Format(" and Dept like '%{0}%'", filter.Dept);
            }
            if (!string.IsNullOrEmpty(filter.Type)) {
                where += string.Format(" and Type like '%{0}%'", filter.Type);
            }
            if (!string.IsNullOrEmpty(search.IsSign)) {
                where += string.Format(" and IsSign = '{0}' ", search.IsSign == "Y" ? "1" : "0");
            }
            //if (!string.IsNullOrEmpty(search.Keywords)) {
            //    where += $" and FormId {search.Keywords} ";
            //}
            //if (!string.IsNullOrEmpty(search.CreatorTime1))
            //{
            //    where += " and CreatorTime >='" + search.CreatorTime1 + " 00:00:00'";
            //}
            //if (!string.IsNullOrEmpty(search.CreatorTime2))
            //{
            //    where += " and CreatorTime <='" + search.CreatorTime2 + " 23:59:59'";
            //}
            PagerInfo pagerInfo = new PagerInfo {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<Internalform> list = await _internalRepos.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            List<InternalformOutputDto> resultList = list.MapTo<InternalformOutputDto>();
            /*
            List<BuildingOutputDto> listResult = new List<BuildingOutputDto>();
            foreach (BuildingOutputDto item in resultList)
            {
                //if (!string.IsNullOrEmpty(item.OrganizeId))
                //{
                //    item.OrganizeName = _organizeService.Get(item.OrganizeId).FullName;
                //}
                //if (!string.IsNullOrEmpty(item.RoleId))
                //{
                //    item.RoleName = _roleService.GetRoleNameStr(item.RoleId);
                //}
                //if (!string.IsNullOrEmpty(item.DepartmentId))
                //{
                //    item.DepartmentName = _organizeService.Get(item.DepartmentId).FullName;
                //}
                //if (!string.IsNullOrEmpty(item.DutyId))
                //{
                //    item.DutyName = _roleService.Get(item.DutyId).FullName;
                //}

                //where = GetDataPrivilege(false);
                //where += string.Format(" and BuildingId = '{0}'", item.Id);

                //List<BuildingBelonging> BuildingBelongings = await _buildingBelongingRepository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
                //item.BuildingBelongings = BuildingBelongings;

                listResult.Add(item);
            }
            */
            PageResult<InternalformOutputDto> pageResult = new PageResult<InternalformOutputDto> {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = resultList,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }

        public async Task<Internalform> GetByFormId(string formId)
        {
            return await _internalRepos.GetByFormId(formId);
        }
    }
}