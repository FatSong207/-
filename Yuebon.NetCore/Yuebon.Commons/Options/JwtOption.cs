using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Commons.Options
{
    /// <summary>
    /// JsonWebToken配置模型。
    /// </summary>
    public class JwtOption
    {
        /// <summary>
        /// 簽發者。
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 收發者。
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 密鑰。
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Token有效期（單位：分鐘）。
        /// </summary>
        public int Expiration { get; set; }


        /// <summary>
        /// Token有效刷新時間（單位：分鐘）。
        /// </summary>
        public int refreshJwtTime { get; set; }
    }
}