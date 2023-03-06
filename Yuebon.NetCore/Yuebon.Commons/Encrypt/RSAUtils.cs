/*******************************************************************************
 * Copyright © 2017-2020 Yuebon.Framework 版權所有
 * Author: Yuebon
 * Description: Yuebon快速開發平臺
 * Website：http://www.yuebon.com
*********************************************************************************/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Yuebon.Commons.Encrypt
{
    /// <summary>
    /// RSA加解密工具
    /// </summary>
    public static class RSAUtils
    {
        /// <summary>
        /// 從本地文件中讀取用來簽發Token的 RSA Key。
        /// </summary>
        /// <param name="filePath">存放密鑰的文件夾路徑</param>
        /// <param name="withPrivate"></param>
        /// <param name="keyParameters"></param>
        /// <returns></returns>
        public static bool TryGetKeyParameters(string filePath, bool withPrivate, out RSAParameters keyParameters)
        {
            string filename = withPrivate ? "key.json" : "key.public.json";
            keyParameters = default(RSAParameters);
            if (Directory.Exists(filePath) == false)
            {
                return false;
            }
            keyParameters = JsonConvert.DeserializeObject<RSAParameters>(File.ReadAllText(Path.Combine(filePath, filename)));
            return true;
        }

        /// <summary>
        /// 生成并保存 RSA 公鑰與私鑰。
        /// </summary>
        /// <param name="filePath">存放密鑰的文件夾路徑</param>
        /// <returns></returns>
        public static RSAParameters GenerateAndSaveKey(string filePath)
        {
            RSAParameters publicKeys, privateKeys;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    privateKeys = rsa.ExportParameters(true);
                    publicKeys = rsa.ExportParameters(false);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
            File.WriteAllText(Path.Combine(filePath, "key.json"), JsonConvert.SerializeObject(privateKeys));
            File.WriteAllText(Path.Combine(filePath, "key.public.json"), JsonConvert.SerializeObject(publicKeys));
            return privateKeys;
        }
    }
}
