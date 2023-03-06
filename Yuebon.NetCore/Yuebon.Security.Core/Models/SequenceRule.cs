using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

using Yuebon.Commons.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yuebon.Security.Models
{
    /// <summary>
    /// 序號編碼規則表，數據實體對象
    /// </summary>
    [Table("Sys_SequenceRule")]
    [Serializable]
    public class SequenceRule:BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 設置或獲取編碼規則名稱
        /// </summary>
        public string SequenceName { get; set; }

        /// <summary>
        /// 設置或獲取規則排序
        /// </summary>
        public int RuleOrder { get; set; }

        /// <summary>
        /// 設置或獲取規則類別，timestamp、const、bumber
        /// </summary>
        public string RuleType { get; set; }

        /// <summary>
        /// 設置或獲取規則參數，如YYMMDD
        /// </summary>
        public string RuleValue { get; set; }

        /// <summary>
        /// 設置或獲取補齊方向，left或right
        /// </summary>
        public string PaddingSide { get; set; }

        /// <summary>
        /// 設置或獲取補齊寬度
        /// </summary>
        public int PaddingWidth { get; set; }

        /// <summary>
        /// 設置或獲取填充字符
        /// </summary>
        public string PaddingChar { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 設置或獲取是否可用
        /// </summary>
        public bool EnabledMark { get; set; }

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
