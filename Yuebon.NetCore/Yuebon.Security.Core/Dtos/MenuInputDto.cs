using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Models;
using Yuebon.Security.Models;

namespace Yuebon.Security.Dtos
{
    /// <summary>
    /// 輸入對象模型
    /// </summary>
    [AutoMap(typeof(Menu))]
    [Serializable]
    public class MenuInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string SystemTypeId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public int? Layers { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string EnCode { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
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
        public bool? IsShow { get; set; }
        /// <summary>
        /// 設置或獲取是否緩存
        /// </summary>
        public bool? IsCache { get; set; }
        /// <summary>
        /// 設置或獲取 是否外鏈
        /// </summary>
        public bool? IsFrame { get; set; }
        /// <summary>
        /// 設置或獲取 批量添加
        /// </summary>
        public bool IsBatch { get; set; }
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
        public bool EnabledMark { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// 創建日期
        /// </summary>
        public virtual DateTime? CreatorTime { get; set; }



        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string SystemTypeName { get; set; }
    }
}
