using System;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 待辦事項，數據實體對象
    /// </summary>
    [Table("Chaochi_ToDoList")]
    [Serializable]
    public class ToDoList:BaseEntity<string>, ICreationAudited, IModificationAudited
    {
        /// <summary>
        /// 設置或獲取功能類型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 設置或獲取功能模組ID
        /// </summary>
        public string TypeId { get; set; }
        /// <summary>
        /// 設置或獲取功能模組資料
        /// </summary>
        public string TypeData { get; set; }        /// <summary>
        /// 設置或獲取待辧事項名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 設置或獲取狀態
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 設置或獲取用戶ID
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 設置或獲取審核意見
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 設置或獲取創建時間
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 設置或獲取創建用戶ID
        /// </summary>
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 設置或獲取最後修改時間
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 設置或獲取最後修改用戶ID
        /// </summary>
        public string LastModifyUserId { get; set; }

    }
}
