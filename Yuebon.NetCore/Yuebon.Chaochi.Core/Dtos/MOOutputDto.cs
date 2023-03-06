using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸出對象模型
    /// </summary>
    [Serializable]
    public class MOOutputDto
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取多物件單編號
        /// </summary>
        [MaxLength(100)]
        public string MOID { get; set; }
        /// <summary>
        /// 設置或獲取多物件名稱
        /// </summary>
        [MaxLength(200)]
        public string MOName { get; set; }
        /// <summary>
        /// 設置或獲取原建物地址
        /// </summary>
        [MaxLength(1000)]        public string OGBuildingName { get; set; }        /// <summary>
        /// 設置或獲取出租人-身份證號或統一編號
        /// </summary>
        [MaxLength(100)]
        public string LSID { get; set; }
        /// <summary>
        /// 設置或獲取出租人-姓名/法人名稱
        /// </summary>
        [MaxLength(100)]
        public string LSName { get; set; }
        /// <summary>
        /// 設置或獲取二房東-統一編號
        /// </summary>
        [MaxLength(100)]
        public string SLID { get; set; }
        /// <summary>
        /// 設置或獲取二房東-公司名稱
        /// </summary>
        [MaxLength(200)]
        public string SLName { get; set; }
        /// <summary>
        /// 設置或獲取合約狀態
        /// </summary>
        [MaxLength(30)]
        public string Status { get; set; }
        /// <summary>
        /// 設置或獲取創建時間
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 設置或獲取創建用戶ID
        /// </summary>
        [MaxLength(100)]
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 設置或獲取創建人所屬組織ID
        /// </summary>
        [MaxLength(100)]
        public string CreatorUserOrgId { get; set; }
        /// <summary>
        /// 設置或獲取創建人所屬部門ID
        /// </summary>
        [MaxLength(100)]
        public string CreatorUserDeptId { get; set; }
        /// <summary>
        /// 設置或獲取刪除標誌
        /// </summary>
        public bool? DeleteMark { get; set; }
        /// <summary>
        /// 設置或獲取刪除時間
        /// </summary>
        public DateTime? DeleteTime { get; set; }
        /// <summary>
        /// 設置或獲取刪除用戶ID
        /// </summary>
        [MaxLength(500)]
        public string DeleteUserId { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        [MaxLength(100)]
        public string LastModifyUserId { get; set; }


        // 物件清單
        public List<MOBuildingOutputDto> BuildingList { get; set; }

        public string RealName { get; set; }

    }
}
