using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// ，數據實體對象
    /// </summary>
    [Table("Chaochi_RemittanceR")]
    [Serializable]
    public class RemittanceR:BaseEntity<string>, ICreationAudited, IModificationAudited
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string CustomerRId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string IDNo { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RAName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RAID { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RBankName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RBankNo { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RBranchName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RBranchNo { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string RANo { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string LastModifyUserId { get; set; }


    }
}
