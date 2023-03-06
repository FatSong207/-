using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.Chaochi.Models;
using System.Collections.Generic;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸入對象模型
    /// </summary>
    [AutoMap(typeof(MO))]
    [Serializable]
    public class MOInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取多物件單編號
        /// </summary>
        public string MOID { get; set; }
        /// <summary>
        /// 設置或獲取多物件名稱
        /// </summary>
        public string MOName { get; set; }

        /// <summary>
        /// 設置或獲取原建物地址
        /// </summary>
        public string OGBuildingName { get; set; }
        /// <summary>
        /// 設置或獲取出租人-身份證號或統一編號
        /// </summary>
        public string LSID { get; set; }
        /// <summary>
        /// 設置或獲取出租人-姓名/法人名稱
        /// </summary>
        public string LSName { get; set; }
        /// <summary>
        /// 設置或獲取二房東-統一編號
        /// </summary>
        public string SLID { get; set; }
        /// <summary>
        /// 設置或獲取二房東-公司名稱
        /// </summary>
        public string SLName { get; set; }
        /// <summary>
        /// 設置或獲取合約狀態
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 設置或獲取創建人所屬組織ID
        /// </summary>
        public string CreatorUserOrgId { get; set; }
        /// <summary>
        /// 設置或獲取創建人所屬部門ID
        /// </summary>
        public string CreatorUserDeptId { get; set; }

        // 物件清單
        public List<MOBuildingInputDto> BuildingList { get; set; }

    }
}
