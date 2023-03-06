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
using System.Data;

namespace Yuebon.CMS.Services
{
    /// <summary>
    /// 文章服務接口實現
    /// </summary>
    public class ArticlenewsService : BaseService<Articlenews, ArticlenewsOutputDto, string>, IArticlenewsService
    {
        private readonly IArticlenewsRepository _repository;
        private readonly IArticlecategoryRepository _articlecategoryRepository;
        public ArticlenewsService(IArticlenewsRepository repository, IArticlecategoryRepository articlecategoryRepository) : base(repository)
        {
            _repository = repository;
            _articlecategoryRepository = articlecategoryRepository;
        }


        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public override async Task<PageResult<ArticlenewsOutputDto>> FindWithPagerAsync(SearchInputDto<Articlenews> search, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege();
            if (search.Keywords is { Length: > 0 })
            {
                where += string.Format(" and  Title like '%{0}%'", search.Keywords);
            };
            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<Articlenews> list = await _repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            PageResult<ArticlenewsOutputDto> pageResult = new PageResult<ArticlenewsOutputDto>
            {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = list.MapTo<ArticlenewsOutputDto>(),
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }

        /// <summary>
        /// 根據用戶角色獲取分類及該分類的文章
        /// </summary>
        /// <returns></returns>
        public async Task<List<CategoryArticleOutputDto>> GetCategoryArticleList()
        {
            string where = "ParentId='2021013118255576392825'";
            IEnumerable<Articlecategory> list = await _articlecategoryRepository.GetAllByIsNotDeleteAndEnabledMarkAsync(where);
            List<CategoryArticleOutputDto> resultList = new List<CategoryArticleOutputDto>();
            foreach (Articlecategory item in list)
            {
                CategoryArticleOutputDto categoryArticleOutputDto = new CategoryArticleOutputDto();
                categoryArticleOutputDto.Id = item.Id;
                categoryArticleOutputDto.Title = item.Title;
                categoryArticleOutputDto.SubTitle = item.SeoTitle;
                categoryArticleOutputDto.ParentId = "";
                where = $"CategoryId='{item.Id}'";
                IEnumerable<Articlenews> articleList = await _repository.GetAllByIsNotDeleteAndEnabledMarkAsync(where);
                List<CategoryArticleOutputDto> subList = new List<CategoryArticleOutputDto>();
                foreach (Articlenews sItem in articleList)
                {
                    CategoryArticleOutputDto subInfo = new CategoryArticleOutputDto();
                    subInfo.Id = sItem.Id;
                    subInfo.Title = sItem.Title;
                    subInfo.SubTitle = sItem.SubTitle;
                    subInfo.ParentId =item.Id;
                    subList.Add(subInfo);
                }
                categoryArticleOutputDto.Children = subList;
                resultList.Add(categoryArticleOutputDto);
            }
            return resultList;
        }
    }
}