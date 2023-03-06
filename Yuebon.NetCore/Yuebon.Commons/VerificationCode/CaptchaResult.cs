using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Yuebon.Commons.VerificationCode
{
    /// <summary>
    /// 驗證碼返回結果
    /// </summary>
    public class CaptchaResult
    {
        /// <summary>
        /// 驗證碼字符串
        /// </summary>
        public string CaptchaCode { get; set; }

        /// <summary>
        /// 驗證碼內存流
        /// </summary>
        public MemoryStream CaptchaMemoryStream { get; set; }

        /// <summary>
        /// 驗證碼生成時間
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
