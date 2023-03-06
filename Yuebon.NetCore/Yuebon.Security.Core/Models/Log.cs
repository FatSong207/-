
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Yuebon.Commons.Core.DataManager;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Models;


namespace Yuebon.Security.Models
{
    /// <summary>
    /// 系統日志，數據實體對象
    /// </summary>
    [AppDBContext("DefaultDb")]
    [Table("Sys_Log")]
    [Serializable]
    public class Log: BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    { 
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
	    public Log()
		{
            this.Id = GuidUtils.CreateNo();
            this.EnabledMark = true;
            this.DeleteMark = false;
            this.CreatorTime = DateTime.Now;
 		}

        #region Property Members

       

        /// <summary>
        /// 日期
        /// </summary>
        public virtual DateTime? Date { get; set; }

        /// <summary>
        /// 用戶名
        /// </summary>
        public virtual string Account { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string NickName { get; set; }

        /// <summary>
        /// 組織主鍵
        /// </summary>
        public virtual string OrganizeId { get; set; }
        /// <summary>
        /// 類型
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public virtual string IPAddress { get; set; }

        /// <summary>
        /// IP所在城市
        /// </summary>
        public virtual string IPAddressName { get; set; }

        /// <summary>
        /// 系統模塊Id
        /// </summary>
        public virtual string ModuleId { get; set; }

        /// <summary>
        /// 系統模塊
        /// </summary>
        public virtual string ModuleName { get; set; }

        /// <summary>
        /// 結果
        /// </summary>
        public virtual bool? Result { get; set; }

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