/*******************************************************************************
 * Copyright © 2017-2020 Yuebon.Framework 版權所有
 * Author: Yuebon
 * Description: Yuebon快速開發平臺
 * Website：http://www.yuebon.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Commons.Encrypt
{
    /// <summary>
    /// QQ密碼加密操作輔助類
    /// </summary>
    public class QQEncryptUtil
    {
        /// <summary>
        /// QQ根據密碼及驗證碼對數據進行加密
        /// </summary>
        /// <param name="password">原始密碼</param>
        /// <param name="verifyCode">驗證碼</param>
        /// <returns></returns>
        public static string EncodePasswordWithVerifyCode(string password, string verifyCode)
        {
            return MD5(MD5_3(password) + verifyCode.ToUpper());
        }

        static string MD5_3(string arg)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(arg);
            buffer = md5.ComputeHash(buffer);
            buffer = md5.ComputeHash(buffer);
            buffer = md5.ComputeHash(buffer);

            return BitConverter.ToString(buffer).Replace("-", "").ToUpper();
        }
        static string MD5(string arg)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(arg);
            buffer = md5.ComputeHash(buffer);

            return BitConverter.ToString(buffer).Replace("-", "").ToUpper();
        }
    }
}
