using System;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Tenants.Models
{
    /// <summary>
    /// 租戶，數據實體對象
    /// </summary>
    [Table("Sys_Tenant")]
    [Serializable]
    public class Tenant:BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    {

        /// <summary>
        /// 設置或獲取租戶帳號
        /// </summary>
        public string TenantName { get; set; }

        /// <summary>
        /// 設置或獲取公司名稱
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 設置或獲取訪問域名
        /// </summary>
        public string HostDomain { get; set; }

        /// <summary>
        /// 設置或獲取租戶Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 設置或獲取聯系人
        /// </summary>
        public string LinkMan { get; set; }

        /// <summary>
        /// 設置或獲取聯系電話
        /// </summary>
        public string Telphone { get; set; }

        /// <summary>
        /// 設置或獲取數據源，分庫使用
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// 設置或獲取租戶介紹
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
