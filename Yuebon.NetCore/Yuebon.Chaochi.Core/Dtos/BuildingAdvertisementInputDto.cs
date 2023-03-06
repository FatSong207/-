using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 建物廣告管理輸入對象模型
    /// </summary>
    [AutoMap(typeof(BuildingAdvertisement))]
    [Serializable]
    public class BuildingAdvertisementInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取建物地址
        /// </summary>
        public string BAdd { get; set; }
        /// <summary>
        /// 設置或獲取建物廣告狀態
        /// </summary>
        public string BAStatus { get; set; }
        /// <summary>
        /// 設置或獲取待招租起始日
        /// </summary>
        public DateTime? BRStartDate { get; set; }        /// <summary>
        /// 設置或獲取廣告刊登日
        /// </summary>
        public DateTime? BAStartDate { get; set; }        /// <summary>
        /// 設置或獲取上架天數
        /// </summary>
        public string BADays { get; set; }
        /// <summary>
        /// 設置或獲取招租天數
        /// </summary>
        public string BRDays { get; set; }
        /// <summary>
        /// 設置或獲取廣告網址
        /// </summary>
        public string BAURL { get; set; }
        /// <summary>
        /// 設置或獲取創建人所屬組織ID
        /// </summary>
        public string CreatorUserOrgId { get; set; }
        /// <summary>
        /// 設置或獲取創建人所屬部門ID
        /// </summary>
        public string CreatorUserDeptId { get; set; }
        // 建物狀態
        public string BState { get; set; }
    }
}
