using System;

namespace Yuebon.Chaochi.Core.Models
{
    [Serializable]
    public class BLOB
    {
        /// <summary>
        /// 設置或獲取內容型態
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// 設置或獲取內容
        /// </summary>
        public byte[] Content { get; set; }
    }
}
