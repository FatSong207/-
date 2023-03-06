using System;
using System.ComponentModel.DataAnnotations;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 管理方輸出對象模型
    /// </summary>
    [Serializable]
    public class ManagementOutputDto
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取統一編號
        /// </summary>
        [MaxLength(100)]
        public string MAID { get; set; }
        /// <summary>
        /// 設置或獲取公司名稱
        /// </summary>
        [MaxLength(200)]
        public string MName { get; set; }
        /// <summary>
        /// 設置或獲取負責人姓名
        /// </summary>
        [MaxLength(200)]
        public string MRep { get; set; }
        /// <summary>
        /// 設置或獲取許可字號/登記證字號
        /// </summary>
        [MaxLength(100)]
        public string MLRNo { get; set; }
        /// <summary>
        /// 設置或獲取營業地址
        /// </summary>
        [MaxLength(100)]
        public string MAdd { get; set; }
        /// <summary>
        /// 設置或獲取聯絡電話
        /// </summary>
        [MaxLength(100)]
        public string MTel { get; set; }
        /// <summary>
        /// 設置或獲取電子郵件信箱
        /// </summary>
        [MaxLength(50)]
        public string MEmail { get; set; }

    }
}
