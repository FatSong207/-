using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.Security.Dtos
{
    /// <summary>
    /// 輸出對象模型
    /// </summary>
    [Serializable]
    public class RoleDataOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string RoleId { get; set; }

        /// <summary>
        /// 類型，company-公司，dept-部門，person-個人
        /// </summary>
        public virtual string DType { get; set; }

        /// <summary>
        /// 數據數據，部門ID或個人ID
        /// </summary>
        public virtual string AuthorizeData { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(1073741823)]
        public string Note { get; set; }


    }
}
