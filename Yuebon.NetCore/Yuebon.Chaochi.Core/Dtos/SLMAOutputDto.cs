using System;
using System.ComponentModel.DataAnnotations;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸出對象模型
    /// </summary>
    [Serializable]
    public class SLMAOutputDto
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
        public string SLMAID { get; set; }
        /// <summary>
        /// 設置或獲取公司名稱
        /// </summary>
        [MaxLength(200)]
        public string Name { get; set; }
        /// <summary>
        /// 設置或獲取負責人姓名
        /// </summary>
        [MaxLength(100)]
        public string Rep { get; set; }
        /// <summary>
        /// 設置或獲取許可字號/登記證字號
        /// </summary>
        [MaxLength(100)]
        public string LRNo { get; set; }
        /// <summary>
        /// 設置或獲取營業地址
        /// </summary>
        [MaxLength(100)]
        public string Address { get; set; }
        /// <summary>
        /// 設置或獲取聯絡電話
        /// </summary>
        [MaxLength(100)]
        public string Tel { get; set; }

        /// <summary>
        /// 設置或獲取傳真號碼
        /// </summary>
        [MaxLength(50)]
        public string Fax { get; set; }
        /// <summary>
        /// 設置或獲取電子郵件信箱
        /// </summary>
        [MaxLength(50)]
        public string Email { get; set; }
        /// <summary>
        /// 設置或獲取租賃住宅管理人員
        /// </summary>
        [MaxLength(200)]
        public string SIName { get; set; }
        /// <summary>
        /// 設置或獲取租賃住宅管理人員-證書字號
        /// </summary>
        [MaxLength(100)]
        public string SILRNo { get; set; }
        /// <summary>
        /// 設置或獲取租賃住宅管理人員-通訊地址
        /// </summary>
        [MaxLength(100)]
        public string SIAdd { get; set; }
        /// <summary>
        /// 設置或獲取租賃住宅管理人員-聯絡電話
        /// </summary>
        [MaxLength(100)]
        public string SITel { get; set; }
        /// <summary>
        /// 設置或獲取租賃住宅管理人員-電子郵件信箱
        /// </summary>
        [MaxLength(50)]
        public string SIEmail { get; set; }
        /// <summary>
        /// 設置或獲取不動產經紀人-統一編號(身分證明文件編號)
        /// </summary>
        [MaxLength(100)]
        public string AGID { get; set; }
        /// <summary>
        /// 設置或獲取不動產經紀人
        /// </summary>
        [MaxLength(200)]
        public string AGName { get; set; }
        /// <summary>
        /// 設置或獲取不動產經紀人-證書字號
        /// </summary>
        [MaxLength(100)]
        public string AGLRNo { get; set; }
        /// <summary>
        /// 設置或獲取不動產經紀人-通訊地址
        /// </summary>
        [MaxLength(100)]
        public string AGAdd { get; set; }
        /// <summary>
        /// 設置或獲取不動產經紀人-聯絡電話
        /// </summary>
        [MaxLength(20)]
        public string AGTel { get; set; }
        /// <summary>
        /// 設置或獲取不動產經紀人-電子郵件信箱
        /// </summary>
        [MaxLength(50)]
        public string AGEmail { get; set; }
        /// <summary>
        /// 設置或獲取創建日期
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 設置或獲取創建用戶主鍵
        /// </summary>
        [MaxLength(50)]
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 設置或獲取最後修改時間
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 設置或獲取最後修改用戶
        /// </summary>
        [MaxLength(50)]
        public string LastModifyUserId { get; set; }

    }
}
