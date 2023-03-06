using System;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸入對象模型
    /// </summary>
    [Serializable]
    public class SignatureInPutDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

        public string Vno { get; set; }

        /// <summary>
        /// 表單編號
        /// </summary>
        public virtual string FName { get; set; }

        /// <summary>
        /// 產權建物門牌地址：戶籍地址
        /// </summary>
        public virtual string BAdd { get; set; }

        public virtual string SignatureBase64_1 { get; set; }
        public virtual string SignatureBase64_2 { get; set; }
        public virtual string SignatureBase64_3 { get; set; }
        public virtual string SignatureBase64_4 { get; set; }
        public virtual string SignatureBase64_5 { get; set; }

    }
}