using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

using Yuebon.Commons.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yuebon.Security.Models
{
    /// <summary>
    /// 單據編碼，數據實體對象
    /// </summary>
    [Table("Sys_Sequence")]
    [Serializable]
    public class Sequence:BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 設置或獲取名稱
        /// </summary>
        public string SequenceName { get; set; }

        /// <summary>
        /// 設置或獲取分隔符
        /// </summary>
        public string SequenceDelimiter { get; set; }

        /// <summary>
        /// 設置或獲取序號重置規則
        /// </summary>
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
        public string CurrentCode { get; set; }

        /// <summary>
        /// 設置或獲取當前重置依賴，即最后一次獲取編碼的日期
        /// </summary>
        public string CurrentReset { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
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
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 設置或獲取創建人組織
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 設置或獲取部門
        /// </summary>
        public string DeptId { get; set; }

        /// <summary>
        /// 設置或獲取修改時間
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 設置或獲取修改人
        /// </summary>
        public string LastModifyUserId { get; set; }

        /// <summary>
        /// 設置或獲取刪除時間
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 設置或獲取刪除人
        /// </summary>
        public string DeleteUserId { get; set; }


    }
}
