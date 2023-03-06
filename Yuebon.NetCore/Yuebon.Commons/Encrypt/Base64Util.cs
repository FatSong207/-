/*******************************************************************************
 * Copyright © 2017-2020 Yuebon.Framework 版權所有
 * Author: Yuebon
 * Description: Yuebon快速開發平臺
 * Website：http://www.yuebon.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Commons
{
    /// <summary>
    /// 基于Base64的加密編碼輔助類，
    /// 可以設置不同的密碼表來獲取不同的編碼與解碼
    /// </summary>
    public class Base64Util
    {
        /// <summary>
        /// 構造函數，初始化編碼表
        /// </summary>
        public Base64Util()
        {
            this.InitDict();
        }
        /// <summary>
        /// 
        /// </summary>
        protected static Base64Util s_b64 = new Base64Util();

        /// <summary>
        /// 使用默認的密碼表加密字符串
        /// </summary>
        /// <param name="input">待加密字符串</param>
        /// <returns></returns>
        public static string Encrypt(string input)
        {
            return s_b64.Encode(input);
        }
        /// <summary>
        /// 使用默認的密碼表解密字符串
        /// </summary>
        /// <param name="input">待解密字符串</param>
        /// <returns></returns>
        public static string Decrypt(string input)
        {
            return s_b64.Decode(input);
        }

        /// <summary>
        /// 獲取具有標準的Base64密碼表的加密類
        /// </summary>
        /// <returns></returns>
        public static Base64Util GetStandardBase64()
        {
            Base64Util b64 = new Base64Util();
            b64.Pad = "=";
            b64.CodeTable = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
            return b64;
        }

        /// <summary>
        /// 密碼表
        /// </summary>
        protected string m_codeTable = @"ABCDEFGHIJKLMNOPQRSTUVWXYZbacdefghijklmnopqrstu_wxyz0123456789*-";
        /// <summary>
        /// 補碼
        /// </summary>
        protected string m_pad = "v";
        /// <summary>
        /// 
        /// </summary>
        protected Dictionary<int, char> m_t1 = new Dictionary<int, char>();
        /// <summary>
        /// 
        /// </summary>
        protected Dictionary<char, int> m_t2 = new Dictionary<char, int>();

        /// <summary>
        /// 密碼表
        /// </summary>
        public string CodeTable
        {
            get { return m_codeTable; }
            set
            {
                if (value == null)
                {
                    throw new Exception("密碼表不能為null");
                }
                else if (value.Length < 64)
                {
                    throw new Exception("密碼表長度必須至少為64");
                }
                else
                {
                    this.ValidateRepeat(value);
                    this.ValidateEqualPad(value, m_pad);
                    m_codeTable = value;
                    this.InitDict();
                }
            }
        }

        /// <summary>
        /// 補碼
        /// </summary>
        public string Pad
        {
            get { return m_pad; }
            set
            {
                if (value == null)
                {
                    throw new Exception("密碼表的補碼不能為null");
                }
                else if (value.Length != 1)
                {
                    throw new Exception("密碼表的補碼長度必須為1");
                }
                else
                {
                    this.ValidateEqualPad(m_codeTable, value);
                    m_pad = value;
                    this.InitDict();
                }
            }
        }

        /// <summary>
        /// 返回編碼后的字符串
        /// </summary>
        /// <param name="source">原字符串</param>
        /// <returns></returns>
        public string Encode(string source)
        {
            if (source == null || source == "")
            {
                return "";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                byte[] tmp = System.Text.UTF8Encoding.UTF8.GetBytes(source);
                int remain = tmp.Length % 3;
                int patch = 3 - remain;
                if (remain != 0)
                {
                    Array.Resize(ref tmp, tmp.Length + patch);
                }
                int cnt = (int)Math.Ceiling(tmp.Length * 1.0 / 3);
                for (int i = 0; i < cnt; i++)
                {
                    sb.Append(this.EncodeUnit(tmp[i * 3], tmp[i * 3 + 1], tmp[i * 3 + 2]));
                }
                if (remain != 0)
                {
                    sb.Remove(sb.Length - patch, patch);
                    for (int i = 0; i < patch; i++)
                    {
                        sb.Append(m_pad);
                    }
                }
                return sb.ToString();
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        protected string EncodeUnit(params byte[] unit)
        {
            int[] obj = new int[4];
            obj[0] = (unit[0] & 0xfc) >> 2;
            obj[1] = ((unit[0] & 0x03) << 4) + ((unit[1] & 0xf0) >> 4);
            obj[2] = ((unit[1] & 0x0f) << 2) + ((unit[2] & 0xc0) >> 6);
            obj[3] = unit[2] & 0x3f;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < obj.Length; i++)
            {
                sb.Append(this.GetEC((int)obj[i]));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        protected char GetEC(int code)
        {
            return m_t1[code];//m_codeTable[code];
        }

        /// <summary>
        /// 獲得解碼字符串
        /// </summary>
        /// <param name="source">原字符串</param>
        /// <returns></returns>
        public string Decode(string source)
        {
            if (source == null || source == "")
            {
                return "";
            }
            else
            {
                List<byte> list = new List<byte>();
                char[] tmp = source.ToCharArray();
                int remain = tmp.Length % 4;
                if (remain != 0)
                {
                    Array.Resize(ref tmp, tmp.Length - remain);
                }
                int patch = source.IndexOf(m_pad);
                if (patch != -1)
                {
                    patch = source.Length - patch;
                }
                int cnt = tmp.Length / 4;
                for (int i = 0; i < cnt; i++)
                {
                    this.DecodeUnit(list, tmp[i * 4], tmp[i * 4 + 1], tmp[i * 4 + 2], tmp[i * 4 + 3]);
                }
                for (int i = 0; i < patch; i++)
                {
                    list.RemoveAt(list.Count - 1);
                }
                return System.Text.Encoding.UTF8.GetString(list.ToArray());
            }
        }

        /// <summary>
        /// 獲得解碼字符串
        /// </summary>
        /// <param name="byteArr"></param>
        /// <param name="chArray"></param>
        protected void DecodeUnit(List<byte> byteArr, params char[] chArray)
        {
            int[] res = new int[3];
            byte[] unit = new byte[chArray.Length];
            for (int i = 0; i < chArray.Length; i++)
            {
                unit[i] = this.FindChar(chArray[i]);
            }
            res[0] = (unit[0] << 2) + ((unit[1] & 0x30) >> 4);
            res[1] = ((unit[1] & 0xf) << 4) + ((unit[2] & 0x3c) >> 2);
            res[2] = ((unit[2] & 0x3) << 6) + unit[3];
            for (int i = 0; i < res.Length; i++)
            {
                byteArr.Add((byte)res[i]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        protected byte FindChar(char ch)
        {
            int pos = m_t2[ch];//m_codeTable.IndexOf(ch);
            return (byte)pos;
        }

        /// <summary>
        /// 初始化雙向哈希字典
        /// </summary>
        protected void InitDict()
        {
            m_t1.Clear();
            m_t2.Clear();
            m_t2.Add(m_pad[0], -1);
            for (int i = 0; i < m_codeTable.Length; i++)
            {
                m_t1.Add(i, m_codeTable[i]);
                m_t2.Add(m_codeTable[i], i);
            }
        }

        /// <summary>
        /// 檢查字符串中的字符是否有重復
        /// </summary>
        /// <param name="input">待檢查字符串</param>
        /// <returns></returns>
        protected void ValidateRepeat(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input.LastIndexOf(input[i]) > i)
                {
                    throw new Exception("密碼表中含有重復字符：" + input[i]);
                }
            }
        }

        /// <summary>
        /// 檢查字符串是否包含補碼字符
        /// </summary>
        /// <param name="input">待檢查字符串</param>
        /// <param name="pad"></param>
        protected void ValidateEqualPad(string input, string pad)
        {
            if (input.IndexOf(pad) > -1)
            {
                throw new Exception("密碼表中包含了補碼字符：" + pad);
            }
        }
        
    }
}

