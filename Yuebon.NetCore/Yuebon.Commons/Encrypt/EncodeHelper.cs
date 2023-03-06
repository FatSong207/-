/*******************************************************************************
 * Copyright © 2017-2020 Yuebon.Framework 版權所有
 * Author: Yuebon
 * Description: Yuebon快速開發平臺
 * Website：http://www.yuebon.com
*********************************************************************************/
using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Yuebon.Commons.Encrypt
{
    /// <summary>
    /// DES對稱加解密、AES RijndaelManaged加解密、Base64加密解密、MD5加密等操作輔助類
    /// </summary>
    public sealed class EncodeHelper
    {
        #region DES對稱加密解密

        /// <summary>
        /// 注意DEFAULT_ENCRYPT_KEY的長度為8位(如果要增加或者減少key長度,調整IV的長度就是了) 
        /// </summary>
        public const string DEFAULT_ENCRYPT_KEY = "12345678";

        /// <summary>
        /// 使用默認加密
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string DesEncrypt(string strText)
        {
            try
            {
                return DesEncrypt(strText, DEFAULT_ENCRYPT_KEY);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 使用默認解密
        /// </summary>
        /// <param name="strText">解密字符串</param>
        /// <returns></returns>
        public static string DesDecrypt(string strText)
        {
            try
            {
                return DesDecrypt(strText, DEFAULT_ENCRYPT_KEY);
            }
            catch
            {
                return "";
            }
        }

        /// <summary> 
        /// 加密字符串,注意strEncrKey的長度為8位
        /// </summary> 
        /// <param name="strText">待加密字符串</param> 
        /// <param name="strEncrKey">加密鍵</param> 
        /// <returns></returns> 
        public static string DesEncrypt(string strText, string strEncrKey)
        {
            byte[] byKey = null;
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            byKey = Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

        /// <summary> 
        /// 解密字符串,注意strEncrKey的長度為8位
        /// </summary> 
        /// <param name="strText">待解密的字符串</param> 
        /// <param name="sDecrKey">解密鍵</param> 
        /// <returns>解密后的字符串</returns> 
        public static string DesDecrypt(string strText, string sDecrKey)
        {
            byte[] byKey = null;
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            byte[] inputByteArray = new Byte[strText.Length];

            byKey = Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            Encoding encoding = new UTF8Encoding();
            return encoding.GetString(ms.ToArray());
        }

        /// <summary> 
        /// 加密數據文件,注意strEncrKey的長度為8位
        /// </summary> 
        /// <param name="m_InFilePath">待加密的文件路徑</param> 
        /// <param name="m_OutFilePath">輸出文件路徑</param> 
        /// <param name="strEncrKey">加密鍵</param> 
        public static void DesEncrypt(string m_InFilePath, string m_OutFilePath, string strEncrKey)
        {
            byte[] byKey = null;
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            byKey = Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
            FileStream fin = new FileStream(m_InFilePath, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(m_OutFilePath, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);
            //Create variables to help with read and write. 
            byte[] bin = new byte[100]; //This is intermediate storage for the encryption. 
            long rdlen = 0; //This is the total number of bytes written. 
            long totlen = fin.Length; //This is the total length of the input file. 
            int len; //This is the number of bytes to be written at a time. 

            DES des = new DESCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(fout, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);

            //Read from the input file, then encrypt and write to the output file. 
            while (rdlen < totlen)
            {
                len = fin.Read(bin, 0, 100);
                encStream.Write(bin, 0, len);
                rdlen = rdlen + len;
            }
            encStream.Close();
            fout.Close();
            fin.Close();
        }

        /// <summary> 
        /// 解密數據文件,注意strEncrKey的長度為8位
        /// </summary> 
        /// <param name="m_InFilePath">待解密的文件路徑</param> 
        /// <param name="m_OutFilePath">輸出路徑</param> 
        /// <param name="sDecrKey">解密鍵</param> 
        public static void DesDecrypt(string m_InFilePath, string m_OutFilePath, string sDecrKey)
        {
            byte[] byKey = null;
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            byKey = Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
            FileStream fin = new FileStream(m_InFilePath, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(m_OutFilePath, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);
            //Create variables to help with read and write. 
            byte[] bin = new byte[100]; //This is intermediate storage for the encryption. 
            long rdlen = 0; //This is the total number of bytes written. 
            long totlen = fin.Length; //This is the total length of the input file. 
            int len; //This is the number of bytes to be written at a time. 

            DES des = new DESCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(fout, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);

            //Read from the input file, then encrypt and write to the output file. 
            while (rdlen < totlen)
            {
                len = fin.Read(bin, 0, 100);
                encStream.Write(bin, 0, len);
                rdlen = rdlen + len;
            }
            encStream.Close();
            fout.Close();
            fin.Close();
        }
        #endregion

        #region 對稱加密算法AES RijndaelManaged加密解密
        private static readonly string Default_AES_Key = "@#yuebonqinwn123";
        private static byte[] Keys = { 0x41, 0x72, 0x65, 0x79, 0x6F, 0x75, 0x6D, 0x79,
                                             0x53,0x6E, 0x6F, 0x77, 0x6D, 0x61, 0x6E, 0x3F };

        /// <summary>
        /// 對稱加密算法AES RijndaelManaged加密(RijndaelManaged（AES）算法是塊式加密算法)
        /// </summary>
        /// <param name="encryptString">待加密字符串</param>
        /// <returns>加密結果字符串</returns>
        public static string AES_Encrypt(string encryptString)
        {
            return AES_Encrypt(encryptString, Default_AES_Key);
        }

        /// <summary>
        /// 對稱加密算法AES RijndaelManaged加密(RijndaelManaged（AES）算法是塊式加密算法)
        /// </summary>
        /// <param name="encryptString">待加密字符串</param>
        /// <param name="encryptKey">加密密鑰，須半角字符</param>
        /// <returns>加密結果字符串</returns>
        public static string AES_Encrypt(string encryptString, string encryptKey)
        {
            encryptKey = GetSubString(encryptKey, 32, "");
            encryptKey = encryptKey.PadRight(32, ' ');

            RijndaelManaged rijndaelProvider = new RijndaelManaged();
            rijndaelProvider.Key = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 32));
            rijndaelProvider.IV = Keys;
            ICryptoTransform rijndaelEncrypt = rijndaelProvider.CreateEncryptor();

            byte[] inputData = Encoding.UTF8.GetBytes(encryptString);
            byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);

            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// 對稱加密算法AES RijndaelManaged解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <returns>解密成功返回解密后的字符串,失敗返源串</returns>
        public static string AES_Decrypt(string decryptString)
        {
            return AES_Decrypt(decryptString, Default_AES_Key);
        }

        /// <summary>
        /// 對稱加密算法AES RijndaelManaged解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密鑰,和加密密鑰相同</param>
        /// <returns>解密成功返回解密后的字符串,失敗返回空</returns>
        public static string AES_Decrypt(string decryptString, string decryptKey)
        {
            try
            {
                decryptKey = GetSubString(decryptKey, 32, "");
                decryptKey = decryptKey.PadRight(32, ' ');

                RijndaelManaged rijndaelProvider = new RijndaelManaged();
                rijndaelProvider.Key = Encoding.UTF8.GetBytes(decryptKey);
                rijndaelProvider.IV = Keys;
                ICryptoTransform rijndaelDecrypt = rijndaelProvider.CreateDecryptor();

                byte[] inputData = Convert.FromBase64String(decryptString);
                byte[] decryptedData = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);

                return Encoding.UTF8.GetString(decryptedData);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 按字節長度(按字節,一個漢字為2個字節)取得某字符串的一部分
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="length">所取字符串字節長度</param>
        /// <param name="tailString">附加字符串(當字符串不夠長時，尾部所添加的字符串，一般為"...")</param>
        /// <returns>某字符串的一部分</returns>
        private static string GetSubString(string sourceString, int length, string tailString)
        {
            return GetSubString(sourceString, 0, length, tailString);
        }

        /// <summary>
        /// 按字節長度(按字節,一個漢字為2個字節)取得某字符串的一部分
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="startIndex">索引位置，以0開始</param>
        /// <param name="length">所取字符串字節長度</param>
        /// <param name="tailString">附加字符串(當字符串不夠長時，尾部所添加的字符串，一般為"...")</param>
        /// <returns>某字符串的一部分</returns>
        private static string GetSubString(string sourceString, int startIndex, int length, string tailString)
        {
            string myResult = sourceString;

            //當是日文或韓文時(注:中文的范圍:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韓文為\xAC00-\xD7A3)
            if (System.Text.RegularExpressions.Regex.IsMatch(sourceString, "[\u0800-\u4e00]+") ||
                System.Text.RegularExpressions.Regex.IsMatch(sourceString, "[\xAC00-\xD7A3]+"))
            {
                //當截取的起始位置超出字段串長度時
                if (startIndex >= sourceString.Length)
                {
                    return string.Empty;
                }
                else
                {
                    return sourceString.Substring(startIndex,
                                                   ((length + startIndex) > sourceString.Length) ? (sourceString.Length - startIndex) : length);
                }
            }

            //中文字符，如"中國人民abcd123"
            if (length <= 0)
            {
                return string.Empty;
            }
            byte[] bytesSource = Encoding.Default.GetBytes(sourceString);

            //當字符串長度大于起始位置
            if (bytesSource.Length > startIndex)
            {
                int endIndex = bytesSource.Length;

                //當要截取的長度在字符串的有效長度范圍內
                if (bytesSource.Length > (startIndex + length))
                {
                    endIndex = length + startIndex;
                }
                else
                {   //當不在有效范圍內時,只取到字符串的結尾
                    length = bytesSource.Length - startIndex;
                    tailString = "";
                }

                int[] anResultFlag = new int[length];
                int nFlag = 0;
                //字節大于127為雙字節字符
                for (int i = startIndex; i < endIndex; i++)
                {
                    if (bytesSource[i] > 127)
                    {
                        nFlag++;
                        if (nFlag == 3)
                        {
                            nFlag = 1;
                        }
                    }
                    else
                    {
                        nFlag = 0;
                    }
                    anResultFlag[i] = nFlag;
                }
                //最后一個字節為雙字節字符的一半
                if ((bytesSource[endIndex - 1] > 127) && (anResultFlag[length - 1] == 1))
                {
                    length = length + 1;
                }

                byte[] bsResult = new byte[length];
                Array.Copy(bytesSource, startIndex, bsResult, 0, length);
                myResult = Encoding.Default.GetString(bsResult);
                myResult = myResult + tailString;

                return myResult;
            }

            return string.Empty;

        }

        /// <summary>
        /// 加密文件流
        /// </summary>
        /// <param name="fs">文件流對象</param>
        /// <param name="encryptKey">加密鍵</param>
        /// <returns></returns>
        public static CryptoStream AES_EncryptStrream(FileStream fs, string encryptKey)
        {
            encryptKey = GetSubString(encryptKey, 32, "");
            encryptKey = encryptKey.PadRight(32, ' ');

            RijndaelManaged rijndaelProvider = new RijndaelManaged();
            rijndaelProvider.Key = Encoding.UTF8.GetBytes(encryptKey);
            rijndaelProvider.IV = Keys;

            ICryptoTransform encrypto = rijndaelProvider.CreateEncryptor();
            CryptoStream cytptostreamEncr = new CryptoStream(fs, encrypto, CryptoStreamMode.Write);
            return cytptostreamEncr;
        }

        /// <summary>
        /// 解密文件流
        /// </summary>
        /// <param name="fs">文件流對象</param>
        /// <param name="decryptKey">解密鍵</param>
        /// <returns></returns>
        public static CryptoStream AES_DecryptStream(FileStream fs, string decryptKey)
        {
            decryptKey = GetSubString(decryptKey, 32, "");
            decryptKey = decryptKey.PadRight(32, ' ');

            RijndaelManaged rijndaelProvider = new RijndaelManaged();
            rijndaelProvider.Key = Encoding.UTF8.GetBytes(decryptKey);
            rijndaelProvider.IV = Keys;
            ICryptoTransform Decrypto = rijndaelProvider.CreateDecryptor();
            CryptoStream cytptostreamDecr = new CryptoStream(fs, Decrypto, CryptoStreamMode.Read);
            return cytptostreamDecr;
        }

        /// <summary>
        /// 對指定文件加密
        /// </summary>
        /// <param name="InputFile">輸入文件</param>
        /// <param name="OutputFile">輸出文件</param>
        /// <returns></returns>
        public static bool AES_EncryptFile(string InputFile, string OutputFile)
        {
            try
            {
                string decryptKey = "www.iqidi.com";

                FileStream fr = new FileStream(InputFile, FileMode.Open);
                FileStream fren = new FileStream(OutputFile, FileMode.Create);
                CryptoStream Enfr = AES_EncryptStrream(fren, decryptKey);
                byte[] bytearrayinput = new byte[fr.Length];
                fr.Read(bytearrayinput, 0, bytearrayinput.Length);
                Enfr.Write(bytearrayinput, 0, bytearrayinput.Length);
                Enfr.Close();
                fr.Close();
                fren.Close();
            }
            catch
            {
                //文件異常
                return false;
            }
            return true;
        }

        /// <summary>
        /// 對指定的文件解壓縮
        /// </summary>
        /// <param name="InputFile">輸入文件</param>
        /// <param name="OutputFile">輸出文件</param>
        /// <returns></returns>
        public static bool AES_DecryptFile(string InputFile, string OutputFile)
        {
            try
            {
                string decryptKey = "www.iqidi.com";
                FileStream fr = new FileStream(InputFile, FileMode.Open);
                FileStream frde = new FileStream(OutputFile, FileMode.Create);
                CryptoStream Defr = AES_DecryptStream(fr, decryptKey);
                byte[] bytearrayoutput = new byte[1024];
                int m_count = 0;

                do
                {
                    m_count = Defr.Read(bytearrayoutput, 0, bytearrayoutput.Length);
                    frde.Write(bytearrayoutput, 0, m_count);
                    if (m_count < bytearrayoutput.Length)
                        break;
                } while (true);

                Defr.Close();
                fr.Close();
                frde.Close();
            }
            catch
            {
                //文件異常
                return false;
            }
            return true;
        }

        #endregion

        #region Base64加密解密
        /// <summary>
        /// Base64是一種使用64基的位置計數法。它使用2的最大次方來代表僅可列印的ASCII 字元。
        /// 這使它可用來作為電子郵件的傳輸編碼。在Base64中的變數使用字元A-Z、a-z和0-9 ，
        /// 這樣共有62個字元，用來作為開始的64個數字，最後兩個用來作為數字的符號在不同的
        /// 系統中而不同。
        /// Base64加密
        /// </summary>
        /// <param name="str">Base64方式加密字符串</param>
        /// <returns></returns>
        public static string Base64Encrypt(string str)
        {
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(encbuff);
        }

        /// <summary>
        /// Base64解密字符串
        /// </summary>
        /// <param name="str">待解密的字符串</param>
        /// <returns></returns>
        public static string Base64Decrypt(string str)
        {
            byte[] decbuff = Convert.FromBase64String(str);
            return System.Text.Encoding.UTF8.GetString(decbuff);
        }
        #endregion

        #region MD5加密

        /// <summary> 
        /// 使用MD5加密字符串
        /// </summary> 
        /// <param name="strText">待加密的字符串</param> 
        /// <returns>MD5加密后的字符串</returns> 
        public static string MD5Encrypt(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(Encoding.Default.GetBytes(strText));
            return Encoding.Default.GetString(result);
        }

        /// <summary>
        /// 使用MD5加密的Hash表
        /// </summary>
        /// <param name="input">待加密字符串</param>
        /// <returns></returns>
        public static string MD5EncryptHash(String input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            //the GetBytes method returns byte array equavalent of a string
            byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(input), 0, input.Length);
            char[] temp = new char[res.Length];
            //copy to a char array which can be passed to a String constructor
            Array.Copy(res, temp, res.Length);
            //return the result as a string
            return new String(temp);
        }

        /// <summary>
        /// 使用Md5加密為16進制字符串
        /// </summary>
        /// <param name="input">待加密字符串</param>
        /// <returns></returns>
        public static string MD5EncryptHashHex(String input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            //the GetBytes method returns byte array equavalent of a string
            byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(input), 0, input.Length);

            String returnThis = string.Empty;

            for (int i = 0; i < res.Length; i++)
            {
                returnThis += Uri.HexEscape((char)res[i]);
            }
            returnThis = returnThis.Replace("%", "");
            returnThis = returnThis.ToLower();

            return returnThis;
        }

        /// <summary>
        /// MD5 三次加密算法.計算過程: (QQ使用)
        /// 1. 驗證碼轉為大寫
        /// 2. 將密碼使用這個方法進行三次加密后,與驗證碼進行疊加
        /// 3. 然后將疊加后的內容再次MD5一下,得到最終驗證碼的值
        /// </summary>
        /// <param name="s">待加密字符串</param>
        /// <returns></returns>
        public static string EncyptMD5_3_16(string s)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(s);
            byte[] bytes1 = md5.ComputeHash(bytes);
            byte[] bytes2 = md5.ComputeHash(bytes1);
            byte[] bytes3 = md5.ComputeHash(bytes2);

            StringBuilder sb = new StringBuilder();
            foreach (var item in bytes3)
            {
                sb.Append(item.ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString().ToUpper();
        }
        #endregion

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
        /// 加密字符串
        /// </summary>
        /// <param name="input">待加密的字符串</param>
        /// <returns></returns>
        public static string EncryptString(string input)
        {
            return MD5Util.AddMD5Profix(Base64Util.Encrypt(MD5Util.AddMD5Profix(input)));
            //return Base64.Encrypt(MD5.AddMD5Profix(Base64.Encrypt(input)));
        }

        /// <summary>
        /// 解密加過密的字符串
        /// </summary>
        /// <param name="input">待解密的字符串</param>
        /// <param name="throwException">解密失敗是否拋異常</param>
        /// <returns></returns>
        public static string DecryptString(string input, bool throwException)
        {
            string res = "";
            try
            {
                res = input;// Base64.Decrypt(input);
                if (MD5Util.ValidateValue(res))
                {
                    return MD5Util.RemoveMD5Profix(Base64Util.Decrypt(MD5Util.RemoveMD5Profix(res)));
                }
                else
                {
                    throw new Exception("字符串無法轉換成功！");
                }
            }
            catch
            {
                if (throwException)
                {
                    throw;
                }
                else
                {
                    return "";
                }
            }
        }
    }
}