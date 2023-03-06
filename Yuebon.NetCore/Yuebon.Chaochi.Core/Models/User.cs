
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 用戶表，數據實體對象
    /// </summary>
    [Table("Chaochi_User")]
    [Serializable]
    public class User: BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    { 
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
	    public User()
        {
            
        }

        #region Property Members
        /// <summary>
        /// 賬戶
        /// </summary>
        public virtual string Account { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string RealName { get; set; }

        /// <summary>
        /// 呢稱
        /// </summary>
        public virtual string NickName { get; set; }

        /// <summary>
        /// 頭像
        /// </summary>
        public virtual string HeadIcon { get; set; }

        /// <summary>
        /// 性別,1=男，0=未知，2=女
        /// </summary>
        public virtual int? Gender { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public virtual DateTime? Birthday { get; set; }

        /// <summary>
        /// 手機
        /// </summary>
        public virtual string MobilePhone { get; set; }

        /// <summary>
        /// 郵箱
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// 微信
        /// </summary>
        public virtual string WeChat { get; set; }

        /// <summary>
        /// 國家
        /// </summary>
        [DataMember]
        public virtual string Country { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [DataMember]
        public virtual string Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [DataMember]
        public virtual string City { get; set; }

        /// <summary>
        /// 地區
        /// </summary>
        [DataMember]
        public virtual string District { get; set; }

        /// <summary>
        /// 主管主鍵
        /// </summary>
        public virtual string ManagerId { get; set; }

        /// <summary>
        /// 安全級別
        /// </summary>
        public virtual int? SecurityLevel { get; set; }

        /// <summary>
        /// 個性簽名
        /// </summary>
        public virtual string Signature { get; set; }

        /// <summary>
        /// 組織主鍵
        /// </summary>
        public virtual string OrganizeId { get; set; }

        /// <summary>
        /// 部門主鍵
        /// </summary>
        public virtual string DepartmentId { get; set; }

        /// <summary>
        /// 角色主鍵
        /// </summary>
        public virtual string RoleId { get; set; }

        /// <summary>
        /// 崗位主鍵
        /// </summary>
        public virtual string DutyId { get; set; }

        /// <summary>
        /// 是否管理員
        /// </summary>
        public virtual bool? IsAdministrator { get; set; }

        /// <summary>
        /// 是否會員
        /// </summary>
        public virtual bool? IsMember { get; set; }

        /// <summary>
        /// 頭像
        /// </summary>
        public string MemberGradeId { get; set; }

        /// <summary>
        /// 上級推廣員
        /// </summary>
        public string ReferralUserId { get; set; }

        /// <summary>
        /// 用戶在微信開放平臺的唯一標識符
        /// </summary>
        public string UnionId { get; set; }

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