using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Security.Models
{
    /// <summary>
    /// 系統應用表，數據實體對象
    /// </summary>
    [Table("Sys_APP")]
    [Serializable]
    public class APP :BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
        public APP()
        {
        }

        #region Property APP


        /// <summary>
        /// 應用Id
        /// </summary>
        [MaxLength(50)]
        [Description("應用Id")]
        public virtual string AppId { get; set; }

        /// <summary>
        /// 應用密鑰
        /// </summary>
        [MaxLength(256)]
        [Description("應用密鑰")]
        public virtual string AppSecret { get; set; }

        /// <summary>
        /// 消息加解密密鑰
        /// </summary>
        [MaxLength(256)]
        [Description("消息加解密密鑰")]
        public virtual string EncodingAESKey { get; set; }

        /// <summary>
        /// 授權請求地址url
        /// </summary>
        [MaxLength(512)]
        [Description("授權請求地址url")]
        public virtual string RequestUrl { get; set; }

        /// <summary>
        /// Token令牌
        /// </summary>
        [Display(Name = "Token令牌")]
        [MaxLength(64)]
        [Description("Token令牌")]
        public virtual string Token { get; set; }
        /// <summary>
        /// 是否開啟消息加解密
        /// </summary>
        [Display(Name = "是否開啟消息加解密")]
        [Description("是否開啟消息加解密")]
        public virtual bool IsOpenAEKey { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "描述")]
        [Description("描述")]
        [MaxLength(200)]
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
        /// 創建用戶組織主鍵
        /// </summary>
        [MaxLength(50)]
        public virtual string CompanyId { get; set; }
        /// <summary>
        /// 創建用戶部門主鍵
        /// </summary>
        [MaxLength(50)]
        public virtual string DeptId { get; set; }
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