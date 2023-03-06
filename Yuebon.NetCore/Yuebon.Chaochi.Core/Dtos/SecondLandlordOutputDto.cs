using System;
using System.ComponentModel.DataAnnotations;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 二房東輸出對象模型
    /// </summary>
    [Serializable]
    public class SecondLandlordOutputDto
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
        public string SLID { get; set; }
        /// <summary>
        /// 設置或獲取公司名稱
        /// </summary>
        [MaxLength(200)]
        public string SLName { get; set; }
        /// <summary>
        /// 設置或獲取負責人姓名
        /// </summary>
        [MaxLength(200)]
        public string SLRep { get; set; }
        /// <summary>
        /// 設置或獲取許可字號/登記證字號
        /// </summary>
        [MaxLength(100)]
        public string SLLRNo { get; set; }
        /// <summary>
        /// 設置或獲取營業地址
        /// </summary>
        [MaxLength(100)]
        public string SLAdd { get; set; }
        /// <summary>
        /// 設置或獲取聯絡電話
        /// </summary>
        [MaxLength(100)]
        public string SLTel { get; set; }

    }
}
