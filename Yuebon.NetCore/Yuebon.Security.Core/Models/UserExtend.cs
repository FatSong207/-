
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Yuebon.Commons.Models;

namespace Yuebon.Security.Models
{
    /// <summary>
    /// 用戶表，數據實體對象
    /// </summary>
    [Table("Sys_UserExtend")]
    [Serializable]
    public class UserExtend : BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    { 
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
	    public UserExtend()
        {
        }

        #region Property Members
        /// <summary>
        /// 賬戶
        /// </summary>
        public virtual string UserId { get; set; }

        /// <summary>
        /// 名片
        /// </summary>
        public virtual string CardContent { get; set; }

        /// <summary>
        /// 手機
        /// </summary>
        public virtual string Telphone { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string Address { get; set; }

        /// <summary>
        /// 公司名稱
        /// </summary>
        public virtual string CompanyName { get; set; }

        /// <summary>
        /// 職位
        /// </summary>
        public virtual string PositionTitle { get; set; }

        /// <summary>
        /// 微信二維碼
        /// </summary>
        public virtual string WeChatQrCode { get; set; }

        /// <summary>
        /// 地區
        /// </summary>
        public virtual string IndustryId { get; set; }

        /// <summary>
        /// 公開情況
        /// </summary>
        public virtual int OpenType { get; set; }

        /// <summary>
        /// IsOtherShare
        /// </summary>
        [DataMember]
        public virtual bool IsOtherShare { get; set; }

        /// <summary>
        /// sharetitle
        /// </summary>
        [DataMember]
        public virtual string ShareTitle { get; set; }
        /// <summary>
        /// 主頁
        /// </summary>
        [DataMember]
        public virtual string WebUrl { get; set; }
        /// <summary>
        /// 公司簡介
        /// </summary>
        [DataMember]
        public virtual string CompanyDesc { get; set; }
        /// <summary>
        /// 主題
        /// </summary>
        public virtual string CardTheme { get; set; }

        /// <summary>
        /// VIP
        /// </summary>
        public virtual string VipGrade { get; set; }

        /// <summary>
        /// 是否認證
        /// </summary>
        public virtual bool IsAuthentication { get; set; }

        /// <summary>
        /// 認證類型
        /// </summary>
        public virtual int AuthenticationType { get; set; }        

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
        /// 公司id
        /// </summary>
        [MaxLength(50)]
        public virtual string CompanyId { get; set; }

        /// <summary>
        /// 部門id
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