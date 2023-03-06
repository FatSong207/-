﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Models;
using Yuebon.Commons.Dtos;
using Yuebon.CMS.Models;

namespace Yuebon.CMS.Dtos
{
    /// <summary>
    /// 文章，通知公告輸入對象模型
    /// </summary>
    [AutoMap(typeof(Articlenews))]
    [Serializable]
    public class ArticlenewsInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取主鍵
        /// </summary>
        public string Id { get; set; }

        /// 設置或獲取文章分類
        /// </summary>
        public string CategoryId { get; set; }

        /// 設置或獲取分類名稱
        /// </summary>
        public string CategoryName { get; set; }

        /// 設置或獲取文章標題
        /// </summary>
        public string Title { get; set; }

        /// 設置或獲取副標題
        /// </summary>
        public string SubTitle { get; set; }

        /// 設置或獲取外鏈
        /// </summary>
        public string LinkUrl { get; set; }

        /// 設置或獲取主圖
        /// </summary>
        public string ImgUrl { get; set; }

        /// 設置或獲取SEO標題
        /// </summary>
        public string SeoTitle { get; set; }

        /// 設置或獲取SEO關鍵詞
        /// </summary>
        public string SeoKeywords { get; set; }

        /// 設置或獲取SEO描述
        /// </summary>
        public string SeoDescription { get; set; }

        /// 設置或獲取標簽，多個用逗號隔開
        /// </summary>
        public string Tags { get; set; }

        /// 設置或獲取摘要
        /// </summary>
        public string Zhaiyao { get; set; }

        /// 設置或獲取詳情
        /// </summary>
        public string Description { get; set; }

        /// 設置或獲取排序
        /// </summary>
        public int? SortCode { get; set; }

        /// 設置或獲取開啟評論
        /// </summary>
        public bool? IsMsg { get; set; }

        /// 設置或獲取是否置頂，默認不置頂
        /// </summary>
        public bool? IsTop { get; set; }

        /// 設置或獲取是否推薦
        /// </summary>
        public bool? IsRed { get; set; }

        /// 設置或獲取是否熱門，默認否
        /// </summary>
        public bool? IsHot { get; set; }

        /// 設置或獲取是否是系統預置文章，不可刪除
        /// </summary>
        public bool? IsSys { get; set; }

        /// 設置或獲取是否推薦到最新
        /// </summary>
        public bool? IsNew { get; set; }

        /// 設置或獲取是否推薦到幻燈
        /// </summary>
        public bool? IsSlide { get; set; }

        /// 設置或獲取點擊量
        /// </summary>
        public int? Click { get; set; }

        /// 設置或獲取喜歡量
        /// </summary>
        public int? LikeCount { get; set; }

        /// 設置或獲取瀏覽量
        /// </summary>
        public int? TotalBrowse { get; set; }

        /// 設置或獲取來源
        /// </summary>
        public string Source { get; set; }

        /// 設置或獲取作者
        /// </summary>
        public string Author { get; set; }

        /// 設置或獲取是否發布
        /// </summary>
        public bool? EnabledMark { get; set; }

    }
}