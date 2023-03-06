using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.Commons.Options
{
    /// <summary>
    /// 緩存中可用的應用
    /// </summary>
    [Serializable]
    public class AllowCacheApp
    {
        /// <summary>
        /// 設置或獲取 ID
        /// </summary>
        [MaxLength(50)]
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取 應用Id
        /// </summary>
        [MaxLength(50)]
        public string AppId { get; set; }

        /// <summary>
        /// 設置或獲取 應用密鑰
        /// </summary>
        [MaxLength(50)]
        public string AppSecret { get; set; }

        /// <summary>
        /// 設置或獲取 消息加密密鑰
        /// </summary>
        [MaxLength(256)]
        public string EncodingAESKey { get; set; }

        /// <summary>
        /// 設置或獲取 請求url
        /// </summary>
        [MaxLength(256)]
        public string RequestUrl { get; set; }

        /// <summary>
        /// 設置或獲取 token
        /// </summary>
        [MaxLength(256)]
        public string Token { get; set; }

        /// <summary>
        /// 設置或獲取  是否開啟消息加解密
        /// </summary>
        public bool? IsOpenAEKey { get; set; }

    }
}
