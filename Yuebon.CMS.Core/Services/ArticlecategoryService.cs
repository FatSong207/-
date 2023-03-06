using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Services;
using Yuebon.CMS.IRepositories;
using Yuebon.CMS.IServices;
using Yuebon.CMS.Dtos;
using Yuebon.CMS.Models;
using System.Linq;
using Yuebon.Commons.Core.Dtos;
using Yuebon.Commons.Models;
using System.Data;
using Yuebon.Commons.Extensions;

namespace Yuebon.CMS.Services
{
    /// <summary>
    /// 文章分類服務接口實現
    /// </summary>
    public class ArticlecategoryService: BaseService<Articlecategory,ArticlecategoryOutputDto, string>, IArticlecategoryService
    {
		private readonly IArticlecategoryRepository _repository;
        private readonly IArticlenewsRepository _articleRepository;
        public ArticlecategoryService(IArticlecategoryRepository repository, IArticlenewsRepository articleRepository) : base(repository)
        {
			_repository=repository;
            _articleRepository = articleRepository;
        }

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public override async Task<PageResult<ArticlecategoryOutputDto>> FindWithPagerAsync(SearchInputDto<Articlecategory> search, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege();
            if (search.Keywords is { Length: > 0 })
            {
                where += string.Format(" and  Title '%{0}%'", search.Keywords);
            };
            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<Articlecategory> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            PageResult<ArticlecategoryOutputDto> pageResult = new PageResult<ArticlecategoryOutputDto>
            {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = list.MapTo<ArticlecategoryOutputDto>(),
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }


        /// <summary>
        /// 獲取分類適用于Vue 樹形列表，關鍵詞為空時獲取所有
        /// </summary>
        /// <param name="keyword">名稱關鍵詞</param>
        /// <returns></returns>
        public async Task<List<ArticlecategoryOutputDto>> GetAllArticlecategoryTreeTable(string keyword)
        {
            List<ArticlecategoryOutputDto> reslist = new List<ArticlecategoryOutputDto>();
            string where = "1=1";
            if(keyword is { Length: > 0 })
            {
                where = string.Format("Title like '%{0}%'", keyword);
            }
            where += " order by ClassLayer,SortCode";
            IEnumerable<Articlecategory> elist = await _repository.GetListWhereAsync(where);
            if (elist.Count() >0)
            {
                List<Articlecategory> list = elist.ToList();
                var ChilList = list.FindAll(t => t.ParentId == "");
                if (ChilList.Count==0)
                {
                    Articlecategory articlecategoryOutputDto = elist.FirstOrDefault<Articlecategory>();
                    reslist = GetSubArticlecategorys(list, articlecategoryOutputDto.ParentId).ToList<ArticlecategoryOutputDto>();
                }
                else
                {
                    reslist = GetSubArticlecategorys(list, "").ToList<ArticlecategoryOutputDto>();
                }
            }
            return reslist;
        }


        /// <summary>
        /// 獲取子集，遞歸調用
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parentId">父級Id</param>
        /// <returns></returns>
        private List<ArticlecategoryOutputDto> GetSubArticlecategorys(List<Articlecategory> data, string parentId)
        {
            List<ArticlecategoryOutputDto> list = new List<ArticlecategoryOutputDto>();
            ArticlecategoryOutputDto articlecategoryOutputDto = new ArticlecategoryOutputDto();
            var ChilList = data.FindAll(t => t.ParentId == parentId);
            foreach (Articlecategory entity in ChilList)
            {
                articlecategoryOutputDto = entity.MapTo<ArticlecategoryOutputDto>();
                articlecategoryOutputDto.Children = GetSubArticlecategorys(data, entity.Id).OrderBy(t => t.SortCode).MapTo<ArticlecategoryOutputDto>();
                list.Add(articlecategoryOutputDto);
            }
            return list;
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
                    IEnumerable<Articlecategory> list = _repository.GetListWhere(where);
                    if (list.Count()>0)
                    {
                        result.ErrMsg = "該分類存在子分類，不能刪除";
                        return result;
                    }

                    where = string.Format("CategoryId='{0}'", idsInfo.Ids[0]);
                    IEnumerable<Articlenews> listArticle = _articleRepository.GetListWhere(where);
                    if (listArticle.Count() > 0)
                    {
                        result.ErrMsg = "該分類有文章數據，不能刪除";
                        return result;
                    }
                }
            }
            where = "id in ('" + idsInfo.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            bool bl = repository.DeleteBatchWhere(where);
            if (bl)
            {
                result.ErrCode = "0";
            }
            return result;
        }

        /// <summary>
        /// 按條件批量刪除
        /// </summary>
        /// <param name="idsInfo">主鍵Id集合</param>
        /// <param name="trans">事務對象</param>
        /// <returns></returns>
        public async Task<CommonResult> DeleteBatchWhereAsync(DeletesInputDto idsInfo, IDbTransaction trans = null)
        {
            CommonResult result = new CommonResult();
            string where = string.Empty;
            for (int i = 0; i < idsInfo.Ids.Length; i++)
            {
                where = string.Format("ParentId='{0}'", idsInfo.Ids[0]);
                IEnumerable<Articlecategory> list = _repository.GetListWhere(where);
                if (list.Count()> 0)
                {
                    result.ErrMsg = "該分類存在子分類，不能刪除";
                    return result;
                }

                where = string.Format("CategoryId='{0}'", idsInfo.Ids[0]);
                IEnumerable<Articlenews> listArticle = _articleRepository.GetListWhere(where);
                if (listArticle.Count()>0)
                {
                    result.ErrMsg = "該分類有文章數據，不能刪除";
                    return result;
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