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
    public class UserOutputDto
    {

        #region Property Members

        /// <summary>
        /// 用戶主鍵
        /// </summary>
        public virtual string Id { get; set; }

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
        /// 性別
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
        /// 密碼
        /// </summary>
        public virtual string UserPassword { get; set; }

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
        public virtual string Country { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public virtual string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public virtual string City { get; set; }
        /// <summary>
        /// 縣區
        /// </summary>
        public virtual string District { get; set; }
        /// <summary>
        /// 是否會員
        /// </summary>
        public virtual bool? IsMember { get; set; }
        /// <summary>
        /// 會員等級
        /// </summary>
        public string MemberGradeId { get; set; }

        /// <summary>
        /// 上級推廣員
        /// </summary>
        public string ReferralUserId { get; set; }

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
        /// 組織名稱
        /// </summary>
        public virtual string OrganizeName { get; set; }

        /// <summary>
        /// 部門主鍵
        /// </summary>
        public virtual string DepartmentId { get; set; }

        /// <summary>
        /// 部門名稱
        /// </summary>
        public virtual string DepartmentName { get; set; }

        /// <summary>
        /// 角色主鍵
        /// </summary>
        public virtual string RoleId { get; set; }

        /// <summary>
        /// 角色名稱
        /// </summary>
        public virtual string RoleName { get; set; }

        /// <summary>
        /// 組織主鍵2
        /// </summary>
        public virtual string OrganizeIdSec { get; set; }

        /// <summary>
        /// 組織名稱2
        /// </summary>
        public virtual string OrganizeSecName { get; set; }

        /// <summary>
        /// 部門主鍵2
        /// </summary>
        public virtual string DepartmentIdSec { get; set; }

        /// <summary>
        /// 部門名稱2
        /// </summary>
        public virtual string DepartmentSecName { get; set; }

        /// <summary>
        /// 角色主鍵2
        /// </summary>
        public virtual string RoleIdSec { get; set; }

        /// <summary>
        /// 角色名稱2
        /// </summary>
        public virtual string RoleSecName { get; set; }

        /// <summary>
        /// 組織主鍵3
        /// </summary>
        public virtual string OrganizeIdThird { get; set; }

        /// <summary>
        /// 組織名稱3
        /// </summary>
        public virtual string OrganizeThirdName { get; set; }

        /// <summary>
        /// 部門主鍵3
        /// </summary>
        public virtual string DepartmentIdThird { get; set; }

        /// <summary>
        /// 部門名稱3
        /// </summary>
        public virtual string DepartmentThirdName { get; set; }

        /// <summary>
        /// 角色主鍵3
        /// </summary>
        public virtual string RoleIdThird { get; set; }

        /// <summary>
        /// 角色名稱3
        /// </summary>
        public virtual string RoleThirdName { get; set; }

        /// <summary>
        /// 崗位主鍵
        /// </summary>
        public virtual string DutyId { get; set; }

        /// <summary>
        /// 崗位名稱
        /// </summary>
        public virtual string DutyName { get; set; }

        /// <summary>
        /// 是否管理員
        /// </summary>
        public virtual bool? IsAdministrator { get; set; }

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
        public virtual string CreatorUserId { get; set; }

        /// <summary>
        /// 最后修改時間
        /// </summary>
        public virtual DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用戶
        /// </summary>
        public virtual string LastModifyUserId { get; set; }

        /// <summary>
        /// 刪除時間
        /// </summary>
        public virtual DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 刪除用戶
        /// </summary>
        public virtual string DeleteUserId { get; set; }
        #endregion
    }
}
