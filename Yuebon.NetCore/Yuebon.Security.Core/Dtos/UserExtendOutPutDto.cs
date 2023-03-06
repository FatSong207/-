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
    public class UserExtendOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取用戶Id
        /// </summary>
        [MaxLength(50)]
        public string UserId { get; set; }

        /// <summary>
        /// 設置或獲取名片內容
        /// </summary>
        [MaxLength(2147483647)]
        public string CardContent { get; set; }

        /// <summary>
        /// 設置或獲取電話
        /// </summary>
        [MaxLength(50)]
        public string Telphone { get; set; }

        /// <summary>
        /// 設置或獲取地址
        /// </summary>
        [MaxLength(250)]
        public string Address { get; set; }

        /// <summary>
        /// 設置或獲取公司名稱
        /// </summary>
        [MaxLength(250)]
        public string CompanyName { get; set; }

        /// <summary>
        /// 設置或獲取職位名稱
        /// </summary>
        [MaxLength(250)]
        public string PositionTitle { get; set; }

        /// <summary>
        /// 設置或獲取個人微信二維碼
        /// </summary>
        [MaxLength(250)]
        public string WechatQrCode { get; set; }

        /// <summary>
        /// 設置或獲取行業
        /// </summary>
        [MaxLength(500)]
        public string IndustryId { get; set; }

        /// <summary>
        /// 設置或獲取隱私：0-不公開，1-公開；2-部分公開
        /// </summary>
        public int? OpenType { get; set; }

        /// <summary>
        /// 設置或獲取是否允許他人轉發你的名片
        /// </summary>
        public bool? IsOtherShare { get; set; }

        /// <summary>
        /// 設置或獲取轉發標題
        /// </summary>
        [MaxLength(250)]
        public string ShareTitle { get; set; }

        /// <summary>
        /// 設置或獲取網址
        /// </summary>
        [MaxLength(250)]
        public string WebUrl { get; set; }

        /// <summary>
        /// 設置或獲取公司簡介
        /// </summary>
        [MaxLength(2147483647)]
        public string CompanyDesc { get; set; }

        /// <summary>
        /// 設置或獲取名片樣式
        /// </summary>
        [MaxLength(250)]
        public string CardTheme { get; set; }

        /// <summary>
        /// 設置或獲取Vip等級
        /// </summary>
        [MaxLength(50)]
        public string VipGrade { get; set; }

        /// <summary>
        /// 設置或獲取是否認證
        /// </summary>
        public bool? IsAuthentication { get; set; }

        /// <summary>
        /// 設置或獲取認證類型1-企業；2-個人
        /// </summary>
        public int? AuthenticationType { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool EnabledMark { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public bool? DeleteMark { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string CompanyId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string DeptId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string LastModifyUserId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string DeleteUserId { get; set; }


    }
}
