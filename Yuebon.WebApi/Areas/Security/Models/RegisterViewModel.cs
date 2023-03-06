using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yuebon.WebApi.Areas.Security.Models
{
    /// <summary>
    /// 註冊信息
    /// </summary>
    [Serializable]
    public class RegisterViewModel
    {
        /// <summary>
        /// 設置帳號
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 設置郵箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 設置密碼
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 設置驗證碼
        /// </summary>
        public string VerificationCode{ get; set; }
        /// <summary>
        /// 設置驗證碼Key
        /// </summary>
        public string VerifyCodeKey { get; set; }
    }
}
