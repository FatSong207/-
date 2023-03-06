
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Models;

namespace Yuebon.Security.Models
{
    /// <summary>
    /// 系統菜單，數據實體對象
    /// </summary>
    [Table("Sys_Menu")]
    [Serializable]
    public class Menu: BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    { 
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
	    public Menu()
		{
          this.Id = GuidUtils.CreateNo();
        }

        #region Property Members


        /// <summary>
        /// 所屬系統主鍵
        /// </summary>
        public virtual string SystemTypeId { get; set; }
        /// <summary>
        /// 父級
        /// </summary>
        public virtual string ParentId { get; set; }

        /// <summary>
        /// 層次
        /// </summary>
        public virtual int? Layers { get; set; }

        /// <summary>
        /// 編碼
        /// </summary>
        public virtual string EnCode { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        public virtual string FullName { get; set; }

        /// <summary>
        /// 圖標
        /// </summary>
        public virtual string Icon { get; set; }

        /// <summary>
        /// 路由
        /// </summary>
        public virtual string UrlAddress { get; set; }

        /// <summary>
        /// 目標打開方式
        /// </summary>
        public virtual string Target { get; set; }

        /// <summary>
        /// 菜單類型（C目錄 M菜單 F按鈕）
        /// </summary>
        public virtual string MenuType { get; set; }
        /// <summary>
        /// 組件路徑
        /// </summary>
        public virtual string Component { get; set; }
        /// <summary>
        /// 設置當前選中菜單，用于新增、編輯、查看操作為單獨的路由時指定選中菜單路由
        /// 同時設置為隱藏時才有效
        /// </summary>
        public virtual string ActiveMenu { get; set; }
        /// <summary>
        /// 展開
        /// </summary>
        public virtual bool IsExpand { get; set; }

        /// <summary>
        /// 設置或獲取 是否顯示
        /// </summary>
        public bool? IsShow { get; set; }
        /// <summary>
        /// 設置或獲取 是否外鏈
        /// </summary>
        public bool? IsFrame { get; set; }
        /// <summary>
        /// 設置或獲取是否緩存
        /// </summary>
        public bool? IsCache { get; set; }
        /// <summary>
        /// 公共
        /// </summary>
        public virtual bool? IsPublic { get; set; }

        /// <summary>
        /// 允許編輯
        /// </summary>
        public virtual bool? AllowEdit { get; set; }

        /// <summary>
        /// 允許刪除
        /// </summary>
        public virtual bool? AllowDelete { get; set; }

        /// <summary>
        /// 排序碼
        /// </summary>
        public virtual int? SortCode { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 刪除標志
        /// </summary>
        public virtual bool? DeleteMark { get; set; }

        /// <summary>
        /// 有效標志
        /// </summary>
        public virtual bool EnabledMark { get; set; }

        /// <summary>
        /// 創建日期
        /// </summary>
        public virtual DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 創建用戶主鍵
        /// </summary>
        [MaxLength(50)]
        public virtual string CreatorUserId { get; set; }

        /// <summary>
        /// 最后修改時間
        /// </summary>
        public virtual DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用戶
        /// </summary>
        [MaxLength(50)]
        public virtual string LastModifyUserId { get; set; }

        /// <summary>
        /// 刪除時間
        /// </summary>
        public virtual DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 刪除用戶
        /// </summary>
        [MaxLength(50)]
        public virtual string DeleteUserId { get; set; }
        #endregion

    }
}