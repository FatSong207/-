using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.Security.Dtos
{
    /// <summary>
    /// 單據編碼輸出對象模型
    /// </summary>
    [Serializable]
    public class SequenceOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取名稱
        /// </summary>
        [MaxLength(50)]
        public string SequenceName { get; set; }

        /// <summary>
        /// 設置或獲取分隔符
        /// </summary>
        [MaxLength(50)]
        public string SequenceDelimiter { get; set; }

        /// <summary>
        /// 設置或獲取序號重置規則
        /// </summary>
        [MaxLength(50)]
        public string SequenceReset { get; set; }

        /// <summary>
        /// 設置或獲取步長
        /// </summary>
        public int Step { get; set; }

        /// <summary>
        /// 設置或獲取當前值
        /// </summary>
        public int CurrentNo { get; set; }

        /// <summary>
        /// 設置或獲取當前編碼
        /// </summary>
        [MaxLength(200)]
        public string CurrentCode { get; set; }

        /// <summary>
        /// 設置或獲取當前重置依賴
        /// </summary>
        [MaxLength(50)]
        public string CurrentReset { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(200)]
        public string Description { get; set; }

        /// <summary>
        /// 設置或獲取是否可用
        /// </summary>
        public bool? EnabledMark { get; set; }

        /// <summary>
        /// 設置或獲取刪除標記
        /// </summary>
        public bool? DeleteMark { get; set; }

        /// <summary>
        /// 設置或獲取創建時間
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 設置或獲取創建人
        /// </summary>
        [MaxLength(50)]
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 設置或獲取創建人組織
        /// </summary>
        [MaxLength(50)]
        public string CompanyId { get; set; }

        /// <summary>
        /// 設置或獲取部門
        /// </summary>
        [MaxLength(50)]
        public string DeptId { get; set; }

        /// <summary>
        /// 設置或獲取修改時間
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 設置或獲取修改人
        /// </summary>
        [MaxLength(50)]
        public string LastModifyUserId { get; set; }

        /// <summary>
        /// 設置或獲取刪除時間
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 設置或獲取刪除人
        /// </summary>
        [MaxLength(50)]
        public string DeleteUserId { get; set; }


    }
}
