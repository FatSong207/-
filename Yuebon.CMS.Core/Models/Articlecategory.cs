using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.CMS.Models
{
    /// <summary>
    /// 文章分類，數據實體對象
    /// </summary>
    [Table("cms_articlecategory")]
    [Serializable]
    public class Articlecategory:BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
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
        public int? SortCode { get; set; }


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
        /// 設置或獲取描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 設置或獲取是否可用
        /// </summary>
        public bool? EnabledMark { get; set; }

        /// <summary>
        /// 設置或獲取刪除標志
        /// </summary>
        public bool? DeleteMark { get; set; }

        /// <summary>
        /// 設置或獲取創建時間
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 設置或獲取創建人
        /// </summary>
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 設置或獲取所屬公司
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 設置或獲取所屬部門
        /// </summary>
        public string DeptId { get; set; }

        /// <summary>
        /// 設置或獲取最近修改時間
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 設置或獲取最近修改人
        /// </summary>
        public string LastModifyUserId { get; set; }

        /// <summary>
        /// 設置或獲取刪除時間
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 設置或獲取刪除人
        /// </summary>
        public string DeleteUserId { get; set; }


    }
}
