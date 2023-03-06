using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.SMS.Zutong
{
    public class SMSResult
    {
        /// <summary>
        /// 請求狀態碼。返回200代表請求成功。
        /// 其他錯誤碼詳見錯誤碼列表。
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 狀態碼的描述。
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 消息ID
        /// </summary>
        public long MsgId { get; set; }
    }
}
