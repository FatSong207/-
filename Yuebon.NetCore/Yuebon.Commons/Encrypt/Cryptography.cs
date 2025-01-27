﻿/*******************************************************************************
 * Copyright © 2017-2020 Yuebon.Framework 版權所有
 * Author: Yuebon
 * Description: Yuebon快速開發平臺
 * Website：http://www.yuebon.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.Commons
{
    /// <summary>
    /// 用于webapi 生成票據使用，公開
    /// </summary>
    public sealed class Cryptography
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tmpStr"></param>
        /// <param name="encodingAESKey"></param>
        /// <returns></returns>
        public static string AES_encrypt(string tmpStr, string encodingAESKey)
        {
            string str = tmpStr + GetMD5_32(encodingAESKey);
            return GetMD5_32(SHA256(str)) + GetMD5_32(tmpStr);
        }
        /// <summary>
        /// SHA256函數
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>SHA256結果(返回長度為44字節的字符串)</returns>
        public static string SHA256(string str)
        {
            byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
            SHA256Managed Sha256 = new SHA256Managed();
            byte[] Result = Sha256.ComputeHash(SHA256Data);
            return Convert.ToBase64String(Result);  //返回長度為44字節的字符串
        }

       

        /// <summary>
        /// 獲得32位的MD5加密
        /// </summary>
        public static string GetMD5_32(string input)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] data = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }

    }
}
