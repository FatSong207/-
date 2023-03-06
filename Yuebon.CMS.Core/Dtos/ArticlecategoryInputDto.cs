using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Models;
using Yuebon.Commons.Dtos;
using Yuebon.CMS.Models;

namespace Yuebon.CMS.Dtos
{
    /// <summary>
    /// 文章分類輸入對象模型
    /// </summary>
    [AutoMap(typeof(Articlecategory))]
    [Serializable]
    public class ArticlecategoryInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取主鍵
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取標題
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 設置或獲取父級Id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 設置或獲取全路徑
        /// </summary>
        public string ClassPath { get; set; }

        /// <summary>
        /// 設置或獲取層級
        /// </summary>
        public int? ClassLayer { get; set; }

        /// <summary>
        /// 設置或獲取排序
        /// </summary>
        public int SortCode { get; set; }

        /// <summary>
        /// 設置或獲取描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 設置或獲取外鏈地址
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// 設置或獲取主圖圖片
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 設置或獲取SEO標題
        /// </summary>
        public string SeoTitle { get; set; }

        /// <summary>
        /// 設置或獲取SEO關鍵詞
        /// </summary>
        public string SeoKeywords { get; set; }

        /// <summary>
        /// 設置或獲取SEO描述
        /// </summary>
        public string SeoDescription { get; set; }

        /// <summary>
        /// 設置或獲取是否熱門
        /// </summary>
        public bool? IsHot { get; set; }

        /// <summary>
        /// 設置或獲取是否可用
        /// </summary>
        public bool? EnabledMark { get; set; }


    }
}
