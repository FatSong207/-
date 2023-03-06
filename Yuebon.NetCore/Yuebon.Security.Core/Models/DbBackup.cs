using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.Security.Models
{
    /// <summary>
    /// 數據庫備份，數據實體對象
    /// </summary>

    [Table("Sys_DbBackup")]
    [Serializable]
    public class DbBackup : BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    { 
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
	    public DbBackup()
		{

 		}

        #region Property Members
        /// <summary>
        /// 備份類型
        /// </summary>
        public virtual string BackupType { get; set; }

        /// <summary>
        /// 數據庫名稱
        /// </summary>
        public virtual string DbName { get; set; }

        /// <summary>
        /// 文件名稱
        /// </summary>
        public virtual string FileName { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public virtual string FileSize { get; set; }

        /// <summary>
        /// 文件路徑
        /// </summary>
        public virtual string FilePath { get; set; }

        /// <summary>
        /// 備份時間
        /// </summary>
        public virtual DateTime? BackupTime { get; set; }

        /// <summary>
        /// 排序碼
        /// </summary>
        public virtual int? SortCode { get; set; }


        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }


        /// <summary>
        /// 刪除標志
        /// </summary>
        public virtual bool? DeleteMark { get; set; }

        /// <summary>
        /// 有效標志
        /// </summary>
        public virtual bool EnabledMark { get; set; }

        /// <summary>
        /// 創建日期
        /// </summary>
        public virtual DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 創建用戶主鍵
        /// </summary>
        [MaxLength(50)]
        public virtual string CreatorUserId { get; set; }

        /// <summary>
        /// 最后修改時間
        /// </summary>
        public virtual DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用戶
        /// </summary>
        [MaxLength(50)]
        public virtual string LastModifyUserId { get; set; }

        /// <summary>
        /// 刪除時間
        /// </summary>
        public virtual DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 刪除用戶
        /// </summary>
        [MaxLength(50)]
        public virtual string DeleteUserId { get; set; }
        #endregion

    }
}