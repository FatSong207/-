using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Security.Dtos
{
    /// <summary>
    /// 驗證碼輸出實體
    /// </summary>
    public class AuthGetVerifyCodeOutputDto
    {
        /// <summary>
        /// 緩存鍵
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 圖片
        /// </summary>
        public string Img { get; set; }
    }
}
