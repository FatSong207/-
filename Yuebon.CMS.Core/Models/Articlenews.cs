using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.CMS.Models
{
    /// <summary>
    /// 文章，通知公告，數據實體對象
    /// </summary>
    [Table("cms_articlenews")]
    [Serializable]
    public class Articlenews:BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 設置或獲取文章分類
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// 設置或獲取分類名稱
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 設置或獲取文章標題
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 設置或獲取副標題
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        /// 設置或獲取外鏈
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// 設置或獲取主圖
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
        /// 設置或獲取標簽，多個用逗號隔開
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// 設置或獲取摘要
        /// </summary>
        public string Zhaiyao { get; set; }

        /// <summary>
        /// 設置或獲取詳情
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 設置或獲取排序
        /// </summary>
        public int? SortCode { get; set; }

        /// <summary>
        /// 設置或獲取開啟評論
        /// </summary>
        public bool? IsMsg { get; set; }

        /// <summary>
        /// 設置或獲取是否置頂，默認不置頂
        /// </summary>
        public bool? IsTop { get; set; }

        /// <summary>
        /// 設置或獲取是否推薦
        /// </summary>
        public bool? IsRed { get; set; }

        /// <summary>
        /// 設置或獲取是否熱門，默認否
        /// </summary>
        public bool? IsHot { get; set; }

        /// <summary>
        /// 設置或獲取是否是系統預置文章，不可刪除
        /// </summary>
        public bool? IsSys { get; set; }

        /// <summary>
        /// 設置或獲取是否推薦到最新
        /// </summary>
        public bool? IsNew { get; set; }

        /// <summary>
        /// 設置或獲取是否推薦到幻燈
        /// </summary>
        public bool? IsSlide { get; set; }

        /// <summary>
        /// 設置或獲取點擊量
        /// </summary>
        public int? Click { get; set; }

        /// <summary>
        /// 設置或獲取喜歡量
        /// </summary>
        public int? LikeCount { get; set; }

        /// <summary>
        /// 設置或獲取瀏覽量
        /// </summary>
        public int? TotalBrowse { get; set; }

        /// <summary>
        /// 設置或獲取來源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 設置或獲取作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 設置或獲取是否發布
        /// </summary>
        public bool? EnabledMark { get; set; }

        /// <summary>
        /// 設置或獲取邏輯刪除標志
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
