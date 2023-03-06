using System;
using System.ComponentModel.DataAnnotations;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸出對象模型
    /// </summary>
    [Serializable]
    public class ContractformOutputDto
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取表單編號
        /// </summary>
        [MaxLength(40)]
        public string FormId { get; set; }
        /// <summary>
        /// 設置或獲取表單版本
        /// </summary>
        [MaxLength(20)]
        public string Vno { get; set; }
        /// <summary>
        /// 設置或獲取表單名稱
        /// </summary>
        [MaxLength(100)]
        public string FormName { get; set; }
        /// <summary>
        /// 設置或獲取合約分類Id
        /// </summary>
        [MaxLength(50)]        public string CateId { get; set; }        /// <summary>
        /// 設置或獲取合約分類
        /// </summary>
        [MaxLength(500)]
        public string Level { get; set; }
        /// <summary>
        /// 設置或獲取創建日期
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 設置或獲取創建用戶ID
        /// </summary>
        [MaxLength(100)]
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 設置或獲取最後修改時間
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 設置或獲取最後修改用戶ID
        /// </summary>
        [MaxLength(100)]
        public string LastModifyUserId { get; set; }
        /// <summary>
        /// 設置或獲取刪除標誌
        /// </summary>
        public bool? DeleteMark { get; set; }

        /// <summary>
        /// 設置或獲取刪除時間
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 設置或獲取刪除用戶ID
        /// </summary>
        public string DeleteUserId { get; set; }
    }
}
