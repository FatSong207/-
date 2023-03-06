
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 客戶表_房東_法人，數據實體對象
    /// </summary>
    [Table("Chaochi_CustomerLJ")]
    [Serializable]
    public class CustomerLJ : BaseEntity<string>, ICreationAudited, IModificationAudited
    { 
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
	    public CustomerLJ()
        {
            
        }

        #region Property Members
        /// <summary>
        /// 統一編號
        /// </summary>
        public virtual string Account { get; set; }

        /// <summary>
        /// 公司全名
        /// </summary>
        public virtual string RealName { get; set; }

        /// <summary>
        /// 公司簡稱
        /// </summary>
        public virtual string NickName { get; set; }

        /// <summary>
        /// 負責人
        /// </summary>
        public virtual string PersonInCharge { get; set; }

        /// <summary>
        /// 公司電話(代表號)
        /// </summary>
        public virtual string Telephone { get; set; }

        /// <summary>
        /// 郵箱
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// 縣市(含直轄市)(聯絡)
        /// </summary>
        public virtual string CountyCity { get; set; }

        /// <summary>
        /// 鄉鎮市區(聯絡)
        /// </summary>
        public virtual string TownshipDistrict { get; set; }

        /// <summary>
        /// 村里(聯絡)
        /// </summary>
        public virtual string Village { get; set; }

        /// <summary>
        /// 鄰(聯絡)
        /// </summary>
        public virtual string Neighborhood { get; set; }

        /// <summary>
        /// 道路名稱(聯絡)
        /// </summary>
        public virtual string RoadStreet { get; set; }

        /// <summary>
        /// 其餘地址(聯絡)
        /// </summary>
        public virtual string OtherAddress { get; set; }

        /// <summary>
        /// 縣市(含直轄市)(登記)
        /// </summary>
        public virtual string CountyCityReg { get; set; }

        /// <summary>
        /// 鄉鎮市區(登記)
        /// </summary>
        public virtual string TownshipDistrictReg { get; set; }

        /// <summary>
        /// 村里(登記)
        /// </summary>
        public virtual string VillageReg { get; set; }

        /// <summary>
        /// 鄰(登記)
        /// </summary>
        public virtual string NeighborhoodReg { get; set; }

        /// <summary>
        /// 道路名稱(登記)
        /// </summary>
        public virtual string RoadStreetReg { get; set; }

        /// <summary>
        /// 其餘地址(登記)
        /// </summary>
        public virtual string OtherAddressReg { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

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
        /// 最後修改時間
        /// </summary>
        public virtual DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最後修改用戶
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