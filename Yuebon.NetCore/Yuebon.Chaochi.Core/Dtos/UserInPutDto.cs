using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Models;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸入對象模型
    /// </summary>
    [AutoMap(typeof(User))]
    [Serializable]
    public class UserInputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string HeadIcon { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string WeChat { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string ManagerId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public int? SecurityLevel { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string DepartmentId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string DutyId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool? IsAdministrator { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool? IsMember { get; set; }
        /// <summary>
        /// 語言
        /// </summary>
        public virtual string language { get; set; }
        /// <summary>
        /// OpenId
        /// </summary>
        public virtual string OpenId { get; set; }
        /// <summary>
        /// 第三方登錄系統類型
        /// </summary>
        public virtual string OpenIdType { get; set; }
        /// <summary>
        /// 會員等級
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


    }
}
