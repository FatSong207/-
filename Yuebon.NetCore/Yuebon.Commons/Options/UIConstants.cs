using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Commons
{
    /// <summary>
    /// 常用軟體字串
    /// </summary>
    public static class UIConstants
    {
        /// <summary>
        /// 逾期時間
        /// </summary>
        public static string ApplicationExpiredDate = "12/29/2012";
        /// <summary>
        /// 軟體版本
        /// </summary>
        public static string SoftwareVersion = "3.0";
        /// <summary>
        /// 軟體產品名稱
        /// </summary>
        public static string SoftwareProductName = "YuebonSoftSystem";

        /// <summary>
        /// 軟體的試用期
        /// </summary>
        public static int SoftwareProbationDay = 20;

        /// <summary>
        /// 獨立存儲目錄名稱
        /// </summary>
        public static string IsolatedStorageDirectoryName = "UserNameDir";
        /// <summary>
        /// 獨立存儲加密鑰
        /// </summary>
        public static string IsolatedStorageEncryptKey = "12345678";

        /// <summary>
        /// 註冊加密公鑰
        /// </summary>
        public static string PublicKey = @"<RSAKeyValue><Modulus>mtDtu679/0quhftVyOc6/cBov/i534Dkh3AB8RwrpC9Vq2RIFB3uvjRUuaAEPR8vMcijQjVzqLZgMM7jFKclzbh21rWTM+YlOeraKz5FPCC7rSLnv6Tfbzia9VI/r5cfM8ogVMuUKCZeU+PTEmVviasCl8nPYyqOQchlf/MftMM=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        /// <summary>
        /// 版權訊息
        /// </summary>
        public static string CopyRight= string.Format("<strong>Copyright &copy; 2017-{0} <a href=\"http://www.ugee.com\" target=\"_blank\">U-gee Tech</a>.</strong>", DateTime.Now.Year);
        /// <summary>
        /// Web驗證地址
        /// </summary>
        public static string WebRegisterURL = "http://www.yuebon.com/WebRegister.aspx";

        /// <summary>
        /// 設置參數值
        /// </summary>
        /// <param name="expiredDate">過期時間</param>
        /// <param name="version">軟體版本</param>
        /// <param name="name">軟體名稱</param>
        /// <param name="publicKey">公鑰字串</param>
        public static void SetValue(string expiredDate, string version, string name, string publicKey)
        {
            UIConstants.ApplicationExpiredDate = expiredDate;
            UIConstants.SoftwareVersion = version;
            UIConstants.SoftwareProductName = name;
            UIConstants.PublicKey = publicKey;
        }
    }
}
