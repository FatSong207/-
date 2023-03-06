using System;
using System.ComponentModel.DataAnnotations;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 待辦事項輸出對象模型
    /// </summary>
    [Serializable]
    public class ToDoListOutputDto
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取功能類型
        /// </summary>
        [MaxLength(100)]
        public string Type { get; set; }
        /// <summary>
        /// 設置或獲取功能模組ID
        /// </summary>
        [MaxLength(100)]
        public string TypeId { get; set; }

        /// <summary>
        /// 設置或獲取功能模組資料
        /// </summary>
        [MaxLength(100)]
        public string TypeData { get; set; }
        /// <summary>
        /// 設置或獲取待辧事項名稱
        /// </summary>
        [MaxLength(200)]
        public string Name { get; set; }
        /// <summary>
        /// 設置或獲取狀態
        /// </summary>
        [MaxLength(10)]
        public string Status { get; set; }

        /// <summary>
        /// 設置或獲取用戶ID
        /// </summary>
        [MaxLength(50)]
        public string Account { get; set; }

        /// <summary>
        /// 設置或獲取審核意見
        /// </summary>
        [MaxLength(4000)]
        public string Memo { get; set; }
        /// <summary>
        /// 設置或獲取創建時間
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

    }
}
