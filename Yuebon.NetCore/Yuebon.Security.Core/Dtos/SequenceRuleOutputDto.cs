using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.Security.Dtos
{
    /// <summary>
    /// 序號編碼規則表輸出對象模型
    /// </summary>
    [Serializable]
    public class SequenceRuleOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取編碼規則名稱
        /// </summary>
        [MaxLength(200)]
        public string SequenceName { get; set; }

        /// <summary>
        /// 設置或獲取規則排序
        /// </summary>
        public int RuleOrder { get; set; }

        /// <summary>
        /// 設置或獲取規則類別，timestamp、const、bumber
        /// </summary>
        [MaxLength(200)]
        public string RuleType { get; set; }

        /// <summary>
        /// 設置或獲取規則參數，如YYMMDD
        /// </summary>
        [MaxLength(200)]
        public string RuleValue { get; set; }

        /// <summary>
        /// 設置或獲取補齊方向，left或right
        /// </summary>
        [MaxLength(200)]
        public string PaddingSide { get; set; }

        /// <summary>
        /// 設置或獲取補齊寬度
        /// </summary>
        public int PaddingWidth { get; set; }

        /// <summary>
        /// 設置或獲取填充字符
        /// </summary>
        [MaxLength(1)]
        public string PaddingChar { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(200)]
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
