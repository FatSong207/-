using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Models;
using Yuebon.Commons.Mapping;
using Yuebon.Security.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [AutoMap(typeof(User))]
    public class UserAllListFocusOutPutDto
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

        /// <summary>
        /// 我是否關注
        /// </summary>
        public virtual string IfFocus { get; set; }

        /// <summary>
        /// 總關注人數
        /// </summary>
        public virtual int TotalFocus { get; set; }

        /// <summary>
        /// 是否認證
        /// </summary>
        public virtual string VipGrade { get; set; }

        /// <summary>
        /// 是否認證
        /// </summary>
        public virtual bool IsAuthentication { get; set; }

        /// <summary>
        /// 認證級別
        /// </summary>
        public virtual int AuthenticationType { get; set; }
        /// <summary>
        /// 記錄總數
        /// </summary>
        public virtual int RecordCount { get; set; }
        #endregion
    }
}
