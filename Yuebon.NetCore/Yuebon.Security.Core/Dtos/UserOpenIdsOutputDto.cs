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
    public class UserOpenIdsOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string UserId { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(256)]
        public string OpenIdType { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(128)]
        public string OpenId { get; set; }

    }
}
