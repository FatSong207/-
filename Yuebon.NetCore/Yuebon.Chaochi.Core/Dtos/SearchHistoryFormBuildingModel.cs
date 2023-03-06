using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Dtos;
using Yuebon.Chaochi.Models;
using Yuebon.Chaochi.Core.Models;

namespace Yuebon.Chaochi.Dtos
{

    /// <summary>
    /// 用戶搜索條件
    /// </summary>
    public class SearchHistoryFormBuildingModel : SearchInputDto<HistoryFormBuilding>
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId
        {
            get; set;
        }
        /// <summary>
        /// 註冊或添加時間 開始
        /// </summary>
        public string CreatorTime1
        {
            get; set;
        }
        /// <summary>
        /// 註冊或添加時間 結束
        /// </summary>
        public string CreatorTime2
        {
            get; set;
        }

    }
}
