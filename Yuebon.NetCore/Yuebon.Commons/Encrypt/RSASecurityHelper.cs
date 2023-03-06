/*******************************************************************************
 * Copyright © 2017-2020 Yuebon.Framework 版權所有
 * Author: Yuebon
 * Description: Yuebon快速開發平臺
 * Website：http://www.yuebon.com
*********************************************************************************/
using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace Yuebon.Commons.Encrypt
{
    /// <summary>
    /// 非對稱加密、解密、驗證輔助類
    /// </summary>
    public class RSASecurityHelper
    {

        /// <summary>
        /// 默認公鑰。必須是8位字符的密鑰字符串(不能有特殊字符)
        /// </summary>
        private const string DESENCRYPT_KEY = "yuebon.com";


        /// <summary>
        /// 非對稱加密生成的私鑰和公鑰 
        /// </summary>
        /// <param name="publicKey">公鑰,必須是8位字符的密鑰字符串(不能有特殊字符)</param>
        /// <param name="privateKey">私鑰</param>
        public static void GenerateRSAKey(out string privateKey, out string publicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            privateKey = rsa.ToXmlString(true);
            publicKey = rsa.ToXmlString(false);
        }

        #region 非對稱數據加密（公鑰加密）
        /// <summary>
        ///  非對稱加密字符串數據，返回加密后的數據
        /// </summary>
        /// <param name="originalString">待加密的字符串</param>
        /// <returns></returns>
        public static string RSAEncrypt(string originalString)
        {
            return RSAEncrypt(originalString, DESENCRYPT_KEY);
        }

        /// <summary>
        /// 非對稱加密字節數組，返回加密后的數據
        /// </summary>
        /// <param name="originalBytes">待加密的字節數組</param>
        public static string RSAEncrypt(byte[] originalBytes)
        {
            return RSAEncrypt(originalBytes, DESENCRYPT_KEY);
        }
        /// <summary>
        /// 非對稱加密字符串數據，返回加密后的數據
        /// </summary>
        /// <param name="originalString">待加密的字符串</param>
        /// <param name="publicKey">公鑰,必須是8位字符的密鑰字符串(不能有特殊字符)</param>
        /// <returns>加密后的數據</returns>
        public static string RSAEncrypt(string originalString, string publicKey)
        {
            if (string.IsNullOrEmpty(publicKey) && publicKey.Length != 8)
            {
                publicKey = DESENCRYPT_KEY;
            }
            byte[] PlainTextBArray;
            byte[] CypherTextBArray;
            string Result;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);
            PlainTextBArray = (new UnicodeEncoding()).GetBytes(originalString);
            CypherTextBArray = rsa.Encrypt(PlainTextBArray, false);
            Result = Convert.ToBase64String(CypherTextBArray);
            return Result;
        }

        /// <summary>
        /// 非對稱加密字節數組，返回加密后的數據
        /// </summary>
        /// <param name="originalBytes">待加密的字節數組</param>
        /// <param name="publicKey">公鑰,必須是8位字符的密鑰字符串(不能有特殊字符)</param>
        /// <returns>返回加密后的數據</returns>
        public static string RSAEncrypt(byte[] originalBytes, string publicKey)
        {
            if (string.IsNullOrEmpty(publicKey) && publicKey.Length != 8)
            {
                publicKey = DESENCRYPT_KEY;
            }
            byte[] CypherTextBArray;
            string Result;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);
            CypherTextBArray = rsa.Encrypt(originalBytes, false);
            Result = Convert.ToBase64String(CypherTextBArray);
            return Result;
        }

        #endregion

        #region 非對稱解密（私鑰解密）

        /// <summary>
        /// 非對稱解密字符串，返回解密后的數據
        /// </summary>
        /// <param name="privateKey">私鑰</param>
        /// <param name="encryptedString">待解密數據</param>
        /// <returns>返回解密后的數據</returns>
        public static string RSADecrypt(string privateKey, string encryptedString)
        {
            byte[] PlainTextBArray;
            byte[] DypherTextBArray;
            string Result;
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            PlainTextBArray = Convert.FromBase64String(encryptedString);
            DypherTextBArray = rsa.Decrypt(PlainTextBArray, false);
            Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
            return Result;
        }

        /// <summary>
        /// 非對稱解密字節數組，返回解密后的數據
        /// </summary>
        /// <param name="privateKey">私鑰</param>
        /// <param name="encryptedBytes">待解密數據</param>
        /// <returns></returns>
        public static string RSADecrypt(string privateKey, byte[] encryptedBytes)
        {
            byte[] DypherTextBArray;
            string Result;
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            DypherTextBArray = rsa.Decrypt(encryptedBytes, false);
            Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
            return Result;
        }
        #endregion

        #region 非對稱加密簽名、驗證

        /// <summary>
        /// 使用非對稱加密簽名數據
        /// </summary>
        /// <param name="privateKey">私鑰</param>
        /// <param name="originalString">待加密的字符串</param>
        /// <returns>加密后的數據</returns>
        public static string RSAEncrypSignature(string originalString, string privateKey)
        {
            string signature;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKey); //私鑰
                // 加密對象 
                RSAPKCS1SignatureFormatter f = new RSAPKCS1SignatureFormatter(rsa);
                f.SetHashAlgorithm("SHA1");
                byte[] source = ASCIIEncoding.ASCII.GetBytes(originalString);
                SHA1Managed sha = new SHA1Managed();
                byte[] result = sha.ComputeHash(source);
                byte[] b = f.CreateSignature(result);
                signature = Convert.ToBase64String(b);
            }
            return signature;
        }

        /// <summary>
        /// 對私鑰加密的字符串，使用公鑰對其進行驗證
        /// </summary>
        /// <param name="originalString">未加密的文本，如機器碼</param>
        /// <param name="encrytedString">加密后的文本，如註冊序列號</param>
        /// <param name="publicKey">非對稱加密的公鑰</param>
        /// <returns>如果驗證成功返回True，否則為False</returns>
        public static bool Validate(string originalString, string encrytedString, string publicKey)
        {
            bool bPassed = false;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                try
                {
                    rsa.FromXmlString(publicKey); //公鑰
                    RSAPKCS1SignatureDeformatter formatter = new RSAPKCS1SignatureDeformatter(rsa);
                    formatter.SetHashAlgorithm("SHA1");

                    byte[] key = Convert.FromBase64String(encrytedString); //驗證
                    SHA1Managed sha = new SHA1Managed();
                    byte[] name = sha.ComputeHash(ASCIIEncoding.ASCII.GetBytes(originalString));
                    if (formatter.VerifySignature(name, key))
                    {
                        bPassed = true;
                    }
                }
                catch
                {
                }
            }
            return bPassed;
        }

        #endregion

        #region Hash 加密

        /// <summary> Hash 加密 </summary>
        /// <param name="str2Hash"></param>
        /// <returns></returns>
        public static int HashEncrypt(string str2Hash)
        {
            const int salt = 100716;    // 鹽值
            str2Hash += "Commons";       // 增加一個常量字符串

            int len = str2Hash.Length;
            int result = (str2Hash[len - 1] - 31) * 95 + salt;
            for (int i = 0; i < len - 1; i++)
            {
                result = (result * 95) + (str2Hash[i] - 32);
            }

            return result;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">待加密字串</param>
        /// <returns>加密后的字串</returns>
        public static string ComputeMD5(string str)
        {
            byte[] hashValue = ComputeMD5Data(str);
            return BitConverter.ToString(hashValue).Replace("-", "");
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">待加密字串</param>
        /// <returns>加密后的字串</returns>
        public static byte[] ComputeMD5Data(string input)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(input);
            return MD5.Create().ComputeHash(buffer);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="data">待加密數據</param>
        /// <returns>加密后的字串</returns>
        public static byte[] ComputeMD5Data(byte[] data)
        {
            return MD5.Create().ComputeHash(data);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="stream">待加密流</param>
        /// <returns>加密后的字串</returns>
        public static byte[] ComputeMD5Data(Stream stream)
        {
            return MD5.Create().ComputeHash(stream);
        }
        #endregion
    }
}