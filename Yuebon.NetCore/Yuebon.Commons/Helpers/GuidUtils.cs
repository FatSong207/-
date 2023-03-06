using System;
using System.Collections.Generic;
using System.Text;

namespace Yuebon.Commons.Helpers
{
    /// <summary>  
    /// Guid工具類  
    /// </summary>  
    public class GuidUtils
    {
        #region 自動生成編號
        /// <summary>
        /// 表示全局唯一標識符 (GUID)。
        /// </summary>
        /// <returns></returns>
        public static string GuId()
        {
            return Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 自動生成編號/唯一訂單號生成，時間戳+隨機數，時間戳精確到毫秒，形如2020052113254137177350
        /// </summary>
        /// <returns></returns>
        public static string CreateNo()
        {
            Random random = new Random();
            string strRandom = random.Next(1000, 100000000).ToString(); //生成隨機編號 
            string code = DateTime.Now.ToString("yyyyMMddHHmmssffff") + strRandom;//形如2020052113254137177350
            return code;
        }
        #endregion
        /// <summary>  
        /// 獲取一個大寫的字符串  
        /// </summary>  
        /// <param name="str"></param>  
        /// <returns></returns>  
        private static string upper(string str)
        {
            return str.ToUpper();
        }

        /// <summary>  
        /// 獲取32位不包含“-”號的GUID字符串  
        /// </summary>  
        /// <returns></returns>  
        public static string NewGuidFormatN(bool isUpper = false)
        {
            var guid = Guid.NewGuid().ToString("N");
            return isUpper == true ? upper(guid) : guid;
        }

        /// <summary>  
        /// 獲取32位包含“-”號的GUID字符串  
        /// </summary>  
        /// <param name="isUpper"></param>  
        /// <returns></returns>  
        public static string NewGuidFormatD(bool isUpper = false)
        {
            var guid = Guid.NewGuid().ToString("D");
            return isUpper == true ? upper(guid) : guid;
        }

        /// <summary>  
        /// 獲取32位包含“-”號的GUID被“(”、“)”包括的字符串  
        /// </summary>  
        /// <param name="isUpper"></param>  
        /// <returns></returns>  
        public static string NewGuidFormatP(bool isUpper = false)
        {
            var guid = Guid.NewGuid().ToString("P");
            return isUpper == true ? upper(guid) : guid;
        }

        /// <summary>  
        /// 獲取32位包含“-”號的GUID被“{”、“}”包括的字符串  
        /// </summary>  
        /// <param name="isUpper"></param>  
        /// <returns></returns>  
        public static string NewGuidFormatB(bool isUpper = false)
        {
            var guid = Guid.NewGuid().ToString("B");
            return isUpper == true ? upper(guid) : guid;
        }

        /// <summary>  
        /// 獲取4個被“{”、“}”包括的十六進制數，其中第四個值位8個被“{”、“}”包括的十六進制數字符串  
        /// </summary>  
        /// <param name="isUpper"></param>  
        /// <returns></returns>  
        public static string NewGuidFormatX(bool isUpper = false)
        {
            var guid = Guid.NewGuid().ToString("X");
            return isUpper == true ? upper(guid) : guid;
        }

    }
}
