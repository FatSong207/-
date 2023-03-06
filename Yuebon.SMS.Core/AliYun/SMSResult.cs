using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.SMS.AliYun
{
    public class SMSResult
    {
        /// <summary>
        /// 發送回執ID
        /// </summary>
        public string BizId { get; set; }
        /// <summary>
        /// 請求狀態碼。返回OK代表請求成功。
        /// 其他錯誤碼詳見錯誤碼列表。
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 狀態碼的描述。
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 請求ID。
        /// </summary>
        public string RequestId { get; set; }
    }
}
