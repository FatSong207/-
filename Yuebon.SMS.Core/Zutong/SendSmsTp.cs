using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.SMS.Zutong
{
    /// <summary>
    /// 模板短信實體
    /// </summary>
    public class SendSmsTp
    {
        /// <summary>
        /// 用戶名
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 密碼，密碼采用32位小寫MD5二次加密，例：md5( md5( password ) + tKey ))
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// tKey為東八區當前時間戳，精確到秒，10位長度。例如：1577352217
        /// </summary>
        public long tKey { get; set; }
        /// <summary>
        /// 已報備的簽名
        /// </summary>
        public string signature { get; set; }
        /// <summary>
        /// 模板id
        /// </summary>
        public long tpId { get; set; }
        /// <summary>
        /// 擴展號。純數字，最多8位。
        /// </summary>
        public int ext { get; set; }
        /// <summary>
        /// 用戶自定義信息，在用戶獲取狀態報告時返回
        /// </summary>
        public string extend { get; set; }
        /// <summary>
        /// 模板變量信息
        /// </summary>
        public List<record> records { get; set; }
    }

    public class record{

        /// <summary>
        /// 單個手機號碼
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 需要替換的變量JSON
        /// </summary>
        public string tpContent { get; set; }
    }
}
