using System;
using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using Yuebon.Commons.Pages;
using System.Threading.Tasks;
using Yuebon.Commons.Dtos;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義內部表單服務接口
    /// </summary>
    public interface ISecurityFormService:IService<SecurityForm,SecurityFormOutputDto, string>
    {

        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        Task<PageResult<SecurityFormOutputDto>> FindWithPagerSearchAsync(SearchInputDto<SecurityForm> search);
    }
}
