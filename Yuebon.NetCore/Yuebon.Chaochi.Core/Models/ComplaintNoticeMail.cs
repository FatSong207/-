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
    [Table("Chaochi_ComplaintNoticeMail")]
    [Serializable]
    public class ComplaintNoticeMail:BaseEntity<string>
    {
        /// <summary>
        /// 設置或獲取投訴單編號
        /// </summary>
        public string CCode { get; set; }

        /// <summary>
        /// 設置或獲取部門Id
        /// </summary>
        public string DeptId { get; set; }
        public string DeptName { get; set; }

        /// <summary>
        /// 設置或獲取UserId
        /// </summary>
        public string UserId { get; set; }
        public string UserName { get; set; }

    }
}
