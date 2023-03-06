using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 內部表單輸出對象模型
    /// </summary>
    [Serializable]
    public class SecurityFormOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }

        /// 設置或獲取 
        /// </summary>
        [MaxLength(40)]
        public string FormId { get; set; }

        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string Vno { get; set; }

        /// 設置或獲取 
        /// </summary>
        [MaxLength(200)]
        public string FormName { get; set; }

        /// 設置或獲取 
        /// </summary>
        [MaxLength(20)]
        public string TBNO { get; set; }

        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string Dept { get; set; }

        /// 設置或獲取 
        /// </summary>
        [MaxLength(40)]
        public string Type { get; set; }

        /// 設置或獲取 
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string CreatorUserId { get; set; }

        /// 設置或獲取 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string LastModifyUserId { get; set; }

    }
}