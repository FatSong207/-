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
    [Table("Chaochi_RemittanceL")]
    [Serializable]
    public class RemittanceL:BaseEntity<string>, ICreationAudited, IModificationAudited
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string CustomerLId { get; set; }

        /// <summary>
        /// 所屬人身分證字號或統編
        /// </summary>
        public string IDNo { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string LAName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string LAID { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string LBankName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string LBankNo { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string LBranchName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string LBranchNo { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string LANo { get; set; }

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
