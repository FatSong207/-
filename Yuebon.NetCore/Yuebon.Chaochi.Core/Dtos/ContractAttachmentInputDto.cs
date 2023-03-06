using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 合約附件輸入對象模型
    /// </summary>
    [AutoMap(typeof(ContractAttachment))]
    [Serializable]
    public class ContractAttachmentInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取合約編號
        /// </summary>
        public string CID { get; set; }
        /// <summary>
        /// 設置或獲取要件名稱
        /// </summary>
        public string FormName { get; set; }
        /// <summary>
        /// 設置或獲取表單歸屬
        /// </summary>
        public string ArchiveTo { get; set; }

        /// <summary>
        /// 設置或獲取表單歸屬ID
        /// </summary>
        public string ArchiveID { get; set; }
        /// <summary>
        /// 設置或獲取附件狀況
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 設置或獲取綁定日期
        /// </summary>
        public DateTime? UploadTime { get; set; }

        /// <summary>
        /// 設置或獲取異動人員
        /// </summary>
        public string UploadUserId { get; set; }
        public string CType { get; set; }        public string CCate { get; set; }        public string FileName { get; set; }
    }
}
