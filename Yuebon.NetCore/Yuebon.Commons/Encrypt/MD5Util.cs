/*******************************************************************************
 * Copyright © 2017-2020 Yuebon.Framework 版權所有
 * Author: Yuebon
 * Description: Yuebon快速開發平臺
 * Website：http://www.yuebon.com
*********************************************************************************/
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Commons.Encrypt
{
    /// <summary>
    /// MD5各種長度加密字符、驗證MD5等操作輔助類
    /// </summary>
    public class MD5Util
    {
        /// <summary>
        /// 構造函數
        /// </summary>
        public MD5Util()
        {
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

        /// <summary>
        /// 獲得16位的MD5加密
        /// </summary>
        public static string GetMD5_16(string input)
        {
            return GetMD5_32(input).Substring(8, 16);
        }

        /// <summary>
        /// 獲得8位的MD5加密
        /// </summary>
        public static string GetMD5_8(string input)
        {
            return GetMD5_32(input).Substring(8, 8);
        }

        /// <summary>
        /// 獲得4位的MD5加密
        /// </summary>
        public static string GetMD5_4(string input)
        {
            return GetMD5_32(input).Substring(8, 4);
        }

        /// <summary>
        /// 添加MD5的前綴，便于檢查有無篡改
        /// </summary>
        public static string AddMD5Profix(string input)
        {
            return GetMD5_4(input) + input;
        }

        /// <summary>
        /// 移除MD5的前綴
        /// </summary>
        public static string RemoveMD5Profix(string input)
        {
            return input.Substring(4);
        }

        /// <summary>
        /// 驗證MD5前綴處理的字符串有無被篡改
        /// </summary>
        public static bool ValidateValue(string input)
        {
            bool res = false;
            if (input.Length >= 4)
            {
                string tmp = input.Substring(4);
                if (input.Substring(0, 4) == GetMD5_4(tmp))
                {
                    res = true;
                }
            }
            return res;
        }

        #region MD5簽名驗證

        /// <summary>
        /// 對給定文件路徑的文件加上標簽
        /// </summary>
        /// <param name="path">要加密的文件的路徑</param>
        /// <returns>標簽的值</returns>
        public static bool AddMD5(string path)
        {
            bool IsNeed = true;

            if (CheckMD5(path)) //已進行MD5處理
                IsNeed = false;

            try
            {
                FileStream fsread = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] md5File = new byte[fsread.Length];
                fsread.Read(md5File, 0, (int)fsread.Length);                               // 將文件流讀取到Buffer中
                fsread.Close();

                if (IsNeed)
                {
                    string result = MD5Buffer(md5File, 0, md5File.Length);             // 對Buffer中的字節內容算MD5
                    byte[] md5 = System.Text.Encoding.ASCII.GetBytes(result);       // 將字符串轉換成字節數組以便寫人到文件中
                    FileStream fsWrite = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                    fsWrite.Write(md5File, 0, md5File.Length);                               // 將文件，MD5值 重新寫入到文件中。
                    fsWrite.Write(md5, 0, md5.Length);
                    fsWrite.Close();
                }
                else
                {
                    FileStream fsWrite = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                    fsWrite.Write(md5File, 0, md5File.Length);
                    fsWrite.Close();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 對給定路徑的文件進行驗證，如果一致返回True，否則返回False
        /// </summary>
        /// <param name="path"></param>
        /// <returns>是否加了標簽或是否標簽值與內容值一致</returns>
        public static bool CheckMD5(string path)
        {
            try
            {
                FileStream get_file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] md5File = new byte[get_file.Length];                                      // 讀入文件
                get_file.Read(md5File, 0, (int)get_file.Length);
                get_file.Close();

                string result = MD5Buffer(md5File, 0, md5File.Length - 32);             // 對文件除最后32位以外的字節計算MD5，這個32是因為標簽位為32位。
                string md5 = System.Text.Encoding.ASCII.GetString(md5File, md5File.Length - 32, 32);   //讀取文件最后32位，其中保存的就是MD5值
                return result == md5;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 計算文件的MD5值
        /// </summary>
        /// <param name="MD5File">MD5簽名文件字符數組</param>
        /// <param name="index">計算起始位置</param>
        /// <param name="count">計算終止位置</param>
        /// <returns>計算結果</returns>
        private static string MD5Buffer(byte[] MD5File, int index, int count)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider get_md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hash_byte = get_md5.ComputeHash(MD5File, index, count);
            string result = System.BitConverter.ToString(hash_byte);

            result = result.Replace("-", "");
            return result;
        }
        #endregion

    }
}
