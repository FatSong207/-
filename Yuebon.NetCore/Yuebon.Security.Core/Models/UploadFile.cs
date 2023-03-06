
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Yuebon.Commons.Models;
using Yuebon.Commons.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yuebon.Security.Models
{
    /// <summary>
	/// 文件
	/// </summary>
    [Table("Sys_UploadFile")]
    public class UploadFile : BaseEntity<string>, ICreationAudited
    {
        /// <summary>
        /// 
        /// </summary>
        public UploadFile()
        {
            this.Id = GuidUtils.CreateNo();
            this.FileName = string.Empty;
            this.FilePath = string.Empty;
            this.Description = string.Empty;
            this.FileType = string.Empty;
            this.Extension = string.Empty;
            this.SortCode = 0;
            this.CreatorTime = DateTime.Now;
            this.Thumbnail = string.Empty;
            this.BelongApp = string.Empty;
            this.BelongAppId = string.Empty;
            this.EnabledMark = true;
            this.DeleteMark = false;
        }

        /// <summary>
	    /// 文件名稱
	    /// </summary>
        public string FileName { get; set; }
        /// <summary>
	    /// 文件路徑
	    /// </summary>
        public string FilePath { get; set; }
        /// <summary>
	    /// 描述
	    /// </summary>
        public string Description { get; set; }
        /// <summary>
	    /// 文件類型
	    /// </summary>
        public string FileType { get; set; }
        /// <summary>
	    /// 文件大小
	    /// </summary>
        public int FileSize { get; set; }
        /// <summary>
	    /// 擴展名稱
	    /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortCode { get; set; }
        /// <summary>
        /// 刪除標志
        /// </summary>
        public virtual bool? DeleteMark { get; set; }

        /// <summary>
        /// 有效標志
        /// </summary>
        public virtual bool? EnabledMark { get; set; }
        /// <summary>
        /// 創建用戶主鍵
        /// </summary>
        [MaxLength(50)]
        public virtual string CreatorUserId { get; set; }
        /// <summary>
	    /// 上傳時間
	    /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
	    /// 縮略圖
	    /// </summary>
        public string Thumbnail { get; set; }
        /// <summary>
	    /// 所屬應用
	    /// </summary>
        public string BelongApp { get; set; }
        /// <summary>
	    /// 所屬應用ID
	    /// </summary>
        public string BelongAppId { get; set; }
    }
}
