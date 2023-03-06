using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Models;
using Yuebon.Security.Models;

namespace Yuebon.Security.Dtos
{
    /// <summary>
    /// 輸入對象模型
    /// </summary>
    [AutoMap(typeof(UserExtend))]
    [Serializable]
    public class UserExtendInputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取名片內容
        /// </summary>
        public string CardContent { get; set; }

        /// <summary>
        /// 設置或獲取電話
        /// </summary>
        public string Telphone { get; set; }

        /// <summary>
        /// 設置或獲取地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 設置或獲取公司名稱
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 設置或獲取職位名稱
        /// </summary>
        public string PositionTitle { get; set; }

        /// <summary>
        /// 設置或獲取個人微信二維碼
        /// </summary>
        public string WechatQrCode { get; set; }

        /// <summary>
        /// 設置或獲取行業
        /// </summary>
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
        public string ShareTitle { get; set; }

        /// <summary>
        /// 設置或獲取網址
        /// </summary>
        public string WebUrl { get; set; }

        /// <summary>
        /// 設置或獲取公司簡介
        /// </summary>
        public string CompanyDesc { get; set; }

        /// <summary>
        /// 設置或獲取名片樣式
        /// </summary>
        public string CardTheme { get; set; }

        /// <summary>
        /// 設置或獲取Vip等級
        /// </summary>
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


    }
}
