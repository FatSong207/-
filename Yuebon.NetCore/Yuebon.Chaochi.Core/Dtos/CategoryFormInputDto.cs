using AutoMapper;
using System;
using Yuebon.Chaochi.Core.Models;
using Yuebon.Commons.Dtos;

namespace Yuebon.Chaochi.Core.Dtos
{
    /// <summary>
    /// 輸入表單分類模型
    /// </summary>
    [AutoMap(typeof(CategoryForm))]
    [Serializable]
    public class CategoryFormInputDto : IInputDto<string>
    {
        /// <summary>
        /// 分類Id 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 父層Id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 層次 
        /// </summary>
        public int? Layers { get; set; }

        /// <summary>
        /// 分類名稱 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序碼 
        /// </summary>
        public int? SortOrder { get; set; }

        /// <summary>
        /// 有效標誌 
        /// </summary>
        public bool EnabledMark { get; set; }

        /// <summary>
        /// 刪除標誌 
        /// </summary>
        public bool DeleteMark { get; set; }
    }
}
