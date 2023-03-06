using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Commons.Models
{
    /// <summary>
    /// Token返回結果對象
    /// </summary>
    public class TokenResult
    {
        /// <summary>
        /// 構造函數
        /// </summary>
        public TokenResult()
        {
        }
        private string m_AccessToken;
        private int m_ExpiresIn;

        /// <summary>
        /// 獲取到的憑證值
        /// </summary>
        public string AccessToken
        {
            get { return m_AccessToken; }
            set { m_AccessToken = value; }
        }
        /// <summary>
        /// 憑證有效時間，單位：分鐘
        /// </summary>
        public int ExpiresIn
        {
            get { return m_ExpiresIn; }
            set { m_ExpiresIn = value; }
        }
    }
    public class GrantType
    {

        /// <summary>
        /// 密碼校驗。
        /// </summary>
        public const string Password = "password";

        /// <summary>
        /// ClientCredential。
        /// </summary>
        public const string ClientCredentials = "client_credential";
    }
}
