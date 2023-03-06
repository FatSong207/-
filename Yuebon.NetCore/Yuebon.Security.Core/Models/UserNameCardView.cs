
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yuebon.Security.Models
{
    /// <summary>
    /// 用戶表，數據實體對象
    /// </summary>
    [Table("Sys_Vw_NameCard")]
    [Serializable]
    [NotMapped]
    public class UserNameCardView : BaseEntity<string>
    { 
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
	    public UserNameCardView()
        {
            
        }

        #region Property Members


        /// <summary>
        /// 用戶id
        /// </summary>
        public virtual string UserId { get; set; }

        /// <summary>
        /// 名片詳情
        /// </summary>
        public virtual string CardContent { get; set; }

        /// <summary>
        /// 電話
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
        /// 行業代碼
        /// </summary>
        public virtual string IndustryId { get; set; }

        /// <summary>
        /// 公開標識
        /// </summary>
        public virtual int OpenType { get; set; }

        /// <summary>
        /// IsOtherShare
        /// </summary>
        public virtual bool? IsOtherShare { get; set; }
        /// <summary>
        /// ShareTitle
        /// </summary>
        public virtual string ShareTitle { get; set; }
        /// <summary>
        /// 主頁
        /// </summary>
        public string WebUrl { get; set; }

        /// <summary>
        /// 公司描述
        /// </summary>
        public string CompanyDesc { get; set; }

        /// <summary>
        /// 名片主題
        /// </summary>
        public virtual string CardTheme { get; set; }

        /// <summary>
        /// vip等級
        /// </summary>
        public virtual string VipGrade { get; set; }

        /// <summary>
        /// 是否認證
        /// </summary>
        public virtual bool? IsAuthentication { get; set; }

        /// <summary>
        /// 認證類型
        /// </summary>
        public virtual int? AuthenticationType { get; set; }

        /// <summary>
        /// 可以標識 
        /// </summary>
        public virtual bool? EnabledMark { get; set; }

        /// <summary>
        /// 刪除標識 
        /// </summary>
        public virtual bool? DeleteMark { get; set; }

        /// <summary>
        /// 創建時間
        /// </summary>
        public virtual DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 創建人ID
        /// </summary>
        public virtual string CreatorUserId { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public virtual string CompanyId { get; set; }

        /// <summary>
        /// 部門ID
        /// </summary>
        public virtual string DeptId { get; set; }

        /// <summary>
        /// 最后修改時間
        /// </summary>
        public virtual DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        public virtual string LastModifyUserId { get; set; }

        /// <summary>
        /// 刪除時間
        /// </summary>
        public virtual DateTime DeleteTime { get; set; }

        /// <summary>
        /// 刪除 人
        /// </summary>
        public virtual string DeleteUserId { get; set; }

        /// <summary>
        /// 真實名稱
        /// </summary>
        public virtual string RealName { get; set; }

        /// <summary>
        /// 昵稱
        /// </summary>
        public virtual string NickName { get; set; }

        /// <summary>
        /// 頭像
        /// </summary>
        public virtual string HeadIcon { get; set; }

        /// <summary>
        /// 手機
        /// </summary>
        public virtual string MobilePhone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public virtual string EMail { get; set; }

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
        /// 性別
        /// </summary>
        public virtual string Gender { get; set; }

        /// <summary>
        /// 用戶主表id
        /// </summary>
        public virtual string MUserId { get; set; }
        #endregion

    }
}