﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Yuebon.Security.Dtos
{
    [Serializable]
    public class UserLoginDto
    {
        #region Property Members

        /// <summary>
        /// ID
        /// </summary>
        public virtual string UserId { get; set; }
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
        /// 排序碼
        /// </summary>
        public virtual int? SortCode { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }


        /// <summary>
        /// 有效標志
        /// </summary>
        public virtual bool EnabledMark { get; set; }

        /// <summary>
        /// 創建日期
        /// </summary>
        public virtual DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 登錄IP地址
        /// </summary>
        public virtual string CurrentLoginIP { get; set; }
        /// <summary>
        /// 角色編碼，多個角色，使用“,”分格
        /// </summary>
        public string Role { get; set; }
        #endregion

    }
}
