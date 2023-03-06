using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸出對象模型
    /// </summary>
    [Serializable]
    public class ComplaintNoticeMailOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取投訴單編號
        /// </summary>
        [MaxLength(100)]
        public string CCode { get; set; }

        public string DeptName { get; set; }

        public string UserName { get; set; }

        ///// <summary>
        ///// 設置或獲取部門Id
        ///// </summary>
        //[MaxLength(100)]
        //public string DeptId { get; set; }

        ///// <summary>
        ///// 設置或獲取UserId
        ///// </summary>
        //[MaxLength(100)]
        //public string UserId { get; set; }


    }
}
