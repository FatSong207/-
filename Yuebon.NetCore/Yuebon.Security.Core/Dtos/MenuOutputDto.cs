using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.Security.Dtos
{
    /// <summary>
    /// 輸出對象模型
    /// </summary>
    [Serializable]
    public class MenuOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string SystemTypeId { get; set; }
        public string SystemTypeName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string ParentId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public int? Layers { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string EnCode { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string FullName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string Icon { get; set; }

        /// <summary>
        /// 設置或獲取路由
        /// </summary>
        public virtual string UrlAddress { get; set; }

        /// <summary>
        /// 設置或獲取目標打開方式
        /// </summary>
        public virtual string Target { get; set; }

        /// <summary>
        /// 設置或獲取菜單類型（C目錄 M菜單 F按鈕）
        /// </summary>
        public virtual string MenuType { get; set; }
        /// <summary>
        /// 設置或獲取組件路徑
        /// </summary>
        public virtual string Component { get; set; }
        /// <summary>
        /// 設置當前選中菜單，用于新增、編輯、查看操作為單獨的路由時指定選中菜單路由
        /// 同時設置為隱藏時才有效
        /// </summary>
        public virtual string ActiveMenu { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool? IsExpand { get; set; }

        /// <summary>
        /// 設置或獲取 是否顯示
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 設置或獲取 是否外鏈
        /// </summary>
        public bool IsFrame { get; set; }
        /// <summary>
        /// 設置或獲取是否緩存
        /// </summary>
        public bool IsCache { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool? IsPublic { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool? AllowEdit { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool? AllowDelete { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public int? SortCode { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool? DeleteMark { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool EnabledMark { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string LastModifyUserId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string DeleteUserId { get; set; }

        /// <summary>
        /// 子菜單集合
        /// </summary>
        public List<MenuOutputDto> SubMenu { get; set; }


    }
}
