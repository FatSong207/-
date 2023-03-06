using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yuebon.WebApi.Areas.WeiXin.Models
{
    /// <summary>
    /// 內容安全校驗
    /// </summary>
    [Serializable]
    public class CheckMsgModel
    {
        /// <summary>
        /// 校驗內容
        /// </summary>
        public string ContenText {
            get;
            set;
        }

    }
}
