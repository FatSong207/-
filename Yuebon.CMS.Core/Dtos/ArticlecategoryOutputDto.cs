using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.CMS.Dtos
{
    /// <summary>
    /// 文章分類輸出對象模型
    /// </summary>
    [Serializable]
    public class ArticlecategoryOutputDto
    {
        /// <summary>
        /// 設置或獲取主鍵
        /// </summary>
        [MaxLength(50)]
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取標題
        /// </summary>
        [MaxLength(100)]
        public string Title { get; set; }

        /// <summary>
        /// 設置或獲取父級Id
        /// </summary>
        [MaxLength(50)]
        public string ParentId { get; set; }

        /// <summary>
        /// 設置或獲取全路徑
        /// </summary>
        [MaxLength(500)]
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
        [MaxLength(255)]
        public string LinkUrl { get; set; }

        /// <summary>
        /// 設置或獲取主圖圖片
        /// </summary>
        [MaxLength(255)]
        public string ImgUrl { get; set; }

        /// <summary>
        /// 設置或獲取SEO標題
        /// </summary>
        [MaxLength(255)]
        public string SeoTitle { get; set; }

        /// <summary>
        /// 設置或獲取SEO關鍵詞
        /// </summary>
        [MaxLength(255)]
        public string SeoKeywords { get; set; }

        /// <summary>
        /// 設置或獲取SEO描述
        /// </summary>
        [MaxLength(255)]
        public string SeoDescription { get; set; }

        /// <summary>
        /// 設置或獲取是否熱門
        /// </summary>
        public bool? IsHot { get; set; }

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
        [MaxLength(50)]
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 設置或獲取所屬公司
        /// </summary>
        [MaxLength(50)]
        public string CompanyId { get; set; }

        /// <summary>
        /// 設置或獲取所屬部門
        /// </summary>
        [MaxLength(50)]
        public string DeptId { get; set; }

        /// <summary>
        /// 設置或獲取最近修改時間
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 設置或獲取最近修改人
        /// </summary>
        [MaxLength(50)]
        public string LastModifyUserId { get; set; }

        /// <summary>
        /// 設置或獲取刪除時間
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 設置或獲取刪除人
        /// </summary>
        [MaxLength(50)]
        public string DeleteUserId { get; set; }

        /// <summary>
        /// 獲取子集
        /// </summary>
        public List<ArticlecategoryOutputDto> Children { get; set; }
    }
}
