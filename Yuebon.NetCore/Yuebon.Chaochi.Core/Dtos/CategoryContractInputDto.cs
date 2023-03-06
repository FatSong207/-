using AutoMapper;
using System;
using Yuebon.Chaochi.Core.Models;
using Yuebon.Commons.Dtos;

namespace Yuebon.Chaochi.Core.Dtos
{
    /// <summary>
    /// 輸入表單分類模型
    /// </summary>
    [AutoMap(typeof(CategoryContract))]
    [Serializable]
    public class CategoryContractInputDto : IInputDto<string>
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
        /// 歸類
        /// </summary>
        public virtual string ArchiveLTo { get; set; }


        /// <summary>
        /// 合約種類
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// 合約子類別
        /// </summary>
        public virtual string SubType { get; set; }

        /// <summary>
        /// 業務是否要簽核
        /// </summary>
        public virtual string NeedSalesSign { get; set; }

        /// <summary>
        /// 主管是否要簽核
        /// </summary>
        public virtual string NeedSupervisorSign { get; set; }

        /// <summary>
        /// 是否要線上簽名
        /// </summary>
        public virtual string NeedSignOnline { get; set; }

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
