using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.WeChat.Model
{
    /// <summary>
    /// 微信用戶信息實體對象
    /// </summary>
   public class WxUserInfo
    {
        /// <summary>
        /// 設置或獲取昵稱
        /// </summary>
        public string nickName
        {
            get;
            set;
        }
        /// <summary>
        /// 設置或獲取用戶頭像圖片的 URL
        /// </summary>
        public string avatarUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 設置或獲取用戶性別
        /// </summary>
        public int gender
        {
            get;
            set;
        }
        /// <summary>
        /// 設置或獲取用戶所在國家
        /// </summary>
        public string country
        {
            get;
            set;
        }
        /// <summary>
        /// 設置或獲取用戶所在省份
        /// </summary>
        public string province
        {
            get;
            set;
        }
        /// <summary>
        /// 設置或獲取所在城市
        /// </summary>
        public string city
        {
            get;
            set;
        }
        /// <summary>
        /// 設置或獲取用戶所在國家
        /// </summary>
        public string language
        {
            get;
            set;
        }

        /// <summary>
        /// 設置或獲取用戶openId
        /// </summary>
        public string openId
        {
            get;
            set;
        }

        /// <summary>
        /// 設置或獲取用戶openIdType
        /// </summary>
        public string openIdType
        {
            get;
            set;
        }

        /// <summary>
        /// 設置或獲取推廣者ReferralUserId
        /// </summary>
        public string ReferralUserId
        {
            get;
            set;
        }
        /// <summary>
        /// 設置或獲取包括敏感數據在內的完整用戶信息的加密數據
        /// </summary>
        public string EncryptedData
        {
            get;
            set;
        }
        /// <summary>
        /// 設置或獲取加密算法的初始向量
        /// </summary>
        public string Iv
        {
            get;
            set;
        }
        /// <summary>
        /// 設置或獲取微信登錄的SessionId
        /// </summary>
        public string SessionId
        {
            get;
            set;
        }

    }
}
