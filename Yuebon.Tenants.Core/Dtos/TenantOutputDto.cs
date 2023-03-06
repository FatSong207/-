using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.Tenants.Dtos
{
    /// <summary>
    /// 租戶輸出對象模型
    /// </summary>
    [Serializable]
    public class TenantOutputDto
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(50)]
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取租戶名稱
        /// </summary>
        [MaxLength(50)]
        public string TenantName { get; set; }

        /// <summary>
        /// 設置或獲取公司名稱
        /// </summary>
        [MaxLength(50)]
        public string CompanyName { get; set; }

        /// <summary>
        /// 設置或獲取訪問域名
        /// </summary>
        [MaxLength(200)]
        public string HostDomain { get; set; }

        /// <summary>
        /// 設置或獲取聯系人
        /// </summary>
        [MaxLength(50)]
        public string LinkMan { get; set; }

        /// <summary>
        /// 設置或獲取聯系電話
        /// </summary>
        [MaxLength(50)]
        public string Telphone { get; set; }

        /// <summary>
        /// 設置或獲取數據源，分庫使用
        /// </summary>
        [MaxLength(200)]
        public string DataSource { get; set; }

        /// <summary>
        /// 設置或獲取租戶介紹
        /// </summary>
        [MaxLength(200)]
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
